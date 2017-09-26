using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows;
using System.Windows.Controls;

namespace MedQuist.BillingAdmin.Presentation.Helpers
{
    /// <summary>
    /// Accessor for DependencyProperty on a ComboBox
    /// </summary>
    public class ComboBoxHelper
    {
        #region SelectedItems

        /// <summary>
        /// SelectedItems Attached Dependency Property
        /// </summary>
        public static readonly DependencyProperty SelectedItemsProperty =
            DependencyProperty.RegisterAttached("SelectedItems", typeof(IList), typeof(ComboBoxHelper),
                new FrameworkPropertyMetadata((IList)null,
                    new PropertyChangedCallback(OnSelectedItemsChanged)));

        /// <summary>
        /// Gets the SelectedItems property.  This dependency property 
        /// indicates ....
        /// </summary>
        public static IList GetSelectedItems(DependencyObject d)
        {
            return (IList)d.GetValue(SelectedItemsProperty);
        }

        /// <summary>
        /// Sets the SelectedItems property.  This dependency property 
        /// indicates ....
        /// </summary>
        public static void SetSelectedItems(DependencyObject d, IList value)
        {
            d.SetValue(SelectedItemsProperty, value);
        }

        /// <summary>
        /// Handles changes to the SelectedItems property.
        /// </summary>
        private static void OnSelectedItemsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var comboBox = (ComboBox)d;
            ReSetSelectedItems(comboBox);
            comboBox.SelectionChanged += delegate
            {
                ReSetSelectedItems(comboBox);
            };
        }

        private static void ReSetSelectedItems(ComboBox comboBox)
        {
            IList selectedItems = GetSelectedItems(comboBox);
            selectedItems.Clear();

        }
        #endregion

    }
}
