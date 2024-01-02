<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LicenseValidate.aspx.cs" Inherits="SMS.LicenseValidate" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8" />
    <title>License</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta content="width=device-width, initial-scale=1" name="viewport" />
    <meta content="" name="description" />
    <meta content="" name="author" />
    <link rel="shortcut icon" href="../favicon.ico" />
    <link rel="shortcut icon" type="image/png" href="../assets/images/logo/icon.png" />
    <link href="http://fonts.googleapis.com/css?family=Open+Sans:400,300,600,700&amp;subset=all" rel="stylesheet" type="text/css" />
    <link href="../CSS/font-awesome.min.css" rel="stylesheet" />
    <link href="../CSS/font-awesome.min.css" rel="stylesheet" />
    <link href="../CSS/simple-line-icons.min.css" rel="stylesheet" />
    <link href="../CSS/bootstrap.min.css" rel="stylesheet" />
    <link href="../CSS/uniform.default.css" rel="stylesheet" />
    <link href="../CSS/bootstrap-switch.min.css" rel="stylesheet" />
    <%--<link href="../pagenotfound.html" rel="stylesheet" type="text/css" />--%>
    <link href="../pagenotfound.html" rel="stylesheet" type="text/css" />
    <link href="../CSS/components-rounded.min.css" rel="stylesheet" />
    <link href="../CSS/plugins.min.css" rel="stylesheet" />
    <link href="../assets/backend/pages/css/login-4.min.css" rel="stylesheet" type="text/css" />
    <%--<script>
        $(document).ready(function () {
            var windowHeight = $(window).height();
            UIService.SaveWindowHeight(windowHeight, OnCompleted);
        });

        function OnCompleted() { }
    </script>--%>
</head>

<body class="login" style="background-image: url(../assets/backend/img/office.jpg)">
    <br />
    <div class="content" style="background-color: black; width: 550px !important">
        <!-- BEGIN LOGIN FORM -->
        <form class="login-form" runat="server" defaultbutton="btnSubmit">
            <input type="hidden" name="_token" value="aYusLVRZWfuDYEcA7QvlS14LozbYFC64554Ci7dt" />
            <asp:Panel runat="server" ID="pnlMain">
                <h3 class="form-title">Add License Key</h3>
                <asp:Label ID="lblLicenseMessage" runat="server" ForeColor="Orange" Text="* Invalid license key." Visible="false"></asp:Label>
                <div style="margin-top: 5px;"></div>
                <div class="form-group" style="display: none">
                    <!--ie8, ie9 does not support html5 placeholder, so we just show field title for that-->
                    <label class="control-label visible-ie8 visible-ie9">Customer Id</label>
                    <div class="input-icon">
                        <i class="fa fa-id-card-o"></i>
                        <asp:TextBox ID="txtCustomerId" Style="background-color: lightgray !important" ReadOnly="true" runat="server" class="form-control placeholder-no-fix" placeholder="Customer Id"></asp:TextBox>
                        <%--<asp:Label ID="lblCustomerId" runat="server" ForeColor="Red" Text="* Enter Contact No" Visible="false"></asp:Label>--%>
                    </div>
                </div>
                <div class="form-group">
                    <!--ie8, ie9 does not support html5 placeholder, so we just show field title for that-->
                    <label class="control-label visible-ie8 visible-ie9">License Key</label>
                    <div class="input-icon">
                        <i class="fa fa-key"></i>
                        <asp:TextBox ID="txtLicenseKey" runat="server" class="form-control placeholder-no-fix" placeholder="License Key"></asp:TextBox>
                        <asp:Label ID="lblerrorusername" runat="server" ForeColor="Orange" Text="* Enter License Key" Visible="false"></asp:Label>
                    </div>
                </div>
                <div class="form-group">
                    <label class="control-label visible-ie8 visible-ie9">MAC Address ("," Separated if multiple.)</label>
                    <div class="input-icon">
                        <i class="fa fa-at "></i>
                        <asp:TextBox ID="txtMACAddress" runat="server" class="form-control placeholder-no-fix" placeholder="MAC Address (',' Separated if multiple.)"></asp:TextBox>
                        <asp:Label ID="lblerrorpassword" runat="server" ForeColor="Orange" Text="* Enter MAC Address " Visible="false"></asp:Label>
                    </div>
                </div>
                <div class="form-actions">
                    <%--<label class="control-label">
                    <input type="checkbox" name="remember"/>
                    Remember me
           
                </label>--%>
                    <asp:Button ID="btnSubmit" runat="server" Style="margin: 5px" class="btn green-dark pull-right" Text="Submit" OnClick="btnSubmit_Click" />
                    <asp:Button ID="btnBack" runat="server" Style="margin: 5px" class="btn grey pull-right" Text="Back to Login" OnClick="btnBack_Click" />
                    <asp:Button ID="btnLicenseKey" runat="server" Style="margin: 5px" class="btn green-dark pull-left" Text="Register as New License" OnClick="btnLicenseKey_Click" />
                    <br />
                </div>
            </asp:Panel>
            <asp:Panel ID="pnlMessage" runat="server" Visible="true">
                <br />
                <br />
                <asp:Label ID="lblfinalMessage" Font-Size="Large" runat="server" ForeColor="Orange" Text="Thank you for registering with us, your license subscribed with life time."></asp:Label>
                <br />
                <br />
                <asp:Button ID="btnBacktologin" runat="server" Style="margin: 5px" class="btn grey pull-left" Text="Back to Login" OnClick="btnBack_Click" />
                <asp:Button ID="btnDashboard" runat="server" Style="margin: 5px" class="btn  green-dark pull-right" Text="Go to Dashboard" OnClick="btnDashboard_Click" />                
                <br />
                <br />
                <br />
            </asp:Panel>
        </form>

    </div>

    <%--<div class="copyright" style="color: black">2021@copyright All rights reserved</div>--%>
</body>
<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
<script src="../License/jquery.samask-masker.js"></script>
<script>
    $(document).ready(function () {
        var rBtnVal = $('#txtMACAddress').val();
        if (rBtnVal == "") {
            $("#txtMACAddress").attr("readonly", false);
        }
        else {
            $("#txtMACAddress").attr("readonly", true);
        }

        $(function () {
            $.samaskHtml();
            $('#txtLicenseKey').samask("FFFF-FFFF-FFFF-FFFF");
        });
    });
</script>

</html>
