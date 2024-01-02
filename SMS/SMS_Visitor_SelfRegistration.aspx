<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SMS_Visitor_SelfRegistration.aspx.cs" Inherits="SMS.SMS_Visitor_SelfRegistration" %>

<%@ Register TagPrefix="uc" Namespace="ASPNET_Captcha" Assembly="ASPNET_Captcha" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="css/Master.css" />
    <script type="text/javascript" src="../webcamjs/webcam.min.js"></script>
    <link href="../css/Master.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery-1.4.2.min.js" type="text/javascript"></script>
    <script src="Scripts/jquery.webcam.js" type="text/javascript"></script>
    <link href="material-kit-master/assets/css/material-kit.css" rel="stylesheet" />
    <link href="material-kit-master/assets/css/css.css" rel="stylesheet" />
    <link href="material-kit-master/assets/css/font-awesome.min.css" rel="stylesheet" />
    <style>
        #my_camera {
            width: 640px;
            height: 480px;
        }
        video {
            border: 2px solid red;
        }
    </style>
    <script language="javascript" type="text/javascript">
        function clearText(field) {
            if (field.defaultValue == field.value) field.value = '';
            else if (field.value == '') field.value = field.defaultValue;
        }
    </script>
</head>
<body>

    <form id="form1" runat="server">
        <div class="main main-raised">
            <div class="section section-basic">
                <asp:ScriptManager ID="scriptmanager1" runat="server" EnableScriptGlobalization="true" AsyncPostBackTimeout="1000">
                </asp:ScriptManager>
                <asp:UpdatePanel ID="UpdateRefresh" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                        <div class="container">
                            <div class="card" style="width: 40rem;">
                                <div class="card-body">
                                    <h4 class="card-title">Visitor Self Registration <a style="margin-left: 286px; display: none;" class="btn green-meadow " data-toggle="modal" href="Student_Master.aspx">Home
                            <i class="fa fa-plus"></i></a></h4>
                                </div>
                            </div>
                            <div class="form-group">
                                <asp:TextBox ID="txtFirst_Name" runat="server" class="form-control" placeholder="First Name"></asp:TextBox>
                                <span id="SpanFirstName" runat="server" style="display: none; color: red;">*** Enter First Name</span>
                            </div>
                            <div class="form-group">
                                <asp:TextBox ID="txtLast_Name" runat="server" class="form-control" placeholder="Last Name"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <asp:TextBox ID="txtCompany" runat="server" class="form-control" placeholder="Company Name"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label for="exampleInputPassword1">Visitor Type</label>
                                <asp:DropDownList ID="ddlVisitor_Type" runat="server" class="form-control">
                                </asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <label for="exampleInputPassword1">Visit Reason</label>
                                <asp:DropDownList ID="ddlVisit_Reason" runat="server" class="form-control">
                                </asp:DropDownList>
                            </div>
                            <div class="form-row">
                                <div class="form-group col-md-6">
                                    <label for="exampleInputPassword1">Country Code</label>
                                    <asp:DropDownList ID="ddlCountry" runat="server" class="form-control">
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group col-md-6">
                                    <label for="exampleInputPassword1">Phone Number</label>
                                    <asp:TextBox ID="txtPhone_Number" runat="server" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <asp:TextBox ID="txtEmail_Id" runat="server" class="form-control" placeholder="Email Id"></asp:TextBox>
                                <span id="error" style="display: none; color: red;">*** Wrong email</span>
                                <span id="SpanEmail" runat="server" style="display: none; color: red;">*** Enter email</span>
                            </div>
                            <div class="form-group">
                                <asp:TextBox ID="txtNational_ID" runat="server" class="form-control" placeholder="National ID"></asp:TextBox>
                            </div>
                            <div class="form-row">
                                <div class="form-group col-md-4" style="max-width: 291px;" id="divimages" runat="server">
                                    <label for="exampleInputPassword1">Visitor Photo</label><br />
                                    <p style="background-color: darkslategrey; border-radius: 5%;">
                                        <asp:HiddenField ID="hidValue" Value="" runat="server" />

                                        <a id="DivUpload" runat="server" class="nav-link" data-toggle="modal" onclick="configure()">
                                            <asp:Image ID="imgStaff" Class="img-circle" runat="server" Height="150px" Width="200px" Style="border-radius: 15%" />
                                            <i class="material-icons icon-image-preview" style="color: white; font-size: 35px; margin-left: 10px;" title="upload visitor photo">camera_alt</i>
                                        </a>
                                    </p>
                                </div>
                            </div>

                            <br />
                            <div class="form-group" style="display: none;">
                                <label for="exampleInputPassword1">Access Level</label>
                                <asp:DropDownList ID="ddlAccess_Level" runat="server" class="form-control">
                                </asp:DropDownList>
                            </div>
                            <div class="form-row">
                                <div class="form-group col-md-6">
                                    <label for="exampleInputPassword1">Available Services</label>
                                    <asp:ListBox ID="lstEmp" runat="server" CssClass="form-control" SelectionMode="Multiple" Height="100px"></asp:ListBox>
                                    <asp:Button ID="btnRight" Text=">>" runat="server" OnClick="RightClick" />
                                </div>
                                <div class="form-group col-md-6">
                                    <label for="exampleInputPassword1">Assigned Services</label>
                                    <asp:ListBox ID="lstmoveemp" runat="server" CssClass="form-control" SelectionMode="Multiple" Height="100px"></asp:ListBox>
                                    <asp:Button ID="btnLeft" Text="<<" runat="server" OnClick="LeftClick" />
                                    <span id="spnservices" runat="server" style="display: none; color: red;">*** Please select atleast one service</span>
                                </div>
                            </div>
                            <asp:Button ID="lnksave" runat="server" Text="Save" class="btn btn-primary" OnClick="btnsave_Click" data-toggle="modal" data-target="#divModel" />
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnRight" />
                        <asp:AsyncPostBackTrigger ControlID="btnLeft" />
                        <asp:AsyncPostBackTrigger ControlID="DivUpload" />
                    </Triggers>
                </asp:UpdatePanel>
                <div class="modal " id="UploadImage" tabindex="-1" role="UploadImage" aria-hidden="true">
                    <div class="modal-dialog" style="margin-top: -5px!important; max-width: 700px">
                        <div class="modal-content">
                            <div class="modal-header" style="display: none;">
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true"></button>
                                <h4 class="modal-title" style="margin-right: 183px">Select Upload File</h4>
                                <asp:Button runat="server" Style="margin-right: -172px" ID="btnclose1" class="btn btn-default" Text="Close" OnClick="btnclose_Click" />
                            </div>
                            <div class="modal-header" style="display: none;">
                                <asp:FileUpload ID="FileUpload1" runat="server" />
                                OR
                            </div>
                            <div class="modal-body" style="display: none;">

                                <div class="form-group">
                                    <div>
                                        <asp:Button ID="btncapture" runat="server" class="btn btn-primary" OnClick="btncapture_Click" Text="Capture Image"></asp:Button>
                                    </div>
                                    <div class="col-md-6">
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <b style="color: red">NOTE :</b> choose any one option
                                </div>
                            </div>
                            <div class="PhotoUploadWrapper">
                                <div class="PhotoUpoloadCoseBtn">
                                </div>
                                <div class="PhotoUploadContent">
                                    <div>

                                        <a id="aback" style="float: left; font-family: Verdana, Geneva, sans-serif; font-size: 14px; line-height: 35px; text-indent: 18px; font-weight: bold; color: white">Close</a>
                                    </div>
                                    <div id="divpreviw">
                                        <div class="PhotoUpoloadRightHeader">
                                            <p style="float: left; font-family: Verdana, Geneva, sans-serif; font-size: 14px; line-height: 35px; text-indent: 18px; font-weight: bold; color: #FFF;">
                                                Camera Preview 
                                            </p>

                                        </div>

                                        <div class="PhotoUpoloadLeftMainCont" style="text-align: center;">
                                            <div>
                                                <div style="padding: 0px 0px 0px 0px; text-align: -webkit-center;">
                                                    <div id="my_camera"></div>
                                                </div>
                                            </div>
                                            <br />
                                            <div style="text-align: center;">
                                                <a onclick="take_snapshot()"><i class="material-icons icon-image-preview" style="color: black; font-size: 60px;" title="Capture Photo">camera</i>
                                                </a>

                                            </div>
                                        </div>
                                    </div>
                                    <div id="divCaptureImages">
                                        <div class="PhotoUpoloadLeftHeader">
                                            <p style="float: left; font-family: Verdana, Geneva, sans-serif; font-size: 14px; line-height: 35px; text-indent: 18px; font-weight: bold; color: #FFF;">
                                                Capture Photo
                                            </p>

                                        </div>

                                        <div>
                                            <div style="padding: 0px 0px 0px 0px; text-align: -webkit-center;">
                                                <div id="results"></div>
                                                <%-- <canvas id="canvas" width="640" height="480"></canvas>--%>
                                            </div>
                                        </div>
                                        <br />
                                        <div style="text-align: center;">
                                            <a id="apreview"><i class="material-icons icon-image-preview" style="color: black; font-size: 60px;" title="Preview Photo">camera_alt</i>
                                            </a>
                                            <a onclick="Save()">
                                                <i class="material-icons icon-image-preview" style="color: black; font-size: 60px;" title="Save">save</i>
                                            </a>

                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                </div>

            </div>
        </div>
    </form>
