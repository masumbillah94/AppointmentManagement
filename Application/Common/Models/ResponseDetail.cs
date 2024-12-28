using Application.Common.Models.Enum;
using Domain.Dto.Common;
using System.Collections;

namespace Application.Common.Models
{
    public abstract class ResponseDetail
    {
        #region Protected Constructors

        protected ResponseDetail()
        {
            MessageType = MessageType.None;
            DateTime = DateTime.Now;
            Success = false;
        }

        #endregion Protected Constructors

        #region Public Properties

        public bool Success { get; set; }

        public Exception Exception { get; set; } = null!;

        public MessageType MessageType { get; set; }


        public int Count { get; set; }


        public string Message { get; set; } = string.Empty;


        public DateTime DateTime { get; set; }

        #endregion Public Properties
    }

    public class ResponseDetail<T> : ResponseDetail
    {
        #region Public Constructors


        public ResponseDetail()
        {
            MessageType = MessageType.None;
            DateTime = DateTime.Now;
            Success = false;
            Data = default!;
        }


        public ResponseDetail(ResponseDetail r)
        {
            Count = r.Count;
            Exception = r.Exception;
            Success = r.Success;
            MessageType = r.MessageType;
            Data = default!;
        }

        public ResponseDetail(ResponseDetail r, T data) : this(r)
        {
            Data = data;
        }

        #endregion Public Constructors

        #region Public Properties

        public T Data { get; set; }

        #endregion Public Properties

        #region Private Methods


        private static int GetCount(T data)
        {
            if (data == null) return 0;

            if (data is IList list) return list.Count;

            return 1;
        }

        #endregion Private Methods

        #region Public Methods

        public ResponseDetail<TU> To<TU>()
        {
            var result = new ResponseDetail<TU>
            {
                Data = (TU)Convert.ChangeType(Data, typeof(TU))!
            };

            return result;
        }


        public ResponseDetail<T> InvalidResponse(string message)
        {
            Count = 0;
            Message = message;
            Exception = null!;
            Success = false;
            MessageType = MessageType.Invalid;
            return this;
        }


        public ResponseDetail<T> InvalidResponse(Exception exception)
        {
            Count = 0;
            Message = exception.InnerException == null ? exception.Message : exception.InnerException.Message;
            Exception = exception.InnerException ?? exception;
            Success = false;
            MessageType = MessageType.Invalid;
            return this;
        }

        public ResponseDetail<T> InvalidResponse(string message, Exception exception)
        {
            Count = 0;
            Message = message + " -- " + (exception.InnerException == null ? exception.Message : exception.InnerException.Message);
            Exception = exception.InnerException ?? exception;
            Success = false;
            MessageType = MessageType.Invalid;
            return this;
        }

        public ResponseDetail<T> ErrorResponse(string message)
        {
            Count = 0;
            Message = message;
            Exception = null!;
            Success = false;
            MessageType = MessageType.Error;
            return this;
        }

        public ResponseDetail<T> ErrorResponse(Exception exception)
        {
            Count = 0;
            Message = exception.InnerException == null ? exception.Message : exception.InnerException.Message;
            Exception = exception;
            Success = false;
            MessageType = MessageType.Error;
            return this;
        }

        public ResponseDetail<T> ErrorResponse(string message, Exception exception)
        {
            Count = 0;
            Message = message + " -- " + (exception.InnerException == null ? exception.Message : exception.InnerException.Message);
            Exception = exception.InnerException ?? exception;
            Success = false;
            MessageType = MessageType.Error;
            return this;
        }

        public ResponseDetail<T> SuccessResponse(T data)
        {
            Data = data;
            Count = ResponseDetail<T>.GetCount(data);
            Message = "Executed Successfully!";
            Exception = null!;
            Success = true;
            MessageType = MessageType.Success;
            return this;
        }



        public ResponseDetail<T> SuccessResponse(T data, string message)
        {
            Data = data;
            Count = ResponseDetail<T>.GetCount(data);
            Message = message;
            Exception = null!;
            Success = true;
            MessageType = MessageType.Success;
            return this;
        }


        public ResponseDetail<T> SuccessResponse(T data, int count)
        {
            Data = data;
            Count = count;
            Message = "Data Found Successfully!";
            Exception = null!;
            Success = true;
            MessageType = MessageType.Success;
            return this;
        }


        public ResponseDetail<T> SuccessResponse(T data, int count, string message)
        {
            Data = data;
            Count = count;
            Message = message;
            Exception = null!;
            Success = true;
            MessageType = MessageType.Success;
            return this;
        }

  
        public ResponseDetail<T> InfoResponse(T data, int count, string message, bool success)
        {
            Data = data;
            Count = count;
            Message = message;
            Exception = null!;
            Success = success;
            MessageType = MessageType.Info;
            return this;
        }

        public ResponseDetail<T> InfoResponse(string message)
        {
            Message = message;
            Exception = null!;
            Success = false;
            MessageType = MessageType.Info;
            return this;
        }

        public ResponseDetail<T> InfoResponse(string message, bool success)
        {
            Message = message;
            Exception = null!;
            Success = success;
            MessageType = MessageType.Info;
            return this;
        }

        #endregion Public Methods
    }
}
