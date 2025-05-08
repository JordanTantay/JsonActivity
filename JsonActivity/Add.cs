using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace jsonActivity
{
    public partial class Add : Form
    {
        private List<GroceryItem> groceryList = new List<GroceryItem>();

        public Add()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (groceryList.Count >= 5)
                {
                    MessageBox.Show("You can only add up to 5 items.");
                    return;
                }

                string itemName = txtItem.Text.Trim();
                if (string.IsNullOrEmpty(itemName))
                {
                    MessageBox.Show("Item name cannot be empty.");
                    return;
                }

                if (!int.TryParse(txtQuantity.Text, out int quantity) || quantity <= 0)
                {
                    MessageBox.Show("Please enter a valid quantity.");
                    return;
                }

                groceryList.Add(new GroceryItem { Item = itemName, Quantity = quantity });
                listBox1.Items.Add($"{itemName} - {quantity}");
                txtItem.Clear();
                txtQuantity.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while adding the item: {ex.Message}");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (groceryList.Count == 0)
                {
                    MessageBox.Show("The grocery list is empty. Add items before saving.");
                    return;
                }

                string filePath = "C:\\Users\\63981\\source\\repos\\JsonActivity\\JsonActivity\\shoppinglist.Json";
                string jsonContent = JsonConvert.SerializeObject(groceryList, Formatting.Indented);
                File.WriteAllText(filePath, jsonContent);

                MessageBox.Show("Grocery list saved successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while saving the list: {ex.Message}");
            }
        }

        private void Add_Load(object sender, EventArgs e)
        {
            try
            {
                string filePath = "C:\\Users\\63981\\source\\repos\\JsonActivity\\JsonActivity\\shoppinglist.Json";
                if (File.Exists(filePath))
                {
                    string jsonContent = File.ReadAllText(filePath);
                    groceryList = JsonConvert.DeserializeObject<List<GroceryItem>>(jsonContent) ?? new List<GroceryItem>();

                    foreach (var item in groceryList)
                    {
                        listBox1.Items.Add($"{item.Item} - {item.Quantity}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading the grocery list: {ex.Message}");
            }
        }
    }
}