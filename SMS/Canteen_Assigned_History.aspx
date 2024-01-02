<%@ Page Title="" Language="C#" MasterPageFile="~/Admin_Master.Master" AutoEventWireup="true" CodeBehind="Canteen_Assigned_History.aspx.cs" Inherits="SMS.Canteen_Assigned_History" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Student Canteen Assignment </title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="CSS/jquery-ui.css" rel="stylesheet" />
    <script src="JS/jquery-ui.min.js"></script>
    <script src="JS/table2excel.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var windowHeight = $(window).height();
            var hidden = document.getElementById('ContentPlaceHolder1_hidValue');
            hidden.value = windowHeight - 46 - 46 - 25;
        });
    </script>
    <script type="text/javascript">
        function Export() {
            $("#ContentPlaceHolder1_gridEmployee").table2excel({
                filename: "Canteen_Assigned_History.xls"
            });
        }
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#datetimepicker1').datetimepicker({ format: 'MM/DD/YYYY' });
            $('#datetimepicker2').datetimepicker({ format: 'MM/DD/YYYY' });
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
        });
        function EndRequestHandler(sender, args) {
            SetDatePicker();
        }
        function SetDatePicker() {
             $('#datetimepicker1').datetimepicker({ format: 'MM/DD/YYYY' });
            $('#datetimepicker2').datetimepicker({ format: 'MM/DD/YYYY' });
        }
    </script>
    <style>
        /* Clearable text inputs */
        .clearable {
            background: #fff url(http://i.stack.imgur.com/mJotv.gif) no-repeat right -10px center;
            border: 1px solid #999;
            padding: 3px 18px 3px 4px; /* Use the same right padding (18) in jQ! */
            border-radius: 3px;
            transition: background 0.4s;
        }
            .clearable.x {
                background-position: right 5px center;
            }
            /* (jQ) Show icon */
            .clearable.onX {
                cursor: pointer;
            }
            /* (jQ) hover cursor style */
            .clearable::-ms-clear {
                display: none;
                width: 0;
                height: 0;
            }
        /* Remove IE default X */
        html, body {
            padding: 0;
            margin: 0;
            overflow: hidden;
        }
        .modalBackground {
            background-color: gray;
            opacity: 0.7;
        }
        .dashboard-stat {
            border-radius: 20px;
        }
    </style>
    <style>
        .vl {
            border-left: 6px solid white;
            height: 20px;
        }
    </style>
    <asp:UpdatePanel ID="UpdateRefresh" UpdateMode="Always" runat="server" ChildrenAsTriggers="true">
        <ContentTemplate>
            <asp:HiddenField ID="hidValue" Value="" runat="server" />
            <br />
            <br />
            <div class="page-content-wrapper" style="margin-bottom: -16px;">
                <div class="page-content">
                    <div class="row" id="divgrid" runat="server">
                        <div class="col-md-12">
                            <div class="portlet box green-meadow">
                                <div class="portlet-title">
                                    <div class="caption">
                                        <i class='fas'>&#xf2e7;</i>Student Canteen Assignment 
                                    </div>
                                    <div class="tools">
                                        <asp:LinkButton ID="lnkAdd" runat="server" OnClick="lnkAdd_Click" ToolTip="Filter"><i class="fa fa-filter" style="font-size:28px;color:white;"></i> </asp:LinkButton>
                                    </div>
                                    <div class="tools">
                                        <a id="btnExport" onclick="Export()" title="Download Excel" runat="server" visible="false"><i class="fa fa-download" style="font-size: 28px; color: white; margin-top: 4px;"></i></a>
                                    </div>
                                    <div class="tools" style="margin-left: 20px">
                                        <span>Total Count :</span>
                                        <asp:Label ID="lblCount" runat="server" Text="0"></asp:Label>
                                    </div>
                                    <div class="tools pull-left" style="margin-left: 10px; padding: 8px;border-left:0px!important;">
                                        <asp:TextBox ID="txtSearch" runat="server" Width="230px" Height="28px" Style="border: 0px; color: black" />
                                    </div>
                                    <div class="tools pull-left" style="margin-left: 5px; padding: 8px;border-left:0px!important;">
                                        <asp:LinkButton ID="btnsearch" runat="server" OnClick="btnsearch_Click">
                                        <i class="fa fa-search" style="font-size: 28px; color: white; margin-top: 4px;"></i>
                                        </asp:LinkButton>
                                    </div>
                                </div>
                                <asp:LinkButton ID="lnkExcel" runat="server" Visible="false" OnClick="lnkExcel_Click" ToolTip="Excel Download"><i class="fa fa-download" style="font-size:28px;color:white;margin-top:4px;"></i> </asp:LinkButton>
                                <div class="portlet-body" id="grid1" runat="server">
                                    <asp:GridView ID="gridEmployee" runat="server" class="table table-striped table-bordered table-hover" AutoGenerateColumns="false" AllowPaging="true" AllowSorting="true" OnSorting="OnSorting"
                                        OnPageIndexChanging="OnPageIndexChanging" PageSize="5000" ShowHeaderWhenEmpty="true" EmptyDataText="No records Found">
                                        <PagerSettings Mode="NumericFirstLast" PageButtonCount="5" FirstPageText="First" LastPageText="Last" />
                                        <PagerStyle CssClass="gridview"></PagerStyle>
                                        <Columns>
                                            <asp:TemplateField ItemStyle-Width="30px" HeaderText="Student Id" SortExpression="StudentId">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStudentId" runat="server" Text='<%#Eval("StudentId") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="30px" HeaderText="Canteen Name" SortExpression="CanteenName">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCanteenName" runat="server" Text='<%#Eval("CanteenName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="30px" HeaderText="Canteen Type" SortExpression="CanteenType">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCanteenType" runat="server" Text='<%#Eval("CanteenType") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="30px" HeaderText="Assign Date" SortExpression="AssignDate">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAssignDate" runat="server" Text='<%#Eval("AssignDate") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="30px" HeaderText="Start Date" SortExpression="StartDate">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStartDate" runat="server" Text='<%#Eval("StartDate") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="30px" HeaderText="End Date" SortExpression="EndDate">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEndDate" runat="server" Text='<%#Eval("EndDate") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="30px" HeaderText="Card Status" SortExpression="CardStatus">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCardStatus" runat="server" Text='<%#Eval("CardStatus") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="30px" HeaderText="Assign By" SortExpression="AssignBy">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAssignBy" runat="server" Text='<%#Eval("AssignBy") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="30px" HeaderText="Created Datetime" SortExpression="CreatedDatetime">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCreatedDatetime" runat="server" Text='<%#Eval("CreatedDatetime") %>'></asp:Label>
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
        </ContentTemplate>
        <Triggers>
        </Triggers>
    </asp:UpdatePanel>
    <div>
        <asp:LinkButton ID="LinkButton1" ForeColor="#ffffff" runat="server"></asp:LinkButton>
        <cc1:ModalPopupExtender ID="ModalPopupExtender2" BehaviorID="mpe" runat="server"
            PopupControlID="Panel1" TargetControlID="LinkButton1" BackgroundCssClass="modalBackground" CancelControlID="Button1">
        </cc1:ModalPopupExtender>
        <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup" Style="display: none; left: 84.5px!important; background: #F7F7F7 url('../Images/body-bg.png') repeat scroll 0% 0%;">
            <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Always" runat="server" ChildrenAsTriggers="true">
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
                                                    <span>Select Canteen Type : </span>
                                                    <asp:DropDownList ID="ddlcanteenType" runat="server" CssClass="form-control">
                                                        <asp:ListItem Value="0">All</asp:ListItem>
                                                        <asp:ListItem Value="1">In Kind</asp:ListItem>
                                                        <asp:ListItem Value="2">In Cash</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:Label ID="lblValidsession" runat="server" Style="color: red;" Text="* Select session..." Visible="false"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class='col-sm-12'>
                                                <div class="form-group">
                                                    <span>Select Student Id : </span>

                                                    <i id="iconcheck" class="fa fa-check-circle hide" style="color: green" runat="server"></i>
                                                    <asp:TextBox ID="txtStudent" runat="server" CssClass="form-control  clearable" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class='col-sm-6'>
                                                <span>Start date : </span>
                                                <div class='input-group date' id='datetimepicker1'>
                                                    <asp:TextBox ID="txtDate" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <span class="input-group-addon">
                                                        <span class="glyphicon glyphicon-calendar"></span>
                                                    </span>
                                                </div>
                                            </div>
                                            <div class='col-sm-6'>
                                                <span>End date : </span>
                                                <div class='input-group date' id='datetimepicker2'>
                                                    <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <span class="input-group-addon">
                                                        <span class="glyphicon glyphicon-calendar"></span>
                                                    </span>
                                                </div>
                                            </div>
                                        </div>
                                        <br />

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
                    <script type="text/javascript">  
                        $(document).ready(function () {
                            /**
                 * Clearable text inputs
                 */
                            function tog(v) { return v ? 'addClass' : 'removeClass'; }
                            $(document).on('input', '.clearable', function () {
                                $(this)[tog(this.value)]('x');
                            }).on('mousemove', '.x', function (e) {
                                $(this)[tog(this.offsetWidth - 18 < e.clientX - this.getBoundingClientRect().left)]('onX');
                            }).on('touchstart click', '.onX', function (ev) {
                                ev.preventDefault();
                                $(this).removeClass('x onX').val('').change();
                                $("#ContentPlaceHolder1_iconcheck").addClass("hide");
                            });

                            // $('.clearable').trigger("input");
                            // Uncomment the line above if you pre-fill values from LS or server
                            SearchText();
                            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
                        });
                        function EndRequestHandler(sender, args) {
                            $('#datetimepicker1').datetimepicker({ format: 'MM/DD/YYYY' });
                            $('#datetimepicker2').datetimepicker({ format: 'MM/DD/YYYY' });
                            SearchText();
                        }
                        function SearchText() {
                            $("#ContentPlaceHolder1_txtStudent").autocomplete({
                                source: function (request, response) {
                                    $.ajax({
                                        type: "POST",
                                        contentType: "application/json; charset=utf-8",
                                        url: "Canteen_Assigned_History.aspx/GetStudentInfo",
                                        data: "{'StudentName':'" + document.getElementById('ContentPlaceHolder1_txtStudent').value + "'}",
                                        dataType: "json",
                                        success: function (data) {

                                            //document.getElementById("iconcheck").style.display = "block";

                                            response(data.d);
                                            //$("iconcheck").toggle();
                                        },
                                        error: function (result) {

                                        }
                                    });
                                }
                            });
                            $("#ContentPlaceHolder1_txtStudent").bind("autocompleteselect", function (event, ui) {

                                $("#ContentPlaceHolder1_iconcheck").removeClass("hide");
                            });
                            $("#ContentPlaceHolder1_txtStudent").keypress(function () {

                                $("#ContentPlaceHolder1_iconcheck").addClass("hide");
                            });
                            $("#ContentPlaceHolder1_txtStudent").keydown(function () {
                                $("#ContentPlaceHolder1_iconcheck").addClass("hide");
                            });
                        }
                    </script>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="lnkGo" />

                </Triggers>
            </asp:UpdatePanel>
        </asp:Panel>
    </div>
</asp:Content>

