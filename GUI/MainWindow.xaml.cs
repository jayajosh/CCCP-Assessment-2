using AssignmentMain;
using Assignment_Gui.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPF_Client;
using ui_command;
using System.Diagnostics;
using System.Net;
using System.Threading;
using AssignmentMain.Objects;
using System.Text.Json;

namespace Assignment_Gui
{
    delegate void BroadcastMessage(List<string> msg);
    delegate void ShowMessage(string msg);
    delegate Dictionary<string, dynamic> GetDataToBroadcast();
    delegate void BroadcastData(List<string> data);
    delegate void ReturnData(Char code, List<string> rData);
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Thread thread;
        private WPFClient client;
        private Task clientTask;
        public Dictionary<string, dynamic> bData;
        public List<string> _returnData;

        public int ActivePage;

        AddItemPage _AddItemPage;
        AddQuantityPage _AddQuantityPage;
        TakeQuantityPage _TakeQuantityPage;
        InventoryReportPage _InventoryReportPage;
        FinancialReportPage _FinancialReportPage;
        TransactionLogPage _TransactionLogPage;
        PersonalUsagePage _PersonalUsagePage;

        public MainWindow()
        {
            InitializeComponent();
            client =  new WPFClient(BroadcastMessage, GetDataToBroadcast, ReturnData);
            clientTask = Task.Run(client.Run);
            ActivePage = 0;
            _AddItemPage = new AddItemPage();
            _AddQuantityPage = new AddQuantityPage();
            _TakeQuantityPage = new TakeQuantityPage();
            _InventoryReportPage = new InventoryReportPage();
            _FinancialReportPage = new FinancialReportPage();
            _TransactionLogPage = new TransactionLogPage();
            _PersonalUsagePage = new PersonalUsagePage();
        }

        private void AddItemClick(object sender, RoutedEventArgs e)
        {
            ActivePage = 1;
            MainFrame.NavigationService.Navigate(_AddItemPage);
        }

        private void AddQuantityClick(object sender, RoutedEventArgs e)
        {
            ActivePage = 2;
            MainFrame.NavigationService.Navigate(_AddQuantityPage);
        }

        private void TakeQuantityClick(object sender, RoutedEventArgs e)
        {
            ActivePage = 3;
            MainFrame.NavigationService.Navigate(_TakeQuantityPage);
        }

        private void InventoryReportClick(object sender, RoutedEventArgs e)
        {
            ActivePage = 4;
            MainFrame.NavigationService.Navigate(_InventoryReportPage);
            _InventoryReportPage.fetchData();
        }

        private void FinancialReportClick(object sender, RoutedEventArgs e)
        {
            ActivePage = 5;
            MainFrame.NavigationService.Navigate(_FinancialReportPage);
            _FinancialReportPage.fetchData();
        }

        private void TransactionLogClick(object sender, RoutedEventArgs e)
        {
            ActivePage = 6;
            MainFrame.NavigationService.Navigate(_TransactionLogPage);
            _TransactionLogPage.fetchData();
        }
        
        private void PersonalUsageClick(object sender, RoutedEventArgs e)
        {
            ActivePage = 7;
            MainFrame.NavigationService.Navigate(_PersonalUsagePage);
        }

        private void ExitClick(object sender, RoutedEventArgs e)
        {
            //client.StopRunning();
            System.Windows.Application.Current.Shutdown();
        }
        private async void UpdateIP(object sender, RoutedEventArgs e)
        {
/*            client.StopRunning();
            client.ipAddress = IPAddress.Parse(IPAddressBox.Text);
            //clientTask.();
            clientTask = Task.Run(client.Run);*/
        }
        public void BroadcastMessage(List<string> msg)
        {
            msg.ForEach(s => Dispatcher.Invoke(() => BroadcastArea.Text += s));
            Dispatcher.Invoke(() => BroadcastArea.ScrollToEnd());
        }

        public void ReturnData(Char code, List<string> msg)
        {
            string data = msg[0];
            Trace.WriteLine(msg[0]);
            Trace.WriteLine(code + ActivePage);

                switch (Int32.Parse(code.ToString()))
                {
                //TODO SHOW MESSAGE
                    case 1:
                        ShowMessage(data);
                        break;
                    case 2:
                        ShowMessage(data);
                        break;
                    case 3:
                        ShowMessage(data);
                        break;
                    case 4:
                        Dispatcher.Invoke(() => InventoryReportUpdate(data));
                        break;
                    case 5:
                        Dispatcher.Invoke(() => FinancialReportUpdate(data));
                        break;
                    case 6:
                        Dispatcher.Invoke(() => TransactionLogUpdate(data));
                        break;
                    case 7:
                        Dispatcher.Invoke(() => PersonalUsageUpdate(data));
                        break;
                    default:
                        break;
                }
        }

