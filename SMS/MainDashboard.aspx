<%@ Page Title="" Language="C#" MasterPageFile="~/Admin_Master.Master" AutoEventWireup="true" CodeBehind="MainDashboard.aspx.cs" Inherits="SMS.MainDashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Dashboard
    </title>
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdateRefresh" UpdateMode="Conditional" runat="server" style="margin-top: -10px">
        <ContentTemplate>
            <br />
            <br />
            <br />
            <div class="page-content-wrapper" style="margin-bottom: -16px;">
                <div class="page-content">
                    <div class="row" id="divgrid" runat="server">
                        <div class="col-md-12">

                            <div class="portlet box green-meadow">
                                <div class="portlet-title">
                                    <div class="caption">
                                        <i class="fa fa-briefcase"></i>Dashboard
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12">
                            <a href="Student_Master.aspx" target="_blank">
                                <div class="dashboard-stat blue">
                                    <div class="visual">
                                        <i class="fa fa-comments"></i>
                                    </div>
                                    <div class="details">
                                        <div class="number">
                                            <%--<span data-counter="counterup" data-value="6">0</span>--%>
                                            <span data-counter="counterup" data-value="6">
                                                <asp:Label ID="lblStudent" runat="server"></asp:Label>
                                            </span>
                                        </div>
                                        <div class="desc">Total Student </div>
                                    </div>


                                </div>
                            </a>
                        </div>
                        <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12">
                            <a href="Staff_Employee_Master.aspx" target="_blank">
                                <div class="dashboard-stat red">
                                    <div class="visual">
                                        <i class="fa fa-bar-chart-o"></i>
                                    </div>
                                    <div class="details">
                                        <div class="number">
                                            <%--<span data-counter="counterup" data-value="236.00">0</span></div>--%>
                                            <span data-counter="counterup" data-value="6">
                                                <asp:Label ID="lblStaff" runat="server"></asp:Label>
                                            </span>
                                            <div class="desc">Total Staff </div>
                                        </div>
                                    </div>
                                </div>
                            </a>
                        </div>
                        <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12">
                            <a href="SMS_Visitor_Master.aspx" target="_blank">
                                <div class="dashboard-stat green">
                                    <div class="visual">
                                        <i class="fa fa-shopping-cart"></i>
                                    </div>
                                    <div class="details">
                                        <div class="number">
                                            <%--<span data-counter="counterup" data-value="3,353.00">0</span>--%>
                                            <span data-counter="counterup" data-value="6">
                                                <asp:Label ID="lblVisitor" runat="server"></asp:Label>
                                            </span>
                                        </div>
                                        <div class="desc">Total Visitor </div>
                                    </div>
                                </div>
                            </a>
                        </div>
                    </div>

                    <div class="row" id="div1" runat="server">
                        <div class="col-md-12">
                            <div class="portlet box green-meadow">
                                <div class="portlet-title" style="background-color: gray">
                                    <div class="caption">
                                        <i class="fa fa-briefcase"></i>Overview
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-2 col-md-2 col-sm-6 col-xs-12">
                            <a href="SMS_Student_Access_List.aspx" target="_blank">
                                <div class="dashboard-stat blue" style="height: 122px">
                                    <div class="visual">
                                        <%--<i class="fa fa-comments"></i>--%>
                                    </div>
                                    <div class="details">
                                        <div class="number">
                                            <%--<span data-counter="counterup" data-value="6">0</span>--%>
                                            <span data-counter="counterup" data-value="6">
                                                <asp:Label ID="lblStudentAccess" runat="server"></asp:Label>
                                            </span>
                                        </div>
                                        <div class="desc">Student Access </div>
                                    </div>
                                    <%--<a class="more" href="Student_Master.aspx"> View more
                            <i class="m-icon-swapright m-icon-white"></i>
                        </a>--%>
                                </div>
                            </a>
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-6 col-xs-12">
                            <a href="SMS_Staff_Access_List.aspx" target="_blank">
                                <div class="dashboard-stat red" style="height: 122px">
                                    <div class="visual">
                                        <%--<i class="fa fa-bar-chart-o"></i>--%>
                                    </div>
                                    <div class="details">
                                        <div class="number">
                                            <%--<span data-counter="counterup" data-value="236.00">0</span>--%>
                                            <span data-counter="counterup" data-value="6">
                                                <asp:Label ID="lblStaffAccess" runat="server"></asp:Label>
                                            </span>
                                        </div>
                                        <div class="desc">Staff Access</div>
                                    </div>
                                    <%--<a class="more" href="Staff_Employee_Master.aspx"> View more
                            <i class="m-icon-swapright m-icon-white"></i>
                        </a>--%>
                                </div>
                            </a>
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-6 col-xs-12">
                            <a href="SMS_Session_Master.aspx" target="_blank">
                                <div class="dashboard-stat green" style="height: 122px">
                                    <div class="visual">
                                        <%--<i class="fa fa-shopping-cart"></i>--%>
                                    </div>
                                    <div class="details">
                                        <div class="number">
                                            <%--<span data-counter="counterup" data-value="3,353.00">0</span>--%>
                                            <span data-counter="counterup" data-value="6">
                                                <asp:Label ID="lblSession" runat="server"></asp:Label>
                                            </span>
                                        </div>
                                        <div class="desc">Session </div>
                                    </div>
                                    <%--<a class="more" href="SMS_Visitor_Master.aspx"> View more
                            <i class="m-icon-swapright m-icon-white"></i>
                        </a>--%>
                                </div>
                            </a>
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-6 col-xs-12">
                            <a href="SMS_Access_Group_Master.aspx" target="_blank">
                                <div class="dashboard-stat grey" style="height: 122px">
                                    <div class="visual">
                                        <%--<i class="fa fa-shopping-cart"></i>--%>
                                    </div>
                                    <div class="details">
                                        <div class="number">
                                            <%--<span data-counter="counterup" data-value="3,353.00">0</span>--%>
                                            <span data-counter="counterup" data-value="6">
                                                <asp:Label ID="lblCanteen" runat="server"></asp:Label>
                                            </span>
                                        </div>
                                        <div class="desc">SAG </div>
                                    </div>
                                    <%--<a class="more" href="SMS_Visitor_Master.aspx"> View more
                            <i class="m-icon-swapright m-icon-white"></i>
                        </a>--%>
                                </div>
                            </a>
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-6 col-xs-12">
                            <a href="SMS_Access_Group_Master.aspx" target="_blank">
                                <div class="dashboard-stat yellow" style="height: 122px">
                                    <div class="visual">
                                        <%--<i class="fa fa-shopping-cart"></i>--%>
                                    </div>
                                    <div class="details">
                                        <div class="number">
                                            <%--<span data-counter="counterup" data-value="3,353.00">0</span>--%>
                                            <span data-counter="counterup" data-value="6">
                                                <asp:Label ID="lblAG" runat="server"></asp:Label>
                                            </span>
                                        </div>
                                        <div class="desc">AG</div>
                                    </div>
                                    <%--<a class="more" href="SMS_Visitor_Master.aspx"> View more
                            <i class="m-icon-swapright m-icon-white"></i>
                        </a>--%>
                                </div>
                            </a>
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-6 col-xs-12">
                            <a href="SMS_Reader_Master.aspx" target="_blank">
                                <div class="dashboard-stat blue-chambray" style="height: 122px">
                                    <div class="visual">
                                        <%--<i class="fa fa-shopping-cart"></i>--%>
                                    </div>
                                    <div class="details">
                                        <div class="number">
                                            <%--<span data-counter="counterup" data-value="3,353.00">0</span>--%>
                                            <span data-counter="counterup" data-value="6">
                                                <asp:Label ID="lblReader" runat="server"></asp:Label>
                                            </span>
                                        </div>
                                        <div class="desc">Reader </div>
                                    </div>
                                    <%--<a class="more" href="SMS_Visitor_Master.aspx"> View more
                            <i class="m-icon-swapright m-icon-white"></i>
                        </a>--%>
                                </div>
                                </a>
                        </div>
                    </div>

                    <div class="row" id="div2" runat="server">
                        <div class="col-md-12">
                            <div class="portlet box green-meadow">
                                <div class="portlet-title" style="background-color: gray">
                                    <div class="caption">
                                        <i class="fa fa-briefcase"></i>Visitor Module
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div>
                        <div class="portlet-body">
                            <div class="row" id="div3" runat="server">
                                <div class='col-sm-3'>
                                    <div class="form-group">
                                        <div class='input-group date' id='datetimepicker1'>
                                            <asp:TextBox ID="txtDate" runat="server" CssClass="form-control"></asp:TextBox>
                                            <span class="input-group-addon">
                                                <span class="glyphicon glyphicon-calendar"></span>
                                            </span>
                                        </div>
                                    </div>
                                </div>
                                <div class='col-sm-3'>
                                    <div class="form-group">
                                        <div class='input-group date' id='datetimepicker2'>
                                            <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control"></asp:TextBox>
                                            <span class="input-group-addon">
                                                <span class="glyphicon glyphicon-calendar"></span>
                                            </span>
                                        </div>
                                    </div>
                                </div>
                                <div class='col-sm-2'>
                                    <asp:LinkButton ID="lnkGo" runat="server" Text="Go" class="btn green-meadow" OnClick="lnkGo_Click" />
                                </div>
                                <div class="col-sm-4"></div>

                                <div class="col-md-8">

                                    <asp:GridView ID="gridEmployee" runat="server" class="table table-striped table-bordered table-hover" AutoGenerateColumns="false" ShowHeader="false">
                                        <Columns>
                                            <asp:TemplateField ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblregister" runat="server" Text='<%#Eval("register") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                        </Columns>

                                    </asp:GridView>
                                </div>
                                <div class="col-md-4">
                                    <asp:GridView ID="Grdvisitors2" runat="server" class="table table-striped table-bordered table-hover" AutoGenerateColumns="false" ShowHeader="false">
                                        <Columns>
                                            <asp:TemplateField ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblregister2" runat="server" Text='<%#Eval("register") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>

                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>


                    <div>
                    </div>
                </div>
            </div>
        </ContentTemplate>

    </asp:UpdatePanel>
</asp:Content>
