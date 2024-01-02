<%@ Page Title="" Language="C#" MasterPageFile="~/Admin_Master.Master" AutoEventWireup="true" CodeBehind="SMS_Role_Master.aspx.cs" Inherits="SMS.SMS_Role_Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Staff Roles</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdateRefresh" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <br />
            <br />
            <div class="page-content-wrapper" style="margin-bottom: -16px;">
                <div class="page-content">
                    <h3 class="page-title bold">Staff Roles</h3>
                    <div class="row" id="divgrid" runat="server">
                        <div class="col-md-12">
                            <div class="portlet box green-meadow">
                                <div class="portlet-title">
                                    <div class="caption">
                                        <i  class='fas'>&#xf502;</i>Staff Roles List
                                    </div>
                                   <div class="tools pull-left" style="margin-left: 10px; padding: 8px;border-left:0px!important;">
                                        <asp:TextBox ID="txtSearch" runat="server"  Width="230px" Height="28px" Style="border: 0px; color: black" />
                                    </div>
                                    <div class="tools pull-left" style="margin-left: 5px; padding: 8px;border-left:0px!important;">
                                        <asp:LinkButton ID="btnsearch" runat="server" OnClick="btnsearch_Click">
                                        <i class="fa fa-search" title="Search" style="font-size: 28px; color: white; margin-top: 4px;"></i>
                                        </asp:LinkButton>
                                    </div>
                                    <div class="tools">
                                        <asp:LinkButton ID="lnkAdd" runat="server" class="pull-right" Style="color: white" OnClick="lnkAdd_Click">
                                              <i class="fa fa-plus" title="Add New Role"></i>
                                        </asp:LinkButton>
                                    </div>
                                </div>
                                <div class="portlet-body" style="overflow: scroll">
                                   <asp:GridView ID="gridEmployee" runat="server" class="table table-striped table-bordered table-hover" AutoGenerateColumns="false" AllowPaging="true"
                                        AllowSorting="true" OnSorting="OnSorting" OnPageIndexChanging="OnPageIndexChanging" PageSize="10" ShowHeaderWhenEmpty="true" OnRowDataBound="gridEmployee_RowDataBound"
                                        EmptyDataText="No records Found">
                                        <PagerSettings Mode="NumericFirstLast" PageButtonCount="5" FirstPageText="First" LastPageText="Last" />
                                        <PagerStyle CssClass="gridview"></PagerStyle>
                                        <Columns>
                                            <asp:TemplateField ItemStyle-Width="5%" HeaderText="Id" SortExpression="Id">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblId" runat="server" Text='<%#Eval("Id") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="70%" HeaderText="Role" SortExpression="Role_Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblApplication_No" runat="server" Text='<%#Eval("Role_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="15%" HeaderText="IsActive" SortExpression="IsActive">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblIsActive" runat="server" Text='<%#Eval("IsActive") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField ItemStyle-Width="5%" HeaderText="Edit">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="linkEdit" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id")%>' OnClick="linkEdit_Click"><i class="material-icons" title="Edit row">&#xe3c9;</i></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="5%" HeaderText="Delete">
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
                                        <label class="text-success">Role : </label>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="description" style="width: 100%; border: 1px solid #ddd; padding: 10px; border-radius: 5px">
                                                    <div class="row">
                                                        <div class="col-md-12" id="Div1">
                                                            <div class="input-group">
                                                                <asp:TextBox ID="txtApplication_No" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:Label ID="lblmsg" runat="server" ForeColor="Red" Text="* Enter Role" Visible="false"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    


                                    <br />

                                    <asp:LinkButton ID="lnksave" runat="server" Text="Save" OnClick="btnsave_Click" class="btn green-meadow" Visible="false" />
                                    <asp:LinkButton ID="lnkupdate" runat="server" Text="Update" OnClick="btnupdate_Click" class="btn green-meadow" Visible="false" />
                                    <asp:LinkButton ID="lnkBack" runat="server" Text="Back" OnClick="lnkBack_Click" class="btn green-meadow" />

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
