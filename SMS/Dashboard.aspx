<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="SMS.Dashboard" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8" />
    <title>DashBoard Of Admin
    </title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta content="width=device-width, initial-scale=1" name="viewport" />
    <meta content="" name="description" />
    <meta content="" name="author" />
    <meta name="csrf-token" content="ax8ZN3lRSoa15FC1BBhhPnP5xckz7rpEbL25Olx3">
    <link rel="shortcut icon" href="favicon.ico" />
    <link rel="shortcut icon" type="image/png" href="http://preview.thesoftking.com/thesoftking/catering/assets/images/logo/icon.png" />
    <!-- BEGIN GLOBAL MANDATORY STYLES -->
    <link href="CSS/font-awesome.css" rel="stylesheet" type="text/css" />
    <link href="CSS/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="CSS/simple-line-icons.min.css" rel="stylesheet" type="text/css" />
    <link href="CSS/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="CSS/uniform.default.css" rel="stylesheet" type="text/css" />
    <link href="CSS/bootstrap-switch.min.css" rel="stylesheet" type="text/css" />
    <!-- END GLOBAL MANDATORY STYLES -->

    <!-- BEGIN PAGE LEVEL PLUGINS -->
    <link href="CSS/daterangepicker-bs3.css" rel="stylesheet" type="text/css" />
    <link href="CSS/morris.css" rel="stylesheet" type="text/css" />
    <link href="CSS/fullcalendar.min.css" rel="stylesheet" type="text/css" />
    <link href="CSS/jqvmap.css" rel="stylesheet" type="text/css" />
    <!-- END PAGE LEVEL PLUGINS -->

    <!-- BEGIN THEME GLOBAL STYLES -->
    <link href="CSS/components-rounded.min.css" rel="stylesheet" id="style_components" type="text/css" />
    <link href="CSS/plugins.min.css" rel="stylesheet" type="text/css" />
    <!-- END THEME GLOBAL STYLES -->
    <!-- BEGIN THEME LAYOUT STYLES -->
    <link href="CSS/layout.min.css" rel="stylesheet" type="text/css" />
    <link href="CSS/darkblue.min.css" rel="stylesheet" type="text/css" id="style_color" />
    <link href="CSS/custom.min.css" rel="stylesheet" type="text/css" />
    <!-- END THEME LAYOUT STYLES -->



    <link href="CSS/bootstrap-multiselect.css" rel="stylesheet" type="text/css" />
    <link href="CSS/p-loading.min.css" rel="stylesheet" type="text/css" />
    <link href="CSS/sweetalert.css" rel="stylesheet" type="text/css" />
    <link href="CSS/style.css" rel="stylesheet" type="text/css" />
    <%--<link href="CSS/style.css"  rel="stylesheet" type="text/css" />--%>

    <script src="JS/jquery.min.js" type="text/javascript"></script>

    <!-- BEGIN PAGE LEVEL PLUGINS -->

    <%--<link href="CSS/bootstrap-datepicker3.min.css" rel="stylesheet" type="text/css" />--%>
    <link href="CSS/bootstrap-datepicker3.min.css" rel="stylesheet" type="text/css" />
    <link href="CSS/bootstrap-timepicker.min.css" rel="stylesheet" type="text/css" />
    <link href="CSS/bootstrap-datetimepicker.min.css" rel="stylesheet" type="text/css" />
    <link href="CSS/clockface.css" rel="stylesheet" type="text/css" />
    <!-- END PAGE LEVEL PLUGINS -->

    <link rel="stylesheet" href="CSS/ajax.css">

    <link href="CSS/layout.min.css" rel="stylesheet" type="text/css" />
    <link href="CSS/darkblue.min.css" rel="stylesheet" type="text/css" id="Link1" />
    <link href="CSS/custom.min.css" rel="stylesheet" type="text/css" />


    <!-- END HEAD -->
