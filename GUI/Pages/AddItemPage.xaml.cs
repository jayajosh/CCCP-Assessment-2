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
using AssignmentMain;

namespace Assignment_Gui.Pages
{
    /// <summary>
    /// Interaction logic for AddItem.xaml
    /// </summary>
    public partial class AddItemPage : Page
    {
        public bool click;
        public AddItemPage()
        {
            InitializeComponent();
        }

        private void AddItemsClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Dictionary<string,dynamic> data = new Dictionary<string, dynamic> { {"employeeID", Int32.Parse(Employee.Text) }, { "itemID", Int32.Parse(ID.Text) }, { "name", Name.Text }, { "quantity", Int32.Parse(Quantity.Text) }, { "price", Double.Parse(Price.Text) } };
                ((MainWindow)Application.Current.MainWindow).bData = data;
                ((MainWindow)Application.Current.MainWindow).GetDataToBroadcast();
                ((MainWindow)Application.Current.MainWindow).addCommand('1');
                //bool success = Program.AddItemToStock(, Int32.Parse(ID.Text), Name.Text, Int32.Parse(Quantity.Text), Double.Parse(Price.Text));

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
