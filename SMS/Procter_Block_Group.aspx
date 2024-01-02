<%@ Page Title="" Language="C#" MasterPageFile="~/Admin_Master.Master" AutoEventWireup="true" CodeBehind="Procter_Block_Group.aspx.cs" Inherits="SMS.Procter_Block_Group" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Block Groups</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdateRefresh" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <br />
            <br />
            <div class="page-content-wrapper" style="margin-bottom: -16px;">
                <div class="page-content">
                    <h3 class="page-title bold">Block Groups</h3>
                    <div class="row" id="divgrid" runat="server">
                        <div class="col-md-12">
                            <div class="portlet box green-meadow">
                                <div class="portlet-title">
                                    <div class="caption">
                                        <i class="fa fa-building"></i>Block Groups
                                    </div>
                                    <div class="tools pull-left" style="margin-left: 10px; padding: 8px;border-left:0px!important;">
                                        <asp:TextBox ID="txtSearch" placeHolder="Search" runat="server" Width="230px" Height="28px" Style="border: 0px; color: black" />
                                    </div>
                                    <div class="tools pull-left" style="margin-left: 5px; padding: 8px;border-left:0px!important;">
                                        <asp:LinkButton ID="btnsearch" runat="server" OnClick="btnsearch_Click">
                                        <i class="fa fa-search" title="Search" style="font-size: 28px; color: white; margin-top: 4px;"></i>
                                        </asp:LinkButton>
                                    </div>
                                    <div class="tools">
                                        <asp:LinkButton ID="lnkAdd" runat="server" class="pull-right" Style="color: white" OnClick="lnkAdd_Click">
                                              <i class="fa fa-plus" title="Add New Block Group"></i>
                                        </asp:LinkButton>
                                    </div>
                                </div>
                                <div class="portlet-body" style="overflow: scroll">
                                    <asp:GridView ID="gridEmployee" runat="server" class="table table-striped table-bordered table-hover" AutoGenerateColumns="false" AllowPaging="true" AllowSorting="true" OnSorting="OnSorting" OnPageIndexChanging="OnPageIndexChanging" PageSize="5" ShowHeaderWhenEmpty="true" EmptyDataText="No records Found">
                                        <PagerSettings Mode="NumericFirstLast" PageButtonCount="5" FirstPageText="First" LastPageText="Last" />
                                        <PagerStyle CssClass="gridview"></PagerStyle>
                                        <Columns>
                                            <asp:TemplateField ItemStyle-Width="30px" HeaderText="Id" SortExpression="Id" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblId1" runat="server" Text='<%#Eval("Id") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="30px" HeaderText="Name" SortExpression="Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblName" runat="server" Text='<%#Eval("Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="30px" HeaderText="Description" SortExpression="Description">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDescription" runat="server" Text='<%#Eval("Description") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="30px" HeaderText="Hostel Name" SortExpression="HostelName">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblHostelName" runat="server" Text='<%#Eval("HostelName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="30px" HeaderText="Block Name" SortExpression="BlockName">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBlockName" runat="server" Text='<%#Eval("BlockName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            
                                            <asp:TemplateField ItemStyle-Width="10px" HeaderText="Edit">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="linkEdit" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id")%>' OnClick="linkEdit_Click"><i class="material-icons" title="Edit row">&#xe3c9;</i></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="10px" HeaderText="Delete">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="linkDelete" runat="server" OnClick="linkDelete_Click"><i class="material-icons" style="color:red" title="Delete row">Delete</i></asp:LinkButton>
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
                                        <div class="row">
                                            <div class="col-md-6 col-sm-6">
                                                <div class="portlet-body">
                                                    <div class="form-body">
                                                        <div class="form-group">
                                                            <label class="col-md-3 control-label">
                                                                Name :
                                                            </label>
                                                            <div class="col-md-6">
                                                                <asp:TextBox ID="txtBlockGroup" runat="server" CssClass="form-control" BackColor="#d3d3d3" placeholder="Name"></asp:TextBox>
                                                                <asp:Label ID="lblBlockGroup" runat="server" Style="color: red;" Text="* Enter Name..." Visible="false"></asp:Label>
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
                                                            <label class="col-md-3 control-label">
                                                                Hostel Name :
                                                            </label>
                                                            <div class="col-md-6">
                                                                <asp:DropDownList ID="ddlHostel" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlHostel_SelectedIndexChanged" AutoPostBack="true" BackColor="#d3d3d3"></asp:DropDownList>
                                                                <asp:Label ID="lblHostel" runat="server" Style="color: red;" Text="* Enter Hostel Name..." Visible="false"></asp:Label>
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
                                                                Available Block Name :
                                                            </label>
                                                            <div class="col-md-6">
                                                                <asp:ListBox ID="lstEmp" runat="server" CssClass="form-control" SelectionMode="Multiple"></asp:ListBox>
                                                                <%--<a href="#" id="add">add &gt;&gt;</a> --%>
                                                                <asp:Button ID="btnRight" Text=">>" runat="server" OnClick="RightClick" />


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
                                                                Assigned Block Name :
                                                            </label>
                                                            <div class="col-md-6">
                                                                <asp:ListBox ID="lstmoveemp" runat="server" CssClass="form-control" SelectionMode="Multiple"></asp:ListBox>
                                                                <%--<a href="#" id="remove">&lt;&lt; remove</a>  --%>
                                                                <asp:Button ID="btnLeft" Text="<<" runat="server" OnClick="LeftClick" />
                                                            </div>


                                                        </div>
                                                    </div>

                                                </div>
                                            </div>
                                            <div class="clearfix"></div>
                                            <br />
                                            <div class="col-md-6 col-sm-6">
                                                <div class="portlet-body">
                                                    <div class="form-body">
                                                        <div class="form-group">
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
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="lnksave" />
            <asp:PostBackTrigger ControlID="lnkupdate" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
