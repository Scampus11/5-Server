<%@ Page Title="" Language="C#" MasterPageFile="~/Admin_Master.Master" AutoEventWireup="true" CodeBehind="SMS_Digital_Form.aspx.cs" Inherits="SMS.SMS_Digital_Form" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Student Digital ID
    </title>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <link rel="stylesheet" type="text/css" href="VisitorManagement/VisitorManagement.css" />
    <script src="VisitorManagement/dymo.connect.framework.js"></script>
    <script type="text/javascript">

        $(document).ready(function () {
            $('#datetimepicker1').datetimepicker({ format: 'DD/MM/YYYY' });
            $('#datetimepicker2').datetimepicker({ format: 'DD/MM/YYYY' });
            $('#datetimepicker3').datetimepicker({ format: 'DD/MM/YYYY' });
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
        });
        function EndRequestHandler(sender, args) {
            SetDatePicker();
        }
        function SetDatePicker() {
            $('#datetimepicker1').datetimepicker({ format: 'DD/MM/YYYY' });
            $('#datetimepicker2').datetimepicker({ format: 'DD/MM/YYYY' });
            $('#datetimepicker3').datetimepicker({ format: 'DD/MM/YYYY' });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdateRefresh" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <br />
            <br />
            <div class="page-content-wrapper" style="margin-bottom: -16px;">
                <div class="page-content">
                    <h3 class="page-title bold">Student Digital ID
                
                    </h3>
                    <hr />
                    <div class="row" id="divgrid" runat="server">
                        <div class="col-md-12">
                            <div class="portlet box green-meadow">
                                <div class="portlet-title">
                                    <div class="caption">
                                        <i class="fa fa-briefcase"></i>
                                        <asp:Label ID="lblStudentId2" runat="server"></asp:Label>
                                        -
                                        <asp:Label ID="lblStudentName2" runat="server"></asp:Label>
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
                                        <asp:LinkButton ID="lnkAdd" runat="server" class="pull-right" Style="color: white" Text="Add Digital Id" OnClick="lnkAdd_Click">
                                            <i class="fa fa-plus" title="Add Digital Id"></i>
                                        </asp:LinkButton>
                                    </div>
                                </div>
                                <div class="portlet-body" style="overflow: scroll">
                                    <asp:GridView ID="gridEmployee" runat="server" class="table table-striped table-bordered table-hover" AutoGenerateColumns="false" AllowPaging="true" AllowSorting="true" OnSorting="OnSorting" OnPageIndexChanging="OnPageIndexChanging" PageSize="5" ShowHeaderWhenEmpty="true" EmptyDataText="No records Found">
                                        <PagerSettings Mode="NumericFirstLast" PageButtonCount="5" FirstPageText="First" LastPageText="Last" />
                                        <PagerStyle CssClass="gridview"></PagerStyle>
                                        <Columns>
                                            <asp:TemplateField ItemStyle-Width="5%" HeaderText="Id" SortExpression="Id">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblId_no" runat="server" Text='<%#Eval("Id_No") %>' Visible="false"></asp:Label>
                                                    <asp:Label ID="lblId1" runat="server" Text='<%#Eval("Id") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="10%" HeaderText="Title" SortExpression="Title">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTitle" runat="server" Text='<%#Eval("Title") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="10%" HeaderText="Status" SortExpression="Digital_Status">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("Digital_Status") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="10%" HeaderText="Assigned Date" SortExpression="AsignDate">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAsignDate" runat="server" Text='<%#Eval("AsignDate") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="10%" HeaderText="Exp Date" SortExpression="Exp_Time">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblExp_Time" runat="server" Text='<%#Eval("Exp_Time") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="15%" HeaderText="Canteens" SortExpression="Canteens">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCanteens" runat="server" Text='<%#Eval("Canteens") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="15%" HeaderText="AGs" SortExpression="AGs">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAGs" runat="server" Text='<%#Eval("AGs") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="15%" HeaderText="BGs" SortExpression="BGs">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBGs" runat="server" Text='<%#Eval("BGs") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="5%" HeaderText="Edit" HeaderStyle-ForeColor="#337ab7">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="linkEdit" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id_No")%>' OnClick="linkEdit_Click"><i class="material-icons" title="Edit row">&#xe3c9;</i></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="5%" HeaderText="Delete" HeaderStyle-ForeColor="#337ab7">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="linkDelete" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id_No")%>' OnClick="linkDelete_Click"><i class="material-icons" style="color:red" title="Delete row">delete</i></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="modal-content" id="DivEdit" runat="server">
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
                                <!-- Nav tabs -->
                                <ul class="nav nav-tabs">
                                    <li class="nav-item active" id="liPersonalDetails" runat="server">
                                        <a class="nav-link" data-toggle="tab" href="#ContentPlaceHolder1_divPersonalDetails" id="aPersonalDetails" runat="server">Digital Id Details</a>
                                    </li>
                                </ul>
                                <!-- Tab panes -->
                                <div class="tab-content">
                                    <div class="tab-pane container active" id="divPersonalDetails" runat="server">
                                        <div class="row">
                                            <div class="col-md-6 col-sm-6">
                                                <div class="portlet-body">
                                                    <div class="form-body">
                                                        <div class="form-group">
                                                            <label class="col-md-3 control-label">
                                                                Student Id :
                                                            </label>
                                                            <div class="col-md-6">
                                                                <asp:Label ID="lblStudentId" runat="server"></asp:Label>
                                                                <asp:TextBox ID="txtStudentId" runat="server" Style="display: none;"></asp:TextBox>
                                                                <asp:TextBox ID="txtxml" runat="server" Style="display: none;"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-6 col-sm-6">
                                                <div class="portlet-body">
                                                    <div class="form-body">
                                                        <div class="form-group">
                                                            <label class="col-md-3 control-label">Student Name : </label>
                                                            <div class="col-md-6">
                                                                <asp:Label ID="lblStudentName" runat="server"></asp:Label>
                                                                <asp:TextBox ID="txtStudentName" runat="server" Style="display: none;"></asp:TextBox>
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
                                                                Department Name :
                                                            </label>
                                                            <div class="col-md-6">
                                                                <asp:Label ID="lblDepartment" runat="server"></asp:Label>
                                                                <asp:Label ID="lblEmailId" runat="server" Visible="false"></asp:Label>
                                                                <asp:Label ID="lblImage64byte" runat="server" Visible="false"></asp:Label>
                                                                <asp:Label ID="lblqrcode" runat="server" Visible="false"></asp:Label>
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
                                                                Status
                                                            </label>
                                                            <div class="col-md-6">
                                                                <asp:DropDownList ID="ddlCardstatus" runat="server" CssClass="form-control">
                                                                    <asp:ListItem Value="1" Selected="True">Active</asp:ListItem>
                                                                    <asp:ListItem Value="2">Revoked</asp:ListItem>
                                                                    <asp:ListItem Value="3">Lost</asp:ListItem>
                                                                    <asp:ListItem Value="4">Suspended</asp:ListItem>
                                                                    <asp:ListItem Value="5">Expired</asp:ListItem>
                                                                    <asp:ListItem Value="6">Missing Active</asp:ListItem>
                                                                    <asp:ListItem Value="7">Graduate</asp:ListItem>
                                                                </asp:DropDownList>
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
                                                                Title :
                                                            </label>
                                                            <div class="col-md-6">
                                                                <asp:TextBox ID="txttitle" runat="server" CssClass="form-control"></asp:TextBox>
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
                                                            <label class="col-md-3 control-label">Assigned Date  : </label>
                                                            <div class="col-md-6">
                                                                <asp:TextBox ID="TxtAssignDate" runat="server" CssClass="form-control" placeholder="Asign Date" Enabled="false"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-6 col-sm-6">
                                                <div class="portlet-body">
                                                    <div class="form-body">
                                                        <div class="form-group">
                                                            <label class="col-md-3 control-label">Expire Date  : </label>
                                                            <div class="col-md-6">
                                                                <div class='input-group date' id='datetimepicker1'>
                                                                    <asp:TextBox ID="txtExpireDate" runat="server" CssClass="form-control" placeholder="Expire Date" BorderColor="#37359C" Style="background-color: LightGrey;"></asp:TextBox>
                                                                    <span class="input-group-addon">
                                                                        <span class="glyphicon glyphicon-calendar"></span>
                                                                    </span>
                                                                </div>
                                                                <asp:Label ID="lblValidExpireDate" runat="server" Style="color: red;" Text="* Selects Expire Date..." Visible="false"></asp:Label>
                                                                <asp:Label ID="lblCompare" runat="server" Style="color: red;" Text="* Expire date should not be less than current date ..." Visible="false"></asp:Label>
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
                                                            <label class="col-md-3 control-label">Digital Id : </label>
                                                            <div class="col-md-6">
                                                                <asp:PlaceHolder ID="plBarCode" runat="server" />
                                                                <asp:Image ID="imgbarcode2" runat="server" Height="200px" Width="200px" ImageUrl="~/Images/Staticbarcode.jpg" Visible="false" />
                                                                <asp:Image ID="imgbarcode3" runat="server" Height="200px" Width="200px" ImageUrl="~/Images/Staticbarcode.jpg" Style="display: none;" />
                                                                <asp:LinkButton ID="lnkGenerate" runat="server" Text="Generate" OnClick="lnkGenerate_Click" class="btn green-meadow" Style="margin-left: 50px; display: none;" /><br />
                                                                <asp:Label ID="lblGenerate" runat="server" Style="color: red;" Text="* Please generate Digital ID ..." Visible="false"></asp:Label>
                                                                <asp:Image ID="imglogo" Style="display: none;" runat="server" Height="120px" Width="200px" ImageUrl="~/Images/visitors.png" />
                                                                <asp:TextBox ID="txtlogoname" Style="display: none;" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:TextBox ID="txtdepartment" Style="display: none;" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:TextBox ID="txtbatchyear" Style="display: none;" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:TextBox ID="txtvalidate" Style="display: none;" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:TextBox ID="txtQrCode" Style="display: none;" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:TextBox ID="txtAG" Style="display: none;" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-6 col-sm-6">
                                                <div class="portlet-body">
                                                    <div class="form-body">
                                                        <div class="form-group">
                                                            <label class="col-md-3 control-label">Student Image : </label>
                                                            <div class="col-md-6">
                                                                <asp:Image ID="image2" runat="server" Height="200px" Width="200px" ImageUrl="~/Images/images1.jpg" />
                                                                <asp:Image ID="Image1" Style="display: none;" runat="server" Height="120px" Width="200px" ImageUrl="~/Images/images1.jpg" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="clearfix"></div>
                                            <hr />
                                            <h3>Assignment List</h3>
                                            <div class="clearfix"></div>
                                            <div class="col-md-6 col-sm-6" id="diveditcanteens" runat="server">
                                                <div class="portlet-body">
                                                    <div class="form-body">
                                                        <div class="form-group">
                                                            <label class="col-md-3 control-label">Available Canteens  : </label>
                                                            <div class="col-md-6">
                                                                <asp:DropDownList ID="ddlcanteen2" CssClass="form-control" runat="server"></asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-6 col-sm-6" id="diveditBG" runat="server">
                                                <div class="portlet-body">
                                                    <div class="form-body">
                                                        <div class="form-group">
                                                            <label class="col-md-3 control-label">Available Block Groups  : </label>
                                                            <div class="col-md-6">
                                                                <asp:Repeater ID="RepeatBlock" runat="server">
                                                                    <ItemTemplate>
                                                                        <div class="col-md-12">
                                                                            <asp:CheckBox ID="chkBlockGroup" CssClass="checkbox" runat="server" Text='<%# Eval("Name")  %>' />
                                                                            <asp:Label ID="lblId" runat="server" Text='<%# Eval("Id") %>' Visible="false"></asp:Label>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                </asp:Repeater>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="clearfix"></div>
                                            <div class="col-md-6 col-sm-6" id="diveditAG" runat="server">
                                                <div class="portlet-body">
                                                    <div class="form-body">
                                                        <div class="form-group">
                                                            <label class="col-md-3 control-label">Available Access Groups  : </label>
                                                            <div class="col-md-6">
                                                                <asp:Repeater ID="RepeatReader" runat="server">
                                                                    <ItemTemplate>
                                                                        <div class="col-md-12">
                                                                            <asp:CheckBox ID="chkReader" CssClass="checkbox" runat="server" Text='<%# Eval("Access_Group_Name")  %>' />
                                                                            <asp:Label ID="lblId" runat="server" Text='<%# Eval("Id") %>' Visible="false"></asp:Label>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                </asp:Repeater>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="clearfix"></div>
                                            <hr />
                                            <div class="col-md-4 col-sm-4" id="divmain" style="display: none;">
                                                <div id="labelImageDiv" style="width: 400px; margin-left: 20px;">
                                                    <img id="labelImage" src="" alt="label preview" />
                                                </div>
                                                <div class="printControls" style="margin-left: 20px; background-color: white;">
                                                    <div id="printersDiv">
                                                        <label for="printersSelect">Printer:</label>
                                                        <select id="printersSelect"></select>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-5 col-sm-5"></div>
                                            <div class="clearfix"></div>
                                            <div class="col-md-12 col-sm-12" style="text-align: right;">
                                                <div class="portlet-body">
                                                    <div class="form-body">
                                                        <div class="form-group">
                                                            <div class="col-md-1 col-sm-1">
                                                                <label class="control-label">
                                                                    <button id="preview" runat="server" style="display: none;" class="btn green-meadow" onclick="previewdata()">Preview</button>

                                                                </label>
                                                            </div>
                                                            <div class="col-md-1 col-sm-1">
                                                                <label class="control-label">
                                                                    <button id="lnkprint" runat="server" style="display: none;" class="btn green-meadow" onclick="Printlable()">Print</button>
                                                                </label>
                                                                <a class="toolbar-button" id="changeLayoutButton" style="display: none;" href="javascript:void(0)">Change Layout</a>
                                                                <a class="toolbar-button" style="background-color: white; padding: 0px"></a>
                                                            </div>
                                                            <div class="col-md-10">

                                                                <asp:LinkButton ID="lnksave" runat="server" Text="Save" OnClick="btnsave_Click" class="btn green-meadow" />
                                                                <asp:LinkButton ID="lnkupdate" runat="server" Text="Update" OnClick="btnupdate_Click" class="btn green-meadow" />
                                                                <asp:LinkButton ID="lnkEmail" Style="background-color: crimson;" runat="server" Text="Notify Student" OnClick="lnkEmail_Click" class="btn green-meadow" />
                                                                <asp:LinkButton ID="lnkBack" runat="server" Text="Back" OnClick="lnkBack_Click" class="btn green-meadow" />
                                                                <asp:LinkButton ID="lnkClear" runat="server" Text="Clear" OnClick="lnkClear_Click" class="btn green-meadow" />
                                                                <asp:Label ID="lblEmailmsg" runat="server" Text="Sent..." Style="color: green;" Visible="false"></asp:Label>
                                                                <asp:Label ID="lblEmailError" runat="server" Text="Please add email id in student master" Style="color: red;" Visible="false"></asp:Label>
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
            </div>

        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="lnksave" />
            <asp:PostBackTrigger ControlID="lnkupdate" />
            <asp:PostBackTrigger ControlID="lnkEmail" />
        </Triggers>
    </asp:UpdatePanel>
    <script>
        
        // stores loaded label info
        var _label;
        // current data on the label. For simplicity we support one Text obje t and one Image object
        var _labelData = {};
        var Xml1;
        function previewdata() {
            Xml1 = document.getElementById('ContentPlaceHolder1_txtxml').value;
            var parameter = Sys.WebForms.PageRequestManager.getInstance();
            parameter.add_endRequest(function () {
                var custDiv = document.getElementById('divmain');
                Barcode();
                if (custDiv.style.display === "none") {
                    custDiv.style.display = "block";
                }
                else {
                    custDiv.style.display = "none";
                }

                var labelFile = document.getElementById('labelFile');
                var labelName = document.getElementById('ContentPlaceHolder1_txtStudentName');
                var labelRegNo = document.getElementById('ContentPlaceHolder1_txtStudentId');
                var labelExpDate = document.getElementById('ContentPlaceHolder1_txtExpireDate');
                var labelBarcode = document.getElementById('ContentPlaceHolder1_txtQrCode');
                var printersSelect = document.getElementById('printersSelect');
                var printButton = document.getElementById('ContentPlaceHolder1_lnkprint');
                var selectPhotoButton = document.getElementById('selectPhotoButton');
                var changeLayoutButton = document.getElementById('changeLayoutButton');
                var img = document.getElementById('ContentPlaceHolder1_Image1');
                var targ = document.getElementById('ContentPlaceHolder1_image2');
                var imglogo = document.getElementById('ContentPlaceHolder1_imglogo');
                var logoName = document.getElementById('ContentPlaceHolder1_txtlogoname');
                var Department = document.getElementById('ContentPlaceHolder1_txtdepartment');
                var validate = document.getElementById('ContentPlaceHolder1_txtvalidate');
                var Batchyear = document.getElementById('ContentPlaceHolder1_txtbatchyear');
                var canten = document.getElementById('ContentPlaceHolder1_ddlcanteen2');
                var AG = document.getElementById('ContentPlaceHolder1_txtAG');
                _labelData.Student_Name = labelName.value;
                _labelData.DID_ExpiryDate = labelExpDate.value;
                _labelData.Student_Validty = validate.value;
                _labelData.Student_ID = labelRegNo.value;
                _labelData.BARCODE = labelBarcode.value;
                _labelData.Student_Department = Department.value;
                _labelData.Student_BatchYear = Batchyear.value;
                _labelData.Company_Name = logoName.value;
                var Canteenlist = canten.options[canten.selectedIndex].text;
                var AGlist = AG.value;
                var Mainlist = "";
                if (Canteenlist == '--Select Canteen--' && AGlist == '') {
                    Mainlist = "";
                }
                else if (Canteenlist != '--Select Canteen--' && AGlist == '') {
                    Mainlist = Canteenlist;
                }
                else if (Canteenlist == '--Select Canteen--' && AGlist != '') {
                    Mainlist = AGlist;
                }
                else if (Canteenlist != '--Select Canteen--' && AGlist != '') {
                    Mainlist = Canteenlist+' | '+AGlist;
                }
                _labelData.Student_AccessLevel = Mainlist;
                var url = targ.src;
                var base64 = img.src;
                var imglogobase64 = imglogo.src;
                _labelData.Student_Photo = base64.replace('data:image/png;base64,', '');
                _labelData.Company_Logo = imglogobase64.replace('data:image/png;base64,', '');
                setupDefaultLayout1();
                updatePreview1();
            });
        }
        function Barcode() {
            var imgbarcode = document.getElementById('ContentPlaceHolder1_imgbarcode3');
            imgbarcode.style.display = "block";
        }
        function setupDefaultLayout1() {
            _label = dymo.label.framework.openLabelXml(Xml1);
            applyDataToLabel1(_label, _labelData);
        }
        // applies data to the label
        function applyDataToLabel1(label, labelData) {
            var names = label.getObjectNames();

            for (var name in labelData)
                if (itemIndexOf1(names, name) >= 0)
                    label.setObjectText(name, labelData[name]);
        }

        // updates label preview image
        // Generates label preview and updates corresponend <img> element
        // Note: this does not work in IE 6 & 7 because they don't support data urls
        // if you want previews in IE 6 & 7 you have to do it on the server side
        function updatePreview1() {
            if (!_label)
                return;

            var pngData = _label.render();
            var labelImage = document.getElementById('labelImage');
            labelImage.src = "data:image/png;base64," + pngData;

        }
        //prints the label
        function Printlable() {
            loadPrinters1();
            Barcode();
            if (!_label) {
                alert("Load label before printing");
                return;
            }

            _label.print(printersSelect.value);
        }
        //returns an index of an item in an array. Returns -1 if not found

        function itemIndexOf1(array, item) {
            for (var i = 0; i < array.length; i++)
                if (array[i] == item) return i;

            return -1;
        }

        function loadPrinters1() {
            var printers = dymo.label.framework.getPrinters();
            if (printers.length == 0) {
                alert("No DYMO printers are installed. Install DYMO printers.");
                return;
            }
            $('#printersSelect').empty();
            for (var i = 0; i < printers.length; i++) {
                var printerName = printers[i].name;
                var option = document.createElement('option');
                option.value = printerName;
                option.appendChild(document.createTextNode(printerName));
                printersSelect.appendChild(option);
            }
        };
    </script>
</asp:Content>
