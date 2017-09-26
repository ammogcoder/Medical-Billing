using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using System.Diagnostics;
using System.Windows;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Globalization;
using System.Collections;

namespace MedQuist.BillingAdmin.ViewModels
{

    public class ModelBase : INotifyPropertyChanged
    {
        private Dictionary<string, object> variableDic = new Dictionary<string, object>();

        protected Observable<T> getVar<T>(string name) //GetVariable
        {
            if (!variableDic.ContainsKey(name))
                variableDic.Add(name, new Observable<T>
            (new Action(() => { NotifyPropertyChanged(name); })));
            return (Observable<T>)variableDic[name];
        }

        protected void setVar<T>(string name, Observable<T> value)
        {
            if (variableDic.ContainsKey(name))
                variableDic.Remove(name);
            variableDic.Add(name, new Observable<T>(new Action(() =>
                    { NotifyPropertyChanged(name); })));
            ((Observable<T>)variableDic[name]).Value = value.Value;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
    }

 
    public class ObservableConverter : TypeConverter
    {
        public override object ConvertTo(ITypeDescriptorContext context,
            CultureInfo culture, object value, Type destinationType)
        {
            object valueValue = value.GetType().GetProperty("Value").GetValue(value, null);
            return base.ConvertTo(context, culture, valueValue, destinationType);
        }
    }

    public static class Tools
    {

        public static T Add<T>(T a, T b)
        {
            // declare the parameters
            ParameterExpression paramA =
            System.Linq.Expressions.Expression.Parameter(typeof(T), "a"),
                paramB = System.Linq.Expressions.Expression.Parameter(typeof(T), "b");
            // add the parameters together
            BinaryExpression body = System.Linq.Expressions.Expression.Add(paramA, paramB);
            // compile it
            Func<T, T, T> add = System.Linq.Expressions.Expression.Lambda<Func<T, T, T>>
                    (body, paramA, paramB).Compile();
            // call it
            return add(a, b);
        }

        public static T Subtract<T>(T a, T b)
        {
            // declare the parameters
            ParameterExpression paramA =
            System.Linq.Expressions.Expression.Parameter(typeof(T), "a"),
                paramB = System.Linq.Expressions.Expression.Parameter(typeof(T), "b");
            // add the parameters together
            BinaryExpression body = System.Linq.Expressions.Expression.Subtract
                    (paramA, paramB);
            // compile it
            Func<T, T, T> subtract = System.Linq.Expressions.Expression.Lambda
                    <Func<T, T, T>>(body, paramA, paramB).Compile();
            // call it
            return subtract(a, b);
        }
    }

    [TypeConverter(typeof(ObservableConverter))]
    public class Observable<T> : INotifyPropertyChanged
    {
        T _value;
        Action _updateAction;

        public Observable(Action updateAction)
        {
            _updateAction = updateAction;
        }

