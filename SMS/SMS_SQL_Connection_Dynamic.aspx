<%@ Page Title="" Language="C#" MasterPageFile="~/Admin_Master.Master" AutoEventWireup="true" CodeBehind="SMS_SQL_Connection_Dynamic.aspx.cs" Inherits="SMS.SMS_SQL_Connection_Dynamic" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Database Configuration</title>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdateRefresh" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
    <br />
    <br />
    <br />
    <div class="page-content-wrapper" style="margin-bottom: -16px;">
        <div class="page-content">
            <div class="modal-content" id="divView" runat="server">
                <div class="modal-body" style="background-color:lightgray;">
                    <div class="portlet-body form" >
                        <div class="tab-content" >
                            <div class="tab-pane container active" id="divPersonalDetails" runat="server">
                                <div class="row ">
                                    <h2>Database Configuration</h2>
                                    <hr />
                                    
                                    <div class="col-md-12 col-sm-12">
                                        <div class="portlet-body">
                                            <div class="form-body">
                                                <div class="form-group">
                                                    <label class="col-md-2 control-label">
                                                        SQL Server Name :
                                                    </label>

                                                    <div class="col-md-6">

                                                        <asp:TextBox ID="txtServerName" runat="server" class="form-control placeholder-no-fix" placeholder="Server Name"></asp:TextBox>
                                                      
                                                        <asp:DropDownList ID="ddlServerName" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlServerName_SelectedIndexChanged" AutoPostBack="true" Visible="false"></asp:DropDownList>
                                                        <asp:Label ID="lblerrorServername" runat="server" ForeColor="Red" Text="* Enter Server Name " Visible="false"></asp:Label>
                                                    </div>
                                                    <div class="col-md-3">
                                                          <asp:LinkButton ID="lnkrefresh" runat="server"   OnClick="lnkrefresh_Click" CssClass="fa fa-refresh" Style="font-size:larger;line-height:30px;"></asp:LinkButton>
                                                        <asp:Label ID="lblServerNameprocess" runat="server" ForeColor="Red" Text="* please wait..... Server Name In process " Visible="false"></asp:Label>
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
                                                    <label class="col-md-2 control-label">Authentication : </label>
                                                    <div class="col-md-6">
                                                        <asp:DropDownList ID="ddlAuthentication" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlAuthentication_SelectedIndexChanged">
                                                            <asp:ListItem Value="1">Windows Authentication</asp:ListItem>
                                                            <asp:ListItem Value="2">SQL Server Authentication</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="clearfix"></div>
                                    <div class="col-md-12 col-sm-12" id="divUser" runat="server" visible="false">
                                        <div class="portlet-body">
                                            <div class="form-body">
                                                <div class="form-group">
                                                    <label class="col-md-2 control-label">
                                                        User Name :
                                                    </label>

                                                    <div class="col-md-6">
                                                        <asp:TextBox ID="txtusername" runat="server" class="form-control placeholder-no-fix" placeholder="User name"></asp:TextBox>
                                                        <asp:Label ID="lblerrorusername" runat="server" ForeColor="Red" Text="* Enter username " Visible="false"></asp:Label>
                                                    </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-12 col-sm-12" id="divPassword" runat="server" visible="false">
                                <div class="portlet-body">
                                    <div class="form-body">
                                        <div class="form-group">
                                            <label class="col-md-2 control-label">
                                                PassWord :
                                            </label>

                                            <div class="col-md-6">
                                                <asp:TextBox ID="txtPassword" runat="server" class="form-control placeholder-no-fix" placeholder="Password" TextMode="Password"></asp:TextBox>
                                                <asp:Label ID="lblerrorpassword" runat="server" ForeColor="Red" Text="* Enter password " Visible="false"></asp:Label>
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
                                                    <label class="col-md-2 control-label">DataBase Name : </label>
                                                    <div class="col-md-6">
                                                        <asp:TextBox ID="txtDBNAme" runat="server" class="form-control placeholder-no-fix" placeholder="DB Name"></asp:TextBox>
                                                        <asp:DropDownList ID="ddlDBName" runat="server" CssClass="form-control" Visible="false"></asp:DropDownList>

                                                        <asp:Label ID="lblerrordbname" runat="server" ForeColor="Red" Text="* Enter DB Name " Visible="false"></asp:Label>
                                                    </div>
                                                    <div class="col-md-3">
                                                          <asp:LinkButton ID="lnkDBname" runat="server"   OnClick="lnkDBname_Click" CssClass="fa fa-refresh" Style="font-size:larger;line-height:30px;"></asp:LinkButton>
                                                        <asp:Label ID="lblDBProcess" runat="server" ForeColor="Red" Text="* please wait..... Database Name In process " Visible="false"></asp:Label>
                                                        </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                        </div>
                    </div>
                </div>
                <br />
                        <asp:Label ID="lblsql" runat="server" ForeColor="Red" Visible="false" Style="margin-left:240px;" ></asp:Label>
                                    <asp:Label ID="lbltestconnection" runat="server" ForeColor="Red" Visible="false" Text="Connection Successfully!!!" Style="margin-left:240px;"></asp:Label>
                <br />
                        <asp:Button ID="BtntestConnection" runat="server" class="btn default" Text="Test Connection" OnClick="BtntestConnection_Click" Style="margin-left:240px;" />
                <asp:Button ID="btnSubmit" runat="server" class="btn default" Text="Save" OnClick="btnSave_Click"  />
                         
            </div>
        </div>
    </div>
    </div>
    </div>
            </ContentTemplate>
        <Triggers>
          <%--  <asp:PostBackTrigger ControlID="lnksave" />
            <asp:PostBackTrigger ControlID="lnkupdate" />--%>
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
