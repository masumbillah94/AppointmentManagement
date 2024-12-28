namespace Application.Common.Models.Enum
{
    /// <summary>
    /// Represents the message type of a response.
    /// </summary>
    /// <remarks>
    /// The <see cref="MessageType"/> enumeration defines different categories or nature of response
    /// messages. It is used to indicate the type of message associated with a response. The
    /// available message types are Success, Error, Info, Invalid, and None.
    /// </remarks>
    public enum MessageType
    {
        /// <summary>
        /// Represents a successful response.
        /// </summary>
        Success,

        /// <summary>
        /// Represents an error response.
        /// </summary>
        Error,

        /// <summary>
        /// Represents an informational response.
        /// </summary>
        Info,

        /// <summary>
        /// Represents an invalid response.
        /// </summary>
        Invalid,

        /// <summary>
        /// Represents a response with no specific message type.
        /// </summary>
        None
    }
}