        public T Value
        {
            get { return _value; }
            set
            {
                if (value == null || !value.Equals(_value))
                {
                    _value = value;
                    NotifyPropertyChanged("Value");
                    if (_updateAction != null)
                        _updateAction.Invoke();
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }

        public static implicit operator Observable<T>(T value)
        {
            return new Observable<T>(null) { Value = value };
        }

        public static Observable<T> operator +(Observable<T> value1, Observable<T> value2)
        {
            return new Observable<T>(null) { Value = Tools.Add(value1.Value, value2.Value) };
        }

        public static Observable<T> operator -(Observable<T> value1, Observable<T> value2)
        {
            return new Observable<T>(null) { Value = Tools.Subtract(value1.Value, value2.Value) };
        }
    }

    ///// <summary>
    ///// Example
    ///// </summary>
    //public class Person : ModelBase
    //{
    //    public Observable<string> LastName { get { return getVar<string>("LastName"); } }
    //    public Observable<int> Age
    //    {
    //        get { return getVar<int>("Age"); }
    //        set { setVar<int>("Age", value); }
    //    }
    //}

    /// <summary>
    /// Base class for all ViewModel classes in the application.
    /// It provides support for property change notifications 
    /// and has a DisplayName property.  This class is abstract.
    /// </summary>
    public abstract class ViewModelBase : INotifyPropertyChanged, IDisposable, IViewModelBase
    {
        #region Constructor

        protected ViewModelBase()
        {
        }

        #endregion // Constructor

        #region DisplayName

        /// <summary>
        /// Returns the user-friendly name of this object.
        /// Child classes can set this property to a new value,
        /// or override it to determine the value on-demand.
        /// </summary>
        public virtual string DisplayName { get; protected set; }

        #endregion // DisplayName

        #region Debugging Aides

        /// <summary>
        /// Warns the developer if this object does not have
        /// a public property with the specified name. This 
        /// method does not exist in a Release build.
        /// </summary>
        [Conditional("DEBUG")]
        [DebuggerStepThrough]
        public void VerifyPropertyName(string propertyName)
        {

            // Verify that the property name matches a real,  
            // public, instance property on this object.
            if (TypeDescriptor.GetProperties(this)[propertyName] == null)
            {
                string msg = "Invalid property name: " + propertyName;

                if (this.ThrowOnInvalidPropertyName)
                    throw new Exception(msg);
                else
                    Debug.Fail(msg);
            }
        }

        /// <summary>
        /// Returns whether an exception is thrown, or if a Debug.Fail() is used
        /// when an invalid property name is passed to the VerifyPropertyName method.
        /// The default value is false, but subclasses used by unit tests might 
        /// override this property's getter to return true.
        /// </summary>
        protected virtual bool ThrowOnInvalidPropertyName { get; private set; }

        #endregion // Debugging Aides

        #region RequestClose [event]

        /// <summary>
        /// Raised when this workspace should be removed from the UI.
        /// </summary>
        public event EventHandler RequestClose;

        public void OnRequestClose()
        {
            EventHandler handler = this.RequestClose;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        #endregion

        #region INotifyPropertyChanged Members

        /// <summary>
        /// Raised when a property on this object has a new value.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises this object's PropertyChanged event.
        /// </summary>
        /// <param name="propertyName">The property that has a new value.</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            this.VerifyPropertyName(propertyName);

            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                handler(this, e);
            }
        }

        #endregion // INotifyPropertyChanged Members

        #region IDisposable Members

        /// <summary>
        /// Invoked when this object is being removed from the application
        /// and will be subject to garbage collection.
        /// </summary>
        public void Dispose()
        {
            this.OnDispose();
        }

        /// <summary>
        /// Child classes can override this method to perform 
        /// clean-up logic, such as removing event handlers.
        /// </summary>
        protected virtual void OnDispose()
        {
        }

#if DEBUG
        /// <summary>
        /// Useful for ensuring that ViewModel objects are properly garbage collected.
        /// </summary>
        ~ViewModelBase()
        {
            string msg = string.Format("{0} ({1}) ({2}) Finalized", this.GetType().Name, this.DisplayName, this.GetHashCode());
            System.Diagnostics.Debug.WriteLine(msg);
        }
#endif

        #endregion // IDisposable Members

    }

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


    }

    public class RelayCommand<T> : ICommand
    {
        #region Fields

        readonly Action<T> _execute = null;
        readonly Predicate<T> _canExecute = null;

        #endregion // Fields

        #region Constructors

        public RelayCommand(Action<T> execute)
            : this(execute, null)
        {
        }

        /// <summary>
        /// Creates a new command.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        /// <param name="canExecute">The execution status logic.</param>
        public RelayCommand(Action<T> execute, Predicate<T> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");

            _execute = execute;
            _canExecute = canExecute;
        }

        #endregion // Constructors

        #region ICommand Members

        [DebuggerStepThrough]
        public bool CanExecute(object parameter)
        {
            return _canExecute == null ? true : _canExecute((T)parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            _execute((T)parameter);
        }

        #endregion // ICommand Members
    }

}
