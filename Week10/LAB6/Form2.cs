using System;
using System.Windows.Forms;

namespace LAB6
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int itemNo = int.Parse(textBox1.Text);
                string description = textBox2.Text;
                decimal price = decimal.Parse(textBox3.Text);

                InventoryItem newItem = new InventoryItem { ItemNo = itemNo, Description = description, Price = price };

                InventoryDB.AddItem(newItem);

                MessageBox.Show("Item added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Close();
            }
            catch (FormatException)
            {
                MessageBox.Show("Error adding item: Please ensure that you enter valid numbers for the item number and price.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding item: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
