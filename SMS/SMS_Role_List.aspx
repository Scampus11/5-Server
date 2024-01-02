<%@ Page Title="" Language="C#" MasterPageFile="~/Admin_Master.Master" AutoEventWireup="true" CodeBehind="SMS_Role_List.aspx.cs" Inherits="SMS.SMS_Role_List" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Staff Mapping</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdateRefresh" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <br />
            <br />
            <div class="page-content-wrapper" style="margin-bottom: -16px;">
                <div class="page-content">
                    <h3 class="page-title bold">Staff Mapping
                    </h3>
                    
                    <div class="row" id="divgrid" runat="server">
                        <div class="col-md-12">
                            <div class="portlet box green-meadow">
                                <div class="portlet-title">
                                    <div class="caption">
                                        <i  class='fas'>&#xf4ff;</i>Staff Mapping List
                                    </div>
                                    <div class="tools pull-left" style="margin-left: 10px; padding: 8px;border-left:0px!important;">
                                        <asp:TextBox ID="txtSearch" runat="server" Width="230px" Height="28px" Style="border: 0px; color: black" />
                                    </div>
                                    <div class="tools pull-left" style="margin-left: 5px; padding: 8px;border-left:0px!important;">
                                        <asp:LinkButton ID="btnsearch" runat="server" OnClick="btnsearch_Click">
                                        <i class="fa fa-search" title="Search" style="font-size: 28px; color: white; margin-top: 4px;"></i>
                                        </asp:LinkButton>
                                    </div>
                                </div>
                                <div class="portlet-body" style="overflow: scroll">
                                    <asp:Label ID="lblmsg" ForeColor="Green" runat="server" Text="Update Successfully!!!" Font-Size="Large" Visible="false"></asp:Label>
                                    <asp:GridView ID="gridEmployee" runat="server" class="table table-striped table-bordered table-hover" AutoGenerateColumns="false" AllowPaging="true" AllowSorting="true" OnSorting="OnSorting"
                                        OnPageIndexChanging="OnPageIndexChanging" PageSize="10" ShowHeaderWhenEmpty="true" EmptyDataText="No records Found" OnRowDataBound="gridEmployee_RowDataBound">
                                        <PagerSettings Mode="NumericFirstLast" PageButtonCount="5" FirstPageText="First" LastPageText="Last" />
                                        <PagerStyle CssClass="gridview"></PagerStyle>
                                        <Columns>
                                            <asp:TemplateField ItemStyle-Width="30px" HeaderText="Staff Id" SortExpression="Staff_Id">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStaff_Id" runat="server" Text='<%#Eval("Staff_Id") %>'></asp:Label>
                                                    <asp:Label ID="lblid" runat="server" Text='<%#Eval("Id") %>' Visible="false"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField ItemStyle-Width="100px" HeaderText="Email Id" SortExpression="Email_Id">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEmail_Id" runat="server" Text='<%#Eval("Email_Id") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField ItemStyle-Width="100px" HeaderText="Role Name" SortExpression="Role_Id">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRole_Id" runat="server" Text='<%#Eval("Role_Id") %>' Visible="false"></asp:Label>
                                                    <asp:DropDownList ID="drpRole" runat="server" CssClass="form-control">
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>

                                    </asp:GridView>
                                    <br />
                                    <asp:LinkButton ID="lnkUpdate" class="btn green-meadow" runat="server" Text="Update" OnClick="lnkUpdate_Click"></asp:LinkButton>
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
