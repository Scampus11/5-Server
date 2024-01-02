<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Visitors_Master.aspx.cs" Inherits="SMS.Visitors_Master" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8" />
    <title>Visitors List
    </title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta content="width=device-width, initial-scale=1" name="viewport" />
    <meta content="" name="description" />
    <meta content="" name="author" />
    <meta name="csrf-token" content="ax8ZN3lRSoa15FC1BBhhPnP5xckz7rpEbL25Olx3">
    <link rel="shortcut icon" href="favicon.ico" />
    <link rel="shortcut icon" type="image/png" href="Images/LOGO.jpeg" />
    <!-- BEGIN GLOBAL MANDATORY STYLES -->
    <link href="http://preview.thesoftking.com/thesoftking/catering/assets/backend/font-awesome.css" rel="stylesheet" type="text/css" />
    <link href="http://preview.thesoftking.com/thesoftking/catering/assets/backend/global/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="http://preview.thesoftking.com/thesoftking/catering/assets/backend/global/plugins/simple-line-icons/simple-line-icons.min.css" rel="stylesheet" type="text/css" />
    <link href="http://preview.thesoftking.com/thesoftking/catering/assets/backend/global/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="http://preview.thesoftking.com/thesoftking/catering/assets/backend/global/plugins/uniform/css/uniform.default.css" rel="stylesheet" type="text/css" />
    <link href="http://preview.thesoftking.com/thesoftking/catering/assets/backend/global/plugins/bootstrap-switch/css/bootstrap-switch.min.css" rel="stylesheet" type="text/css" />
    <!-- END GLOBAL MANDATORY STYLES -->

    <!-- BEGIN PAGE LEVEL PLUGINS -->
    <link href="http://preview.thesoftking.com/thesoftking/catering/assets/backend/global/plugins/bootstrap-daterangepicker/daterangepicker-bs3.css" rel="stylesheet" type="text/css" />
    <link href="http://preview.thesoftking.com/thesoftking/catering/assets/backend/global/plugins/morris/morris.css" rel="stylesheet" type="text/css" />
    <link href="http://preview.thesoftking.com/thesoftking/catering/assets/backend/global/plugins/fullcalendar/fullcalendar.min.css" rel="stylesheet" type="text/css" />
    <link href="http://preview.thesoftking.com/thesoftking/catering/assets/backend/global/plugins/jqvmap/jqvmap/jqvmap.css" rel="stylesheet" type="text/css" />
    <!-- END PAGE LEVEL PLUGINS -->

    <!-- BEGIN THEME GLOBAL STYLES -->
    <link href="http://preview.thesoftking.com/thesoftking/catering/assets/backend/css/components-rounded.min.css" rel="stylesheet" id="style_components" type="text/css" />
    <link href="http://preview.thesoftking.com/thesoftking/catering/assets/backend/css/plugins.min.css" rel="stylesheet" type="text/css" />
    <!-- END THEME GLOBAL STYLES -->
    <!-- BEGIN THEME LAYOUT STYLES -->
    <link href="http://preview.thesoftking.com/thesoftking/catering/assets/backend/css/layout.min.css" rel="stylesheet" type="text/css" />
    <link href="http://preview.thesoftking.com/thesoftking/catering/assets/backend/css/darkblue.min.css" rel="stylesheet" type="text/css" id="style_color" />
    <link href="http://preview.thesoftking.com/thesoftking/catering/assets/backend/css/custom.min.css" rel="stylesheet" type="text/css" />
    <!-- END THEME LAYOUT STYLES -->



    <link href="http://preview.thesoftking.com/thesoftking/catering/assets/backend/custom/css/bootstrap-multiselect.css" rel="stylesheet" type="text/css" />
    <link href="http://preview.thesoftking.com/thesoftking/catering/assets/backend/custom/css/p-loading.min.css" rel="stylesheet" type="text/css" />
    <link href="http://preview.thesoftking.com/thesoftking/catering/assets/backend/custom/css/sweetalert.css" rel="stylesheet" type="text/css" />
    <link href="http://preview.thesoftking.com/thesoftking/catering/assets/backend/custom/css/style.css" rel="stylesheet" type="text/css" />
    <link href="http://preview.thesoftking.com/thesoftking/catering/assets/backend/custom/css/style.css" rel="stylesheet" type="text/css" />

    <script src="http://preview.thesoftking.com/thesoftking/catering/assets/backend/global/plugins/jquery.min.js" type="text/javascript"></script>

    <!-- BEGIN PAGE LEVEL PLUGINS -->

    <link href="http://preview.thesoftking.com/thesoftking/catering/assets/backend/global/plugins/bootstrap-daterangepicker/daterangepicker-bs3.css" rel="stylesheet" type="text/css" />
    <link href="http://preview.thesoftking.com/thesoftking/catering/assets/backend/global/plugins/bootstrap-datepicker/css/bootstrap-datepicker3.min.css" rel="stylesheet" type="text/css" />
    <link href="http://preview.thesoftking.com/thesoftking/catering/assets/backend/global/plugins/bootstrap-timepicker/css/bootstrap-timepicker.min.css" rel="stylesheet" type="text/css" />
    <link href="http://preview.thesoftking.com/thesoftking/catering/assets/backend/global/plugins/bootstrap-datetimepicker/css/bootstrap-datetimepicker.min.css" rel="stylesheet" type="text/css" />
    <link href="http://preview.thesoftking.com/thesoftking/catering/assets/backend/global/plugins/clockface/css/clockface.css" rel="stylesheet" type="text/css" />
    <!-- END PAGE LEVEL PLUGINS -->

    <link rel="stylesheet" href="http://preview.thesoftking.com/thesoftking/catering/assets/backend/ajax.css">

    <link href="http://preview.thesoftking.com/thesoftking/catering/assets/backend/layouts/layout/css/layout.min.css" rel="stylesheet" type="text/css" />
    <link href="http://preview.thesoftking.com/thesoftking/catering/assets/backend/layouts/layout/css/themes/darkblue.min.css" rel="stylesheet" type="text/css" id="Link1" />
    <link href="http://preview.thesoftking.com/thesoftking/catering/assets/backend/layouts/layout/css/custom.min.css" rel="stylesheet" type="text/css" />
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
    <!-- END HEAD -->
