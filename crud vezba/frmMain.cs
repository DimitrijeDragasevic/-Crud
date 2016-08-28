using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace crud_vezba
{
    public partial class frmMain : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename=C:\USERS\LENOVO\SOURCE\REPOS\PROJEKATMINIOOP\MALA-VEZBA\CRUD VEZBA\CRUD VEZBA\SAMPLE.MDF;Integrated Security = True");
        SqlCommand cmd;
        SqlDataAdapter adapt;
        int ID = 0;
        public frmMain()
             
        {
            InitializeComponent();
            DisplayData();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btn_Insert_Click(object sender, EventArgs e)
        {
            if (txt_Name.Text != "" && txt_State.Text != "")
            {
                cmd = new SqlCommand("insert into tbl_Record(Name,State) values(@name,@state)", con);
                con.Open();
                cmd.Parameters.AddWithValue("@name", txt_Name.Text);
                cmd.Parameters.AddWithValue("@state", txt_State.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Record Inserted Successfully");
                DisplayData();
                ClearData();
            }
            else
            {
                MessageBox.Show("Please Provide Details!");
            }
        }
        private void DisplayData()
        {
            con.Open();
            DataTable dt = new DataTable();
            adapt = new SqlDataAdapter("select * from tbl_Record", con);
            adapt.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }
        //Clear Data  
        private void ClearData()
        {
            txt_Name.Text = "";
            txt_State.Text = "";
            ID = 0;
        }
       

        private void btn_Update_Click(object sender, EventArgs e)
        {
            if (txt_Name.Text != "" && txt_State.Text != "")
            {
                cmd = new SqlCommand("update tbl_Record set Name=@name,State=@state where ID=@id", con);
                con.Open();
                cmd.Parameters.AddWithValue("@id", ID);
                cmd.Parameters.AddWithValue("@name", txt_Name.Text);
                cmd.Parameters.AddWithValue("@state", txt_State.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record Updated Successfully");
                con.Close();
                DisplayData();
                ClearData();
            }
            else
            {
                MessageBox.Show("Please Select Record to Update");
            }
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {

            if (ID != 0)
            {
                cmd = new SqlCommand("delete tbl_Record where ID=@id", con);
                con.Open();
                cmd.Parameters.AddWithValue("@id", ID);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Record Deleted Successfully!");
                DisplayData();
                ClearData();
            }
            else
            {
                MessageBox.Show("Please Select Record to Delete");
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
            txt_Name.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            txt_State.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
        }
    }
}
