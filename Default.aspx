<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        Mayın sayısı: <asp:TextBox ID="msayisitxt" Text="50" runat="server"></asp:TextBox><br />
        Boyut: <asp:TextBox ID="ktxt" Text="22" runat="server"></asp:TextBox><br />
        <asp:Button ID="olustur" runat="server" Text="Oluştur" OnClick="olustur_Click" />
        <asp:CheckBox ID="gstr" Text="Mayınları Göster" OnCheckedChanged="gstr_CheckedChanged" AutoPostBack="true" runat="server" />
    <div><br /><br />
    <asp:Panel ID="pnl" runat="server">
        

    </asp:Panel>
    </div>
    </form>
</body>
</html>
