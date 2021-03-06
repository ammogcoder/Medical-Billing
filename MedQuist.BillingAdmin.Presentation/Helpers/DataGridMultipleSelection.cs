﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace MedQuist.BillingAdmin.Presentation.Helpers
{
    public static class DataGridMultipleSelection
    {
        /// <summary>
        /// Used in the selectedItemsSources private registry to hold state
        /// </summary>
        private class DataGridsAndInitiatedSelectionChange
        {
            public readonly List<WeakReference> BoundDataGridReferences = new List<WeakReference>();
            public bool InitiatedSelectionChange { get; set; }
        }

        // Use each source list's hashcode as the key so that we don't hold on
        // to any references in case the DataGrid gets disposed without telling
        // to remove the source list from our registry.
        private static Dictionary<int, DataGridsAndInitiatedSelectionChange> selectedItemsSources = new Dictionary<int, DataGridsAndInitiatedSelectionChange>();

        #region InitiatedSelectionChangeProperty
        /// <summary>
        /// For use only by this implementation to track if a DataGrid is the one changing the selection
        /// </summary>
        private static readonly DependencyProperty InitiatedSelectionChangeProperty = DependencyProperty.RegisterAttached("InitiatedSelectionChange", typeof(bool), typeof(DataGridMultipleSelection), new PropertyMetadata(false));

        /// <summary>
        /// Accessor to get the InitiatedSelectionChange DependencyProperty
        /// </summary>
        private static bool GetInitiatedSelectionChange(DataGrid element)
        {
            return (bool)element.GetValue(DataGridMultipleSelection.InitiatedSelectionChangeProperty);
        }

        /// <summary>
        /// Accessor to set the InitiatedSelectionChange DependencyProperty 
        /// </summary>
        private static void SetInitiatedSelectionChange(DataGrid element, bool value)
        {
            element.SetValue(DataGridMultipleSelection.InitiatedSelectionChangeProperty, value);
        }
        #endregion

        #region SelectedItemsSourceProperty

        /// <summary>
        /// Holds an IList implementing INotifyCollectionChanged to use as an items source for DataGrid.SelectedItems
        /// </summary>
        public static readonly DependencyProperty SelectedItemsSourceProperty = DependencyProperty.RegisterAttached("SelectedItemsSource", typeof(INotifyCollectionChanged), typeof(DataGridMultipleSelection), new PropertyMetadata(null, SelectedItemsSourceChanged));

        /// <summary>
        /// Accessor to get the SelectedItemsSource DependencyProperty
        /// </summary>
        public static INotifyCollectionChanged GetSelectedItemsSource(DataGrid element)
        {
            return element.GetValue(DataGridMultipleSelection.SelectedItemsSourceProperty) as INotifyCollectionChanged;
        }

        /// <summary>
        /// Accessor to set the SelectedItemsSource DependencyProperty
        /// </summary>
        public static void SetSelectedItemsSource(DataGrid element, INotifyCollectionChanged value)
        {
            element.SetValue(DataGridMultipleSelection.SelectedItemsSourceProperty, value);
        }

        /// <summary>
        /// Updates the items source registry when the SelectedItemsSource for a DataGrid changes
        /// </summary>
        private static void SelectedItemsSourceChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            DataGrid dataGrid = sender as DataGrid;
            IList selectedItemsSource = null;

            // Check if the app is setting the source to a new or different list, or if it is removing the binding
            if (args.NewValue != null)
            {
                selectedItemsSource = args.NewValue as IList;
                if (selectedItemsSource == null)
                {
                    throw new ArgumentException("The value for SelectedItemsSource must implement IList.");
                }

                INotifyCollectionChanged collection = args.NewValue as INotifyCollectionChanged;
                if (collection == null)
                {
                    throw new ArgumentException("The value for SelectedItemsSource must implement INotifyCollectionChanged.");
                }

                // Don't add the event handler if the DataGrid is not setting its SelectedItemsSource for the first time
                if (args.OldValue == null)
                {
                    // Sign up for changes to the DataGrid's selected items to enable a two-way binding effect
                    dataGrid.SelectionChanged += UpdateSourceListOnDataGridSelectionChanged;
                }

                // Track this DataGrid instance for the specified source list
                DataGridsAndInitiatedSelectionChange sourceListInfo = null;
                if (DataGridMultipleSelection.selectedItemsSources.TryGetValue(selectedItemsSource.GetHashCode(), out sourceListInfo))
                {
                    sourceListInfo.BoundDataGridReferences.Add(new WeakReference(dataGrid));
                }
                else
                {
                    // This is a new source collection
                    sourceListInfo = new DataGridsAndInitiatedSelectionChange() { InitiatedSelectionChange = false };
                    sourceListInfo.BoundDataGridReferences.Add(new WeakReference(dataGrid));
                    DataGridMultipleSelection.selectedItemsSources.Add(selectedItemsSource.GetHashCode(), sourceListInfo);

                    // Sign up for changes to the source only on the first time the source is added
                    collection.CollectionChanged += UpdateDataGridsOnSourceCollectionChanged;
                }

                // Now force the DataGrid to update its SelectedItems to match the current
                // contents of the source list
                sourceListInfo.InitiatedSelectionChange = true;
                UpdateDataGrid(dataGrid, selectedItemsSource, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
                sourceListInfo.InitiatedSelectionChange = false;
            }
            else
            {
                // This DataGrid is removing its SelectedItems binding to any list
                dataGrid.SelectionChanged -= UpdateSourceListOnDataGridSelectionChanged;
                dataGrid.SelectedItems.Clear();
            }

            if (args.OldValue != null)
            {
                // Clean up the items source that was the old value

                // Remove the DataGrid from the source list's registry and remove the source list
                // if there are no more DataGrids bound to it.
                DataGridsAndInitiatedSelectionChange sourceListInfo = DataGridMultipleSelection.selectedItemsSources[args.OldValue.GetHashCode()];
                WeakReference dataGridReferenceNeedingRemoval = null;
                foreach (WeakReference dataGridReference in sourceListInfo.BoundDataGridReferences)
                {
                    if (dataGridReference.IsAlive && (dataGridReference.Target == dataGrid))
                    {
                        dataGridReferenceNeedingRemoval = dataGridReference;
                        break;
                    }
                }
                sourceListInfo.BoundDataGridReferences.Remove(dataGridReferenceNeedingRemoval);
                if (sourceListInfo.BoundDataGridReferences.Count == 0)
                {
                    DataGridMultipleSelection.selectedItemsSources.Remove(args.OldValue.GetHashCode());

                    // Detach the event handlers and clear DataGrid.SelectedItems since the source is now null
                    INotifyCollectionChanged collection = args.OldValue as INotifyCollectionChanged;
                    if (collection != null)
                    {
                        collection.CollectionChanged -= UpdateDataGridsOnSourceCollectionChanged;
                    }
                }
            }
        }

        /// <summary>
        /// INotifyCollectionChanged.CollectionChanged handler for updating DataGrid.SelectedItems when the source list changes
        /// </summary>
        private static void UpdateDataGridsOnSourceCollectionChanged(object source, NotifyCollectionChangedEventArgs collectionChangedArgs)
        {
            DataGridsAndInitiatedSelectionChange sourceListInfo = DataGridMultipleSelection.selectedItemsSources[source.GetHashCode()];

            // For each DataGrid that is bound to this list, is alive, and did not initate selection changes, update its selection
            sourceListInfo.InitiatedSelectionChange = true;
            IList sourceList = source as IList;
            Debug.Assert(sourceList != null, "SelectedItemsSource must be of type IList");
            DataGrid dataGrid = null;
            foreach (WeakReference dataGridReference in sourceListInfo.BoundDataGridReferences)
            {
                if (dataGridReference.IsAlive && !DataGridMultipleSelection.GetInitiatedSelectionChange(dataGridReference.Target as DataGrid))
                {
                    dataGrid = dataGridReference.Target as DataGrid;
                    UpdateDataGrid(dataGrid, sourceList, collectionChangedArgs);
                }
            }
            sourceListInfo.InitiatedSelectionChange = false;
        }

        /// <summary>
        /// Helper method to update the items in DataGrid.SelectedItems based on the changes defined in the given NotifyCollectionChangedEventArgs
        /// </summary>
        /// <param name="dataGrid">DataGrid which owns the SelectedItems collection to update</param>
        /// <param name="sourceList">IList which is the SelectedItemsSource</param>
        /// <param name="collectionChangedArgs">The NotifyCollectionChangedEventArgs that was passed into the CollectionChanged event handler</param>
        private static void UpdateDataGrid(DataGrid dataGrid, IList sourceList, NotifyCollectionChangedEventArgs collectionChangedArgs)
        {
            switch (collectionChangedArgs.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (object newItem in collectionChangedArgs.NewItems)
                    {
                        dataGrid.SelectedItems.Add(newItem);
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    foreach (object oldItem in collectionChangedArgs.OldItems)
                    {
                        dataGrid.SelectedItems.Remove(oldItem);
                    }
                    break;
                case NotifyCollectionChangedAction.Reset:
                    // Unfortunately can not do the following two steps as an atomic change
                    // so the target list could raise multiple notifications as it gets updated
                    dataGrid.SelectedItems.Clear();
                    foreach (object item in sourceList)
                    {
                        dataGrid.SelectedItems.Add(item);
                    }
                    break;
                default:
                    throw new NotImplementedException("Only Add, Remove, and Reset actions are implemented.");
            }
        }

        /// <summary>
        /// DataGrid.SelectionChanged handler to update the source list given the SelectionChangedEventArgs
        /// </summary>
        private static void UpdateSourceListOnDataGridSelectionChanged(object sender, SelectionChangedEventArgs selectionChangedArgs)
        {
            DataGrid dataGrid = sender as DataGrid;
            IList selectedItemsSource = DataGridMultipleSelection.GetSelectedItemsSource(dataGrid) as IList;
            Debug.Assert(selectedItemsSource != null, "SelectedItemsSource must be of type IList");
            // If the source list initiated the changes then don't pass the DataGrid's changes back down to the source list
            if (!DataGridMultipleSelection.selectedItemsSources[selectedItemsSource.GetHashCode()].InitiatedSelectionChange)
            {
                DataGridMultipleSelection.SetInitiatedSelectionChange(dataGrid, true);
                foreach (object removedItem in selectionChangedArgs.RemovedItems)
                {
                    selectedItemsSource.Remove(removedItem);
                }

                foreach (object addedItem in selectionChangedArgs.AddedItems)
                {
                    selectedItemsSource.Add(addedItem);
                }
                DataGridMultipleSelection.SetInitiatedSelectionChange(dataGrid, false);
            }
        }
        #endregion
    }
}
