using AssignmentMain;
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
    public partial class TakeQuantityPage : Page
    {
        public TakeQuantityPage()
        {
            InitializeComponent();
    }

        private void TakeQuantityClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Dictionary<string, dynamic> data = new Dictionary<string, dynamic> { { "employeeID", Int32.Parse(Employee.Text) }, { "itemID", Int32.Parse(ID.Text) }, { "quantity", Int32.Parse(Quantity.Text) } };
                ((MainWindow)Application.Current.MainWindow).bData = data;
                ((MainWindow)Application.Current.MainWindow).GetDataToBroadcast();
                ((MainWindow)Application.Current.MainWindow).addCommand('3');
                /*int employeeID = Int32.Parse(Employee.Text);
                int itemId = Int32.Parse(ID.Text);
                int quantityToRemove = Int32.Parse(Quantity.Text);

                if (facade.FindEmployee(employeeID) == null)
                {
                    throw new Exception("ERROR: Employee not found");
                }

                ItemDTO itemDTO = facade.FindItem(itemId);
                if (itemDTO == null)
                {
                    throw new Exception("ERROR: Item not found");
                }


                Item item = converter.Convert(itemDTO);


                facade.RemoveQuantity(itemId, quantityToRemove);

                facade.AddTransaction("Quantity Removed", item.ID, item.Name, -1, quantityToRemove, employeeID);
                MessageBox.Show($"{employeeID} has removed {quantityToRemove} of Item ID: {itemId} on {DateTime.Now.ToString("dd/MM/yyyy")}");*/
            }

            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
}
    }
}
