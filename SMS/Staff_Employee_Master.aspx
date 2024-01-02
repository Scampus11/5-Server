<%@ Page Title="" Language="C#" MasterPageFile="~/Admin_Master.Master" AutoEventWireup="true" CodeBehind="Staff_Employee_Master.aspx.cs" Inherits="SMS.Staff_Employee_Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Staff</title>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#datetimepicker1').datetimepicker({ format: 'DD/MM/YYYY' });
            $('#datetimepicker2').datetimepicker({ format: 'DD/MM/YYYY' });
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
        });
        function EndRequestHandler(sender, args) {
            SetDatePicker();
        }
        function SetDatePicker() {
            $('#datetimepicker1').datetimepicker({ format: 'DD/MM/YYYY' });
            $('#datetimepicker2').datetimepicker({ format: 'DD/MM/YYYY' });
        }

    </script>

    <style>
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
    <br />
    <br />
    <div class="page-content-wrapper" style="margin-bottom: -16px;">
        <div class="page-content">
            <h3 class="page-title bold">Staffs
                <asp:LinkButton ID="lnkviewgrid" runat="server" Text="View Staff" class="btn green-meadow pull-right" OnClick="lnkviewgrid_Click" Visible="false" Style="display: none"></asp:LinkButton>
                <asp:LinkButton ID="lnkEdit_menu" runat="server" Text="Edit Staff" class="btn green-meadow pull-right" OnClick="lnkEdit_menu_Click" Visible="false" Style="display: none"></asp:LinkButton>
                <asp:LinkButton ID="lnkView_Menu" runat="server" Text="View Staff" class="btn green-meadow pull-right" OnClick="lnkView_Menu_Click" Visible="false" Style="display: none"></asp:LinkButton>
            </h3>

            <div class="row" id="divgrid" runat="server">
                <div class="col-md-12">
                    <div class="portlet box green-meadow">
                        <div class="portlet-title">
                            <div class="caption">
                                <i class='fas'>&#xf508;</i>Staff List
                            </div>
                            <div class="tools pull-left" style="margin-left: 10px; padding: 8px; border-left: 0px!important;">
                                <asp:TextBox ID="txtSearch" placeHolder="Search" runat="server" Width="230px" Height="28px" Style="border: 0px; color: black" />
                            </div>
                            <div class="tools pull-left" style="margin-left: 5px; padding: 8px; border-left: 0px!important;">
                                <asp:LinkButton ID="btnsearch" runat="server" OnClick="btnsearch_Click">
                                        <i class="fa fa-search" title="Search" style="font-size: 28px; color: white; margin-top: 4px;"></i>
                                </asp:LinkButton>
                            </div>
                            <div class="tools" id="divadd" runat="server" visible="true">
                                <asp:LinkButton ID="lnkAdd" runat="server" class="pull-right" Style="color: white" OnClick="lnkAdd_Click">
                                              <i class="fa fa-plus" title="Add New Staff"></i>
                                </asp:LinkButton>
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
                                <a id="lnkBase64" class="pull-right" style="color: white;" target="_blank" href="ConvertFolderImageToBase64.aspx?Name=Staff">
                                    <i class="fa fa-image" title="Convert to Base 64"></i>
                                </a>
                            </div>
                            <div class="tools">
                                <asp:LinkButton ID="lnkImportExcel" runat="server" Style="margin-bottom: 10px; color: white;" class="pull-right" OnClick="lnkImportExcel_Click">
                                              <i class="fa fa-plus-circle"></i> Import Excel  
                                </asp:LinkButton>
                            </div>
                        </div>
                        <div class="portlet-body" style="overflow: scroll">
                            <asp:Label ID="lblSyncmsg" runat="server" Visible="false" Text="Sync Completed" Style="color: green;"></asp:Label>
                            <asp:GridView ID="gridEmployee" runat="server" class="table table-striped table-bordered table-hover" AutoGenerateColumns="false"
                                AllowPaging="true" AllowSorting="true" OnSorting="OnSorting" OnPageIndexChanging="OnPageIndexChanging" PageSize="10"
                                ShowHeaderWhenEmpty="true" EmptyDataText="No records Found" OnRowDataBound="gridEmployee_RowDataBound">
                                <%--set pagination --%>
                                <PagerSettings Mode="NumericFirstLast" PageButtonCount="5" FirstPageText="First" LastPageText="Last" />
                                <PagerStyle CssClass="gridview"></PagerStyle>
                                <Columns>
                                    <asp:TemplateField ItemStyle-Width="10%" HeaderText="Images" HeaderStyle-ForeColor="#337ab7">
                                        <ItemTemplate>
                                            <asp:Label ID="lblpath" Visible="false" runat="server" Text='<%#Eval("Emp_Photo") %>'></asp:Label>
                                            <asp:Image ID="imgStaff" runat="server" Visible="false" ImageUrl='<%#Eval("Emp_Photo") %>' class="circle" />
                                            <asp:Image ID="imgdefault" runat="server" Visible="false" ImageUrl="~/Images/student.jpeg" class="circle" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="25%" HeaderText="Id" SortExpression="Id" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblId1" runat="server" Text='<%#Eval("Id") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField ItemStyle-Width="30px" HeaderText="UId" SortExpression="UId" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblUId1" runat="server" Text='<%#Eval("UId") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField ItemStyle-Width="10%" HeaderText="Staff Id" SortExpression="Staff_Id">
                                        <ItemTemplate>
                                            <asp:Label ID="lblStaff_Id" runat="server" Text='<%#Eval("Staff_Id") %>'></asp:Label>
                                            <asp:Label ID="lblid" runat="server" Text='<%#Eval("Id") %>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField ItemStyle-Width="25%" HeaderText="Full Name" SortExpression="Full_Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFull_Name" runat="server" Text='<%#Eval("Full_Name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField ItemStyle-Width="10%" HeaderText="Email Id" SortExpression="Email_Id">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEmail_Id" runat="server" Text='<%#Eval("Email_Id") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="20%" HeaderText="Department Name" SortExpression="Dept">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDepartment" runat="server" Text='<%#Eval("Dept") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="20%" HeaderText="Job Title" SortExpression="jobtitle">
                                        <ItemTemplate>
                                            <asp:Label ID="lblJob_Title" runat="server" Text='<%#Eval("jobtitle") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="5%" HeaderText="Edit" HeaderStyle-ForeColor="#337ab7">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="linkEdit" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id")%>' OnClick="linkEdit_Click"><i class="material-icons" title="Edit row">&#xe3c9;</i></asp:LinkButton>
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
                        <!-- Nav tabs -->
                        <ul class="nav nav-tabs">
                            <li class="nav-item active" id="liPersonalDetails" runat="server">
                                <a class="nav-link" data-toggle="tab" href="#ContentPlaceHolder1_divPersonalDetails" id="aPersonalDetails" runat="server">Personal Information</a>
                            </li>
                            <li class="nav-item" id="liEducationinformation" runat="server">
                                <a class="nav-link" data-toggle="tab" href="#ContentPlaceHolder1_divEducationinformation" id="aEducationinformation" runat="server">Company Information</a>
                            </li>
                            <li class="nav-item" id="liOtherinformaion" runat="server">
                                <a class="nav-link" data-toggle="tab" href="#ContentPlaceHolder1_divOtherinformaion" id="aOtherinformaion" runat="server">Access Information</a>
                            </li>
                            <li class="nav-item" id="liAddress" runat="server">
                                <a class="nav-link" data-toggle="tab" href="#ContentPlaceHolder1_divAddress" id="aAddress" runat="server">Address</a>
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
                                                        Staff Id :
                                                    </label>

                                                    <div class="col-md-6">
                                                        <asp:TextBox ID="txtStudentID" runat="server" CssClass="form-control" BackColor="#d3d3d3" Style="border-color: #37359C;" placeholder="Staff Id"></asp:TextBox>
                                                        <asp:Label ID="lblId" runat="server" Visible="false"></asp:Label>
                                                        <asp:Label ID="lblValidstudent" runat="server" Style="color: red;" Text="* Enter Staff ID..." Visible="false"></asp:Label>
                                                        <asp:Label ID="lblValidstudentalready" runat="server" Style="color: red;" Text="* Staff id already exits..." Visible="false"></asp:Label>
                                                    </div>

                                                </div>
                                            </div>

                                        </div>
                                    </div>

                                    <div class="col-md-6 col-sm-6">
                                        <div class="portlet-body">
                                            <div class="form-body">
                                                <div class="form-group">
                                                    <label class="col-md-3 control-label">Full Name : </label>
                                                    <div class="col-md-6">
                                                        <asp:TextBox ID="txtFull_Name" runat="server" CssClass="form-control" placeholder="Full Name" BackColor="#d3d3d3" Style="border-color: #37359C;"></asp:TextBox>
                                                        <asp:Label ID="lblvalidFullname" runat="server" Style="color: red;" Text="* Enter full name..." Visible="false"></asp:Label>
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
                                                        Employee Code :
                                                    </label>

                                                    <div class="col-md-6">
                                                        <asp:TextBox ID="txtEmployeeCode" runat="server" CssClass="form-control" placeholder="Employee Code"></asp:TextBox>
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
                                                        Full Name (amharic) :
                                                    </label>

                                                    <div class="col-md-6">
                                                        <asp:TextBox ID="txtamharicFullName" runat="server" CssClass="form-control" placeholder="FullName(amharic)"></asp:TextBox>
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
                                                        Date Of Birth :
                                                    </label>

                                                    <div class="col-md-6">
                                                        <div class='input-group date' id='datetimepicker1'>
                                                            <div class="input-group">
                                                                <asp:TextBox ID="txtDOB" runat="server" CssClass="form-control" placeholder="Date Of Birth"></asp:TextBox>
                                                                <span class="input-group-addon">
                                                                    <span class="glyphicon glyphicon-calendar"></span>
                                                                </span>
                                                            </div>
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


                                    <div class="clearfix"></div>
                                    <div class="col-md-6 col-sm-6">
                                        <div class="portlet-body">
                                            <div class="form-body">
                                                <div class="form-group">
                                                    <label class="col-md-3 control-label">Blood Group : </label>
                                                    <div class="col-md-6">
                                                        <asp:DropDownList ID="ddlBloodgroup" runat="server" CssClass="form-control">
                                                            <asp:ListItem Value="0">Select Blood Group</asp:ListItem>
                                                            <asp:ListItem Value="A+">A+</asp:ListItem>
                                                            <asp:ListItem Value="A-">A-</asp:ListItem>
                                                            <asp:ListItem Value="B+">B+</asp:ListItem>
                                                            <asp:ListItem Value="B-">B-</asp:ListItem>
                                                            <asp:ListItem Value="O+">O+</asp:ListItem>
                                                            <asp:ListItem Value="O-">O-</asp:ListItem>
                                                            <asp:ListItem Value="AB+">AB+</asp:ListItem>
                                                            <asp:ListItem Value="AB-">AB-</asp:ListItem>
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
                                                    <label class="col-md-3 control-label">Personal Phone : </label>
                                                    <div class="col-md-6">
                                                        <asp:TextBox ID="txtPersonalPhone" runat="server" CssClass="form-control" TextMode="Number" placeholder="Personal Phone"></asp:TextBox>
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
                                                    <label class="col-md-3 control-label">Email Address : </label>
                                                    <div class="col-md-6">
                                                        <asp:TextBox ID="txtEmail_Id" runat="server" CssClass="form-control" BackColor="#d3d3d3" Style="border-color: #37359C;" placeholder="Email Address"></asp:TextBox>
                                                        <span id="error" style="display: none; color: red;">Wrong email</span>
                                                        <asp:Label ID="lblValidEmail" runat="server" Style="color: red;" Text="* Enter email id..." Visible="false"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-6 col-sm-6">
                                        <div class="portlet-body">
                                            <div class="form-body">
                                                <div class="form-group">
                                                    <label class="col-md-3 control-label">password : </label>
                                                    <div class="col-md-6">
                                                        <asp:TextBox ID="txtpassword" runat="server" CssClass="form-control" BackColor="#d3d3d3" placeholder="password" Style="border-color: #37359C;"></asp:TextBox>
                                                        <asp:HiddenField ID="hdnpassword" runat="server"></asp:HiddenField>
                                                        <asp:Label ID="lblvalidPassword" runat="server" Style="color: red;" Text="* Enter password..." Visible="false"></asp:Label>
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
                                                    <label class="col-md-3 control-label">Address : </label>
                                                    <div class="col-md-6">
                                                        <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control" placeholder="Address"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-6 col-sm-6">
                                        <div class="portlet-body">
                                            <div class="form-body">
                                                <div class="form-group">
                                                    <label class="col-md-3 control-label">Country Name : </label>
                                                    <div class="col-md-6">
                                                        <asp:DropDownList ID="ddlCountry" runat="server" CssClass="form-control">
                                                            <asp:ListItem Value="0">Select Country</asp:ListItem>
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
                                                    <label class="col-md-3 control-label">Staff Image : </label>

                                                    <div class="col-md-6">
                                                        <asp:Image ID="imgStaff" runat="server" Height="120px" Width="200px" Visible="false" ImageUrl="~/Images/student.jpeg" /><br />
                                                        <%--<span class="btn btn-default btn-file">--%>
                                                        <label class="btn btn-default">
                                                            <asp:FileUpload ID="FileUpload1" runat="server" />
                                                        </label>
                                                        <span style="color: darkviolet">Note : Only Image Suported Format is .jpg | .jpeg | .png | .bmp | .gif | .eps...</span>
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
                                                        <asp:Image ID="imgSignature" runat="server" Height="120px" Width="200px" Visible="false" ImageUrl="~/Images/student.jpeg" /><br />
                                                        <%--<span class="btn btn-default btn-file">--%>
                                                        <label class="btn btn-default">
                                                            <asp:FileUpload ID="FileUpload2" runat="server" />
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
                                                        Department :
                                                    </label>

                                                    <div class="col-md-6">
                                                        <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="form-control">
                                                            <asp:ListItem Value="0">Select Department</asp:ListItem>
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
                                                        Job Title :
                                                    </label>

                                                    <div class="col-md-6">
                                                        <asp:DropDownList ID="ddljobtitle" runat="server" CssClass="form-control">
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
                                                        Company Name :
                                                    </label>
                                                    <div class="col-md-6">
                                                        <asp:DropDownList ID="ddlCompany" runat="server" CssClass="form-control">
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
                                                        Issue Date :
                                                    </label>

                                                    <div class="col-md-6">
                                                        <div class='input-group date' id='datetimepicker2'>
                                                            <div class="input-group">
                                                                <asp:TextBox ID="txtIssue_Date" runat="server" CssClass="form-control" placeholder="Issue Date"></asp:TextBox>
                                                                <span class="input-group-addon">
                                                                    <span class="glyphicon glyphicon-calendar"></span>
                                                                </span>
                                                            </div>
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
                                                    <label class="col-md-3 control-label">
                                                        Facility :
                                                    </label>

                                                    <div class="col-md-6">
                                                        <asp:DropDownList ID="ddlFacility" runat="server" CssClass="form-control">
                                                            <asp:ListItem Value="0">Select Facility</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>

                                                </div>
                                            </div>

                                        </div>
                                    </div>

                                </div>

                            </div>
                            <div class="tab-pane container fade" id="divOtherinformaion" runat="server">
                                <div class="row ">
                                    <div class="col-md-6 col-sm-6">
                                        <div class="portlet-body">
                                            <div class="form-body">
                                                <div class="form-group">
                                                    <label class="col-md-3 control-label">
                                                        UID/UHF :
                                                    </label>
                                                    <div class="col-md-6">
                                                        <asp:TextBox ID="txtUID" runat="server" CssClass="form-control" placeholder="UID/UHF"></asp:TextBox>
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
                                                        Vehicle Number/Plate No  :
                                                    </label>
                                                    <div class="col-md-6">
                                                        <asp:TextBox ID="txtVehicleNumber" runat="server" CssClass="form-control" placeholder="Vehicle Number/Plate No"></asp:TextBox>
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
                                                            <asp:ListItem Value="7">Temporary Expired</asp:ListItem>
                                                            <asp:ListItem Value="8">Absconded</asp:ListItem>
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
                                                        <asp:TextBox ID="txtCard_no" runat="server" CssClass="form-control" placeholder="Card Number"></asp:TextBox>
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
                                                        Canteen :
                                                    </label>
                                                    <div class="col-md-6">
                                                        <asp:DropDownList ID="ddlcanteen2" runat="server" CssClass="form-control"></asp:DropDownList>
                                                        <asp:Label ID="lblAC" runat="server" Text="* Please select atleast one available access group or available canteen" Visible="false" ForeColor="Red"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </div>
                            <div class="tab-pane container fade" id="divAddress" runat="server">
                                <div class="row ">
                                    <div class="col-md-6 col-sm-6">
                                        <div class="portlet-body">
                                            <div class="form-body">
                                                <div class="form-group">
                                                    <label class="col-md-3 control-label">
                                                        Woreda :
                                                    </label>
                                                    <div class="col-md-6">
                                                        <asp:TextBox ID="txtWoreda" runat="server" CssClass="form-control" placeholder="Woreda"></asp:TextBox>
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
                                                        Subcity  :
                                                    </label>
                                                    <div class="col-md-6">
                                                        <asp:TextBox ID="txtSubcity" runat="server" CssClass="form-control" placeholder="Subcity"></asp:TextBox>
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
                                                        House Number :
                                                    </label>
                                                    <div class="col-md-6">
                                                        <asp:TextBox ID="txtHouseNumber" runat="server" CssClass="form-control" placeholder="House Number"></asp:TextBox>
                                                    </div>

                                                </div>
                                            </div>

                                        </div>
                                    </div>

                                    <div class="clearfix"></div>

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
                                                <asp:LinkButton ID="lnkClear" runat="server" Text="Clear" OnClick="lnkClear_Click" class="btn green-meadow" />
                                                <asp:HiddenField ID="HiddenField1" runat="server"></asp:HiddenField>
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
                            <label class="text-success">Application No : </label>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="description" style="width: 100%; border: 1px solid #ddd; padding: 10px; border-radius: 5px">
                                        <div class="row">
                                            <div class="col-md-12" id="Div1">
                                                <div class="input-group">
                                                    <asp:TextBox ID="txtApplication_No" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="form-group" style="display: none">
                            <label class="text-success">SL No : </label>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="description" style="width: 100%; border: 1px solid #ddd; padding: 10px; border-radius: 5px">
                                        <div class="row">
                                            <div class="col-md-12" id="Div2">
                                                <div class="input-group">
                                                    <asp:TextBox ID="txtSL_No" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="form-group" style="display: none">
                            <label class="text-success">ID No : </label>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="description" style="width: 100%; border: 1px solid #ddd; padding: 10px; border-radius: 5px">
                                        <div class="row">
                                            <div class="col-md-12" id="Div11">
                                                <div class="input-group">
                                                    <asp:TextBox ID="txtID_no" runat="server"></asp:TextBox>
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
    <asp:HiddenField ID="hdnimage" runat="server" />
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
                    <asp:Label ID="Label2" Text="This will sync Department,Job title and Facility masters data. It will take time to complete the sync, Do you want to continue ?" runat="server" Visible="false"></asp:Label>
                    <asp:Label ID="Label3" Text="This will sync staff master data. It will take time to complete the sync, Do you want to continue ?" runat="server" Visible="false"></asp:Label>
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
    <script>
        $('#ContentPlaceHolder1_txtEmail_Id').on('keypress', function () {
            var re = /([A-Z0-9a-z_-][^@])+?@[^$#<>?]+?\.[\w]{2,4}/.test(this.value);
            if (!re) {
                $('#error').show();
            } else {
                $('#error').hide();
            }
        })

    </script>
</asp:Content>
