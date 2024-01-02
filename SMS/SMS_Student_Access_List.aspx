<%@ Page Title="" Language="C#" MasterPageFile="~/Admin_Master.Master" AutoEventWireup="true" CodeBehind="SMS_Student_Access_List.aspx.cs" Inherits="SMS.SMS_Student_Access_List" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Student Access</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdateRefresh" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <br />
            <br />
            <div class="page-content-wrapper" style="margin-bottom: -16px;">
                <div class="page-content">
                    <h3 class="page-title bold">Student Access
                <asp:LinkButton ID="lnkAdd" runat="server" Text="Add New Student Access List " class="btn green-meadow pull-right" OnClick="lnkAdd_Click" Visible="false">
                </asp:LinkButton>
                    </h3>
                    <div class="row" id="divgrid" runat="server">
                        <div class="col-md-12">
                            <div class="portlet box green-meadow">
                                <div class="portlet-title">
                                    <div class="caption">
                                        <i class='fas'>&#xf502;</i>Student Access List  
                                    </div>
                                    <div class="tools pull-left" style="margin-left: 10px; padding: 8px; border-left: 0px!important;">
                                        <asp:TextBox ID="txtSearch" placeHolder="Search" runat="server" Width="230px" Height="28px" Style="border: 0px; color: black" />
                                    </div>
                                    <div class="tools pull-left" style="margin-left: 5px; padding: 8px; border-left: 0px!important;">
                                        <asp:LinkButton ID="btnsearch" runat="server" OnClick="btnsearch_Click">
                                        <i class="fa fa-search" title="Search" style="font-size: 28px; color: white; margin-top: 4px;"></i>
                                        </asp:LinkButton>
                                    </div>
                                    <div class="tools">
                                        <asp:LinkButton ID="lnkadvanceSearch" runat="server" OnClick="lnkadvanceSearch_Click" Text="Bulk AG" ToolTip="Bulk AG" Font-Bold="true" ForeColor="White"></asp:LinkButton>
                                    </div>
                                </div>
                                <div class="portlet-body" style="overflow: scroll">
                                    <div runat="server" id="divAdvanceSearch" visible="false">
                                        <div class="row">
                                            <div class='col-sm-12'>
                                                <div class="form-group">
                                                    <div class="col-lg-4">
                                                        <span>Select Campus :</span>
                                                        <asp:DropDownList ID="ddlCampus" CssClass="form-control" runat="server" OnTextChanged="ddlCampus_TextChanged" AutoPostBack="true"></asp:DropDownList>
                                                    </div>
                                                    <div class="col-lg-4">
                                                        <span>Select College :</span>
                                                        <asp:DropDownList ID="ddlCollege" CssClass="form-control" runat="server" OnTextChanged="ddlCollege_TextChanged" AutoPostBack="true">
                                                            <asp:ListItem Value="0">--Select College--</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-lg-4">
                                                        <span>Select Department :</span>
                                                        <asp:DropDownList ID="ddlDepartment" CssClass="form-control" runat="server"> <%--OnTextChanged="ddlDepartment_TextChanged" AutoPostBack="true"--%>
                                                            <asp:ListItem Value="0">--Select Department--</asp:ListItem>
                                                        </asp:DropDownList>

                                                    </div>

                                                </div>
                                            </div>
                                        </div>
                                        <div class="clearfix"></div>
                                        <hr />
                                        <div class="row">
                                            <div class='col-sm-12'>
                                                <div class="form-group">
                                                    <div class="col-lg-4">
                                                        <span>Select Batch Year :</span>
                                                        <asp:DropDownList ID="ddlBatchYear" CssClass="form-control" runat="server">
                                                             <%--OnTextChanged="ddlBatchYear_TextChanged" AutoPostBack="true"--%>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-lg-4">
                                                        <span>Select Admission Type :</span>
                                                        <asp:DropDownList ID="ddlAdmissiontype" CssClass="form-control" runat="server">
                                                             <%--OnTextChanged="ddlAdmissiontype_TextChanged" AutoPostBack="true"--%>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-lg-4">
                                                        <span>Select Gender :</span>
                                                        <asp:DropDownList ID="drpGender" runat="server" CssClass="form-control"> <%--OnTextChanged="drpGender_TextChanged" AutoPostBack="true"--%>
                                                            <asp:ListItem Value="0">--Select Gender--</asp:ListItem>
                                                            <asp:ListItem Value="Male">Male</asp:ListItem>
                                                            <asp:ListItem Value="Female">Female</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="clearfix"></div>
                                          <hr />
                                        <div class="row">
                                            <div class='col-sm-12'>
                                                <div class="form-group">
                                                    <div class="col-lg-1">
                                                        <asp:LinkButton ID="lnkFilterlist" runat="server" CssClass="btn green-meadow" OnClick="lnkFilterlist_Click" Text="Go Filter"></asp:LinkButton>
                                                    </div>
                                                    <div class="col-lg-2">
                                                        <asp:LinkButton ID="lnkClearFilter" runat="server" CssClass="btn green-meadow" OnClick="lnkClearFilter_Click" Text="Clear Filter"></asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="clearfix"></div>
                                        <hr />
                                        <div class="row" id="divStudentCanteens" runat="server">
                                            <div class='col-sm-12'>
                                                <div class="form-group">
                                                    <div class="col-md-4">
                                                        Available Canteens :
                            <asp:DropDownList ID="ddlcanteen" CssClass="form-control" runat="server"></asp:DropDownList>

                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <hr />
                                        <div class="row" id="divStudentAG" runat="server">
                                            <div class="form-group">
                                                <span style="margin-left: 30px;">Available Access Groups : </span>
                                                <div class="col-md-12">
                                                    <div runat="server">

                                                        <asp:Repeater ID="rptAccessgrp" runat="server">
                                                            <ItemTemplate>
                                                                <div class="col-md-4">
                                                                    <asp:CheckBox ID="chkAccessGrp" CssClass="checkbox" Style="margin-left: 22px;" runat="server" Text='<%# Eval("Access_Group_Name")  %>' />
                                                                    <asp:Label ID="lblAccessGrp" runat="server" Text='<%# Eval("Id") %>' Visible="false"></asp:Label>
                                                                </div>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <hr />
                                        <div class="row" id="divStudentBG" runat="server">
                                            <span style="margin-left: 30px;">Available Block Groups : </span>
                                            <div class='col-sm-12'>
                                                <div class="form-group">
                                                    <div runat="server" id="divblock">
                                                        <asp:Repeater ID="rptBulkBlockGroup" runat="server">
                                                            <ItemTemplate>
                                                                <div class="col-md-4">
                                                                    <asp:CheckBox ID="chkBulkBlockGroup" CssClass="checkbox" Style="margin-left: 22px;" runat="server" Text='<%# Eval("Name")  %>' />&nbsp;
                                    <asp:Label ID="lblBulkBlockGroup" runat="server" Text='<%# Eval("Id") %>' Visible="false"></asp:Label>
                                                                </div>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </div>
                                                </div>
                                            </div>
                                            <hr />
                                        </div>
                                        <br />
                                        <asp:LinkButton ID="lnkStudent" class="btn green-meadow" runat="server" Text="Update" OnClick="lnkStudent_Click" Visible="false"></asp:LinkButton>
                                        &nbsp;&nbsp;&nbsp;
                            <asp:LinkButton ID="lnkRemoveAll" class="btn green-meadow" runat="server" Text="RemoveAllAG" OnClick="lnkRemoveAll_Click" Visible="false"></asp:LinkButton>
                                    </div>
                                    <br />
                                    <asp:Label ID="lblValchk" runat="server" Text="* Please select atleast one student list" Visible="false" ForeColor="Red" Font-Size="Large"></asp:Label>
                                    <asp:Label ID="lblVal" runat="server" Text="* Please select atleast one available access groups or available canteens" Visible="false" ForeColor="Red" Font-Size="Large"></asp:Label>
                                    <asp:Label ID="lblmsg" runat="server" Visible="false" Text="Insert Successfully!" Style="color: green"></asp:Label>
                                    <asp:Label ID="lblmsg1" runat="server" Visible="false" Text="Remove Successfully!" Style="color: red"></asp:Label>
                                    <asp:GridView ID="gridEmployee" runat="server" class="table table-striped table-bordered table-hover" AutoGenerateColumns="false" AllowPaging="true" AllowSorting="true" OnSorting="OnSorting" OnPageIndexChanging="OnPageIndexChanging" PageSize="10" ShowHeaderWhenEmpty="true" EmptyDataText="No records Found">
                                        <PagerSettings Mode="NumericFirstLast" PageButtonCount="5" FirstPageText="First" LastPageText="Last" />
                                        <PagerStyle CssClass="gridview"></PagerStyle>
                                        <Columns>
                                            <asp:TemplateField ItemStyle-Width="1%">
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkALLStudent" runat="server" OnCheckedChanged="chkALLStudent_CheckedChanged" AutoPostBack="true" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkStudent" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="30px" HeaderText="Access List  Id" SortExpression="Id" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblId1" runat="server" Text='<%#Eval("Id") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="9%" HeaderText="Student Id" SortExpression="StudentId">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblApplication_No" runat="server" Text='<%#Eval("StudentId") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField ItemStyle-Width="11%" HeaderText="Student Name" SortExpression="Student_Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStudent_Name" runat="server" Text='<%#Eval("Student_Name") %>'></asp:Label>
                                                    <asp:Label ID="lblid" runat="server" Text='<%#Eval("Id") %>' Visible="false"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="10%" HeaderText="College Name" SortExpression="College">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCollege" runat="server" Text='<%#Eval("College") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="10%" HeaderText="Department  Name" SortExpression="Department">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDepartment" runat="server" Text='<%#Eval("Department") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="10%" HeaderText="Admission Type" SortExpression="AdmissionType">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAdmissionType" runat="server" Text='<%#Eval("AdmissionType") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="10%" HeaderText="Campus Name" SortExpression="Campus">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCampus" runat="server" Text='<%#Eval("Campus") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="5%" HeaderText="Batch Year" SortExpression="Batch_Year">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBatch_Year" runat="server" Text='<%#Eval("Batch_Year") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="8%" HeaderText="Access Group Name" SortExpression="Access_Group_ID">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblReader_id" runat="server" Text='<%#Eval("Access_Group_ID") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="8%" HeaderText="Canteen Name" SortExpression="Canteen">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCanteen" runat="server" Text='<%#Eval("Canteen") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="8%" HeaderText="Block Group" SortExpression="BlockGpName">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBlockGpName" runat="server" Text='<%#Eval("BlockGpName") %>'></asp:Label>
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
                            <!-- END EXAMPLE TABLE PORTLET-->
                        </div>
                    </div>
                    <div class="modal-content" id="divView" runat="server" visible="false">
                        <div class="modal-body">
                            <div class="portlet-body form">
                                <!-- BEGIN FORM-->
                                <div class="form-body">
                                    <div class="form-group">
                                        <label class="text-success">Student  Id : </label>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="description" style="width: 100%; border: 1px solid #ddd; padding: 10px; border-radius: 5px">
                                                    <div class="row">
                                                        <div class="col-md-12" id="Div1">
                                                            <div class="input-group">
                                                                <asp:Label ID="txtApplication_No" runat="server"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label class="text-success">Student Name : </label>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="description" style="width: 100%; border: 1px solid #ddd; padding: 10px; border-radius: 5px">
                                                    <div class="row">
                                                        <div class="col-md-12" id="Div2">
                                                            <div class="input-group">
                                                                <asp:Label ID="txtDescription" runat="server"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group" runat="server" id="diveditAG">
                                        <label class="text-success">Available Access Groups : </label>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="description" style="width: 100%; border: 1px solid #ddd; padding: 10px; border-radius: 5px">
                                                    <div class="row">
                                                        <div class="col-md-12" id="Div3">
                                                            <div>
                                                                <asp:Repeater ID="RepeatReader" runat="server">
                                                                    <ItemTemplate>
                                                                        <div class="col-md-4">
                                                                            <asp:CheckBox ID="chkReader" CssClass="checkbox" runat="server" Text='<%# Eval("Access_Group_Name")  %>' />
                                                                            <asp:Label ID="lblId" runat="server" Text='<%# Eval("Id") %>' Visible="false"></asp:Label>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                </asp:Repeater>
                                                                <asp:Label ID="lblAAG" runat="server" Text="* Please select atleast one available access groups or available canteens" Visible="false" ForeColor="Red"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group" runat="server" id="diveditcanteens">
                                        <label class="text-success">Available Canteens : </label>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="description" style="width: 100%; border: 1px solid #ddd; padding: 10px; border-radius: 5px">
                                                    <div class="row">
                                                        <div class="col-md-12" id="Div5">
                                                            <div class="input-group">
                                                                <asp:DropDownList ID="ddlcanteen2" CssClass="form-control" runat="server"></asp:DropDownList>
                                                                <asp:Label ID="lblAC" runat="server" Text="* Please select atleast one available access groups or available canteens" Visible="false" ForeColor="Red"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group" style="display: none">
                                        <label class="text-success">Available Sessions : </label>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="description" style="width: 100%; border: 1px solid #ddd; padding: 10px; border-radius: 5px">
                                                    <div class="row">
                                                        <div class="col-md-12" id="Div4">
                                                            <div>
                                                                <asp:Repeater ID="rptSession" runat="server">
                                                                    <ItemTemplate>
                                                                        <div class="col-md-4">
                                                                            <asp:CheckBox ID="chkSession" CssClass="checkbox" runat="server" Text='<%# Eval("Session_Name")  %>' />&nbsp;
                                                                                        <asp:Label ID="lblSession_Id" runat="server" Text='<%# Eval("Session_Id") %>' Visible="false"></asp:Label>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                </asp:Repeater>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group" runat="server" id="diveditBG">
                                        <label class="text-success">Available Block Groups : </label>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="description" style="width: 100%; border: 1px solid #ddd; padding: 10px; border-radius: 5px">
                                                    <div class="row">
                                                        <div class="col-md-12" id="DivBlockGroup">
                                                            <div>
                                                                <asp:Repeater ID="RepeatBlock" runat="server">
                                                                    <ItemTemplate>
                                                                        <div class="col-md-4">
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
                                        </div>
                                    </div>
                                    <br />
                                    <asp:LinkButton ID="lnksave" runat="server" Text="Save" OnClick="btnsave_Click" class="btn green-meadow" Visible="false" />
                                    <asp:LinkButton ID="lnkupdate" runat="server" Text="Update" OnClick="btnupdate_Click" class="btn green-meadow" Visible="false" />
                                    <asp:LinkButton ID="lnkBack" runat="server" Text="Back" OnClick="lnkBack_Click" class="btn green-meadow" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div id="myModal" runat="server" class="modal">
                <!-- Modal content -->
                <div class="modal-content" style="top: 100px; left: 320px; width: 700px; height: 200px;">
                    <br />
                    <br />
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <ContentTemplate>
                            <div style="text-align: center;">
                                <h5><b style="color: green">
                                    <asp:Label ID="Label1" runat="server"></asp:Label></b></h5>
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
                            <asp:AsyncPostBackTrigger ControlID="lnkFilterlist" />
                            <asp:AsyncPostBackTrigger ControlID="lnkpushCancel" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>

            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="lnkStudent" />
        </Triggers>
    </asp:UpdatePanel>

</asp:Content>
