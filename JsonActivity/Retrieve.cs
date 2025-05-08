using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows.Forms;
using JsonActivity;
using Newtonsoft.Json;

namespace jsonActivity
{
    public partial class Retrieve : Form
    {
        public Retrieve()
        {
            InitializeComponent();
        }
        private void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                string filePath = "C:\\Users\\63981\\source\\repos\\JsonActivity\\JsonActivity\\shoppinglist.Json";
                if (File.Exists(filePath))
                {
                    string jsonData = File.ReadAllText(filePath);
                    DataTable dataTable = JsonConvert.DeserializeObject<DataTable>(jsonData);
                    dataGridView1.DataSource = dataTable;
                }
                else
                {
                    MessageBox.Show("JSON file not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnOpenAddForm_Click(object sender, EventArgs e)
        {
            Add addForm = new Add();
            addForm.Show(); // Opens the Add form as a separate window
        }
    }

    public class GroceryItem
    {
        public string Item { get; set; }
        public int Quantity { get; set; }
    }
}