        public Dictionary<string, dynamic> GetDataToBroadcast()
        {
            return bData;
        }

        public void addCommand(char Command)
        {
            client.AddCommand(Command);
        }
        private void ShowMessage(string msg)
        {
            MessageBox.Show(msg);
        }

        private void InventoryReportUpdate(string data)
        {
            string report1;
            string report2;
            string report3;

            MainFrame.NavigationService.Navigate(_InventoryReportPage);
            List<Dictionary<string, dynamic>> TL = JsonSerializer.Deserialize<List<Dictionary<string, dynamic>>>(data);

            report1 = "ID";
            report2 = "NAME";
            report3 = "QUANTITY";

            foreach (Dictionary<string, dynamic> entry in TL)
            {
                /*                double p = Double.Parse(entry["price"]);
                                int q = Int32.Parse(entry["quantity"]);*/
                report1 += "\n" + entry["id"];
                report2 += "\n" + entry["name"];
                report3 += "\n" + entry["quantity"];
                //total += (p * q);
            }

            _InventoryReportPage.ReportBlock1.Text = report1;
            _InventoryReportPage.ReportBlock2.Text = report2;
            _InventoryReportPage.ReportBlock3.Text = report3;
        }

        private void FinancialReportUpdate(string data) 
        {
            MainFrame.NavigationService.Navigate(_FinancialReportPage);
            List<Dictionary<string, dynamic>> TL = JsonSerializer.Deserialize<List<Dictionary<string, dynamic>>>(data);

            string report1 = "NAME";
            string report2 = "ITEM COSTS";
            //string report3 = "PRICE";
            double total = 0;

            foreach (Dictionary<string, dynamic> entry in TL)
            {
                report1 += "\n" + entry["name"];
                report2 += "\n" + string.Format("{0:C}", entry["cost"]);
                //report3 += "\n" + (entry["price"].Equals("Quantity Removed") ? "" : "" + string.Format("{0:C}", entry["price"]));
                total += (Double.Parse(entry["cost"].ToString())); //TODO FIX
            }

            _FinancialReportPage.ReportBlock1.Text = report1;
            _FinancialReportPage.ReportBlock2.Text = report2;
            _FinancialReportPage.TotalBlock.Text = "Total Price Of All Items: " + total.ToString();
        }
        private void TransactionLogUpdate(string data) 
        {
            MainFrame.NavigationService.Navigate(_TransactionLogPage);
            List<Dictionary<string, dynamic>> TL = JsonSerializer.Deserialize<List<Dictionary<string, dynamic>>>(data);

            string report1 = "DATE";
            string report2 = "TYPE";
            string report3 = "ID";
            string report4 = "NAME";
            string report5 = "QUANTITY";
            string report6 = "EMPLOYEE";
            string report7 = "PRICE";

            foreach (Dictionary<string, dynamic> entry in TL)
                {
                    report1 += "\n" + entry["date"];
                    report2 += "\n" + entry["type"];
                    report3 += "\n" + entry["iid"];
                    report4 += "\n" + entry["name"];
                    report5 += "\n" + entry["quantity"];
                    report6 += "\n" + entry["employee"];
                    report7 += "\n" + (entry["type"].Equals("Quantity Removed") ? "" : "" + string.Format("{0:C}", entry["price"]));
                }

            _TransactionLogPage.ReportBlock1.Text = report1;
            _TransactionLogPage.ReportBlock2.Text = report2;
            _TransactionLogPage.ReportBlock3.Text = report3;
            _TransactionLogPage.ReportBlock4.Text = report4;
            _TransactionLogPage.ReportBlock5.Text = report5;
            _TransactionLogPage.ReportBlock6.Text = report6;
            _TransactionLogPage.ReportBlock7.Text = report7;
        }
        private void PersonalUsageUpdate(string data)
        {
            MainFrame.NavigationService.Navigate(_PersonalUsagePage);

            List<Dictionary<string, dynamic>> PU = JsonSerializer.Deserialize<List<Dictionary<string, dynamic>>>(data);
            string report1 = "DATE TAKEN";
            string report2 = "ID";
            string report3 = "NAME";
            string report4 = "QUANTITY REMOVED";

            foreach (Dictionary<string, dynamic> entry in PU)
            {
                report1 += "\n" + entry["date"];
                report2 += "\n" + entry["iid"];
                report3 += "\n" + entry["name"];
                report4 += "\n" + entry["quantity"];
            }
            _PersonalUsagePage.ReportBlock1.Text = report1;
            _PersonalUsagePage.ReportBlock2.Text = report2;
            _PersonalUsagePage.ReportBlock3.Text = report3;
            _PersonalUsagePage.ReportBlock4.Text = report4;
        }
    }
}
