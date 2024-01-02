<%@ Page Title="" Language="C#" MasterPageFile="~/Admin_Master.Master" AutoEventWireup="true" CodeBehind="SMS_Change_password.aspx.cs" Inherits="SMS.SMS_Change_password" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Change password</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdateRefresh" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
    <br />
    <br />
    <br />
    <div class="page-content-wrapper" style="margin-bottom: -16px;">
        <div class="page-content">
            <h3 class="page-title bold">Change Password
              
            </h3>
            <hr />
            <div class="modal-content" id="divView" runat="server">
                <div class="modal-body">
                    <div class="portlet-body form">
                        <div class="form-body">
                            <div class="form-group">
                                <label class="text-success">Enter Current password : </label>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="description" style="width: 100%; border: 1px solid #ddd; padding: 10px; border-radius: 5px">
                                            <div class="row">
                                                <div class="col-md-12" id="Div1">
                                                    <div class="input-group">
                                                        <asp:TextBox ID="txtpassword" runat="server"  OnTextChanged="txtpassword_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                        <asp:Label ID="lblerrorpassword" runat="server" ForeColor="Red"  Text="* Invalid password ! please currect password" Visible="false"></asp:Label>
                                                        <asp:Label ID="lblcurrentpwd" runat="server" ForeColor="Red" Text="* Enter password" Visible="false"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <hr />

                            <div class="form-group">
                                <label class="text-success">Enter New password : </label>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="description" style="width: 100%; border: 1px solid #ddd; padding: 10px; border-radius: 5px">
                                            <div class="row">
                                                <div class="col-md-12" id="Div2">
                                                    <div class="input-group">
                                                        <asp:TextBox ID="txtNewpwd"  runat="server" OnTextChanged="txtNewpwd_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                        <asp:Label ID="lblnewpwd" runat="server" ForeColor="Red" Text="* Enter new password" Visible="false"></asp:Label>
                                                        <asp:Label ID="lblerrorsamepwd" runat="server" ForeColor="Red" Text="* Current password and new password can not be same" Visible="false"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <hr />
                            <div class="form-group">
                                <label class="text-success">Enter Confirm New password : </label>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="description" style="width: 100%; border: 1px solid #ddd; padding: 10px; border-radius: 5px">
                                            <div class="row">
                                                <div class="col-md-12" id="Div3">
                                                    <div class="input-group">
                                                        <asp:TextBox ID="txtconfirmpwd" runat="server" OnTextChanged="txtconfirmpwd_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                        <asp:Label ID="lblerrorconfirmpwd" runat="server" ForeColor="Red" Text="* New password and confirm new password do not match ! Please currect password" Visible="false"></asp:Label>
                                                        <asp:Label ID="lblconfirmpwderror" runat="server" ForeColor="Red" Text="* Enter confirm new password" Visible="false"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <br />

                            <asp:LinkButton ID="lnksave" runat="server" Text="Update" OnClick="lnksave_Click" class="btn green-meadow" />
                            
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
            </ContentTemplate>
        <Triggers>
            <%--<asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />--%>
        </Triggers>
         </asp:UpdatePanel>
</asp:Content>
