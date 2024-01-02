<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConvertByteTOimage.aspx.cs" Inherits="SMS.ConvertByteTOimage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        Insert  Byte to Image Format  For Student: 
    <asp:Button ID="btnAdd" runat="server" OnClick="btnConvert_Click" Text="Insert Student" />
    </div>
        <br /><br />
         <div>
        Insert  Byte to Image Format  For Employee/Staff: 
    <asp:Button ID="BtnEmployee" runat="server" OnClick="BtnEmployee_Click" Text="Insert Employee" />
    </div>
    </form>
</body>
</html>
