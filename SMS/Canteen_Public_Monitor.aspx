<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="Canteen_Public_Monitor.aspx.cs" Inherits="SMS.Canteen_Public_Monitor" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Canteen Public Monitor</title>
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
    <link href="magicscroll-trial/magicscroll/magicscroll.css" rel="stylesheet" />
    <script src="magicscroll-trial/magicscroll/magicscroll.js"></script>
     <script type="text/javascript">
        $(document).ready(function () {
            var windowHeight = $(window).height();
            var hidden = document.getElementById('hidValue');
            hidden.value = windowHeight;
        });
    </script>
    <style>
        .circle1{
            width: 70px;
    height: 70px;
    /* background: Lightgrey; */
    -moz-border-radius: 160px;
    -webkit-border-radius: 160px;
    border-radius: 160px;
    margin:10px;
    object-fit: cover;
    overflow: hidden;
    border: 1px solid green;
        }
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
        .modalBackground {
            background-color: gray;
            opacity: 0.7;
        }

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
    <style>
        a {
            display: none;
        }
    </style>

    <style>
        div#threadPostDiv {
            margin-bottom: 10px;
            background-color: #CCFFFF; /*this doesnt work*/
            display: block;
        }


        div#threadPostLeftDiv {
            display: block;
            margin: 10px;
            padding: 5px;
            width: 20%;
            float: left;
            height: auto;
        }

        div#threadPostHeaderDiv {
            margin-bottom: 15px;
            display: block;
        }

        div#threadPostTitleDiv {
            font-weight: bold; /*this works*/
            display: block;
            font-style: italic; /*this works*/
        }

        div#threadPostRightDiv {
            border: 1px solid #FFFFFF; /*this doesn't works*/
            display: block;
            /*width: 70%;*/
            float: right;
            height: auto;
            margin: 10px;
            padding: 5px;
            width: 180px;
        }

        .rectangle {
            background-color: white;
            width: 600px;
            height: 266px;
            border: 3px solid black;
            padding: 50px;
            margin: 20px;
        }

        .rectanglecanteen {
            background-color: white;
            width: 300px;
            height: 200px;
            border: 1px solid gray;
            padding: 50px;
            margin: 20px;
        }

        .rectanglecanteen {
            background-color: white;
            width: 240px;
            height: 200px;
            border: 1px solid gray;
            padding: 50px;
            margin: 20px;
        }

        .gridcanteen {
            margin-left: 72px !important;
            margin-bottom: 0px !important;
            margin-right: -212px !important;
            margin-top: -65px !important;
        }
    </style>
    <style>
         html, body {
			padding: 0;
			margin: 0;
		 	overflow: hidden;
		}
        
    </style>


</head>

