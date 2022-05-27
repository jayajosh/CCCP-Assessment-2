using AssignmentMain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;

namespace Assignment_Gui.Pages
{
    /// <summary>
    /// Interaction logic for InventoryReport.xaml
    /// </summary>
    public partial class FinancialReportPage : Page
    {
        string report1;
        string report2;
        List<TransactionLogEntry> FR;

        public FinancialReportPage()
        {
            InitializeComponent();
            report1 = "NAME";
            report2 = "PRICE";
            double total = 0;

            //Console.WriteLine("\nFinancial Report:");

           /* foreach (TransactionLogEntry entry in FR)
            {
                if (entry.TypeOfTransaction.Equals("Item Added")
                    || entry.TypeOfTransaction.Equals("Quantity Added"))
                {
                    double cost = entry.ItemPrice * entry.Quantity;
                    report1 += "\n" + entry.ItemName;
                    report2 += "\n" + cost.ToString("c2");
                    //Console.WriteLine("{0}: Total price of item: {1:C}", entry.ItemName, cost);
                    total += cost;
                }
            }*/

            //Console.WriteLine("{0}: {1:C}", "Total price of all items", total);
            ReportBlock1.Text = report1;
            ReportBlock2.Text = report2;
            TotalBlock.Text = "Total Price Of All Items: " + total.ToString("c2");
        }

        public void fetchData()
        {
            ((MainWindow)Application.Current.MainWindow).addCommand('5');
        }

    }
}