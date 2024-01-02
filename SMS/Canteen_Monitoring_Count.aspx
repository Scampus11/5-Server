<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Canteen_Monitoring_Count.aspx.cs" Inherits="SMS.Canteen_Monitoring_Count" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8" />
    <title>Live SAG Monitoring Records 
</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta content="width=device-width, initial-scale=1" name="viewport" />
    <meta content="" name="description" />
    <meta content="" name="author" />
    <meta name="csrf-token" content="ax8ZN3lRSoa15FC1BBhhPnP5xckz7rpEbL25Olx3">
    <link rel="shortcut icon" href="favicon.ico" />
    <link rel="shortcut icon" type="image/png" href="Images/LOGO.jpeg"/>
<!-- BEGIN GLOBAL MANDATORY STYLES -->Live SAG Monitoring Records
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
    <link href="http://preview.thesoftking.com/thesoftking/catering/assets/backend/custom/css/style.css"  rel="stylesheet" type="text/css" />

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
    background: Lightgrey;
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

                <img src="Images/LOGO.jpeg" style="height: 30px" alt="logo" class="logo-default" /> </a>
            <div class="menu-toggler sidebar-toggler"></div>
        </div>
        <a href="javascript:;" class="menu-toggler responsive-toggler" data-toggle="collapse" data-target=".navbar-collapse"> </a>

        <div class="top-menu">
            <ul class="nav navbar-nav pull-right">

                <li class="dropdown dropdown-user">
                    <a href="javascript:;" class="dropdown-toggle" data-toggle="dropdown" data-hover="dropdown" data-close-others="true">

                        <span class="username username-hide-on-mobile">
                            admin
                        </span>
                        <i class="fa fa-angle-down"></i>
                    </a>
                    <ul class="dropdown-menu dropdown-menu-default">
                        <li>
                            <a href="#changePassword" data-toggle="modal">
                                <i class="icon-settings"></i> Change Password
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

<div class="clearfix"> </div>
    
<div class="page-container">

<div class="page-sidebar-wrapper" style="margin-bottom: -16px;">

    <div class="page-sidebar navbar-collapse collapse">

        <div class="page-sidebar navbar-collapse collapse">

        <ul class="page-sidebar-menu  page-header-fixed " data-keep-expanded="false" data-auto-scroll="true" data-slide-speed="200" style="padding-top: 20px">
            <li class="sidebar-toggler-wrapper hide">

                <div class="sidebar-toggler"> </div>

            </li>
            <li class="sidebar-search-wrapper">



            </li>
            <br/>
            <br/>
            <li class="nav-item start active1" runat="server" id="liAlloedMem" >
                                <asp:LinkButton ID="lnkalloedmem" runat="server" class="nav-link nav-toggle" OnClick="lnkalloedmem_Click">
                                    <i class="icon-briefcase"></i>
                                    <span class="title">Allowed Members(<asp:Label runat="server" ID="lblallowedcount"></asp:Label>)</span>
                                    <span class="selected"></span>
                                </asp:LinkButton>
                            </li>
                            <li class="nav-item start active1" runat="server" id="liAccessMem" >
                                <asp:LinkButton ID="lnkAccessMem" runat="server" class="nav-link nav-toggle" OnClick="lnkAccessMem_Click">
                                    <i class="icon-briefcase"></i>
                                    <span class="title">Access Members(<asp:Label runat="server" ID="lblaccesscount"></asp:Label>)</span>
                                    <span class="selected"></span>
                                </asp:LinkButton>
                            </li>
                            <li class="nav-item start active1" runat="server" id="liPendingMem">
                                <asp:LinkButton ID="lnkPendingMem" runat="server" class="nav-link nav-toggle" OnClick="lnkPendingMem_Click">
                                    <i class="icon-briefcase"></i>
                                    <span class="title"> Pending Members(<asp:Label runat="server" ID="lblPendingCount"></asp:Label>)</span>
                                    <span class="selected"></span>
                                </asp:LinkButton>
                            </li>
                            <li class="nav-item start active1" runat="server" id="lideniedMem" >
                                <asp:LinkButton ID="lnkdeniedMem" runat="server" class="nav-link nav-toggle" OnClick="lnkdeniedMem_Click">
                                    <i class="icon-briefcase"></i>
                                    <span class="title">denied Members(<asp:Label runat="server" ID="lbldeniedCount"></asp:Label>)</span>
                                    <span class="selected"></span>
                                </asp:LinkButton>
                            </li>
        </ul>

    </div>

    </div>
