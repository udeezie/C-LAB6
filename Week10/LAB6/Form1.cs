using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace LAB6
{
    public partial class Form1 : Form
    {
        private List<InventoryItem> _items;

        public Form1()
        {
            InitializeComponent();
            _items = InventoryDB.GetItems();
            LoadItems();
        }

        private void LoadItems()
        {
            try
            {
                if (_items.Count == 0)
                {
                    // Load default items from file if inventory is empty
                    string[] defaultItems = {
                        "3245649|Eggs|3.99",
                        "3762592|Cheese|2.99",
                        "9210584|Bread|3.99",
                        "4738459|Fish|12.99"
                    };

                    foreach (var item in defaultItems)
                    {
                        string[] itemInfo = item.Split('|');
                        int itemNo = int.Parse(itemInfo[0]);
                        string description = itemInfo[1];
                        decimal price = decimal.Parse(itemInfo[2]);

                        _items.Add(new InventoryItem { ItemNo = itemNo, Description = description, Price = price });
                    }

                    // Save default items to file
                    InventoryDB.SaveItems(_items);
                }

                DisplayItems();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading items: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DisplayItems()
        {
            listBox1.Items.Clear();
            foreach (var item in _items)
            {
                listBox1.Items.Add($"{item.ItemNo} - {item.Description} - {item.Price}");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Form2 addItemForm = new Form2();
                addItemForm.ShowDialog();
                _items = InventoryDB.GetItems(); // Refresh items after adding
                DisplayItems(); // Refresh ListBox
            }
            catch (FormatException)
            {
                MessageBox.Show("Error adding item: Please ensure that you enter valid numbers for the item number and price.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                DialogResult result = MessageBox.Show("Are you sure you want to delete this item?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        InventoryItem selectedItem = _items[listBox1.SelectedIndex];
                        _items.Remove(selectedItem);
                        InventoryDB.SaveItems(_items);
                        DisplayItems(); // Refresh ListBox after deletion
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error deleting item: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select an item to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