</head>

<body class="page-header-fixed page-sidebar-closed-hide-logo page-content-white">
    <form id="form1" runat="server">
        <div class="page-header navbar navbar-fixed-top">

            <div class="page-header-inner ">

                <div class="page-logo">

                    <img src="Images/LOGO.jpeg" style="height: 30px" alt="logo" class="logo-default" />
                    </a>
            <div class="menu-toggler sidebar-toggler"></div>
                </div>
                <a href="javascript:;" class="menu-toggler responsive-toggler" data-toggle="collapse" data-target=".navbar-collapse"></a>

                <div class="top-menu">
                    <ul class="nav navbar-nav pull-right">

                        <li class="dropdown dropdown-user">
                            <a href="javascript:;" class="dropdown-toggle" data-toggle="dropdown" data-hover="dropdown" data-close-others="true">

                                <span class="username username-hide-on-mobile">admin
                                </span>
                                <i class="fa fa-angle-down"></i>
                            </a>
                            <ul class="dropdown-menu dropdown-menu-default">
                                <li>
                                    <a href="#changePassword" data-toggle="modal">
                                        <i class="icon-settings"></i>Change Password
                                    </a>
                                </li>
                                <li>
                                    <asp:LinkButton ID="lnklogout" runat="server" OnClick="lnklogout_Click">Log Out</asp:LinkButton>
                                </li>
                            </ul>
                        </li>

                    </ul>
                </div>

            </div>

        </div>

        <div class="clearfix"></div>

        <div class="page-container">

            <div class="page-sidebar-wrapper">

                <div class="page-sidebar navbar-collapse collapse">

                    <div class="page-sidebar navbar-collapse collapse">

                        <ul class="page-sidebar-menu  page-header-fixed " data-keep-expanded="false" data-auto-scroll="true" data-slide-speed="200" style="padding-top: 20px">
                            <li class="sidebar-toggler-wrapper hide">

                                <div class="sidebar-toggler"></div>

                            </li>
                            <li class="sidebar-search-wrapper"></li>
                            <br />
                            <br />
                            <li class="nav-item start active1">
                                <a href="Employee_Master.aspx" class="nav-link nav-toggle">
                                    <i class="icon-briefcase"></i>
                                    <span class="title">Student Management</span>
                                    <span class="selected"></span>
                                </a>
                            </li>

                            <li class="nav-item start active1">
                                <a href="Officer_Master.aspx" class="nav-link nav-toggle">
                                    <i class="icon-briefcase"></i>
                                    <span class="title">Officer Master</span>
                                    <span class="selected"></span>
                                </a>
                            </li>
                            <li class="nav-item start active1">
                                <a href="Reader_Master.aspx" class="nav-link nav-toggle">
                                    <i class="icon-briefcase"></i>
                                    <span class="title">Reader Master</span>
                                    <span class="selected"></span>
                                </a>
                            </li>
                            <li class="nav-item start active1">
                                <a href="Door_Group_Master.aspx" class="nav-link nav-toggle">
                                    <i class="icon-briefcase"></i>
                                    <span class="title">Door Group Master</span>
                                    <span class="selected"></span>
                                </a>
                            </li>
                            <li class="nav-item start active1">
                                <a href="Access_Group_Master.aspx" class="nav-link nav-toggle">
                                    <i class="icon-briefcase"></i>
                                    <span class="title">Access Group  Master</span>
                                    <span class="selected"></span>
                                </a>
                            </li>

                             <li class="nav-item start active1">
                <a href="Access_List.aspx" class="nav-link nav-toggle">
                    <i class="icon-briefcase"></i>
                    <span class="title">Student Access List  Master</span>
                    <span class="selected"></span>
                </a>
            </li>
            <li class="nav-item start active1">
                <a href="Staff_Access_List.aspx" class="nav-link nav-toggle">
                    <i class="icon-briefcase"></i>
                    <span class="title">Staff Access List  Master</span>
                    <span class="selected"></span>
                </a>
            </li>
                            <li class="nav-item start active1">
                                <a href="Session_Master.aspx" class="nav-link nav-toggle">
                                    <i class="icon-briefcase"></i>
                                    <span class="title">Session List  Master</span>
                                    <span class="selected"></span>
                                </a>
                            </li>
                            <li class="nav-item start active">
                                <a href="Visitors_Master.aspx" class="nav-link nav-toggle">
                                    <i class="icon-briefcase"></i>
                                    <span class="title">Visitor Master</span>
                                    <span class="selected"></span>
                                </a>
                            </li>
                        </ul>

                    </div>

                </div>
            </div>


            <!-- BEGIN CONTENT -->
            <div class="page-content-wrapper">
                <!-- BEGIN CONTENT BODY -->
                <div class="page-content">
                    <!-- BEGIN PAGE HEADER-->
                    <!-- END PAGE BAR -->
                    <!-- BEGIN PAGE TITLE-->
                    <h3 class="page-title bold">Visitors Master
                <small>Visitors-list </small>
                        <!-- BEGIN EXAMPLE TABLE PORTLET-->
                        <asp:LinkButton ID="lnkAdd" runat="server" Text="Add New Officer" class="btn green-meadow pull-right" OnClick="lnkAdd_Click" Visible="false"></asp:LinkButton>
                        <asp:LinkButton ID="lnkviewgrid" runat="server" Text="View Officer-list" class="btn green-meadow pull-right" OnClick="lnkviewgrid_Click" Visible="false" Style="display: none"></asp:LinkButton>
                        <asp:LinkButton ID="lnkEdit_menu" runat="server" Text="Edit Officer" class="btn green-meadow pull-right" OnClick="lnkEdit_menu_Click" Visible="false" Style="display: none"></asp:LinkButton>
                        <asp:LinkButton ID="lnkView_Menu" runat="server" Text="View Officer" class="btn green-meadow pull-right" OnClick="lnkView_Menu_Click" Visible="false" Style="display: none"></asp:LinkButton>

                        <hr>
                    </h3>
                    <!-- END PAGE TITLE-->
                    <!-- BEGIN PAGE CONTENT-->

                    <div class="row" id="divgrid" runat="server">
                        <div class="col-md-12">

                            <div class="portlet box green-meadow">
                                <div class="portlet-title">
                                    <div class="caption">
                                        <i class="fa fa-briefcase"></i>Visitors List
                                    </div>
                                    <div class="tools">
                                    </div>
                                </div>
                                <div class="portlet-body">
                                     Search:
                                <asp:TextBox ID="txtSearch" runat="server" Width="230px" Height="35px" />&nbsp;&nbsp;&nbsp;
                                <%--<asp:Button ID="btnsearch" Text="Search" runat="server" OnClick="btnsearch_Click" />--%>
                                    <asp:LinkButton ID="btnsearch" class="btn green-meadow" runat="server" Text="Search" OnClick="btnsearch_Click"></asp:LinkButton>
                                <%-- <hr />--%>
                                    <asp:GridView ID="gridEmployee" runat="server" class="table table-striped table-bordered table-hover" AutoGenerateColumns="false" AllowPaging="true" AllowSorting="true" OnSorting="OnSorting" OnPageIndexChanging="OnPageIndexChanging" PageSize="5">
                                        <PagerSettings Mode="NumericFirstLast" PageButtonCount="5" FirstPageText="First" LastPageText="Last"/>
                                        <pagerstyle cssclass="gridview"></pagerstyle>
                                        <Columns>
                                            <asp:TemplateField ItemStyle-Width="30px" HeaderText="SLN_ACS_Visitor_Info" SortExpression="SLN_ACS_Visitor_Info" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSLN_ACS_Visitor_Info" runat="server" Text='<%#Eval("SLN_ACS_Visitor_Info") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField ItemStyle-Width="30px" HeaderText="Visitor_RecId" SortExpression="Visitor_RecId" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblVisitor_RecId" runat="server" Text='<%#Eval("Visitor_RecId") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField ItemStyle-Width="30px" HeaderText="Visitor_GUID" SortExpression="Visitor_GUID" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblVisitor_GUID" runat="server" Text='<%#Eval("Visitor_GUID") %>'></asp:Label>
                                                    <asp:Label ID="lblid" runat="server" Text='<%#Eval("SLN_ACS_Visitor_Info") %>' Visible="false"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField ItemStyle-Width="100px" HeaderText="First_Name" SortExpression="First_Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFull_Name" runat="server" Text='<%#Eval("First_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField ItemStyle-Width="100px" HeaderText="Email Id" SortExpression="Email_Id">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEmail_Id" runat="server" Text='<%#Eval("Email_Id") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="100px" HeaderText="Company" SortExpression="Company">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCompany" runat="server" Text='<%#Eval("Company") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="100px" HeaderText="Visit_Reason" SortExpression="Visit_Reason">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblVisit_Reason" runat="server" Text='<%#Eval("Visit_Reason") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="100px" HeaderText="Check In" SortExpression="Check_In">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCheckin" runat="server" Text='<%#Eval("Check_In") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="100px" HeaderText="Check Out" SortExpression="Check_Out">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCheckout" runat="server" Text='<%#Eval("Check_Out") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                          <asp:TemplateField ItemStyle-Width="100px" HeaderText="Visitor Type" SortExpression="Visitor_Type">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblVisitor_Type" runat="server" Text='<%#Eval("Visitor_Type") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="100px" HeaderText="Access Level" SortExpression="Access_Level">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAccess_Level" runat="server" Text='<%#Eval("Access_Level") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="100px" HeaderText="View">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="linkView" class="btn green-meadow" runat="server" Text="View" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "SLN_ACS_Visitor_Info")%>' OnClick="linkView_Click"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField ItemStyle-Width="100px" HeaderText="Edit">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="linkEdit" class="btn green-meadow" runat="server" Text="Edit" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "SLN_ACS_Visitor_Info")%>' OnClick="linkEdit_Click"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                        </Columns>

                                    </asp:GridView>
                                </div>
                            </div>
                            <!-- END EXAMPLE TABLE PORTLET-->
                        </div>
                    </div>
                    <div class="modal-content" id="divView" runat="server" visible="false">
                        <div class="modal-body">
                            <div class="portlet-body form">
                                <!-- BEGIN FORM-->
                                <div class="form-body" runat="server" visible="false" id="divSLN" >
                                    
                                    <div class="form-group" style="display:none">
                                        <div class="description" style="width: 100%; border: 1px solid #ddd; padding: 10px; border-radius: 5px">
                                            <p class="text-success">
                                        SLN_ACS :
                                    </p>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <asp:TextBox ID="txtSLN_ACS_Visitor_Info" runat="server"></asp:TextBox>
                                                    <asp:Label ID="lblSLN_ACS_Visitor_Info" runat="server" Visible="false"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>


                                    <div class="form-group" style="display:none">
                                        <label class="text-success">Visitor Recored Id : </label>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="description" style="width: 100%; border: 1px solid #ddd; padding: 10px; border-radius: 5px">
                                                    <div class="row">
                                                        <div class="col-md-12" id="Div1">
                                                            <div class="input-group">
                                                                <asp:TextBox ID="txtVisitor_RecId" runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <%-- <hr />--%>

                                    <div class="form-group" style="display:none">
                                        <label class="text-success">Visitor_GUID : </label>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="description" style="width: 100%; border: 1px solid #ddd; padding: 10px; border-radius: 5px">
                                                    <div class="row">
                                                        <div class="col-md-12" id="Div2">
                                                            <div class="input-group">
                                                                <asp:TextBox ID="txtVisitor_GUID" runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label class="text-success">First_Name : </label>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="description" style="width: 100%; border: 1px solid #ddd; padding: 10px; border-radius: 5px">
                                                    <div class="row">
                                                        <div class="col-md-12" id="Div3">
                                                            <div class="input-group">
                                                                <asp:TextBox ID="txtFirst_Name" runat="server" style="width: 714px"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <%-- <hr />--%>

                                    <div class="form-group">
                                        <label class="text-success">Last_Name : </label>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="description" style="width: 100%; border: 1px solid #ddd; padding: 10px; border-radius: 5px">
                                                    <div class="row">
                                                        <div class="col-md-12" id="Div4">
                                                            <div class="input-group">
                                                                <asp:TextBox ID="txtLast_Name" runat="server" style="width: 714px"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                   <%-- <hr />--%>

                                    <div class="form-group">
                                        <label class="text-success">Company Name: </label>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="description" style="width: 100%; border: 1px solid #ddd; padding: 10px; border-radius: 5px">
                                                    <div class="row">
                                                        <div class="col-md-12" id="Div5">
                                                            <div class="input-group">
                                                                <asp:TextBox ID="txtCompany" runat="server" style="width: 714px"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <%-- <hr />--%>
                                    <div class="form-group">
                                        <label class="text-success">Visitor_Type : </label>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="description" style="width: 100%; border: 1px solid #ddd; padding: 10px; border-radius: 5px">
                                                    <div class="row">
                                                        <div class="col-md-12" id="Div6">
                                                            <div class="input-group">
                                                                <%--<asp:TextBox ID="txtVisitor_Type" runat="server"></asp:TextBox>--%>
                                                                <asp:DropDownList ID="ddlVisitor_Type" runat="server" style="width: 714px">
                                                                    <asp:ListItem Value="Visitor" Selected="True" >Visitor</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="text-success">Visit Reason : </label>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="description" style="width: 100%; border: 1px solid #ddd; padding: 10px; border-radius: 5px">
                                                    <div class="row">
                                                        <div class="col-md-12" id="Div17">
                                                            <div class="input-group">
                                                                <%--<asp:TextBox ID="txtVisit_Reason" runat="server"></asp:TextBox>--%>
                                                                <asp:DropDownList ID="ddlVisit_Reason" runat="server" style="width: 714px">
                                                                    <asp:ListItem Value="Sales Call" Selected="True">Sales Call</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <%-- <hr />--%>

                                    
                                    <%-- <hr />--%>

                                    <div class="form-group">
                                        <label class="text-success">Phone_Number : </label>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="description" style="width: 100%; border: 1px solid #ddd; padding: 10px; border-radius: 5px">
                                                    <div class="row">
                                                        <div class="col-md-12" id="Div7">
                                                            <div class="input-group">
                                                                <asp:TextBox ID="txtPhone_Number" runat="server" MaxLength="10" style="width: 714px"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <%-- <hr />--%>
                                    <div class="form-group">
                                        <label class="text-success">Email_Id : </label>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="description" style="width: 100%; border: 1px solid #ddd; padding: 10px; border-radius: 5px">
                                                    <div class="row">
                                                        <div class="col-md-12" id="Div13">
                                                            <div class="input-group">
                                                                <asp:TextBox ID="txtEmail_Id" runat="server" style="width: 714px"></asp:TextBox>
                                                                <span id="error" style="display: none; color: red;">Wrong email</span>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <%-- <hr />--%>
                                    <div class="form-group">
                                        <label class="text-success">National_ID : </label>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="description" style="width: 100%; border: 1px solid #ddd; padding: 10px; border-radius: 5px">
                                                    <div class="row">
                                                        <div class="col-md-12" id="Div10">
                                                            <div class="input-group">
                                                                <asp:TextBox ID="txtNational_ID" runat="server" style="width: 714px"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <%-- <hr />--%>

                                    <div class="form-group">
                                        <label class="text-success">Visitor_Photo : </label>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="description" style="width: 100%; border: 1px solid #ddd; padding: 10px; border-radius: 5px">
                                                    <div class="row">
                                                        <div class="col-md-12" id="Div8">
                                                            <div class="input-group">
                                                                <%--<asp:TextBox ID="txtEmp_Photo" runat="server"></asp:TextBox>--%>
                                                                <asp:Image ID="imgStaff" runat="server" Height="120px" Width="200px" Visible="false" /><br />
                                                                <asp:FileUpload ID="FileUpload1" runat="server"  />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <%-- <hr />--%>

                                    <div class="form-group">
                                        <label class="text-success">ID_Number : </label>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="description" style="width: 100%; border: 1px solid #ddd; padding: 10px; border-radius: 5px">
                                                    <div class="row">
                                                        <div class="col-md-12" id="Div9">
                                                            <div class="input-group">
                                                                <asp:TextBox ID="txtID_Number" runat="server" style="width: 714px"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <%-- <hr />--%>

                                    
                                    <div class="form-group">
                                        <label class="text-success">Host_Employee_Code : </label>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="description" style="width: 100%; border: 1px solid #ddd; padding: 10px; border-radius: 5px">
                                                    <div class="row">
                                                        <div class="col-md-12" id="Div11">
                                                            <div class="input-group">
                                                                <asp:TextBox ID="txtHost_Employee_Code" runat="server" style="width: 714px"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <%-- <hr />--%>

                                    <div class="form-group">
                                        <label class="text-success">Access_Card_Number : </label>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="description" style="width: 100%; border: 1px solid #ddd; padding: 10px; border-radius: 5px">
                                                    <div class="row">
                                                        <div class="col-md-12" id="Div12">
                                                            <div class="input-group">
                                                                <asp:TextBox ID="txtAccess_Card_Number" runat="server" MaxLength="8" style="width: 714px"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <%-- <hr />--%>

                                    

                                    <div class="form-group" style="display:none">
                                        <label class="text-success">Valid_From : </label>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="description" style="width: 100%; border: 1px solid #ddd; padding: 10px; border-radius: 5px">
                                                    <div class="row">
                                                        <div class="col-md-12" id="Div14">
                                                            <div class="input-group">
                                                                <asp:TextBox ID="txtValid_From" runat="server" style="width: 714px"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <%-- <hr />--%>

                                    <div class="form-group" style="display:none">
                                        <label class="text-success">Valid_To : </label>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="description" style="width: 100%; border: 1px solid #ddd; padding: 10px; border-radius: 5px">
                                                    <div class="row">
                                                        <div class="col-md-12" id="Div15">
                                                            <div class="input-group">
                                                                <asp:TextBox ID="txtValid_To" runat="server" style="width: 714px"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <%-- <hr />--%>

                                    <div class="form-group">
                                        <label class="text-success">Access_Level : </label>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="description" style="width: 100%; border: 1px solid #ddd; padding: 10px; border-radius: 5px">
                                                    <div class="row">
                                                        <div class="col-md-12" id="Div16">
                                                            <div class="input-group" >
                                                                <%--<asp:TextBox ID="txtAccess_Level" runat="server"></asp:TextBox>--%>
                                                                <asp:DropDownList ID="ddlAccess_Level" runat="server" style="width: 714px">
                                                                    <asp:ListItem Value="Test Access" >Test Access</asp:ListItem>
                                                                    <asp:ListItem Value="Library" >Library</asp:ListItem>
                                                                    <asp:ListItem Value="Mueseums">Mueseums</asp:ListItem>
                                                                    <asp:ListItem Value="Main Gate">Main Gate</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <%-- <hr />--%>
                                     <div class="form-group" style="display:none">
                                        <label class="text-success">Check_In : </label>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="description" style="width: 100%; border: 1px solid #ddd; padding: 10px; border-radius: 5px">
                                                    <div class="row">
                                                        <div class="col-md-12" id="Div18">
                                                            <div class="input-group">
                                                                <asp:TextBox ID="txtCheck_In" runat="server" Enabled="false"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <%-- <hr />--%>
                                     <div class="form-group" style="display:none">
                                        <label class="text-success">Check_Out : </label>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="description" style="width: 100%; border: 1px solid #ddd; padding: 10px; border-radius: 5px">
                                                    <div class="row">
                                                        <div class="col-md-12" id="Div19">
                                                            <div class="input-group">
                                                                <asp:TextBox ID="txtCheck_Out" runat="server" Enabled="false"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <%-- <hr />--%>
                                     <div class="form-group" style="display:none">
                                        <label class="text-success">CreatedBy : </label>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="description" style="width: 100%; border: 1px solid #ddd; padding: 10px; border-radius: 5px">
                                                    <div class="row">
                                                        <div class="col-md-12" id="Div20">
                                                            <div class="input-group">
                                                                <asp:TextBox ID="txtCreatedBy" runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <%-- <hr />--%>
                                     <div class="form-group" style="display:none">
                                        <label class="text-success">LastUpdatedBy : </label>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="description" style="width: 100%; border: 1px solid #ddd; padding: 10px; border-radius: 5px">
                                                    <div class="row">
                                                        <div class="col-md-12" id="Div21">
                                                            <div class="input-group">
                                                                <asp:TextBox ID="txtLastUpdatedBy" runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <%-- <hr />--%>
                                     <div class="form-group" style="display:none">
                                        <label class="text-success">RecordStatus : </label>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="description" style="width: 100%; border: 1px solid #ddd; padding: 10px; border-radius: 5px">
                                                    <div class="row">
                                                        <div class="col-md-12" id="Div22">
                                                            <div class="input-group">
                                                                <asp:TextBox ID="txtRecordStatus" runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <br />

                                    <asp:LinkButton ID="lnksave" runat="server" Text="Save" OnClick="btnsave_Click" class="btn green-meadow" Visible="false" />
                                    <asp:LinkButton ID="lnkupdate" runat="server" Text="Update" OnClick="btnupdate_Click" class="btn green-meadow" Visible="false" />
                                    <asp:LinkButton ID="lnkBack" runat="server" Text="Back" OnClick="lnkBack_Click" class="btn green-meadow" /> 
                                </div>
                            </div>
                        </div>



                        <!-- END FORM-->
                    </div>
                </div>
                <!-- END EXAMPLE TABLE PORTLET-->
            </div>
            <!-- END PAGE CONTENT-->
    </div>
        <!-- END CONTAINER -->
    </form>
    <!-- BEGIN FOOTER -->
    <div class="page-footer">
        <div class="page-footer-inner">
            2018 &copy; ALL RIGHTS RESERVED
        </div>
        <div class="scroll-to-top">
            <i class="icon-arrow-up"></i>
        </div>
    </div>
    <!-- END FOOTER -->
    <script src="http://preview.thesoftking.com/thesoftking/catering/assets/backend/global/plugins/respond.min.js"></script>
    <script src="http://preview.thesoftking.com/thesoftking/catering/assets/backend/global/plugins/excanvas.min.js"></script>

    <script src="http://preview.thesoftking.com/thesoftking/catering/assets/backend/pages/scripts/table-datatables-colreorder.min.js"></script>
    <script src="http://preview.thesoftking.com/thesoftking/catering/assets/backend/global/scripts/datatable.js" type="text/javascript"></script>
    <script src="http://preview.thesoftking.com/thesoftking/catering/assets/backend/global/plugins/datatables/datatables.min.js" type="text/javascript"></script>
    <script src="http://preview.thesoftking.com/thesoftking/catering/assets/backend/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.js" type="text/javascript"></script>
    <script src="http://preview.thesoftking.com/thesoftking/catering/assets/backend/global/scripts/app.min.js" type="text/javascript"></script>

    <!-- BEGIN CORE PLUGINS -->

    <script src="http://preview.thesoftking.com/thesoftking/catering/assets/backend/global/plugins/bootstrap/js/bootstrap.min.js" type="text/javascript"></script>
    <script src="http://preview.thesoftking.com/thesoftking/catering/assets/backend/global/plugins/js.cookie.min.js" type="text/javascript"></script>
    <script src="http://preview.thesoftking.com/thesoftking/catering/assets/backend/global/plugins/bootstrap-hover-dropdown/bootstrap-hover-dropdown.min.js" type="text/javascript"></script>
    <script src="http://preview.thesoftking.com/thesoftking/catering/assets/backend/global/plugins/jquery-slimscroll/jquery.slimscroll.min.js" type="text/javascript"></script>
    <script src="http://preview.thesoftking.com/thesoftking/catering/assets/backend/global/plugins/jquery.blockui.min.js" type="text/javascript"></script>
    <script src="http://preview.thesoftking.com/thesoftking/catering/assets/backend/global/plugins/uniform/jquery.uniform.min.js" type="text/javascript"></script>
    <script src="http://preview.thesoftking.com/thesoftking/catering/assets/backend/global/plugins/bootstrap-switch/js/bootstrap-switch.min.js" type="text/javascript"></script>
    <!-- END CORE PLUGINS -->
    <!-- BEGIN PAGE LEVEL PLUGINS -->
    <script src="http://preview.thesoftking.com/thesoftking/catering/assets/backend/global/plugins/bootstrap-daterangepicker/moment.min.js" type="text/javascript"></script>
    <script src="http://preview.thesoftking.com/thesoftking/catering/assets/backend/global/plugins/bootstrap-daterangepicker/daterangepicker.js" type="text/javascript"></script>
    <script src="http://preview.thesoftking.com/thesoftking/catering/assets/backend/global/plugins/bootstrap-datepicker/js/bootstrap-datepicker.min.js" type="text/javascript"></script>
    <script src="http://preview.thesoftking.com/thesoftking/catering/assets/backend/global/plugins/bootstrap-timepicker/js/bootstrap-timepicker.min.js" type="text/javascript"></script>
    <script src="http://preview.thesoftking.com/thesoftking/catering/assets/backend/global/plugins/bootstrap-datetimepicker/js/bootstrap-datetimepicker.min.js" type="text/javascript"></script>
    <script src="http://preview.thesoftking.com/thesoftking/catering/assets/backend/global/plugins/clockface/js/clockface.js" type="text/javascript"></script>

    <script src="http://preview.thesoftking.com/thesoftking/catering/assets/backend/global/plugins/bootstrap-daterangepicker/moment.min.js" type="text/javascript"></script>
    <script src="http://preview.thesoftking.com/thesoftking/catering/assets/backend/global/plugins/bootstrap-daterangepicker/daterangepicker.js" type="text/javascript"></script>
    <script src="http://preview.thesoftking.com/thesoftking/catering/assets/backend/global/plugins/morris/morris.min.js" type="text/javascript"></script>
    <script src="http://preview.thesoftking.com/thesoftking/catering/assets/backend/global/plugins/morris/raphael-min.js" type="text/javascript"></script>
    <script src="http://preview.thesoftking.com/thesoftking/catering/assets/backend/global/plugins/counterup/jquery.waypoints.min.js" type="text/javascript"></script>
    <script src="http://preview.thesoftking.com/thesoftking/catering/assets/backend/global/plugins/counterup/jquery.counterup.min.js" type="text/javascript"></script>
    <script src="http://preview.thesoftking.com/thesoftking/catering/assets/backend/global/plugins/fullcalendar/fullcalendar.min.js" type="text/javascript"></script>
    <script src="http://preview.thesoftking.com/thesoftking/catering/assets/backend/global/plugins/flot/jquery.flot.min.js" type="text/javascript"></script>
    <script src="http://preview.thesoftking.com/thesoftking/catering/assets/backend/global/plugins/flot/jquery.flot.resize.min.js" type="text/javascript"></script>
    <script src="http://preview.thesoftking.com/thesoftking/catering/assets/backend/global/plugins/flot/jquery.flot.categories.min.js" type="text/javascript"></script>
    <script src="http://preview.thesoftking.com/thesoftking/catering/assets/backend/global/plugins/jquery-easypiechart/jquery.easypiechart.min.js" type="text/javascript"></script>
    <script src="http://preview.thesoftking.com/thesoftking/catering/assets/backend/global/plugins/jquery.sparkline.min.js" type="text/javascript"></script>
    <!-- END PAGE LEVEL PLUGINS -->
    <!-- BEGIN THEME GLOBAL SCRIPTS -->
    <script src="http://preview.thesoftking.com/thesoftking/catering/assets/backend/js/app.min.js" type="text/javascript"></script>
    <!-- END THEME GLOBAL SCRIPTS -->
    <script src="http://preview.thesoftking.com/thesoftking/catering/assets/backend/pages/scripts/components-date-time-pickers.min.js" type="text/javascript"></script>
    <script src="http://preview.thesoftking.com/thesoftking/catering/assets/backend/global/plugins/bootstrap-fileinput/bootstrap-fileinput.js" type="text/javascript"></script>
    <!-- BEGIN PAGE LEVEL SCRIPTS -->
    <script src="http://preview.thesoftking.com/thesoftking/catering/assets/backend/js/dashboard.min.js" type="text/javascript"></script>
    <!-- END PAGE LEVEL SCRIPTS -->
    <!-- BEGIN THEME LAYOUT SCRIPTS -->
    <script src="http://preview.thesoftking.com/thesoftking/catering/assets/backend/js/layout.min.js" type="text/javascript"></script>
    <script src="http://preview.thesoftking.com/thesoftking/catering/assets/backend/js/demo.min.js" type="text/javascript"></script>
    <script src="http://preview.thesoftking.com/thesoftking/catering/assets/backend/js/quick-sidebar.min.js" type="text/javascript"></script>
    <!-- END THEME LAYOUT SCRIPTS -->
    <!--custom script register-->
    <script src="http://preview.thesoftking.com/thesoftking/catering/assets/backend/custom/js/p-loading.min.js"></script>
    <script src="http://preview.thesoftking.com/thesoftking/catering/assets/backend/custom/js/jquery.uploadPreview.min.js"></script>
    <script src="http://preview.thesoftking.com/thesoftking/catering/assets/backend/custom/js/bootstrap-multiselect.js"></script>
    <script src="http://preview.thesoftking.com/thesoftking/catering/assets/backend/custom/js/sweetalert.min.js"></script>
    <script src="http://preview.thesoftking.com/thesoftking/catering/assets/backend/custom/js/parsley.min.js"></script>
    <!--data table export script-->
    <script type="text/javascript" src="http://preview.thesoftking.com/thesoftking/catering/assets/backend/nicEdit.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            bkLib.onDomLoaded(function () { nicEditors.allTextAreas() });
        });
    </script>

    <script>
        var max = 1;
        $(document).ready(function () {
            $("#btnAddDescription").on('click', function () {
                appendPlanDescField($("#planDescriptionContainer"));
            });

            $(document).on('click', '.Delete_desc', function () {
                $(this).closest('.input-group').remove();
            });
        });

        function appendPlanDescField(container) {
            max++;
            container.append(
                '<div class="input-group">' +
                '<input name="description' + max + '" value="" class="form-control margin-top-10" type="text" required placeholder="Designation">\n' +
                '<span class="input-group-btn">' +
                '<button class="btn btn-danger margin-top-10 Delete_desc" type="button"><i class="fa fa-times"></i></button>' +
                '</span>' +
                '</div>'

            );
        }
    </script>
    <script>
        $('#txtEmail_Id').on('keypress', function () {
            var re = /([A-Z0-9a-z_-][^@])+?@[^$#<>?]+?\.[\w]{2,4}/.test(this.value);
            if (!re) {
                $('#error').show();
            } else {
                $('#error').hide();
            }
        })

    </script>
</body>
</html>
