<%@ Page Title="" Language="C#" MasterPageFile="~/Admin_Master.Master" AutoEventWireup="true" CodeBehind="Supervisor_Canteen_Monitor.aspx.cs" Inherits="SMS.Supervisor_Canteen_Monitor" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>SuperVisor Canteen Monitor</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            var windowHeight = $(window).height();
            var hidden = document.getElementById('ContentPlaceHolder1_hidValue');
            hidden.value = windowHeight - 46 - 50;
        });
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#datetimepicker1').datetimepicker({ format: 'DD/MM/YYYY' });
            $('#datetimepicker3').datetimepicker({
                format: 'LT'
            });
            $('#datetimepicker2').datetimepicker({
                format: 'LT'
            });
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
        });
        function EndRequestHandler(sender, args) {

            SetDatePicker();
        }
        function SetDatePicker() {
            $('#datetimepicker1').datetimepicker({ format: 'DD/MM/YYYY' });

        }

        $(function () {
            $('#datetimepicker3').datetimepicker({
                format: 'LT'
            });
        });
        $(function () {
            $('#datetimepicker2').datetimepicker({
                format: 'LT'
            });
        });

    </script>
    <script type="text/javascript">
        function pageLoad(sender, args) {
            $('#datetimepicker2').datetimepicker({ format: 'LT' });
            $('#datetimepicker3').datetimepicker({ format: 'LT' });
        }
    </script>
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
        html, body {
			padding: 0;
			margin: 0;
		 	overflow: hidden;
		}


        .dashboard-stat {
            border-radius: 20px;
        }
    </style>

    <asp:UpdatePanel ID="UpdateRefresh" UpdateMode="Always" runat="server" ChildrenAsTriggers="true">
        <ContentTemplate>
            
            <asp:Timer ID="Timer1" OnTick="Timer1_Tick" runat="server"></asp:Timer>
            
            <div class="page-content-wrapper">
                <asp:HiddenField ID="hidValue" Value="" runat="server" />
                 <asp:HiddenField ID="hidCanteenId" Value="" runat="server" />
                 <asp:HiddenField ID="hidSessionId" Value="" runat="server" />
                <asp:HiddenField ID="hidCurrentDate" Value="" runat="server" />
                 <asp:HiddenField ID="hidStartTime" Value="" runat="server" />
                 <asp:HiddenField ID="hidEndTime" Value="" runat="server" />
                 <asp:HiddenField ID="hidCanteenName" Value="" runat="server" />
                 <asp:HiddenField ID="hidSessionName" Value="" runat="server" />
                <div class="page-content">

                    <div class="row" id="divgrid" runat="server">
                        <div class="col-md-3">
                            <div class="portlet-body">
                                <h3>Accessed Students</h3>
                                <div id="grid1" runat="server">
                                    <asp:GridView ID="gridEmployee" runat="server" class="table table-striped table-bordered table-hover"
                                        AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataText="No records Found" ShowHeader="false" OnRowDataBound="gridEmployee_RowDataBound" OnSelectedIndexChanged="gridEmployee_SelectedIndexChanged">
                                        <%--<PagerSettings Mode="NumericFirstLast" PageButtonCount="10" FirstPageText="First" LastPageText="Last" />AllowPaging="true" AllowSorting="true" OnSorting="OnSorting" OnPageIndexChanging="OnPageIndexChanging"
                                    PageSize="5" 
                                    <PagerStyle CssClass="gridview"></PagerStyle>--%>
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <div id="threadPostDiv">
                                                        <div id="threadPostLeftDiv">
                                                            <asp:Image ID="tmgstudent" runat="server" ImageUrl='<%#Eval("StudentImages")==""?"~/Images/images1.jpg":Eval("StudentImages") %>'
                                                                class="circle" />

                                                            <div id="threadPostRightDiv" class="gridcanteen">
                                                                <div style="word-wrap: break-word;">
                                                                    <div>
                                                                        <asp:LinkButton ID="lblStudentId" runat="server" Visible="false" Text='<%#Eval("StudentId") %>' OnClick="lblStudentId_Click"></asp:LinkButton>
                                                                        <asp:Label ID="lblStudentId1" runat="server" Text='<%#Eval("StudentId") %>'></asp:Label>
                                                                    </div>
                                                                    <asp:Label ID="lblID" runat="server" Visible="false" Text='<%#Eval("Id") %>'></asp:Label>
                                                                    <asp:Label ID="lblStudent_Name" runat="server" Text='<%#Eval("Student_Name") %>'></asp:Label>
                                                                    <asp:Label ID="lblAccess" runat="server" Visible="false" Text='<%#Eval("Access_Code") %>'></asp:Label>
                                                                </div>
                                                                <asp:Label ID="lblPunch_Datetime" runat="server" Text='<%#Eval("Punch_Datetime") %>'></asp:Label>
                                                                <div>
                                                                    <asp:Label ID="lblDept" runat="server" Text='<%#Eval("Department") %>'></asp:Label>
                                                                </div>
                                                                <div>
                                                                    <asp:Label ID="lblCardnumber" runat="server" Text='<%#Eval("Cardnumber") %>'></asp:Label>
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
                        </div>
                        <div class="col-md-6">
                            <div class="col-md-9">
                                <h2><b style="color:#cf7303">SuperVisor Canteen Monitor</b></h2>
                            </div>
                            <div class="col-md-3" style="margin-top:18px">
                                <asp:LinkButton ID="lnkAdd" runat="server" ToolTip="Filter" class="btn green-meadow " OnClick="lnkAdd_Click"><i class="fa fa-filter" style="margin-top:3px;font-size:20px"></i> </asp:LinkButton>
                                <asp:LinkButton ID="lnkCopy" runat="server" ToolTip="Canteen Url" class="btn green-meadow" OnClick="lnkCopy_Click" Visible="false"> <i class="fa fa-link" style="margin-top:3px;font-size:20px"></i>  </asp:LinkButton>
                            </div>

                            <div id="divstudent" runat="server" visible="false">
                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                    <div class="dashboard-stat grey" style="height: 280px">
                                        <div style="margin-top: 5px;">
                                            <div class="col-lg-6 col-md-12 col-sm-12 col-xs-12">
                                                <asp:Image ID="tmgstudent_S" runat="server" class="circle" /><br />
                                                <asp:Label ID="lblgrantmsg" runat="server"></asp:Label>
                                            </div>
                                            <div class="col-lg-1 col-md-12 col-sm-12 col-xs-12"></div>
                                            <div class="col-lg-5 col-md-12 col-sm-12 col-xs-12" style="margin-left: -80px; margin-top: 35px; position: relative">
                                                <asp:Label ID="lblStudentId_S" runat="server" Style="font-size: 20px"></asp:Label><br />
                                                <asp:Label ID="lblStudent_Name_S" runat="server" Style="font-size: 18px"></asp:Label><br />
                                                <asp:Label ID="lblPunch_Datetime_S" runat="server" Style="font-size: 18px"></asp:Label><br />
                                                <asp:Label ID="lblDept_S" runat="server" Style="font-size: 18px"></asp:Label><br />
                                                <asp:Label ID="lblCardnumber_S" runat="server" Style="font-size: 18px"></asp:Label>
                                                                
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="clearfix"></div>
                                <div>
                                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">

                                        <div class="dashboard-stat grey" style="height: 240px">
                                            <div style="margin-top: 20px">
                                                <div style="font-size: 30px; margin-left: 60px;">Canteen Info</div>
                                                <hr style="color: black" />

                                                <div style="margin-left: 23px">
                                                    <i class="glyphicon glyphicon-cutlery" style="font-size: 20px"></i>
                                                    <asp:Label ID="lblCanteenName" runat="server" Style="font-size: 20px"></asp:Label><br />
                                                    <br />
                                                    <i class="glyphicon glyphicon-bed" style="font-size: 20px"></i>
                                                    <asp:Label ID="lblCanteenType" runat="server" Style="font-size: 20px"></asp:Label><br />
                                                    <br />
                                                    <i class="glyphicon glyphicon-time" style="font-size: 20px"></i>
                                                    <asp:Label ID="lblCanteenFromTime" runat="server" Style="font-size: 20px"></asp:Label>
                                                    <b>To </b>
                                                    <i class="glyphicon glyphicon-time" style="font-size: 20px"></i>
                                                    <asp:Label ID="lblCanteenToTime" runat="server" Style="font-size: 20px"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                        <div class="dashboard-stat grey" style="height: 240px">
                                            <div style="margin-top: 20px">
                                                <div style="font-size: 30px; margin-left: 50px">Access Count</div>
                                                <hr style="color: black" />

                                                <div style="margin-left: 17px">
                                                    <div class="col-lg-6 col-md-12 col-sm-12 col-xs-12">
                                                        <asp:Label ID="lblDenied" runat="server" Style="font-size: 20px; margin-left: 17px; position: relative;"></asp:Label><br />
                                                        <b>Denied</b><br />
                                                        <br />
                                                        <asp:Label ID="lblPending" runat="server" Style="font-size: 20px; margin-left: 17px; position: relative;"></asp:Label><br />
                                                        <b>Pending</b>
                                                    </div>
                                                    <div class="col-lg-6 col-md-12 col-sm-12 col-xs-12">
                                                        <asp:Label ID="lblServed" runat="server" Style="font-size: 20px; margin-left: 17px; position: relative;"></asp:Label><br />
                                                        <b>Served</b><br />
                                                        <br />
                                                        <asp:Label ID="lblTotal" runat="server" Style="font-size: 20px; margin-left: 17px; position: relative;"></asp:Label><br />
                                                        <b style="margin-left: 7px">Total</b>
                                                    </div>

                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="portlet-body">
                                <h3>WatchList Students</h3>
                                <div id="grid2" runat="server">
                                    <asp:GridView ID="GridView1" runat="server" class="table table-striped table-bordered table-hover"
                                        AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataText="No records Found" ShowHeader="false"
                                        OnRowDataBound="GridView1_RowDataBound" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
                                        <%-- <PagerSettings Mode="NumericFirstLast" PageButtonCount="10" FirstPageText="First" LastPageText="Last" />AllowPaging="true" AllowSorting="true" OnSorting="OnSorting1"
                                    OnPageIndexChanging="OnPageIndexChanging1" PageSize="5"
                                    <PagerStyle CssClass="gridview"></PagerStyle>--%>
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-Width="10px" ItemStyle-Width="10px" HeaderStyle-HorizontalAlign="Center" HeaderText="" SortExpression="Id">
                                                <ItemTemplate>
                                                    <div id="threadPostDiv">
                                                        <div id="threadPostLeftDiv">
                                                            <asp:Image ID="tmgstudent" runat="server" ImageUrl='<%#Eval("StudentImages")==""?"~/Images/images1.jpg":Eval("StudentImages") %>'
                                                                class="circle" Style="width: 65px; height: 65px; border: 5px solid red !important" />

                                                            <div id="threadPostRightDiv" class="gridcanteen">
                                                                <div>
                                                                    <div>
                                                                        <asp:LinkButton ID="lblStudentId" runat="server" Visible="false" Text='<%#Eval("StudentId") %>' OnClick="lblStudentId_Click"></asp:LinkButton>
                                                                        <asp:Label ID="lblStudentId1" runat="server" Text='<%#Eval("StudentId") %>'></asp:Label>
                                                                    </div>
                                                                    <asp:Label ID="lblID" runat="server" Visible="false" Text='<%#Eval("Id") %>'></asp:Label>
                                                                    <asp:Label ID="lblStudent_Name" runat="server" Text='<%#Eval("Student_Name") %>'></asp:Label>
                                                                </div>
                                                                <asp:Label ID="lblPunch_Datetime" runat="server" Text='<%#Eval("Punch_Datetime", "{0:dd, MMM yyyy}") %>'></asp:Label>
                                                                <div>
                                                                    <asp:Label ID="lblDept" runat="server" Text='<%#Eval("Department") %>'></asp:Label>
                                                                </div>
                                                                <div>
                                                                    <asp:Label ID="lblCardnumber" runat="server" Text='<%#Eval("Cardnumber") %>'></asp:Label>
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
                                <div class="form-body">
                                    <div class="row" style="margin-top: -50px">
                                        <div class="col-lg-12">
                                            <div class="modal-header">
                                                
                                                <h3>&nbsp;Canteen public Monitor Link
                                                 
                                                <asp:LinkButton ID="lnkCloseicon"  runat="server" ToolTip="Close" OnClick="lnkCloseicon_Click" Style="margin-left:110px" ><i class="fa fa-close" style="font-size:25px;color:red" ></i></asp:LinkButton>
                                                   </h3>
                                            </div>

                                            <div class="modal-body">
                                                <asp:TextBox ID="txturl" runat="server" Style="width: 425px; height: 35px; border: 0px; background-color: #f1f3f4; color: #5f6368;"></asp:TextBox>
                                                <a href="javascript:void(0)" onclick="myFunction()"><i class="fa fa-copy" style="font-size: larger"></i></a>
                                                <asp:Button ID="btnHide" CssClass="btn btn-primary" runat="server" Text="Close" Style="display: none" />

                                            </div>

                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
            </div>
            <h3><a id="aPublic" target="_blank" runat="server" style="display: none"></a></h3>
            <script>

                function myFunction() {

                    /* Get the text field */
                    var copyText = document.getElementById("ContentPlaceHolder1_txturl");

                    /* Select the text field */
                    copyText.select();
                    copyText.setSelectionRange(0, 99999); /*For mobile devices*/

                    /* Copy the text inside the text field */
                    document.execCommand("copy");
                }
            </script>
            
        </ContentTemplate>
        <Triggers>
            <%--<asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />--%>
        </Triggers>
    </asp:UpdatePanel>
    <div>
        <asp:LinkButton ID="LinkButton10" ForeColor="#ffffff" runat="server"></asp:LinkButton>
        <cc1:ModalPopupExtender ID="ModalPopupExtender2" BehaviorID="mpe" runat="server"
            PopupControlID="Panel10" TargetControlID="LinkButton10" BackgroundCssClass="modalBackground" CancelControlID="Button1">
        </cc1:ModalPopupExtender>
        <asp:Panel ID="Panel10" runat="server" CssClass="modalPopup" Style="display: none; left: 84.5px!important; background: #F7F7F7 url('../Images/body-bg.png') repeat scroll 0% 0%;">
            <asp:UpdatePanel ID="UpdatePanel10" UpdateMode="Conditional" runat="server" ChildrenAsTriggers="false">
                <ContentTemplate>
                    <div class="modal-content" id="div2" runat="server" style="background-color: #fff">
                        <div class="modal-body" style="width: 500px!important; position: relative;">
                            <div class="portlet-body form">
                                <div class="form-body">
                                    <div runat="server" id="divAdvanceSearch">
                                        <div class="row">

                                            <div class='col-sm-12 pull-left'>
                                                <div class="form-group">
                                                    <span>Select Canteen : </span>
                                                    <asp:DropDownList ID="ddlCanteen" runat="server" CssClass="form-control"></asp:DropDownList>
                                                    <asp:Label ID="lblValidcanteen" runat="server" Style="color: red;" Text="* Select canteen..." Visible="false"></asp:Label>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class='col-sm-12'>
                                                <div class="form-group">
                                                    <span>Select Session : </span>
                                                    <asp:DropDownList ID="ddlsession" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlsession_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                    <asp:Label ID="lblValidsession" runat="server" Style="color: red;" Text="* Select session..." Visible="false"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class='col-sm-12'>
                                                <span>Session date : </span>
                                                <div class='input-group date' id='datetimepicker1'>

                                                    <asp:TextBox ID="txtDate" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <span class="input-group-addon">
                                                        <span class="glyphicon glyphicon-calendar"></span>
                                                    </span>
                                                </div>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class='col-sm-6'>
                                                <span>Session start time : </span>
                                                <div class="form-group">
                                                    <div class='input-group date' id='datetimepicker2'>
                                                        <asp:TextBox ID="txtStartTime" runat="server" class="form-control"></asp:TextBox>
                                                        <span class="input-group-addon">
                                                            <span class="glyphicon glyphicon-time"></span>
                                                        </span>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class='col-sm-6'>
                                                <span>Session end time : </span>
                                                <div class="form-group">
                                                    <div class='input-group date' id='datetimepicker3'>
                                                        <asp:TextBox ID="txtEndTime" runat="server" class="form-control"></asp:TextBox>
                                                        <span class="input-group-addon">
                                                            <span class="glyphicon glyphicon-time"></span>
                                                        </span>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class='col-sm-8  pull-left'></div>
                                            <div class='col-sm-4  pull-right'>
                                                <asp:LinkButton ID="lnkGo" runat="server" Text="Go" OnClick="lnkGo_Click" class="btn green-meadow" />
                                                <asp:Button ID="Button1" CssClass="btn btn-primary" runat="server" Text="Close" OnClick="Button1_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="lnkGo" />
                    <asp:AsyncPostBackTrigger ControlID="ddlsession" EventName="SelectedIndexChanged" />
                </Triggers>
            </asp:UpdatePanel>
        </asp:Panel>
    </div>
</asp:Content>
