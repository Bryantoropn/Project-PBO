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
    public partial class Celana : System.Web.UI.Page
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
                    cmd.CommandText = "SELECT idc, nama, l_paha, l_pinggang, l_pisak, l_ujung_celana, p_celana FROM celana INNER JOIN pelanggan ON celana.id = pelanggan.id; ";
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
            string id = GridView1.DataKeys[rowIndex]["idc"].ToString();
           
            if (e.CommandName == "hapus")
            {
                hapusUser(id);
            }
            else if (e.CommandName == "ubah")
            {
                tbid.Text = GridView1.DataKeys[rowIndex]["idc"].ToString();
                tbl_paha.Text = GridView1.DataKeys[rowIndex]["l_paha"].ToString();
                tbl_pinggang.Text = GridView1.DataKeys[rowIndex]["l_pinggang"].ToString();
                tbl_pisak.Text = GridView1.DataKeys[rowIndex]["l_pisak"].ToString();
                tbl_ujung_celana.Text = GridView1.DataKeys[rowIndex]["l_ujung_celana"].ToString();
                tbp_celana.Text = GridView1.DataKeys[rowIndex]["p_celana"].ToString();

                ViewState["idc"] = id;
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
                    string query = "delete from celana where idc =" + id;

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
                    cmd.CommandText = "insert into celana (id, l_paha, l_pinggang, l_pisak, l_ujung_celana, p_celana) values(@id, @l_paha, @l_pinggang, @l_pisak, @l_ujung_celana, @p_celana)";
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(new NpgsqlParameter("@id", Convert.ToInt32(tbid.Text)));
                    cmd.Parameters.Add(new NpgsqlParameter("@l_paha", Convert.ToInt32(tbl_paha.Text)));
                    cmd.Parameters.Add(new NpgsqlParameter("@l_pinggang", Convert.ToInt32(tbl_pinggang.Text)));
                    cmd.Parameters.Add(new NpgsqlParameter("@l_pisak", Convert.ToInt32(tbl_pisak.Text)));
                    cmd.Parameters.Add(new NpgsqlParameter("@l_ujung_celana", Convert.ToInt32(tbl_ujung_celana.Text)));
                    cmd.Parameters.Add(new NpgsqlParameter("@p_celana", Convert.ToInt32(tbp_celana.Text)));
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    connection.Close();
                    tbid.Text = " ";
                    tbl_paha.Text = " ";
                    tbl_pinggang.Text = " ";
                    tbl_pisak.Text = " ";
                    tbl_ujung_celana.Text = " ";
                    tbp_celana.Text = " ";
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
                    cmd.CommandText = "update celana set l_paha=@l_paha, l_pinggang=@l_pinggang, l_pisak=@l_pisak, l_ujung_celana=@l_ujung_celana, p_celana=@p_celana where idc=" + ViewState["idc"];
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(new NpgsqlParameter("@l_paha", Convert.ToInt32(tbl_paha.Text)));
                    cmd.Parameters.Add(new NpgsqlParameter("@l_pinggang", Convert.ToInt32(tbl_pinggang.Text)));
                    cmd.Parameters.Add(new NpgsqlParameter("@l_pisak", Convert.ToInt32(tbl_pisak.Text)));
                    cmd.Parameters.Add(new NpgsqlParameter("@l_ujung_celana", Convert.ToInt32(tbl_ujung_celana.Text)));
                    cmd.Parameters.Add(new NpgsqlParameter("@p_celana", Convert.ToInt32(tbp_celana.Text)));
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    tbid.Text = " ";
                    tbl_paha.Text = " ";
                    tbl_pinggang.Text = " ";
                    tbl_pisak.Text = " ";
                    tbl_ujung_celana.Text = " ";
                    tbp_celana.Text = " ";
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
            tbl_paha.Text = " ";
            tbl_pinggang.Text = " ";
            tbl_pisak.Text = " ";
            tbl_ujung_celana.Text = " ";
            tbp_celana.Text = " ";
            panelUser.Visible = true;
            panelPengguna.Visible = false;
            panelForm.Visible = false;
        }
    }
}