<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FileUpload.aspx.cs" Inherits="SMS.FileUpload" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8" />
    <title>Student List
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
                                    <%--<a href="http://preview.thesoftking.com/thesoftking/catering/admin/logout" onclick="event.preventDefault();
                        document.getElementById('logout-form').submit();">
                                
                                    <input type="hidden" name="_token" value="ax8ZN3lRSoa15FC1BBhhPnP5xckz7rpEbL25Olx3">
                                    <i class="icon-key"></i> Log Out
                                
                            </a>--%>
                                    <asp:LinkButton ID="lnklogout" runat="server" OnClick="lnklogout_Click" Text="Log Out"></asp:LinkButton>
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

                    <ul class="page-sidebar-menu  page-header-fixed " data-keep-expanded="false" data-auto-scroll="true" data-slide-speed="200" style="padding-top: 20px">
                        <li class="sidebar-toggler-wrapper hide">

                            <div class="sidebar-toggler"></div>

                        </li>
                        <li class="sidebar-search-wrapper"></li>
                        <br />
                        <br />
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
                                <span class="title">Access List  Master</span>
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
                        <li class="nav-item start active">
                            <a href="FileUpload.aspx" class="nav-link nav-toggle">
                                <i class="icon-briefcase"></i>
                                <span class="title">Import To excel</span>
                                <span class="selected"></span>
                            </a>
                        </li>
                    </ul>

                </div>
            </div>


            <!-- BEGIN CONTENT -->
            <div class="page-content-wrapper">
                <!-- BEGIN CONTENT BODY -->
                <div class="page-content">
                    <!-- BEGIN PAGE HEADER-->
                    <!-- END PAGE BAR -->
                    <!-- BEGIN PAGE TITLE-->
                    <h3 class="page-title bold">Import To Excel
                <small>Import To Excel</small>
                        <!-- BEGIN EXAMPLE TABLE PORTLET-->
                        <a class="btn green-meadow pull-right" data-toggle="modal" href="#static" style="display: none">Add New Department
                    <i class="fa fa-plus"></i></a>
                        <hr>
                    </h3>
                    <!-- END PAGE TITLE-->
                    <!-- BEGIN PAGE CONTENT-->

                    <div class="row" id="divgrid" runat="server">
                        <div class="col-md-12">

                            <div class="portlet box green-meadow">
                                <div class="portlet-title">
                                    <div class="caption">
                                        <i class="fa fa-briefcase"></i>Import to Excel
                                    </div>
                                    <div class="tools">
                                    </div>
                                </div>

                                <div class="portlet-body">
                                </div>
                                <div class="modal-content" >
                        <div class="modal-body">
                            <div class="portlet-body form">
                                <!-- BEGIN FORM-->
                                <div class="form-body">
                                    <p class="text-success">
                                        Import To Excel:
                                    </p>
                                    <div class="form-group">
                                        <div class="description" style="width: 100%; border: 1px solid #ddd; padding: 10px; border-radius: 5px">
                                            <div class="row">
                                                <div class="col-md-4">
                                                    <asp:FileUpload ID="FileUpload1" runat="server" />
                                                     
                                                </div>
                                                <div class="col-md-8">
                                                    <%--<asp:Button ID="Button1" runat="server" Text="Export" OnClick="btnUpload_Click" />--%>
                                                    <asp:LinkButton ID="lnkupdate" runat="server" Text="Import" OnClick="btnUpload_Click" class="btn green-meadow" />
                                                     <asp:Label ID="Label1" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    </div>
                                </div>
                            </div>
                                    </div>
                                
                            </div>
                            <!-- END EXAMPLE TABLE PORTLET-->
                        </div>
                    </div>



                    <!-- END PAGE CONTENT-->

                </div>
            </div>


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
</body>
</html>
