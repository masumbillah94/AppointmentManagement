using Application.Common.Models.Enum;
using Domain.Dto.Common;
using System.Collections;

namespace Application.Common.Models
{
    /// <summary>
    /// Represents an abstract base class for response details.
    /// </summary>
    /// <remarks>
    /// The <see cref="ResponseDetail"/> class serves as a base class for response details. It
    /// provides common properties and methods that can be used by derived classes to handle and
    /// represent response information. This class is meant to be inherited from and extended to
    /// create concrete response detail classes for specific scenarios.
    /// </remarks>
    public abstract class ResponseDetail
    {
        #region Protected Constructors

        /// <summary>
        /// Protected constructor of the <see cref="ResponseDetail"/> class.
        /// </summary>
        /// <remarks>
        /// This constructor initializes a new instance of the <see cref="ResponseDetail"/> class
        /// with default values. It is marked as protected to restrict direct instantiation of the
        /// base class. Use this constructor in derived classes to set initial values for the
        /// MessageType, DateTime, and Success properties.
        /// </remarks>
        protected ResponseDetail()
        {
            MessageType = MessageType.None;
            DateTime = DateTime.Now;
            Success = false;
        }

        #endregion Protected Constructors

        #region Public Properties

        /// <summary>
        /// Gets or sets a value indicating whether the response is successful.
        /// </summary>
        /// <value><c>true</c> if the response is successful; otherwise, <c>false</c>.</value>
        /// <remarks>
        /// This property represents the success status of the response. Set it to <c>true</c> if
        /// the response represents a successful operation; otherwise, set it to <c>false</c>.
        /// </remarks>
        public bool Success { get; set; }

        /// <summary>
        /// Gets or sets the exception associated with the response.
        /// </summary>
        /// <value>The exception associated with the response.</value>
        /// <remarks>
        /// This property represents the exception associated with the response, if any. Set it to
        /// the exception object if an exception occurred during the operation; otherwise, set it to <c>null</c>.
        /// </remarks>
        public Exception Exception { get; set; } = null!;

        /// <summary>
        /// Gets or sets the message type of the response.
        /// </summary>
        /// <value>The message type of the response.</value>
        /// <remarks>
        /// This property represents the message type of the response, indicating the category or
        /// nature of the response message. Set it to one of the values from the <see
        /// cref="MessageType"/> enumeration.
        /// </remarks>
        public MessageType MessageType { get; set; }

        /// <summary>
        /// Gets or sets the count of elements in the response data.
        /// </summary>
        /// <value>The count of elements in the response data.</value>
        /// <remarks>
        /// This property represents the count of elements in the response data. Set it to the
        /// number of elements present in the data.
        /// </remarks>
        public int Count { get; set; }

        /// <summary>
        /// Gets or sets the message associated with the response.
        /// </summary>
        /// <value>The message associated with the response.</value>
        /// <remarks>
        /// This property represents the message associated with the response. Set it to the desired
        /// message string that provides additional information about the response.
        /// </remarks>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the date and time of the response.
        /// </summary>
        /// <value>The date and time of the response.</value>
        /// <remarks>
        /// This property represents the date and time of the response. Set it to the appropriate
        /// date and time value when the response is generated.
        /// </remarks>
        public DateTime DateTime { get; set; }

        #endregion Public Properties
    }

