<%@ Page Title="" Language="C#" MasterPageFile="~/Admin_Master.Master" AutoEventWireup="true" CodeBehind="SMS_Email_Configuration.aspx.cs" Inherits="SMS.SMS_Email_Configuration" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Email Configuration</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .modalBackground {
            background-color: gray;
            opacity: 0.7;
        }
    </style>

    <br /><br />
    <div class="page-content-wrapper">
        <div class="page-content">
            <h3 class="page-title bold">Email Configuration</h3>
            <div class="modal-content" id="divView" runat="server">
                <div class="modal-body">
                    <div class="portlet-body form">
                        <div class="form-body">
                            <div class="col-md-6 col-sm-6">
                                <div class="portlet-body">
                                    <div class="form-body">
                                        <div class="form-group">
                                            <label class="col-md-3 control-label">
                                                Your Name :
                                            </label>
                                            <div class="col-md-6">
                                                <asp:TextBox ID="txtName" runat="server" CssClass="form-control" placeholder="Your Name" BackColor="#d3d3d3"></asp:TextBox>
                                                <asp:Label ID="lblvalidname" runat="server" Style="color: red;" Text="* Enter your name..." Visible="false"></asp:Label>
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
                                                Email:
                                            </label>
                                            <div class="col-md-6">
                                                <asp:TextBox ID="txtYourEmail" runat="server" TextMode="Email" placeholder="Email Address" BackColor="#d3d3d3" CssClass="form-control"></asp:TextBox>
                                                <span id="error" style="display: none; color: red;">Wrong email</span>
                                                <asp:Label ID="lblEmailId" runat="server" Style="color: red;" Text="* Enter Email Address..." Visible="false"></asp:Label>
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
                                                Connection :
                                            </label>

                                            <div class="col-md-6">
                                                <asp:DropDownList ID="ddlEncrypted" runat="server" CssClass="form-control">
                                                    <asp:ListItem Value="0">None</asp:ListItem>
                                                    <asp:ListItem Value="1">TLS</asp:ListItem>
                                                    <asp:ListItem Value="2">SSL</asp:ListItem>
                                                    <asp:ListItem Value="3">Auto</asp:ListItem>
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
                                                Password  :
                                            </label>

                                            <div class="col-md-6">
                                                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" placeholder="Password" CssClass="form-control" BackColor="#d3d3d3"></asp:TextBox>
                                                <asp:Label ID="lblvalidpassword" runat="server" Style="color: red;" Text="* Enter password..." Visible="false"></asp:Label>
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
                                                SMTP Server :
                                            </label>
                                            <div class="col-md-6">
                                                <asp:TextBox ID="txtOutgoingServerName" runat="server" BackColor="#d3d3d3" placeholder="Outgoing ServerName (SMTP)" CssClass="form-control"></asp:TextBox>
                                                <asp:Label ID="lblValidOutgoingServerName" runat="server" Style="color: red;" Text="* Enter Outgoing ServerName..." Visible="false"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6 col-sm-6" style="display:none;">
                                <div class="portlet-body">
                                    <div class="form-body">
                                        <div class="form-group">
                                            <label class="col-md-3 control-label">
                                                User Name :
                                            </label>
                                            <div class="col-md-6">
                                                <asp:TextBox ID="txtUserName" runat="server" placeholder="User Name" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            
                            <div class="col-md-6 col-sm-6" style="display:none;">
                                <div class="portlet-body">
                                    <div class="form-body">
                                        <div class="form-group">
                                            <label class="col-md-3 control-label">
                                                POP3 Server :
                                            </label>
                                            <div class="col-md-6">
                                                <asp:TextBox ID="txtincomingServer" runat="server" CssClass="form-control" placeholder="POP3 Server"></asp:TextBox>
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
                                                SMTP Port :
                                            </label>
                                            <div class="col-md-6">
                                                <asp:TextBox ID="txtSMTP" runat="server" BackColor="#d3d3d3" placeholder="SMTP Port" CssClass="form-control"></asp:TextBox>
                                                <asp:Label ID="lblValidSMTP" runat="server" Style="color: red;" Text="* Enter SMTP Port..." Visible="false"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6 col-sm-6" style="display:none">
                                <div class="portlet-body">
                                    <div class="form-body">
                                        <div class="form-group">
                                            <label class="col-md-3 control-label">
                                                Pop3 Port :
                                            </label>
                                            <div class="col-md-6">
                                                <asp:TextBox ID="txtpop3" runat="server" CssClass="form-control" placeholder="Pop3 Port"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            
                            
                            <div class="col-md-6 col-sm-6" style="display:none;">
                                <div class="portlet-body">
                                    <div class="form-body">
                                        <div class="form-group">
                                            <label class="col-md-3 control-label">
                                                Email Sender :
                                            </label>
                                            <div class="col-md-6">
                                                <asp:DropDownList ID="ddlEmailSender" runat="server" CssClass="form-control">
                                                    <asp:ListItem Value="1" Selected="True">Scampus</asp:ListItem>
                                                    <asp:ListItem Value="2">Smart Campus</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6 col-sm-6" style="display:none">
                                <div class="portlet-body">
                                    <div class="form-body">
                                        <div class="form-group">
                                            <label class="col-md-3 control-label">
                                                IMAP Server :
                                            </label>
                                            <div class="col-md-6">
                                                <asp:TextBox ID="txtImapServerName" runat="server" CssClass="form-control" placeholder="IMAP Server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6 col-sm-6" style="display:none;">
                                <div class="portlet-body">
                                    <div class="form-body">
                                        <div class="form-group">
                                            <label class="col-md-3 control-label">
                                                Imap Authentication :
                                            </label>

                                            <div class="col-md-6">
                                                <asp:TextBox ID="txtimapAuthentication" runat="server" CssClass="form-control" placeholder="Imap Authentication"></asp:TextBox>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="clearfix"></div>
                            <div class="col-md-6 col-sm-6" style="display:none">
                                <div class="portlet-body">
                                    <div class="form-body">
                                        <div class="form-group">
                                            <label class="col-md-3 control-label">IMAP Port : </label>
                                            <div class="col-md-6">
                                                <asp:TextBox ID="txtImapPort" runat="server" CssClass="form-control" placeholder="IMAP Port"></asp:TextBox>
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
                                        <div class="form-group"><div class="col-md-6">
                            <asp:LinkButton ID="lnkSubmit" runat="server" Text="Submit" class="btn green" OnClick="lnkSubmit_Click" />
                            <asp:Label ID="lblStatus" runat="server"></asp:Label>
                            <asp:Label ID="lblstatusNone" Visible="false" runat="server"></asp:Label>
                                </div></div></div></div></div>
                            <div class="clearfix"></div>
                            <br />
                            <br />
                            <div class="col-md-6 col-sm-6">
                                <div class="portlet-body">
                                    <div class="form-body">
                                        <div class="form-group">
                                            <label class="col-md-3 control-label">Test Email: </label>
                                            <div class="col-md-6">
                                                <asp:TextBox ID="txttestEmail" runat="server" TextMode="Email" CssClass="form-control" BackColor="#d3d3d3" placeholder="Test Email"></asp:TextBox>
                                                 
                                            </div>
                                            <div class="col-md-6" style="margin-left:410px;margin-top:-34px;">
                                                <asp:LinkButton ID="lnktest" runat="server" Text="Test" class="btn green" OnClick="lnktest_Click" />
                                            </div>
                                            <br /><asp:Label ID="lblvalidtestemail" runat="server" Style="color: red;" Text="* Enter Email..." Visible="false"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <br />
                            <br />
                            <br />
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>
    <div runat="server" id="divpopup" class="modal">
        <div class="modal-content" runat="server" style="top: 100px; left: 320px; width: 700px; height: 200px;">
                        <div class="modal-body">
                            <div class="portlet-body form">
                                <div class="form-body" runat="server">
                                    <div class="form-group" style="text-align: center;"  runat="server" id="divAdvanceSearch">
                                         <h5><b style="color:green"><asp:Label ID="lblmsg" runat="server"></asp:Label></b></h5>
                                    </div>
                                    <br />
                                    <div style="text-align: center;">
                                       <asp:LinkButton ID="lnkGo" runat="server" Text="ok" OnClick="lnkGo_Click" class="btn green-meadow" />
                                        </div>
                                </div>
                            </div>
                        </div>
                    </div>
        </div>
        <%--<asp:LinkButton ID="LinkButton4" ForeColor="#ffffff" runat="server"></asp:LinkButton>
        <cc1:ModalPopupExtender ID="ModalPopupExtender2" BehaviorID="mpe" runat="server"
            PopupControlID="Panel1" TargetControlID="LinkButton4" BackgroundCssClass="modalBackground">
        </cc1:ModalPopupExtender>
        <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup" Style="display: none; left: 84.5px!important; background: #F7F7F7 url('../Images/body-bg.png') repeat scroll 0% 0%;">
            <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Always" runat="server" ChildrenAsTriggers="true">
                <ContentTemplate>--%>
                    <%--<div class="modal-content" id="divpopup" runat="server" style="background-color: #fff" visible="false" >
                        <div class="modal-body" style="width: 500px!important; position: relative;">
                            <div class="portlet-body form">
                                <div class="form-body">
                                    <div runat="server" id="divAdvanceSearch">
                                        <div class="row">
                                            <h5><b style="color:green"><asp:Label ID="lblmsg" runat="server"></asp:Label></b></h5>
                                        </div>
                                       
                                        <br />

                                        <div class="row">
                                            <div class='col-sm-8  pull-left'></div>
                                            <div class='col-sm-4  pull-right'>
                                                <asp:LinkButton ID="lnkGo" runat="server" Text="ok" OnClick="lnkGo_Click" class="btn green-meadow" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>--%>
                <%--</ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="lnkGo" />

                </Triggers>
            </asp:UpdatePanel>
        </asp:Panel>--%>
</asp:Content>
