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
using ui_command;

namespace Assignment_Gui.Pages
{
    /// <summary>
    /// Interaction logic for InventoryReport.xaml
    /// </summary>
    public partial class InventoryReportPage : Page
    {
        string report1;
        string report2;
        string report3;
        List<ItemDTO> IR;

        public InventoryReportPage()
        {
            InitializeComponent();


            report1 = "ID";
            report2 = "NAME";
            report3 = "QUANTITY";
           /* foreach (ItemDTO i in IR)
            {
                report1 += "\n" + i.ID;
                report2 += "\n" + i.Name;
                report3 += "\n" + i.Quantity;
            }*/
            ReportBlock1.Text = report1;
            ReportBlock2.Text = report2;
            ReportBlock3.Text = report3;
        }

        public void fetchData()
        {
            ((MainWindow)Application.Current.MainWindow).addCommand('4');
        }

    }
}