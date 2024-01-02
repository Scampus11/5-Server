<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Access_Group_Master.aspx.cs" Inherits="SMS.Access_Group_Master" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8" />
    <title>Access Group  List
</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta content="width=device-width, initial-scale=1" name="viewport" />
    <meta content="" name="description" />
    <meta content="" name="author" />
    <meta name="csrf-token" content="ax8ZN3lRSoa15FC1BBhhPnP5xckz7rpEbL25Olx3">
    <link rel="shortcut icon" href="favicon.ico" />
    <link rel="shortcut icon" type="image/png" href="Images/LOGO.jpeg"/>
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
                            <%--<a href="http://preview.thesoftking.com/thesoftking/catering/admin/logout" onclick="event.preventDefault();
                        document.getElementById('logout-form').submit();">
                                
                                    <input type="hidden" name="_token" value="ax8ZN3lRSoa15FC1BBhhPnP5xckz7rpEbL25Olx3">
                                    <i class="icon-key"></i> Log Out
                                
                            </a>--%>
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

<div class="page-sidebar-wrapper">

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
            <%--<li class="nav-item start active1">
                <a href="Dashboard.aspx" class="nav-link nav-toggle">
                    <i class="icon-home"></i>
                    <span class="title">Dashboard</span>
                    <span class="selected"></span>
                </a>
            </li>--%>

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
                    <span class="title">officer Master</span>
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
                    <span class="title">Door Group  Master</span>
                    <span class="selected"></span>
                </a>
            </li>
            
            <li class="nav-item start active">
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
            <li class="nav-item start active1">
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
            <h3 class="page-title bold">Access Group  Master
                <small> Access Group -list </small>
                <!-- BEGIN EXAMPLE TABLE PORTLET-->
                <%--<a class="btn green-meadow pull-right" data-toggle="modal" href="#static">
                    Add New Department
                    <i class="fa fa-plus"></i> </a>--%>
                <asp:LinkButton ID="lnkAdd" runat="server" Text="Add New Access Group " class="btn green-meadow pull-right" OnClick="lnkAdd_Click">

                </asp:LinkButton>
                <hr>
            </h3>
            <!-- END PAGE TITLE-->
                        <!-- BEGIN PAGE CONTENT-->
            
                        <div class="row" id="divgrid" runat="server">
                            <div class="col-md-12">

                                <div class="portlet box green-meadow">
                                    <div class="portlet-title">
                                        <div class="caption">
                                            <i class="fa fa-briefcase"></i>Access Group  List
                                        </div>
                                        <div class="tools">
                                        </div>
                                    </div>
                                    <div class="portlet-body">
                <asp:GridView ID="gridEmployee" runat="server" class="table table-striped table-bordered table-hover" AutoGenerateColumns="false" AllowPaging="true" AllowSorting="true" OnSorting="OnSorting" OnPageIndexChanging="OnPageIndexChanging" PageSize="5">
                    <PagerSettings Mode="NumericFirstLast" PageButtonCount="5" FirstPageText="First" LastPageText="Last"/>
                                        <pagerstyle cssclass="gridview"></pagerstyle>
                    <Columns>
                        <asp:TemplateField ItemStyle-Width="30px" HeaderText="Access Group  Id" SortExpression="Id">
                            <ItemTemplate>
                                <asp:Label ID="lblId1" runat="server" Text='<%#Eval("Id") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-Width="30px" HeaderText="Access Group  Name" SortExpression="Access_Group_Name">
                            <ItemTemplate>
                                <asp:Label ID="lblApplication_No" runat="server" Text='<%#Eval("Access_Group_Name") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField ItemStyle-Width="30px" HeaderText="Description" SortExpression="Description">
                            <ItemTemplate>
                                <asp:Label ID="lblStaff_Id" runat="server" Text='<%#Eval("Description") %>'></asp:Label>
                                <asp:Label ID="lblid" runat="server" Text='<%#Eval("Id") %>' Visible="false"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%--<asp:TemplateField ItemStyle-Width="30px" HeaderText="Door Group Name">
                            <ItemTemplate>
                                <asp:Label ID="lblDoorGroup" runat="server" Text='<%#Eval("Door_Name") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-Width="30px" HeaderText="Session Name">
                            <ItemTemplate>
                                <asp:Label ID="lblSession" runat="server" Text='<%#Eval("Session_Name") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                         <%--<asp:TemplateField ItemStyle-Width="30px" HeaderText="Door Group Name">
                            <ItemTemplate>
                                <asp:Label ID="lblReader_id" runat="server" Text='<%#Eval("Door_Group_Id") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                       --%>
                        <asp:TemplateField ItemStyle-Width="50px" HeaderText="VIEW/Edit">
                            <ItemTemplate>
                                <%--<asp:LinkButton ID="lnkview" runat="server" class="btn green-meadow"  data-toggle="modal" 
                                    PostBackUrl='<%# "Officer_Master.aspx?StudentID="+Eval("StudentID") %>'
                                     > View/Edit</asp:LinkButton>--%>
                                <asp:LinkButton ID="linkEdit" class="btn green-meadow" runat="server" Text="Edit" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id")%>' OnClick="linkEdit_Click"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <%--<asp:TemplateField ItemStyle-Width="50px" HeaderText="Delete">
                            <ItemTemplate>
                                
                                <asp:LinkButton ID="linkDelete" class="btn green-meadow" runat="server" Text="Delete"  OnClick="linkDelete_Click"
                                    ></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                    </Columns>


                </asp:GridView>
                                        <%--<table class="table table-striped table-bordered table-hover">
                                            <thead>
                                            <tr>
                                                <th>
                                                    ID
                                                </th>
                                                <th>
                                                    Department Name
                                                </th>
                                                <th>
                                                    Designations
                                                </th>
                                                <th>
                                                    Action
                                                </th>
                                            </tr>
                                            </thead>
                                            <tbody>

                                                                                        <tr id="row1">
                                                <td>
                                                    1
                                                </td>
                                                <td>
                                                    Laravel
                                                </td>
                                                                                                <td>
                                                                                                        <ul>
                                                        <li>Intern</li>
                                                    </ul>
                                                                                                        <ul>
                                                        <li>Entry</li>
                                                    </ul>
                                                                                                        <ul>
                                                        <li>Mid Level</li>
                                                    </ul>
                                                                                                    </td>
                                                <td>
                                                    <a class="btn green-meadow"  data-toggle="modal" href="http://preview.thesoftking.com/thesoftking/catering/admin/department/Edit/39" onclick="showEdit(1,'PHP')"><i class="fa fa-Edit"></i> View/Edit</a>
                                                    <a class="btn red" href="http://preview.thesoftking.com/thesoftking/catering/admin/department/Delete/39"><i class="fa fa-trash"></i> Delete</a>
                                                </td>
                                            </tr>
                                                                                        <tr id="Tr1">
                                                <td>
                                                    2
                                                </td>
                                                <td>
                                                    Android
                                                </td>
                                                                                                <td>
                                                                                                        <ul>
                                                        <li>Pro</li>
                                                    </ul>
                                                                                                        <ul>
                                                        <li>Begginer</li>
                                                    </ul>
                                                                                                        <ul>
                                                        <li>Intern-level</li>
                                                    </ul>
                                                                                                    </td>
                                                <td>
                                                    <a class="btn green-meadow"  data-toggle="modal" href="http://preview.thesoftking.com/thesoftking/catering/admin/department/Edit/41" onclick="showEdit(1,'PHP')"><i class="fa fa-Edit"></i> View/Edit</a>
                                                    <a class="btn red" href="http://preview.thesoftking.com/thesoftking/catering/admin/department/Delete/41"><i class="fa fa-trash"></i> Delete</a>
                                                </td>
                                            </tr>
                                                                                        </tbody>
                                        </table>--%>
                                    </div>
                                </div>
                                <!-- END EXAMPLE TABLE PORTLET-->
                            </div>
                        </div>
                <div class="modal-content" id="divView" runat="server" visible="false">
                                    <div class="modal-body">
                                        <div class="portlet-body form">
                                            <!-- BEGIN FORM-->
                                            <div class="form-body">
                                                    <%--<p class="text-success">
                                                        Staff ID :
                                                    </p>
                                                    <div class="form-group">
                                                        <div class="description" style="width: 100%;border: 1px solid #ddd;padding: 10px;border-radius: 5px" >
                                                            <div class="row">
                                                        <div class="col-md-12">
                                                            <asp:TextBox ID="txtStudentID" runat="server"></asp:TextBox>
                                                        </div>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <hr/>--%>
                                                    
                                                    <div class="form-group">
                                                        <label class="text-success"> Access Group  Name : </label>
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                <div class="description" style="width: 100%;border: 1px solid #ddd;padding: 10px;border-radius: 5px" >
                                                                    <div class="row">
                                                                        <div class="col-md-12" id="Div1">
                                                                            <div class="input-group">
                                                                                <asp:TextBox ID="txtApplication_No" runat="server"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                <hr/>
                                                    
                                                    <div class="form-group">
                                                        <label class="text-success"> Description : </label>
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                <div class="description" style="width: 100%;border: 1px solid #ddd;padding: 10px;border-radius: 5px" >
                                                                    <div class="row">
                                                                        <div class="col-md-12" id="Div2">
                                                                            <div class="input-group">
                                                                                <%--<asp:DropDownList ID="ddlType" runat="server" >
                                                                                    <asp:ListItem Text="Entry">Entry</asp:ListItem>
                                                                                    <asp:ListItem Text="Exit">Exit</asp:ListItem>
                                                                                </asp:DropDownList>--%>
                                                                                <asp:TextBox ID="txtDescription" runat="server"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                <hr/>
                                                <div class="form-group">
                                                        
                                                   <%-- <label class="text-success"> Session : </label>--%>
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                <div class="description" style="width: 100%;border: 1px solid #ddd;padding: 10px;border-radius: 5px" >
                                                                    <div class="row">
                                                                        <div class="col-md-4">
                                                                            <label class="text-success"> Door Group : </label>
                                                                            <div class="input-group">
                                                                                <asp:DropDownList ID="ddlDoorgroup" runat="server" >
                                                                                </asp:DropDownList>
                                                                                
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-md-4">
                                                                            <label class="text-success"> Session : </label>
                                                                            <div class="input-group">
                                                                                <asp:DropDownList ID="ddlSession" runat="server" >
                                                                                </asp:DropDownList>
                                                                                
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-md-4">
                                                                            <label class="text-success"></label>
                                                                            <div class="input-group">
                                                                                <asp:LinkButton ID="LnkAdd1" runat="server" Text="Add" OnClick="LnkAdd1_Click" class="btn green-meadow" />
                                                                                
                                                                            </div>
                                                                        </div>
                                                                        
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>

                                                   <%--temp grid to bind--%>
                                                     <div class="row" id="div3" runat="server">
                            <div class="col-md-12">

                                <div class="portlet box green-meadow">
                                    <div class="portlet-title">
                                        <div class="caption">
                                            <i class="fa fa-briefcase"></i>Access Group  List
                                        </div>
                                        <div class="tools">
                                        </div>
                                    </div>
                                    <div class="portlet-body">
                <asp:GridView ID="gvAccess" runat="server" class="table table-striped table-bordered table-hover" AutoGenerateColumns="false" ShowFooter="true">
                    <Columns>
                        <asp:TemplateField ItemStyle-Width="30px" HeaderText="Access Group Id" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblChildId" runat="server" Text='<%#Eval("ChildId") %>'></asp:Label>
                                <asp:Label ID="lblAccessGroupId" runat="server" Text='<%#Eval("Id") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-Width="30px" HeaderText="Door Group">
                            <ItemTemplate>
                                <asp:Label ID="lblDoorGroup" runat="server" Text='<%#Eval("Door_Group") %>'></asp:Label>
                                <asp:Label ID="lblDoorGroupId" Visible="false" runat="server" Text='<%#Eval("Door_GroupId") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField ItemStyle-Width="30px" HeaderText="Session">
                            <ItemTemplate>
                                <asp:Label ID="lblSession" runat="server" Text='<%#Eval("Session") %>'></asp:Label>
                                <asp:Label ID="lblSessionId" Visible="false" runat="server" Text='<%#Eval("SessionId") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <%--<asp:TemplateField ItemStyle-Width="50px" HeaderText="VIEW/Edit">
                            <ItemTemplate>
                               <asp:LinkButton ID="linkEdit" class="btn green-meadow" runat="server" Text="Edit" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id")%>' OnClick="linkEdit_Click"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>--%>

                        <asp:TemplateField ItemStyle-Width="50px" HeaderText="Delete">
                            <ItemTemplate>
                               <asp:LinkButton ID="linkChildDelete" class="btn green-meadow" runat="server" Text="Delete"  OnClick="linkChildDelete_Click"
                                    ></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>


                </asp:GridView>
                                        
                                    </div>
                                </div>
                            </div>
                        </div>
                                                    </div>
                                                    
                                               <%-- <div class="form-group">
                                                        <label class="text-success"> Reader Type : </label>
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                <div class="description" style="width: 100%;border: 1px solid #ddd;padding: 10px;border-radius: 5px" >
                                                                    <div class="row">
                                                                        <div class="col-md-12" id="Div3">
                                                                            <div class="input-group">
                                                                                <asp:Repeater ID="RepeatReader" runat="server">
                                                                                    <ItemTemplate>
                                                                                        <asp:CheckBox ID="chkReader" runat="server" Text='<%# Eval("Door_Name")  %>'  />&nbsp;
                                                                                        <asp:Label ID ="lblId" runat="server" Text='<%# Eval("Id") %>' Visible="false"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    </asp:Repeater>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>--%>
                                                   
                                                <br/>

                                                <asp:LinkButton ID="lnksave" runat="server" Text="Save" OnClick="btnsave_Click" class="btn green-meadow" Visible="false" />
                                                <asp:LinkButton ID="lnkupdate" runat="server" Text="Update" OnClick="btnupdate_Click" class="btn green-meadow" Visible="false" />
                                                <asp:LinkButton ID="lnkBack" runat="server" Text="Back" OnClick="lnkBack_Click" class="btn green-meadow" />
                                                    
                                                   <%-- <div class="form-group">
                                                        <label class="text-success"> UniqueNo : </label>
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                <div class="description" style="width: 100%;border: 1px solid #ddd;padding: 10px;border-radius: 5px" >
                                                                    <div class="row">
                                                                        <div class="col-md-12" id="Div17">
                                                                            <div class="input-group">
                                                                                <asp:Label ID="lblUniqueNo" runat="server"></asp:Label>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                <hr/>
                                                    
                                                    <div class="form-group">
                                                        <label class="text-success"> Status : </label>
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                <div class="description" style="width: 100%;border: 1px solid #ddd;padding: 10px;border-radius: 5px" >
                                                                    <div class="row">
                                                                        <div class="col-md-12" id="Div18">
                                                                            <div class="input-group">
                                                                                <asp:Label ID="lblStatus" runat="server"></asp:Label>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                <hr/>
                                                    
                                                    <div class="form-group">
                                                        <label class="text-success"> Isactive : </label>
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                <div class="description" style="width: 100%;border: 1px solid #ddd;padding: 10px;border-radius: 5px" >
                                                                    <div class="row">
                                                                        <div class="col-md-12" id="Div19">
                                                                            <div class="input-group">
                                                                                <asp:Label ID="lblIsactive" runat="server"></asp:Label>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                <hr/>
                                                    
                                                    <div class="form-group">
                                                        <label class="text-success"> id : </label>
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                <div class="description" style="width: 100%;border: 1px solid #ddd;padding: 10px;border-radius: 5px" >
                                                                    <div class="row">
                                                                        <div class="col-md-12" id="Div20">
                                                                            <div class="input-group">
                                                                                <asp:Label ID="lblid" runat="server"></asp:Label>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                <hr/>
                                                    
                                                    <div class="form-group">
                                                        <label class="text-success"> UNIQUEID : </label>
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                <div class="description" style="width: 100%;border: 1px solid #ddd;padding: 10px;border-radius: 5px" >
                                                                    <div class="row">
                                                                        <div class="col-md-12" id="Div21">
                                                                            <div class="input-group">
                                                                                <asp:Label ID="lblUNIQUEID" runat="server"></asp:Label>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>--%>
                                                        </div>
                                                    </div>
                                                </div>

                                                
                                            
                                            <!-- END FORM-->
                                        </div>
                                    </div>
                                    <!-- END EXAMPLE TABLE PORTLET-->
                                </div>
                        <!-- END PAGE CONTENT-->
                        <div id="static" class="modal fade" tabindex="-1" data-backdrop="static" data-keyboard="false">
                            <div class="modal-dialog">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">Close</button>
                                        <h4 class="modal-title"><strong><i class="fa fa-plus"></i> New Department</strong></h4>
                                    </div>
                                    <div class="modal-body">
                                        <div class="portlet-body form">
                                            <!-- BEGIN FORM-->
                                            
                                                <input type="hidden" name="_token" value="ax8ZN3lRSoa15FC1BBhhPnP5xckz7rpEbL25Olx3">
                                                <div class="form-body">
                                                    <p class="text-success">
                                                        Department
                                                    </p>
                                                    <div class="form-group">
                                                        <div class="description" style="width: 100%;border: 1px solid #ddd;padding: 10px;border-radius: 5px" >
                                                            <div class="row">
                                                        <div class="col-md-12">
                                                            <input class="form-control form-control-inline " name="name" type="text" value="" placeholder="Department Name"/>
                                                        </div>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <hr>
                                                    
                                                    <div class="form-group">
                                                        <label class="text-success"> Designation : </label>
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                <div class="description" style="width: 100%;border: 1px solid #ddd;padding: 10px;border-radius: 5px" >
                                                                    <div class="row">
                                                                        <div class="col-md-12" id="planDescriptionContainer">
                                                                            <div class="input-group">
                                                                                <input name="deg_name" class="form-control margin-top-10" type="text" required placeholder="Designation Name">
                                                                                <span class="input-group-btn">
                                                                                    <button class="btn btn-danger margin-top-10 Delete_desc" type="button"><i class='fa fa-times'></i></button></span>
                                                                            </div>
                                                                        </div>
                                                                    </div>

                                                                    <div class="row">
                                                                        <div class="col-md-12 text-right margin-top-10">
                                                                            <button id="btnAddDescription" type="button" class="btn btn-sm grey-mint pullri">Add Designation</button>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="form-actions">
                                                    <div class="row">
                                                        <div class="col-md-12">
                                                            <button type="submit" data-loading-text="Submitting..." class="demo-loading-btn btn green-meadow"><i class="fa fa-check"></i> Submit</button>
                                                            <button type="button" class="btn dark btn-outline" data-dismiss="modal">Close</button>
                                                        </div>
                                                    </div>
                                                </div>
                                            
                                            <!-- END FORM-->
                                        </div>
                                    </div>
                                    <!-- END EXAMPLE TABLE PORTLET-->
                                </div>
                            </div>
                        </div>
                    </div>
                </div>


    </div>

