<%@ Page Title="" Language="C#" MasterPageFile="~/Admin_Master.Master" AutoEventWireup="true" CodeBehind="ProcterAccessLogsList.aspx.cs" Inherits="SMS.ProcterAccessLogsList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Procter Access Logs List</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="CSS/jquery-ui.css" rel="stylesheet" />
    <script src="JS/jquery-ui.min.js"></script>
    <script src="JS/table2excel.js"></script>
    <script type="text/javascript">
        function Export() {
            $("#ContentPlaceHolder1_gridEmployee").table2excel({
                filename: "ProcterAccessLogsList.xls"
            });
        }
        
    </script>
    <asp:UpdatePanel ID="UpdateRefresh" UpdateMode="Always" runat="server" ChildrenAsTriggers="true">
        <ContentTemplate>
            <br />
            <br />
            <div class="page-content-wrapper" style="margin-bottom: -16px;">
                <div class="page-content">
                    <div class="row" id="divgrid" runat="server">
                        <div class="col-md-12">
                            <div class="portlet box green-meadow">
                                <div class="portlet-title">
                                    <div class="caption">
                                        <i class="material-icons">&#xe80b;</i>Procter Access Logs List
                                    </div>
                                    <div class="tools">
                                        <a id="btnExport" onclick="Export()" title="Download Excel" runat="server" visible="false"><i class="fa fa-download" style="font-size: 28px; color: white; margin-top: 4px;"></i></a>
                                    </div>
                                    <div class="tools pull-left" style="margin-left: 10px; padding: 8px; border-left: 0px!important;">
                                        <asp:TextBox ID="txtSearch" runat="server" Width="230px" Height="28px" Style="border: 0px; color: black" />
                                    </div>
                                    <div class="tools pull-left" style="margin-left: 5px; padding: 8px; border-left: 0px!important;">
                                        <asp:LinkButton ID="btnsearch" runat="server" OnClick="btnsearch_Click">
                                        <i class="fa fa-search" style="font-size: 28px; color: white; margin-top: 4px;"></i>
                                        </asp:LinkButton>
                                    </div>
                                </div>
                                <div class="portlet-body" id="grid1" runat="server">
                                    <asp:GridView ID="gridEmployee" runat="server" class="table table-striped table-bordered table-hover" AutoGenerateColumns="false" AllowPaging="true" AllowSorting="true" OnSorting="OnSorting"
                                        OnPageIndexChanging="OnPageIndexChanging" PageSize="5000" ShowHeaderWhenEmpty="true" EmptyDataText="No records Found"
                                        OnRowDataBound="gridEmployee_RowDataBound">
                                        <PagerSettings Mode="NumericFirstLast" PageButtonCount="5" FirstPageText="First" LastPageText="Last" />
                                        <PagerStyle CssClass="gridview"></PagerStyle>
                                        <Columns>
                                            <asp:TemplateField ItemStyle-Width="30px" HeaderText="Student Images" SortExpression="StudentImages">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblpath" Visible="false" runat="server" Text='<%#Eval("StudentImages") %>'></asp:Label>
                                                    <asp:Image ID="imgStudent" runat="server" Visible="false" ImageUrl='<%#Eval("StudentImages") %>' class="circle" />
                                                    <asp:Image ID="imgdefault" runat="server" Visible="false" ImageUrl="~/Images/images1.jpg" class="circle" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="30px" HeaderText="StudentId" SortExpression="Student_Id">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStudentId" runat="server" Text='<%#Eval("Student_Id") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="30px" HeaderText="Student Name" SortExpression="Student_Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStudent_Name" runat="server" Text='<%#Eval("Student_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="30px" HeaderText="Card Number" SortExpression="Card_Number">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCardnumber" runat="server" Text='<%#Eval("Card_Number") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="30px" HeaderText="BGName" SortExpression="BGName">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBGName" runat="server" Text='<%#Eval("BGName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="30px" HeaderText="Access Datetime" SortExpression="Access_Datetime">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAccess_Datetime" runat="server" Text='<%#Eval("Access_Datetime") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="30px" HeaderText="Access Status" SortExpression="Access_Code">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAccess_Code" runat="server" Text='<%#Eval("Access_Code") %>'></asp:Label>
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
</asp:Content>
