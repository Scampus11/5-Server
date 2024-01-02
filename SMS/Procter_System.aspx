<%@ Page Title="" Language="C#" MasterPageFile="~/Admin_Master.Master" AutoEventWireup="true" CodeBehind="Procter_System.aspx.cs" Inherits="SMS.Procter_System" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Procter System</title>
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdateRefresh" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <br />
            <br />
            <div class="page-content-wrapper" style="margin-bottom: -16px;">
                <div class="page-content">
                    <h3 class="page-title bold">Procter System</h3>
                    <div class="modal-content" id="DivEdit" runat="server">
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
                                    <li class="nav-item active" id="liHostel" runat="server">
                                        <a class="nav-link" href="Procter_System.aspx?HostelGrid=Grid" id="aHostel" runat="server">Hostel</a>
                                    </li>
                                    <li class="nav-item" id="liBlock" runat="server">
                                        <a class="nav-link" href="Procter_System.aspx?BlockGrid=Grid" id="aBlock" runat="server">Block</a>
                                    </li>
                                    <li class="nav-item" id="liFloor" runat="server">
                                        <a class="nav-link" href="Procter_System.aspx?FloorGrid=Grid" id="aFloor" runat="server">Floor</a>
                                    </li>
                                    <li class="nav-item" id="liRoom" runat="server">
                                        <a class="nav-link" href="Procter_System.aspx?RoomGrid=Grid" id="aRoom" runat="server">Room</a>
                                    </li>
                                    <li class="nav-item" id="liBed" runat="server">
                                        <a class="nav-link" href="Procter_System.aspx?BedGrid=Grid" id="aBed" runat="server">Bed</a>
                                    </li>
                                    <li class="nav-item" id="liLocker" runat="server">
                                        <a class="nav-link" href="Procter_System.aspx?LockerGrid=Grid" id="aLocker" runat="server">Locker</a>
                                    </li>
                                </ul>

                                <!-- Tab panes -->
                                <div class="tab-content">
                                    <%--Hotel--%>
                                    <div class="tab-pane container active" id="divHostel" runat="server" visible="false">
                                        <div class="row ">
                                            <div class="col-md-6 col-sm-6">
                                                <div class="portlet-body">
                                                    <div class="form-body">
                                                        <div class="form-group">
                                                            <label class="col-md-3 control-label">
                                                                Hostel Id :
                                                            </label>
                                                            <div class="col-md-6">
                                                                <asp:TextBox ID="txtHostelId" runat="server" CssClass="form-control" BackColor="#d3d3d3" placeholder="Hostel Id" MaxLength="20"></asp:TextBox>
                                                                <asp:Label ID="lblvalidHostelId" runat="server" Style="color: red;" Text="* Enter Hostel Id..." Visible="false"></asp:Label>
                                                                <asp:Label ID="lblValidDuplicateHostelId" runat="server" Style="color: red;" Text="* Hostel Id Already Exist..." Visible="false"></asp:Label>
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
                                                                Hostel Name :
                                                            </label>
                                                            <div class="col-md-6">
                                                                <asp:TextBox ID="txtHostelName" runat="server" CssClass="form-control" BackColor="#d3d3d3" placeholder="Hostel Name"></asp:TextBox>
                                                                <asp:Label ID="lblvalidHostelName" runat="server" Style="color: red;" Text="* Enter Hostel Name..." Visible="false"></asp:Label>
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
                                                            <label class="col-md-3 control-label">Hostel Type : </label>
                                                            <div class="col-md-6">
                                                                <asp:DropDownList ID="ddlHostelType" runat="server" CssClass="form-control" BackColor="#d3d3d3">
                                                                    <asp:ListItem Value="0" Selected="True">--Select Hostel Type--</asp:ListItem>
                                                                    <asp:ListItem Value="1">Boys</asp:ListItem>
                                                                    <asp:ListItem Value="2 ">Girls</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:Label ID="lblvalidHostelType" runat="server" Style="color: red;" Text="* Enter Hostel Type..." Visible="false"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-6 col-sm-6">
                                                <div class="portlet-body">
                                                    <div class="form-body">
                                                        <div class="form-group">
                                                            <label class="col-md-3 control-label">Total Block : </label>
                                                            <div class="col-md-6">
                                                                <asp:TextBox ID="txtTotalBlock" runat="server" CssClass="form-control" BackColor="#d3d3d3" placeholder="Total Block" MaxLength="3" Text="1" onkeypress="return isNumber(event)"></asp:TextBox>
                                                                <asp:Label ID="lblValidTotalBlock" runat="server" Style="color: red;" Text="* Enter Total Block..." Visible="false"></asp:Label>
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
                                                            <label class="col-md-3 control-label">Status : </label>
                                                            <div class="col-md-6">
                                                                <asp:DropDownList ID="ddlHostelStatus" runat="server" CssClass="form-control" BackColor="#d3d3d3">
                                                                    <asp:ListItem Value="0" Selected="True">--Select Status--</asp:ListItem>
                                                                    <asp:ListItem Value="1">Active</asp:ListItem>
                                                                    <asp:ListItem Value="2">Deactive</asp:ListItem>
                                                                    <asp:ListItem Value="3">Damage</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:Label ID="lblValidHostelStatus" runat="server" Style="color: red;" Text="* Select Status..." Visible="false"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="clearfix"></div>
                                            <br />
                                            <br />
                                            <br />
                                            <asp:LinkButton ID="lnkhostelsave" runat="server" Text="Save" class="btn green-meadow" OnClick="lnkhostelsave_Click" />
                                            <asp:LinkButton ID="lnkhostelupdate" runat="server" Text="Update" class="btn green-meadow" OnClick="lnkhostelupdate_Click" />
                                            <asp:LinkButton ID="lnkhostelBack" runat="server" Text="Back" class="btn green-meadow" OnClick="lnkhostelBack_Click" />
                                            <asp:LinkButton ID="lnkhostelClear" runat="server" Text="Clear" class="btn green-meadow" OnClick="lnkhostelClear_Click" />
                                        </div>
                                    </div>
                                    <div class="row" id="divgridHostel" visible="false" runat="server">
                                        <div class="col-md-12">
                                            <div class="portlet box green-meadow">
                                                <div class="portlet-title">
                                                    <div class="caption">
                                                        <i class="fas fa-city"></i>Hostels
                                                    </div>
                                                    <div class="tools pull-left" style="margin-left: 10px; padding: 8px; border-left: 0px!important;">
                                                        <asp:TextBox ID="txtSearchHostel" placeHolder="Search" runat="server" Width="230px" Height="28px" Style="border: 0px; color: black" />
                                                    </div>
                                                    <div class="tools pull-left" style="margin-left: 5px; padding: 8px; border-left: 0px!important;">
                                                        <asp:LinkButton ID="btnsearchHostel" runat="server" OnClick="btnsearchHostel_Click">
                                        <i class="fa fa-search" title="Search"  style="font-size: 28px; color: white; margin-top: 4px;"></i>
                                                        </asp:LinkButton>
                                                    </div>
                                                    <div class="tools">
                                                        <a class="pull-right" style="color: white" href="Procter_System.aspx?HostelAdd=Add">
                                                            <i class="fa fa-plus" title="Add New Hostel"></i></a>
                                                    </div>
                                                </div>

                                                <div class="portlet-body">
                                                    <asp:GridView ID="gridHostel" runat="server" class="table table-striped table-bordered table-hover" AutoGenerateColumns="false"
                                                        AllowPaging="true" AllowSorting="true" OnSorting="HostelOnSorting" OnPageIndexChanging="HostelOnPageIndexChanging" PageSize="5"
                                                        ShowHeaderWhenEmpty="true" EmptyDataText="No records Found">
                                                        <PagerSettings Mode="NumericFirstLast" PageButtonCount="10" FirstPageText="First" LastPageText="Last" />
                                                        <PagerStyle CssClass="gridview"></PagerStyle>
                                                        <Columns>
                                                            <asp:TemplateField ItemStyle-Width="30px" HeaderText="Hostel Id" SortExpression="HostelId">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblHostelId" runat="server" Text='<%#Eval("HostelId") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ItemStyle-Width="30px" HeaderText="Name" SortExpression="Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblName" runat="server" Text='<%#Eval("Name") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ItemStyle-Width="70px" HeaderText="Type" SortExpression="Type">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblType" runat="server" Text='<%#Eval("Type") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ItemStyle-Width="100px" HeaderText="Total Blocks" SortExpression="TotalBlocks">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblTotalBlocks" runat="server" Text='<%#Eval("TotalBlocks") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ItemStyle-Width="100px" HeaderText="Status" SortExpression="Status">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("Status") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ItemStyle-Width="100px" HeaderText="User Name" SortExpression="UserId" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblUserId" runat="server" Text='<%#Eval("UserId") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ItemStyle-Width="100px" HeaderText="CreatedDate" SortExpression="CreatedDate" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblCreatedDate" runat="server" Text='<%#Eval("CreatedDate","{0: MMM dd yyy hh:mm ss}") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ItemStyle-Width="100px" HeaderText="UpdatedDate" SortExpression="UpdatedDate" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblUpdatedDate" runat="server" Text='<%#Eval("UpdatedDate","{0: MMM dd yyy hh:mm ss}") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ItemStyle-Width="10px" HeaderText="Edit" SortExpression="Id">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkHostelEdit" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id")%>' OnClick="lnkHostelEdit_Click"><i class="material-icons" title="Edit row">&#xe3c9;</i></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <%--Block--%>
                                    <div class="tab-pane container active" id="divBlock" runat="server" visible="true">
                                        <div class="row ">
                                            <div class="col-md-6 col-sm-6">
                                                <div class="portlet-body">
                                                    <div class="form-body">
                                                        <div class="form-group">
                                                            <label class="col-md-3 control-label">
                                                                Block Number :
                                                            </label>
                                                            <div class="col-md-6">
                                                                <asp:TextBox ID="txtBlockNumber" runat="server" CssClass="form-control" BackColor="#d3d3d3" placeholder="Block Number" MaxLength="20"></asp:TextBox>
                                                                <asp:Label ID="lblValidBlockNumber" runat="server" Style="color: red;" Text="* Enter Block Number..." Visible="false"></asp:Label>
                                                                <asp:Label ID="lblValidDuplicateBlockNumber" runat="server" Style="color: red;" Text="* Block Number Already Exist..." Visible="false"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-6 col-sm-6">
                                                <div class="portlet-body">
                                                    <div class="form-body">
                                                        <div class="form-group">
                                                            <label class="col-md-3 control-label">Hostel Name : </label>
                                                            <div class="col-md-6">
                                                                <asp:DropDownList ID="ddlHostelName" runat="server" CssClass="form-control" BackColor="#d3d3d3"></asp:DropDownList>
                                                                <asp:Label ID="lblValidBlockHostelName" runat="server" Style="color: red;" Text="* Enter Hostel Name..." Visible="false"></asp:Label>
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
                                                                Block Name :
                                                            </label>
                                                            <div class="col-md-6">
                                                                <asp:TextBox ID="txtBlockName" runat="server" CssClass="form-control" BackColor="#d3d3d3" placeholder="Block Name"></asp:TextBox>
                                                                <asp:Label ID="lblValidBlockName" runat="server" Style="color: red;" Text="* Enter Block Name..." Visible="false"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-6 col-sm-6">
                                                <div class="portlet-body">
                                                    <div class="form-body">
                                                        <div class="form-group">
                                                            <label class="col-md-3 control-label">Block Description : </label>
                                                            <div class="col-md-6">
                                                                <asp:TextBox ID="txtBlockDescription" runat="server" CssClass="form-control" placeholder="Block Description"></asp:TextBox>
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
                                                            <label class="col-md-3 control-label">Total Occupancy : </label>
                                                            <div class="col-md-6">
                                                                <asp:TextBox ID="txtTotalOccupancy" runat="server" CssClass="form-control" placeholder="Total Occupancy" Text="0" onkeypress="return isNumber(event)"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-6 col-sm-6">
                                                <div class="portlet-body">
                                                    <div class="form-body">
                                                        <div class="form-group">
                                                            <label class="col-md-3 control-label">Assign Occupancy : </label>
                                                            <div class="col-md-6">
                                                                <asp:TextBox ID="txtAssignOccupancy" runat="server" CssClass="form-control" placeholder="Assign Occupancy" Text="0" onkeypress="return isNumber(event)"></asp:TextBox>
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
                                                            <label class="col-md-3 control-label">Available Occupancy : </label>
                                                            <div class="col-md-6">
                                                                <asp:TextBox ID="txtAvailableOccupancy" runat="server" CssClass="form-control" placeholder="Available Occupancy" Text="0" onkeypress="return isNumber(event)"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-6 col-sm-6">
                                                <div class="portlet-body">
                                                    <div class="form-body">
                                                        <div class="form-group">
                                                            <label class="col-md-3 control-label">Status : </label>
                                                            <div class="col-md-6">
                                                                <asp:DropDownList ID="ddlBlockStatus" runat="server" CssClass="form-control" BackColor="#d3d3d3">
                                                                    <asp:ListItem Value="0" Selected="True">--Select Status--</asp:ListItem>
                                                                    <asp:ListItem Value="1">Active</asp:ListItem>
                                                                    <asp:ListItem Value="2">Deactive</asp:ListItem>
                                                                    <asp:ListItem Value="3">Damage</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:Label ID="lblValidBlockhostelStatus" runat="server" Style="color: red;" Text="* Select Status..." Visible="false"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="clearfix"></div>
                                            <br />
                                            <br />
                                            <br />
                                            <asp:LinkButton ID="lnkBlocksave" runat="server" Text="Save" class="btn green-meadow" OnClick="lnkBlocksave_Click" />
                                            <asp:LinkButton ID="lnkBlockupdate" runat="server" Text="Update" class="btn green-meadow" OnClick="lnkBlockupdate_Click" />
                                            <asp:LinkButton ID="lnkBlockback" runat="server" Text="Back" class="btn green-meadow" OnClick="lnkBlockback_Click" />
                                            <asp:LinkButton ID="lnkBlockclear" runat="server" Text="Clear" class="btn green-meadow" OnClick="lnkBlockclear_Click" />
                                        </div>
                                    </div>
                                    <div class="row" id="divgridBlock" visible="false" runat="server">
                                        <div class="col-md-12">
                                            <div class="portlet box green-meadow">
                                                <div class="portlet-title">
                                                    <div class="caption">
                                                        <i class="fa fa-building"></i>Blocks
                                                    </div>
                                                    <div class="tools pull-left" style="margin-left: 10px; padding: 8px; border-left: 0px!important;">
                                                        <asp:TextBox ID="txtBlockSearch" placeHolder="Search" runat="server" Width="230px" Height="28px" Style="border: 0px; color: black" />
                                                    </div>
                                                    <div class="tools pull-left" style="margin-left: 5px; padding: 8px; border-left: 0px!important;">
                                                        <asp:LinkButton ID="lnkBlockSearch" runat="server" OnClick="lnkBlockSearch_Click">
                                        <i class="fa fa-search" title="Search"  style="font-size: 28px; color: white; margin-top: 4px;"></i>
                                                        </asp:LinkButton>
                                                    </div>
                                                    <div class="tools">
                                                        <a class="pull-right" style="color: white" href="Procter_System.aspx?BlockAdd=Add">
                                                            <i class="fa fa-plus" title="Add New Block"></i></a>
                                                    </div>
                                                </div>

                                                <div class="portlet-body">
                                                    <asp:GridView ID="GridBlock" runat="server" class="table table-striped table-bordered table-hover" AutoGenerateColumns="false"
                                                        AllowPaging="true" AllowSorting="true" OnSorting="GridBlock_Sorting" OnPageIndexChanging="GridBlock_PageIndexChanging" PageSize="5"
                                                        ShowHeaderWhenEmpty="true" EmptyDataText="No records Found">
                                                        <PagerSettings Mode="NumericFirstLast" PageButtonCount="10" FirstPageText="First" LastPageText="Last" />
                                                        <PagerStyle CssClass="gridview"></PagerStyle>
                                                        <Columns>
                                                            <asp:TemplateField ItemStyle-Width="30px" HeaderText="Block Number" SortExpression="Number">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblBlockNumber" runat="server" Text='<%#Eval("Number") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ItemStyle-Width="30px" HeaderText="Name" SortExpression="Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblName" runat="server" Text='<%#Eval("Name") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ItemStyle-Width="30px" HeaderText="Hostel Name" SortExpression="HostelName">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblHostelName" runat="server" Text='<%#Eval("HostelName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField ItemStyle-Width="70px" HeaderText="Description" SortExpression="Description">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDescription" runat="server" Text='<%#Eval("Description") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ItemStyle-Width="100px" HeaderText="Total Occupancy" SortExpression="Total_Occupancy">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblTotal_Occupancy" runat="server" Text='<%#Eval("Total_Occupancy") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ItemStyle-Width="100px" HeaderText="Assign Occupancy" SortExpression="Assign_Occupancy">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblAssign_Occupancy" runat="server" Text='<%#Eval("Assign_Occupancy") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ItemStyle-Width="100px" HeaderText="Available Occupancy" SortExpression="Available_Occupancy">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblAvailable_Occupancy" runat="server" Text='<%#Eval("Available_Occupancy") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ItemStyle-Width="100px" HeaderText="Status" SortExpression="Status">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("Status") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ItemStyle-Width="100px" HeaderText="User Name" SortExpression="UserId" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblUserId" runat="server" Text='<%#Eval("UserId") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ItemStyle-Width="100px" HeaderText="CreatedDate" SortExpression="CreatedDate" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblCreatedDate" runat="server" Text='<%#Eval("CreatedDate","{0: MMM dd yyy hh:mm ss}") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ItemStyle-Width="100px" HeaderText="UpdatedDate" SortExpression="UpdatedDate" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblUpdatedDate" runat="server" Text='<%#Eval("UpdatedDate","{0: MMM dd yyy hh:mm ss}") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ItemStyle-Width="10px" HeaderText="Edit" SortExpression="Id">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkBlockEdit" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id")%>' OnClick="lnkBlockEdit_Click"><i class="material-icons" title="Edit row">&#xe3c9;</i></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <%--Floor--%>
                                    <div class="tab-pane container active" id="divFloor" runat="server">
                                        <div class="row ">
                                            <div class="col-md-6 col-sm-6">
                                                <div class="portlet-body">
                                                    <div class="form-body">
                                                        <div class="form-group">
                                                            <label class="col-md-3 control-label">
                                                                Floor Number :
                                                            </label>
                                                            <div class="col-md-6">
                                                                <asp:TextBox ID="txtFloorNumber" runat="server" CssClass="form-control" BackColor="#d3d3d3" placeholder="Floor Number" MaxLength="20"></asp:TextBox>
                                                                <asp:Label ID="lblValidFloorNumber" runat="server" Style="color: red;" Text="* Enter Floor Number..." Visible="false"></asp:Label>
                                                                <asp:Label ID="lblValiddFlooruplicate" runat="server" Style="color: red;" Text="* Floor Number Already Exist..." Visible="false"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-6 col-sm-6">
                                                <div class="portlet-body">
                                                    <div class="form-body">
                                                        <div class="form-group">
                                                            <label class="col-md-3 control-label">Hostel Name : </label>
                                                            <div class="col-md-6">
                                                                <asp:DropDownList ID="ddlFloorHostelName" runat="server" CssClass="form-control" BackColor="#d3d3d3" OnTextChanged="ddlFloorHostelName_TextChanged" AutoPostBack="true">
                                                                </asp:DropDownList>
                                                                <asp:Label ID="lblValidFloorhostelName" runat="server" Style="color: red;" Text="* Select Hostel Name..." Visible="false"></asp:Label>
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
                                                            <label class="col-md-3 control-label">Block Name : </label>
                                                            <div class="col-md-6">
                                                                <asp:DropDownList ID="ddlBlockName" runat="server" CssClass="form-control" BackColor="#d3d3d3">
                                                                    <asp:ListItem Value="0">--Select Block Name--</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:Label ID="lblValidFloorBlockName" runat="server" Style="color: red;" Text="* Select Block Name..." Visible="false"></asp:Label>
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
                                                                Floor Name :
                                                            </label>
                                                            <div class="col-md-6">
                                                                <asp:TextBox ID="txtFloorName" runat="server" CssClass="form-control" BackColor="#d3d3d3" placeholder="Floor Name"></asp:TextBox>
                                                                <asp:Label ID="lblValidFloorName" runat="server" Style="color: red;" Text="* Enter Floor Name..." Visible="false"></asp:Label>
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
                                                            <label class="col-md-3 control-label">Room Capacity : </label>
                                                            <div class="col-md-6">
                                                                <asp:TextBox ID="txtRoomCapacity" runat="server" CssClass="form-control" placeholder="Room Capacity" Text="0" onkeypress="return isNumber(event)"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-6 col-sm-6">
                                                <div class="portlet-body">
                                                    <div class="form-body">
                                                        <div class="form-group">
                                                            <label class="col-md-3 control-label">Status : </label>
                                                            <div class="col-md-6">
                                                                <asp:DropDownList ID="ddlFloorStatus" runat="server" CssClass="form-control" BackColor="#d3d3d3">
                                                                    <asp:ListItem Value="0" Selected="True">--Select Status--</asp:ListItem>
                                                                    <asp:ListItem Value="1">Active</asp:ListItem>
                                                                    <asp:ListItem Value="2">Deactive</asp:ListItem>
                                                                    <asp:ListItem Value="3">Damage</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:Label ID="lblValidFloorStatus" runat="server" Style="color: red;" Text="* Select Status..." Visible="false"></asp:Label>
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
                                                            <label class="col-md-3 control-label">Floor Description : </label>
                                                            <div class="col-md-6">
                                                                <asp:TextBox ID="txtFloorDescription" runat="server" CssClass="form-control" placeholder="Floor Description"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="clearfix"></div>
                                            <br />
                                            <br />
                                            <br />
                                            <asp:LinkButton ID="lnkFloorsave" runat="server" Text="Save" class="btn green-meadow" OnClick="lnkFloorsave_Click" />
                                            <asp:LinkButton ID="lnkFloorupdate" runat="server" Text="Update" class="btn green-meadow" OnClick="lnkFloorupdate_Click" />
                                            <asp:LinkButton ID="lnkFloorback" runat="server" Text="Back" class="btn green-meadow" OnClick="lnkFloorback_Click" />
                                            <asp:LinkButton ID="lnkFloorclear" runat="server" Text="Clear" class="btn green-meadow" OnClick="lnkFloorclear_Click" />
                                        </div>
                                    </div>
                                    <div class="row" id="divgridFloor" visible="false" runat="server">
                                        <div class="col-md-12">
                                            <div class="portlet box green-meadow">
                                                <div class="portlet-title">
                                                    <div class="caption">
                                                        <i  class='far'>&#xf07b;</i>Floors
                                                    </div>
                                                    <div class="tools pull-left" style="margin-left: 10px; padding: 8px; border-left: 0px!important;">
                                                        <asp:TextBox ID="txtFloorSearch" placeHolder="Search" runat="server" Width="230px" Height="28px" Style="border: 0px; color: black" />
                                                    </div>
                                                    <div class="tools pull-left" style="margin-left: 5px; padding: 8px; border-left: 0px!important;">
                                                        <asp:LinkButton ID="lnkFloorSearch" runat="server" OnClick="lnkFloorSearch_Click">
                                        <i class="fa fa-search" title="Search"  style="font-size: 28px; color: white; margin-top: 4px;"></i>
                                                        </asp:LinkButton>
                                                    </div>
                                                    <div class="tools">
                                                        <a class="pull-right" style="color: white" href="Procter_System.aspx?FloorAdd=Add">
                                                            <i class="fa fa-plus" title="Add New Floor"></i></a>
                                                    </div>
                                                </div>

                                                <div class="portlet-body">
                                                    <asp:GridView ID="GridFloor" runat="server" class="table table-striped table-bordered table-hover" AutoGenerateColumns="false"
                                                        AllowPaging="true" AllowSorting="true" OnSorting="GridFloor_Sorting" OnPageIndexChanging="GridFloor_PageIndexChanging" PageSize="10"
                                                        ShowHeaderWhenEmpty="true" EmptyDataText="No records Found">
                                                        <PagerSettings Mode="NumericFirstLast" PageButtonCount="10" FirstPageText="First" LastPageText="Last" />
                                                        <PagerStyle CssClass="gridview"></PagerStyle>
                                                        <Columns>
                                                            <asp:TemplateField ItemStyle-Width="30px" HeaderText="Floor Number" SortExpression="Number">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblBlockNumber" runat="server" Text='<%#Eval("Number") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ItemStyle-Width="30px" HeaderText="Name" SortExpression="Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblName" runat="server" Text='<%#Eval("Name") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ItemStyle-Width="30px" HeaderText="Description" SortExpression="Description">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDescription" runat="server" Text='<%#Eval("Description") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ItemStyle-Width="30px" HeaderText="Block Name" SortExpression="BlockID">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblBlockID" runat="server" Text='<%#Eval("BlockID") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ItemStyle-Width="30px" HeaderText="Room Capacity" SortExpression="Room_Capacity">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblRoom_Capacity" runat="server" Text='<%#Eval("Room_Capacity") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ItemStyle-Width="100px" HeaderText="Status" SortExpression="Status">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("Status") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ItemStyle-Width="100px" HeaderText="User Name" SortExpression="UserId" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblUserId" runat="server" Text='<%#Eval("UserId") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ItemStyle-Width="100px" HeaderText="CreatedDate" SortExpression="CreatedDate" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblCreatedDate" runat="server" Text='<%#Eval("CreatedDate","{0: MMM dd yyy hh:mm ss}") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ItemStyle-Width="100px" HeaderText="UpdatedDate" SortExpression="UpdatedDate" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblUpdatedDate" runat="server" Text='<%#Eval("UpdatedDate","{0: MMM dd yyy hh:mm ss}") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ItemStyle-Width="10px" HeaderText="Edit" SortExpression="Id">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkFloorEdit" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id")%>' OnClick="lnkFloorEdit_Click"><i class="material-icons" title="Edit row">&#xe3c9;</i></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <%--Room--%>
                                    <div class="tab-pane container active" id="divRoom" runat="server">
                                        <div class="row ">
                                            <div class="col-md-6 col-sm-6">
                                                <div class="portlet-body">
                                                    <div class="form-body">
                                                        <div class="form-group">
                                                            <label class="col-md-3 control-label">
                                                                Room Number :
                                                            </label>
                                                            <div class="col-md-6">
                                                                <asp:TextBox ID="txtRoomNumber" runat="server" CssClass="form-control" BackColor="#d3d3d3" placeholder="Room Number" MaxLength="20"></asp:TextBox>
                                                                <asp:Label ID="lblValidRoomNumber" runat="server" Style="color: red;" Text="* Enter Room Number..." Visible="false"></asp:Label>
                                                                <asp:Label ID="lblValidduplicateRoomNumber" runat="server" Style="color: red;" Text="* Room Number Already Exist..." Visible="false"></asp:Label>
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
                                                                Hostel Name :
                                                            </label>
                                                            <div class="col-md-6">
                                                                <asp:DropDownList ID="ddlRoomHostelName" runat="server" CssClass="form-control" BackColor="#d3d3d3" OnTextChanged="ddlRoomHostelName_TextChanged" AutoPostBack="true"></asp:DropDownList>
                                                                <asp:Label ID="lblValidRoomHostelName" runat="server" Style="color: red;" Text="* Select Hostel Name..." Visible="false"></asp:Label>
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
                                                                Block Name :
                                                            </label>
                                                            <div class="col-md-6">
                                                                <asp:DropDownList ID="ddlRoomBlockName" runat="server" CssClass="form-control" BackColor="#d3d3d3" OnTextChanged="ddlRoomBlockName_TextChanged" AutoPostBack="true">
                                                                    <asp:ListItem Value="0">--Select Block Name--</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:Label ID="lblValidRoomBlockName" runat="server" Style="color: red;" Text="* Select Block Name..." Visible="false"></asp:Label>
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
                                                                Floor Name :
                                                            </label>
                                                            <div class="col-md-6">
                                                                <asp:DropDownList ID="ddlFloorName" runat="server" CssClass="form-control" BackColor="#d3d3d3">
                                                                    <asp:ListItem Value="0">--Select Floor Name--</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:Label ID="lblValidFloorId" runat="server" Style="color: red;" Text="* Enter Floor Name..." Visible="false"></asp:Label>
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
                                                            <label class="col-md-3 control-label">Status : </label>
                                                            <div class="col-md-6">
                                                                <asp:DropDownList ID="ddlRoomStatus" runat="server" CssClass="form-control" BackColor="#d3d3d3">
                                                                    <asp:ListItem Value="0" Selected="True">--Select Status--</asp:ListItem>
                                                                    <asp:ListItem Value="1">Active</asp:ListItem>
                                                                    <asp:ListItem Value="2">Deactive</asp:ListItem>
                                                                    <asp:ListItem Value="3">Damage</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:Label ID="lblValidRoomStatus" runat="server" Style="color: red;" Text="* Select Room Status..." Visible="false"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="clearfix"></div>
                                            <br />
                                            <br />
                                            <br />
                                            <asp:LinkButton ID="lnkRoomSave" runat="server" Text="Save" class="btn green-meadow" OnClick="lnkRoomSave_Click" />
                                            <asp:LinkButton ID="lnkRoomupdate" runat="server" Text="Update" class="btn green-meadow" OnClick="lnkRoomupdate_Click" />
                                            <asp:LinkButton ID="lnkRoomBack" runat="server" Text="Back" class="btn green-meadow" OnClick="lnkRoomBack_Click" />
                                            <asp:LinkButton ID="lnkRoomClear" runat="server" Text="Clear" class="btn green-meadow" OnClick="lnkRoomClear_Click" />
                                        </div>
                                    </div>
                                    <div class="row" id="divgridRoom" visible="false" runat="server">
                                        <div class="col-md-12">
                                            <div class="portlet box green-meadow">
                                                <div class="portlet-title">
                                                    <div class="caption">
                                                        <i  class='fas'>&#xf965;</i>Rooms
                                                    </div>
                                                    <div class="tools pull-left" style="margin-left: 10px; padding: 8px; border-left: 0px!important;">
                                                        <asp:TextBox ID="txtRoomSearch" placeHolder="Search" runat="server" Width="230px" Height="28px" Style="border: 0px; color: black" />
                                                    </div>
                                                    <div class="tools pull-left" style="margin-left: 5px; padding: 8px; border-left: 0px!important;">
                                                        <asp:LinkButton ID="lnkRoomSearch" runat="server" OnClick="lnkRoomSearch_Click">
                                        <i class="fa fa-search" title="Search"  style="font-size: 28px; color: white; margin-top: 4px;"></i>
                                                        </asp:LinkButton>
                                                    </div>
                                                    <div class="tools">
                                                        <a class="pull-right" style="color: white" href="Procter_System.aspx?RoomAdd=Add">
                                                            <i class="fa fa-plus" title="Add New Room"></i></a>
                                                    </div>
                                                </div>

                                                <div class="portlet-body">
                                                    <asp:GridView ID="GridRoom" runat="server" class="table table-striped table-bordered table-hover" AutoGenerateColumns="false"
                                                        AllowPaging="true" AllowSorting="true" OnSorting="GridRoom_Sorting" OnPageIndexChanging="GridRoom_PageIndexChanging" PageSize="5"
                                                        ShowHeaderWhenEmpty="true" EmptyDataText="No records Found">
                                                        <PagerSettings Mode="NumericFirstLast" PageButtonCount="10" FirstPageText="First" LastPageText="Last" />
                                                        <PagerStyle CssClass="gridview"></PagerStyle>
                                                        <Columns>
                                                            <asp:TemplateField ItemStyle-Width="30px" HeaderText="Room Number" SortExpression="Number">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblRoomNumber" runat="server" Text='<%#Eval("Number") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ItemStyle-Width="30px" HeaderText="Floor Name" SortExpression="FloorID">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblFloorID" runat="server" Text='<%#Eval("FloorID") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ItemStyle-Width="100px" HeaderText="Status" SortExpression="Status">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("Status") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ItemStyle-Width="100px" HeaderText="User Name" SortExpression="UserId" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblUserId" runat="server" Text='<%#Eval("UserId") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ItemStyle-Width="100px" HeaderText="CreatedDate" SortExpression="CreatedDate" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblCreatedDate" runat="server" Text='<%#Eval("CreatedDate","{0: MMM dd yyy hh:mm ss}") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ItemStyle-Width="100px" HeaderText="UpdatedDate" SortExpression="UpdatedDate" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblUpdatedDate" runat="server" Text='<%#Eval("UpdatedDate","{0: MMM dd yyy hh:mm ss}") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ItemStyle-Width="10px" HeaderText="Edit" SortExpression="Id">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkRoomEdit" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id")%>' OnClick="lnkRoomEdit_Click"><i class="material-icons" title="Edit row">&#xe3c9;</i></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <%--BED--%>
                                    <div class="tab-pane container active" id="divBed" runat="server">
                                        <div class="row ">
                                            <div class="col-md-6 col-sm-6">
                                                <div class="portlet-body">
                                                    <div class="form-body">
                                                        <div class="form-group">
                                                            <label class="col-md-3 control-label">
                                                                Bed Number :
                                                            </label>
                                                            <div class="col-md-6">
                                                                <asp:TextBox ID="txtBedNumber" runat="server" CssClass="form-control" BackColor="#d3d3d3" placeholder="Bed Number" MaxLength="20"></asp:TextBox>
                                                                <asp:Label ID="lblValidBedNumber" runat="server" Style="color: red;" Text="* Enter Bed Number..." Visible="false"></asp:Label>
                                                                <asp:Label ID="lblValidduplicateBedNumber" runat="server" Style="color: red;" Text="* Bed Number Already Exist..." Visible="false"></asp:Label>
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
                                                                Hostel Name :
                                                            </label>
                                                            <div class="col-md-6">
                                                                <asp:DropDownList ID="ddlBedHostelName" runat="server" CssClass="form-control" BackColor="#d3d3d3" OnTextChanged="ddlBedHostelName_TextChanged" AutoPostBack="true">
                                                                </asp:DropDownList>
                                                                <asp:Label ID="lblValidBedHostelName" runat="server" Style="color: red;" Text="* Select Bed Name..." Visible="false"></asp:Label>
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
                                                                Block Name :
                                                            </label>
                                                            <div class="col-md-6">
                                                                <asp:DropDownList ID="ddlBedBlockName" runat="server" CssClass="form-control" BackColor="#d3d3d3" OnTextChanged="ddlBedBlockName_TextChanged" AutoPostBack="true">
                                                                    <asp:ListItem Value="0" Selected="True">--Select Block Name--</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:Label ID="lblValidBedBlockName" runat="server" Style="color: red;" Text="* Select Block Name..." Visible="false"></asp:Label>
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
                                                                Floor Name :
                                                            </label>
                                                            <div class="col-md-6">
                                                                <asp:DropDownList ID="ddlBedFloorName" runat="server" CssClass="form-control" BackColor="#d3d3d3" OnTextChanged="ddlBedFloorName_TextChanged" AutoPostBack="true">
                                                                    <asp:ListItem Value="0" Selected="True">--Select Floor Name--</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:Label ID="lblValidBedFloorName" runat="server" Style="color: red;" Text="* Select Floor Name..." Visible="false"></asp:Label>
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
                                                                Room Number :
                                                            </label>
                                                            <div class="col-md-6">
                                                                <asp:DropDownList ID="ddlBED_RoomNumber" runat="server" CssClass="form-control" BackColor="#d3d3d3">
                                                                    <asp:ListItem Value="0" Selected="True">--Select Room Number--</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:Label ID="lblBED_RoomNumber" runat="server" Style="color: red;" Text="* Enter Room Number..." Visible="false"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-6 col-sm-6">
                                                <div class="portlet-body">
                                                    <div class="form-body">
                                                        <div class="form-group">
                                                            <label class="col-md-3 control-label">Status : </label>
                                                            <div class="col-md-6">
                                                                <asp:DropDownList ID="ddlBedStatus" runat="server" CssClass="form-control" BackColor="#d3d3d3">
                                                                    <asp:ListItem Value="0" Selected="True">--Select Status--</asp:ListItem>
                                                                    <asp:ListItem Value="1">Active</asp:ListItem>
                                                                    <asp:ListItem Value="2">Deactive</asp:ListItem>
                                                                    <asp:ListItem Value="3">Damage</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:Label ID="lblValidBedStatus" runat="server" Style="color: red;" Text="* Select Status..." Visible="false"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="clearfix"></div>
                                            <br />
                                            <br />
                                            <br />
                                            <asp:LinkButton ID="lnkBedSave" runat="server" Text="Save" class="btn green-meadow" OnClick="lnkBedSave_Click" />
                                            <asp:LinkButton ID="lnkBedUpdate" runat="server" Text="Update" class="btn green-meadow" OnClick="lnkBedUpdate_Click" />
                                            <asp:LinkButton ID="lnkBedBack" runat="server" Text="Back" class="btn green-meadow" OnClick="lnkBedBack_Click" />
                                            <asp:LinkButton ID="lnkBedClear" runat="server" Text="Clear" class="btn green-meadow" OnClick="lnkBedClear_Click" />
                                        </div>
                                    </div>
                                    <div class="row" id="divgridBED" visible="false" runat="server">
                                        <div class="col-md-12">
                                            <div class="portlet box green-meadow">
                                                <div class="portlet-title">
                                                    <div class="caption">
                                                        <i  class="fa">&#xf236;</i>Beds
                                                    </div>
                                                    <div class="tools pull-left" style="margin-left: 10px; padding: 8px; border-left: 0px!important;">
                                                        <asp:TextBox ID="txtBEDSearch" placeHolder="Search" runat="server" Width="230px" Height="28px" Style="border: 0px; color: black" />
                                                    </div>
                                                    <div class="tools pull-left" style="margin-left: 5px; padding: 8px; border-left: 0px!important;">
                                                        <asp:LinkButton ID="lnkBEDSearch" runat="server" OnClick="lnkBEDSearch_Click">
                                        <i class="fa fa-search" title="Search"  style="font-size: 28px; color: white; margin-top: 4px;"></i>
                                                        </asp:LinkButton>
                                                    </div>
                                                    <div class="tools">
                                                        <a class="pull-right" style="color: white" href="Procter_System.aspx?BEDAdd=Add">
                                                            <i class="fa fa-plus" title="Add New BED"></i></a>
                                                    </div>
                                                </div>

                                                <div class="portlet-body">
                                                    <asp:GridView ID="GridBED" runat="server" class="table table-striped table-bordered table-hover" AutoGenerateColumns="false"
                                                        AllowPaging="true" AllowSorting="true" OnSorting="GridBED_Sorting" OnPageIndexChanging="GridBED_PageIndexChanging" PageSize="10"
                                                        ShowHeaderWhenEmpty="true" EmptyDataText="No records Found">
                                                        <PagerSettings Mode="NumericFirstLast" PageButtonCount="10" FirstPageText="First" LastPageText="Last" />
                                                        <PagerStyle CssClass="gridview"></PagerStyle>
                                                        <Columns>
                                                            <asp:TemplateField ItemStyle-Width="30px" HeaderText="BED Number" SortExpression="Number">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblBEDNumber" runat="server" Text='<%#Eval("Number") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ItemStyle-Width="30px" HeaderText="Room Name" SortExpression="RoomID">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblRoomID" runat="server" Text='<%#Eval("RoomID") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ItemStyle-Width="100px" HeaderText="Status" SortExpression="Status">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("Status") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ItemStyle-Width="100px" HeaderText="User Name" SortExpression="UserId" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblUserId" runat="server" Text='<%#Eval("UserId") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ItemStyle-Width="100px" HeaderText="CreatedDate" SortExpression="CreatedDate" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblCreatedDate" runat="server" Text='<%#Eval("CreatedDate","{0: MMM dd yyy hh:mm ss}") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ItemStyle-Width="100px" HeaderText="UpdatedDate" SortExpression="UpdatedDate" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblUpdatedDate" runat="server" Text='<%#Eval("UpdatedDate","{0: MMM dd yyy hh:mm ss}") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ItemStyle-Width="10px" HeaderText="Edit" SortExpression="Id">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkBEDEdit" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id")%>' OnClick="lnkBEDEdit_Click"><i class="material-icons" title="Edit row">&#xe3c9;</i></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <%--Locker--%>
                                    <div class="tab-pane container active" id="divLocker" runat="server">
                                        <div class="row ">
                                            <div class="col-md-6 col-sm-6">
                                                <div class="portlet-body">
                                                    <div class="form-body">
                                                        <div class="form-group">
                                                            <label class="col-md-3 control-label">
                                                                Locker Number :
                                                            </label>
                                                            <div class="col-md-6">
                                                                <asp:TextBox ID="txtLockerNumber" runat="server" CssClass="form-control" BackColor="#d3d3d3" placeholder="Locker Number" MaxLength="20"></asp:TextBox>
                                                                <asp:Label ID="lblValidLockerNumber" runat="server" Style="color: red;" Text="* Enter Locker Number..." Visible="false"></asp:Label>
                                                                <asp:Label ID="lblValidduplicateLockerNumber" runat="server" Style="color: red;" Text="* Locker Number Already Exist..." Visible="false"></asp:Label>
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
                                                                Locker Name :
                                                            </label>
                                                            <div class="col-md-6">
                                                                <asp:TextBox ID="txtLockerName" runat="server" CssClass="form-control" BackColor="#d3d3d3" placeholder="Locker Name"></asp:TextBox>
                                                                <asp:Label ID="lblValidLockerName" runat="server" Style="color: red;" Text="* Enter Locker Name..." Visible="false"></asp:Label>
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
                                                            <label class="col-md-3 control-label">Hostel Name : </label>
                                                            <div class="col-md-6">
                                                                <asp:DropDownList ID="ddlLockerHostelName" runat="server" CssClass="form-control" BackColor="#d3d3d3" OnSelectedIndexChanged="ddlLockerHostelName_SelectedIndexChanged" AutoPostBack="true">
                                                                </asp:DropDownList>
                                                                <asp:Label ID="lblValidLockerHostelName" runat="server" Style="color: red;" Text="* Select Hostel Name..." Visible="false"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-6 col-sm-6">
                                                <div class="portlet-body">
                                                    <div class="form-body">
                                                        <div class="form-group">
                                                            <label class="col-md-3 control-label">Block Name : </label>
                                                            <div class="col-md-6">
                                                                <asp:DropDownList ID="ddlLockerBlockName" runat="server" CssClass="form-control" BackColor="#d3d3d3" OnSelectedIndexChanged="ddlLockerBlockName_SelectedIndexChanged" AutoPostBack="true">
                                                                    <asp:ListItem Value="0" Selected="True">--Select Block Name--</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:Label ID="lblValidLockerBlockName" runat="server" Style="color: red;" Text="* Select Block Name..." Visible="false"></asp:Label>
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
                                                            <label class="col-md-3 control-label">Floor Name : </label>
                                                            <div class="col-md-6">
                                                                <asp:DropDownList ID="ddllockerFloorName" runat="server" CssClass="form-control" BackColor="#d3d3d3" OnSelectedIndexChanged="ddllockerFloorName_SelectedIndexChanged" AutoPostBack="true">
                                                                    <asp:ListItem Value="0" Selected="True">--Select Floor Name--</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:Label ID="lblValidlockerFloorName" runat="server" Style="color: red;" Text="* Select Floor Name..." Visible="false"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-6 col-sm-6">
                                                <div class="portlet-body">
                                                    <div class="form-body">
                                                        <div class="form-group">
                                                            <label class="col-md-3 control-label">Room Number : </label>
                                                            <div class="col-md-6">
                                                                <asp:DropDownList ID="ddlLockerRoomNumber" runat="server" CssClass="form-control" BackColor="#d3d3d3" OnSelectedIndexChanged="ddlLockerRoomNumber_SelectedIndexChanged" AutoPostBack="true">
                                                                    <asp:ListItem Value="0" Selected="True">--Select Room Number--</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:Label ID="lblValidLockerRoomNumber" runat="server" Style="color: red;" Text="* Select Room Number..." Visible="false"></asp:Label>
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
                                                            <label class="col-md-3 control-label">Bed Number : </label>
                                                            <div class="col-md-6">
                                                                <asp:DropDownList ID="ddlBedNumber" runat="server" CssClass="form-control" BackColor="#d3d3d3">
                                                                     <asp:ListItem Value="0" Selected="True">--Select Bed Number--</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:Label ID="lblValidLockerBedNumber" runat="server" Style="color: red;" Text="* Select Bed Number..." Visible="false"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-6 col-sm-6">
                                                <div class="portlet-body">
                                                    <div class="form-body">
                                                        <div class="form-group">
                                                            <label class="col-md-3 control-label">Status : </label>
                                                            <div class="col-md-6">
                                                                <asp:DropDownList ID="ddlLockerStatus" runat="server" CssClass="form-control" BackColor="#d3d3d3">
                                                                    <asp:ListItem Value="0" Selected="True">--Select Status--</asp:ListItem>
                                                                    <asp:ListItem Value="1">Active</asp:ListItem>
                                                                    <asp:ListItem Value="2">Deactive</asp:ListItem>
                                                                    <asp:ListItem Value="3">Damage</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:Label ID="lblValidLockerStatus" runat="server" Style="color: red;" Text="* Select Status..." Visible="false"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="clearfix"></div>
                                            <br />
                                            <br />
                                            <br />
                                            <asp:LinkButton ID="lnkLockerSave" runat="server" Text="Save" class="btn green-meadow" OnClick="lnkLockerSave_Click" />
                                            <asp:LinkButton ID="lnkLockerUpdate" runat="server" Text="Update" class="btn green-meadow" OnClick="lnkLockerUpdate_Click" />
                                            <asp:LinkButton ID="lnkLockerBack" runat="server" Text="Back" class="btn green-meadow" OnClick="lnkLockerBack_Click" />
                                            <asp:LinkButton ID="lnkLockerClear" runat="server" Text="Clear" class="btn green-meadow" OnClick="lnkLockerClear_Click" />
                                        </div>
                                    </div>
                                    <div class="row" id="divgridLocker" visible="false" runat="server">
                                        <div class="col-md-12">
                                            <div class="portlet box green-meadow">
                                                <div class="portlet-title">
                                                    <div class="caption">
                                                        <i class="fa fa-lock"></i>Lockers
                                                    </div>
                                                    <div class="tools pull-left" style="margin-left: 10px; padding: 8px; border-left: 0px!important;">
                                                        <asp:TextBox ID="txtLockerSearch" placeHolder="Search" runat="server" Width="230px" Height="28px" Style="border: 0px; color: black" />
                                                    </div>
                                                    <div class="tools pull-left" style="margin-left: 5px; padding: 8px; border-left: 0px!important;">
                                                        <asp:LinkButton ID="lnkLockerSearch" runat="server" OnClick="lnkLockerSearch_Click">
                                        <i class="fa fa-search" title="Search"  style="font-size: 28px; color: white; margin-top: 4px;"></i>
                                                        </asp:LinkButton>
                                                    </div>
                                                    <div class="tools">
                                                        <a class="pull-right" style="color: white" href="Procter_System.aspx?LockerAdd=Add">
                                                            <i class="fa fa-plus" title="Add New Locker"></i></a>
                                                    </div>
                                                </div>

                                                <div class="portlet-body">
                                                    <asp:GridView ID="GridLocker" runat="server" class="table table-striped table-bordered table-hover" AutoGenerateColumns="false"
                                                        AllowPaging="true" AllowSorting="true" OnSorting="GridLocker_Sorting" OnPageIndexChanging="GridLocker_PageIndexChanging" PageSize="5"
                                                        ShowHeaderWhenEmpty="true" EmptyDataText="No records Found">
                                                        <PagerSettings Mode="NumericFirstLast" PageButtonCount="10" FirstPageText="First" LastPageText="Last" />
                                                        <PagerStyle CssClass="gridview"></PagerStyle>
                                                        <Columns>
                                                            <asp:TemplateField ItemStyle-Width="30px" HeaderText="Locker Number" SortExpression="Number">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblLockerNumber" runat="server" Text='<%#Eval("Number") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ItemStyle-Width="30px" HeaderText="Locker Name" SortExpression="Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblLockerName" runat="server" Text='<%#Eval("Name") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ItemStyle-Width="30px" HeaderText="Room Number" SortExpression="RoomID">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblRoomID" runat="server" Text='<%#Eval("RoomID") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ItemStyle-Width="30px" HeaderText="Bed Number" SortExpression="BedID">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblBedID" runat="server" Text='<%#Eval("BedID") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ItemStyle-Width="100px" HeaderText="Status" SortExpression="Status">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("Status") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ItemStyle-Width="100px" HeaderText="User Name" SortExpression="UserId" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblUserId" runat="server" Text='<%#Eval("UserId") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ItemStyle-Width="100px" HeaderText="CreatedDate" SortExpression="CreatedDate" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblCreatedDate" runat="server" Text='<%#Eval("CreatedDate","{0: MMM dd yyy hh:mm ss}") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ItemStyle-Width="100px" HeaderText="UpdatedDate" SortExpression="UpdatedDate" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblUpdatedDate" runat="server" Text='<%#Eval("UpdatedDate","{0: MMM dd yyy hh:mm ss}") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ItemStyle-Width="10px" HeaderText="Edit" SortExpression="Id">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkLockerEdit" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id")%>' OnClick="lnkLockerEdit_Click"><i class="material-icons" title="Edit row">&#xe3c9;</i></asp:LinkButton>
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


                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="lnkhostelsave" />
            <asp:PostBackTrigger ControlID="lnkhostelupdate" />
            <asp:PostBackTrigger ControlID="lnkBlocksave" />
            <asp:PostBackTrigger ControlID="lnkBlockupdate" />
            <asp:PostBackTrigger ControlID="lnkFloorsave" />
            <asp:PostBackTrigger ControlID="lnkFloorupdate" />
            <asp:PostBackTrigger ControlID="lnkRoomsave" />
            <asp:PostBackTrigger ControlID="lnkRoomupdate" />
            <asp:PostBackTrigger ControlID="lnkBedsave" />
            <asp:PostBackTrigger ControlID="lnkBedupdate" />
            <asp:PostBackTrigger ControlID="lnkLockerSave" />
            <asp:PostBackTrigger ControlID="lnkLockerupdate" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
