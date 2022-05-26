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

namespace Assignment_Gui
{
    delegate void BroadcastMessage(List<string> msg);
    delegate void ShowMessage(string msg);
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private WPFClient client;
        private Task clientTask;

        public MainWindow()
        {
            InitializeComponent();
            client =  new WPFClient(BroadcastMessage);
            clientTask = Task.Run(client.Run);
        }

        private void AddItemClick(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(new AddItemPage());
        }

        private void AddQuantityClick(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(new AddQuantityPage());
        }

        private void TakeQuantityClick(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(new TakeQuantityPage());
        }

        private void InventoryReportClick(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(new InventoryReportPage());
        }

        private void FinancialReportClick(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(new FinancialReportPage());
        }

        private void TransactionLogClick(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(new TransactionLogPage());
        }
        
        private void PersonalUsageClick(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(new PersonalUsagePage());
        }

        private void ExitClick(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
        public void BroadcastMessage(List<string> msg)
        {
            msg.ForEach(s => Dispatcher.Invoke(() => BroadcastArea.Text += s));
            Dispatcher.Invoke(() => BroadcastArea.ScrollToEnd());
        }
        private void ShowMessage(string msg)
        {
            MessageBox.Show(msg);
        }
    }
}
