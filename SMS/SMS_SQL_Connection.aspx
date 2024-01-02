<%@ Page  Language="C#"  AutoEventWireup="true" CodeBehind="SMS_SQL_Connection.aspx.cs" Inherits="SMS.SMS_SQL_Connection" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta content="width=device-width, initial-scale=1" name="viewport" />
    <meta content="" name="description" />
    <meta content="" name="author" />
    <meta name="csrf-token" content="ax8ZN3lRSoa15FC1BBhhPnP5xckz7rpEbL25Olx3" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

    <script type="text/javascript" src="/Scripts/jquery-1.9.1.min.js"></script>
    <script type="text/javascript" src="/Scripts/moment.min.js"></script>
    <script type="text/javascript" src="/Scripts/bootstrap.min.js"></script>
    <script type="text/javascript" src="/Scripts/bootstrap-datetimepicker.js"></script>
    <link rel="stylesheet" href="/Content/bootstrap-datetimepicker.css" />
    <link rel="stylesheet" href="/Content/bootstrap.css" />
    <link rel="shortcut icon" href="favicon.ico" />
    <link rel="shortcut icon" type="image/png" href="Images/LOGO.jpeg" />
    <link href="CSS/font-awesome.css" rel="stylesheet" />
    <link href="CSS/font-awesome.min.css" rel="stylesheet" />
    <link href="CSS/simple-line-icons.min.css" rel="stylesheet" />
    <link href="CSS/bootstrap.min.css" rel="stylesheet" />
    <%--<link href="CSS/uniform.default.css" rel="stylesheet" />--%>
    <link href="CSS/bootstrap-switch.min.css" rel="stylesheet" />
    <%--<link href="CSS/daterangepicker-bs3.css" rel="stylesheet" />--%>
    <link href="CSS/morris.css" rel="stylesheet" />
    <link href="CSS/fullcalendar.min.css" rel="stylesheet" />
    <link href="CSS/jqvmap.css" rel="stylesheet" />
    <link href="CSS/components-rounded.min.css" rel="stylesheet" />
    <link href="CSS/plugins.min.css" rel="stylesheet" />
    <link href="CSS/layout.min.css" rel="stylesheet" />
    <link href="CSS/darkblue.min.css" rel="stylesheet" />
    <link href="CSS/custom.min.css" rel="stylesheet" />
    <link href="CSS/bootstrap-multiselect.css" rel="stylesheet" />
    <link href="CSS/p-loading.min.css" rel="stylesheet" />
    <link href="CSS/sweetalert.css" rel="stylesheet" />
    <link href="CSS/style.css" rel="stylesheet" />

    <%--<link href="CSS/daterangepicker-bs3.css" rel="stylesheet" />--%>
    <%--<link href="CSS/bootstrap-datepicker3.min.css" rel="stylesheet" />--%>
    <link href="CSS/bootstrap-timepicker.min.css" rel="stylesheet" />
    <%--<link href="CSS/bootstrap-datetimepicker.min.css" rel="stylesheet" />--%>
    <link href="CSS/clockface.css" rel="stylesheet" />
    <link href="CSS/ajax.css" rel="stylesheet" />
    <link href="CSS/layout.min.css" rel="stylesheet" />
    <link href="CSS/darkblue.min.css" rel="stylesheet" />
    <link href="CSS/custom.min.css" rel="stylesheet" />


    <style>
        .circle {
            width: 100px;
            height: 70px;
            background: Lightgrey;
            -moz-border-radius: 160px;
            -webkit-border-radius: 160px;
            border-radius: 160px;
        }

        .gridview {
            background-color: #fff;
            padding: 2px;
            margin: 2% auto;
        }

            .gridview a {
                margin: auto 1%;
                border-radius: 50%;
                background-color: #444;
                padding: 5px 10px 5px 10px;
                color: #fff;
                text-decoration: none;
                -o-box-shadow: 1px 1px 1px #111;
                -moz-box-shadow: 1px 1px 1px #111;
                -webkit-box-shadow: 1px 1px 1px #111;
                box-shadow: 1px 1px 1px #111;
            }

                .gridview a:hover {
                    background-color: #1e8d12;
                    color: #fff;
                }

            .gridview span {
                background-color: #ae2676;
                color: #fff;
                -o-box-shadow: 1px 1px 1px #111;
                -moz-box-shadow: 1px 1px 1px #111;
                -webkit-box-shadow: 1px 1px 1px #111;
                box-shadow: 1px 1px 1px #111;
                border-radius: 50%;
                padding: 5px 10px 5px 10px;
            }
    </style>
    
    <style type="text/css">
        .modal {
            position: fixed;
            top: 0;
            left: 0;
            background-color: black;
            z-index: 99;
            opacity: 0.8;
            filter: alpha(opacity=80);
            -moz-opacity: 0.8;
            min-height: 100%;
            width: 100%;
        }

        .loading {
            font-family: Arial;
            font-size: 10pt;
            border: 5px solid #67CFF5;
            width: 200px;
            height: 100px;
            display: none;
            position: fixed;
            background-color: White;
            z-index: 999;
        }
    </style>


</head>


