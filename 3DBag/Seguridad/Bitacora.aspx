<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Bitacora.aspx.cs" Inherits="_3DBag.Bitacora" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:GridView ID="GridBitacora" PageSize="10" runat="server" AllowPaging="true" AutoGenerateColumns="False" Width="100%" Height="154px" OnDataBound="GridBitacora_DataBound" OnPageIndexChanging="GridBitacora_PageIndexChanging" >
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
                   <td>
                       Pagina N° <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                       Total Pag. <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
                   </td>
               </tr>
           </table>
        </PagerTemplate>
        <PagerSettings Mode="NextPrevious" Position="Bottom" />
    </asp:GridView>    
</asp:Content>
