
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace asom.lib.core
{
    public class CommandResponse
    {
        private bool _success;

        public bool Success
        {
            get => _success;
            set
            {
                _success = value;
                if (value)
                {
                    Errors = new List<ErrorInfo>();
                    Code = 200;
                }

            }
        }

        public string Message { get; set; }
        public bool HasErrors => Errors.Any();
        public IList<ErrorInfo> Errors { get; set; } = new List<ErrorInfo>();
        public int Code { get; set; }

        public static CommandResponse Failure(string message = "Failed Action")
        {
            return new CommandResponse()
            {
                Message = message,
                Success = false,
                Errors = new List<ErrorInfo>()
            };
        }

        public static CommandResponse Successful(string message = "Successful")
        {
            return new CommandResponse()
            {
                Message = message,
                Success = true,
                Code  = 200,
                Errors = new List<ErrorInfo>()
            };
        }

        public void ClearErrors()
        {
            Errors.Clear();
        }
    }
    public class CommandResponse<T> : CommandResponse
    {
        public T Data { get; set; }

        public static CommandResponse<T> FailedResponse(string message = "Request Failed!", int statusCode = (int) HttpStatusCode.BadRequest)
        {
            return new CommandResponse<T>()
            {
                Message = message,
                Success = false,
                Code = statusCode
            };
        }

        public static CommandResponse<T> SuccessResponse(string message, T data = default, int statusCode = 200)
        {
            return new CommandResponse<T>()
            {
                Message = message,
                Success = true,
                Data = data,
                Code = statusCode
            };
        }

        public static CommandResponse<T> ExceptionThrown(Exception err, int statusCode = (int) HttpStatusCode.InternalServerError,
            string environment = "Development")
        {
            return new CommandResponse<T>
            {
                Success = false,
                Message = "An Error Occurred!",
                Code = statusCode
            };
        }
        public static CommandResponse<T> ExceptionThrown(string errMessage, int statusCode = (int) HttpStatusCode.InternalServerError)
        {
            return new CommandResponse<T>
            {
                Success = false,
                Message = errMessage ?? "An Error Occurred!",
                Code = statusCode
            };
        }

        public CommandResponse<T1> CloneWith<T1>(T1 value = default)
        {
            return new CommandResponse<T1>()
            {
                Data = value,
                Message = Message,
                Success = Success,
                Code = Code,
                Errors = Errors
            };
        }
    }

    public class ErrorInfo
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public string Expectation { get; set; }
    }
}
