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
    public partial class Pemesanan : System.Web.UI.Page
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
                    cmd.CommandText = "SELECT idp, nama,idb, idc, idr, status_pemesanan,tgl_pesan, tgl_ambil from pemesanan inner join pelanggan on pemesanan.id = pelanggan.id order by idp";
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
            string id = GridView1.DataKeys[rowIndex]["idp"].ToString();
           
            if (e.CommandName == "hapus")
            {
                hapusUser(id);
            }
            else if (e.CommandName == "ubah")
            {
                tbidb.Text = GridView1.DataKeys[rowIndex]["idb"].ToString();
                tbidc.Text = GridView1.DataKeys[rowIndex]["idc"].ToString();
                tbidr.Text = GridView1.DataKeys[rowIndex]["idr"].ToString();
                tbstatus_pemesanan.Text = GridView1.DataKeys[rowIndex]["status_pemesanan"].ToString();
                tbtgl_pesan.Text = GridView1.DataKeys[rowIndex]["tgl_pesan"].ToString();
                tbtgl_ambil.Text = GridView1.DataKeys[rowIndex]["tgl_ambil"].ToString();

                ViewState["idp"] = id;
                btSimpan.Visible = false;
                btUpdate.Visible = true;
                panelUser.Visible = false;
                panelForm.Visible = true;
                panelPengguna.Visible = false;
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
                    string query = "delete from pemesanan where idp =" + id;

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
                    if (string.IsNullOrEmpty(tbidr.Text))
                    {
                        cmd.CommandText = "insert into pemesanan (id, idb, idc, status_pemesanan, tgl_ambil, tgl_pesan) values(@id, @idb, @idc, @status_pemesanan, @tgl_pesan, @tgl_ambil)";
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add(new NpgsqlParameter("@id", Convert.ToInt32(tbid.Text)));
                        cmd.Parameters.Add(new NpgsqlParameter("@idb", Convert.ToInt32(tbidb.Text)));
                        cmd.Parameters.Add(new NpgsqlParameter("@idc", Convert.ToInt32(tbidc.Text)));
                        cmd.Parameters.Add(new NpgsqlParameter("@status_pemesanan", tbstatus_pemesanan.Text));
                        cmd.Parameters.Add(new NpgsqlParameter("@tgl_pesan", tbtgl_pesan.Text));
                        cmd.Parameters.Add(new NpgsqlParameter("@tgl_ambil", tbtgl_ambil.Text));
                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                        connection.Close();
                        tbid.Text = "";
                        tbidb.Text = "";
                        tbidc.Text = "";
                        tbidr.Text = "";
                        tbstatus_pemesanan.Text = "";
                        tbtgl_pesan.Text = "";
                        tbtgl_ambil.Text = "";
                    }
                    else if (string.IsNullOrEmpty(tbidc.Text))
                    {
                        cmd.CommandText = "insert into pemesanan (id, idb, idr, status_pemesanan, tgl_ambil, tgl_pesan) values(@id, @idb, @idr, @status_pemesanan, @tgl_pesan, @tgl_ambil)";
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add(new NpgsqlParameter("@id", Convert.ToInt32(tbid.Text)));
                        cmd.Parameters.Add(new NpgsqlParameter("@idb", Convert.ToInt32(tbidb.Text)));
                        cmd.Parameters.Add(new NpgsqlParameter("@idr", Convert.ToInt32(tbidr.Text)));
                        cmd.Parameters.Add(new NpgsqlParameter("@status_pemesanan", tbstatus_pemesanan.Text));
                        cmd.Parameters.Add(new NpgsqlParameter("@tgl_pesan", tbtgl_pesan.Text));
                        cmd.Parameters.Add(new NpgsqlParameter("@tgl_ambil", tbtgl_ambil.Text));
                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                        connection.Close();
                        tbid.Text = "";
                        tbidb.Text = "";
                        tbidc.Text = "";
                        tbidr.Text = "";
                        tbstatus_pemesanan.Text = "";
                        tbtgl_pesan.Text = "";
                        tbtgl_ambil.Text = "";
                    }
                    else if (string.IsNullOrEmpty(tbidb.Text))
                    {
                        cmd.CommandText = "insert into pemesanan (id, idc, idr, status_pemesanan, tgl_ambil, tgl_pesan) values(@id, @idc, @idr, @status_pemesanan, @tgl_pesan, @tgl_ambil)";
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add(new NpgsqlParameter("@id", Convert.ToInt32(tbid.Text)));
                        cmd.Parameters.Add(new NpgsqlParameter("@idc", Convert.ToInt32(tbidc.Text)));
                        cmd.Parameters.Add(new NpgsqlParameter("@idr", Convert.ToInt32(tbidr.Text)));
                        cmd.Parameters.Add(new NpgsqlParameter("@status_pemesanan", tbstatus_pemesanan.Text));
                        cmd.Parameters.Add(new NpgsqlParameter("@tgl_pesan", tbtgl_pesan.Text));
                        cmd.Parameters.Add(new NpgsqlParameter("@tgl_ambil", tbtgl_ambil.Text));
                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                        connection.Close();
                        tbid.Text = "";
                        tbidb.Text = "";
                        tbidc.Text = "";
                        tbidr.Text = "";
                        tbstatus_pemesanan.Text = "";
                        tbtgl_pesan.Text = "";
                        tbtgl_ambil.Text = "";
                    }
                    cmd.CommandText = "insert into pemesanan (id, idb ,idc, idr, status_pemesanan, tgl_ambil, tgl_pesan) values(@id, @idb, @idc, @idr, @status_pemesanan, @tgl_pesan, @tgl_ambil)";
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(new NpgsqlParameter("@id", Convert.ToInt32(tbid.Text)));
                    cmd.Parameters.Add(new NpgsqlParameter("@idb", Convert.ToInt32(tbidb.Text)));
                    cmd.Parameters.Add(new NpgsqlParameter("@idc", Convert.ToInt32(tbidc.Text)));
                    cmd.Parameters.Add(new NpgsqlParameter("@idr", Convert.ToInt32(tbidr.Text)));
                    cmd.Parameters.Add(new NpgsqlParameter("@status_pemesanan", tbstatus_pemesanan.Text));
                    cmd.Parameters.Add(new NpgsqlParameter("@tgl_pesan", tbtgl_pesan.Text));
                    cmd.Parameters.Add(new NpgsqlParameter("@tgl_ambil", tbtgl_ambil.Text));
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    connection.Close();
                    tbid.Text = "";
                    tbidb.Text = "";
                    tbidc.Text = "";
                    tbidr.Text = "";
                    tbstatus_pemesanan.Text = "";
                    tbtgl_pesan.Text = "";
                    tbtgl_ambil.Text = "";
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
                    if (string.IsNullOrEmpty(tbidr.Text))
                    {
                        cmd.CommandText = "update pemesanan set idb=@idb, idc=@idc, status_pemesanan=@status_pemesanan,tgl_pesan=@tgl_pesan, tgl_ambil=@tgl_ambil where idp=" + ViewState["idp"];
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add(new NpgsqlParameter("@idb", Convert.ToInt32(tbidb.Text)));
                        cmd.Parameters.Add(new NpgsqlParameter("@idc", Convert.ToInt32(tbidc.Text)));
                        cmd.Parameters.Add(new NpgsqlParameter("@status_pemesanan", tbstatus_pemesanan.Text));
                        cmd.Parameters.Add(new NpgsqlParameter("@tgl_pesan", tbtgl_pesan.Text));
                        cmd.Parameters.Add(new NpgsqlParameter("@tgl_ambil", tbtgl_ambil.Text));
                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                        connection.Close();
                        tbid.Text = "";
                        tbidb.Text = "";
                        tbidc.Text = "";
                        tbidr.Text = "";
                        tbstatus_pemesanan.Text = "";
                        tbtgl_pesan.Text = "";
                        tbtgl_ambil.Text = "";
                    }
                    else if (string.IsNullOrEmpty(tbidc.Text))
                    {
                        cmd.CommandText = "update pemesanan set idb=@idb, idr=@idr, status_pemesanan=@status_pemesanan,tgl_pesan=@tgl_pesan, tgl_ambil=@tgl_ambil where idp=" + ViewState["idp"];
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add(new NpgsqlParameter("@idb", Convert.ToInt32(tbidb.Text)));
                        cmd.Parameters.Add(new NpgsqlParameter("@idr", Convert.ToInt32(tbidr.Text)));
                        cmd.Parameters.Add(new NpgsqlParameter("@status_pemesanan", tbstatus_pemesanan.Text));
                        cmd.Parameters.Add(new NpgsqlParameter("@tgl_pesan", tbtgl_pesan.Text));
                        cmd.Parameters.Add(new NpgsqlParameter("@tgl_ambil", tbtgl_ambil.Text));
                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                        connection.Close();
                        tbid.Text = "";
                        tbidb.Text = "";
                        tbidc.Text = "";
                        tbidr.Text = "";
                        tbstatus_pemesanan.Text = "";
                        tbtgl_pesan.Text = "";
                        tbtgl_ambil.Text = "";
                    }
                    else if (string.IsNullOrEmpty(tbidb.Text))
                    {
                        cmd.CommandText = "update pemesanan set idc=@idc, idr=@idr, status_pemesanan=@status_pemesanan,tgl_pesan=@tgl_pesan, tgl_ambil=@tgl_ambil where idp=" + ViewState["idp"];
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add(new NpgsqlParameter("@idc", Convert.ToInt32(tbidc.Text)));
                        cmd.Parameters.Add(new NpgsqlParameter("@idr", Convert.ToInt32(tbidr.Text)));
                        cmd.Parameters.Add(new NpgsqlParameter("@status_pemesanan", tbstatus_pemesanan.Text));
                        cmd.Parameters.Add(new NpgsqlParameter("@tgl_pesan", tbtgl_pesan.Text));
                        cmd.Parameters.Add(new NpgsqlParameter("@tgl_ambil", tbtgl_ambil.Text));
                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                        connection.Close();
                        tbid.Text = "";
                        tbidb.Text = "";
                        tbidc.Text = "";
                        tbidr.Text = "";
                        tbstatus_pemesanan.Text = "";
                        tbtgl_pesan.Text = "";
                        tbtgl_ambil.Text = "";
                    }

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
            tbid.Text = "";
            tbidb.Text = "";
            tbidc.Text = "";
            tbidr.Text = "";
            tbstatus_pemesanan.Text = "";
            tbtgl_pesan.Text = "";
            tbtgl_ambil.Text = "";
            panelUser.Visible = true;
            panelPengguna.Visible = false;
            panelForm.Visible = false;
        }
    }
}