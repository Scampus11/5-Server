<%@ Page Title="" Language="C#" MasterPageFile="~/Admin_Master.Master" AutoEventWireup="true" CodeBehind="ProcterAccessLogs.aspx.cs" Inherits="SMS.ProcterAccessLogs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Procter Access Logs</title>
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
            /*border: 1px solid #FFFFFF;*/ /*this doesn't works*/
            display: block;
            /*width: 70%;*/
            float: right;
            height: auto;
            /*margin: 10px;
            padding: 5px;*/
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

        .modalBackground {
            background-color: gray;
            opacity: 0.7;
        }
    </style>
    <style>
        /*html, body {
            padding: 0;
            margin: 0;
            overflow: hidden;
        }*/

        .dashboard-stat {
            border-radius: 20px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdateRefresh" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <br />
            <br />
            <asp:HiddenField ID="hidValue" Value="" runat="server" />
            <div class="page-content-wrapper" style="margin-bottom: -16px;">
                <div class="page-content">
                    <h3 class="page-title">PROCTER ACCESS LOGS</h3>
                    <div class="row">
                        <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                            <div class="form-group">
                                <asp:DropDownList ID="ddlBGList" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                                <asp:Label ID="lblBGList" runat="server" Visible="false" Text="* Select Block Group" Style="color: red;"></asp:Label>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                            <div class="form-group">
                                <div class='input-group date' id='Fromdatetimepicker'>
                                    <asp:TextBox ID="txtfromDate" runat="server" placeholder="From Date" CssClass="form-control"></asp:TextBox>
                                    <span class="input-group-addon">
                                        <span class="glyphicon glyphicon-calendar"></span>
                                    </span>
                                </div>
                                <asp:Label ID="lblFromDate" runat="server" Visible="false" Text="* Select From Date" Style="color: red;"></asp:Label>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                            <div class="form-group">
                                <div class='input-group date' id='Todatetimepicker'>
                                    <asp:TextBox ID="txtToDate" runat="server" placeholder="To Date" CssClass="form-control"></asp:TextBox>
                                    <span class="input-group-addon">
                                        <span class="glyphicon glyphicon-calendar"></span>
                                    </span>
                                </div>
                                <asp:Label ID="lblToDate" runat="server" Visible="false" Text="* Select To Date" Style="color: red;"></asp:Label>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                            <div class="form-group">
                                <asp:LinkButton ID="lnkGo" runat="server" Text="Go" class="btn green-meadow" OnClick="lnkGo_Click" OnClientClick="CheckValidation();" />
                            </div>
                        </div>
                    </div>
                    <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Always" runat="server" ChildrenAsTriggers="true">
                        <ContentTemplate>
                            <asp:Timer ID="Timer1" OnTick="Timer1_Tick" runat="server"></asp:Timer>
                            <div class="row">
                                <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                                    <div class="dashboard-stat green">
                                        <div class="visual">
                                            <i class="fa fa-shopping-cart"></i>
                                        </div>
                                        <div class="details">
                                            <div class="number">
                                                <span data-counter="counterup" data-value="3,353.00">
                                                    <asp:Label ID="lblCheckInCount" runat="server" Text="0"></asp:Label>
                                                </span>
                                            </div>
                                            <div class="desc">Check In</div>
                                        </div>
                                        <a runat="server" class="more" href="#" target="_blank" id="aCheckIn">View Details
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
                                                <span data-counter="counterup" data-value="3,353.00">
                                                    <asp:Label ID="lblCheckOutCount" runat="server" Text="0"></asp:Label>
                                                </span>
                                            </div>
                                            <div class="desc">Check Out</div>
                                        </div>
                                        <a class="more" id="aCheckOut" runat="server" href="#" target="_blank">View Details
                            <i class="m-icon-swapright m-icon-white"></i>
                                        </a>
                                    </div>
                                </div>
                            </div>
                            <div class="clearfix"></div>
                            <div class="row">
                                <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                                </div>

                                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                    <asp:GridView ID="GrdBG" runat="server" class="table table-striped table-bordered table-hover"
                                        AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataText="No records Found" ShowHeader="false"
                                        OnRowDataBound="GrdBG_RowDataBound">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <div id="threadPostDiv">
                                                        <div id="threadPostLeftDiv">
                                                            <asp:Label ID="lblpath" Visible="false" runat="server" Text='<%#Eval("StudentImages") %>'></asp:Label>
                                                            <asp:Image ID="imgStudent" runat="server" Visible="false" ImageUrl='<%#Eval("StudentImages") %>' class="circle" />
                                                            <asp:Image ID="imgdefault" runat="server" Visible="false" ImageUrl="~/Images/images1.jpg" class="circle" Style="width: 100px!important;" />
                                                            <div id="threadPostRightDiv" class="gridcanteen">
                                                                <div style="word-wrap: break-word;">
                                                                    <div>
                                                                        <asp:Label ID="lblStudentId" runat="server" Text='<%#Eval("Student_Id") %>'></asp:Label>
                                                                    </div>
                                                                    <asp:Label ID="lblID" runat="server" Visible="false" Text='<%#Eval("Id") %>'></asp:Label>
                                                                    <asp:Label ID="lblStudentName" runat="server" Text='<%#Eval("Student_Name") %>'></asp:Label>
                                                                    <asp:Label ID="lblAccess" runat="server" Visible="false" Text='<%#Eval("Access_Code") %>'></asp:Label>
                                                                </div>
                                                                <asp:Label ID="lblAccessDatetime" runat="server" Text='<%#Eval("Access_Datetime") %>'></asp:Label>
                                                                <div>
                                                                    <asp:Label ID="lblBGName" runat="server" Text='<%#Eval("BGName") %>'></asp:Label>
                                                                </div>
                                                                <div>
                                                                    <asp:Label ID="lblCardnumber" runat="server" Text='<%#Eval("Card_Number") %>'></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                        </Triggers>
                    </asp:UpdatePanel>
                    <div class="clearfix"></div>
                    <h3 class="page-title">MUSTURING ACCESS LOGS</h3>
                    <div class="row">
                        <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                            <div class="form-group">
                                <asp:DropDownList ID="ddlBGList2" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                                <asp:Label ID="lblBGList2" runat="server" Visible="false" Text="* Select Block Group" Style="color: red;"></asp:Label>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                            <div class="form-group">
                                <asp:LinkButton ID="lnkGo2" runat="server" Text="Go" OnClick="lnkGo2_Click" class="btn green-meadow" />
                            </div>
                        </div>
                    </div>
                    <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Always" runat="server" ChildrenAsTriggers="true">
                        <ContentTemplate>
                            <asp:Timer ID="Timer2" OnTick="Timer1_Tick" runat="server"></asp:Timer>
                            <div class="row">
                                <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                                    <div class="dashboard-stat yellow">
                                        <div class="visual">
                                            <i class="fa fa-shopping-cart"></i>
                                        </div>
                                        <div class="details">
                                            <div class="number">
                                                <span data-counter="counterup" data-value="3,353.00">
                                                    <asp:Label ID="lblTotal" runat="server" Text="0"></asp:Label>
                                                </span>
                                            </div>
                                            <div class="desc">Total</div>
                                        </div>
                                        <a class="more" id="aTotal" runat="server" target="_blank" href="#">View Details
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
                                                <span data-counter="counterup" data-value="3,353.00">
                                                    <asp:Label ID="lblMusteringCheckInCount" runat="server" Text="0"></asp:Label>
                                                </span>
                                            </div>
                                            <div class="desc">Check In</div>
                                        </div>
                                        <a class="more" id="aMusteringCheckIn" runat="server" target="_blank">View Details
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
                                                <span data-counter="counterup" data-value="3,353.00">
                                                    <asp:Label ID="lblMusteringCheckOutCount" runat="server" Text="0"></asp:Label>
                                                </span>
                                            </div>
                                            <div class="desc">Check Out</div>
                                        </div>
                                        <a class="more" id="aMusteringCheckOut" runat="server" target="_blank">View Details
                            <i class="m-icon-swapright m-icon-white"></i>
                                        </a>
                                    </div>
                                </div>
                                <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                                    <div class="dashboard-stat blue">
                                        <div class="visual">
                                            <i class="fa fa-globe"></i>
                                        </div>
                                        <div class="details">
                                            <div class="number">
                                                <span data-counter="counterup" data-value="3,353.00">
                                                    <asp:Label ID="lblMusteringCount" runat="server" Text="0"></asp:Label>
                                                </span>
                                            </div>
                                            <div class="desc">Musturing</div>
                                        </div>
                                        <a class="more" id="aMustering" runat="server" target="_blank">View Details
                            <i class="m-icon-swapright m-icon-white"></i>
                                        </a>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                        </Triggers>
                    </asp:UpdatePanel>
                    <div class="clearfix"></div>
                </div>
            </div>
            <script type="text/javascript">
                $(document).ready(function () {
                    $('#Fromdatetimepicker').datetimepicker({ format: 'MM/DD/YYYY' });
                    $('#Todatetimepicker').datetimepicker({ format: 'MM/DD/YYYY' });
                    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
                });
                function EndRequestHandler(sender, args) {
                    SetDatePicker();
                }
                function SetDatePicker() {
                    $('#Fromdatetimepicker').datetimepicker({ format: 'MM/DD/YYYY' });
                    $('#Todatetimepicker').datetimepicker({ format: 'MM/DD/YYYY' });
                }
            </script>

        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="lnkGo" />
            <asp:AsyncPostBackTrigger ControlID="lnkGo2" />
        </Triggers>
    </asp:UpdatePanel>
    <%--<script type="text/javascript">
                $(document).ready(function () {
                    LoadEvent();
                });
                function EndRequestHandler(sender, args) {
                    LoadEvent();
                }
                function LoadEvent() {
                    //var pathname = window.location.pathname; // Returns path only (/path/example.html)
                    //var url = window.location.href;     // Returns full URL (https://example.com/path/example.html)
                    debugger
                    var BGID = 0;
                    var FromDate = '';
                    var ToDate = '';
                    var origin = window.location.origin;   // Returns base URL (https://example.com)
                    if ($("ContentPlaceHolder1_ddlBGList").length != 0)
                        BGID = $("ContentPlaceHolder1_ddlBGList").val();
                    if ($("ContentPlaceHolder1_txtfromDate").length != 0)
                        BGID = $("ContentPlaceHolder1_txtfromDate").val();
                    if ($("ContentPlaceHolder1_ToDate").length != 0)
                        BGID = $("ContentPlaceHolder1_ToDate").val();

                    //$("#aCheckIn").attr("href"
                    //    , origin + "/ProcterAccessLogsList.aspx?Flag=Mustering&&BGId=1");
                    $("#aCheckIn").attr("href"
                        , origin + "/ProcterAccessLogsList.aspx?Flag=CheckIn&&BGId=" +
                        BGID + "&&FromDate=" + FromDate + "&&ToDate=" + ToDate);
                    $("#aCheckOut").attr("href"
                        , origin + "/ProcterAccessLogsList.aspx?Flag=CheckOut&&BGId=" +
                        BGID + "&&FromDate=" + FromDate + "&&ToDate=" + ToDate);
        }
        function CheckValidation() {
            LoadEvent();
        }
            </script>--%>
</asp:Content>
