using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace System.Waf.Applications
{
    /// <summary>
    /// SimpleCommand
    /// </summary>
    public class SimpleCommand : ICommand
    {
        /// <summary>
        /// Gets or sets the Predicate to execute when the 
        /// CanExecute of the command gets called
        /// </summary>
        public Predicate<object> CanExecuteDelegate { get; set; }

        /// <summary>
        /// Gets or sets the action to be called when the 
        /// Execute method of the command gets called
        /// </summary>
        public Action<object> ExecuteDelegate { get; set; }

        #region ICommand Members

        /// <summary>
        /// Checks if the command Execute method can run
        /// </summary>
        /// <param name="parameter">THe command parameter to 
        /// be passed</param>
        /// <returns>Returns true if the command can execute. 
        /// By default true is returned so that if the user of 
        /// SimpleCommand does not specify a CanExecuteCommand 
        /// delegate the command still executes.</returns>
        public bool CanExecute(object parameter)
        {
            if (CanExecuteDelegate != null)
                return CanExecuteDelegate(parameter);
            return true;// if there is no can execute default to true
        }

        /// <summary>
        /// CanExecuteChanged
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        /// <summary>
        /// Executes the actual command
        /// </summary>
        /// <param name="parameter">THe command parameter 
        /// to be passed</param>
        public void Execute(object parameter)
        {
            if (ExecuteDelegate != null)
                ExecuteDelegate(parameter);
        }

        #endregion
    }    /// <summary>
    /// Abstract base class for a ViewModel implementation.
    /// </summary>
    /// <typeparam name="TView">The type of the view. Do provide an interface as type and not the concrete type itself.</typeparam>
    public abstract class ViewModel<TView> : ViewModel where TView : IView
    {
        private readonly TView view;


        /// <summary>
        /// Initializes a new instance of the <see cref="ViewModel&lt;TView&gt;"/> class and
        /// attaches itself as <c>DataContext</c> to the view.
        /// </summary>
        /// <param name="view">The view.</param>
        protected ViewModel(TView view)
            : this(view, false)
        { }


        /// <summary>
        /// Initializes a new instance of the <see cref="ViewModel&lt;TView&gt;"/> class.
        /// </summary>
        /// <param name="view">The view.</param>
        /// <param name="isChild">if set to <c>true</c> then the ViewModel is a child of another ViewModel.</param>
        protected ViewModel(TView view, bool isChild)
            : base(view, isChild)
        {
            this.view = view;
        }


        /// <summary>
        /// Gets the associated view as specified view type.
        /// </summary>
        /// <remarks>
        /// Use this property in a ViewModel subclass to avoid casting.
        /// </remarks>
        protected TView ViewCore { get { return view; } }


    }
}