</head>
<body class="page-header-fixed page-sidebar-closed-hide-logo page-content-white">
    <form id="form1" runat="server">
        <div class="page-header navbar navbar-fixed-top">

            <div class="page-header-inner ">

                <div class="page-logo">
                    <a href="">
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
                                    <a href="#changePassword" data-toggle="modal">Change Password
                                    </a>
                                </li>
                                <li>
                                    <%-- <a href="http://preview.thesoftking.com/thesoftking/catering/admin/logout" onclick="event.preventDefault();
                        document.getElementById('logout-form').submit();">
                                <form id="logout-form" action="http://preview.thesoftking.com/thesoftking/catering/admin/logout" method="POST">
                                    <input type="hidden" name="_token" value="ax8ZN3lRSoa15FC1BBhhPnP5xckz7rpEbL25Olx3">
                                    <i class="icon-key"></i> Log Out
                                </form>
                            </a>--%>
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
                            <li class="nav-item start active">
                                <a href="Dashboard.aspx" class="nav-link nav-toggle">
                                    <i class="icon-home"></i>
                                    <span class="title">Dashboard</span>
                                    <span class="selected"></span>
                                </a>
                            </li>

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
                        </ul>

                    </div>

                </div>
            </div>


            <!-- BEGIN CONTENT -->
            <div class="page-content-wrapper">
                <!-- BEGIN CONTENT BODY -->
                <div class="page-content">
                    <!-- BEGIN PAGE TITLE-->
                    <h3 class="page-title">Dashboard
                <small>dashboard & statistics</small>
                    </h3>
                    <div class="row">
                        <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                            <div class="dashboard-stat blue">
                                <div class="visual">
                                    <i class="fa fa-comments"></i>
                                </div>
                                <div class="details">
                                    <div class="number">
                                        <span data-counter="counterup" data-value="6">0</span>
                                    </div>
                                    <div class="desc">Total Employee </div>
                                </div>
                                <a class="more" href="http://preview.thesoftking.com/thesoftking/catering/admin/employee">View more
                            <i class="m-icon-swapright m-icon-white"></i>
                                </a>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                            <div class="dashboard-stat red">
                                <div class="visual">
                                    <i class="fa fa-bar-chart-o"></i>
                                </div>
                                <div class="details">
                                    <div class="number">
                                        <span data-counter="counterup" data-value="236.00">0</span>
                                    </div>
                                    <div class="desc">Total Income </div>
                                </div>
                                <a class="more" href="http://preview.thesoftking.com/thesoftking/catering/admin/accounts">View more
                            <i class="m-icon-swapright m-icon-white"></i>
                                </a>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                            <div class="dashboard-stat green">
                                <div class="visual">
                                    <i class="fa fa-shopping-cart"></i>
                                </div>
                                <div class="details">
                                    <div class="number">
                                        <span data-counter="counterup" data-value="3,353.00">0</span>
                                    </div>
                                    <div class="desc">Total Expense </div>
                                </div>
                                <a class="more" href="http://preview.thesoftking.com/thesoftking/catering/admin/accounts">View more
                            <i class="m-icon-swapright m-icon-white"></i>
                                </a>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                            <div class="dashboard-stat purple">
                                <div class="visual">
                                    <i class="fa fa-globe"></i>
                                </div>
                                <div class="details">
                                    <div class="number">
                                        +
                                <span data-counter="counterup" data-value="89"></span>%
                                    </div>
                                    <div class="desc">Brand Popularity  </div>
                                </div>
                                <a class="more" href="javascript:;">View more
                            <i class="m-icon-swapright m-icon-white"></i>
                                </a>
                            </div>
                        </div>
                    </div>
                    <div class="clearfix"></div>
                    <!-- END DASHBOARD STATS 1-->
                    <div class="row">
                        <div class="col-md-11">
                            <div id="myfirstchart" style="height: 450px; width: 100%;"></div>

                        </div>
                    </div>
                </div>
                <!-- END CONTENT BODY -->
            </div>
            <!-- END CONTENT -->


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
    </form>
    <!-- END CONTAINER -->
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
    <script src="JS/excanvas.min.js"></script>

    <script src="JS/table-datatables-colreorder.min.js"></script>
    <script src="JS/datatable.js" type="text/javascript"></script>
    <script src="JS/datatables.min.js" type="text/javascript"></script>
    <script src="JS/datatables.bootstrap.js" type="text/javascript"></script>
    <script src="JS/app.min.js" type="text/javascript"></script>

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
        $(document).ready(function () {
            new Morris.Bar({
                element: 'myfirstchart',
                data: [{ year: '2011', value: 25 }, { year: '2012', value: 55 }, { year: '2013', value: 40 }, { year: '2014', value: 65 }, { year: '2015', value: 60 }, { year: '2016', value: 72 }, { year: '2017', value: 45 }, { year: '2018', value: 10 }],
                xkey: 'year',
                ykeys: ['value'],
                // chart.
                labels: ['Value']
            });
        });
    </script>
</body>
</html>
