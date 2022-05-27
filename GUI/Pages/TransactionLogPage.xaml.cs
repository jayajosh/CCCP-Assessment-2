using AssignmentMain;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace Assignment_Gui.Pages
{
    /// <summary>
    /// Interaction logic for TransactionLogPage.xaml
    /// </summary>
    public partial class TransactionLogPage : Page
    {
        string report1;
        string report2;
        string report3;
        string report4;
        string report5;
        string report6;
        string report7;
        List<TransactionLogEntry> TL;
        public TransactionLogPage()
        {
            InitializeComponent();
            report1 = "DATE";
            report2 = "TYPE";
            report3 = "ID";
            report4 = "NAME";
            report5 = "QUANTITY";
            report6 = "EMPLOYEE";
            report7 = "PRICE";

            

            /*foreach (TransactionLogEntry entry in TL)
            {
                report1 += "\n" + entry.DateAdded.ToString("dd/MM/yyyy");
                report2 += "\n" + entry.TypeOfTransaction;
                report3 += "\n" + entry.ItemID;
                report4 += "\n" + entry.ItemName;
                report5 += "\n" + entry.Quantity;
                report6 += "\n" + facade.FindEmployee(entry.EmployeeID).EmpName;
                report7 += "\n" + (entry.TypeOfTransaction.Equals("Quantity Removed") ? "" : "" + string.Format("{0:C}", entry.ItemPrice));
            }

            ReportBlock1.Text = report1;
            ReportBlock2.Text = report2;
            ReportBlock3.Text = report3;
            ReportBlock4.Text = report4;
            ReportBlock5.Text = report5;
            ReportBlock6.Text = report6;
            ReportBlock7.Text = report7;*/
        }

        public void fetchData()
        {
            ((MainWindow)Application.Current.MainWindow).addCommand('6');
        }
    }
}
