using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;



namespace InventoryDatabaseApp
{
    public partial class Form1 : Form
    {
        private string connectionString =
    @"Data Source=(localdb)\MSSQLLocalDB;
      Initial Catalog=InventoryDB;
      Integrated Security=True;";
        private void LoadProducts()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql =
                    "SELECT ProductId, ProductName, Category, UnitPrice, Quantity, " +
                    "(UnitPrice * Quantity) AS StockValue FROM Products";

                SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                DataTable table = new DataTable();

                adapter.Fill(table);
                dgvProducts.DataSource = table;
            }
        }
        private int selectedProductId = 0;
        private void dgvProducts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvProducts.Rows[e.RowIndex];

                selectedProductId = Convert.ToInt32(row.Cells["ProductId"].Value);
                txtProductName.Text = row.Cells["ProductName"].Value.ToString();
                txtCategory.Text = row.Cells["Category"].Value.ToString();
                txtUnitPrice.Text = row.Cells["UnitPrice"].Value.ToString();
                txtQuantity.Text = row.Cells["Quantity"].Value.ToString();
            }
        }
        private void ClearInput()
        {
            selectedProductId = 0;
            txtProductName.Clear();
            txtCategory.Clear();
            txtUnitPrice.Clear();
            txtQuantity.Clear();
            txtProductName.Focus();
        }
        private void SearchProducts()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                string sql =
                    "SELECT ProductId, ProductName, Category, UnitPrice, Quantity, " +
                    "(UnitPrice * Quantity) AS StockValue FROM Products " +
                    "WHERE ProductName LIKE @Search OR Category LIKE @Search";

                SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);

                adapter.SelectCommand.Parameters.AddWithValue(
                    "@Search", "%" + txtSearch.Text.Trim() + "%");

                DataTable table = new DataTable();

                adapter.Fill(table);
                dgvProducts.DataSource = table;
            }
        }

        public Form1()
        {
            

            InitializeComponent();
            LoadProducts();

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            LoadProducts();

        }


private void btnAdd_Click(object sender, EventArgs e)

        {
           
            string name = txtProductName.Text.Trim();
            string category = txtCategory.Text.Trim();

            bool okPrice = decimal.TryParse(txtUnitPrice.Text.Trim(), out decimal price);
            bool okQuantity = int.TryParse(txtQuantity.Text.Trim(), out int quantity);

            

            if (name == "" || category == "" || !okPrice || !okQuantity ||
                price <= 0 || quantity < 0)
            {
                MessageBox.Show("Please enter valid product details.");
                return;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql =
                    "INSERT INTO Products (ProductName, Category, UnitPrice, Quantity) " +
                    "VALUES (@ProductName, @Category, @UnitPrice, @Quantity)";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@ProductName", name);
                    command.Parameters.AddWithValue("@Category", category);
                    command.Parameters.AddWithValue("@UnitPrice", price);
                    command.Parameters.AddWithValue("@Quantity", quantity);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Product added successfully.");

            LoadProducts();
            ClearInput();
        }



        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (selectedProductId == 0)
            {
                MessageBox.Show("Please select a product to update.");
                return;
            }

            string name = txtProductName.Text.Trim();
            string category = txtCategory.Text.Trim();

            bool okPrice = decimal.TryParse(txtUnitPrice.Text, out decimal price);
            bool okQuantity = int.TryParse(txtQuantity.Text, out int quantity);

            if (name == "" || category == "" || !okPrice || !okQuantity ||
                price <= 0 || quantity < 0)
            {
                MessageBox.Show("Please enter valid product details.");
                return;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                string sql =
                    "UPDATE Products SET ProductName=@ProductName, Category=@Category, " +
                    "UnitPrice=@UnitPrice, Quantity=@Quantity " +
                    "WHERE ProductId=@ProductId";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {

                    command.Parameters.AddWithValue("@ProductName", name);
                    command.Parameters.AddWithValue("@Category", category);
                    command.Parameters.AddWithValue("@UnitPrice", price);
                    command.Parameters.AddWithValue("@Quantity", quantity);
                    command.Parameters.AddWithValue("@ProductId", selectedProductId);

                    connection.Open();
                    command.ExecuteNonQuery();

                    MessageBox.Show("Product updated successfully.");

                    LoadProducts();
                    ClearInput();
                }
            }
        }
       




        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedProductId == 0)
            {
                MessageBox.Show("Please select a product to delete.");
                return;
            }

            DialogResult result = MessageBox.Show(
                "Delete selected product?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result != DialogResult.Yes)
            {
                return;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                string sql = "DELETE FROM Products WHERE ProductId=@ProductId";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@ProductId", selectedProductId);

                    connection.Open();
                    command.ExecuteNonQuery();

                    MessageBox.Show("Product deleted successfully.");

                    LoadProducts();
                    ClearInput();
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchProducts();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearInput();
        }
    }
}

            

        

                
                
            
        
    
