using Syncfusion.Data;
using Syncfusion.UI.Xaml.Grid;
using Syncfusion.UI.Xaml.Grid.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace SfDataGridDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            dataGrid.ItemsSourceChanged += DataGrid_ItemsSourceChanged;           
            DataContext = new MainViewModel();
            dataGrid.Loaded += DataGrid_Loaded;
        }

        private void DataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            //get the Generated items in SfDataGrid
            var  generatedRecords= dataGrid.RowGenerator.Items;

            foreach (var record in generatedRecords)
            {
                //check the record is DetailsViewDatarow
                if (record is DetailsViewDataRow)
                {
                    //get the row data value of DetailsView
                    var rowdata = (record as DetailsViewDataRow).RowData;
                    //Get the record information 
                    var data = (rowdata as RecordEntry).Data;

                    var transactionModifiers = (data as OrderTransactionList).OrderTransaction.TransactionModifiers;
                    //check the value
                    if (transactionModifiers == null)
                    {
                        //set the height to hide the TemplateViewDefinition
                        dataGrid.GetVisualContainer().RowHeights[record.RowIndex] = 0;
                        dataGrid.GetVisualContainer().InvalidateMeasure();
                    }
                }
            }
        }

        private void DataGrid_ItemsSourceChanged(object sender, GridItemsSourceChangedEventArgs e)
        {
            this.dataGrid.ExpandAllDetailsView();
        }
    }

    public class MainViewModel
    {
        public MainViewModel()
        {
            OrderTransactions = new ObservableCollection<OrderTransactionList>();
            OrderTransactions.Add(new OrderTransactionList
            {
                IsSelected = false,
                OrderTransaction = new OrderTransactionDto
                {
                    GrandPrice = 3.00,
                    Qty = 1,
                    Name = "Fresh Milk Tea",                    
                }
            });
            OrderTransactions.Add(new OrderTransactionList
            {
                IsSelected = false,
                OrderTransaction = new OrderTransactionDto
                {
                    GrandPrice = 5.00,
                    Qty = 1,
                    Name = "Special Tea",                    
                }
            });
            OrderTransactions.Add(new OrderTransactionList
            {
                IsSelected = false,
                OrderTransaction = new OrderTransactionDto
                {
                    GrandPrice = 3.00,
                    Qty = 1,
                    Name = "Coffee",
                    TransactionModifiers = new List<TransactionModifierDto>
                    {
                        new TransactionModifierDto
                        {
                            DisplayName = "Without Sugar"
                        }
                    },
                    DisplayDiscount = "Discount -3"
                }
            });
            OrderTransactions.Add(new OrderTransactionList
            {
                IsSelected = false,
                OrderTransaction = new OrderTransactionDto
                {
                    GrandPrice = 7.00,
                    Qty = 1,
                    Name = "Cappucinno",
                }
            });
            OrderTransactions.Add(new OrderTransactionList
            {
                IsSelected = false,
                OrderTransaction = new OrderTransactionDto
                {
                    GrandPrice = 7.00,
                    Qty = 1,
                    Name = "Hot Chocolate",
                    TransactionModifiers = new List<TransactionModifierDto>
                    {
                        new TransactionModifierDto
                        {
                            DisplayName = "Without Sugar"
                        }
                    },                    
                }
            });
        }
        public ObservableCollection<OrderTransactionList> OrderTransactions { get; set; }
    }

    public class OrderTransactionList
    {
        public bool IsSelected { get; set; }
        public OrderTransactionDto OrderTransaction { get; set; }
    }

    public class OrderTransactionDto
    {
        public int Qty { get; set; }        
        public double GrandPrice { get; set; }
        public string Name { get; set; }
        public ICollection<TransactionModifierDto> TransactionModifiers { get; set; }
        public string DisplayDiscount { get; set; }
    }

    public class TransactionModifierDto
    {
        public string DisplayName { get; set; }
    }

    public class StringToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var val = (string)value;
            if (val != null)
                if (!string.IsNullOrWhiteSpace(val))
                    return true;
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class IntToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var val = (int?)value;
            if (val != null)
                if (val > 0)
                    return true;
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
