using AssignmentMain;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Text.Json;
using System.Threading;
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
        string report1;
        string report2;
        string report3;
        string report4;
        List<Dictionary<string, dynamic>> PU;
        public PersonalUsagePage()
        {
            InitializeComponent();
            report1 = "DATE TAKEN";
            report2 = "ID";
            report3 = "NAME";
            report4 = "QUANTITY REMOVED";
        }

        private void update(MainWindow mw)
        {
            Trace.WriteLine("THREADED");
            while (mw._returnData == null && mw.ActivePage == 7)
            {
                report1 = "DATE TAKEN";
                report2 = "ID";
                report3 = "NAME";
                report4 = "QUANTITY REMOVED";
                Thread.Sleep(1000);
                Trace.WriteLine("test");
            }
            dynamic data = mw._returnData;
            if (data != null)
            {
                Trace.WriteLine(data);
                PU = JsonSerializer.Deserialize<List<Dictionary<string, dynamic>>>(data);
                foreach (Dictionary<string, dynamic> entry in PU)
                {
                        report1 += "\n" + entry["date"];
                        report2 += "\n" + entry["iid"];
                        report3 += "\n" + entry["name"];
                        report4 += "\n" + entry["quantity"];
                }
                ReportBlock1.Text = report1;
                ReportBlock2.Text = report2;
                ReportBlock3.Text = report3;
                ReportBlock4.Text = report4;
            }

        }
        private void EmployeeChanged(object sender, SelectionChangedEventArgs e)
        {
            report1 = "DATE TAKEN";
            report2 = "ID";
            report3 = "NAME";
            report4 = "QUANTITY REMOVED";

            ComboBoxItem typeItem = (ComboBoxItem)Employee.SelectedItem;
            string employeeName = typeItem.Content.ToString();
            //PU = facade.GetTransactions();

            Dictionary<string, dynamic> data = new Dictionary<string, dynamic> { { "employeeID", Int32.Parse(employeeName) } };
            ((MainWindow)Application.Current.MainWindow).bData = data;
            ((MainWindow)Application.Current.MainWindow).GetDataToBroadcast();
            ((MainWindow)Application.Current.MainWindow).addCommand('7');


           /* foreach (TransactionLogEntry entry in PU)
            {
                if (entry.TypeOfTransaction.Equals("Quantity Removed") && entry.EmployeeID == Int32.Parse(employeeName))
                {
                    report1 += "\n" + entry.DateAdded;
                    report2 += "\n" + entry.ItemID;
                    report3 += "\n" + entry.ItemName;
                    report4 += "\n" + entry.Quantity;
                }
            }*/

            ReportBlock1.Text = report1;
            ReportBlock2.Text = report2;
            ReportBlock3.Text = report3;
            ReportBlock4.Text = report4;
        }
    }
}
