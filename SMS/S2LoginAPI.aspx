<%@ Page Title="" Language="C#" MasterPageFile="~/Admin_Master.Master" AutoEventWireup="true" CodeBehind="S2LoginAPI.aspx.cs" Inherits="SMS.S2LoginAPI" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>S2 Login API</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-content-wrapper" style="margin-bottom: -16px;">
        <div class="page-content">

            <div class="row" id="divgrid" runat="server">
                <div class="col-md-12">
                </div>
            </div>
        </div>
    </div>
    <div>
        <asp:LinkButton ID="LinkButton1" ForeColor="#ffffff" runat="server"></asp:LinkButton>
        <cc1:ModalPopupExtender ID="ModalPopupExtender2" BehaviorID="mpe" runat="server"
            PopupControlID="Panel1" TargetControlID="LinkButton1" BackgroundCssClass="modalBackground" CancelControlID="Button1">
        </cc1:ModalPopupExtender>
        <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup" Style="display: none; left: 84.5px!important; background: #F7F7F7 url('../Images/body-bg.png') repeat scroll 0% 0%;">
            <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Always" runat="server" ChildrenAsTriggers="true">
                <ContentTemplate>
                    <div class="modal-content" id="div2" runat="server" style="background-color: #fff">
                        <div class="modal-body" style="width: 500px!important; position: relative; background-color: #2b3643;">
                            <div class="portlet-body form">
                                <div class="form-body">
                                    <div runat="server" id="divAdvanceSearch">
                                        <h2>S2 API credentials</h2>
                                        <div class="row">
                                            <div class='col-sm-12 pull-left'>
                                                <div class="form-group">
                                                    <span style="color: white;">IP Address : </span>
                                                    <div class="input-icon">
                                                        <i class="fa fa-address-card"></i>
                                                        <asp:TextBox ID="txtipaddress" runat="server" placeHolder="Enter IP Address Like : 125.123.234.56" autocomplete="off" CssClass="form-control placeholder-no-fix"></asp:TextBox>
                                                    </div>
                                                    <asp:Label ID="lblipaddressvdn" runat="server" Style="color: red;" Text="* Select canteen..." Visible="false"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class='col-sm-12'>
                                                <div class="form-group">
                                                    <span style="color: white;">User Name : </span>
                                                    <div class="input-icon">
                                                        <i class="fa fa-user"></i>
                                                        <asp:TextBox ID="txtusername" runat="server" placeHolder="Enter User Name" autocomplete="off" CssClass="form-control placeholder-no-fix"></asp:TextBox>
                                                    </div>
                                                    <asp:Label ID="lblusernamevdn" runat="server" Style="color: red;" Text="* Select session..." Visible="false"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class='col-sm-12'>
                                                <div class="form-group">
                                                    <span style="color: white;">Password : </span>
                                                    <div class="input-icon">
                                                        <i class="fa fa-lock"></i>
                                                        <asp:TextBox ID="txtpassword" runat="server" placeHolder="Enter Password" autocomplete="off" CssClass="form-control placeholder-no-fix"></asp:TextBox>
                                                    </div>
                                                    <asp:Label ID="lblpasswordvdn" runat="server" Style="color: red;" Text="* Select password" Visible="false"></asp:Label>
                                                </div>
                                            </div>
                                        </div>

                                        <br />

                                        <div class="row">
                                            <div class='col-sm-7  pull-left'></div>
                                            <div class='col-sm-5  pull-right' style="margin-right: -39px;">
                                                <asp:LinkButton ID="lnklogin" runat="server" Text="Login" class="btn green-meadow" OnClick="lnklogin_Click" />
                                                <asp:Button ID="Button1" CssClass="btn btn-primary" runat="server" Text="Cancel" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <script type="text/javascript">  
                        $(document).ready(function () {
                            /**
                 * Clearable text inputs
                 */
                            function tog(v) { return v ? 'addClass' : 'removeClass'; }
                            $(document).on('input', '.clearable', function () {
                                $(this)[tog(this.value)]('x');
                            }).on('mousemove', '.x', function (e) {
                                $(this)[tog(this.offsetWidth - 18 < e.clientX - this.getBoundingClientRect().left)]('onX');
                            }).on('touchstart click', '.onX', function (ev) {
                                ev.preventDefault();
                                $(this).removeClass('x onX').val('').change();
                                $("#ContentPlaceHolder1_iconcheck").addClass("hide");
                            });

                            // $('.clearable').trigger("input");
                            // Uncomment the line above if you pre-fill values from LS or server
                            SearchText();
                            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
                        });
                        function EndRequestHandler(sender, args) {
                            $('#datetimepicker1').datetimepicker({ format: 'DD/MM/YYYY' });
                            $('#datetimepicker2').datetimepicker({ format: 'DD/MM/YYYY' });
                            SearchText();
                        }
                        function SearchText() {
                            $("#ContentPlaceHolder1_txtStudent").autocomplete({
                                source: function (request, response) {
                                    $.ajax({
                                        type: "POST",
                                        contentType: "application/json; charset=utf-8",
                                        url: "Canteen_Report.aspx/GetStudentInfo",
                                        data: "{'StudentName':'" + document.getElementById('ContentPlaceHolder1_txtStudent').value + "'}",
                                        dataType: "json",
                                        success: function (data) {

                                            //document.getElementById("iconcheck").style.display = "block";

                                            response(data.d);
                                            //$("iconcheck").toggle();
                                        },
                                        error: function (result) {

                                        }
                                    });
                                }
                            });
                            $("#ContentPlaceHolder1_txtStudent").bind("autocompleteselect", function (event, ui) {

                                $("#ContentPlaceHolder1_iconcheck").removeClass("hide");
                            });
                            $("#ContentPlaceHolder1_txtStudent").keypress(function () {

                                $("#ContentPlaceHolder1_iconcheck").addClass("hide");
                            });
                            $("#ContentPlaceHolder1_txtStudent").keydown(function () {
                                $("#ContentPlaceHolder1_iconcheck").addClass("hide");
                            });
                        }
                    </script>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="lnkGo" />

                </Triggers>
            </asp:UpdatePanel>
        </asp:Panel>
    </div>
</asp:Content>
