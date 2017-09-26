namespace System.Waf.Applications.Services
{
    /// <summary>
    /// This service shows messages to the user. It returns the answer when the message was a question.
    /// </summary>
    /// <remarks>
    /// This interface is designed for simplicity. If you have to accomplish more advanced
    /// scenarios then we recommend implementing your own specific message service.
    /// </remarks>
    public interface IMessageService
    {
        /// <summary>
        /// Shows the message.
        /// </summary>
        /// <param name="message">The message.</param>
        void ShowMessage(string message);

        /// <summary>
        /// Shows the message as warning.
        /// </summary>
        /// <param name="message">The message.</param>
        void ShowWarning(string message);

        /// <summary>
        /// Shows the message as error.
        /// </summary>
        /// <param name="message">The message.</param>
        void ShowError(string message);

        /// <summary>
        /// Shows the specified question.
        /// </summary>
        /// <param name="message">The question.</param>
        /// <returns><c>true</c> for yes, <c>false</c> for no and <c>null</c> for cancel.</returns>
        bool? ShowQuestion(string message);

        /// <summary>
        /// Shows the specified yes/no question.
        /// </summary>
        /// <param name="message">The question.</param>
        /// <returns><c>true</c> for yes and <c>false</c> for no.</returns>
        bool ShowYesNoQuestion(string message);
    }
}
