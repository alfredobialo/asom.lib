using System;

namespace asom.lib.core
{
    public class PagedDataCriteria : Criteria
    {
        public bool UsePagination { get; set; } = true;
        public int PageSize { get; set; } = 10;
        public int CurrentPage { get; set; } = 1;

        public static PagedDataCriteria Default =>
            new PagedDataCriteria
            {
                CurrentPage = 1, UsePagination = true, PageSize = 10
            };
    }

    public class DateFilterCriteria : PagedDataCriteria
    {
        public bool UseDateRange { get; set; } = false;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public static DateFilterCriteria Default =>
            new DateFilterCriteria
            {
                CurrentPage = 1, UsePagination = true, UseDateRange = false, PageSize = 10
            };
    }
}
