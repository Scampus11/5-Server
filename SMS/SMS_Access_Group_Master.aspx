<%@ Page Title="" Language="C#" MasterPageFile="~/Admin_Master.Master" AutoEventWireup="true" CodeBehind="SMS_Access_Group_Master.aspx.cs" Inherits="SMS.SMS_Access_Group_Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Access Groups</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--<asp:UpdatePanel ID="UpdateRefresh" UpdateMode="Conditional" runat="server">
        <ContentTemplate>--%>
            <br />
            <br />
            <div class="page-content-wrapper" style="margin-bottom: -16px;">
                <div class="page-content">
                    <h3 class="page-title bold">Access Groups</h3>
                    <div class="row" id="divgrid" runat="server">
                        <div class="col-md-12">
                            <div class="portlet box green-meadow">
                                <div class="portlet-title">
                                    <div class="caption">
                                        <i class="material-icons">&#xe886;</i>Access Group  List
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
                                              <i class="fa fa-plus" title="Add New Access Group"></i>
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
                                            <asp:TemplateField ItemStyle-Width="40%" HeaderText="Access Group  Name" SortExpression="Access_Group_Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblApplication_No" runat="server" Text='<%#Eval("Access_Group_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="10%" HeaderText="Canteen Type" SortExpression="CanteenType">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCanteenType" runat="server" Text='<%#Eval("CanteenType") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="35%" HeaderText="Description" SortExpression="Description">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStaff_Id" runat="server" Text='<%#Eval("Description") %>'></asp:Label>
                                                    <asp:Label ID="lblid" runat="server" Text='<%#Eval("Id") %>' Visible="false"></asp:Label>
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
                                                            Access Group Name :
                                                        </label>
                                                        <div class="col-md-6">
                                                            <asp:TextBox ID="txtApplication_No" runat="server" CssClass="form-control" BorderColor="#37359C" placeholder="Access Group Name" Style="background-color: LightGrey;"></asp:TextBox>
                                                            <asp:Label ID="lblValiaccessgroupname" runat="server" Style="color: red;" Text="* Enter access group name..." Visible="false"></asp:Label>
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
                                        <div class="clearfix"></div>
                                        <div class="col-md-6 col-sm-6">
                                            <div class="portlet-body">
                                                <div class="form-body">
                                                    <div class="form-group">
                                                        <label class="col-md-2" style="width: 28%; margin-top: 11px!important">
                                                            Is SAG :
                                                        </label>
                                                        <div class="col-md-6">
                                                            <asp:CheckBox ID="chkcanteen" CssClass="checkbox" runat="server" Text="Is Canteen" OnCheckedChanged="chkcanteen_CheckedChanged" AutoPostBack="true"></asp:CheckBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-6 col-sm-6" id="DivCanteenType" runat="server" visible="false">
                                            <div class="portlet-body">
                                                <div class="form-body">
                                                    <div class="form-group">
                                                        <label class="col-md-3 control-label">
                                                            Canteen Type :
                                                        </label>
                                                        <div class="col-md-6">
                                                            <asp:RadioButton ID="RdbInkind" runat="server" Text="In Kind" GroupName="canteentype" />
                                                            <asp:RadioButton ID="RdbIncash" runat="server" Text="In Cash" GroupName="canteentype" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="clearfix"></div>
                                        <br />
                                        <div class="form-group">
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="description" style="width: 100%; border: 1px solid #ddd; padding: 10px; border-radius: 5px">
                                                        <div class="row">
                                                            <div class="col-md-4">
                                                                <label class="text-success">Door Group : </label>
                                                                <div class="input-group">
                                                                    <asp:DropDownList ID="ddlDoorgroup" CssClass="form-control" runat="server">
                                                                    </asp:DropDownList>

                                                                </div>
                                                            </div>
                                                            <div class="col-md-4">
                                                                <label class="text-success">Session : </label>
                                                                <div class="input-group">
                                                                    <asp:DropDownList ID="ddlSession" CssClass="form-control" runat="server">
                                                                    </asp:DropDownList>

                                                                </div>
                                                            </div>
                                                            <div class="col-md-4">
                                                                <label class="text-success"></label>
                                                                <div class="input-group">
                                                                    <asp:LinkButton ID="LnkAdd1" runat="server" Text="Add" OnClick="LnkAdd1_Click" class="btn green-meadow" />

                                                                </div>
                                                            </div>

                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <%--temp grid to bind--%>
                                            <div class="row" id="div3" runat="server">
                                                <div class="col-md-12">
                                                    <asp:Label ID="lblAlreadyValid" runat="server" Style="color: red;" Text="* Same record is already available..." Visible="false"></asp:Label>
                                                    <div class="portlet box green-meadow">
                                                        <div class="portlet-title">
                                                            <div class="caption">
                                                                <i class="fa fa-briefcase"></i>Access Group  List
                                                            </div>
                                                            <div class="tools">
                                                            </div>
                                                        </div>
                                                        <div class="portlet-body">
                                                            <asp:GridView ID="gvAccess" runat="server" class="table table-striped table-bordered table-hover" AutoGenerateColumns="false" ShowFooter="true">
                                                                <Columns>
                                                                    <asp:TemplateField ItemStyle-Width="45%" HeaderText="Access Group Id" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblChildId" runat="server" Text='<%#Eval("ChildId") %>'></asp:Label>
                                                                            <asp:Label ID="lblAccessGroupId" runat="server" Text='<%#Eval("Id") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField ItemStyle-Width="45%" HeaderText="Door Group" HeaderStyle-ForeColor="#337ab7">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblDoorGroup" runat="server" Text='<%#Eval("Door_Group") %>'></asp:Label>
                                                                            <asp:Label ID="lblDoorGroupId" Visible="false" runat="server" Text='<%#Eval("Door_GroupId") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField ItemStyle-Width="10%" HeaderText="Session" HeaderStyle-ForeColor="#337ab7">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblSession" runat="server" Text='<%#Eval("Session") %>'></asp:Label>
                                                                            <asp:Label ID="lblSessionId" Visible="false" runat="server" Text='<%#Eval("SessionId") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField ItemStyle-Width="10%" HeaderText="Delete" HeaderStyle-ForeColor="#337ab7">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="linkChildDelete" runat="server" Text="Delete" OnClick="linkChildDelete_Click"><i class="material-icons" style="color:red" title="Delete row">delete</i></asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <asp:LinkButton ID="lnksave" runat="server" Text="Save" OnClick="btnsave_Click" class="btn green-meadow" Visible="false" />
                                        <asp:LinkButton ID="lnkupdate" runat="server" Text="Update" OnClick="btnupdate_Click" class="btn green-meadow" Visible="false" />
                                        <asp:LinkButton ID="lnkBack" runat="server" Text="Back" OnClick="lnkBack_Click" class="btn green-meadow" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                </div>
        <%--</ContentTemplate>
        <Triggers>
        </Triggers>
    </asp:UpdatePanel>--%>
</asp:Content>
