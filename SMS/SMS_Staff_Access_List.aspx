<%@ Page Title="" Language="C#" MasterPageFile="~/Admin_Master.Master" AutoEventWireup="true" CodeBehind="SMS_Staff_Access_List.aspx.cs" Inherits="SMS.SMS_Staff_Access_List" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Staff Access</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:UpdatePanel ID="UpdateRefresh" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <br />
            <br />
            <div class="page-content-wrapper" style="margin-bottom: -16px;">
                <div class="page-content">
                    <h3 class="page-title bold">Staff Access
                <asp:LinkButton ID="lnkAdd" runat="server" Text="Add New Staff Access List " class="btn green-meadow pull-right" OnClick="lnkAdd_Click" Visible="false">
                </asp:LinkButton>
                    </h3>
                    <div class="row" id="divgrid" runat="server">
                        <div class="col-md-12">
                            <div class="portlet box green-meadow">
                                <div class="portlet-title">
                                    <div class="caption">
                                        <i class='fas'>&#xf4ff;</i>Staff  Access List  
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
                                        <asp:LinkButton ID="lnkadvanceSearch" runat="server" OnClick="lnkadvanceSearch_Click" Text="Bulk AG" ToolTip="Bulk AG" Font-Bold="true" ForeColor="White"></asp:LinkButton>
                                    </div>
                                </div>

                                <div class="portlet-body" style="overflow: scroll">
                                    <div runat="server" id="divAdvanceSearch" visible="false">
                                        <div class="row">
                                            <div class='col-sm-12'>
                                                <div class="form-group">
                                                    <div class="col-lg-4">
                                                        <span>Select Department :</span>
                                                        <asp:DropDownList ID="ddlDepartment" CssClass="form-control" runat="server" OnTextChanged="ddlDepartment_TextChanged" AutoPostBack="true"></asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <hr />
                                        <div class="row" runat="server" id="divStaffAG">
                                            <div class="form-group">
                                                <span style="margin-left:30px;">Available Access Groups : </span>
                                                <div class="col-md-12" style="margin-left:30px;">
                                                    <div runat="server">
                                                                <asp:Repeater ID="RptAG" runat="server">
                                                                    <ItemTemplate>
                                                                        <div class="col-md-4">
                                                                            <asp:CheckBox ID="chkAG" CssClass="checkbox" runat="server" Text='<%# Eval("Access_Group_Name")  %>' />
                                                                            <asp:Label ID="lblId1" runat="server" Text='<%# Eval("Id") %>' Visible="false"></asp:Label>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                </asp:Repeater>
                                                        
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                        <br />
                                                                 <asp:Label ID="lblAG" runat="server" ForeColor="Red" Text="Please select atleast one access group" Visible="false"></asp:Label>
                                        <div class="clearfix"></div>
                                        <hr />
                                        <br />
                                        <asp:LinkButton ID="lnkStaffupdate" class="btn green-meadow" runat="server" Text="Update" OnClick="lnkStaffupdate_Click"  ></asp:LinkButton>
                                        &nbsp;&nbsp;&nbsp;
                            <asp:LinkButton ID="lnkRemoveAll" class="btn green-meadow" runat="server" Text="RemoveAllAG" OnClick="lnkRemoveAll_Click" ></asp:LinkButton>
                                        <hr />
                                        <br />
                                        <asp:Label ID="lblValchk" runat="server" Text="* Please select atleast one staff list" Visible="false" ForeColor="Red" Font-Size="Large"></asp:Label>
                                    </div>
                                    <asp:Label ID="lblmsg" runat="server" Visible="false" Text="Insert Successfully!" Style="color: green"></asp:Label>
                                    <asp:Label ID="lblmsg1" runat="server" Visible="false" Text="Remove Successfully!" Style="color: red"></asp:Label>
                                    <asp:GridView ID="gridEmployee" runat="server" class="table table-striped table-bordered table-hover" AutoGenerateColumns="false" AllowPaging="true" AllowSorting="true" OnSorting="OnSorting" OnPageIndexChanging="OnPageIndexChanging" PageSize="10" ShowHeaderWhenEmpty="true" EmptyDataText="No records Found">
                                        <PagerSettings Mode="NumericFirstLast" PageButtonCount="5" FirstPageText="First" LastPageText="Last" />
                                        <PagerStyle CssClass="gridview"></PagerStyle>
                                        <Columns>
                                            <asp:TemplateField ItemStyle-Width="1%">
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkALLStaff" runat="server" OnCheckedChanged="chkALLStaff_CheckedChanged" AutoPostBack="true" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkStaff" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="5%" HeaderText="Id" SortExpression="Id">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblId1" runat="server" Text='<%#Eval("Id") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="10%" HeaderText="Staff Id" SortExpression="Staff_Id">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblApplication_No" runat="server" Text='<%#Eval("Staff_Id") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            
                                            <asp:TemplateField ItemStyle-Width="28%" HeaderText="Staff Name" SortExpression="Staff_Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStaff_Id" runat="server" Text='<%#Eval("Staff_Name") %>'></asp:Label>
                                                    <asp:Label ID="lblid" runat="server" Text='<%#Eval("Id") %>' Visible="false"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="10%" HeaderText="Department Name" SortExpression="DeptName">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDeptName" runat="server" Text='<%#Eval("DeptName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="37%" HeaderText="Access Group Name" SortExpression="Access_Group_ID">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblReader_id" runat="server" Text='<%#Eval("Access_Group_ID") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="5%" HeaderText="Edit" HeaderStyle-ForeColor="#337ab7">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="linkEdit"  runat="server"  CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id")%>' OnClick="linkEdit_Click"><i class="material-icons" title="Edit row">&#xe3c9;</i></asp:LinkButton>
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
                                        <label class="text-success">Staff  Id : </label>
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
                                        <label class="text-success">Staff Name : </label>
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


                                    <div class="form-group">
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
                                                                 <asp:Label ID="lblerrorAG" runat="server" ForeColor="Red" Text="Please select atleast one access group" Visible="false"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group" style="display: none">
                                        <label class="text-success">Available Session : </label>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="description" style="width: 100%; border: 1px solid #ddd; padding: 10px; border-radius: 5px">
                                                    <div class="row">
                                                        <div class="col-md-12" id="Div4">
                                                            <div class="input-group">
                                                                <asp:Repeater ID="rptSession" runat="server">
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkSession" runat="server" Text='<%# Eval("Session_Name")  %>' />&nbsp;
                                                                                        <asp:Label ID="lblSession_Id" runat="server" Text='<%# Eval("Session_Id") %>' Visible="false"></asp:Label>
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
                        <asp:Label ID="lblS2" Text="Do you want to push this to S2 system ?" runat="server" Visible="false"></asp:Label> </b></h5>
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
        </ContentTemplate>
        <Triggers>
            <%--<asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />--%>
        </Triggers>
    </asp:UpdatePanel>

</asp:Content>
