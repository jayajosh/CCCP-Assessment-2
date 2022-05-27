﻿using System;
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
using AssignmentMain;
using DataGateway;
using dto;

namespace Assignment_Gui.Pages
{
    /// <summary>
    /// Interaction logic for AddItem.xaml
    /// </summary>
    public partial class AddItemPage : Page
    {
        private readonly GatewayFacade facade;
        private readonly DTOConverter converter;
        public bool click;
        public AddItemPage()
        {
            InitializeComponent();
            facade = new GatewayFacade();
            converter = new DTOConverter();
        }

        private void AddItemsClick(object sender, RoutedEventArgs e)
        {
            List<string> data = new List<string>{Employee.Text, ID.Text, Name.Text, Quantity.Text, Price.Text};
            ((MainWindow)Application.Current.MainWindow).bData = data;
            ((MainWindow)Application.Current.MainWindow).GetDataToBroadcast();
            ((MainWindow)Application.Current.MainWindow).addCommand('1');
            Trace.WriteLine(data[2]);
            //bool success = Program.AddItemToStock(, Int32.Parse(ID.Text), Name.Text, Int32.Parse(Quantity.Text), Double.Parse(Price.Text));
            try
            {
/*                int employeeID = Int32.Parse(Employee.Text);
                if (facade.FindEmployee(employeeID) == null)
                {
                    throw new Exception("ERROR: Employee not found");
                }

                int itemId = Int32.Parse(ID.Text);
                string itemName = Name.Text;
                int itemQuantity = Int32.Parse(Quantity.Text);
                double itemPrice = Double.Parse(Price.Text);

                if (itemPrice < 0)
                {
                    throw new Exception("ERROR: Price below 0");
                }

                ItemDTO i = facade.AddItem(itemId, itemName, itemQuantity);
                facade.AddTransaction("Item Added", i.ID, i.Name, itemPrice, i.Quantity, employeeID);

                MessageBox.Show("Item Added");*/
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }
    }  
}
