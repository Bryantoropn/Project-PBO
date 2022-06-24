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
    public partial class Baju : System.Web.UI.Page
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
                    cmd.CommandText = "SELECT idb, nama, l_dada, l_kerah, l_ujung_lengan, p_bahu, p_baju, p_lengan FROM baju INNER JOIN pelanggan ON baju.id = pelanggan.id; ";
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
            string id = GridView1.DataKeys[rowIndex]["idb"].ToString();
           
            if (e.CommandName == "hapus")
            {
                hapusUser(id);
            }
            else if (e.CommandName == "ubah")
            {
                tbid.Text = GridView1.DataKeys[rowIndex]["idb"].ToString();
                tbl_dada.Text = GridView1.DataKeys[rowIndex]["l_dada"].ToString();
                tbl_kerah.Text = GridView1.DataKeys[rowIndex]["l_kerah"].ToString();
                tbl_ujung_lengan.Text = GridView1.DataKeys[rowIndex]["l_ujung_lengan"].ToString();
                tbp_bahu.Text = GridView1.DataKeys[rowIndex]["p_bahu"].ToString();
                tbp_baju.Text = GridView1.DataKeys[rowIndex]["p_baju"].ToString();
                tbp_lengan.Text = GridView1.DataKeys[rowIndex]["p_lengan"].ToString();

                ViewState["idb"] = id;
                btSimpan.Visible = false;
                btUpdate.Visible = true;
                panelUser.Visible = false;
                panelForm.Visible = true;
                panelPengguna.Visible = true;

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
                    string query = "delete from baju where idb =" + id;

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
                    cmd.CommandText = "insert into baju (id, l_dada, l_kerah, l_ujung_lengan, p_bahu, p_baju, p_lengan) values(@id, @l_dada, @l_kerah, @l_ujung_lengan, @p_bahu, @p_baju, @p_lengan)";
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(new NpgsqlParameter("@id", Convert.ToInt32(tbid.Text)));
                    cmd.Parameters.Add(new NpgsqlParameter("@l_dada", Convert.ToInt32(tbl_dada.Text)));
                    cmd.Parameters.Add(new NpgsqlParameter("@l_kerah", Convert.ToInt32(tbl_kerah.Text)));
                    cmd.Parameters.Add(new NpgsqlParameter("@l_ujung_lengan", Convert.ToInt32(tbl_ujung_lengan.Text)));
                    cmd.Parameters.Add(new NpgsqlParameter("@p_bahu", Convert.ToInt32(tbp_bahu.Text)));
                    cmd.Parameters.Add(new NpgsqlParameter("@p_baju", Convert.ToInt32(tbp_baju.Text)));
                    cmd.Parameters.Add(new NpgsqlParameter("@p_lengan", Convert.ToInt32(tbp_lengan.Text)));
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    connection.Close();
                    tbid.Text = " ";
                    tbl_dada.Text = " ";
                    tbl_kerah.Text = " ";
                    tbl_ujung_lengan.Text = " ";
                    tbp_bahu.Text = " ";
                    tbp_baju.Text = " ";
                    tbp_lengan.Text = " ";
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
                    cmd.CommandText = "update baju set l_dada=@l_dada, l_kerah=@l_kerah, l_ujung_lengan=@l_ujung_lengan, p_bahu=@p_bahu, p_baju=@p_baju, p_lengan=@p_lengan where idb=" + ViewState["idb"];
                    cmd.CommandType = CommandType.Text;
                    //cmd.Parameters.Add(new NpgsqlParameter("@id", tbid.Text));
                    cmd.Parameters.Add(new NpgsqlParameter("@l_dada", Convert.ToInt32(tbl_dada.Text)));
                    cmd.Parameters.Add(new NpgsqlParameter("@l_kerah", Convert.ToInt32(tbl_kerah.Text)));
                    cmd.Parameters.Add(new NpgsqlParameter("@l_ujung_lengan", Convert.ToInt32(tbl_ujung_lengan.Text)));
                    cmd.Parameters.Add(new NpgsqlParameter("@p_bahu", Convert.ToInt32(tbp_bahu.Text)));
                    cmd.Parameters.Add(new NpgsqlParameter("@p_baju", Convert.ToInt32(tbp_baju.Text)));
                    cmd.Parameters.Add(new NpgsqlParameter("@p_lengan", Convert.ToInt32(tbp_lengan.Text)));
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    tbid.Text = " ";
                    tbl_dada.Text = " ";
                    tbl_kerah.Text = " ";
                    tbl_ujung_lengan.Text = " ";
                    tbp_bahu.Text = " ";
                    tbp_baju.Text = " ";
                    tbp_lengan.Text = " ";
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
        }

        protected void btBatal_Click(object sender, EventArgs e)
        {
            tbid.Text = " ";
            tbl_dada.Text = " ";
            tbl_kerah.Text = " ";
            tbl_ujung_lengan.Text = " ";
            tbp_bahu.Text = " ";
            tbp_baju.Text = " ";
            tbp_lengan.Text = " ";
            panelUser.Visible = true;
            panelPengguna.Visible = false;
            panelForm.Visible = false;
        }
    }
}