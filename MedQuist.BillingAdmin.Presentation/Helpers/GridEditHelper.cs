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
    public class GridEditHelper : DataGrid// UserControl
    {

        public GridEditHelper()
        {
                this.RowEditEnding += DataGrid_Standard_RowEditEnding;
                PreviewKeyDown += DataGrid_Standard_PreviewKeyDown;
                BeginningEdit += DataGrid_Standard_BeginningEdit;
                PreviewMouseMove += DataGrid_mouse_moved;
                PreviewLostKeyboardFocus += DataGrid_lostFocus;
          
        }

        protected override void OnSelectionChanged(SelectionChangedEventArgs e)
        {
            object a = CurrentItem;
            if (IsEditingRow)
            {
                _currentEditingRow.IsSelected = true;

                if (CurrentItem != _currentObject)
                {
                    _currentEditingRow.IsSelected = true;
                }
            }

            base.OnSelectionChanged(e);
            
        }

        protected override void OnMouseEnter(MouseEventArgs e)
        {
            base.OnMouseEnter(e);
        }

        public void DataGrid_lostFocus(object sender, KeyboardFocusChangedEventArgs e)
        { 
        }

        public static readonly DependencyProperty EditingRowProperty =
            DependencyProperty.Register(
              "IsEditingRow",
              typeof(bool),
              typeof(GridEditHelper),
              new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, null));

        public bool IsEditingRow
        {
            get
            {
                return (bool)GetValue(EditingRowProperty);
            }
            set
            {
                SetValue(EditingRowProperty, value);
            }
        }

        public static readonly DependencyProperty GridIsModifiedProperty =
            DependencyProperty.Register(
              "GridIsModified",
              typeof(bool),
              typeof(GridEditHelper),
              new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, null));

        public bool GridIsModified
        {
            get
            {
                return (bool)GetValue(GridIsModifiedProperty);
            }
            set
            {
                SetValue(GridIsModifiedProperty, value);
            }
        }


        protected override void OnLoadingRow(DataGridRowEventArgs e)
        {
            IsEditingRow = false;
            GridIsModified = false;

            base.OnLoadingRow(e);
        }

        private DataGridRow _currentEditingRow;
        private object _currentObject;

        private void DataGrid_Standard_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            if (!IsEditingRow)
            {
                // set edit mode state
                IsEditingRow = true;
                _currentEditingRow = e.Row;
                _currentObject =  CurrentItem;

                IEnumerable<DataGridRow> rows = GetDataGridRows(this);
                foreach (DataGridRow row in rows)
                    if (row != _currentEditingRow)
                    {
                        row.IsEnabled = false;
                        row.Background = SystemColors.InactiveBorderBrush;
                    }
                GridIsModified = true;

            }
            else if (e.Row != _currentEditingRow)
            {
                // cancel all new edits for different rows
                e.Cancel = true;
            }
        }


        private void DataGrid_mouse_moved(object sender, MouseEventArgs e)
        {
        }


        private void DataGrid_Standard_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            DataGridRow row = GetRow(this, Items.IndexOf(CurrentItem));
            if (row.IsEditing && e.Key == Key.Enter)
            {
                // only 'Enter' key on an editing row will allow a commit to occur
                // IsEditingRow = false;
            }
        }

        private void DataGrid_Standard_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Cancel)
            {
                // cancelling the entire row will reset the state
                IsEditingRow = false;
            }
            else if (e.EditAction == DataGridEditAction.Commit)
            {
                e.Cancel = false;

            }
        }

        public IEnumerable<DataGridRow> GetDataGridRows(DataGrid grid)
        {
            var itemsSource = grid.ItemsSource as IEnumerable;
            if (null == itemsSource) yield return null;
            foreach (var item in itemsSource)
            {
                var row = grid.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;
                if (null != row) 
                    yield return row;
            }
        }

        public static DataGridRow GetRow(DataGrid dataGrid, int index)
        {
            DataGridRow row = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromIndex(index);
            if (row == null)
            {
                // may be virtualized, bring into view and try again
                dataGrid.ScrollIntoView(dataGrid.Items[index]);
                dataGrid.UpdateLayout();

                row = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromIndex(index);
            }

            return row;
        }
    }
}
