// <copyright file="EntityExistsException.cs" company="Downing Interview Project">
// (c) Downing 2023
// </copyright>

namespace DowningInterviewProject.Api.Exceptions
{
    /// <summary>
    /// The exception thrown if an entity already exists.
    /// </summary>
    public class EntityExistsException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityExistsException"/> class.
        /// </summary>
        /// <param name="message">The message to be sent</param>
        public EntityExistsException(string message)
            : base(message: message)
        {
        }
    }
}
