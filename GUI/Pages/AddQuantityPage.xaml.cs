using AssignmentMain;
using DataGateway;
using dto;
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

namespace Assignment_Gui.Pages
{
    /// <summary>
    /// Interaction logic for AddQuantityPage.xaml
    /// </summary>
    public partial class AddQuantityPage : Page
    {
        private readonly GatewayFacade facade;
        private readonly DTOConverter converter;
        public AddQuantityPage()
        {
            InitializeComponent();
            facade = new GatewayFacade();
            converter = new DTOConverter();
        }

        private void AddQuantityClick(object sender, RoutedEventArgs e)
        {
            try
            {
                int employeeID = Int32.Parse(Employee.Text);
                if (facade.FindEmployee(employeeID) == null)
                {
                    throw new Exception("ERROR: Employee not found");
                }

                int itemId = Int32.Parse(ID.Text);
                ItemDTO itemDTO = facade.FindItem(itemId);
                if (itemDTO == null)
                {
                    throw new Exception("ERROR: Item not found");
                }

                Item item = converter.Convert(itemDTO);

                int quantityToAdd = Int32.Parse(Quantity.Text);
                double itemPrice = Double.Parse(Price.Text);

                if (itemPrice < 0)
                {
                    throw new Exception("ERROR: Price below 0");
                }

                facade.AddQuantity(itemId, quantityToAdd);
                facade.AddTransaction("Quantity Added", item.ID, item.Name, itemPrice, quantityToAdd, employeeID);
                MessageBox.Show($"{quantityToAdd} items have been added to Item ID: {itemId} on {DateTime.Now.ToString("dd / MM / yyyy")}");
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }
    }
}
