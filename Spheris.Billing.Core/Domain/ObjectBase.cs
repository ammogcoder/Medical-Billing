using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Collections.Generic;

namespace Spheris.Billing.Core.Domain
{
    public abstract class ObjectPropertyEquatedBase<T> : ObjectBase<T>, IEquatable<T> where T : new()
    {

        public static object GetPropertyValue(object obj, string propName)
        {
            foreach (System.Reflection.PropertyInfo info in obj.GetType().GetProperties())
            {
                if (info.Name == propName)
                {
                    if (!info.CanRead || !info.CanWrite)
                        return null;
                    return info.GetValue(obj, null);
                }
            }
            return null;
        }
#if NEVER
        public override int GetHashCode()
        {
            int hash = 0;
            foreach (System.Reflection.PropertyInfo info in GetType().GetProperties())
            {
                if (info.CanRead && info.CanWrite)
                {
                    object obj = info.GetValue(this, null);
                    hash ^= (obj == null) ? 0 : info.GetValue(this, null).GetHashCode();
                }
            }
            return hash;
        }       
#endif
        bool  IEquatable<T>.Equals(T other)
        {
            if (other == null || other.GetType() != typeof(T))
                return false;

            foreach (System.Reflection.PropertyInfo info in GetType().GetProperties())
            {
                if (info.CanRead && info.CanWrite)
                {
                    object thisObjs = info.GetValue(this, null);
                    object otherObjs = GetPropertyValue(other, info.Name);
                    if (thisObjs == null || otherObjs == null)
                        return false;
                    if (otherObjs.Equals(thisObjs) == false)
                        return false;
                }
            }
            return true;
        }
    }

    public abstract class ObjectBase<T> : IDataErrorInfo, INotifyPropertyChanged, IGetValidationError where T : new()
    {
        #region Validation Helpers

        public abstract override bool Equals(object obj);
        public abstract override int GetHashCode();


        static public int GetStringHash(string str)
        {
            return (string.IsNullOrEmpty(str)) ? 0 : str.GetHashCode();
        }

        static public bool HasValue(object value)
        {
            if (value == null)
                return false;
            if (value is string)
            {
                if ((value as string).Trim() == String.Empty)
                    return false;
            }
            return true;
        }
        #endregion

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
            Modified = true;
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                handler(this, e);
            }
        }

        #endregion // INotifyPropertyChanged Members

        #region IDataErrorInfo Members

        string IDataErrorInfo.Error { get { return null; } }

        public string this[string propertyName]
        {
            get { return this.GetValidationError(propertyName); }
        }

        //protected virtual string GetValidationError(string propertyName)
        //{
        //    throw new NotImplementedException();
        //}

        public abstract string GetValidationError(string propertyName);

        #endregion

        public bool WasCloned
        { get; set; }

        public T BaseClone<T>() where T : class
        {
            WasCloned = true;
            return MemberwiseClone() as T;
        }

        /*
        public bool Equals(Person other)
        {
            return other != null &&
            other.GetType() == typeof(Person) &&
            Name == other.Name &&
            Shoesize == other.Shoesize &&
            Father == other.Father;
        } 
        */

        //public abstract bool Equals(T compare);


        #region GetHashCode()
        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>A 32-bit signed integer hash code.</returns>
        /// <a href="http://msdn.microsoft.com/en-us/library/system.object.gethashcode.aspx"/>
        //public abstract override int GetHashCode();
        #endregion

        private List<string> validatedProperties;
        public List<string> ValidatedProperties
        {
            get
            {
                if(validatedProperties == null)
                    FetchProperties();
                return validatedProperties;
            }
        }

        private void FetchProperties()
        {
            validatedProperties = new List<string>();
            foreach (System.Reflection.PropertyInfo info in GetType().GetProperties())
            {
                if (info.PropertyType.IsPublic)
                    validatedProperties.Add(info.Name);
            }
        }

        private bool modified;
        public bool Modified
        {
            get
            {
                return modified;
            }
            set
            {
                modified = value;
            }
        }

    }
}
