using System;

namespace Spheris.Billing.Core.Domain
{
    /// <summary>
    /// BRAND
    /// </summary>
    public class Brand : ObjectBase<Brand>
    {
        /// <summary>
        /// BRAND.AllowDBNull = false;
        /// BRAND.Unique = true;
        /// BRAND.MaxLength = 5;
        /// KEY
        /// </summary>
        private string _BRAND;
        public string BRAND
        {
            get
            {
                return _BRAND;
            }
            set
            {
                _BRAND = value;
                OnPropertyChanged("BRAND");
            }
        }


        /// <summary>
        /// DESCR.MaxLength = 30;
        /// </summary>
        private string _DESCR;
        public string DESCR
        {
            get
            {
                return _DESCR;
            }
            set
            {
                _DESCR = value;
                OnPropertyChanged("DESCR");
            }
        }


        /// <summary>
        /// GRAPHIC_FILENAME.MaxLength = 255;
        /// </summary>
        private string _GRAPHIC_FILENAME;
        public string GRAPHIC_FILENAME
        {
            get
            {
                return _GRAPHIC_FILENAME;
            }
            set
            {
                _GRAPHIC_FILENAME = value;
                OnPropertyChanged("GRAPHIC_FILENAME");
            }
        }


        public override string GetValidationError(string propertyName)
        {
            // TODO - Is edited?
            return null;
        }


        #region Equals
        public override int GetHashCode()
        {
            return 0.CombineHashCode(BRAND);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            var t = obj as Brand;
            if (t == null)
                return false;
            if (BRAND == t.BRAND)
                return true;
            return false;
        }
        #endregion

    }
}
