using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MedQuist.BillingAdmin.Presentation.Helpers
{
    /// <summary>
    /// Accessor for DependencyProperty on a DataGrid
    /// </summary>
    public class DataGridHelper : DataGrid// UserControl
    {
        #region SelectedItems

        public DataGridHelper()
        {
        }

        /// <summary>
        /// SelectedItems Attached Dependency Property
        /// </summary>
        public static readonly DependencyProperty SelectedItemsProperty =
            DependencyProperty.RegisterAttached("SelectedItems", typeof(IList), typeof(DataGridHelper),
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
            var DataGrid = (DataGrid)d;
            ReSetSelectedItems(DataGrid);
            DataGrid.SelectionChanged += delegate
            {
                ReSetSelectedItems(DataGrid);
            };
        }

        private static void ReSetSelectedItems(DataGrid DataGrid)
        {
            IList selectedItems = GetSelectedItems(DataGrid);
            selectedItems.Clear();
            if (DataGrid.SelectedItems != null)
            {
                foreach (var item in DataGrid.SelectedItems)
                    selectedItems.Add(item);
            }
        }
       #endregion
    }
}
