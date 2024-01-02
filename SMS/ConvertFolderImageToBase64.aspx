<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConvertFolderImageToBase64.aspx.cs" Inherits="SMS.ConvertFolderImageToBase64" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8" />
    <title>Convert Image To Base64</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
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
    <script>
        $(document).ready(function () {
            var windowHeight = $(window).height();
            UIService.SaveWindowHeight(windowHeight, OnCompleted);
        });

        function OnCompleted() { }
    </script>
</head>

<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="scriptmanager1" runat="server" EnableScriptGlobalization="true" AsyncPostBackTimeout="1000">
        </asp:ScriptManager>
        <asp:UpdateProgress ID="upprogess" runat="server">
            <ProgressTemplate>
                <div style="left: 50%; bottom: 50%; position: absolute;">
                    <img id="loader1_imgwaiticon" src="../images/loader.gif" alt="" class="imgstl">
                    <asp:Label runat="server" ID="lblupdatetext" Text="loading please wait....."></asp:Label>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div style="text-align: center;">
                    <br />
                    <br />
                    <br />
                    <br />
                    <span>
                        <h3>Click here for convert image folder to base64</h3>
                    </span>
                    <asp:Button ID="btnAdd" CssClass="btn green-meadow" Text="Student" runat="server" OnClick="btnAdd_Click" />
                    <asp:Button ID="btnStaff" CssClass="btn green-meadow" Text="Staff" runat="server" OnClick="btnStaff_Click" />
                </div>
                <div runat="server" id="divpopup" class="modal">
                    <div class="modal-content" runat="server" style="top: 100px; left: 320px; width: 700px; height: 200px;">
                        <div class="modal-body">
                            <div class="portlet-body form">
                                <div class="form-body" runat="server">
                                    <div class="form-group" style="text-align: center;" runat="server" id="divAdvanceSearch">
                                        <h5><b style="color: green">
                                            <asp:Label ID="lblmsg" runat="server"></asp:Label></b></h5>
                                    </div>
                                    <br />
                                    <div style="text-align: center;">
                                        <asp:LinkButton ID="lnkGo" runat="server" Text="ok" OnClick="lnkGo_Click" class="btn green-meadow" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnAdd" />
                <asp:AsyncPostBackTrigger ControlID="btnStaff" />
            </Triggers>
        </asp:UpdatePanel>
    </form>
</body>
</html>