</div>


    <div class="page-content-wrapper">
        <div class="page-content">
            <h3 class="page-title bold">Live SAG Monitoring Records
                <%--<small> Live Canteen Monitoring Records </small>--%>
                
            </h3>
                        <div class="row" id="divgrid" runat="server">
                            <div class="col-md-12">

                                <div class="portlet box green-meadow">
                                    <div class="portlet-title">
                                        <div class="caption">
                                            <i class="fa fa-briefcase"></i>Live SAG Monitoring Records
                                             &nbsp;&nbsp;&nbsp;&nbsp;
                                             <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/session.jpg" Height="50px" />
                                        <asp:Label ID="lblSessionName" runat="server" ></asp:Label>
                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/canteen1.jpg" Height="50px" />
                                        <asp:Label ID="lblcanteen" runat="server" ></asp:Label>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:LinkButton ID="lnkExportToExcel" class="btn red" runat="server" OnClick="ExportToExcel" Visible="false">Export To Excel</asp:LinkButton>
                                            <asp:LinkButton ID="lnkExportToExcel2" class="btn red" runat="server" OnClick="ExportToExcel2" Visible="false">Export To Excel</asp:LinkButton>
                                            <asp:LinkButton ID="lnkExportToPDF" class="btn red" runat="server" OnClick="ExportToPDF" Visible="false">Export To PDF</asp:LinkButton>
                                        </div>
                                        <div class="tools">
                                        </div>
                                    </div>
                                    <div class="portlet-body" style="overflow: scroll">
                                        <div id="divsearch" runat="server">
                                        Search:
                                <asp:TextBox ID="txtSearch" runat="server" Width="230px" Height="35px" />&nbsp;&nbsp;&nbsp;
                            <asp:LinkButton ID="btnsearch" class="btn green-meadow" runat="server" Text="Search" OnClick="btnsearch_Click"></asp:LinkButton><br />
                                        </div><br />
                            <asp:GridView ID="gridEmployee" HeaderStyle-BackColor="#3AC0F2" HeaderStyle-ForeColor="White"
    RowStyle-BackColor="#A1DCF2" AlternatingRowStyle-BackColor="White" AlternatingRowStyle-ForeColor="#000" runat="server" class="table table-striped table-bordered table-hover" AutoGenerateColumns="false" AllowPaging="true" AllowSorting="true"  OnPageIndexChanging="OnPageIndexChanging" PageSize="5"   Width="100%" EmptyDataText="No Records Found!!!">
                                <PagerSettings Mode="NumericFirstLast" PageButtonCount="5" FirstPageText="First" LastPageText="Last" />
                                <PagerStyle CssClass="gridview"></PagerStyle>
                                <Columns>
                                    <asp:TemplateField ItemStyle-Width="10%" ItemStyle-Height="30px" HeaderText="Image"  >
                                        <ItemTemplate>
                                           <%--<asp:Image ID="tmgstudent" runat="server" ImageUrl='<%#Eval("StudentImages") %>' class="circle" />--%>
                                            <asp:Image ID="tmgstudent" runat="server" ImageUrl='<%#Eval("StudentImages")==""?"~/Images/images1.jpg":Eval("StudentImages") %>' class="circle" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField ItemStyle-Width="10%" ItemStyle-Height="30px" HeaderText="StudentId"  >
                                        <ItemTemplate>
                                           <asp:Label ID="lblstudentId" runat="server" Text='<%#Eval("StudentId") %>'></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                   <asp:TemplateField ItemStyle-Width="20%" ItemStyle-Height="30px" HeaderText="Student Name"  >
                                        <ItemTemplate>
                                           <asp:Label ID="lblStudent_Name" runat="server" Text='<%#Eval("Student_Name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="10%" ItemStyle-Height="30px" HeaderText="Canteen Name" >
                                        <ItemTemplate>
                                           <asp:Label ID="lblAGNAME" runat="server" Text='<%#Eval("AGNAME") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="10%" ItemStyle-Height="30px" HeaderText="Department"  >
                                        <ItemTemplate>
                                           <asp:Label ID="lblDepartment" runat="server" Text='<%#Eval("Department") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="10%" ItemStyle-Height="30px" HeaderText="Card Number"  >
                                        <ItemTemplate>
                                           <asp:Label ID="lblCardid" runat="server" Text='<%#Eval("Cardid") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="10%" ItemStyle-Height="30px" HeaderText="MealNumber"  >
                                        <ItemTemplate>
                                           <asp:Label ID="lblMealNumber" runat="server" Text='<%#Eval("MealNumber") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="10%" ItemStyle-Height="30px" HeaderText="Session Name"  >
                                        <ItemTemplate>
                                           <asp:Label ID="lblSession_Name" runat="server" Text='<%#Eval("Session_Name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>
                            </asp:GridView>

                        </div>

                        <div class="portlet-body" Style="overflow:scroll">
                            <div id="divsearch2" runat="server" visible="false">
                                        Search:
                                <asp:TextBox ID="txtSearch2" runat="server" Width="230px" Height="35px" />&nbsp;&nbsp;&nbsp;
                            <asp:LinkButton ID="lblSearch2" class="btn green-meadow" runat="server" Text="Search" OnClick="lblSearch2_Click"></asp:LinkButton><br />
                                </div><br />
                            <asp:GridView ID="GridMemaccess" HeaderStyle-BackColor="#3AC0F2" HeaderStyle-ForeColor="White"
    RowStyle-BackColor="#A1DCF2" AlternatingRowStyle-BackColor="White" AlternatingRowStyle-ForeColor="#000" runat="server" class="table table-striped table-bordered table-hover" AutoGenerateColumns="false" AllowPaging="true" AllowSorting="true"  OnPageIndexChanging="OnPageIndexChanging" PageSize="5"   Width="100%" Style="overflow:scroll" EmptyDataText="No Records Found!!!">
                                <PagerSettings Mode="NumericFirstLast" PageButtonCount="5" FirstPageText="First" LastPageText="Last" />
                                <PagerStyle CssClass="gridview"></PagerStyle>
                                <Columns>
                                    <asp:TemplateField ItemStyle-Width="10%" ItemStyle-Height="30px" HeaderText="Image"  >
                                        <ItemTemplate>
                                            <asp:Image ID="tmgstudent" runat="server" ImageUrl='<%#Eval("StudentImages")==""?"~/Images/images1.jpg":Eval("StudentImages") %>' class="circle" />
                                           <%--<asp:Image ID="tmgstudent" runat="server" ImageUrl='<%#Eval("StudentImages") %>' class="circle" />--%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField ItemStyle-Width="10%" ItemStyle-Height="30px" HeaderText="StudentId"  >
                                        <ItemTemplate>
                                           <asp:Label ID="lblstudentId" runat="server" Text='<%#Eval("StudentId") %>'></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                   <asp:TemplateField ItemStyle-Width="20%" ItemStyle-Height="30px" HeaderText="Student Name"  >
                                        <ItemTemplate>
                                           <asp:Label ID="lblStudent_Name" runat="server" Text='<%#Eval("Student_Name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="10%" ItemStyle-Height="30px" HeaderText="Punch Datetime"  >
                                        <ItemTemplate>
                                           <asp:Label ID="lblPunch_Datetime" runat="server" Text='<%#Eval("Punch_Datetime") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="10%" ItemStyle-Height="30px" HeaderText="Canteen Name"  >
                                        <ItemTemplate>
                                           <asp:Label ID="lblAGNAME" runat="server" Text='<%#Eval("AGNAME") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="10%" ItemStyle-Height="30px" HeaderText="Department"  >
                                        <ItemTemplate>
                                           <asp:Label ID="lblDepartment" runat="server" Text='<%#Eval("Department") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="10%" ItemStyle-Height="30px" HeaderText="Card Number"  >
                                        <ItemTemplate>
                                           <asp:Label ID="lblCardid" runat="server" Text='<%#Eval("Cardid") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="10%" ItemStyle-Height="30px" HeaderText="MealNumber"  >
                                        <ItemTemplate>
                                           <asp:Label ID="lblMealNumber" runat="server" Text='<%#Eval("MealNumber") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="10%" ItemStyle-Height="30px" HeaderText="Access Code"  >
                                        <ItemTemplate>
                                           <asp:Label ID="lblAccess_Code" runat="server" Text='<%#Eval("Access_Code") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="10%" ItemStyle-Height="30px" HeaderText="Grant Access"  Visible="false">
                                        <ItemTemplate>
                                           <asp:Label ID="lblGrant_Access" runat="server" Text='<%#Eval("Grant_Access") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="10%" ItemStyle-Height="30px" HeaderText="Reader Id"  Visible="false">
                                        <ItemTemplate>
                                           <asp:Label ID="lblSLN_Reader_Id" runat="server" Text='<%#Eval("SLN_Reader_Id") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="10%" ItemStyle-Height="30px" HeaderText="Reader Name"  Visible="false">
                                        <ItemTemplate>
                                           <asp:Label ID="lblReader_Name" runat="server" Text='<%#Eval("Reader_Name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="10%" ItemStyle-Height="30px" HeaderText="Session ID" Visible="false" >
                                        <ItemTemplate>
                                           <asp:Label ID="lblSession_ID" runat="server" Text='<%#Eval("Session_ID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="10%" ItemStyle-Height="30px" HeaderText="Session Name"  >
                                        <ItemTemplate>
                                           <asp:Label ID="lblSession_Name" runat="server" Text='<%#Eval("Session_Name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>
                            </asp:GridView>

                        </div>
                                </div>
                            </div>
                        </div>
                
                                    </div>
                                </div>
                        
                    </div>


    </div>


<!-- END CONTAINER -->
        </form>
<!-- BEGIN FOOTER -->
<div class="page-footer">
    <div class="page-footer-inner"> 2018 &copy; ALL RIGHTS RESERVED
    </div>
    <div class="scroll-to-top">
        <i class="icon-arrow-up"></i>
    </div>
</div><!-- END FOOTER -->
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
</body>
</html>
