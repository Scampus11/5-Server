<%@ Page Title="" Language="C#" MasterPageFile="~/Admin_Master.Master" AutoEventWireup="true" CodeBehind="SMS_Door_Group_Master.aspx.cs" Inherits="SMS.SMS_Door_Group_Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Door Groups</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <br />
    <div class="page-content-wrapper" style="margin-bottom: -16px;">
        <div class="page-content">
            <h3 class="page-title bold">Door Groups</h3>
            <div class="row" id="divgrid" runat="server">
                <div class="col-md-12">
                    <div class="portlet box green-meadow">
                        <div class="portlet-title">
                            <div class="caption">
                                <i class='fas'>&#xf52b;</i>Door Groups List
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
                                              <i class="fa fa-plus" title="Add New Door Group"></i>
                                </asp:LinkButton>
                            </div>
                        </div>
                        <div class="portlet-body" style="overflow: scroll">
                            <asp:GridView ID="gridEmployee" runat="server" class="table table-striped table-bordered table-hover" AutoGenerateColumns="false" AllowPaging="true" AllowSorting="true" OnSorting="OnSorting" OnPageIndexChanging="OnPageIndexChanging" PageSize="5" ShowHeaderWhenEmpty="true" EmptyDataText="No records Found">
                                <PagerSettings Mode="NumericFirstLast" PageButtonCount="10" FirstPageText="First" LastPageText="Last" />
                                <PagerStyle CssClass="gridview"></PagerStyle>
                                <Columns>
                                    <asp:TemplateField ItemStyle-Width="5%" HeaderText="Id" SortExpression="Id">
                                        <ItemTemplate>
                                            <asp:Label ID="lblId1" runat="server" Text='<%#Eval("Id") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="30%" HeaderText="Door Group Name" SortExpression="Door_Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblApplication_No" runat="server" Text='<%#Eval("Door_Name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="25%" HeaderText="Description" SortExpression="Description">
                                        <ItemTemplate>
                                            <asp:Label ID="lblStaff_Id" runat="server" Text='<%#Eval("Description") %>'></asp:Label>
                                            <asp:Label ID="lblid" runat="server" Text='<%#Eval("Id") %>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="30%" HeaderText="Reader Name" SortExpression="Reader_Id">
                                        <ItemTemplate>
                                            <asp:Label ID="lblReader_id" runat="server" Text='<%#Eval("Reader_Id") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="5%" HeaderText="Edit" HeaderStyle-ForeColor="#337ab7">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="linkEdit" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id")%>' OnClick="linkEdit_Click"><i class="material-icons" title="Edit row">&#xe3c9;</i></asp:LinkButton>
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
                                                    Door Group Name :
                                                </label>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtApplication_No" runat="server" CssClass="form-control" BorderColor="#37359C" placeholder="Door Group Name" Style="background-color: LightGrey;"></asp:TextBox>
                                                    <asp:Label ID="lblValidoorgroupname" runat="server" Style="color: red;" Text="* Enter door group name..." Visible="false"></asp:Label>
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
                                                    <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control" placeholder="Description"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-12 col-sm-12">
                                    <div class="portlet-body">
                                        <div class="form-body">
                                            <div class="form-group">
                                                <label class="col-md-2" style="width: 13%">
                                                    Reader Type :
                                                </label>
                                                <div class="col-md-6">
                                                    <asp:Repeater ID="RepeatReader" runat="server">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkReader" CssClass="checkbox" runat="server" Text='<%# Eval("Name")  %>' />
                                                            <asp:Label ID="lblId" runat="server" Text='<%# Eval("Id") %>' Visible="false"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
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
    </div>
</asp:Content>
