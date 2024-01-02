<%@ Page Title="" Language="C#" MasterPageFile="~/Admin_Master.Master" AutoEventWireup="true" CodeBehind="Visitor_Served_History.aspx.cs" Inherits="SMS.Visitor.Visitor_Served_History" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Employee Served Visitors </title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="CSS/jquery-ui.css" rel="stylesheet" />
    <%--<script src="JS/jquery.min.js"></script>--%>
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

                filename: "Visitor_Served_History.xls"
            });
        }
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#datetimepicker1').datetimepicker({ format: 'DD/MM/YYYY' });
            $('#datetimepicker2').datetimepicker({ format: 'DD/MM/YYYY' });

            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
        });
        function EndRequestHandler(sender, args) {

            SetDatePicker();
        }
        function SetDatePicker() {
            $('#datetimepicker1').datetimepicker({ format: 'DD/MM/YYYY' });
            $('#datetimepicker2').datetimepicker({ format: 'DD/MM/YYYY' });
        }
    </script>
    <style>
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
                                        <i class='fas'>&#xf4fc;</i>Employee Served Visitors
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
                                            <asp:TemplateField ItemStyle-Width="20px" HeaderText="Images">
                                                <ItemTemplate>
                                                    <asp:Image ID="tmgstudent" runat="server" ImageUrl='<%#Eval("Visitor_Photo")==""?"~/Images/student.jpeg":Eval("Visitor_Photo") %>' class="circle" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="30px" HeaderText="Visitor Reg No" SortExpression="Visitor_Reg_No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStudentId" runat="server" Text='<%#Eval("Visitor_Reg_No") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="30px" HeaderText="Visitor Name" SortExpression="VisitorName">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCanteenName" runat="server" Text='<%#Eval("VisitorName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="30px" HeaderText="Email Id" SortExpression="Email_Id">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAssignDate" runat="server" Text='<%#Eval("Email_Id") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="30px" HeaderText="Phone Number" SortExpression="Phone_Number">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCanteenType" runat="server" Text='<%#Eval("Phone_Number") %>'></asp:Label>
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
                                            <asp:TemplateField ItemStyle-Width="100px" HeaderText="Access Card Number" SortExpression="Access_Card_Number">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAccess_CardNumber" runat="server" Text='<%#Eval("Access_Card_Number") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="30px" HeaderText="Service Name" SortExpression="ServiceName">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCardStatus" runat="server" Text='<%#Eval("ServiceName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="30px" HeaderText="Emp Name" SortExpression="EmpName">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAssignBy" runat="server" Text='<%#Eval("EmpName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="30px" HeaderText="Assign Datetime" SortExpression="assignDatetime">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblassignDatetime" runat="server" Text='<%#Eval("assignDatetime") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="30px" HeaderText="Served Datetime" SortExpression="ServedDatetime">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCreatedDatetime" runat="server" Text='<%#Eval("ServedDatetime") %>'></asp:Label>
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
                                            <div class='col-sm-10 pull-left'>
                                                <div class="form-group">
                                                    <span>Search Service : </span>
                                                    <i id="iServices" class="fa fa-check-circle hide" style="color: green" runat="server"></i>
                                                    <asp:TextBox ID="txtServices" runat="server" CssClass="form-control" />
                                                    <asp:TextBox ID="txtServicesId" runat="server" CssClass="form-control" Style="display: none;" />
                                                    <asp:Label ID="lblValidcanteen" runat="server" Style="color: red;" Text="* Select canteen..." Visible="false"></asp:Label>
                                                </div>
                                            </div>
                                             <div class='col-sm-1' style="margin-top:18px;">
                                                <div class="form-group">
                                                    <a class="btn btn-primary" id="btnservices" onclick="RemoveServices();"><i class="fa fa-remove"></i></a>
                                                    </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class='col-sm-10'>
                                                <div class="form-group">
                                                    <span>Search Employee : </span>
                                                    <i id="iemployee" class="fa fa-check-circle hide" style="color: green" runat="server"></i>
                                                    <asp:TextBox ID="txtEmployee" runat="server" CssClass="form-control" />
                                                    <asp:TextBox ID="txtEmployeeId" runat="server" CssClass="form-control" Style="display: none;" />
                                                </div>
                                            </div>
                                            <div class='col-sm-1' style="margin-top:18px;">
                                                <div class="form-group">
                                                    <a class="btn btn-primary"  onclick="RemoveEmployee();"><i class="fa fa-remove"></i></a>
                                                    </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class='col-sm-10'>
                                                <div class="form-group">
                                                    <span>Search Visitor : </span>
                                                    <i id="iconcheck" class="fa fa-check-circle hide" style="color: green" runat="server"></i>
                                                    <asp:TextBox ID="txtVisitor" runat="server" CssClass="form-control" />
                                                    <asp:TextBox ID="txtVisitorId" runat="server" CssClass="form-control" Style="display: none;" />
                                                </div>
                                            </div>
                                            <div class='col-sm-1' style="margin-top:18px;">
                                                <div class="form-group">
                                                    <a class="btn btn-primary"  onclick="RemoveVisitor();"><i class="fa fa-remove"></i></a>
                                                    </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class='col-sm-12'>
                                                <div class="form-group">
                                                    <span>Select  Status : </span>
                                                    <asp:DropDownList ID="ddlServeFlag" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlServeFlag_SelectedIndexChanged" AutoPostBack="true">
                                                        <%--<asp:ListItem Value="0">All</asp:ListItem>--%>
                                                        <asp:ListItem Value="1">Served</asp:ListItem>
                                                        <asp:ListItem Value="2">Not Served</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:Label ID="lblValidsession" runat="server" Style="color: red;" Text="* Select session..." Visible="false"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" runat="server" id="divServedfilter">
                                            <div class='col-sm-6'>
                                                <span>
                                                    <asp:Label ID="lblStartServed" runat="server" Text="Start Served date"></asp:Label>
                                                    : </span>
                                                <div class='input-group date' id='datetimepicker1'>
                                                    <asp:TextBox ID="txtDate" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <span class="input-group-addon">
                                                        <span class="glyphicon glyphicon-calendar"></span>
                                                    </span>
                                                </div>
                                            </div>
                                            <div class='col-sm-6'>
                                                <span>
                                                    <asp:Label ID="lblEndServed" runat="server" Text="End Served date"></asp:Label>
                                                    : </span>
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
                            // Uncomment the line above if you pre-fill values from LS or server
                            SearchText();
                            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
                        });
                        function EndRequestHandler(sender, args) {
                            $('#datetimepicker1').datetimepicker({ format: 'DD/MM/YYYY' });
                            $('#datetimepicker2').datetimepicker({ format: 'DD/MM/YYYY' });
                            SearchText();
                        }
                        function RemoveServices() {
                            
                             $("#ContentPlaceHolder1_txtServices").val('');
                            $("#ContentPlaceHolder1_txtServicesId").val('');
                                $("#ContentPlaceHolder1_iServices").addClass("hide");
                        };
                        function RemoveEmployee() {
                            
                             $("#ContentPlaceHolder1_txtEmployeeId").val('');
                            $("#ContentPlaceHolder1_txtEmployee").val('');
                                $("#ContentPlaceHolder1_iemployee").addClass("hide");
                        };
                         function RemoveVisitor() {
                            
                             $("#ContentPlaceHolder1_txtVisitor").val('');
                            $("#ContentPlaceHolder1_txtVisitorId").val('');
                                $("#ContentPlaceHolder1_iconcheck").addClass("hide");
                        };
                        function SearchText() {
                            $("#ContentPlaceHolder1_txtVisitor").autocomplete({
                                source: function (request, response) {
                                    $.ajax({
                                        type: "POST",
                                        contentType: "application/json; charset=utf-8",
                                        
                                        url:"Visitor_Served_History.aspx/GetVisitorInfo",
                                        data: "{'Name':'" + document.getElementById('ContentPlaceHolder1_txtVisitor').value + "'}",
                                        dataType: "json",
                                        success: function (data) {
                                            response($.map(data.d, function (item) {
                                                return {
                                                    label: item.split('-')[0],
                                                    val: item.split('-')[1]
                                                }
                                            }))
                                        },
                                        error: function (result) {

                                        }
                                    });
                                }
                            });
                            $("#ContentPlaceHolder1_txtVisitor").bind("autocompleteselect", function (event, ui) {

                                $("#ContentPlaceHolder1_txtVisitorId").val(ui.item.val);
                                $("#ContentPlaceHolder1_iconcheck").removeClass("hide");
                            });
                            $("#ContentPlaceHolder1_txtVisitor").keypress(function () {

                                $("#ContentPlaceHolder1_iconcheck").addClass("hide");
                            });
                            $("#ContentPlaceHolder1_txtVisitor").keydown(function () {
                                $("#ContentPlaceHolder1_iconcheck").addClass("hide");
                            });

                            $("#ContentPlaceHolder1_txtServices").autocomplete({

                                source: function (request, response) {
                                    $.ajax({
                                        type: "POST",
                                        contentType: "application/json; charset=utf-8",
                                        url:"Visitor_Served_History.aspx/GetServicesInfo",
                                        
                                        data: "{'Name':'" + document.getElementById('ContentPlaceHolder1_txtServices').value + "'}",
                                        dataType: "json",
                                        success: function (data) {

                                            response($.map(data.d, function (item) {
                                                return {
                                                    label: item.split('-')[0],
                                                    val: item.split('-')[1]
                                                }
                                            }))
                                        },
                                        error: function (result) {

                                        }
                                    });
                                },
                                select: function (e, i) {
                                    var text = this.value.split(/,\s*/);
                                    text.pop();
                                    text.push(i.item.value);
                                    text.push("");
                                    this.value = text.join(", ");
                                    var value = $("#ContentPlaceHolder1_txtServicesId").val().split(/,\s*/);
                                    value.pop();
                                    value.push(i.item.val);
                                    value.push("");
                                    $("#ContentPlaceHolder1_txtServicesId")[0].value = value.join(",");
                                     $("#ContentPlaceHolder1_iServices").removeClass("hide");
                                    return false;
                                },
                                minLength: 1
                            });
                            //$("#ContentPlaceHolder1_txtServices").bind("autocompleteselect", function (event, ui) {
                            //    var text = ui.item.value.split(/,\s*/);
                            //    text.pop();
                            //    text.push(ui.item.value);
                            //    text.push("");
                            //    ui.item.value = text.join(", ");
                            //    var value = $("#ContentPlaceHolder1_txtServicesId").val().split(/,\s*/);
                            //    value.pop();
                            //    value.push(ui.item.val);
                            //    value.push("");
                            //    $("#ContentPlaceHolder1_txtServicesId")[0].value = value.join(", ");
                            //    //$("#ContentPlaceHolder1_txtServicesId").val(ui.item.val);
                            //    $("#ContentPlaceHolder1_iServices").removeClass("hide");
                            //});
                            $("#ContentPlaceHolder1_txtServices").keypress(function () {

                                $("#ContentPlaceHolder1_iServices").addClass("hide");
                            });
                            $("#ContentPlaceHolder1_txtServices").keydown(function () {
                                $("#ContentPlaceHolder1_iServices").addClass("hide");
                            });

                            $("#ContentPlaceHolder1_txtEmployee").autocomplete({
                                source: function (request, response) {
                                    $.ajax({
                                        type: "POST",
                                        contentType: "application/json; charset=utf-8",
                                        
                                        url: "Visitor_Served_History.aspx/GetEmployeesInfo",
                                        data: "{'Name':'" + document.getElementById('ContentPlaceHolder1_txtEmployee').value + "'}",
                                        dataType: "json",
                                        success: function (data) {
                                            response($.map(data.d, function (item) {
                                                return {
                                                    label: item.split('-')[0],
                                                    val: item.split('-')[1]
                                                }
                                            }))
                                        },
                                        error: function (result) {

                                        }
                                    });
                                }
                            });
                            $("#ContentPlaceHolder1_txtEmployee").bind("autocompleteselect", function (event, ui) {

                                $("#ContentPlaceHolder1_txtEmployeeId").val(ui.item.val);
                                $("#ContentPlaceHolder1_iemployee").removeClass("hide");
                            });
                            $("#ContentPlaceHolder1_txtEmployee").keypress(function () {

                                $("#ContentPlaceHolder1_iemployee").addClass("hide");
                            });
                            $("#ContentPlaceHolder1_txtEmployee").keydown(function () {
                                $("#ContentPlaceHolder1_iemployee").addClass("hide");
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
