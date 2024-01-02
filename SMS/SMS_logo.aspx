<%@ Page Title="" Language="C#" MasterPageFile="~/Admin_Master.Master" AutoEventWireup="true" CodeBehind="SMS_logo.aspx.cs" Inherits="SMS.SMS_logo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Company Info</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script>
        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }
    </script>
    <br />
    <br />
    <div class="page-content-wrapper" style="margin-bottom: -16px;">
        <div class="page-content">
            <div class="row" id="divgrid" runat="server">
                <div class="col-md-12">
                    <div class="portlet box green-meadow">
                        <div class="portlet-title">
                            <div class="caption">
                                <i class='fas'>&#xf129;</i>Company Info
                            </div>
                        </div>
                        <div class="portlet-body" style="overflow: scroll">
                            <asp:GridView ID="gridEmployee" runat="server" class="table table-striped table-bordered table-hover" AutoGenerateColumns="false" AllowPaging="true" AllowSorting="true" OnSorting="OnSorting"
                                OnPageIndexChanging="OnPageIndexChanging" PageSize="5" ShowHeaderWhenEmpty="true" EmptyDataText="No records Found" OnRowDataBound="gridEmployee_RowDataBound">
                                <PagerSettings Mode="NumericFirstLast" PageButtonCount="5" FirstPageText="First" LastPageText="Last" />
                                <PagerStyle CssClass="gridview"></PagerStyle>
                                <Columns>
                                    <asp:TemplateField ItemStyle-Width="5%" HeaderText="Id" SortExpression="Id">
                                        <ItemTemplate>
                                            <asp:Label ID="lblId1" runat="server" Text='<%#Eval("Id") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="10%" HeaderText="Super Visor Timer Time" SortExpression="timer_time">
                                        <ItemTemplate>
                                            <asp:Label ID="lblApplication_No" runat="server" Text='<%#Eval("timer_time") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="10%" HeaderText="Public Timer Time" SortExpression="Public_timer">
                                        <ItemTemplate>
                                            <asp:Label ID="lblpublic" runat="server" Text='<%#Eval("Public_timer") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="40%" HeaderText="Mustering Time" SortExpression="MusteringTime">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMusteringTime" runat="server" Text='<%#Eval("MusteringTime") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="40%" HeaderText="Company Name" SortExpression="Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblName" runat="server" Text='<%#Eval("Name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="15%" HeaderText="Company Logo" SortExpression="Images">
                                        <ItemTemplate>
                                            <asp:Label ID="lblpath" Visible="false" runat="server" Text='<%#Eval("Images") %>'></asp:Label>
                                            <asp:Image ID="imgStudent" runat="server" Visible="false" ImageUrl='<%#Eval("Images") %>' class="circle" />
                                            <asp:Image ID="imgdefault" runat="server" Visible="false" ImageUrl="~/Images/images1.jpg" class="circle" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="15%" HeaderText="DigitalId Logo" SortExpression="DigitalIdImages">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDGIpath" Visible="false" runat="server" Text='<%#Eval("DigitalIdImages") %>'></asp:Label>
                                            <asp:Image ID="imgDGIpathMain" runat="server" Visible="false" ImageUrl='<%#Eval("DigitalIdImages") %>' class="circle" />
                                            <asp:Image ID="imgDGIdefault" runat="server" Visible="false" ImageUrl="~/Images/images1.jpg" class="circle" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="5%" HeaderText="Action">
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
                <div class="row-fluid">
                    <div class="span12">
                        <div class="widget">
                            <div class="widget-title">
                                <span class="tools">
                                    <a href="javascript:;" class="icon-chevron-down"></a>
                                </span>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-body">
                    <div class="portlet-body form">
                        <!-- Nav tabs -->
                        <ul class="nav nav-tabs">
                            <li class="nav-item active" id="liPersonalDetails" runat="server">
                                <a class="nav-link" data-toggle="tab" href="#ContentPlaceHolder1_divPersonalDetails" id="aPersonalDetails" runat="server">Company Info</a>
                            </li>
                        </ul>
                        <!-- Tab panes -->
                        <div class="tab-content">
                            <div class="tab-pane container active" id="divPersonalDetails" runat="server">
                                <div class="row">
                                    <div class="col-md-6 col-sm-6">
                                        <div class="portlet-body">
                                            <div class="form-body">
                                                <div class="form-group">
                                                    <label class="col-md-3 control-label">
                                                        Super Visor Timer Time :
                                                    </label>
                                                    <div class="col-md-6">
                                                        <asp:TextBox ID="txtApplication_No" runat="server" CssClass="form-control" Text="0" onkeypress="return isNumber(event)"></asp:TextBox>
                                                        <b>Note : Time is milliseconds</b>
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
                                                        Public Canteen Timer Time :
                                                    </label>
                                                    <div class="col-md-6">
                                                        <asp:TextBox ID="txtpublic" runat="server" CssClass="form-control" Text="0" onkeypress="return isNumber(event)"></asp:TextBox>
                                                        <b>Note : Time is milliseconds</b>
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
                                                        Mustering Timer Time :
                                                    </label>
                                                    <div class="col-md-6">
                                                        <asp:TextBox ID="txtMusteringTime" runat="server" CssClass="form-control" Text="0" onkeypress="return isNumber(event)"></asp:TextBox>
                                                        <b>Note : Time is milliseconds</b>
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
                                                        Company Name :
                                                    </label>
                                                    <div class="col-md-6">
                                                        <asp:TextBox ID="txtName" CssClass="form-control" runat="server"></asp:TextBox>
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
                                                        Upload logo :
                                                    </label>
                                                    <div class="col-md-6">
                                                        <asp:Image ID="imgStaff" runat="server" Height="60px" Width="180px" /><br />
                                                        <br />
                                                        <asp:FileUpload ID="FileUpload1" runat="server" />(Recommended Image Dimensions : 180px x 60px)
                                                        <asp:Label ID="lblimg" runat="server" Style="color: red" Text="* Please select image" Visible="false"></asp:Label><br />
                                                        <span style="color: darkviolet">Note : Only Image Suported Format is .jpg | .jpeg | .png | .bmp | .gif | .eps...</span><br />
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
                                                    <label class="col-md-3 control-label">
                                                        Digitalid logo :
                                                    </label>
                                                    <div class="col-md-6">
                                                        <asp:Image ID="imgDigitalId" runat="server" Height="60px" Width="180px" /><br />
                                                        <br />
                                                        <asp:FileUpload ID="FileDigitalIdUpload" runat="server" />
                                                        <asp:Label ID="lblValidDigitalIdmsg" runat="server" Style="color: red" Text="* Please select image" Visible="false"></asp:Label><br />
                                                        <span style="color: darkviolet">Note : Only Image Suported Format is .jpg | .jpeg | .png | .bmp | .gif | .eps...</span><br />
                                                        <asp:Label ID="lblDigitalIdimgnotsuppoted" runat="server" Style="color: red;" Text="You upload image format not suppoted.." Visible="false"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="clearfix"></div>
                                    <div class="col-md-12 col-sm-12" style="text-align: right;">
                                        <div class="portlet-body">
                                            <div class="form-body">
                                                <div class="form-group">
                                                    <div class="col-md-1 col-sm-1">
                                                    </div>
                                                    <div class="col-md-1 col-sm-1">
                                                    </div>
                                                    <div class="col-md-10">
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
    </div>
</asp:Content>
