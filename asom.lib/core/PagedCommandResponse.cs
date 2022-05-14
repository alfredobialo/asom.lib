using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace asom.lib.core
{
    public class PagedCommandResponse<T>  : CommandResponse<T>
    {
        private int _totalRecord;

        public void SetPagerConfig(PagedDataCriteria criteria)
        {
            this.CurrentPage = criteria.CurrentPage;
            PageSize = criteria.PageSize;
            UsePagination = criteria.UsePagination;
            Criteria = criteria;
        }

        public bool UsePagination { get; internal set; } = true;
        public int PageSize { get; internal set; } = 10;
        public int CurrentPage { get; internal set; } = 1;

        public int TotalRecord
        {
            get => _totalRecord;
            private set => _totalRecord = value;
        }

        public void SetTotalRecord(int value) => _totalRecord = value < 0  ? 0 : value;

        private dynamic Criteria { get;  set; }
        public int TotalPages =>
            !UsePagination ? 1 : (int)(Math.Floor(this.TotalRecord / (double) this.PageSize) +
                                       (this.TotalRecord % this.PageSize > 0 ? 1 : 0));

        public IEnumerable<TSource> Paginate<TSource>(IEnumerable<TSource> source)
        {
            this.TotalRecord = source.Count<TSource>();
            PageSize = !UsePagination ? TotalRecord : PageSize;
            CurrentPage  =  CurrentPage < 1 ? 1 : CurrentPage;
            CurrentPage = CurrentPage > TotalPages ? TotalPages : CurrentPage;
            return this.UsePagination
                ? source.Skip<TSource>(this.PageSize * (this.CurrentPage - 1)).Take<TSource>(this.PageSize)
                : source;
        }
        public IQueryable<TSource> Paginate<TSource>(IQueryable<TSource> source)
        {
            this.TotalRecord = source.Count<TSource>();
            PageSize = !UsePagination ? TotalRecord : PageSize;
            CurrentPage  =  CurrentPage < 1 ? 1 : CurrentPage;
            CurrentPage = CurrentPage > TotalPages ? TotalPages : CurrentPage;
            return this.UsePagination
                ? source.Skip<TSource>(this.PageSize * (this.CurrentPage - 1)).Take<TSource>(this.PageSize)
                : source;
        }

        public static PagedCommandResponse<T> ExceptionThrown(Exception err, int statusCode = (int) HttpStatusCode.InternalServerError, string environment = "Development")
        {
            return new PagedCommandResponse<T>()
            {
                Success = false,
                Message = "An Error Occurred!",
                Code = statusCode
            };
        }
        public PagedCommandResponse<T1> Copy<T1>(T1 value = default)
        {
            return new PagedCommandResponse<T1>()
            {
                Data = value,
                Message = Message,
                Success = Success,
                Code = Code,
                Errors = Errors,
                CurrentPage =  CurrentPage,
                PageSize = PageSize,
                Criteria = Criteria,
                TotalRecord = TotalRecord,
                UsePagination = UsePagination,


            };
        }
    }
}