<body runat="server" id="body">
    <form id="form1" runat="server">
     <asp:ScriptManager ID="scriptmanager1" runat="server">
        </asp:ScriptManager>
            <asp:UpdatePanel ID="UpdateRefresh" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
    <br />
    <br />
    <br />
    <div class="page-content-wrapper" style="margin-bottom: -16px;">
        <div class="page-content">
            <div class="modal-content" id="divView" runat="server">
                <div class="modal-body" style="background-color:lightgray;">
                    <div class="portlet-body form" >
                        <div class="tab-content" >
                            <div class="tab-pane container active" id="divPersonalDetails" runat="server">
                                <div class="row ">
                                    <h2>Database Configuration</h2>
                                    <hr />
                                    
                                    <div class="col-md-12 col-sm-12">
                                        <div class="portlet-body">
                                            <div class="form-body">
                                                <div class="form-group">
                                                    <label class="col-md-2 control-label">
                                                        SQL Server Name :
                                                    </label>

                                                    <div class="col-md-6">

                                                        <asp:TextBox ID="txtServerName" runat="server" class="form-control placeholder-no-fix" placeholder="Server Name"></asp:TextBox>
                                                      
                                                        <asp:DropDownList ID="ddlServerName" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlServerName_SelectedIndexChanged" AutoPostBack="true" Visible="false"></asp:DropDownList>
                                                        <asp:Label ID="lblerrorServername" runat="server" ForeColor="Red" Text="* Enter Server Name " Visible="false"></asp:Label>
                                                    </div>
                                                    <div class="col-md-3">
                                                          <asp:LinkButton ID="lnkrefresh" runat="server"   OnClick="lnkrefresh_Click" CssClass="fa fa-refresh" Style="font-size:larger;line-height:30px;"></asp:LinkButton>
                                                        <asp:Label ID="lblServerNameprocess" runat="server" ForeColor="Red" Text="* please wait..... Server Name In process " Visible="false"></asp:Label>
                                                        </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    
                                    <div class="clearfix"></div>

                                    <div class="col-md-12 col-sm-12">
                                        <div class="portlet-body">
                                            <div class="form-body">
                                                <div class="form-group">
                                                    <label class="col-md-2 control-label">Authentication : </label>
                                                    <div class="col-md-6">
                                                        <asp:DropDownList ID="ddlAuthentication" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlAuthentication_SelectedIndexChanged">
                                                            <asp:ListItem Value="1">Windows Authentication</asp:ListItem>
                                                            <asp:ListItem Value="2">SQL Server Authentication</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="clearfix"></div>
                                    <div class="col-md-12 col-sm-12" id="divUser" runat="server" visible="false">
                                        <div class="portlet-body">
                                            <div class="form-body">
                                                <div class="form-group">
                                                    <label class="col-md-2 control-label">
                                                        User Name :
                                                    </label>

                                                    <div class="col-md-6">
                                                        <asp:TextBox ID="txtusername" runat="server" class="form-control placeholder-no-fix" placeholder="User name"></asp:TextBox>
                                                        <asp:Label ID="lblerrorusername" runat="server" ForeColor="Red" Text="* Enter username " Visible="false"></asp:Label>
                                                    </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-12 col-sm-12" id="divPassword" runat="server" visible="false">
                                <div class="portlet-body">
                                    <div class="form-body">
                                        <div class="form-group">
                                            <label class="col-md-2 control-label">
                                                PassWord :
                                            </label>

                                            <div class="col-md-6">
                                                <asp:TextBox ID="txtPassword" runat="server" class="form-control placeholder-no-fix" placeholder="Password" ></asp:TextBox>
                                                <asp:Label ID="lblerrorpassword" runat="server" ForeColor="Red" Text="* Enter password " Visible="false"></asp:Label>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="clearfix"></div>
                                    <div class="col-md-12 col-sm-12">
                                        <div class="portlet-body">
                                            <div class="form-body">
                                                <div class="form-group">
                                                    <label class="col-md-2 control-label">DataBase Name : </label>
                                                    <div class="col-md-6">
                                                        <asp:TextBox ID="txtDBNAme" runat="server" class="form-control placeholder-no-fix" placeholder="DB Name"></asp:TextBox>
                                                        <asp:DropDownList ID="ddlDBName" runat="server" CssClass="form-control" Visible="false"></asp:DropDownList>

                                                        <asp:Label ID="lblerrordbname" runat="server" ForeColor="Red" Text="* Enter DB Name " Visible="false"></asp:Label>
                                                    </div>
                                                    <div class="col-md-3">
                                                          <asp:LinkButton ID="lnkDBname" runat="server"   OnClick="lnkDBname_Click" CssClass="fa fa-refresh" Style="font-size:larger;line-height:30px;"></asp:LinkButton>
                                                        <asp:Label ID="lblDBProcess" runat="server" ForeColor="Red" Text="* please wait..... Database Name In process " Visible="false"></asp:Label>
                                                        </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                        </div>
                    </div>
                </div>
                <br />
                        <asp:Label ID="lblsql" runat="server" ForeColor="Red" Visible="false" Style="margin-left:240px;" ></asp:Label>
                                    <asp:Label ID="lbltestconnection" runat="server" ForeColor="Red" Visible="false" Text="Connection Successfully!!!" Style="margin-left:240px;"></asp:Label>
                <br />
                        <asp:Button ID="BtntestConnection" runat="server" class="btn default" Text="Test Connection" OnClick="BtntestConnection_Click" Style="margin-left:240px;" />
                <asp:Button ID="btnSubmit" runat="server" class="btn default" Text="Save" OnClick="btnSave_Click"  />
                         
            </div>
        </div>
    </div>
    </div>
    </div>
            </ContentTemplate>
        <Triggers>
          <%--  <asp:PostBackTrigger ControlID="lnksave" />
            <asp:PostBackTrigger ControlID="lnkupdate" />--%>
        </Triggers>
    </asp:UpdatePanel>
    

        </form>
    </body>
    </html>