<body runat="server" id="body" class="page-sidebar-closed" style="background-color: white!important">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="scriptmanager1" runat="server">
        </asp:ScriptManager>
        <asp:HiddenField ID="hidValue" Value="" runat="server" />
        
                <br />
                <div class="page-content-wrapper" style="margin-top: -60px; margin-left: -18px">
                    <div class="page-content">
                        <div class="row">
                            <div class='col-sm-4'>
                                <div class="form-group" style="margin-left: 17px">
                                    <%--<h3>Canteen Public Monitor   
                                    </h3>--%>
                                </div>
                            </div>

                        </div>
                        
                        <div class="modal-content" id="divView" runat="server">
                            <div class="modal-body" style="padding: 0px; background-color: lightgray">
                                <div class="portlet-body form" id="divmain" runat="server">
                                    <div class="form-body">
                                        <asp:UpdatePanel ID="UpdateRefresh" UpdateMode="Conditional" runat="server">
            <ContentTemplate>
                <asp:Timer ID="Timer1" OnTick="Timer1_Tick" runat="server" ></asp:Timer>
                                        <div class="row" id="divgrid" runat="server">
                                            <div class="col-lg-3">
                                                <div class="dashboard-stat white" style="height: 270px;border-top-left-radius: 20px!important;border-top-right-radius: 20px!important;">
                                                    <div style="margin-top: 20px">
                                                        <div style="font-size: 30px; margin-left: 60px;">Canteen Info</div>
                                                        <hr />

                                                        <div style="margin-left: 23px">
                                                            <i class="glyphicon glyphicon-cutlery" style="font-size:20px"></i> 
                                                            <asp:Label ID="lblCanteenName" runat="server" Style="font-size: 20px"></asp:Label><br />
                                                            <br />
                                                            <i class="glyphicon glyphicon-bed" style="font-size:20px"></i>
                                                            <asp:Label ID="lblCanteenType" runat="server" Style="font-size: 20px"></asp:Label><br />
                                                            <br />
                                                            <i class="glyphicon glyphicon-time" style="font-size:20px"></i>
                                                            <asp:Label ID="lblCanteenFromTime" runat="server" Style="font-size: 20px"></asp:Label>
                                                            <b>To </b>
                                                            <i class="glyphicon glyphicon-time" style="font-size:20px"></i>
                                                            <asp:Label ID="lblCanteenToTime" runat="server" Style="font-size: 20px"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div id="divstudent" runat="server" visible="false">
                                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="margin-left: -20px">
                                                        <div class="dashboard-stat white" style="height: 270px;width:600px;border-style: solid;border-color:black;border-top-left-radius: 20px!important;border-top-right-radius: 20px!important;">
                                                            <div style="margin-top: 5px;">
                                                                <div class="col-lg-6 col-md-12 col-sm-12 col-xs-12">
                                                                    <asp:Image ID="tmgstudent_S" runat="server" class="circle" /><br />
                                                                    <asp:Label ID="lblgrantmsg" runat="server"></asp:Label>
                                                                </div>
                                                                <div class="col-lg-1 col-md-12 col-sm-12 col-xs-12"></div>
                                                                <div class="col-lg-5 col-md-12 col-sm-12 col-xs-12" style="margin-left: -80px; margin-top: 35px">
                                                                    <asp:Label ID="lblStudentId_S" runat="server" Style="font-size: 20px"></asp:Label><br />
                                                                    <asp:Label ID="lblStudent_Name_S" runat="server" Style="font-size: 18px"></asp:Label><br />
                                                                    <asp:Label ID="lblPunch_Datetime_S" runat="server" Style="font-size: 18px"></asp:Label><br />
                                                                    <asp:Label ID="lblDept_S" runat="server" Style="font-size: 18px"></asp:Label><br />
                                                                     <asp:Label ID="lblCardnumber_S" runat="server" Style="font-size: 18px"></asp:Label>
                                                                </div>


                                                            </div>
                                                        </div>
                                                    </div>


                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                
                                                <asp:Label ID="lblstatus" runat="server"></asp:Label>
                                                <br />
                                                <asp:Label ID="lblallows" runat="server"></asp:Label>
                                            </div>
                                        </div>
                </ContentTemplate>
            <Triggers>
                <%--<asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />--%>
            </Triggers>
        </asp:UpdatePanel>

                                        <div class="row">
                                             <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
            <ContentTemplate>
                <asp:Timer ID="Timer2" OnTick="Timer1_Tick" runat="server" ></asp:Timer>
                                            <div class="col-lg-3">
                                                <div class="dashboard-stat white" style="height: 270px;border-top-left-radius: 20px!important;border-top-right-radius: 20px!important;">
                                                    <div style="margin-top: 20px">
                                                        <div style="font-size: 30px; margin-left: 60px">Access Count</div>
                                                        <hr />
                                                        <div style="margin-left: 17px">
                                                            <div class="col-lg-6 col-md-12 col-sm-12 col-xs-12">
                                                                <asp:Label ID="lblDenied" runat="server" Style="font-size: 20px;margin-left:17px;position:relative;"></asp:Label><br />
                                                                <b>Denied</b><br />
                                                                <br />
                                                                <asp:Label ID="lblPending" runat="server" Style="font-size: 20px;margin-left:17px;position:relative;"></asp:Label><br />
                                                                <b>Pending</b>
                                                            </div>
                                                            <div class="col-lg-6 col-md-12 col-sm-12 col-xs-12">
                                                                <asp:Label ID="lblServed" runat="server" Style="font-size: 20px;margin-left:17px;position:relative;"></asp:Label><br />
                                                                <b>Served</b><br />
                                                                <br />
                                                                <asp:Label ID="lblTotal" runat="server" Style="font-size: 20px;margin-left:17px;position:relative;"></asp:Label><br />
                                                                <b style="margin-left:7px">Total</b>
                                                            </div>

                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            </ContentTemplate>
            <Triggers>
            </Triggers>
        </asp:UpdatePanel>
                                            <div class="col-md-6" style="margin-top: -16px">
                                                <div id="divVideo" runat="server" style="display: none"></div>
                                                <%--<video  id="videoPlayer" width="570" height="300" src="Food/Food Safety and Coronavirus (COVID-19) Updated 4-14-20_HD.mp4" controls autoplay onplay >--%>
                                                <video id="myvideo" runat="server" width="600" height="300" controls="controls" style="background: black">
                                                    <%--<source class="active" src="http://www.w3schools.com/html/mov_bbb.mp4" type="video/mp4"  />
  <source src="http://www.w3schools.com/html/movie.mp4" type="video/mp4" />
                                               <source  src="http://www.w3schools.com/html/mov_bbb.mp4" type="video/mp4"  />--%>
                                                </video>
                                            </div>
                                            <div class="col-md-3" style="margin-top: -213px; position: relative;">
                                                <div class="dashboard-stat white" style="height: 500px; border-radius: 0px!important; position: relative;">
                                                    <div class="col-md-1"></div>
                                                     <div class="col-md-10">
                                                         
