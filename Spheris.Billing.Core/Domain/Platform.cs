using System;


namespace Spheris.Billing.Core.Domain
{

    /// <summary>
    /// PLATFORM
    /// KEY - PLATFORM
    /// </summary>
    public class Platform : ObjectBase<Platform>
    {
        /// <summary>
        /// PLATFORM.AllowDBNull = false;
        /// PLATFORM.Unique = true;
        /// PLATFORM.MaxLength = 5;
        /// </summary>
        private string _PLATFORM;
        public string PLATFORM 
        { 
            get
            {   
                return _PLATFORM;
            }
            set
            {
                _PLATFORM = value;
                OnPropertyChanged("PLATFORM");
            }
        }


        /// <summary>
        /// DESCR.AllowDBNull = false;
        /// DESCR.MaxLength = 15;
        /// </summary>
        private string _descr { get; set; }
        public string DESCR
        {
            get
            {
                return _descr;
            }
            set
            {
                _descr = value;
                this.OnPropertyChanged("DESCR");
            }
        }

        /// <summary>
        /// GL_DISTR_NBR_DOM_TR.AllowDBNull = false;
        /// GL_DISTR_NBR_DOM_TR.MaxLength = 15;
        /// </summary>
        private string _GL_DISTR_NBR_DOM_TR;
        public string GL_DISTR_NBR_DOM_TR 
        { 
            get
            {   
                return _GL_DISTR_NBR_DOM_TR;
            }
            set
            {
                _GL_DISTR_NBR_DOM_TR = value;
                OnPropertyChanged("GL_DISTR_NBR_DOM_TR");
            }
        }


        /// <summary>
        /// GL_DISTR_NBR_GLOBAL_TR.AllowDBNull = false;
        /// GL_DISTR_NBR_GLOBAL_TR.MaxLength = 15;
        /// </summary>
        private string _GL_DISTR_NBR_GLOBAL_TR;
        public string GL_DISTR_NBR_GLOBAL_TR 
        { 
            get
            {   
                return _GL_DISTR_NBR_GLOBAL_TR;
            }
            set
            {
                _GL_DISTR_NBR_GLOBAL_TR = value;
                OnPropertyChanged("GL_DISTR_NBR_GLOBAL_TR");
            }
        }


        /// <summary>
        /// GL_DISTR_NBR_DOM_SR.AllowDBNull = false;
        /// GL_DISTR_NBR_DOM_SR.MaxLength = 15;
        /// </summary>
        private string _GL_DISTR_NBR_DOM_SR;
        public string GL_DISTR_NBR_DOM_SR 
        { 
            get
            {   
                return _GL_DISTR_NBR_DOM_SR;
            }
            set
            {
                _GL_DISTR_NBR_DOM_SR = value;
                OnPropertyChanged("GL_DISTR_NBR_DOM_SR");
            }
        }


        /// <summary>
        /// GL_DISTR_NBR_GLOBAL_SR.AllowDBNull = false;
        /// GL_DISTR_NBR_GLOBAL_SR.MaxLength = 15;
        /// </summary>
        private string _GL_DISTR_NBR_GLOBAL_SR;
        public string GL_DISTR_NBR_GLOBAL_SR 
        { 
            get
            {   
                return _GL_DISTR_NBR_GLOBAL_SR;
            }
            set
            {
                _GL_DISTR_NBR_GLOBAL_SR = value;
                OnPropertyChanged("GL_DISTR_NBR_GLOBAL_SR");
            }
        }


        /// <summary>
        /// GL_DISTR_NBR_INHOUSE.AllowDBNull = false;
        /// GL_DISTR_NBR_INHOUSE.MaxLength = 15;
        /// </summary>
        private string _GL_DISTR_NBR_INHOUSE;
        public string GL_DISTR_NBR_INHOUSE 
        { 
            get
            {   
                return _GL_DISTR_NBR_INHOUSE;
            }
            set
            {
                _GL_DISTR_NBR_INHOUSE = value;
                OnPropertyChanged("GL_DISTR_NBR_INHOUSE");
            }
        }


        public override string GetValidationError(string propertyName)
        {
            return null;

        }

        #region Equals
        public override int GetHashCode()
        {
            return 0.CombineHashCode(PLATFORM);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            var t = obj as Platform;
            if (t == null)
                return false;
            if (PLATFORM == t.PLATFORM )
                return true;
            return false;
        }
        #endregion    
    }




}