<div class="modal fade" id="changePassword" tabindex="-1" role="changePassword" aria-hidden="true">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-hidden="true"></button>
                <h4 class="modal-title">Change Password</h4>
            </div>
            <div class="modal-body">
               
                    <input type="hidden" name="_token" value="ax8ZN3lRSoa15FC1BBhhPnP5xckz7rpEbL25Olx3">

                    <input type="hidden" value="1" name="id">

                    <div class="form-group">
                        <label for="password" class="col-md-4 control-label">Old Password</label>

                        <div class="col-md-6">
                            <input id="password" type="password" class="form-control" name="passwordold" required>

                                                    </div>
                    </div>

                    <div class="form-group">
                        <label for="password" class="col-md-4 control-label">New Password</label>

                        <div class="col-md-6">
                            <input id="password1" type="password" class="form-control" name="password" required>

                                                    </div>
                    </div>

                    <div class="form-group">
                        <label for="password-confirm" class="col-md-4 control-label">Confirm Password</label>
                        <div class="col-md-6">
                            <input id="password-confirm" type="password" class="form-control" name="password_confirmation" required>

                                                    </div>
                    </div>

                    <div class="modal-footer">
                        <button type="button" class="btn dark btn-outline" data-dismiss="modal">Close</button>
                        <button class="btn green" type="submit">Change</button>
                    </div>
               


            </div>
        </div>
        <!-- /.modal-content -->
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