    /// <summary>
    /// Represents a detailed response object with specific data of type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type of the response data.</typeparam>
    /// <remarks>
    /// The <see cref="ResponseDetail{T}"/> class extends the <see cref="ResponseDetail"/> class and
    /// provides additional functionality for handling responses with specific data of type
    /// <typeparamref name="T"/>. It contains properties and methods to store and manipulate the
    /// response data, as well as properties to represent the response status and message type. Use
    /// this class to encapsulate detailed response information and handle different types of
    /// responses in a generic way.
    /// </remarks>
    public class ResponseDetail<T> : ResponseDetail
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ResponseDetail{T}"/> class.
        /// </summary>
        /// <typeparam name="T">The type of the response data.</typeparam>
        /// <remarks>
        /// Use this constructor to create a new instance of the <see cref="ResponseDetail{T}"/>
        /// class. The MessageType property will be set to None, indicating no specific message
        /// type. The DateTime property will be set to the current date and time. The Success
        /// property will be set to false, indicating the response is not successful by default. The
        /// Data property will be set to the default value of the type <typeparamref name="T"/>.
        /// </remarks>
        public ResponseDetail()
        {
            MessageType = MessageType.None;
            DateTime = DateTime.Now;
            Success = false;
            Data = default!;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResponseDetail{T}"/> class based on an
        /// existing response.
        /// </summary>
        /// <typeparam name="T">The type of the response data.</typeparam>
        /// <param name="r">The existing response to initialize from.</param>
        /// <remarks>
        /// Use this constructor to create a new instance of the <see cref="ResponseDetail{T}"/>
        /// class based on an existing response. The properties of the existing response will be
        /// copied to the new instance, except for the Data property, which will be set to the
        /// default value of the type <typeparamref name="T"/>. This constructor allows you to
        /// create a new response with the same properties as an existing response, but with
        /// different data.
        /// </remarks>
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

        /// <summary>
        /// Gets or sets the data associated with the response.
        /// </summary>
        /// <typeparam name="T">The type of the data.</typeparam>
        /// <value>The data associated with the response.</value>
        /// <remarks>
        /// This property represents the data associated with the response. It can hold any value of
        /// the specified type <typeparamref name="T"/>. Use this property to access or modify the
        /// data of the response.
        /// </remarks>
        public T Data { get; set; }

        #endregion Public Properties

        #region Private Methods

        /// <summary>
        /// Gets the count of elements in the specified data.
        /// </summary>
        /// <typeparam name="T">The type of the data.</typeparam>
        /// <param name="data">The data to get the count from.</param>
        /// <returns>The count of elements in the data.</returns>
        /// <remarks>
        /// Use this method to get the count of elements in the specified data. If the data is null,
        /// the count will be 0. If the data is an <see cref="IList"/> or its derived types, the
        /// count will be the number of elements in the list. Otherwise, the count will be 1,
        /// indicating the presence of a single element.
        /// </remarks>
        private static int GetCount(T data)
        {
            if (data == null) return 0;

            if (data is IList list) return list.Count;

            return 1;
        }

        #endregion Private Methods

        #region Public Methods

        /// <summary>
        /// Converts the current response to a new response with a different type parameter.
        /// </summary>
        /// <typeparam name="TU">The type of the new response data.</typeparam>
        /// <returns>A new response with the converted data.</returns>
        /// <remarks>
        /// Use this method to convert the current response to a new response with a different type
        /// parameter. The data in the current response will be converted to the specified type
        /// using the <see cref="Convert.ChangeType(object, Type)"/> method. If the conversion
        /// fails, an exception will be thrown. The converted data will be assigned to the Data
        /// property of the new response.
        /// </remarks>
        public ResponseDetail<TU> To<TU>()
        {
            var result = new ResponseDetail<TU>
            {
                Data = (TU)Convert.ChangeType(Data, typeof(TU))!
            };

            return result;
        }

        /// <summary>
        /// Creates an invalid response with the provided error <see cref="string"/><paramref
        /// name="message"/> and returns error message with <see cref="MessageType.Invalid"/>.
        /// </summary>
        /// <typeparam name="T">The type of the response data.</typeparam>
        /// <param name="message">error message</param>
        /// <returns>The invalid response with <see cref="MessageType.Invalid"/>.</returns>
        /// <remarks>
        /// Use this method to create an invalid response object with the given error message. The
        /// response will have a Count of 0, indicating no data is present. The response Message
        /// will be set to the provided error message. The Exception property will be set to null,
        /// indicating no associated exception. The Success status will be set to false, indicating
        /// the response represents an invalid state. The response will have a MessageType of
        /// Invalid, indicating it is an invalid response.
        /// </remarks>
        public ResponseDetail<T> InvalidResponse(string message)
        {
            Count = 0;
            Message = message;
            Exception = null!;
            Success = false;
            MessageType = MessageType.Invalid;
            return this;
        }

        /// <summary>
        /// Creates an invalid response with the provided <see cref="Exception"/><paramref
        /// name="exception"/> and returns error message with <see cref="MessageType.Invalid"/>.
        /// </summary>
        /// <typeparam name="T">The type of the response data.</typeparam>
        /// <param name="exception">The exception associated with the error.</param>
        /// <returns>The invalid response with <see cref="MessageType.Invalid"/>.</returns>
        /// <remarks>
        /// Use this method to create an invalid response object with the given exception. The
        /// response will have a Count of 0, indicating no data is present. The response Message
        /// will be set to the innermost exception message (if available) or the exception message
        /// itself. The Exception property will be set to the innermost exception (if available) or
        /// the provided exception. The Success status will be set to false, indicating the response
        /// represents an invalid state. The response will have a MessageType of Invalid, indicating
        /// it is an invalid response.
        /// </remarks>
        public ResponseDetail<T> InvalidResponse(Exception exception)
        {
            Count = 0;
            Message = exception.InnerException == null ? exception.Message : exception.InnerException.Message;
            Exception = exception.InnerException ?? exception;
            Success = false;
            MessageType = MessageType.Invalid;
            return this;
        }

        /// <summary>
        /// Creates an invalid response with the provided error <see cref="string"/><paramref
        /// name="message"/> and <see cref="Exception"/><paramref name="exception"/> and returns
        /// error message with <see cref="MessageType.Invalid"/>.
        /// </summary>
        /// <typeparam name="T">The type of the response data.</typeparam>
        /// <param name="message">error message</param>
        /// <param name="exception">exception</param>
        /// <returns>The invalid response with <see cref="MessageType.Invalid"/>.</returns>
        /// <remarks>
        /// Use this method to create an invalid response object with the given error message and
        /// exception. The response will have a Count of 0, indicating no data is present. The
        /// response Message will be constructed by combining the error message with the innermost
        /// exception message (if available). The Exception property will be set to the innermost
        /// exception (if available) or the provided exception. The Success status will be set to
        /// false, indicating the response represents an invalid state. The response will have a
        /// MessageType of Invalid, indicating it is an invalid response.
        /// </remarks>
        public ResponseDetail<T> InvalidResponse(string message, Exception exception)
        {
            Count = 0;
            Message = message + " -- " + (exception.InnerException == null ? exception.Message : exception.InnerException.Message);
            Exception = exception.InnerException ?? exception;
            Success = false;
            MessageType = MessageType.Invalid;
            return this;
        }

        /// <summary>
        /// Creates an error response with the provided error <see cref="string"/><paramref
        /// name="message"/> and returns error message with <see cref="MessageType.Error"/>
        /// </summary>
        /// <typeparam name="T">The type of the response data.</typeparam>
        /// <param name="message">error message</param>
        /// <returns>The error response with <see cref="MessageType.Error"/></returns>
        /// <remarks>
        /// Use this method to create an error response object with the given error message. The
        /// response will have a Count of 0, indicating no data is present. The response Message
        /// will be set to the provided error message. The Exception property will be set to null,
        /// indicating no associated exception. The Success status will be set to false, indicating
        /// the response represents an error. The response will have a MessageType of Error,
        /// indicating it is an error response.
        /// </remarks>
        public ResponseDetail<T> ErrorResponse(string message)
        {
            Count = 0;
            Message = message;
            Exception = null!;
            Success = false;
            MessageType = MessageType.Error;
            return this;
        }

        /// <summary>
        /// Creates an error response with the provided <see cref="Exception"/><paramref
        /// name="exception"/> and returns error response. message with <see cref="MessageType.Error"/>
        /// </summary>
        /// <typeparam name="T">The type of the response data.</typeparam>
        /// <param name="exception">exception</param>
        /// <returns>The error response with <see cref="MessageType.Error"/></returns>
        /// <remarks>
        /// Use this method to create an error response object with the given exception. The
        /// response will have a Count of 0, indicating no data is present. The response Message
        /// will be set to the innermost exception message (if available) or the exception message
        /// itself. The Exception property will be set to the provided exception. The Success status
        /// will be set to false, indicating the response represents an error. The response will
        /// have a MessageType of Error, indicating it is an error response.
        /// </remarks>
        public ResponseDetail<T> ErrorResponse(Exception exception)
        {
            Count = 0;
            Message = exception.InnerException == null ? exception.Message : exception.InnerException.Message;
            Exception = exception;
            Success = false;
            MessageType = MessageType.Error;
            return this;
        }

        /// <summary>
        /// Creates an error response with the provided error <see cref="string"/><paramref
        /// name="message"/> and <see cref="Exception"/><paramref name="exception"/> and returns
        /// error message with <see cref="MessageType.Error"/>.
        /// </summary>
        /// <typeparam name="T">The type of the response data.</typeparam>
        /// <param name="message">The error message for the response.</param>
        /// <param name="exception">The exception associated with the error.</param>
        /// <returns>The error response with <see cref="MessageType.Error"/>.</returns>
        /// <remarks>
        /// Use this method to create an error response object with the given error message and
        /// exception. The response will have a Count of 0, indicating no data is present. The
        /// response Message will be constructed by combining the error message with the innermost
        /// exception message (if available). The Exception property will be set to the innermost
        /// exception (if available) or the provided exception. The Success status will be set to
        /// false, indicating the response represents an error. The response will have a MessageType
        /// of Error, indicating it is an error response.
        /// </remarks>
        public ResponseDetail<T> ErrorResponse(string message, Exception exception)
        {
            Count = 0;
            Message = message + " -- " + (exception.InnerException == null ? exception.Message : exception.InnerException.Message);
            Exception = exception.InnerException ?? exception;
            Success = false;
            MessageType = MessageType.Error;
            return this;
        }

        /// <summary>
        /// Creates a success response with the provided <see cref="T"/><paramref name="data"/>
        /// process with <see cref="MessageType.Success"/> code and <see cref="Data"/>
        /// </summary>
        /// <typeparam name="T">The type of the response data.</typeparam>
        /// <param name="data">Data to return</param>
        /// <returns>
        /// Instance of existing response with <see cref="MessageType.Success"/> and data <see cref="T"/>
        /// </returns>
        /// <remarks>
        /// Use this method to create a success response object with the given data, and count. The
        /// response will have a Success status of true and a MessageType of Success, indicating it
        /// is a successful response.
        /// </remarks>
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
        //public ResponseDetail<PaginatedResult<T>> SuccessResponse(PaginatedResult<T> data)
        //{
        //    return new ResponseDetail<PaginatedResult<T>>
        //    {
        //        Data = data,
        //        Message = "Data Found Successfully!",
        //        Exception = null,
        //        Success = true,
        //        MessageType = MessageType.Success
        //    };
        //}

        /// <summary>
        /// Creates a success response with the provided <see cref="T"/><paramref name="data"/>
        /// process with <see cref="MessageType.Success"/> code , <see cref="Data"/> and Provided
        /// <see cref="string"/><paramref name="data"/>
        /// </summary>
        /// <typeparam name="T">The type of the response data.</typeparam>
        /// <param name="data">Data to return</param>
        /// <param name="message">Message to return</param>
        /// <returns>
        /// Instance of existing response with <see cref="MessageType.Success"/> and data <see cref="T"/>
        /// </returns>
        /// <remarks>
        /// Use this method to create a success response object with the given data, and count. The
        /// response will have a Success status of true and a MessageType of Success, indicating it
        /// is a successful response.
        /// </remarks>
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

        /// <summary>
        /// Creates a success response with the provided <see cref="T"/><paramref name="data"/>
        /// process with <see cref="MessageType.Success"/> code , <see cref="Data"/> and Provided
        /// <see cref="int"/><paramref name="count"/>
        /// </summary>
        /// <typeparam name="T">The type of the response data.</typeparam>
        /// <param name="data">Data to return</param>
        /// <param name="count">Count to return</param>
        /// <returns>
        /// Instance of existing response with <see cref="MessageType.Success"/> and data <see cref="T"/>
        /// </returns>
        /// <remarks>
        /// Use this method to create a success response object with the given data, and count. The
        /// response will have a Success status of true and a MessageType of Success, indicating it
        /// is a successful response.
        /// </remarks>
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

        /// <summary>
        /// Creates a success response with the provided <see cref="T"/><paramref name="data"/>
        /// process with <see cref="MessageType.Success"/> code , <see cref="Data"/> , Provided <see
        /// cref="int"/><paramref name="count"/> and provided <see cref="string"/><paramref name="message"/>
        /// </summary>
        /// <typeparam name="T">The type of the response data.</typeparam>
        /// <param name="data">Data to return</param>
        /// <param name="count">Count to return</param>
        /// <param name="message">Message to return</param>
        /// <returns>
        /// Instance of existing response with <see cref="MessageType.Success"/> and data <see cref="T"/>
        /// </returns>
        /// <remarks>
        /// Use this method to create a success response object with the given data, count, and
        /// message. The response will have a Success status of true and a MessageType of Success,
        /// indicating it is a successful response.
        /// </remarks>
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

        /// <summary>
        /// Creates an information response with the provided <typeparamref name="T"/><paramref
        /// name="data"/>, <see cref="int"/><paramref name="count"/>, <see cref="string"/><paramref
        /// name="message"/>, and <see cref="bool"/><paramref name="success"/> status.
        /// </summary>
        /// <typeparam name="T">The type of the response data.</typeparam>
        /// <param name="data">The data for the response.</param>
        /// <param name="count">The count of the data.</param>
        /// <param name="message">The message for the response.</param>
        /// <param name="success">The success status of the response.</param>
        /// <returns>The information response.</returns>
        /// <remarks>
        /// Use this method to create an information response object with the given data, count,
        /// message, and success status. The response will have a MessageType of Info, indicating it
        /// is an informational response.
        /// </remarks>
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

        /// <summary>
        /// Creates an information response with the provided <see cref="string"/><paramref
        /// name="message"/>. The success status of the response is set to false.
        /// </summary>
        /// <typeparam name="T">The type of the response data.</typeparam>
        /// <param name="message">The message for the response.</param>
        /// <returns>The information response.</returns>
        /// <remarks>
        /// Use this method to create an information response object with the given message. The
        /// response will have a success status of false and a MessageType of Info, indicating it is
        /// an informational response.
        /// </remarks>
        public ResponseDetail<T> InfoResponse(string message)
        {
            Message = message;
            Exception = null!;
            Success = false;
            MessageType = MessageType.Info;
            return this;
        }

        /// <summary>
        /// Creates an information response with the provided <see cref="string"/><paramref
        /// name="message"/> and the provided <see cref="bool"/><paramref name="success"/> status.
        /// </summary>
        /// <typeparam name="T">The type of the response data.</typeparam>
        /// <param name="message">The message for the response.</param>
        /// <param name="success">The success status of the response.</param>
        /// <returns>The information response.</returns>
        /// <remarks>
        /// Use this method to create an information response object with the given message and
        /// success status. The response will have a MessageType of Info, indicating it is an
        /// informational response.
        /// </remarks>
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
