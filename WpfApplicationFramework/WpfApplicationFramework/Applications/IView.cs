namespace System.Waf.Applications
{
    /// <summary>
    /// Represents a view
    /// </summary>
    public interface IView
    {
        /// <summary>
        /// Gives the VM a "Look" at what its view's name is
        /// </summary>
        string MyTitle { get; }
        /// <summary>
        /// Gets or sets the data context of the view.
        /// </summary>
        object DataContext { get; set; }

        /// <summary>
        /// Show A message to the user.
        /// </summary>
        /// <param name="errMsg"></param>
        void ShowMsg(string errMsg);

        /// <summary>
        /// Determine if the window is in view
        /// </summary>
        bool IsInView { get; }
    }
}
