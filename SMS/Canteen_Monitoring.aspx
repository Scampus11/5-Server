<%@ Page Title="" Language="C#" MasterPageFile="~/Admin_Master.Master" AutoEventWireup="true" CodeBehind="Canteen_Monitoring.aspx.cs" Inherits="SMS.Canteen_Monitoring" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <br />
    <br />
    <div class="page-content-wrapper">
        <div class="page-content">
            <h3 class="page-title bold">Live SAG Monitoring Records
                <small>Live SAG Monitoring Records</small>
            </h3>
            <hr />
            <div class="row" id="divgrid" runat="server">
                <div class="col-md-12">
                    <div class="portlet box green-meadow">
                        <div class="portlet-title">
                            <div class="caption">
                                <i class="fa fa-briefcase"></i>Live SAG Monitoring Records
                            </div>
                            <div class="tools">
                                
                            </div>
                        </div>

                        <div class="portlet-body" >
                            <asp:GridView ID="gridEmployee" runat="server" class="table table-striped table-bordered table-hover" AutoGenerateColumns="false" AllowPaging="true" AllowSorting="true"  OnPageIndexChanging="OnPageIndexChanging" PageSize="5"   Width="100%" >
                                <PagerSettings Mode="NumericFirstLast" PageButtonCount="5" FirstPageText="First" LastPageText="Last" />
                                <PagerStyle CssClass="gridview"></PagerStyle>
                                <Columns>
                                    <asp:TemplateField ItemStyle-Width="10%" ItemStyle-Height="30px" HeaderText="Image"  >
                                        <ItemTemplate>
                                           <asp:Image ID="tmgstudent" runat="server" ImageUrl='<%#Eval("StudentImages") %>' class="circle" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField ItemStyle-Width="50%" ItemStyle-Height="30px" HeaderText="StudentId"  >
                                        <ItemTemplate>
                                           <asp:Label ID="lblstudentId" runat="server" Text='<%#Eval("StudentId") %>'></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                   <asp:TemplateField ItemStyle-Width="10%" ItemStyle-Height="30px" HeaderText="Student Name"  >
                                        <ItemTemplate>
                                           <asp:Label ID="lblStudent_Name" runat="server" Text='<%#Eval("Student_Name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="10%" ItemStyle-Height="30px" HeaderText="Canteen Name"  >
                                        <ItemTemplate>
                                           <asp:Label ID="lblAGNAME" runat="server" Text='<%#Eval("AGNAME") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="10%" ItemStyle-Height="30px" HeaderText="Department"  >
                                        <ItemTemplate>
                                           <asp:Label ID="lblDepartment" runat="server" Text='<%#Eval("Department") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="10%" ItemStyle-Height="30px" HeaderText="Card Number"  >
                                        <ItemTemplate>
                                           <asp:Label ID="lblCardid" runat="server" Text='<%#Eval("Cardid") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="10%" ItemStyle-Height="30px" HeaderText="MealNumber"  >
                                        <ItemTemplate>
                                           <asp:Label ID="lblMealNumber" runat="server" Text='<%#Eval("MealNumber") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>

                        </div>

                        <div class="portlet-body" Style="overflow:scroll">
                            <asp:GridView ID="GridMemaccess" runat="server" class="table table-striped table-bordered table-hover" AutoGenerateColumns="false" AllowPaging="true" AllowSorting="true"  OnPageIndexChanging="OnPageIndexChanging" PageSize="5"   Width="100%" Style="overflow:scroll">
                                <PagerSettings Mode="NumericFirstLast" PageButtonCount="5" FirstPageText="First" LastPageText="Last" />
                                <PagerStyle CssClass="gridview"></PagerStyle>
                                <Columns>
                                    <asp:TemplateField ItemStyle-Width="10%" ItemStyle-Height="30px" HeaderText="Image"  >
                                        <ItemTemplate>
                                           <asp:Image ID="tmgstudent" runat="server" ImageUrl='<%#Eval("StudentImages") %>' class="circle" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField ItemStyle-Width="50%" ItemStyle-Height="30px" HeaderText="StudentId"  >
                                        <ItemTemplate>
                                           <asp:Label ID="lblstudentId" runat="server" Text='<%#Eval("StudentId") %>'></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                   <asp:TemplateField ItemStyle-Width="10%" ItemStyle-Height="30px" HeaderText="Student Name"  >
                                        <ItemTemplate>
                                           <asp:Label ID="lblStudent_Name" runat="server" Text='<%#Eval("Student_Name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="10%" ItemStyle-Height="30px" HeaderText="Canteen Name"  >
                                        <ItemTemplate>
                                           <asp:Label ID="lblAGNAME" runat="server" Text='<%#Eval("AGNAME") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="10%" ItemStyle-Height="30px" HeaderText="Department"  >
                                        <ItemTemplate>
                                           <asp:Label ID="lblDepartment" runat="server" Text='<%#Eval("Department") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="10%" ItemStyle-Height="30px" HeaderText="Card Number"  >
                                        <ItemTemplate>
                                           <asp:Label ID="lblCardid" runat="server" Text='<%#Eval("Cardid") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="10%" ItemStyle-Height="30px" HeaderText="MealNumber"  >
                                        <ItemTemplate>
                                           <asp:Label ID="lblMealNumber" runat="server" Text='<%#Eval("MealNumber") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="10%" ItemStyle-Height="30px" HeaderText="Access Code"  >
                                        <ItemTemplate>
                                           <asp:Label ID="lblAccess_Code" runat="server" Text='<%#Eval("Access_Code") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="10%" ItemStyle-Height="30px" HeaderText="Grant Access"  >
                                        <ItemTemplate>
                                           <asp:Label ID="lblGrant_Access" runat="server" Text='<%#Eval("Grant_Access") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="10%" ItemStyle-Height="30px" HeaderText="Reader Id"  >
                                        <ItemTemplate>
                                           <asp:Label ID="lblSLN_Reader_Id" runat="server" Text='<%#Eval("SLN_Reader_Id") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="10%" ItemStyle-Height="30px" HeaderText="Reader Name"  >
                                        <ItemTemplate>
                                           <asp:Label ID="lblReader_Name" runat="server" Text='<%#Eval("Reader_Name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="10%" ItemStyle-Height="30px" HeaderText="Session ID"  >
                                        <ItemTemplate>
                                           <asp:Label ID="lblSession_ID" runat="server" Text='<%#Eval("Session_ID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="10%" ItemStyle-Height="30px" HeaderText="Session Name"  >
                                        <ItemTemplate>
                                           <asp:Label ID="lblSession_Name" runat="server" Text='<%#Eval("Session_Name") %>'></asp:Label>
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
</asp:Content>
