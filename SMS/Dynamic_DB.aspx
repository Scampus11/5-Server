<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dynamic_DB.aspx.cs" Inherits="SMS.Dynamic_DB" %>

<!DOCTYPE html>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8" />
    <title> Login</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
    <meta content="width=device-width, initial-scale=1" name="viewport" />
    <meta content="" name="description" />
    <meta content="" name="author" />
    <link rel="shortcut icon" href="favicon.ico" />
    <link rel="shortcut icon" type="image/png" href="assets/images/logo/icon.png" />
    <link href="http://fonts.googleapis.com/css?family=Open+Sans:400,300,600,700&amp;subset=all" rel="stylesheet" type="text/css" />
    <link href="CSS/font-awesome.min.css" rel="stylesheet" />
    <link href="CSS/font-awesome.min.css" rel="stylesheet" />
    <link href="CSS/simple-line-icons.min.css" rel="stylesheet" />
    <link href="CSS/bootstrap.min.css" rel="stylesheet" />
    <link href="CSS/uniform.default.css" rel="stylesheet" />
    <link href="CSS/bootstrap-switch.min.css" rel="stylesheet" />
    <link href="pagenotfound.html" rel="stylesheet" type="text/css" />
    <link href="pagenotfound.html" rel="stylesheet" type="text/css" />
    <link href="CSS/components-rounded.min.css" rel="stylesheet" />
    <link href="CSS/plugins.min.css" rel="stylesheet" />
    <link href="assets/backend/pages/css/login-4.min.css" rel="stylesheet" type="text/css" />
</head>

<body class="login" style="background-image: url(assets/backend/img/office.jpg)">
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />

    <div class="content" style="background-color: black">
        <!-- BEGIN LOGIN FORM -->
        <form class="login-form" runat="server" >
            <input type="hidden" name="_token" value="aYusLVRZWfuDYEcA7QvlS14LozbYFC64554Ci7dt" />
            <h3 class="form-title"> SQL Authentication </h3>   
            
            <asp:Label ID="lblerror" runat="server" ForeColor="Red" Text="* Invalid username and password" Visible="false"></asp:Label>
            <asp:Label ID="Label1" runat="server" ForeColor="Red" Text="* Sorry!!! You do not have access to this portal,kindly contact to administrator." Visible="false"></asp:Label><br />
            <asp:Label ID="Label2" runat="server" ForeColor="Red" Text="* Enter username and password" Visible="false"></asp:Label>
            <div class="alert alert-danger display-hide">
                <button class="close" data-close="alert"></button>
                <span>Enter Your Email and password. </span>
            </div>
            <div class="form-group">
                <!--ie8, ie9 does not support html5 placeholder, so we just show field title for that-->
                <label class="control-label visible-ie8 visible-ie9">Server Name</label>
                <div class="input-icon">
                    <i class="fa fa-user"></i>
                    
                    <asp:TextBox ID="txtServerName" runat="server" class="form-control placeholder-no-fix" placeholder="Server Name"></asp:TextBox>
                    <asp:Label ID="lblerrorServername" runat="server" ForeColor="Red" Text="* Enter Server Name " Visible="false"></asp:Label>
                </div>
            </div>
            <div class="form-group">
                <!--ie8, ie9 does not support html5 placeholder, so we just show field title for that-->
                <label class="control-label visible-ie8 visible-ie9">DB Name</label>
                <div class="input-icon">
                    <i class="fa fa-user"></i>
                    
                    <asp:TextBox ID="txtDBNAme" runat="server" class="form-control placeholder-no-fix" placeholder="DB Name"></asp:TextBox>
                    <asp:Label ID="lblerrordbname" runat="server" ForeColor="Red" Text="* Enter DB Name " Visible="false"></asp:Label>
                </div>
            </div>
            <div class="form-group">
                <!--ie8, ie9 does not support html5 placeholder, so we just show field title for that-->
                <label class="control-label visible-ie8 visible-ie9">User Name</label>
                <div class="input-icon">
                    <i class="fa fa-user"></i>
                    
                    <asp:TextBox ID="txtusername" runat="server" class="form-control placeholder-no-fix" placeholder="User name"></asp:TextBox>
                    <asp:Label ID="lblerrorusername" runat="server" ForeColor="Red" Text="* Enter username " Visible="false"></asp:Label>
                </div>
            </div>
            <div class="form-group">
                <label class="control-label visible-ie8 visible-ie9">Password</label>
                <div class="input-icon">
                    <i class="fa fa-lock"></i>
                    <asp:TextBox ID="txtPassword" runat="server" class="form-control placeholder-no-fix" placeholder="Password" TextMode="Password"></asp:TextBox>
                    <asp:Label ID="lblerrorpassword" runat="server" ForeColor="Red" Text="* Enter password " Visible="false"></asp:Label>
                </div>
            </div>
            <div class="form-actions">
                <%--<label class="control-label">
                    <input type="checkbox" name="remember"/>
                    Remember me
           
                </label>--%>
                <asp:Button ID="btnSubmit" runat="server" class="btn green-dark pull-right" Text="Submit" OnClick="btnSave_Click" />
                <br />
                

            </div>

        </form>

    </div>

    <div class="copyright" style="color: black">2020@copyright All rights reserved</div>

</body>

</html>

