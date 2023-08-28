<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Bitacora.aspx.cs" Inherits="_3DBag.Bitacora" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Label ID="lblTitulo" SkinID="Bitacora" runat="server">Bitacora</asp:Label>
    <br />
    <br />
    <div id="divFiltro" style="display: flex; justify-content: left; align-items: center;">

        <asp:Label ID="lblDesde" SkinID="Desde" runat="server">Desde</asp:Label>
        <asp:TextBox ID="txtDesde" runat="server"></asp:TextBox>
        <asp:ImageButton id="imageButtonDesde" runat="server" ImageUrl="~/Images/calendar.png" ImageAlign="AbsBottom" Height="22px" Width="27px" OnClick="imageButtonDesde_Click"/>
        <asp:Calendar ID="Calendar1" runat="server" BackColor="#999999" BorderColor="#3366CC" BorderWidth="1px" CellPadding="1" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="#003399" Height="200px" Width="220px" Visible="False" OnSelectionChanged="Calendar1_SelectionChanged">
            <DayHeaderStyle BackColor="#99CCCC" ForeColor="#336666" Height="1px" />
            <NextPrevStyle Font-Size="8pt" ForeColor="#CCCCFF" />
            <OtherMonthDayStyle ForeColor="#999999" />
            <SelectedDayStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
            <SelectorStyle BackColor="#99CCCC" ForeColor="#336666" />
            <TitleStyle BackColor="#003399" BorderColor="#3366CC" BorderWidth="1px" Font-Bold="True" Font-Size="10pt" ForeColor="#CCCCFF" Height="25px" />
            <TodayDayStyle BackColor="#99CCCC" ForeColor="White" />
            <WeekendDayStyle BackColor="#CCCCFF" />
        </asp:Calendar>
        <br />
        <asp:Label ID="lblHasta" SkinID="Hasta" runat="server">Hasta</asp:Label>
        <asp:TextBox ID="txtHasta" runat="server"></asp:TextBox>
        <asp:ImageButton id="imageButtonHasta" runat="server" ImageUrl="~/Images/calendar.png" ImageAlign="AbsBottom" Height="22px" Width="27px" OnClick="imageButtonHasta_Click"/>
        <asp:Calendar ID="Calendar2" runat="server" BackColor="#999999" BorderColor="#3366CC" BorderWidth="1px" CellPadding="1" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="#003399" Height="200px" Width="220px" Visible="False" OnSelectionChanged="Calendar2_SelectionChanged">
            <DayHeaderStyle BackColor="#99CCCC" ForeColor="#336666" Height="1px" />
            <NextPrevStyle Font-Size="8pt" ForeColor="#CCCCFF" />
            <OtherMonthDayStyle ForeColor="#999999" />
            <SelectedDayStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
            <SelectorStyle BackColor="#99CCCC" ForeColor="#336666" />
            <TitleStyle BackColor="#003399" BorderColor="#3366CC" BorderWidth="1px" Font-Bold="True" Font-Size="10pt" ForeColor="#CCCCFF" Height="25px" />
            <TodayDayStyle BackColor="#99CCCC" ForeColor="White" />
            <WeekendDayStyle BackColor="#CCCCFF" />
        </asp:Calendar>
        <br />
    </div>
    <asp:GridView ID="GridBitacora" PageSize="15" runat="server" AllowPaging="true" AutoGenerateColumns="False" Width="100%" CssClass="table table-bordered table-condensed table-responsive table-hover" OnDataBound="GridBitacora_DataBound" OnPageIndexChanging="GridBitacora_PageIndexChanging">
        <AlternatingRowStyle BackColor="White" />
        <HeaderStyle BackColor="#6B696B" Font-Bold="true" Font-Size="Larger" ForeColor="White" />
        <RowStyle BackColor="#f5f5f5" />
        <SelectedRowStyle BackColor="#669999" Font-Bold="true" ForeColor="White" />
        <Columns>
            <asp:BoundField DataField="NickUs" HeaderText="NickUs" />
            <asp:BoundField DataField="Descripcion" HeaderText="Descripcion" />
            <asp:BoundField DataField="Fecha" HeaderText="Fecha" />
            <asp:BoundField DataField="Criticidad" HeaderText="Criticidad" />
        </Columns>
        <PagerTemplate>
            <table>
                <tr>
                    <td>
                        <asp:LinkButton ID="LinkAnterior" runat="server" Text="Anterior" CommandName="Page" CommandArgument="Prev" />
                        <asp:LinkButton ID="LinkSiguiente" runat="server" Text="Siguiente" CommandName="Page" CommandArgument="Next" />
                    </td>
                    <td>Pagina N°
                        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                        Total Pag.
                        <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
                    </td>
                </tr>
            </table>
        </PagerTemplate>
        <PagerSettings Mode="NextPrevious" Position="Bottom" />
    </asp:GridView>
</asp:Content>
