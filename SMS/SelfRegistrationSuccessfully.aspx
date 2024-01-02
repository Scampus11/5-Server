<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelfRegistrationSuccessfully.aspx.cs" Inherits="SMS.SelfRegistrationSuccessfully" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="material-kit-master/assets/css/material-kit.css" rel="stylesheet" />
    <link rel="stylesheet" type="text/css" href="https://fonts.googleapis.com/css?family=Roboto:300,400,500,700|Roboto+Slab:400,700|Material+Icons" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/latest/css/font-awesome.min.css"/>
</head>
<body>
    <form id="form1" runat="server">
        <div class="main main-raised">
            <div class="section section-basic">
                <div class="container">
    <span><h2>You are successfully registered!!!</h2><br /><br />
        Your registration number is :<b><asp:Label ID="lblId" runat="server"></asp:Label></b>   <a href="SMS_Visitor_SelfRegistration.aspx" class="btn green-meadow"  >Back</a>  <a href="Student_Master.aspx" class="btn green-meadow" style="display:none"  >Home</a>
    </span>
                    </div>
                </div>
            </div>
    </form>
</body>
    <!--   Core JS Files   -->
<script src="material-kit-master//core/jquery.min.js" type="text/javascript"></script>
<script src="material-kit-master//core/popper.min.js" type="text/javascript"></script>
<script src="material-kit-master//core/bootstrap-material-design.min.js" type="text/javascript"></script>
<script src="material-kit-master//plugins/moment.min.js"></script>
<!--	Plugin for the Datepicker, full documentation here: https://github.com/Eonasdan/bootstrap-datetimepicker -->
<script src="material-kit-master//plugins/bootstrap-datetimepicker.js" type="text/javascript"></script>
<!--  Plugin for the Sliders, full documentation here: http://refreshless.com/nouislider/ -->
<script src="material-kit-master//plugins/nouislider.min.js" type="text/javascript"></script>
<!--  Google Maps Plugin  -->
<script type="text/javascript" src="https://maps.googleapis.com/maps/api/js?key=YOUR_KEY_HERE"></script>
<!-- Place this tag in your head or just before your close body tag. -->
<script async defer src="https://buttons.github.io/buttons.js"></script>
<!-- Control Center for Material Kit: parallax effects, scripts for the example pages etc -->
<script src="material-kit-master//material-kit.js?v=2.0.5" type="text/javascript"></script>
</html>
