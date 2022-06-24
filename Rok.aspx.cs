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
    public partial class Rok : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                isiData();
                isiDataPengguna();
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
                    cmd.CommandText = "SELECT idr, nama, l_panggul, l_pinggang, p_rok, t_panggul FROM rok INNER JOIN pelanggan ON rok.id = pelanggan.id; ";
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

        private void isiDataPengguna()
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection("Server=localhost; Port=5432;Database=;User Id=;Password="))
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

                    GridView2.DataSource = dt;
                    GridView2.DataBind();
                    panelPengguna.Visible = false;
                }
            }
            catch (Exception ex) { }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            int rowIndex = int.Parse(e.CommandArgument.ToString());
            string id = GridView1.DataKeys[rowIndex]["idr"].ToString();
           
            if (e.CommandName == "hapus")
            {
                hapusUser(id);
            }
            else if (e.CommandName == "ubah")
            {
                tbid.Text = GridView1.DataKeys[rowIndex]["idr"].ToString();
                tbl_panggul.Text = GridView1.DataKeys[rowIndex]["l_panggul"].ToString();
                tbl_pinggang.Text = GridView1.DataKeys[rowIndex]["l_pinggang"].ToString();
                tbp_rok.Text = GridView1.DataKeys[rowIndex]["p_rok"].ToString();
                tbt_panggul.Text = GridView1.DataKeys[rowIndex]["t_panggul"].ToString();

                ViewState["idr"] = id;
                btSimpan.Visible = false;
                btUpdate.Visible = true;
                panelUser.Visible = false;
                panelForm.Visible = true;
                panelPengguna.Visible = false;
                tbid.Visible = false;
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
                    string query = "delete from rok where idr =" + id;

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
                    cmd.CommandText = "insert into rok (id, l_panggul, l_pinggang, p_rok, t_panggul) values(@id, @l_panggul, @l_pinggang, @p_rok, @t_panggul)";
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(new NpgsqlParameter("@id", Convert.ToInt32(tbid.Text)));
                    cmd.Parameters.Add(new NpgsqlParameter("@l_panggul", Convert.ToInt32(tbl_panggul.Text)));
                    cmd.Parameters.Add(new NpgsqlParameter("@l_pinggang", Convert.ToInt32(tbl_pinggang.Text)));
                    cmd.Parameters.Add(new NpgsqlParameter("@p_rok", Convert.ToInt32(tbp_rok.Text)));
                    cmd.Parameters.Add(new NpgsqlParameter("@t_panggul", Convert.ToInt32(tbt_panggul.Text)));
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    connection.Close();
                    tbid.Text = " ";
                    tbl_panggul.Text = " ";
                    tbl_pinggang.Text = " ";
                    tbp_rok.Text = " ";
                    tbt_panggul.Text = " ";
                }
            }
            catch (Exception ex) { }
            isiData();

            panelUser.Visible = true;
            panelForm.Visible = false;
            panelPengguna.Visible = false;
        }

        protected void btUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection("Server=localhost; Port=5432; Database=;User Id=;Password="))
                {
                    connection.Open();
                    NpgsqlCommand cmd = new NpgsqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandText = "update rok set l_panggul=@l_panggul, l_pinggang=@l_pinggang, p_rok=@p_rok, t_panggul=@t_panggul where idr=" + ViewState["idr"];
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(new NpgsqlParameter("@id", Convert.ToInt32(tbid.Text)));
                    cmd.Parameters.Add(new NpgsqlParameter("@l_panggul", Convert.ToInt32(tbl_panggul.Text)));
                    cmd.Parameters.Add(new NpgsqlParameter("@l_pinggang", Convert.ToInt32(tbl_pinggang.Text)));
                    cmd.Parameters.Add(new NpgsqlParameter("@p_rok", Convert.ToInt32(tbp_rok.Text)));
                    cmd.Parameters.Add(new NpgsqlParameter("@t_panggul", Convert.ToInt32(tbt_panggul.Text)));
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    tbid.Text = " ";
                    tbl_panggul.Text = " ";
                    tbl_pinggang.Text = " ";
                    tbp_rok.Text = " ";
                    tbt_panggul.Text = " ";
                }
            }
            catch (Exception ex) { }
            isiData();

            panelUser.Visible = true;
            panelForm.Visible = false;
            panelPengguna.Visible = false;

        }

        protected void lbTambah_Click(object sender, EventArgs e)
        {
            panelUser.Visible = false;
            panelForm.Visible = true;
            panelPengguna.Visible = true;
            btSimpan.Visible = true;
            btUpdate.Visible = false;
            tbid.Visible = true;
        }

        protected void btBatal_Click(object sender, EventArgs e)
        {
            tbid.Text = " ";
            tbl_panggul.Text = " ";
            tbl_pinggang.Text = " ";
            tbp_rok.Text = " ";
            tbt_panggul.Text = " ";
            panelUser.Visible = true;
            panelPengguna.Visible = false;
            panelForm.Visible = false;
        }
    }
}