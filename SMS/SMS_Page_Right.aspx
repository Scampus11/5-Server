<%@ Page Title="" Language="C#" MasterPageFile="~/Admin_Master.Master" AutoEventWireup="true" CodeBehind="SMS_Page_Right.aspx.cs" Inherits="SMS.SMS_Page_Right" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Page Rights</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdateRefresh" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <br />
            <br />
            <div class="page-content-wrapper" style="margin-bottom: -16px;">
                <div class="page-content">
                    <h3 class="page-title bold">Page Rights</h3>
                    <div class="row" id="divgrid" runat="server">
                        <div class="col-md-12">
                            <div class="portlet box green-meadow">
                                <div class="portlet-title">
                                    <div class="caption">
                                        <i  class='fas'>&#xf4fc;</i>Page Rights
                                    </div>
                                </div>
                                <div class="portlet-body">
                                    <div class="col-md-4 col-sm-4">
                                        <div class="portlet-body">
                                            <div class="form-body">
                                                <div class="form-group">
                                                    <label class="col-md-3 control-label">
                                                        User Role :
                                                    </label>
                                                    <div class="col-md-6">
                                                        <asp:DropDownList ID="ddlRole" runat="server" CssClass="form-control" OnTextChanged="ddlRole_TextChanged" AutoPostBack="true"></asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-4 col-sm-4">
                                        <div class="portlet-body">
                                            <div class="form-body">
                                                <div class="form-group">
                                                    <label class="col-md-4 control-label">
                                                        Staff/Employee User :
                                                    </label>
                                                    <div class="col-md-6">
                                                        <asp:DropDownList ID="ddlStaff" runat="server" CssClass="form-control" OnTextChanged="ddlStaff_TextChanged" AutoPostBack="true"></asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="clearfix"></div>
                                    <hr />
                                    <asp:GridView ID="gridEmployee" runat="server" class="table table-striped table-bordered table-hover" AutoGenerateColumns="false" OnRowDataBound="gridEmployee_RowDataBound"
                                        AllowPaging="true" AllowSorting="true" OnSorting="OnSorting" OnPageIndexChanging="OnPageIndexChanging" PageSize="100" ShowHeaderWhenEmpty="true" EmptyDataText="No records Found">
                                        <PagerSettings Mode="NumericFirstLast" PageButtonCount="5" FirstPageText="First" LastPageText="Last" />
                                        <PagerStyle CssClass="gridview"></PagerStyle>
                                        <Columns>
                                            <asp:TemplateField ItemStyle-Width="10%" HeaderText="Select page">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkisactive" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField ItemStyle-Width="50%" HeaderText="Page Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPanelName" runat="server" Text='<%#Eval("PanelName") %>'></asp:Label>
                                                    <asp:Label ID="lblPage_Name" runat="server" Text='<%#Eval("Page_Name") %>' Visible="false"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField ItemStyle-Width="40%" HeaderText="">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkcanteens" CssClass="checkbox" Style="margin-left:20px;" Text="Canteens" Visible="false" runat="server" />
                                                    <asp:CheckBox ID="chkAccessgroup" CssClass="checkbox" Style="margin-left:20px;" Text="Access Group"  Visible="false" runat="server" />
                                                    <asp:CheckBox ID="chkblockgroup" CssClass="checkbox" Style="margin-left:20px;" Text="Block Group" Visible="false" runat="server" />
                                                    <asp:CheckBox ID="chkStaffAccessgroup" CssClass="checkbox" Style="margin-left:20px;" Text="Access Group" Visible="false" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>

                                    </asp:GridView>
                                    <br />
                                    <asp:LinkButton ID="lnkUpdate" class="btn green-meadow" runat="server" Text="Update" Visible="false" OnClick="lnkUpdate_Click"></asp:LinkButton>
                                </div>

                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <%--<asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />--%>
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