<%--                                                         <div class="dashboard-stat gray" style="background-color:lightgray">
                                                             <img src="Food/Food.jpg" width="20" height="20" class="circle1">&nbsp;&nbsp;&nbsp;<span style="font-size:25px">Samosa</span>
                                                             </div>
                                                         --%>
                                                    <marquee behavior="scroll" direction="up" runat="server" id="ImagesDiv">  </marquee></div>

                                                </div>
                                            </div>
                                        </div>
                                        <marquee behavior="scroll" direction="left" runat="server" id="DivQuote" style="background-color: lightgray"> </marquee>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div>

                    <asp:LinkButton ID="lnkDummy" ForeColor="#ffffff" runat="server"></asp:LinkButton>
                    <cc1:ModalPopupExtender ID="ModalPopupExtender1" BehaviorID="mpe" runat="server"
                        PopupControlID="pnlPopup" TargetControlID="lnkDummy" BackgroundCssClass="modalBackground" CancelControlID="btnHide">
                    </cc1:ModalPopupExtender>
                    <asp:Panel ID="pnlPopup" runat="server" CssClass="modalPopup" Style="display: none; background: #F7F7F7 url('../Images/body-bg.png') repeat scroll 0% 0%;">
                        <div class="modal-content" id="div1" runat="server" style="background-color: #fff">
                            <div class="modal-body">
                                <div class="portlet-body form">
                                    <div class="form-body" style="width: 500px; height: 250px; position: relative;">
                                        <div class="row">
                                            <div class="col-lg-12">
                                                <div class="modal-header">
                                                    <h3>&nbsp;Canteen public Monitor</h3>
                                                    <asp:ImageButton ID="ibtnclose" runat="server" Style="float: right; margin-top: -30px;" ImageUrl="~/Images/del.png"
                                                        OnClick="ibtnclose_Click"></asp:ImageButton>
                                                </div>

                                                <div class="modal-body">
                                                    <b>Your Session has Expired.</b>
                                                    <br />
                                                </div>

                                                <div class="modal-footer">
                                                    <asp:Button ID="btnHide" CssClass="btn btn-primary" runat="server" Text="Ok" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                </div>

                <script type="text/javascript">
                                       
                    var myvid = document.getElementById('myvideo');
                    if (myvid) {
                        
                        myvid.addEventListener('ended', function (e) {
                            // get the active source and the next video source.
                            // I set it so if there's no next, it loops to the first one
                            var activesource = document.querySelector("#myvideo source.active");
                            var nextsource = document.querySelector("#myvideo source.active + source") || document.querySelector("#myvideo source:first-child");

                            // deactivate current source, and activate next one
                            activesource.className = "";
                            nextsource.className = "active";
                           
                            // update the video source and play
                            myvid.src = nextsource.src;

                            myvid.play();
                        });
                    }
                </script>
            

    </form>
    <br />


