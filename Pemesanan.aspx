<%@ Page  Language="C#" AutoEventWireup="true" CodeBehind="Pemesanan.aspx.cs" Inherits="TRY1.Pemesanan" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.2.0-beta1/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-0evHe/X+R7YkIZDRvuzKMRqM+OrBnVFBL6DOitfPri4tjfHxaWutUpFmBp4vmVor" crossorigin="anonymous" />
<body>
    <nav class="navbar navbar-expand-lg navbar-light bg-light">
      <div class="container-fluid">
        <a class="navbar-brand" href="#">HOME</a>
        <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
          <span class="navbar-toggler-icon"></span>
        </button>
        <div class="collapse navbar-collapse" id="navbarSupportedContent">
          <ul class="navbar-nav me-auto mb-2 mb-lg-0">
            <li class="nav-item">
              <a class="nav-link active" aria-current="page" href="pemesanan.aspx">Pemesanan</a>
            </li>
            <li class="nav-item">
              <a class="nav-link active" aria-current="page" href="pelanggan.aspx">Pelanggan</a>
            </li>
            <li class="nav-item dropdown">
              <a class="nav-link dropdown-toggle" id="navbarDropdown" role="button" data-bs-toggle="dropdown" aria-expanded="false">
                Ukuran
              </a>
              <ul class="dropdown-menu" aria-labelledby="navbarDropdown">
                <li><a class="dropdown-item" href="baju.aspx">Baju</a></li>
                <li><a class="dropdown-item" href="celana.aspx">Celana</a></li>
                <li><a class="dropdown-item" href="rok.aspx">Rok</a></li>
              </ul>
            </li>
          </ul>
        </div>
      </div>
    </nav>
    <form id="form2" runat="server">
        <div style="padding: 50px;">
            <asp:Panel runat="server" ID="panelUser">
                <h3 style="text-align: left;">Daftar Data Pemesanan
                </h3>
                <div style="padding: 10px; max-width: 500px; text-align: right;">
                    <asp:LinkButton runat="server" ID="lbTambah" OnClick="lbTambah_Click">Tambah Data</asp:LinkButton>
                </div>
                <div style="clear: right;"></div>
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="500px"
                    DataKeyNames="idp, nama,idb, idc, idr, status_pemesanan,tgl_pesan, tgl_ambil" OnRowCommand="GridView1_RowCommand" CellPadding="3" ForeColor="Black" GridLines="Vertical" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px">
                    <AlternatingRowStyle BackColor="#CCCCCC" />
                    <Columns>
                        <asp:TemplateField HeaderText="No">
                            <ItemTemplate>
                                <asp:Label ID="tbnomor" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Nama">
                            <ItemTemplate>
                                <asp:Label ID="tbnama" runat="server" Text='<%# Bind("nama") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ID Baju">
                            <ItemTemplate>
                                <asp:Label ID="tbidb" runat="server" Text='<%# Bind("idb") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ID Celana">
                            <ItemTemplate>
                                <asp:Label ID="tbidc" runat="server" Text='<%# Bind("idc") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ID Rok">
                            <ItemTemplate>
                                <asp:Label ID="tbidr" runat="server" Text='<%# Bind("idr") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Status Pemesanan">
                            <ItemTemplate>
                                <asp:Label ID="tbstatus_pemesanan" runat="server" Text='<%# Bind("status_pemesanan") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Tanggan Pesan">
                            <ItemTemplate>
                                <asp:Label ID="tbtgl_pesan" runat="server" Text='<%# Bind("tgl_pesan") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Tanggal Ambil">
                            <ItemTemplate>
                                <asp:Label ID="tbtgl_ambil" runat="server" Text='<%# Bind("tgl_ambil") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                                <asp:LinkButton runat="server" ID="lbEdit" CommandName="ubah" CommandArgument="<%# Container.DataItemIndex %>">Edit</asp:LinkButton>
                                
                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>

                    <FooterStyle BackColor="#CCCCCC" />
                    <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                    <SortedAscendingHeaderStyle BackColor="#808080" />
                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                    <SortedDescendingHeaderStyle BackColor="#383838" />

                </asp:GridView>

                <br />
            </asp:Panel>

            <asp:Panel runat="server" ID="panelPengguna">
                <h3 style="text-align: left;">Daftar Pelanggan
                </h3>
                <div style="clear: right;"></div>
                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" Width="500px"
                    DataKeyNames="id,nama,no_telp,alamat" OnRowCommand="GridView1_RowCommand" CellPadding="3" ForeColor="Black" GridLines="Vertical" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px">
                    <AlternatingRowStyle BackColor="#CCCCCC" />
                    <Columns>
                        <asp:TemplateField HeaderText="No">
                            <ItemTemplate>
                                <asp:Label ID="tbnomor" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Id">
                            <ItemTemplate>
                                <asp:Label ID="tbid" runat="server" Text='<%# Bind("id") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Nama">
                            <ItemTemplate>
                                <asp:Label ID="tbnama" runat="server" Text='<%# Bind("nama") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="No telp">
                            <ItemTemplate>
                                <asp:Label ID="tbno_telp" runat="server" Text='<%# Bind("no_telp") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Alamat">
                            <ItemTemplate>
                                <asp:Label ID="tbalamat" runat="server" Text='<%# Bind("alamat") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>

                    <FooterStyle BackColor="#CCCCCC" />
                    <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                    <SortedAscendingHeaderStyle BackColor="#808080" />
                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                    <SortedDescendingHeaderStyle BackColor="#383838" />

                </asp:GridView>

                <br />
            </asp:Panel>
            <hr />


            <asp:Panel runat="server" ID="panelForm" Visible="false">
                <h3>Form Pemesanan
                </h3>
                <table>
                    <tr>
                        <td>ID Pelanggan</td>
                        <td>
                            <asp:TextBox runat="server" ID="tbid" Text=""></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>ID Baju</td>
                        <td>
                            <asp:TextBox runat="server" ID="tbidb" Text=""></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>ID Celana</td>
                        <td>
                            <asp:TextBox runat="server" ID="tbidc" Text=""></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>ID Rok</td>
                        <td>
                            <asp:TextBox runat="server" ID="tbidr" Text=""></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Status Pemesanan</td>
                        <td>
                            <asp:TextBox runat="server" ID="tbstatus_pemesanan" Text=""></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Tanggal Pesan</td>
                        <td>
                            <asp:TextBox runat="server" ID="tbtgl_pesan" Text=""></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Tanggal Ambil</td>
                        <td>
                            <asp:TextBox runat="server" ID="tbtgl_ambil" Text=""></asp:TextBox>
                        </td>
                    </tr>
                    </table>
                <br />
                <asp:Button runat="server" ID="btSimpan" Text="Simpan" Visible="true" OnClick="btSimpan_Click" />
                <asp:Button runat="server" ID="btUpdate" Text="Update" Visible="false" OnClick="btUpdate_Click" />
                <asp:Button runat="server" ID="btBatal" Text="Batal" Visible="true" OnClick="btBatal_Click" />
            </asp:Panel>
        </div>
    </form>
</body>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.2.0-beta1/dist/js/bootstrap.bundle.min.js" integrity="sha384-pprn3073KE6tl6bjs2QrFaJGz5/SUsLqktiwsUTF55Jfv3qYSDhgCecCxMW52nD2" crossorigin="anonymous"></script>
</html>
