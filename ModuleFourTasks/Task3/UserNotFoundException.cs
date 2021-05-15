using System;

namespace Task3
{
    /// <summary>
    /// Exception raising when user wasn't found by some reason.
    /// </summary>
    public class UserNotFoundException : Exception
    {
        /// <summary>
        /// Without message.
        /// </summary>
        public UserNotFoundException()
        {
        }

        /// <summary>
        /// With message.
        /// </summary>
        /// <param name="message"></param>
        public UserNotFoundException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// With message and inner exception.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="inner"></param>
        public UserNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
