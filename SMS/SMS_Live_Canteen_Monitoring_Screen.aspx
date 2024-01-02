<%@ Page Title="" Language="C#" MasterPageFile="~/Admin_Master.Master" AutoEventWireup="true" CodeBehind="SMS_Live_Canteen_Monitoring_Screen.aspx.cs" Inherits="SMS.SMS_Live_Canteen_Monitoring_Screen" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Live Canteen Monitoring</title>
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Timer ID="Timer1" OnTick="Timer1_Tick" runat="server" Interval="1000000"></asp:Timer>
    <asp:UpdatePanel ID="UpdateRefresh" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <br />
            <br />
            <div class="page-content-wrapper" style="margin-bottom: -16px;">
                <div class="page-content">
                    <h3 class="page-title bold">Live SAG Monitoring Screen
                    </h3>
                    
                    <div class="row" id="divgrid" runat="server">
                        <div class="col-md-12">
                            <div class="portlet box green-meadow">
                                <div class="portlet-title">
                                    <div class="caption">
                                        <i class="fa fa-briefcase"></i>Live SAG Monitoring Screen
                                    </div>
                                </div>

                                <div class="portlet-body">
                                    <div runat="server" id="divAdvanceSearch">

                                        <br />
                                        <div class="container">
                                            <div class="row">
                                                <div class='col-sm-3'>
                                                    <div class="form-group">

                                                        <asp:DropDownList ID="ddlCanteen" runat="server" OnTextChanged="ddlCanteen_TextChanged" AutoPostBack="true" CssClass="form-control"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class='col-sm-3'>
                                                    <div class="form-group">
                                                        <div class='input-group date' id='datetimepicker1'>
                                                            <asp:TextBox ID="txtDate" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <span class="input-group-addon">
                                                                <span class="glyphicon glyphicon-calendar"></span>
                                                            </span>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class='col-sm-3'>
                                                    <div class="form-group">
                                                        <div class='input-group date' id='datetimepicker2'>
                                                            <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <span class="input-group-addon">
                                                                <span class="glyphicon glyphicon-calendar"></span>
                                                            </span>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class='col-sm-3'>
                                                    <asp:LinkButton ID="lnkGo" runat="server" Text="Go" OnClick="lnkGo_Click" class="btn green-meadow" />
                                                </div>
                                            </div>
                                        </div>

                                    </div>

                                    <br />
                                    <br />

                                    <asp:GridView ID="gridEmployee" runat="server" class="table table-striped table-bordered table-hover" 
                                        AutoGenerateColumns="false" AllowPaging="true" AllowSorting="true" OnPageIndexChanging="OnPageIndexChanging" PageSize="5" 
                                        ShowHeader="false" Width="100%" ShowHeaderWhenEmpty="true" EmptyDataText="No records Found" >
                                        <PagerSettings Mode="NumericFirstLast" PageButtonCount="5" FirstPageText="First" LastPageText="Last" />
                                        <PagerStyle CssClass="gridview"></PagerStyle>
                                        <Columns>
                                            <asp:TemplateField ItemStyle-Width="30%" ItemStyle-Height="30px" HeaderText="Head Count">
                                                <ItemTemplate>
                                                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/Canteen.jpg" Height="100px" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="50%" ItemStyle-Height="30%" HeaderText="Head Count">
                                                <ItemTemplate>
                                                    <%--<a  style="width: 140px !important; height: 135px !important; margin-bottom: 10px !important; vertical-align: middle !important; text-align: center !important; text-wrap: normal !important; padding: 15px 0 0 0 !important;background-color: #4cff00;">--%>
                                                    <label style="color: #00ff90">Canteen Name :</label>
                                                    <asp:LinkButton ID="lblId" runat="server" Style="color: #00ff90" Text='<%#Eval("AGNAME") %>' OnClick="lnkCanteenName_Click"></asp:LinkButton>
                                                    <asp:LinkButton ID="lnkCanteenName" runat="server" Text='<%#Eval("Id") %>' Visible="false"></asp:LinkButton>
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            
                                           <label style="color: #0026ff">Allowed Members:</label>
                                                    <asp:LinkButton ID="lnkAllowedmem" runat="server" Style="color: #0026ff" Text='<%#Eval("AllowedCount") %>' OnClick="lnkAllowedmem_Click"></asp:LinkButton>
                                                    

                                                    <label style="color: #4cff00">Access Members:</label>
                                                    <asp:LinkButton ID="lnkAccessMem" runat="server" Style="color: #4cff00" Text='<%#Eval("AccessMembers") %>' OnClick="lnkAccessMem_Click"></asp:LinkButton>
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

                                            <label style="color: #808080">Pending Members:</label>
                                                    <asp:LinkButton ID="lnkPenMem" runat="server" Style="color: #808080" Text='<%#Eval("PendingMembers") %>' OnClick="lnkPenMem_Click"></asp:LinkButton>
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

                                            <label style="color: #f00">denied Members:</label>
                                                    <asp:LinkButton ID="lnkDeniedMem" runat="server" Style="color: #f00" Text='<%#Eval("deniedMembers") %>' OnClick="lnkDeniedMem_Click"></asp:LinkButton>

                                                    <%--</a>--%>
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

        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
            <%--<asp:PostBackTrigger ControlID="lnkCanteenName" />--%>
        </Triggers>
    </asp:UpdatePanel>

</asp:Content>
