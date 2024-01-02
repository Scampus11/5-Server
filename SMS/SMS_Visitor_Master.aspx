<%@ Page Title="" Language="C#" MasterPageFile="~/Admin_Master.Master" AutoEventWireup="true" CodeBehind="SMS_Visitor_Master.aspx.cs" Inherits="SMS.SMS_Visitor_Master" ValidateRequest="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Visitors</title>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <link rel="stylesheet" type="text/css" href="VisitorManagement/VisitorManagement.css" />
    <script src="VisitorManagement/dymo.connect.framework.js"></script>
    <%--<script src="VisitorManagement/Layout.js" type="text/javascript" charset="UTF-8"></script>
    <script src="VisitorManagement/VisitorManagement.js" type="text/javascript" charset="UTF-8"></script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <style>
        #my_camera {
            width: 640px;
            height: 480px;
        }

        video {
            border: 2px solid red;
        }

        .modalBackground {
            background-color: gray;
            opacity: 0.7;
        }

        canvas {
            background-color: lightblue
        }
    </style>
    <script type="text/javascript" src="../QR_Code/grid.js"></script>
    <script type="text/javascript" src="../QR_Code/version.js"></script>
    <script type="text/javascript" src="../QR_Code/detector.js"></script>
    <script type="text/javascript" src="../QR_Code/formatinf.js"></script>
    <script type="text/javascript" src="../QR_Code/errorlevel.js"></script>
    <script type="text/javascript" src="../QR_Code/bitmat.js"></script>
    <script type="text/javascript" src="../QR_Code/datablock.js"></script>
    <script type="text/javascript" src="../QR_Code/bmparser.js"></script>
    <script type="text/javascript" src="../QR_Code/datamask.js"></script>
    <script type="text/javascript" src="../QR_Code/rsdecoder.js"></script>
    <script type="text/javascript" src="../QR_Code/gf256poly.js"></script>
    <script type="text/javascript" src="../QR_Code/gf256.js"></script>
    <script type="text/javascript" src="../QR_Code/decoder.js"></script>
    <script type="text/javascript" src="../QR_Code/qrcode.js"></script>
    <script type="text/javascript" src="../QR_Code/findpat.js"></script>
    <script type="text/javascript" src="../QR_Code/alignpat.js"></script>
    <script type="text/javascript" src="../QR_Code/databr.js"></script>
    <script type="text/javascript">

        $(document).ready(function () {
            $('#datetimepicker1').datetimepicker();
            $('#datetimepicker2').datetimepicker();
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
        });
        function EndRequestHandler(sender, args) {
            SetDatePicker();
        }
        function SetDatePicker() {
            $('#datetimepicker1').datetimepicker();
            $('#datetimepicker2').datetimepicker();
        }
    </script>


    <br />
    <br />
    <asp:UpdatePanel ID="UpdateRefresh" UpdateMode="Always" runat="server" ChildrenAsTriggers="true">
        <ContentTemplate>
            <div class="page-content-wrapper">
                <div class="page-content">
                    <asp:LinkButton ID="lnkAdd" runat="server" Text="Add New Officer" class="btn green-meadow pull-right" OnClick="lnkAdd_Click" Visible="false"></asp:LinkButton>
                    <asp:LinkButton ID="lnkviewgrid" runat="server" Text="View Officer-list" class="btn green-meadow pull-right" OnClick="lnkviewgrid_Click" Visible="false" Style="display: none"></asp:LinkButton>
                    <asp:LinkButton ID="lnkEdit_menu" runat="server" Text="Edit Officer" class="btn green-meadow pull-right" OnClick="lnkEdit_menu_Click" Visible="false" Style="display: none"></asp:LinkButton>
                    <asp:LinkButton ID="lnkView_Menu" runat="server" Text="View Officer" class="btn green-meadow pull-right" OnClick="lnkView_Menu_Click" Visible="false" Style="display: none"></asp:LinkButton>
                    <h3 runat="server" id="h3visitor" class="page-title bold">Visitors
                    </h3>
                    <h3 runat="server" id="h1previsitor" visible="false" class="page-title bold">Visitor Pre Registration
                    </h3>
                    <h3 runat="server" id="h1selfvisitor" visible="false" class="page-title bold">Visitor Self Registration
                    </h3>
                    <h3 runat="server" id="h1checkinout" visible="false" class="page-title bold">Visitor Details
                    </h3>
                    <div class="row" id="divgrid" runat="server">
                        <div class="col-md-12">
                            <div class="portlet box green-meadow">
                                <div class="portlet-title">
                                    <div class="caption">
                                        <i class='fas'>&#xf554;</i>Visitors List
                                    </div>
                                    <div class="tools pull-left" style="margin-left: 10px; padding: 8px; border-left: 0px!important;">
                                        <asp:TextBox ID="txtSearch" placeholder="Search" runat="server" Width="230px" Height="28px" Style="border: 0px; color: black" />
                                    </div>
                                    <div class="tools pull-left" style="margin-left: 5px; padding: 8px; border-left: 0px!important;">
                                        <asp:LinkButton ID="btnsearch" ToolTip="Search" runat="server" OnClick="btnsearch_Click">
                                        <i class="fa fa-search" style="font-size: 28px; color: white; margin-top: 4px;"></i>
                                        </asp:LinkButton>
                                    </div>
                                    <div class="tools">
                                        <a class="pull-right" style="color: white" href="SMS_Visitor_Master.aspx?Add=Add pre-Reg">
                                            <i class="fa fa-plus" title="Add New Pre-Registration"></i></a>
                                    </div>
                                    <div class="tools">
                                        <%-- <a class="pull-right" style="color: white" href="SMS_Visitor_Master.aspx?QRCode=Scan QR">
                                    <i class="fa fa-qrcode" style="width:20px;height:20px;" title="Scan QR"></i></a>--%>
                                        <asp:LinkButton ID="lnkQrCode" class="pull-right" Style="color: white" runat="server" OnClientClick="Webcam();">
                                    <i class="fa fa-qrcode" style="width:20px;height:20px;" title="Scan QR"></i>
                                        </asp:LinkButton>
                                    </div>
                                    <div class="tools">
                                        <%--<a class="pull-right" style="color: white" href="SMS_Visitor_Master.aspx?Search_Registration_Number=Search Registration Number">
                                    <i class="fa fa-plus-circle" title="Search Registration Number"></i></a>--%>
                                        <asp:LinkButton ID="lnkregnum" class="pull-right" Style="color: white" runat="server" OnClick="lnkregnum_Click">
                                    <i class="fa fa-search" title="Search Registration Number"></i>
                                        </asp:LinkButton>

                                    </div>
                                </div>
                                <div class="portlet-body" style="overflow: scroll">
                                    <asp:GridView ID="gridEmployee" runat="server" class="table table-striped table-bordered table-hover" AutoGenerateColumns="false" AllowPaging="true" AllowSorting="true" OnSorting="OnSorting" OnPageIndexChanging="OnPageIndexChanging" PageSize="10" OnRowDataBound="gridEmployee_RowDataBound" ShowHeaderWhenEmpty="true" EmptyDataText="No records Found">
                                        <PagerSettings Mode="NumericFirstLast" PageButtonCount="5" FirstPageText="First" LastPageText="Last" />
                                        <PagerStyle CssClass="gridview"></PagerStyle>
                                        <Columns>
                                            <asp:TemplateField ItemStyle-Width="20px" HeaderText="Photo" SortExpression="Visitor_Photo">
                                                <ItemTemplate>
                                                    <asp:Image ID="tmgstudent" runat="server" ImageUrl='<%#Eval("Visitor_Photo")==""?"~/Images/visitors.png":Eval("Visitor_Photo") %>' class="circle" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="30px" HeaderText="Visitor Reg No" SortExpression="Visitor_Reg_No">
                                                <ItemTemplate>
                                                    <%--<asp:LinkButton ID="lnkvis" runat="server" PostBackUrl='<%# String.Format("~/SMS_Visitor_Master.aspx?CopyVisitor={0}", Eval("Visitor_Reg_No"))%>' Text='<%# Eval("Visitor_Reg_No") %>'></asp:LinkButton> --%>
                                                    <a href="/SMS_Visitor_Master.aspx?CopyVisitor=<%#Eval("Visitor_Reg_No")%>"><%#Eval("Visitor_Reg_No")%></a>
                                                    <asp:Label ID="lblVisitor_Reg_No" runat="server" Visible="false" Text='<%#Eval("Visitor_Reg_No") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="30px" HeaderText="SLN ACS Visitor Info" SortExpression="SLN_ACS_Visitor_Info" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSLN_ACS_Visitor_Info" runat="server" Text='<%#Eval("SLN_ACS_Visitor_Info") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="30px" HeaderText="Visitor RecId" SortExpression="Visitor_RecId" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblVisitor_RecId" runat="server" Text='<%#Eval("Visitor_RecId") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="30px" HeaderText="Visitor GUID" SortExpression="Visitor_GUID" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblVisitor_GUID" runat="server" Text='<%#Eval("Visitor_GUID") %>'></asp:Label>
                                                    <asp:Label ID="lblid" runat="server" Text='<%#Eval("SLN_ACS_Visitor_Info") %>' Visible="false"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="100px" HeaderText="Name" SortExpression="First_Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFull_Name" runat="server" Text='<%#Eval("First_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="100px" HeaderText="Email Id" SortExpression="Email_Id">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEmail_Id" runat="server" Text='<%#Eval("Email_Id") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="100px" HeaderText="Phone Number" SortExpression="Phone_Number">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPhone_Number" runat="server" Text='<%#Eval("Phone_Number") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="100px" HeaderText="Visit Reason" SortExpression="Visit_Reason">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblVisit_Reason" runat="server" Text='<%#Eval("Visit_Reason") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="100px" HeaderText="Check In" SortExpression="Check_In">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCheckin" runat="server" Text='<%#Eval("Check_In") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="100px" HeaderText="Check Out" SortExpression="Check_Out">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCheckout" runat="server" Text='<%#Eval("Check_Out") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="100px" HeaderText="Visitor Type" SortExpression="Visitor_Type">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblVisitor_Type" runat="server" Text='<%#Eval("Visitor_Type") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="100px" HeaderText="Access Level" Visible="false" SortExpression="Access_Level">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAccess_Level" runat="server" Text='<%#Eval("Access_Level") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="100px" HeaderText="Card Number" SortExpression="Access_Card_Number">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAccess_CardNumber" runat="server" Text='<%#Eval("Access_Card_Number") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="100px" HeaderText="Valid From" Visible="false" SortExpression="Valid_From_Datetime">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblValid_From_Datetime" runat="server" Text='<%#Eval("Valid_From_Datetime") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="100px" HeaderText="Valid To" Visible="false" SortExpression="Valid_To_Datetime">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblValid_To_Datetime" runat="server" Text='<%#Eval("Valid_To_Datetime") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="100px" HeaderText="Visitor Registor Status" SortExpression="Visitor_PreReg_Status">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblVisitor_PreReg_Status" runat="server" Text='<%#Eval("Visitor_PreReg_Status") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="10px" HeaderText="Serve Status" SortExpression="SLN_ACS_Visitor_Info">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkServedby" runat="server" Visible="false" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "SLN_ACS_Visitor_Info")%>' OnClick="lnkServedby_Click"><i class="fa fa-bell" title="Assign Emp services"></i></asp:LinkButton>
                                                    <asp:LinkButton ID="lnkClosed" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "SLN_ACS_Visitor_Info")%>' OnClick="lnkClosed_Click" Visible="false"><i class="fa fa-bell" title="Served" style="color:greenyellow"></i></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="10px" HeaderText="Asign Card Number" Visible="false">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkCardNumber" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "SLN_ACS_Visitor_Info")%>' OnClick="lnkCardNumber_Click"><i class="fa fa-plus-circle" title="Asign CardNumber"></i></asp:LinkButton>
                                                    <asp:Label ID="lblcompleted" runat="server" Text="Completed" ForeColor="Green" Visible="false"></asp:Label>
                                                    <asp:LinkButton ID="lnkRemoveCardNo" class="btn green-meadow" runat="server" Text="RemoveCardNumber" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "SLN_ACS_Visitor_Info")%>' OnClick="lnkRemoveCardNo_Click"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="100px" HeaderText="View" Visible="false">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="linkView" class="btn green-meadow" runat="server" Text="View" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "SLN_ACS_Visitor_Info")%>' OnClick="linkView_Click"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="10px" HeaderText="Edit" SortExpression="SLN_ACS_Visitor_Info">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="linkEdit" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "SLN_ACS_Visitor_Info")%>' OnClick="linkEdit_Click"><i class="material-icons" title="Edit row">&#xe3c9;</i></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-content" visible="false" id="divasignCarsnumber" runat="server">
                        <div class="modal-body">
                            <div class="portlet-body form">
                                <div class="form-body" runat="server">
                                    <div class="col-md-6 col-sm-6">
                                        <div class="portlet-body">
                                            <div class="form-body">
                                                <div class="form-group">
                                                    <label class="col-md-3 control-label">
                                                        First Name :
                                                    </label>

                                                    <div class="col-md-6">
                                                        <asp:Label ID="lblAsignFirstName" runat="server" CssClass="form-control"></asp:Label>
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
                                                        Last Name :
                                                    </label>
                                                    <div class="col-md-6">
                                                        <asp:Label ID="lblAsignLastName" runat="server" CssClass="form-control"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="clearfix"></div>
                                    <br />
                                    <br />
                                    <br />
                                    <asp:LinkButton ID="LnkAsignUpdate" runat="server" Text="Update" OnClick="LnkAsignUpdate_Click" class="btn green-meadow" />
                                    <asp:LinkButton ID="LinkButton3" runat="server" Text="Back" OnClick="lnkBack_Click" class="btn green-meadow" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-content" visible="false" id="divServedBy" runat="server">
                        <div class="modal-body">
                            <div class="portlet-body form">
                                <div class="form-body" runat="server">
                                    <div class="col-md-6 col-sm-6">
                                        <div class="portlet-body">
                                            <div class="form-body">
                                                <div class="form-group">
                                                    <label class="col-md-3 control-label">
                                                        Service Name :
                                                    </label>

                                                    <div class="col-md-6">
                                                        <asp:DropDownList ID="ddlservices" runat="server" CssClass="form-control"></asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="clearfix"></div>
                                    <asp:Label ID="lblservedbyemp" runat="server" Visible="false" Style="color: greenyellow; font-size: medium" Font-Bold="true"></asp:Label>
                                    <div class="clearfix"></div>
                                    <br />
                                    <br />
                                    <br />
                                    <asp:LinkButton ID="lnkServedBy" runat="server" Text="Serve" OnClick="lnkServedBy_Click1" class="btn green-meadow" />
                                    <asp:LinkButton ID="LinkButton2" runat="server" Text="Back" OnClick="lnkBack_Click" class="btn green-meadow" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-content" visible="false" id="DivViewServedby" runat="server">
                        <div class="modal-body">
                            <div class="portlet-body form">
                                <div class="form-body" runat="server">
                                    <div class="form-group">
                                        <div id="divServed" runat="server"></div>
                                    </div>
                                    <br />
                                    <asp:LinkButton ID="LinkButton1" runat="server" Text="Back" OnClick="lnkBack_Click" class="btn green-meadow" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-content" id="divView" runat="server" visible="false">
                        <div class="modal-body">
                            <div class="portlet-body form">
                                <div class="form-body" runat="server" visible="false" id="divSLN">
                                    <div class="col-md-6 col-sm-6">
                                        <div class="portlet-body">
                                            <div class="form-body">
                                                <div class="form-group">
                                                    <label class="col-md-3 control-label">
                                                        First Name :
                                                    </label>
                                                    <div class="col-md-6">
                                                        <asp:TextBox ID="txtFirst_Name" runat="server" CssClass="form-control" BackColor="#d3d3d3"></asp:TextBox>
                                                        <asp:Label ID="lblvalidfirstname" runat="server" Style="color: red;" Text="* Enter first name..." Visible="false"></asp:Label>
                                                        <asp:Label ID="lblVisitorRegno" runat="server" Visible="false"></asp:Label>
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
                                                        Last Name :
                                                    </label>
                                                    <div class="col-md-6">
                                                        <asp:TextBox ID="txtLast_Name" runat="server" CssClass="form-control"></asp:TextBox>
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
                                                        Company Name :
                                                    </label>

                                                    <div class="col-md-6">
                                                        <asp:TextBox ID="txtCompany" runat="server" CssClass="form-control"></asp:TextBox>
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
                                                        Visitor Type :
                                                    </label>
                                                    <div class="col-md-6">
                                                        <asp:DropDownList ID="ddlVisitor_Type" runat="server" CssClass="form-control">
                                                        </asp:DropDownList>
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
                                                        Visit Reason :
                                                    </label>
                                                    <div class="col-md-6">
                                                        <asp:DropDownList ID="ddlVisit_Reason" runat="server" CssClass="form-control">
                                                        </asp:DropDownList>
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
                                                        Phone Number :
                                                    </label>
                                                    <div class="col-md-6">
                                                        <asp:TextBox ID="txtPhone_Number" runat="server" MaxLength="10" CssClass="form-control"></asp:TextBox>
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
                                                        Email Id :
                                                    </label>

                                                    <div class="col-md-6">
                                                        <asp:TextBox ID="txtEmail_Id" runat="server" CssClass="form-control" BackColor="#d3d3d3"></asp:TextBox>
                                                        <span id="error" style="display: none; color: red;">Wrong email</span>
                                                        <asp:Label ID="lblEmailId" runat="server" Style="color: red;" Text="* Enter Email Id..." Visible="false"></asp:Label>
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
                                                        National Id :
                                                    </label>
                                                    <div class="col-md-6">
                                                        <asp:TextBox ID="txtNational_ID" runat="server" CssClass="form-control"></asp:TextBox>
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
                                                        Available Services :
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
                                                        Assigned  Services :
                                                    </label>
                                                    <div class="col-md-6">
                                                        <asp:ListBox ID="lstmoveemp" runat="server" CssClass="form-control" SelectionMode="Multiple"></asp:ListBox>
                                                        <%--<a href="#" id="remove">&lt;&lt; remove</a>  --%>
                                                        <asp:Button ID="btnLeft" Text="<<" runat="server" OnClick="LeftClick" />
                                                        <asp:Label ID="lblvalidmove" Style="color: red;" runat="server" Visible="false" Text="* Please select atleast one service"></asp:Label>
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
                                                        Visitor Photo :
                                                    </label>

                                                    <div class="col-md-6">
                                                        <asp:Image ID="imgStaff" runat="server" Height="120px" Width="200px" ImageUrl="~/Images/visitors.png" /><br />
                                                        <label class="btn btn-default">
                                                            <asp:FileUpload ID="FileUpload1" runat="server" Style="color: blue" />
                                                        </label>
                                                        <asp:HiddenField ID="hidValue" Value="" runat="server" />
                                                        <asp:LinkButton ID="DivUpload" runat="server" class="btn btn-info" OnClientClick="configure()">WebCam</asp:LinkButton>
                                                        <span style="color: darkviolet">Note : Only Image Suported Format is .jpg | .jpeg | .png | .bmp | .gif | .eps...</span>
                                                        <asp:Label ID="lblf1" runat="server" Style="color: red;" Text="You upload image format not suppoted.." Visible="false"></asp:Label>
                                                        <asp:Image ID="Image1" Style="display: none;" runat="server" Height="120px" Width="200px" ImageUrl="~/Images/visitors.png" />
                                                        <asp:Image ID="imglogo" Style="display: none;" runat="server" Height="120px" Width="200px" ImageUrl="~/Images/visitors.png" />
                                                        <asp:Image ID="imgvisitor" Style="display: none;" runat="server" Height="120px" Width="200px" ImageUrl="~/Images/visitors.png" />
                                                        <asp:TextBox ID="txtlogoname" Style="display: none;" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <asp:Label ID="lblvisitor" runat="server" Style="display: none;"></asp:Label>
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
                                                        Visitor Status :
                                                    </label>

                                                    <div class="col-md-6">
                                                        <asp:DropDownList ID="ddlCardstatus" runat="server" CssClass="form-control">
                                                            <asp:ListItem Value="1" Selected="True">Active</asp:ListItem>
                                                            <asp:ListItem Value="2">Revoked</asp:ListItem>
                                                            <asp:ListItem Value="3">Lost</asp:ListItem>
                                                            <asp:ListItem Value="4">Suspended</asp:ListItem>
                                                            <asp:ListItem Value="5">Expired</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6 col-sm-6" id="divValidFromDatetime" runat="server">
                                        <div class="portlet-body">
                                            <div class="form-body">
                                                <div class="form-group">
                                                    <label class="col-md-3 control-label">Valid From : </label>
                                                    <div class="col-md-6">
                                                        <div class='input-group date' id='datetimepicker1'>
                                                            <asp:TextBox ID="txtValidFromDatetime" runat="server" CssClass="form-control" BackColor="#d3d3d3" placeholder="Valid From"></asp:TextBox>
                                                            <span class="input-group-addon">
                                                                <span class="glyphicon glyphicon-calendar"></span>
                                                            </span>
                                                        </div>
                                                        <asp:Label ID="lblvalidValidDateUntil2" runat="server" Style="color: red;" Text="*Valid to does not less than valid from ..." Visible="false"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6 col-sm-6" id="divValidToDatetime" runat="server">
                                        <div class="portlet-body">
                                            <div class="form-body">
                                                <div class="form-group">
                                                    <label class="col-md-3 control-label">Valid To : </label>
                                                    <div class="col-md-6">
                                                        <div class='input-group date' id='datetimepicker2'>
                                                            <asp:TextBox ID="txtValidToDatetime" runat="server" CssClass="form-control" BackColor="#d3d3d3" placeholder="Valid To"></asp:TextBox>
                                                            <span class="input-group-addon">
                                                                <span class="glyphicon glyphicon-calendar"></span>
                                                            </span>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6 col-sm-6" id="divAccess" runat="server" visible="false">
                                        <div class="portlet-body">
                                            <div class="form-body">
                                                <div class="form-group">
                                                    <label class="col-md-3 control-label">
                                                        Access Type :
                                                    </label>

                                                    <div class="col-md-6">
                                                        <asp:DropDownList ID="ddlbadgeaccess" runat="server" CssClass="form-control" OnTextChanged="ddlbadgeaccess_TextChanged" AutoPostBack="true">
                                                            <%--<asp:ListItem Value="0">Select Access</asp:ListItem>--%>
                                                            <asp:ListItem Value="1">Card access</asp:ListItem>
                                                            <asp:ListItem Value="2">Badge access</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6 col-sm-6" id="divregno" runat="server" visible="false">
                                        <div class="portlet-body">
                                            <div class="form-body">
                                                <div class="form-group">
                                                    <label class="col-md-3 control-label">
                                                        Visitor Reg No :
                                                    </label>

                                                    <div class="col-md-6">
                                                        <asp:TextBox ID="txtvisitorregno" runat="server" CssClass="form-control" MaxLength="20"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6 col-sm-6" id="divCardNumber_Status" runat="server" visible="false">
                                        <div class="portlet-body">
                                            <div class="form-body">
                                                <div class="form-group">
                                                    <label class="col-md-3 control-label">
                                                        Card Number :
                                                    </label>

                                                    <div class="col-md-5">
                                                        <asp:TextBox ID="txtAsignCardNumber" runat="server" CssClass="form-control" MaxLength="20"></asp:TextBox>
                                                        <asp:Label ID="lblvalidationcard" runat="server" Text="Enter Card Number" Visible="false" Style="color: red"></asp:Label>
                                                        <asp:Label ID="lblAlreadyAssign" runat="server" Text="Card Number Already Asigned ! Please select Another Card Number" Visible="false" Style="color: red"></asp:Label>
                                                        <asp:Label ID="lblLost" runat="server" Text="Card Number Is Expired or  Lost/ Demanged ! Please select Another Card Number" Visible="false" Style="color: red"></asp:Label>
                                                        <asp:Label ID="lblNotAvailable" runat="server" Text="Card Number Not Available" Style="color: red" Visible="false"></asp:Label>

                                                    </div>
                                                    <div class="col-md-1">
                                                        <asp:LinkButton ID="lnkcardnumber" runat="server" OnClick="lnkcardnumber_Click1" class="btn btn-info btn-lg" Style="height: 35px; margin-left: -26px;"> <span class="glyphicon glyphicon-random"></span></asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="clearfix"></div>
                                    <hr />
                                    <div class="col-md-4 col-sm-4" id="divmain" style="display: none;">
                                        <div id="labelImageDiv" style="width: 400px; margin-left: 20px; text-align: center;">
                                            <img id="labelImage" src="" alt="label preview" />
                                        </div>
                                        <div class="printControls" style="margin-left: 20px; background-color: white;">
                                            <div id="printersDiv">
                                                <label for="printersSelect">Printer:</label>
                                                <select id="printersSelect"></select>
                                            </div>
                                            <a class="toolbar-button" id="changeLayoutButton" style="display: none;" href="javascript:void(0)">Change Layout</a>
                                            <a class="toolbar-button" style="background-color: white; padding: 0px"></a>
                                        </div>
                                    </div>
                                    <asp:TextBox ID="txtxml" runat="server" Style="display: none;"></asp:TextBox>
                                    <div class="col-md-5 col-sm-5"></div>
                                    <%--Display:none--%>
                                    <div class="form-group" style="display: none">
                                        <label class="text-success">ID_Number : </label>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="description" style="width: 100%; border: 1px solid #ddd; padding: 10px; border-radius: 5px">
                                                    <div class="row">
                                                        <div class="col-md-12" id="Div9">
                                                            <div class="input-group">
                                                                <asp:TextBox ID="txtID_Number" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group" style="display: none">
                                        <div class="description" style="width: 100%; border: 1px solid #ddd; padding: 10px; border-radius: 5px">
                                            <p class="text-success">
                                                SLN_ACS :
                                            </p>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <asp:TextBox ID="txtSLN_ACS_Visitor_Info" runat="server"></asp:TextBox>
                                                    <asp:Label ID="lblSLN_ACS_Visitor_Info" runat="server" Visible="false"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group" style="display: none">
                                        <label class="text-success">Visitor Recored Id : </label>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="description" style="width: 100%; border: 1px solid #ddd; padding: 10px; border-radius: 5px">
                                                    <div class="row">
                                                        <div class="col-md-12" id="Div1">
                                                            <div class="input-group">
                                                                <asp:TextBox ID="txtVisitor_RecId" runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group" style="display: none">
                                        <label class="text-success">Visitor_GUID : </label>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="description" style="width: 100%; border: 1px solid #ddd; padding: 10px; border-radius: 5px">
                                                    <div class="row">
                                                        <div class="col-md-12" id="Div2">
                                                            <div class="input-group">
                                                                <asp:TextBox ID="txtVisitor_GUID" runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group" style="display: none">
                                        <label class="text-success">Host_Employee_Code : </label>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="description" style="width: 100%; border: 1px solid #ddd; padding: 10px; border-radius: 5px">
                                                    <div class="row">
                                                        <div class="col-md-12" id="Div11">
                                                            <div class="input-group">
                                                                <asp:TextBox ID="txtHost_Employee_Code" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group" visible="false" runat="server" id="divCardNumber">
                                        <label class="text-success">Access_Card_Number : </label>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="description" style="width: 100%; border: 1px solid #ddd; padding: 10px; border-radius: 5px">
                                                    <div class="row">
                                                        <div class="col-md-12" id="Div12">
                                                            <div class="input-group">
                                                                <asp:TextBox ID="txtAccess_Card_Number" runat="server" MaxLength="8" CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group" style="display: none">
                                        <label class="text-success">Valid_From : </label>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="description" style="width: 100%; border: 1px solid #ddd; padding: 10px; border-radius: 5px">
                                                    <div class="row">
                                                        <div class="col-md-12" id="Div14">
                                                            <div class="input-group">
                                                                <asp:TextBox ID="txtValid_From" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group" style="display: none">
                                        <label class="text-success">Valid_To : </label>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="description" style="width: 100%; border: 1px solid #ddd; padding: 10px; border-radius: 5px">
                                                    <div class="row">
                                                        <div class="col-md-12" id="Div15">
                                                            <div class="input-group">
                                                                <asp:TextBox ID="txtValid_To" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group" style="display: none;">
                                        <label class="text-success">Access_Level : </label>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="description" style="width: 100%; border: 1px solid #ddd; padding: 10px; border-radius: 5px">
                                                    <div class="row">
                                                        <div class="col-md-12" id="Div16">
                                                            <div class="input-group">
                                                                <asp:DropDownList ID="ddlAccess_Level" runat="server" CssClass="form-control">
                                                                    <%--<asp:DropDownList ID="ddlAccess_Level" runat="server" CssClass="form-control">
                                                            <asp:ListItem Value="Full Access">Full Access</asp:ListItem>
                                                            <asp:ListItem Value="Limited Access">Limited Access</asp:ListItem>
                                                            <asp:ListItem Value="Session Access">Session Access</asp:ListItem>
                                                            <asp:ListItem Value="Other">Other</asp:ListItem>--%>
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group" style="display: none">
                                        <label class="text-success">Check_In : </label>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="description" style="width: 100%; border: 1px solid #ddd; padding: 10px; border-radius: 5px">
                                                    <div class="row">
                                                        <div class="col-md-12" id="Div18">
                                                            <div class="input-group">
                                                                <asp:TextBox ID="txtCheck_In" runat="server" Enabled="false"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group" style="display: none">
                                        <label class="text-success">Check_Out : </label>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="description" style="width: 100%; border: 1px solid #ddd; padding: 10px; border-radius: 5px">
                                                    <div class="row">
                                                        <div class="col-md-12" id="Div19">
                                                            <div class="input-group">
                                                                <asp:TextBox ID="txtCheck_Out" runat="server" Enabled="false"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group" style="display: none">
                                        <label class="text-success">CreatedBy : </label>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="description" style="width: 100%; border: 1px solid #ddd; padding: 10px; border-radius: 5px">
                                                    <div class="row">
                                                        <div class="col-md-12" id="Div20">
                                                            <div class="input-group">
                                                                <asp:TextBox ID="txtCreatedBy" runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group" style="display: none">
                                        <label class="text-success">LastUpdatedBy : </label>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="description" style="width: 100%; border: 1px solid #ddd; padding: 10px; border-radius: 5px">
                                                    <div class="row">
                                                        <div class="col-md-12" id="Div21">
                                                            <div class="input-group">
                                                                <asp:TextBox ID="txtLastUpdatedBy" runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="clearfix"></div>
                                    <div class="col-md-12 col-sm-12" style="text-align: right;">
                                        <div class="portlet-body">
                                            <div class="form-body">
                                                <div class="form-group">
                                                    <label class="col-md-1 control-label">
                                                        <button id="preview" runat="server" style="display: none;" class="btn green-meadow" onclick="previewdata()">Preview</button>

                                                    </label>
                                                    <label class="col-md-1 control-label">
                                                        <button id="lnkprint" runat="server" style="display: none;" class="btn green-meadow" onclick="Printlable()">Print</button>
                                                    </label>
                                                    <div class="col-md-10">
                                                        <asp:Label ID="lblStatus" runat="server"></asp:Label>
                                                        <asp:Label ID="lblstatusNone" Visible="false" runat="server"></asp:Label>

                                                        <asp:LinkButton ID="lnksave" runat="server" Text="Pre-Reg" OnClick="btnsave_Click" class="btn green" Visible="false" />
                                                        <asp:LinkButton ID="lnkCheckIn" runat="server" Text="CheckIn" OnClick="lnkCheckIn_Click" class="btn blue" Visible="false" />
                                                        <asp:LinkButton ID="lnkCheckOut" runat="server" Text="CheckOut" OnClick="lnkCheckOut_Click" class="btn red" Visible="false" />
                                                        <asp:LinkButton ID="lnkCopy" runat="server" Text="Copy Visitor" OnClick="lnkCopy_Click" class="btn green-meadow" Visible="false" />
                                                        <asp:LinkButton ID="lnkupdate" runat="server" Text="Update" OnClick="btnupdate_Click" class="btn green-meadow" Visible="false" />
                                                        <asp:LinkButton ID="lnkpushtos2" runat="server" Text="Push to S2" Style="background-color: fuchsia; display: none;" class="btn blue" OnClick="lnkpushtos2_Click" Visible="false" />
                                                        <asp:LinkButton ID="lnkmodifys2" runat="server" Text="Push to S2" Style="background-color: fuchsia; display: none;" class="btn blue" OnClick="lnkmodifys2_Click" Visible="false" />
                                                        <asp:LinkButton ID="lnkBack" runat="server" Text="Back" OnClick="lnkBack_Click" class="btn green-meadow" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <br />

                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-content" visible="false" id="divScanQR" runat="server">
                        <div class="modal-body">
                            <div class="portlet-body form">
                                <div class="form-body" runat="server">


                                    <div class="clearfix"></div>
                                    <br />
                                    <br />
                                    <br />

                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-content" visible="false" id="divSearchRegistrationNumber" runat="server">
                        <div class="modal-body">
                            <div class="portlet-body form">
                                <div class="form-body" runat="server">


                                    <div class="clearfix"></div>
                                    <br />
                                    <br />
                                    <br />

                                    <asp:LinkButton ID="LinkButton5" runat="server" Text="Back" OnClick="lnkBack_Click" class="btn green-meadow" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnRight" />
            <asp:AsyncPostBackTrigger ControlID="btnLeft" />
            <asp:PostBackTrigger ControlID="lnksave" />
            <asp:PostBackTrigger ControlID="lnkupdate" />
            <asp:PostBackTrigger ControlID="lnkCheckIn" />
            <asp:PostBackTrigger ControlID="lnkCheckOut" />
            <asp:PostBackTrigger ControlID="lnkpushtos2" />
            <asp:PostBackTrigger ControlID="lnkmodifys2" />
            <asp:AsyncPostBackTrigger ControlID="DivUpload" />
            <asp:PostBackTrigger ControlID="lnkregnum" />
        </Triggers>
    </asp:UpdatePanel>
    <div>
        <!-- The Modal -->
        <div id="myModal" runat="server" class="modal">
            <!-- Modal content -->
            <div class="modal-content" style="top: 100px; left: 320px; width: 700px; height: 200px;">
                <br />
                <br />
                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                    <ContentTemplate>
                        <div style="text-align: center;">
                            <h5><b style="color: green">
                                <asp:Label ID="lblmsg" runat="server"></asp:Label></b></h5>
                            <br />
                            <h5><b style="color: green">
                                <asp:Label ID="lblS2" Text="Do you want to push this to S2 system ?" runat="server" Visible="false"></asp:Label>
                            </b></h5>
                        </div>
                        <br />
                        <div style="text-align: center;">
                            <asp:LinkButton ID="lnkOkk" runat="server" Text="ok" OnClick="lnkpushCancel_Click" class="btn green-meadow" Visible="false" />
                            <asp:LinkButton ID="lnkGo" runat="server" Text="Yes" OnClick="lnkGo_Click" class="btn green-meadow" Visible="false" />
                            <asp:LinkButton ID="lnkpushCancel" runat="server" Text="No" OnClick="lnkpushCancel_Click" class="btn green-meadow" Visible="false" />
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="lnkOkk" />
                        <asp:AsyncPostBackTrigger ControlID="lnkGo" />
                        <asp:AsyncPostBackTrigger ControlID="lnkpushCancel" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>

        </div>
        <div id="mymodal2" runat="server" class="modal">

            <!-- Modal content -->
            <div class="modal-content" style="top: 100px; left: 320px; width: 700px; height: 200px;">
                <br />
                <br />
                <div style="text-align: center;">
                    <h5><b style="color: red">
                        <asp:Label ID="lblqr" runat="server"></asp:Label></b></h5>
                </div>
                <br />
                <div style="text-align: center;">
                    <asp:LinkButton ID="lnkok" runat="server" Text="ok" OnClick="lnkok_Click" class="btn green-meadow" />
                </div>
            </div>

        </div>
        <asp:LinkButton ID="LinkButton4" ForeColor="#ffffff" runat="server"></asp:LinkButton>
        <cc1:ModalPopupExtender ID="ModalPopupExtender2" BehaviorID="mpe" runat="server"
            PopupControlID="Panel1" TargetControlID="LinkButton4" BackgroundCssClass="modalBackground">
        </cc1:ModalPopupExtender>
        <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup" Style="display: none; left: 84.5px!important; background: #F7F7F7 url('../Images/body-bg.png') repeat scroll 0% 0%;">
            <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Always" runat="server" ChildrenAsTriggers="true">
                <ContentTemplate>
                    <div class="modal-content" id="div3" runat="server" style="background-color: #fff">
                        <div class="modal-body" style="width: 500px!important; position: relative;">
                            <div class="portlet-body form">
                                <div class="form-body">
                                    <div runat="server" id="divAdvanceSearch">
                                        <div class="row">
                                        </div>

                                        <br />

                                        <div class="row">
                                            <div class='col-sm-8  pull-left'></div>
                                            <div class='col-sm-4  pull-right'>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="lnkGo" />

                </Triggers>
            </asp:UpdatePanel>
        </asp:Panel>
    </div>
    <div>
       <div id="ModalPopupExtender1" runat="server" class="modal" style="left: 410.5px !important;display: none;margin-top: 150px;max-width: 786px;height: 700px;text-align: -webkit-center;">
            <div class="modal-content" style="margin-top: 150px;max-width: 786px;height: 270px; left: 410.5px !important;">
                <br />
                <br />
                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                    <ContentTemplate>
                        <div>
                            <div>
                                <div >
                                    <div runat="server" id="div5">
                                        <div class="row" style="margin-left:30px;margin-right:30px;">
                                            <div class='col-sm-12 pull-left'>
                                                <div class="form-group">
                                                    <span>Visitor Registration Number : </span>
                                                    <asp:TextBox ID="txtSearchRegNumber" runat="server" CssClass="form-control" placeholder="Enter Visitor Reg Number" Style="background-color: LightGrey;"></asp:TextBox>
                                                    <asp:Label ID="lblValidationSearchRegNumber" runat="server" Text="Enter Visitor Registration Number" Visible="false" Style="color: red"></asp:Label>
                                                    <asp:Label ID="lblValidationSRNNotValid" runat="server" Text="This visitor registration number is not valid.please enter valid visitor registration number" Visible="false" Style="color: red"></asp:Label>
                                                </div>
                                            </div>
                                        </div>

                                        <br />

                                        <div class="row">
                                            <div class='col-sm-7  pull-left'></div>
                                            <div class='col-sm-5  pull-right'>
                                                <asp:LinkButton ID="lnkSearchRegNum" runat="server" Text="Search" OnClick="lnkSearchRegNum_Click" class="btn green-meadow" />
                                                <a id="lnkclose" class="btn btn-primary"  onclick="HidePopup();">Close</a>
                                                    
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <script type="text/javascript">
                            function HidePopup() {
                                $("#ContentPlaceHolder1_ModalPopupExtender1").hide();
                                $("#ContentPlaceHolder1_ModalPopupExtender3").hide();
                            }

                        </script>
                    </ContentTemplate>
                    <Triggers>
                         <asp:AsyncPostBackTrigger ControlID="lnkGo" />
                         <%--<asp:AsyncPostBackTrigger ControlID="Button1" />--%>
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <div>
        <div id="ModalPopupExtender3" runat="server" class="modal" style="left: 410.5px !important;display: none;margin-top: 150px;max-width: 786px;height: 700px;text-align: -webkit-center;">
            <div class="modal-content" id="div6" runat="server" style="background-color: #fff">
                        <div class="modal-body" style="width: 600px!important; position: relative;">
                            <div class="portlet-body form">
                                <div class="form-body">
                                    <div runat="server" id="div7">
                                        <div class="row">
                                            <div class="col-md-12 col-sm-12">
                                                <div class="portlet-body">
                                                    <div class="form-body">
                                                        <div class="form-group">
                                                            <div class="col-md-12">
                                                                <video id="player" style="display: none;" controls autoplay></video>
                                                                <canvas id="qr-canvas" width="500" height="400"></canvas>

                                                            </div>
                                                            <div class="col-md-6">
                                                                <div>
                                                                    <button id="captureSnapshotButton" style="display: none">Capture Snapshot</button>
                                                                    <button id="attemptDecodeButton" style="display: none" disabled>Attempt Decode</button>
                                                                    <button id="startAutoCaptureButton" style="display: none">Start Auto-Capture</button>
                                                                    <button id="stopAutoCaptureButton" style="display: none">Stop Auto-Capture</button>

                                                                </div>
                                                                <div id="output">
                                                                    <h2>Visitor number: </h2>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <br />

                                        <div class="row">
                                            <div class='col-sm-7  pull-left'></div>
                                            <div class='col-sm-5  pull-right'>
                                                <button id="stopCameraButton" class="btn btn-primary">Close</button>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                  
        </div>
    </div>
    <div class="modal" id="ModalWebcamPopup" runat="server">
        <div style="margin-top: 60px!important; max-width: 752px; margin: 1.75rem auto;">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title" style="margin-right: 183px">Select Upload File</h4>
                    <a style="margin-right: -172px" id="btnclose1" class="btn btn-default">Close</a>
                </div>
                <div class="PhotoUploadWrapper">
                    <div id="divpreviw">
                        <div class="PhotoUpoloadLeftMainCont" style="text-align: center;">
                            <div style="padding: 0px 0px 0px 0px; text-align: -webkit-center;">
                                <div id="my_camera"></div>
                            </div>
                            <br />
                            <div style="text-align: center;">
                                <a class="btn btn-info" onclick="take_snapshot()">Capture Phote
                                </a>
                                <br />
                                <br />
                            </div>
                        </div>
                    </div>
                    <div id="divCaptureImages">
                        <div style="padding: 0px 0px 0px 0px; text-align: -webkit-center;">
                            <div id="results"></div>
                        </div>
                        <br />
                        <div style="text-align: center;">
                            <a class="btn btn-info" id="apreview">Preview
                            </a>
                            <a onclick="Save()" class="btn btn-info">Save
                            </a>
                            <br />
                            <br />
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript" src="../webcamjs/webcam.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {

            $("divCaptureImages").hide();
            var parameter = Sys.WebForms.PageRequestManager.getInstance();
            parameter.add_endRequest(function () {
                $("#ContentPlaceHolder1_DivUpload").click(function () {
                    debugger;
                    $("#divpreviw").show();
                    $("#divCaptureImages").hide();
                    $("#ContentPlaceHolder1_ModalWebcamPopup").show();
                });
                $("#apreview").click(function () {
                    configure();
                    $("#divpreviw").show();
                    $("#divCaptureImages").hide();
                    $("#ContentPlaceHolder1_ModalWebcamPopup").show();
                });
                $("#btnclose1").click(function () {
                    Webcam.reset();
                    $("#ContentPlaceHolder1_ModalWebcamPopup").hide();
                });
                var hidden = document.getElementById('ContentPlaceHolder1_hidValue');
                $('#ContentPlaceHolder1_imgStaff').attr("src", hidden.value);
            });
        });
    </script>
    <!-- Configure a few settings and attach camera -->
    <script lang="JavaScript">
        $("divCaptureImages").hide();
        function configure() {
            Webcam.set({
                width: 640,
                height: 480,
                image_format: 'jpeg',
                jpeg_quality: 90
            });
            Webcam.attach('#my_camera');
        }
        $("#ContentPlaceHolder1_DivUpload").click(function () {
            debugger;
            $("#divpreviw").show();
            $("#divCaptureImages").hide();
            $("#ContentPlaceHolder1_ModalWebcamPopup").show();
        });
        // preload shutter audio clip
        var shutter = new Audio();
        shutter.autoplay = false;
        shutter.src = navigator.userAgent.match(/Firefox/) ? 'shutter.ogg' : 'shutter.mp3';
        //<!-- Code to handle taking the snapshot and displaying it locally -->
        function take_snapshot() {
            // play sound effect
            //shutter.play();
            // take snapshot and get image data
            Webcam.snap(function (data_uri) {
                debugger;
                // display results in page
                var hidden = document.getElementById('ContentPlaceHolder1_hidValue');
                hidden.value = data_uri;
                document.getElementById('results').innerHTML =
                    '<img style="border: 2px solid greenyellow;" src="' + data_uri + '"/>';
                Webcam.reset();
                $("#divpreviw").hide();
                $("#divCaptureImages").show();
            });
        }
        $("#apreview").click(function () {
            configure();
            $("#divpreviw").show();
            $("#divCaptureImages").hide();
            $("#ContentPlaceHolder1_ModalWebcamPopup").show();
        });
        function Save() {
            var hidden = document.getElementById('ContentPlaceHolder1_hidValue');
            $('#ContentPlaceHolder1_imgStaff').attr("src", hidden.value);
            $("#ContentPlaceHolder1_ModalWebcamPopup").hide();
        }
        $("#btnclose1").click(function () {
            Webcam.reset();
            $("#ContentPlaceHolder1_ModalWebcamPopup").hide();
        });
    </script>
    <script>
                        function Webcam() {
                            navigator.getMedia = (navigator.getUserMedia || // use the proper vendor prefix
                                navigator.webkitGetUserMedia ||
                                navigator.mozGetUserMedia ||
                                navigator.msGetUserMedia);

                            navigator.getMedia({ video: true }, function () {
                                // webcam is available
                            }, function () {
                                $("#ContentPlaceHolder1_ModalPopupExtender3").hide();
                                $("#ContentPlaceHolder1_ModalPopupExtender1").hide();
                                alert("Camera is not available!");
                                // webcam is not available
                            });

                            $("#ContentPlaceHolder1_ModalPopupExtender3").show();
                            $("#ContentPlaceHolder1_ModalPopupExtender1").hide();
                            //$("#mpe_backgroundElement").show();
                            const canvas = document.getElementById('qr-canvas');
                            const context = canvas.getContext('2d');
                            let autoCaptureStatus = false;
                            let decodeFailures = 0;

                            const constraints = {
                                video: {
                                    width: 320,
                                    height: 240
                                }
                            };

                            showDefaultCanvas();

                            // Attach the video stream to the video element and autoplay.
                            navigator.mediaDevices.getUserMedia(constraints)
                                .then((stream) => {
                                    player.srcObject = stream;
                                });

                            captureSnapshotButton.addEventListener('click', () => {
                                // Draw the video frame to the canvas.
                                attemptDecodeButton.disabled = false;
                                context.drawImage(player, 0, 0, canvas.width, canvas.height);
                            });



                            attemptDecodeButton.addEventListener('click', () => {
                                // Decode QR Code
                                try {
                                    decodedValue = qrcode.decode();
                                    console.log(decodedValue);
                                    updateOutputValue(decodedValue);
                                    autoCaptureStatus = false;
                                } catch (err) {
                                    console.log(err);
                                    //updateOutputValue("[Failed to decode (" + ++decodeFailures + ")]");
                                    //if (err !== "Couldn't find enough finder patterns (found 0)") {
                                    //    alert(err);
                                    //    throw err;
                                    //}
                                }
                            });
                            stopCameraButton.addEventListener('click', () => {
                                // Stop video capture.
                                player.srcObject.getVideoTracks().forEach(track => track.stop());
                                disableButtons();
                                autoCapture = false;
                                $("#ContentPlaceHolder1_ModalPopupExtender3").hide();
                                $("#ContentPlaceHolder1_ModalPopupExtender1").hide();
                                //$("#mpe_backgroundElement").hide();
                                //output.innerHTML = '<h2 style="color:#F00">Reload page to restart camera.</h2>';
                                showDefaultCanvas();
                                //window.location.href = "../SMS_Visitor_Master.aspx";
                            });
                            startAutoCaptureButton.addEventListener('click', () => {
                                // Start taking snapshots to canvas
                                autoCaptureStatus = true;
                                decodeFailures = 0;
                                autoCapture();
                            });

                            stopAutoCaptureButton.addEventListener('click', () => {
                                // Stop taking snapshots to canvas
                                autoCaptureStatus = false;
                            });


                            function autoCapture() {
                                if (autoCaptureStatus) {
                                    captureSnapshotButton.click();
                                    attemptDecodeButton.click();
                                    setTimeout(autoCapture, 100);
                                }
                            }

                            function updateOutputValue(val) {
                                $("#ContentPlaceHolder1_ModalPopupExtender3").hide();
                                $("#ContentPlaceHolder1_ModalPopupExtender1").hide();
                                //  $("#mpe_backgroundElement").hide();
                                output.innerHTML = "<h2>Decoded value: " + val + "</h2>";
                                window.location.href = "../SMS_Visitor_Master.aspx?CopyVisitor=" + val;
                            }

                            function disableButtons() {
                                buttons = document.getElementsByTagName("button");
                                Array.from(buttons).map(button => button.disabled = true);
                            }

                            function showDefaultCanvas() {
                                context.clearRect(0, 0, canvas.width, canvas.height);
                                context.font = "30px Arial";
                                context.fillText("Scan QR", 50, 130);
                                autoCaptureStatus = true;
                                decodeFailures = 0;
                                autoCapture();
                            }
                        }

    </script>
</asp:Content>
