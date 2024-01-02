<%@ Page Title="" Language="C#" MasterPageFile="~/Admin_Master.Master" AutoEventWireup="true" CodeBehind="SMS_Service_Master.aspx.cs" Inherits="SMS.SMS_Service_Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Services</title>
    <script src="js/jquery.js" type="text/javascript"></script>
    <script type="text/javascript">

        $(document).ready(function () {
            //$('#add').click(function () {
            //    return !$('#ContentPlaceHolder1_lstEmp option:selected').remove().appendTo('#ContentPlaceHolder1_lstmoveemp');
            //});
            //$('#remove').click(function () {
            //    return !$('#ContentPlaceHolder1_lstmoveemp option:selected').remove().appendTo('#ContentPlaceHolder1_lstEmp');
            //});
            $('#datetimepicker1').datetimepicker();
            $('#datetimepicker2').datetimepicker();
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
        });
        function EndRequestHandler(sender, args) {
            SetDatePicker();
        }
        function SetDatePicker() {
            // $('#add').click(function () {
            //    return !$('#ContentPlaceHolder1_lstEmp option:selected').remove().appendTo('#ContentPlaceHolder1_lstmoveemp');
            //});
            //$('#remove').click(function () {
            //    return !$('#ContentPlaceHolder1_lstmoveemp option:selected').remove().appendTo('#ContentPlaceHolder1_lstEmp');
            //});
            $('#datetimepicker1').datetimepicker();
            $('#datetimepicker2').datetimepicker();
        }
    </script>


    <%--<style type="text/css">
        a {
            display: block;
            border: 1px solid #aaa;
            text-decoration: none;
            background-color: #fafafa;
            color: #123456;
            margin: 2px;
            clear: both;
        }

        div {
            float: left;
            text-align: center;
            margin: 10px;
        }

        select {
            width: 100px;
            height: 80px;
        }
    </style>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdateRefresh" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <br />
            <br />
            <div class="page-content-wrapper" style="margin-bottom: -16px;">
                <div class="page-content">
                    <h3 class="page-title bold">Services</h3>
                    <div class="row" id="divgrid" runat="server">
                        <div class="col-md-12">
                            <div class="portlet box green-meadow">
                                <div class="portlet-title">
                                    <div class="caption">
                                        <i  class='far'>&#xf2dc;</i>Services List
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
                                              <i class="fa fa-plus" title="Add New Service"></i>
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
                                            <asp:TemplateField ItemStyle-Width="30px" HeaderText="Service Name" SortExpression="Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblServiceName" runat="server" Text='<%#Eval("Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="30px" HeaderText="Service Description" SortExpression="Description">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDescription" runat="server" Text='<%#Eval("Description") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="30px" HeaderText="Start DateTime" SortExpression="StartDateTime">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStartDateTime" runat="server" Text='<%#Eval("StartDateTime") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="30px" HeaderText="End Datetime" SortExpression="EndDatetime">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEndDatetime" runat="server" Text='<%#Eval("EndDatetime") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="30px" HeaderText="Department" SortExpression="DeptId">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDeptId" runat="server" Text='<%#Eval("DeptId") %>'></asp:Label>
                                                    <asp:Label ID="lblid" runat="server" Text='<%#Eval("Id") %>' Visible="false"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="30px" HeaderText="Employee Name" SortExpression="Employee">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEmployee" runat="server" Text='<%#Eval("Employee") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="10px" HeaderText="Edit">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="linkEdit" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id")%>' OnClick="linkEdit_Click"><i class="material-icons" title="Edit row">&#xe3c9;</i></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="10px" HeaderText="Delete">
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
                                        <div class="row">
                                            <div class="col-md-6 col-sm-6">
                                                <div class="portlet-body">
                                                    <div class="form-body">
                                                        <div class="form-group">
                                                            <label class="col-md-3 control-label">
                                                                Service Name :
                                                            </label>
                                                            <div class="col-md-6">
                                                                <asp:TextBox ID="txtService" runat="server" CssClass="form-control" BackColor="#d3d3d3" placeholder="Service Name"></asp:TextBox>
                                                                <asp:Label ID="lblService" runat="server" Style="color: red;" Text="* Enter service name..." Visible="false"></asp:Label>
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
                                                                Service Description :
                                                            </label>
                                                            <div class="col-md-6">
                                                                <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control" placeholder="Service Description"></asp:TextBox>

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
                                                                Start DateTime :
                                                            </label>
                                                            <div class="col-md-6">
                                                                <div class='input-group date' id='datetimepicker1'>

                                                                    <asp:TextBox ID="txtStartDate" runat="server" BackColor="#d3d3d3" placeholder="Start DateTime" CssClass="form-control"></asp:TextBox>
                                                                    <span class="input-group-addon">
                                                                        <span class="glyphicon glyphicon-calendar"></span>
                                                                    </span>
                                                                </div>
                                                                <asp:Label ID="lblStartDate" runat="server" Style="color: red;" Text="* Enter start datetime..." Visible="false"></asp:Label>
                                                                <asp:Label ID="lblvalidValidDateUntil2" runat="server" Style="color: red;" Text="*Start datetime does not less than end datetime..." Visible="false"></asp:Label>
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
                                                                End DateTime :
                                                            </label>
                                                            <div class="col-md-6">
                                                                <div class='input-group date' id='datetimepicker2'>

                                                                    <asp:TextBox ID="txtEndDate" runat="server" BackColor="#d3d3d3" placeholder="End DateTime" CssClass="form-control"></asp:TextBox>
                                                                    <span class="input-group-addon">
                                                                        <span class="glyphicon glyphicon-calendar"></span>
                                                                    </span>
                                                                </div>
                                                                <asp:Label ID="lblEndDate" runat="server" Style="color: red;" Text="* Enter end datetime..." Visible="false"></asp:Label>
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
                                                                <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                                <asp:Label ID="lblDepartment" runat="server" Style="color: red;" Text="* Enter start datetime..." Visible="false"></asp:Label>
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
                                                                Available Host Employee :
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
                                                                Assigned Host Employee :
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


    <%-- <script>
        $('form').submit(function() {  
 $('#ContentPlaceHolder1_lstmoveemp option').each(function(i) {  
  $(this).attr("selected", "selected");  
 });  
});  
    </script>--%>


    <%--<div>  
  <select multiple id="select1">  
   <option value="1">Option 1</option>  
   <option value="2">Option 2</option>  
   <option value="3">Option 3</option>  
   <option value="4">Option 4</option>  
  </select>  
  <a href="#" id="add">add &gt;&gt;</a>  
 </div>
    <div>  
  <select multiple id="select2"></select>  
  <a href="#" id="remove">&lt;&lt; remove</a>  
 </div>--%>
</asp:Content>
