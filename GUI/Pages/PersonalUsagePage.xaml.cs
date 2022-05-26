using AssignmentMain;
using DataGateway;
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
    /// Interaction logic for PersonalUsagePage.xaml
    /// </summary>
    public partial class PersonalUsagePage : Page
    {
        private readonly GatewayFacade facade;
        string report1;
        string report2;
        string report3;
        string report4;
        List<TransactionLogEntry> PU; 
        public PersonalUsagePage()
        {
            InitializeComponent();
            report1 = "DATE TAKEN";
            report2 = "ID";
            report3 = "NAME";
            report4 = "QUANTITY REMOVED";
            facade = new GatewayFacade();
        }

        private void EmployeeChanged(object sender, SelectionChangedEventArgs e)
        {
            report1 = "DATE TAKEN";
            report2 = "ID";
            report3 = "NAME";
            report4 = "QUANTITY REMOVED";

            ComboBoxItem typeItem = (ComboBoxItem)Employee.SelectedItem;
            string employeeName = typeItem.Content.ToString();
            PU = facade.GetTransactions();

            foreach (TransactionLogEntry entry in PU)
            {
                if (entry.TypeOfTransaction.Equals("Quantity Removed") && entry.EmployeeID == Int32.Parse(employeeName))
                {
                    report1 += "\n" + entry.DateAdded;
                    report2 += "\n" + entry.ItemID;
                    report3 += "\n" + entry.ItemName;
                    report4 += "\n" + entry.Quantity;
                }
            }

            ReportBlock1.Text = report1;
            ReportBlock2.Text = report2;
            ReportBlock3.Text = report3;
            ReportBlock4.Text = report4;
        }
    }
}
