/// <summary>
/// Creator: Skyann Heintz
/// Created: 2025/02/17
/// Summary: This class handles the needlist interface for adding a new item.
/// It interacts with the AddItemManger to validate user input and insert
/// a new item.
/// Last Upaded By: Stan Anderson
/// Last Updated: 2025/04/07
/// What Was Changed: Added connections
/// </summary>
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using LogicLayer;

namespace WpfPresentation.Pages
{
    public partial class AddItemDeskTop : Page
    {
        MainWindow main = MainWindow.GetMainWindow(); // Main window Reference

        private NeedListManager _needListManager;
        private int _projectID;

        /// <summary>
        /// Creator:Skyann Heintz
        /// Created: 2025/02/10
        /// Summary: Initializes the AddItem page and sets up the AddItemManager instance. 
        /// This constructor prepares the UI components and ensures user data management
        /// can be accessed during account creation. 
        /// Last Upaded By:
        /// Last Updated:
        /// What Was Changed:
        /// </summary>
        public AddItemDeskTop(int projectID)
        {
            InitializeComponent();
            _needListManager = new NeedListManager();
            _projectID = projectID;
        }


        ///<summary>
        /// Creator: Skyann Heintz
        /// Created: 2025/02/17
        /// Summary: This method handles the click event for saving add item. It validates that all necessary fields are filled out, including
        /// item name, quantity, price and description. It ensures that the quantity must be a whole number and price cannot be longer than 
        /// 2 decimal places. It also ensures in the validation that if the user enters an '$' for price that it is removed by the system 
        /// before it is entered into the database. If all validations pass, it inserts the item into the system. Success and error messages 
        /// are displayed based on the result of the 
        /// insertion attempt. If an exception occurs during the process, an error message is shown.
        /// Last Updated By: Stan Anderson
        /// Last Updated: 2025/04/07
        /// What Was Changed: Changed redirect
        /// </summary>
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string itemName = tbItemName.Text;
                string quantityText = tbQuantity.Text;
                string priceText = tbPrice.Text;
                string description = tbDescription.Text;


                if (string.IsNullOrWhiteSpace(itemName))
                {
                    MessageBox.Show("Please enter a name for your item.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }


                if (itemName.Length > 50)
                {
                    MessageBox.Show("The name of your item cannot exceed 50 characters.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (!int.TryParse(quantityText, out int quantity))
                {
                    MessageBox.Show("Please enter the quantity needed of your item. " +
                        "Please ensure the quanitity entered is a whole number.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (quantity < 1)
                {
                    MessageBox.Show("Quantity must be at least 1.", "Invalid Quantity", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (priceText.StartsWith("$"))
                {
                    priceText = priceText.Substring(1); // Removes the "$" symbol
                }

                if (!decimal.TryParse(priceText, out decimal price))
                {
                    MessageBox.Show("Please enter the price of your item.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (price <= 0)
                {
                    MessageBox.Show("Price must be greater than zero.", "Invalid Price", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Check if price has more than 2 decimal places
                if (priceText.Contains("."))
                {
                    string[] priceParts = priceText.Split('.');
                    if (priceParts.Length > 1 && priceParts[1].Length > 2)
                    {
                        MessageBox.Show("Price cannot have more than two decimal places.", "Invalid Price Format", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                }

                if (description.Length > 250)
                {
                    MessageBox.Show("Description of the item cannot exceed 250 characters.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                

                // Check if the item already exists in the NeedList
                if (_needListManager.SelectExistingItemName(_projectID, itemName))
                {
                    MessageBox.Show($"The item '{itemName}' is already in the need list. Please enter a different item " +
                        $"or update your item in the list.",
                                    "Duplicate Entry", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                bool success = _needListManager.InsertItemToNeedList(_projectID, itemName, quantity, price, description);

                if (success)
                {
                    MessageBox.Show("Item added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.NavigationService.Navigate(new ViewNeedList(_projectID));
                }
                else
                {
                    MessageBox.Show("Failed to add item.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        ///<summary>
        /// Creator: Skyann Heintz
        /// Created: 2025/02/19
        /// Summary: Handles the Cancel button click event. If the user clicks cancel, a confirmation message
        /// will appear to ask them if they are sure they want to cancel.
        /// Last Updated By: Stan Anderson
        /// Last Updated: 2025/04/07
        /// What Was Changed: Changed navigation to go back instead of close the whole app
        /// </summary>
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to cancel? Any unsaved data will be lost.",
                                                     "Confirm Cancellation", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                NavigationService.GoBack();
            }
        }
    }
}

