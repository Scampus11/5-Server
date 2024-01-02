<%@ Page Title="" Language="C#" MasterPageFile="~/Admin_Master.Master" AutoEventWireup="true" CodeBehind="SMS_Campus_Master.aspx.cs" Inherits="SMS.SMS_Campus_Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Campus</title>
    <style>
        .modal {
            top: 50px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdateRefresh" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <br />
            <br />
            <br />
            <div class="page-content-wrapper" style="margin-bottom: -16px;">
                <div class="page-content">
                    <h3 class="page-title bold">Campus</h3>
                    <div class="row" id="divgrid" runat="server">
                        <div class="col-md-12">
                            <div class="portlet box green-meadow">
                                <div class="portlet-title">
                                    <div class="caption">
                                        <i class='fas'>&#xf549;</i>Campus List
                                    </div>
                                    <div class="tools pull-left" style="margin-left: 10px; padding: 8px; border-left: 0px!important;">
                                        <asp:TextBox ID="txtSearch" runat="server" Width="230px" Height="28px" Style="border: 0px; color: black" />
                                    </div>
                                    <div class="tools pull-left" style="margin-left: 5px; padding: 8px; border-left: 0px!important;">
                                        <asp:LinkButton ID="btnsearch" runat="server" OnClick="btnsearch_Click">
                                        <i class="fa fa-search" title="Search" style="font-size: 28px; color: white; margin-top: 4px;"></i>
                                        </asp:LinkButton>
                                    </div>
                                    <div class="tools" id="divadd" runat="server" visible="true">
                                        <asp:LinkButton ID="lnkAdd" runat="server" class="pull-right" Style="color: white" OnClick="lnkAdd_Click">
                                              <i class="fa fa-plus" title="Add New Campus"></i>
                                        </asp:LinkButton>
                                    </div>
                                    <div class="tools" style="display: none;">
                                        <asp:LinkButton ID="lnksyncjob" runat="server" class="pull-right" Style="color: white" OnClick="lnksyncjob_Click">
                                              <i class="fa fa-laptop" title="Sync job"></i>
                                        </asp:LinkButton>
                                    </div>
                                    <div class="tools">
                                        <asp:LinkButton ID="lnkImportExcel" runat="server" Style="margin-bottom: 10px; color: white;" class="pull-right" OnClick="lnkImportExcel_Click">
                                              <i class="fa fa-plus-circle"></i> Import Excel  
                                        </asp:LinkButton>
                                    </div>
                                </div>

                                <div class="portlet-body" style="overflow: scroll">
                                    <asp:Label ID="lblSyncmsg" runat="server" Visible="false" Text="Sync Completed" Style="color: green;"></asp:Label>
                                    <asp:Label ID="lblSave" runat="server" Text="Insert successfully!!" Style="color: green" Visible="false"></asp:Label>
                                    <asp:GridView ID="gridEmployee" runat="server" class="table table-striped table-bordered table-hover" AutoGenerateColumns="false" AllowPaging="true" AllowSorting="true" OnSorting="OnSorting" OnPageIndexChanging="OnPageIndexChanging" PageSize="10" ShowHeaderWhenEmpty="true" EmptyDataText="No records Found">
                                        <PagerSettings Mode="NumericFirstLast" PageButtonCount="10" FirstPageText="First" LastPageText="Last" />
                                        <PagerStyle CssClass="gridview"></PagerStyle>
                                        <Columns>
                                            <asp:TemplateField ItemStyle-Width="5%" HeaderText="Id" SortExpression="Id">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblId1" runat="server" Text='<%#Eval("Id") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="85%" HeaderText="Campus Name" SortExpression="Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblApplication_No" runat="server" Text='<%#Eval("Name") %>'></asp:Label>
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
                        <div class="row-fluid">
                            <div class="span12">
                                <div class="widget">
                                    <div class="widget-title">

                                        <span class="tools">
                                            <a href="javascript:;" class="icon-chevron-down"></a>
                                        </span>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-body">
                            <div class="portlet-body form">
                                <ul class="nav nav-tabs">
                                    <li class="nav-item active" id="liPersonalDetails" runat="server">
                                        <a class="nav-link" data-toggle="tab" href="#ContentPlaceHolder1_divPersonalDetails" id="aPersonalDetails" runat="server">Campus Details</a>
                                    </li>
                                </ul>
                                <div class="tab-content">
                                    <div class="tab-pane container active" id="divPersonalDetails" runat="server">
                                        <div class="row ">
                                            <div class="col-md-6 col-sm-6">
                                                <div class="portlet-body">
                                                    <div class="form-body">
                                                        <div class="form-group">
                                                            <label class="col-md-3 control-label">
                                                                Campus Name :
                                                            </label>
                                                            <div class="col-md-6">
                                                                <asp:TextBox ID="txtApplication_No" runat="server" CssClass="form-control" BorderColor="#37359C" placeholder="Campus Name" Style="background-color: LightGrey;"></asp:TextBox>
                                                                <asp:Label ID="lblValidCampusname" runat="server" Style="color: red;" Text="* Enter campus name..." Visible="false"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="clearfix"></div>
                                            <div class="col-md-12 col-sm-12">
                                                <div class="portlet-body">
                                                    <div class="form-body">
                                                        <div class="form-group">
                                                            <label class="col-md-3 control-label">
                                                            </label>
                                                            <div class="col-md-12">
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
                    <div class="modal" id="myModal" runat="server" style="margin-bottom: ">
                        <div class="modal-dialog">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h4 class="modal-title">Import Excel File</h4>
                                
                                    <asp:LinkButton ID="lnkdownload" runat="server" style="text-align: right;" CssClass="btn btn-info" Text="Download Excel" OnClick="lnkdownload_Click"></asp:LinkButton>
                                </div>
                                <div class="modal-body">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <label>Choose excel file</label>
                                                <div class="input-group">
                                                    <div class="custom-file">
                                                        <asp:FileUpload ID="FileUpload1" CssClass="custom-file-input" runat="server" />
                                                        <label class="custom-file-label"></label>

                                                    </div>
                                                    <label id="filename"></label>
                                                    <div class="input-group-append">
                                                        <asp:LinkButton ID="lnkUpload" runat="server" CssClass="btn btn-primary" Text="Upload" OnClick="lnkUpload_Click"></asp:LinkButton>
                                                    </div>

                                                </div>
                                                <asp:Label ID="lblMessage" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <asp:LinkButton ID="lnkClose" runat="server" CssClass="btn btn-danger" OnClick="lnkClose_Click">Close</asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <%--<asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />--%>
            <asp:AsyncPostBackTrigger ControlID="lnkImportExcel" />
            <asp:PostBackTrigger ControlID="lnkUpload" />
            <asp:PostBackTrigger ControlID="lnkdownload" />
            
        </Triggers>
    </asp:UpdatePanel>

</asp:Content>
