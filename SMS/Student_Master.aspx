<%@ Page Title="" Language="C#" MasterPageFile="~/Admin_Master.Master" AutoEventWireup="true" CodeBehind="Student_Master.aspx.cs" Inherits="SMS.Student_Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Student Master</title>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#datetimepicker1').datetimepicker({ format: 'MM/DD/YYYY' });
            $('#datetimepicker2').datetimepicker({ format: 'MM/DD/YYYY' });
            $('#datetimepicker3').datetimepicker({ format: 'MM/DD/YYYY' });
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
        });
        function EndRequestHandler(sender, args) {
            SetDatePicker();
        }
        function SetDatePicker() {
            $('#datetimepicker1').datetimepicker({ format: 'MM/DD/YYYY' });
            $('#datetimepicker2').datetimepicker({ format: 'MM/DD/YYYY' });
            $('#datetimepicker3').datetimepicker({ format: 'MM/DD/YYYY' });
        }
    </script>

    <style>
        .modal {
            top: 50px !important;
        }

        .btn-file {
            position: relative;
            overflow: hidden;
        }

            .btn-file input[type=file] {
                position: absolute;
                top: 0;
                right: 0;
                min-width: 100%;
                min-height: 100%;
                font-size: 100px;
                text-align: right;
                filter: alpha(opacity=0);
                opacity: 0;
                outline: none;
                background: white;
                cursor: inherit;
                display: block;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdateRefresh" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <br />
            <br />
            <div class="page-content-wrapper" style="margin-bottom: -16px;">
                <div class="page-content">
                    <h3 class="page-title bold">Students</h3>
                    <div class="row" id="divgrid" runat="server">
                        <div class="col-md-12">
                            <div class="portlet box green-meadow">
                                <div class="portlet-title">
                                    <div class="caption">
                                        <i class='fas'>&#xf501;</i>Student List
                                    </div>
                                    <div class="tools pull-left" style="margin-left: 10px; padding: 8px; border-left: 0px!important;">
                                        <asp:TextBox ID="txtSearch" placeHolder="Search" runat="server" Width="230px" Height="28px" Style="border: 0px; color: black" />
                                    </div>
                                    <div class="tools pull-left" style="margin-left: 5px; padding: 8px; border-left: 0px!important;">
                                        <asp:LinkButton ID="btnsearch" runat="server" OnClick="btnsearch_Click">
                                        <i class="fa fa-search" title="Search"  style="font-size: 28px; color: white; margin-top: 4px;"></i>
                                        </asp:LinkButton>
                                    </div>
                                    <div class="tools" style="display: none;">
                                        <asp:LinkButton ID="LnkExport" class="btn green-meadow" Visible="false" runat="server" Text="Export" OnClick="LnkExport_Click"></asp:LinkButton>
                                    </div>
                                    <div class="tools" id="divadd" runat="server" visible="true">
                                        <a class="pull-right" style="color: white" href="Student_Master.aspx?Add=Add">
                                            <i class="fa fa-plus" title="Add New Student"></i></a>
                                    </div>
                                    <div class="tools" style="display: none">
                                        <asp:LinkButton ID="lnksyncjob" runat="server" class="pull-right" Style="color: white" OnClick="lnksyncjob_Click">
                                              <i class="fa fa-laptop" title="Sync job"></i>
                                        </asp:LinkButton>
                                    </div>
                                    <div class="tools" style="display: none">
                                        <asp:LinkButton ID="lnkAllMasters" runat="server" class="pull-right" Style="color: white" OnClick="lnkAllMasters_Click">
                                              <i class="fa fa-laptop" title="Sync all Masters job"></i>
                                        </asp:LinkButton>
                                    </div>
                                    <div class="tools" id="divBase64" runat="server" visible="true">
                                        <a id="lnkBase64" class="pull-right" style="color: white;" target="_blank" href="ConvertFolderImageToBase64.aspx?Name=Student">
                                            <i class="fa fa-image" title="Convert to Base 64"></i>
                                        </a>
                                    </div>
                                    <div class="tools">
                                        <asp:LinkButton ID="lnkImportExcel" runat="server" Style="margin-bottom: 10px; color: white;" class="pull-right" OnClick="lnkImportExcel_Click">
                                              <i class="fa fa-plus-circle"></i> Import Excel  
                                        </asp:LinkButton>
                                    </div>
                                </div>

                                <div class="portlet-body">
                                    <asp:Label ID="lblSyncmsg" runat="server" Visible="false" Text="Sync Completed" Style="color: green;"></asp:Label>
                                    <asp:GridView ID="gridEmployee" runat="server" class="table table-striped table-bordered table-hover" AutoGenerateColumns="false"
                                        AllowPaging="true" AllowSorting="true" OnSorting="OnSorting" OnPageIndexChanging="OnPageIndexChanging" PageSize="10"
                                        ShowHeaderWhenEmpty="true" EmptyDataText="No records Found" OnRowDataBound="gridEmployee_RowDataBound">
                                        <%--set pagination --%>
                                        <PagerSettings Mode="NumericFirstLast" PageButtonCount="10" FirstPageText="First" LastPageText="Last" />
                                        <PagerStyle CssClass="gridview"></PagerStyle>
                                        <Columns>
                                            <asp:TemplateField ItemStyle-Width="10%" HeaderText="Images" HeaderStyle-ForeColor="#337ab7">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblpath" Visible="false" runat="server" Text='<%#Eval("StudentImages") %>'></asp:Label>
                                                    <asp:Image ID="imgStudent" runat="server" Visible="false" ImageUrl='<%#Eval("StudentImages") %>' class="circle" />
                                                    <asp:Image ID="imgdefault" runat="server" Visible="false" ImageUrl="~/Images/images1.jpg" class="circle" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="10%" HeaderText="Student Id" SortExpression="StudentID">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStudentID" runat="server" Text='<%#Eval("StudentID") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="14%" HeaderText="UId" SortExpression="UNIQUEId" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblUId1" runat="server" Text='<%#Eval("UNIQUEId") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="14%" HeaderText="Full Name" SortExpression="FirstName">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFirstName" runat="server" Text='<%#Eval("FirstName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="100px" HeaderText="DateOfBirth" SortExpression="DateOfBirth" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDateOfBirth" runat="server" Text='<%#Eval("DateOfBirth") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="10%" HeaderText="Card Number" SortExpression="cardid">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCardNumber" runat="server" Text='<%#Eval("cardid") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="14%" HeaderText="College Name" SortExpression="College">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCollege" runat="server" Text='<%#Eval("College") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="14%" HeaderText="Department Name" SortExpression="Department">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDepartment" runat="server" Text='<%#Eval("Department") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="14%" HeaderText="Digital" SortExpression="DigitalStatus">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDigitalStatus" runat="server" Text='<%#Eval("DigitalStatus") %>' Visible="false"></asp:Label>
                                                    <asp:Label ID="lblDigitalRequest" runat="server" Text='<%#Eval("DigitalRequest") %>' Visible="false"></asp:Label>
                                                    <asp:LinkButton ID="linkdigital" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "StudentID")%>' OnClick="linkdigital_Click">
                                                        <asp:Image ID="imgDigitalStatus" runat="server" ImageUrl="~/Images/DigitalID.png" Width="50px" Visible="false" />
                                                    </asp:LinkButton>
                                                    <asp:Image ID="imgQRMain" runat="server" ImageUrl="~/Images/QRMain.png" Width="50px" Visible="false" />
                                                    <asp:Image ID="imgDigitalRequest" runat="server" ImageUrl="~/Images/Request.png" Width="50px" Visible="false" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="14%" HeaderText="Hostel Name" SortExpression="HostelName">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblHostelName" runat="server" Text='<%#Eval("HostelName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="100px" HeaderText="VIEW" Visible="false">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="linkEdit1" class="btn green-meadow" runat="server" Text="View" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "StudentID")%>' OnClick="linkEdit_Click"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="5%" HeaderText="Edit" HeaderStyle-ForeColor="#337ab7">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="linkEdit" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "StudentID")%>' OnClick="linkEdit_Click1"><i class="material-icons" title="Edit row">&#xe3c9;</i></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                            <!-- END EXAMPLE TABLE PORTLET-->
                        </div>
                    </div>
                    <div class="modal-content" id="DivEdit" runat="server" visible="false">
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
                                        <a class="nav-link" data-toggle="tab" href="#ContentPlaceHolder1_divPersonalDetails" id="aPersonalDetails" runat="server">Personal Details</a>
                                    </li>
                                    <li class="nav-item" id="liEducationinformation" runat="server">
                                        <a class="nav-link" data-toggle="tab" href="#ContentPlaceHolder1_divEducationinformation" id="aEducationinformation" runat="server">Education Information</a>
                                    </li>
                                    <li class="nav-item" id="liOtherinformaion" runat="server">
                                        <a class="nav-link" data-toggle="tab" href="#ContentPlaceHolder1_divOtherinformaion" id="aOtherinformaion" runat="server">Other Information</a>
                                    </li>
                                    <li class="nav-item" id="liHostel" runat="server">
                                        <a class="nav-link" data-toggle="tab" href="#ContentPlaceHolder1_divHostel" id="aHostel" runat="server">Hostel Information</a>
                                    </li>
                                </ul>

                                <!-- Tab panes -->
                                <div class="tab-content">
                                    <div class="tab-pane container active" id="divPersonalDetails" runat="server">
                                        <div class="row ">
                                            <div class="col-md-6 col-sm-6">
                                                <div class="portlet-body">
                                                    <div class="form-body">
                                                        <div class="form-group">
                                                            <label class="col-md-3 control-label">
                                                                Student Id :
                                                            </label>

                                                            <div class="col-md-6">
                                                                <asp:TextBox ID="txtStudentId" runat="server" CssClass="form-control" BackColor="#d3d3d3" placeholder="Student Id" Style="border-color: #37359C;"></asp:TextBox>
                                                                <asp:Label ID="lblValidstudent" runat="server" Style="color: red;" Text="* Enter student id..." Visible="false"></asp:Label>
                                                            </div>

                                                        </div>
                                                    </div>

                                                </div>
                                            </div>

                                            <div class="col-md-6 col-sm-6">
                                                <div class="portlet-body">
                                                    <div class="form-body">
                                                        <div class="form-group">
                                                            <label class="col-md-3 control-label">First Name : </label>
                                                            <div class="col-md-6">
                                                                <asp:TextBox ID="txtfirstname" runat="server" CssClass="form-control" BackColor="#d3d3d3" placeholder="First Name" Style="border-color: #37359C;"></asp:TextBox>
                                                                <asp:Label ID="lblvalidfirstname" runat="server" Style="color: red;" Text="* Enter first name..." Visible="false"></asp:Label>

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
                                                                Father's Name :
                                                            </label>

                                                            <div class="col-md-6">
                                                                <asp:TextBox ID="txtFathername" runat="server" CssClass="form-control" placeholder="Father's Name"></asp:TextBox>
                                                            </div>

                                                        </div>
                                                    </div>

                                                </div>
                                            </div>

                                            <div class="col-md-6 col-sm-6">
                                                <div class="portlet-body">
                                                    <div class="form-body">
                                                        <div class="form-group">
                                                            <label class="col-md-3 control-label">Grand Father's Name : </label>

                                                            <div class="col-md-6">

                                                                <asp:TextBox ID="txtGrandfathername" runat="server" CssClass="form-control" BackColor="#d3d3d3" placeholder="Grand Father's Name" Style="border-color: #37359C;"></asp:TextBox>
                                                                <asp:Label ID="lbllastnamevalid" runat="server" Style="color: red;" Text="* Enter grand father's name..." Visible="false"></asp:Label>
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
                                                                Gender :
                                                            </label>

                                                            <div class="col-md-6">
                                                                <%--<asp:TextBox ID="txtGender" runat="server" CssClass="form-control" placeholder="Gender"></asp:TextBox>--%>
                                                                <asp:DropDownList ID="drpGender" runat="server" CssClass="form-control">
                                                                    <asp:ListItem Value="Male">Male</asp:ListItem>
                                                                    <asp:ListItem Value="Female">Female</asp:ListItem>
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
                                                                Eng Full Name :
                                                            </label>

                                                            <div class="col-md-6">
                                                                <asp:TextBox ID="txtMealNumber" runat="server" CssClass="form-control" placeholder="Full Name(amharic) :"></asp:TextBox>
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
                                                            <label class="col-md-3 control-label">Student Image : </label>

                                                            <div class="col-md-6">
                                                                <asp:Image ID="image2" runat="server" Height="120px" Width="200px" ImageUrl="~/Images/images1.jpg" />
                                                                <%--<span class="btn btn-default btn-file">--%>
                                                                <label class="btn btn-default">
                                                                    <asp:FileUpload ID="FileUpload1" runat="server" Style="color: blue" />
                                                                </label>
                                                                <span style="color: darkviolet">Note : Only Image Suported Format is .jpg | .jpeg | .png | .bmp | .gif | .eps...</span><br />
                                                                <asp:Label ID="lblf1" runat="server" Style="color: red;" Text="You upload image format not suppoted.." Visible="false"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-6 col-sm-6">
                                                <div class="portlet-body">
                                                    <div class="form-body">
                                                        <div class="form-group">
                                                            <label class="col-md-3 control-label">Signature : </label>

                                                            <div class="col-md-6">
                                                                <asp:Image ID="imgSignature" runat="server" Height="120px" Width="200px" ImageUrl="~/Images/images1.jpg" />
                                                                <%--<span class="btn btn-default btn-file">--%>
                                                                <label class="btn btn-default">
                                                                    <asp:FileUpload ID="FileUpload2" runat="server" Style="color: blue" />
                                                                </label>
                                                                <span style="color: darkviolet">Note : Only Image Suported Format is .jpg | .jpeg | .png | .bmp | .gif | .eps...</span>
                                                                <asp:Label ID="lblf2" runat="server" Style="color: red;" Text="You upload image format not suppoted.." Visible="false"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="clearfix"></div>
                                        </div>
                                    </div>
                                    <div class="tab-pane container fade" id="divEducationinformation" runat="server">
                                        <div class="row ">
                                            <div class="col-md-6 col-sm-6">
                                                <div class="portlet-body">
                                                    <div class="form-body">
                                                        <div class="form-group">
                                                            <label class="col-md-3 control-label">
                                                                Campus :
                                                            </label>

                                                            <div class="col-md-6">
                                                                <asp:DropDownList ID="ddlCampus" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlCampus_SelectedIndexChanged" AutoPostBack="true">
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
                                                                College :
                                                            </label>

                                                            <div class="col-md-6">
                                                                <asp:DropDownList ID="ddlCollege" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged" AutoPostBack="true">
                                                                    <asp:ListItem Value="0">--Select College--</asp:ListItem>
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
                                                                Department :
                                                            </label>

                                                            <div class="col-md-6">
                                                                <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="form-control">
                                                                    <asp:ListItem Value="0">--Select Department--</asp:ListItem>
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
                                                                Year :
                                                            </label>

                                                            <div class="col-md-6">
                                                                <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-control">
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
                                                                Program :
                                                            </label>

                                                            <div class="col-md-6">
                                                                <asp:DropDownList ID="ddlProgram" runat="server" CssClass="form-control">
                                                                    <asp:ListItem Value="0">--Select Program--</asp:ListItem>
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
                                                                Degree Type :
                                                            </label>

                                                            <div class="col-md-6">
                                                                <asp:DropDownList ID="ddlDegreeType" runat="server" CssClass="form-control">
                                                                    <asp:ListItem Value="0">--Select Degree Type--</asp:ListItem>
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
                                                                Admission Type :
                                                            </label>

                                                            <div class="col-md-6">
                                                                <asp:DropDownList ID="ddlAdmissionType" runat="server" CssClass="form-control" OnTextChanged="ddlAdmissionType_TextChanged" AutoPostBack="true">
                                                                    <asp:ListItem Value="0">--Select Admission Type--</asp:ListItem>
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
                                                                Admission Type Short :
                                                            </label>

                                                            <div class="col-md-6">
                                                                <asp:DropDownList ID="ddlAdmissionTypeShort" runat="server" CssClass="form-control">
                                                                    <asp:ListItem Value="0">--Select Admission Type Short--</asp:ListItem>
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
                                                                Valid Date Until :
                                                            </label>

                                                            <div class="col-md-6">
                                                                <div class='input-group date' id='datetimepicker3'>
                                                                    <asp:TextBox ID="txtValidDateUntil" runat="server" BackColor="#d3d3d3" Style="border-color: #37359C;" CssClass="form-control" placeholder="Valid Date Until :"></asp:TextBox>
                                                                    <span class="input-group-addon">
                                                                        <span class="glyphicon glyphicon-calendar"></span>
                                                                    </span>
                                                                </div>
                                                                <asp:Label ID="lblvalidValidDateUntil" runat="server" Style="color: red;" Text="* Enter Valid Date Until..." Visible="false"></asp:Label>
                                                                <asp:Label ID="lblvalidValidDateUntil2" runat="server" Style="color: red;" Text="*Valid Date Until Expired, please select Datetime..." Visible="false"></asp:Label>
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
                                                                Issue Date :
                                                            </label>

                                                            <div class="col-md-6">
                                                                <div class='input-group date' id='datetimepicker2'>
                                                                    <asp:TextBox ID="txtIssueDate" runat="server" CssClass="form-control" placeholder="Issue Date"></asp:TextBox>
                                                                    <span class="input-group-addon">
                                                                        <span class="glyphicon glyphicon-calendar"></span>
                                                                    </span>
                                                                </div>
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
                                                            <label class="col-md-3 control-label">Date Of Birth : </label>

                                                            <div class="col-md-6">
                                                                <div class='input-group date' id='datetimepicker1'>

                                                                    <asp:TextBox ID="txtDateOfBirth" runat="server" CssClass="form-control" placeholder="Date Of Birth"></asp:TextBox>
                                                                    <span class="input-group-addon">
                                                                        <span class="glyphicon glyphicon-calendar"></span>
                                                                    </span>
                                                                </div>
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
                                                                <asp:TextBox ID="txtUniqueNo" runat="server" CssClass="form-control" placeholder="Email Id"></asp:TextBox>
                                                                <span id="error" style="display: none; color: red;">Wrong email</span>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="clearfix"></div>
                                        </div>
                                    </div>
                                    <div class="tab-pane container fade" id="divOtherinformaion" runat="server">
                                        <div class="row ">
                                            <div class="col-md-6 col-sm-6">
                                                <div class="portlet-body">
                                                    <div class="form-body">
                                                        <div class="form-group">
                                                            <label class="col-md-3 control-label">
                                                                Card Status :
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

                                            <div class="col-md-6 col-sm-6">
                                                <div class="portlet-body">
                                                    <div class="form-body">
                                                        <div class="form-group">
                                                            <label class="col-md-3 control-label">
                                                                Card Number :
                                                            </label>

                                                            <div class="col-md-6">
                                                                <asp:TextBox ID="txtCardNumber" runat="server" BackColor="#d3d3d3" Style="border-color: #37359C;" CssClass="form-control" placeholder="Card Number"></asp:TextBox>
                                                                <asp:Label ID="lblcardnumber" runat="server" Style="color: red;" Text="* Enter card number..." Visible="false"></asp:Label>
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
                                                                Digital Status :
                                                            </label>

                                                            <div class="col-md-6">
                                                                <asp:CheckBox ID="chkdigitalStatus" runat="server" Text="Digital Status" CssClass="checkbox" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="tab-pane container fade" id="divHostel" runat="server">
                                        <div class="row ">
                                            <div class="col-md-6 col-sm-6">
                                                <div class="portlet-body">
                                                    <div class="form-body">
                                                        <div class="form-group">
                                                            <label class="col-md-6 control-label" style="margin-left: 16px;">
                                                                <asp:CheckBox ID="chkRoomLockerassgnment" runat="server" Text="Enable Hostel" CssClass="checkbox" OnCheckedChanged="chkRoomLockerassgnment_CheckedChanged" AutoPostBack="true" />
                                                            </label>
                                                            <div class="col-md-6">
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="clearfix"></div>
                                            <div id="divMainHostel" runat="server" visible="false">
                                                <div class="col-md-6 col-sm-6">
                                                    <div class="portlet-body">
                                                        <div class="form-body">
                                                            <div class="form-group">
                                                                <label class="col-md-3 control-label">Hostel Name : </label>
                                                                <div class="col-md-6">
                                                                    <asp:DropDownList ID="ddlLockerHostelName" runat="server" CssClass="form-control" BackColor="#d3d3d3" Style="border-color: #37359C;" OnSelectedIndexChanged="ddlLockerHostelName_SelectedIndexChanged" AutoPostBack="true">
                                                                        <asp:ListItem Value="0" Selected="True">--Select Hostel Name--</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:Label ID="lblValidLockerHostelName" runat="server" Style="color: red;" Text="* Select Hostel Name..." Visible="false"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-6 col-sm-6">
                                                    <div class="portlet-body">
                                                        <div class="form-body">
                                                            <div class="form-group">
                                                                <label class="col-md-3 control-label">Block Name : </label>
                                                                <div class="col-md-6">
                                                                    <asp:DropDownList ID="ddlLockerBlockName" runat="server" CssClass="form-control" BackColor="#d3d3d3" Style="border-color: #37359C;" OnSelectedIndexChanged="ddlLockerBlockName_SelectedIndexChanged" AutoPostBack="true">
                                                                        <asp:ListItem Value="0" Selected="True">--Select Block Name--</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:Label ID="lblValidLockerBlockName" runat="server" Style="color: red;" Text="* Select Block Name..." Visible="false"></asp:Label>
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
                                                                <label class="col-md-3 control-label">Floor Name : </label>
                                                                <div class="col-md-6">
                                                                    <asp:DropDownList ID="ddllockerFloorName" runat="server" CssClass="form-control" BackColor="#d3d3d3" Style="border-color: #37359C;" OnSelectedIndexChanged="ddllockerFloorName_SelectedIndexChanged" AutoPostBack="true">
                                                                        <asp:ListItem Value="0" Selected="True">--Select Floor Name--</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:Label ID="lblValidlockerFloorName" runat="server" Style="color: red;" Text="* Select Floor Name..." Visible="false"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-6 col-sm-6">
                                                    <div class="portlet-body">
                                                        <div class="form-body">
                                                            <div class="form-group">
                                                                <label class="col-md-3 control-label">Room Number : </label>
                                                                <div class="col-md-6">
                                                                    <asp:DropDownList ID="ddlLockerRoomNumber" runat="server" CssClass="form-control" BackColor="#d3d3d3" Style="border-color: #37359C;" OnSelectedIndexChanged="ddlLockerRoomNumber_SelectedIndexChanged" AutoPostBack="true">
                                                                        <asp:ListItem Value="0" Selected="True">--Select Room Number--</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:Label ID="lblValidLockerRoomNumber" runat="server" Style="color: red;" Text="* Select Room Number..." Visible="false"></asp:Label>
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
                                                                <label class="col-md-3 control-label">Bed Number : </label>
                                                                <div class="col-md-6">
                                                                    <asp:DropDownList ID="ddlBedNumber" runat="server" CssClass="form-control" BackColor="#d3d3d3" Style="border-color: #37359C;" OnTextChanged="ddlBedNumber_TextChanged" AutoPostBack="true">
                                                                        <asp:ListItem Value="0" Selected="True">--Select Bed Number--</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:Label ID="lblValidLockerBedNumber" runat="server" Style="color: red;" Text="* Select Bed Number..." Visible="false"></asp:Label>
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
                                                                    Locker Number :
                                                                </label>

                                                                <div class="col-md-6">
                                                                    <asp:DropDownList ID="ddlLocker" runat="server" CssClass="form-control" BackColor="#d3d3d3" Style="border-color: #37359C;">
                                                                        <asp:ListItem Value="0" Selected="True">--Select Locker Number--</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:Label ID="lblValidLockerNumber" runat="server" Style="color: red;" Text="* Select Locker Number..." Visible="false"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
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
                                                        <asp:LinkButton ID="lnksave" runat="server" Text="Save" OnClick="btnupdate_Click" class="btn green-meadow" />
                                                        <asp:LinkButton ID="lnkupdate" runat="server" Text="Update" OnClick="btnupdate_Click" class="btn green-meadow" />
                                                        <asp:LinkButton ID="lnkBack" runat="server" Text="Back" OnClick="lnkBack_Click" class="btn green-meadow" />
                                                        <asp:LinkButton ID="lnkClear" runat="server" Text="Clear" OnClick="lnkClear_Click" class="btn green-meadow" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <br />
                                    <br />
                                </div>

                                <div class="form-group" style="display: none">
                                    <label class="text-success">Status : </label>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="description">
                                                <div class="row">
                                                    <div class="col-md-12" id="Div40">
                                                        <div class="input-group">
                                                            <asp:Label ID="Label19" runat="server"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="form-group" style="display: none">
                                    <label class="text-success">Isactive : </label>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="description">
                                                <div class="row">
                                                    <div class="col-md-12" id="Div41">
                                                        <div class="input-group">
                                                            <asp:Label ID="Label20" runat="server"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="form-group" style="display: none">
                                    <label class="text-success">id : </label>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="description">
                                                <div class="row">
                                                    <div class="col-md-12" id="Div42">
                                                        <div class="input-group">
                                                            <asp:Label ID="Label21" runat="server"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="form-group" style="display: none">
                                    <label class="text-success">UNIQUEID : </label>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="description">
                                                <div class="row">
                                                    <div class="col-md-12" id="Div43">
                                                        <div class="input-group">
                                                            <asp:Label ID="Label22" runat="server"></asp:Label>
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
                <div class="modal-content" id="divView" runat="server" visible="false">
                    <div class="modal-body">
                        <div class="portlet-body form">
                            <!-- BEGIN FORM-->
                            <div class="form-body">
                                <p class="text-success">
                                    StudentId :
                                </p>
                                <div class="form-group">
                                    <div class="description">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <asp:Label ID="lblStudentID" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <hr />

                                <div class="form-group">
                                    <label class="text-success">FirstName : </label>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="description">
                                                <div class="row">
                                                    <div class="col-md-12" id="Div1">
                                                        <div class="input-group">
                                                            <asp:Label ID="lblFirstName" runat="server"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <hr />

                                <div class="form-group">
                                    <label class="text-success">Middle Name : </label>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="description">
                                                <div class="row">
                                                    <div class="col-md-12" id="Div2">
                                                        <div class="input-group">
                                                            <asp:Label ID="lblFatherName" runat="server"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <hr />

                                <div class="form-group">
                                    <label class="text-success">Last Name : </label>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="description">
                                                <div class="row">
                                                    <div class="col-md-12" id="Div3">
                                                        <div class="input-group">
                                                            <asp:Label ID="lblGrandFatherName" runat="server"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <hr />

                                <div class="form-group">
                                    <label class="text-success">Gender : </label>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="description">
                                                <div class="row">
                                                    <div class="col-md-12" id="Div4">
                                                        <div class="input-group">
                                                            <asp:Label ID="lblGender" runat="server"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <hr />

                                <div class="form-group">
                                    <label class="text-success">DateOfBirth : </label>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="description">
                                                <div class="row">
                                                    <div class="col-md-12" id="Div5">
                                                        <div class="input-group">
                                                            <asp:Label ID="lblDateOfBirth" runat="server"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <hr />

                                <div class="form-group">
                                    <label class="text-success">Signature : </label>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="description">
                                                <div class="row">
                                                    <div class="col-md-3" id="Div6">
                                                        <div class="input-group">
                                                            <asp:Label ID="lblSignature" runat="server" Visible="false"></asp:Label>
                                                            <asp:Image ID="img1" runat="server" ImageUrl="~/Images/LOGO.jpeg" Height="120px" Width="200px" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <hr />

                                <div class="form-group">
                                    <label class="text-success">College : </label>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="description">
                                                <div class="row">
                                                    <div class="col-md-3" id="Div7">
                                                        <div class="input-group">
                                                            <asp:Label ID="lblCollege" runat="server"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <hr />

                                <div class="form-group">
                                    <label class="text-success">Department : </label>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="description">
                                                <div class="row">
                                                    <div class="col-md-3" id="Div8">
                                                        <div class="input-group">
                                                            <asp:Label ID="lblDepartment" runat="server"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <hr />

                                <div class="form-group">
                                    <label class="text-success">Campus : </label>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="description">
                                                <div class="row">
                                                    <div class="col-md-3" id="Div9">
                                                        <div class="input-group">
                                                            <asp:Label ID="lblCampus" runat="server"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <hr />

                                <div class="form-group">
                                    <label class="text-success">Program : </label>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="description">
                                                <div class="row">
                                                    <div class="col-md-12" id="Div10">
                                                        <div class="input-group">
                                                            <asp:Label ID="lblProgram" runat="server"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <hr />

                                <div class="form-group">
                                    <label class="text-success">DegreeType : </label>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="description">
                                                <div class="row">
                                                    <div class="col-md-12" id="Div11">
                                                        <div class="input-group">
                                                            <asp:Label ID="lblDegreeType" runat="server"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <hr />

                                <div class="form-group">
                                    <label class="text-success">AdmissionType : </label>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="description">
                                                <div class="row">
                                                    <div class="col-md-12" id="Div12">
                                                        <div class="input-group">
                                                            <asp:Label ID="lblAdmissionType" runat="server"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <hr />

                                <div class="form-group">
                                    <label class="text-success">AdmissionTypeShort : </label>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="description">
                                                <div class="row">
                                                    <div class="col-md-12" id="Div13">
                                                        <div class="input-group">
                                                            <asp:Label ID="lblAdmissionTypeShort" runat="server"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <hr />

                                <div class="form-group">
                                    <label class="text-success">ValidDateUntil : </label>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="description">
                                                <div class="row">
                                                    <div class="col-md-12" id="Div14">
                                                        <div class="input-group">
                                                            <asp:Label ID="lblValidDateUntil" runat="server"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <hr />

                                <div class="form-group">
                                    <label class="text-success">IssueDate : </label>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="description">
                                                <div class="row">
                                                    <div class="col-md-12" id="Div15">
                                                        <div class="input-group">
                                                            <asp:Label ID="lblIssueDate" runat="server"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <hr />

                                <div class="form-group">
                                    <label class="text-success">MealNumber : </label>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="description">
                                                <div class="row">
                                                    <div class="col-md-12" id="Div16">
                                                        <div class="input-group">
                                                            <asp:Label ID="lblMealNumber" runat="server"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <hr />

                                <div class="form-group">
                                    <label class="text-success">UniqueNo : </label>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="description">
                                                <div class="row">
                                                    <div class="col-md-12" id="Div17">
                                                        <div class="input-group">
                                                            <asp:Label ID="lblUniqueNo" runat="server"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <hr />

                                <div class="form-group" style="display: none">
                                    <label class="text-success">Status : </label>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="description">
                                                <div class="row">
                                                    <div class="col-md-12" id="Div18">
                                                        <div class="input-group">
                                                            <asp:Label ID="lblStatus" runat="server"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <%-- <hr />--%>

                                <div class="form-group" style="display: none">
                                    <label class="text-success">Isactive : </label>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="description">
                                                <div class="row">
                                                    <div class="col-md-12" id="Div19">
                                                        <div class="input-group">
                                                            <asp:Label ID="lblIsactive" runat="server"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <%--<hr />--%>

                                <div class="form-group" style="display: none">
                                    <label class="text-success">id : </label>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="description">
                                                <div class="row">
                                                    <div class="col-md-12" id="Div20">
                                                        <div class="input-group">
                                                            <asp:Label ID="lblid" runat="server"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <%--<hr />--%>

                                <div class="form-group" style="display: none">
                                    <label class="text-success">UNIQUEID : </label>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="description">
                                                <div class="row">
                                                    <div class="col-md-12" id="Div21">
                                                        <div class="input-group">
                                                            <asp:Label ID="lblUNIQUEID" runat="server"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <%--<hr />--%>
                                <div class="form-group">
                                    <p class="text-success">
                                        Card Status :
                                    </p>
                                    <div class="description">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <asp:Label ID="lblCardStatus" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <hr />


                                <div class="form-group">
                                    <label class="text-success">Student Image : </label>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="description">
                                                <div class="row">
                                                    <div class="col-md-12" id="Div22">
                                                        <div class="input-group">
                                                            <asp:Image ID="image1" runat="server" Height="120px" Width="200px" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>



                            <!-- END FORM-->
                        </div>
                    </div>
                    <!-- END EXAMPLE TABLE PORTLET-->
                </div>

                <!-- END PAGE CONTENT-->
                <div id="static" class="modal fade" tabindex="-1" data-backdrop="static" data-keyboard="false">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">Close</button>
                                <h4 class="modal-title"><strong><i class="fa fa-plus"></i>New Department</strong></h4>
                            </div>
                            <div class="modal-body">
                                <div class="portlet-body form">
                                    <!-- BEGIN FORM-->

                                    <input type="hidden" name="_token" value="ax8ZN3lRSoa15FC1BBhhPnP5xckz7rpEbL25Olx3">
                                    <div class="form-body">
                                        <p class="text-success">
                                            Department
                                        </p>
                                        <div class="form-group">
                                            <div class="description">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <input class="form-control form-control-inline " name="name" type="text" value="" placeholder="Department Name" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <hr>

                                        <div class="form-group">
                                            <label class="text-success">Designation : </label>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="description">
                                                        <div class="row">
                                                            <div class="col-md-12" id="planDescriptionContainer">
                                                                <div class="input-group">
                                                                    <input name="deg_name" class="form-control margin-top-10" type="text" required placeholder="Designation Name">
                                                                    <span class="input-group-btn">
                                                                        <button class="btn btn-danger margin-top-10 Delete_desc" type="button"><i class='fa fa-times'></i></button>
                                                                    </span>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="row">
                                                            <div class="col-md-12 text-right margin-top-10">
                                                                <button id="btnAddDescription" type="button" class="btn btn-sm grey-mint pullri">Add Designation</button>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <asp:HiddenField ID="hdnimage" runat="server" />
                                    <div class="form-actions">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <button type="submit" data-loading-text="Submitting..." class="demo-loading-btn btn green-meadow"><i class="fa fa-check"></i>Submit</button>
                                                <button type="button" class="btn dark btn-outline" data-dismiss="modal">Close</button>
                                            </div>
                                        </div>
                                    </div>

                                    <!-- END FORM-->
                                </div>
                            </div>
                            <!-- END EXAMPLE TABLE PORTLET-->
                        </div>
                    </div>
                </div>
            </div>
            </div>
            <div class="modal" id="DivModalImport" runat="server" style="margin-bottom: ">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title">Import Excel File</h4>

                            <asp:LinkButton ID="lnkdownload" runat="server" Style="text-align: right;" CssClass="btn btn-info" Text="Download Excel" OnClick="lnkdownload_Click"></asp:LinkButton>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label>Choose excel file</label>
                                        <div class="input-group">
                                            <div class="custom-file">
                                                <asp:FileUpload ID="FileUpload3" CssClass="custom-file-input" runat="server" />
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
        </ContentTemplate>
        <Triggers>
            <%--<asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />--%>
            <asp:PostBackTrigger ControlID="lnksave" />
            <asp:PostBackTrigger ControlID="lnkupdate" />
            <asp:AsyncPostBackTrigger ControlID="lnkImportExcel" />
            <asp:PostBackTrigger ControlID="lnkUpload" />
            <asp:PostBackTrigger ControlID="lnkdownload" />
        </Triggers>
    </asp:UpdatePanel>

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
    <div id="DivSync" runat="server" class="modal">

        <!-- Modal content -->
        <div class="modal-content" style="top: 100px; left: 320px; width: 700px; height: 200px;">
            <br />
            <br />
            <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>--%>
            <div style="text-align: center;">
                <h5><b style="color: green">
                    <asp:Label ID="Label1" runat="server"></asp:Label></b></h5>
                <br />
                <h5><b style="color: green">
                    <asp:Label ID="Label2" Text="This will sync College, Department, Year, Program, Degree Type, Admission, AdmissionType and AdmissionTypeShort masters data. It will take time to complete the sync, Do you want to continue ?" runat="server" Visible="false"></asp:Label>
                    <asp:Label ID="Label3" Text="This will sync student master data. It will take time to complete the sync, Do you want to continue ?" runat="server" Visible="false"></asp:Label>
                </b>
                </h5>

            </div>
            <br />
            <div style="text-align: center;">
                <asp:LinkButton ID="lnksyncyes" runat="server" Text="Yes" OnClick="LinkButton2_Click" class="btn green-meadow" Visible="false" />
                <asp:LinkButton ID="lnksyncyes2" runat="server" Text="Yes" OnClick="LinkButton3_Click" class="btn green-meadow" Visible="false" />
                <asp:LinkButton ID="lnksyncno" runat="server" Text="No" OnClick="LinkButton1_Click" class="btn green-meadow" />
            </div>
            <%-- </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="lnksyncyes" />
                        <asp:AsyncPostBackTrigger ControlID="lnksyncyes2" />
                        <asp:AsyncPostBackTrigger ControlID="lnksyncyes2" />
                    </Triggers>
                </asp:UpdatePanel>--%>
        </div>

    </div>
    <script>
        $('#ContentPlaceHolder1_txtUniqueNo').on('keypress', function () {
            var re = /([A-Z0-9a-z_-][^@])+?@[^$#<>?]+?\.[\w]{2,4}/.test(this.value);
            if (!re) {
                $('#error').show();
            } else {
                $('#error').hide();
            }
        })

    </script>
</asp:Content>
