<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Page_Not_Access.aspx.cs" Inherits="SMS.Page_Not_Access" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="text-align:center">
            <div style="text-align: center;">
                <h5><b style="color: red">You have no access to this page. Please login again.</b></h5>
            </div>
            <div style="text-align: center;">
                <asp:LinkButton ID="lnkGo" style="font-size: x-large;" runat="server" Text="Login again" OnClick="lnkGo_Click" class="btn green-meadow" />
            </div>
            <img src="assets/backend/img/lock.jpeg" />
        </div>
    </form>
</body>
</html>
