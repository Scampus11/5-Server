<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm2.aspx.cs" Inherits="SMS.WebForm2" %>


<!DOCTYPE html>
<html>
    <head>
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <link type="text/css" href="../CSS/bootstrap.min.css" rel="stylesheet" />
        <%--<link type="text/css" href="css/bootstrap.min.css" />--%>
        <link type="text/css" href="../CSS/bootstrap-timepicker.min.css" rel="stylesheet"/>
        <%--<link type="text/css" href="css/bootstrap-timepicker.min.css" />--%>
        <script type="text/javascript" src="../JS/jquery.min.js"></script>
        <%--<script type="text/javascript" src="js/jquery.min.js"></script>--%>
        <script type="text/javascript" src="../JS/bootstrap.min.js"></script>
        <%--<script type="text/javascript" src="js/bootstrap.min.js"></script>--%>
        <script type="text/javascript" src="../JS/bootstrap-timepicker.min.js"></script>
        <%--<script type="text/javascript" src="js/bootstrap-timepicker.min.js"></script>--%>
    </head>
    <body>
        <div class="input-group bootstrap-timepicker timepicker">
            <input id="timepicker1" type="text" class="form-control input-small">
            <span class="input-group-addon"><i class="glyphicon glyphicon-time"></i></span>
        </div>
 
        <script type="text/javascript">
            $('#timepicker1').timepicker();
        </script>
    </body>
</html>