</body>

<script type="text/javascript">
    $("#DivUpload").click(function () {

        $("#divpreviw").show();
        $("#divCaptureImages").hide();
        $("#UploadImage").show();
    });
    $("#btnclose").click(function () {
        $("#UploadImage").hide();
    });
    $("#FileUpload1").click(function () {
        $("#btnclose").hide();
    });
    $("#btncapture").click(function () {
        $("#UploadImage").hide();
        $("#Captureimages").show();
    });
    $("#aback").click(function () {
        Webcam.reset();
        $("#UploadImage").hide();
    });
    $("#apreview").click(function () {
        configure();
        $("#divpreviw").show();
        $("#divCaptureImages").hide();

        $("#UploadImage").show();
    });

</script>
<script type="text/javascript">
    $(document).ready(function () {

        $("divCaptureImages").hide();
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $("#DivUpload").click(function () {

                $("#divpreviw").show();
                $("#divCaptureImages").hide();

                $("#UploadImage").show();
            });
            $("#btnclose").click(function () {
                $("#UploadImage").hide();
            });
            $("#FileUpload1").click(function () {
                $("#btnclose").hide();
            });
            $("#btncapture").click(function () {
                $("#UploadImage").hide();
                $("#Captureimages").show();
            });
            $("#aback").click(function () {
                Webcam.reset();
                $("#UploadImage").hide();
            });
            $("#apreview").click(function () {
                configure();
                $("#divpreviw").show();
                $("#divCaptureImages").hide();

                $("#UploadImage").show();
            });

            var hidden = document.getElementById('hidValue');
            $('#imgStaff').attr("src", hidden.value);
        });
    });
</script>
<!-- Configure a few settings and attach camera -->
<script lang="JavaScript">
    function configure() {
        Webcam.set({
            width: 640,
            height: 480,
            image_format: 'jpeg',
            jpeg_quality: 90
        });
        Webcam.attach('#my_camera');


    }
    // preload shutter audio clip
    var shutter = new Audio();
    shutter.autoplay = false;
    shutter.src = navigator.userAgent.match(/Firefox/) ? 'shutter.ogg' : 'shutter.mp3';
    //<!-- Code to handle taking the snapshot and displaying it locally -->
    function take_snapshot() {
        // play sound effect
        shutter.play();
        // take snapshot and get image data
        Webcam.snap(function (data_uri) {
            // display results in page
            var hidden = document.getElementById('hidValue');
            hidden.value = data_uri;
            document.getElementById('results').innerHTML =
                '<img style="border: 2px solid greenyellow;" src="' + data_uri + '"/>';
            Webcam.reset();
            $("#divpreviw").hide();
            $("#divCaptureImages").show();
        });
    }
    function Save() {
        var hidden = document.getElementById('hidValue');
        $('#imgStaff').attr("src", hidden.value);
        $("#UploadImage").hide();
    }
</script>
</html>

