<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GridPaging.aspx.cs" Inherits="SMS.GridPaging" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        .circle {
    width: 100px;
    height: 70px;
    background: LightPink;
    -moz-border-radius: 160px;
    -webkit-border-radius: 160px;
    border-radius: 160px;
}
        .gridview{
    background-color:#fff;
   
   padding:2px;
   margin:2% auto;
   
   
}
        .gridview a{
  margin:auto 1%;
    border-radius:50%;
      background-color:#444;
      padding:5px 10px 5px 10px;
      color:#fff;
      text-decoration:none;
      -o-box-shadow:1px 1px 1px #111;
      -moz-box-shadow:1px 1px 1px #111;
      -webkit-box-shadow:1px 1px 1px #111;
      box-shadow:1px 1px 1px #111;
     
}

        .gridview a:hover{
    background-color:#1e8d12;
    color:#fff;
}
        .gridview span{
    background-color:#ae2676;
    color:#fff;
     -o-box-shadow:1px 1px 1px #111;
      -moz-box-shadow:1px 1px 1px #111;
      -webkit-box-shadow:1px 1px 1px #111;
      box-shadow:1px 1px 1px #111;

    border-radius:50%;
    padding:5px 10px 5px 10px;
}
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:GridView ID="GridView1" AutoGenerateColumns="true" runat="server">  
</asp:GridView>  
            <br />
<asp:Repeater ID="rptPager" runat="server" >
<ItemTemplate>
    <asp:LinkButton ID="lnkPage" runat="server" Text = '<%#Eval("Text") %>' CommandArgument = '<%# Eval("Value") %>' Enabled = '<%# Eval("Enabled") %>' OnClick = "lnkPage_Click" cssclass="gridview"></asp:LinkButton>
</ItemTemplate>
</asp:Repeater>
        </div>
    </form>
</body>
</html>