</body>

<!-- BEGIN CORE PLUGINS -->

<script src="assets/backend/global/plugins/bootstrap/js/bootstrap.min.js" type="text/javascript"></script>
<script src="assets/backend/global/plugins/js.cookie.min.js" type="text/javascript"></script>
<script src="assets/backend/global/plugins/bootstrap-hover-dropdown/bootstrap-hover-dropdown.min.js" type="text/javascript"></script>
<script src="assets/backend/global/plugins/jquery-slimscroll/jquery.slimscroll.min.js" type="text/javascript"></script>
<script src="assets/backend/global/plugins/jquery.blockui.min.js" type="text/javascript"></script>
<script src="assets/backend/global/plugins/uniform/jquery.uniform.min.js" type="text/javascript"></script>
<script src="assets/backend/global/plugins/bootstrap-switch/js/bootstrap-switch.min.js" type="text/javascript"></script>
<!-- END CORE PLUGINS -->
<!-- BEGIN PAGE LEVEL PLUGINS -->


<script src="assets/backend/global/plugins/bootstrap-daterangepicker/moment.min.js" type="text/javascript"></script>
<script src="assets/backend/global/plugins/bootstrap-daterangepicker/daterangepicker.js" type="text/javascript"></script>
<script src="assets/backend/global/plugins/morris/morris.min.js" type="text/javascript"></script>
<%--<script src="assets/backend/global/plugins/morris/morris.min.js"></script>--%>
<script src="assets/backend/global/plugins/morris/raphael-min.js" type="text/javascript"></script>

<!-- END PAGE LEVEL PLUGINS -->
<!-- BEGIN THEME GLOBAL SCRIPTS -->
<script src="assets/backend/js/app.min.js" type="text/javascript"></script>
<!-- END THEME GLOBAL SCRIPTS -->

<!-- BEGIN PAGE LEVEL SCRIPTS -->
<script src="assets/backend/js/dashboard.min.js" type="text/javascript"></script>
<!-- END PAGE LEVEL SCRIPTS -->
<!-- BEGIN THEME LAYOUT SCRIPTS -->
<script src="assets/backend/js/layout.min.js" type="text/javascript"></script>
<script src="assets/backend/js/demo.min.js" type="text/javascript"></script>
<script src="assets/backend/js/quick-sidebar.min.js" type="text/javascript"></script>
<!-- END THEME LAYOUT SCRIPTS -->
<!--custom script register-->
<script src="assets/backend/custom/js/p-loading.min.js"></script>
<script src="assets/backend/custom/js/jquery.uploadPreview.min.js"></script>
<script src="assets/backend/custom/js/bootstrap-multiselect.js"></script>
<script src="assets/backend/custom/js/sweetalert.min.js"></script>
<script src="assets/backend/custom/js/parsley.min.js"></script>
<!--data table export script-->
<script type="text/javascript" src="assets/backend/nicEdit.js"></script>
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
</html>

