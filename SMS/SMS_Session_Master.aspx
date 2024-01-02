<%@ Page Title="" Language="C#" MasterPageFile="~/Admin_Master.Master" AutoEventWireup="true" CodeBehind="SMS_Session_Master.aspx.cs" Inherits="SMS.SMS_Session_Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Sessions</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
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
    <br />
    <br />
    <div class="page-content-wrapper" style="margin-bottom: -16px;">
        <div class="page-content">
            <h3 class="page-title bold">Sessions</h3>
            <div class="row" id="divgrid" runat="server">
                <div class="col-md-12">
                    <div class="portlet box green-meadow">
                        <div class="portlet-title">
                            <div class="caption">
                                <i class="material-icons">&#xe8b5;</i>Session List
                            </div>
                            <div class="tools pull-left" style="margin-left: 10px; padding: 8px; border-left: 0px!important;">
                                <asp:TextBox ID="txtSearch" runat="server" Width="230px" Height="28px" Style="border: 0px; color: black" />
                            </div>
                            <div class="tools pull-left" style="margin-left: 5px; padding: 8px; border-left: 0px!important;">
                                <asp:LinkButton ID="btnsearch" runat="server" OnClick="btnsearch_Click">
                                        <i class="fa fa-search" title="Search" style="font-size: 28px; color: white; margin-top: 4px;"></i>
                                </asp:LinkButton>
                            </div>
                            <div class="tools">
                                <asp:LinkButton ID="lnkAdd" runat="server" class="pull-right" Style="color: white" OnClick="lnkAdd_Click">
                                              <i class="fa fa-plus" title="Add New Session"></i>
                                </asp:LinkButton>
                            </div>
                        </div>
                        <div class="portlet-body">
                            <asp:GridView ID="gridEmployee" runat="server" class="table table-striped table-bordered table-hover" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataText="No records Found">
                                <PagerSettings Mode="NumericFirstLast" PageButtonCount="10" FirstPageText="First" LastPageText="Last" />
                                <PagerStyle CssClass="gridview"></PagerStyle>
                                <Columns>
                                    <asp:TemplateField ItemStyle-Width="5%" HeaderText="Id" SortExpression="Session_Id" HeaderStyle-ForeColor="#337ab7">
                                        <ItemTemplate>
                                            <asp:Label ID="lblId1" runat="server" Text='<%#Eval("Session_Id") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="35%" HeaderText="Session Name" SortExpression="Session_Name" HeaderStyle-ForeColor="#337ab7">
                                        <ItemTemplate>
                                            <asp:Label ID="lblApplication_No" runat="server" Text='<%#Eval("Session_Name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="36%" HeaderText="Session Description" SortExpression="Session_Description" HeaderStyle-ForeColor="#337ab7">
                                        <ItemTemplate>
                                            <asp:Label ID="lblStaff_Id" runat="server" Text='<%#Eval("Session_Description") %>'></asp:Label>
                                            <asp:Label ID="lblid" runat="server" Text='<%#Eval("Session_Id") %>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="7%" HeaderText="Start Time" SortExpression="Start_Time" HeaderStyle-ForeColor="#337ab7">
                                        <ItemTemplate>
                                            <asp:Label ID="lblStart_Time" runat="server" Text='<%#Eval("Start_Time") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="7%" HeaderText="End Time" SortExpression="End_Time" HeaderStyle-ForeColor="#337ab7">
                                        <ItemTemplate>
                                            <asp:Label ID="lblStart_End" runat="server" Text='<%#Eval("End_Time") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="5%" HeaderText="Edit" HeaderStyle-ForeColor="#337ab7">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="linkEdit" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Session_Id")%>' OnClick="linkEdit_Click"><i class="material-icons" title="Edit row">&#xe3c9;</i></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="5%" HeaderText="Delete" HeaderStyle-ForeColor="#337ab7">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="linkDelete" runat="server" OnClick="linkDelete_Click"><i class="material-icons" style="color:red" title="Delete row">delete</i></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal-content" id="divView" runat="server" visible="false">
                <div class="modal-body">
                    <div class="portlet-body form">
                        <div class="form-body">
                            <div class="form-group">
                                <div class="col-md-6 col-sm-6">
                                    <div class="portlet-body">
                                        <div class="form-body">
                                            <div class="form-group">
                                                <label class="col-md-3 control-label">
                                                    Session Name :
                                                </label>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtApplication_No" runat="server" CssClass="form-control" BorderColor="#37359C" placeholder="Session Name" Style="background-color: LightGrey;"></asp:TextBox>
                                                    <asp:Label ID="lblValisessionname" runat="server" Style="color: red;" Text="* Enter session name..." Visible="false"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6 col-sm-6">
                                    <div class="portlet-body">
                                        <div class="form-body">
                                            <div class="form-group">
                                                <label class="col-md-3 control-label">
                                                    Description :
                                                </label>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtSessionDescription" runat="server" CssClass="form-control" placeholder="Description"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="clearfix"></div>
                                <div class="col-md-6 col-sm-6">
                                    <div class="portlet-body">
                                        <div class="form-body">
                                            <div class="form-group">
                                                <label class="col-md-3 control-label">
                                                    Start Time :
                                                </label>
                                                <div class="col-md-6">
                                                    <div class='input-group date' id='datetimepicker2'>
                                                        <asp:TextBox ID="txtStartTime" runat="server" class="form-control" BorderColor="#37359C" placeholder="Start Time" Style="background-color: LightGrey;"></asp:TextBox>
                                                        <span class="input-group-addon">
                                                            <span class="glyphicon glyphicon-time"></span>
                                                        </span>
                                                    </div>
                                                    <asp:Label ID="lblValistarttime" runat="server" Style="color: red;" Text="* Enter start time..." Visible="false"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6 col-sm-6">
                                    <div class="portlet-body">
                                        <div class="form-body">
                                            <div class="form-group">
                                                <label class="col-md-3 control-label">
                                                    End Time :
                                                </label>
                                                <div class="col-md-6">
                                                    <div class='input-group date' id='datetimepicker3'>
                                                        <asp:TextBox ID="txtEndTime" runat="server" class="form-control" BorderColor="#37359C" placeholder="End Time" Style="background-color: LightGrey;"></asp:TextBox>
                                                        <span class="input-group-addon">
                                                            <span class="glyphicon glyphicon-time"></span>
                                                        </span>
                                                    </div>
                                                    <asp:Label ID="lblValiendtime" runat="server" Style="color: red;" Text="* Enter end time..." Visible="false"></asp:Label>
                                                    <asp:Label ID="lblvalistartend" runat="server" Style="color: red;" Text="* End time can not be exceed than start time of session..." Visible="false"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="clearfix"></div>
                                <br />
                                <br />
                                <div class="col-md-6 col-sm-6">
                                    <div class="portlet-body">
                                        <div class="form-body">
                                            <div class="form-group">

                                                <div class="col-md-6">
                                                    <asp:LinkButton ID="lnksave" runat="server" Text="Save" OnClick="btnsave_Click" class="btn green-meadow" Visible="false" />
                                                    <asp:LinkButton ID="lnkupdate" runat="server" Text="Update" OnClick="btnupdate_Click" class="btn green-meadow" Visible="false" />
                                                    <asp:LinkButton ID="lnkBack" runat="server" Text="Back" OnClick="lnkBack_Click" class="btn green-meadow" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <br />
                                <br />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
</asp:Content>
