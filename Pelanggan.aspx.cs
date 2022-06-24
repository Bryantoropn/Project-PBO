using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Npgsql;

namespace TRY1
{
    public partial class Pelanggan : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                isiData();
            }
        }

        private void isiData()
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection("Server=localhost; Port=5432; Database=;User Id=;Password="))
                {
                    connection.Open();
                    NpgsqlCommand cmd = new NpgsqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandText = "select * from pelanggan order by id";
                    cmd.CommandType = CommandType.Text;
                    NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);

                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    cmd.Dispose();
                    connection.Close();

                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                }
            }
            catch (Exception ex) { }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            int rowIndex = int.Parse(e.CommandArgument.ToString());
            string id = GridView1.DataKeys[rowIndex]["id"].ToString();

            if (e.CommandName == "hapus")
            {
                hapusUser(id);
            }
            else if (e.CommandName == "ubah")
            {
                tbnama.Text = GridView1.DataKeys[rowIndex]["nama"].ToString();
                tbno_telp.Text = GridView1.DataKeys[rowIndex]["no_telp"].ToString();
                tbalamat.Text = GridView1.DataKeys[rowIndex]["alamat"].ToString();


                ViewState["id"] = id;
                btSimpan.Visible = false;
                btUpdate.Visible = true;
                panelUser.Visible = false;
                panelForm.Visible = true;


            }
        }

        private void hapusUser(string id)
        {

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection("Server=localhost; Port=5432; Database=;User Id=;Password="))
                {
                    connection.Open();
                    NpgsqlCommand cmd = new NpgsqlCommand();
                    cmd.Connection = connection;
                    string query = "delete from pelanggan where id = " + ViewState["id"];

                    cmd.CommandText = query;
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();

                    cmd.Dispose();
                    connection.Close();

                }
            }
            catch (Exception ex) { }
            isiData();
        }


        protected void btSimpan_Click(object sender, EventArgs e)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection("Server=localhost; Port=5432; Database=;User Id=;Password="))
                {
                    connection.Open();
                    NpgsqlCommand cmd = new NpgsqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandText = "Insert into pelanggan (nama, no_telp, alamat) values(@Nama,@NoTelp,@Alamat)";
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(new NpgsqlParameter("@Nama", tbnama.Text));
                    cmd.Parameters.Add(new NpgsqlParameter("@NoTelp", tbno_telp.Text));
                    cmd.Parameters.Add(new NpgsqlParameter("@Alamat", tbalamat.Text));
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    connection.Close();
                    tbnama.Text = " ";
                    tbno_telp.Text = " ";
                    tbalamat.Text = " ";

                }
            }
            catch (Exception ex) { }
            isiData();

            panelUser.Visible = true;
            panelForm.Visible = false;
        }

        protected void btUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection("Server=localhost; Port=5432;Database=;User Id=;Password="))
                {
                    connection.Open();
                    NpgsqlCommand cmd = new NpgsqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandText = "update pelanggan set nama=@Nama,no_telp=@NoTelp,alamat=@Alamat where id=" + ViewState["id"];
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(new NpgsqlParameter("@Nama", tbnama.Text));
                    cmd.Parameters.Add(new NpgsqlParameter("@NoTelp", tbno_telp.Text));
                    cmd.Parameters.Add(new NpgsqlParameter("@Alamat", tbalamat.Text));
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    tbnama.Text = " ";
                    tbno_telp.Text = " ";
                    tbalamat.Text = " ";
                }
            }
            catch (Exception ex) { }
            isiData();

            panelUser.Visible = true;
            panelForm.Visible = false;
        }

        protected void lbTambah_Click(object sender, EventArgs e)
        {
            panelUser.Visible = false;
            panelForm.Visible = true;
            btSimpan.Visible = true;
            btUpdate.Visible = false;
        }

        protected void btBatal_Click(object sender, EventArgs e)
        {
            tbnama.Text = " ";
            tbno_telp.Text = " ";
            tbalamat.Text = " ";
            panelUser.Visible = true;
            panelForm.Visible = false;
        }
    }
}