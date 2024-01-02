<%@ Page Title="" Language="C#" MasterPageFile="~/Admin_Master.Master" AutoEventWireup="true" CodeBehind="SMS_Food_Video_Master.aspx.cs" Inherits="SMS.SMS_Food_Video_Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Canteen Videos</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <br />
    <div class="page-content-wrapper" style="margin-bottom: -16px;">
        <div class="page-content">
            <h3 class="page-title bold">Canteen Videos</h3>
            <div class="row" id="divgrid" runat="server">
                <div class="col-md-12">
                    <div class="portlet box green-meadow">
                        <div class="portlet-title">
                            <div class="caption">
                                <i class='fas'>&#xf03d;</i>Canteen Videos List
                            </div>
                            <div class="tools pull-left" style="margin-left: 10px; padding: 8px;border-left:0px!important;">
                                <asp:TextBox ID="txtSearch" runat="server" Width="230px" Height="28px" Style="border: 0px; color: black" />
                            </div>
                            <div class="tools pull-left" style="margin-left: 5px; padding: 8px;border-left:0px!important;">
                                <asp:LinkButton ID="btnsearch" runat="server" OnClick="btnsearch_Click">
                                        <i class="fa fa-search" title="Search" style="font-size: 28px; color: white; margin-top: 4px;"></i>
                                </asp:LinkButton>
                            </div>
                            <div class="tools">
                                <asp:LinkButton ID="lnkAdd" runat="server" class="pull-right" Style="color: white" OnClick="lnkAdd_Click">
                                              <i class="fa fa-plus" title="Add New Canteen Video"></i>
                                </asp:LinkButton>
                            </div>
                        </div>
                        <div class="portlet-body" style="overflow: scroll">
                            <asp:GridView ID="gridEmployee" runat="server" class="table table-striped table-bordered table-hover" AutoGenerateColumns="false"
                                AllowPaging="true" AllowSorting="true" OnSorting="OnSorting" OnPageIndexChanging="OnPageIndexChanging" PageSize="10"
                                ShowHeaderWhenEmpty="true" EmptyDataText="No records Found">
                                <PagerSettings Mode="NumericFirstLast" PageButtonCount="10" FirstPageText="First" LastPageText="Last" />
                                <PagerStyle CssClass="gridview"></PagerStyle>
                                <Columns>
                                    <asp:TemplateField ItemStyle-Width="20px" HeaderText="Images">
                                        <ItemTemplate>
                                            <video id="VideoPlayer" src='<%#Eval("VideoPath")==""?"~/Images/images1.jpg":Eval("VideoPath1") %>' controls="true" class="circle" loop="true">
                                            </video>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="30px" HeaderText="Video Name" SortExpression="Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblApplication_No" runat="server" Text='<%#Eval("VideoName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="30px" HeaderText="Video Description" SortExpression="CollegeId">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDescription" runat="server" Text='<%#Eval("VideoDescription") %>'></asp:Label>
                                            <asp:Label ID="lblid" runat="server" Text='<%#Eval("VideoId") %>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="30px" HeaderText="Canteen Name" SortExpression="Id">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCanteen" runat="server" Text='<%#Eval("CanteenId") %>'></asp:Label>
                                            <asp:Label ID="lblId1" runat="server" Visible="false" Text='<%#Eval("VideoId") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="30px" HeaderText="Session Name" SortExpression="Id">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSession" runat="server" Text='<%#Eval("SessionId") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField ItemStyle-Width="10px" HeaderText="Edit">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="linkEdit" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "VideoId")%>' OnClick="linkEdit_Click"><i class="material-icons" title="Edit row">&#xe3c9;</i></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="10px" HeaderText="Delete">
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
                                <label class="text-success">Canteen Name : </label>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="description" style="width: 100%; border: 1px solid #ddd; padding: 10px; border-radius: 5px">
                                            <div class="row">
                                                <div class="col-md-12" id="Div2">
                                                    <div class="input-group">
                                                        <asp:DropDownList ID="ddlcanteen" runat="server" CssClass="form-control">
                                                        </asp:DropDownList>
                                                        <asp:Label ID="lblValidCanteenName" runat="server" Style="color: red;" Text="* Enter Canteen name..." Visible="false"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="text-success">Session Name : </label>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="description" style="width: 100%; border: 1px solid #ddd; padding: 10px; border-radius: 5px">
                                            <div class="row">
                                                <div class="col-md-12" id="Div2">
                                                    <div class="input-group">
                                                        <asp:DropDownList ID="ddlsession" runat="server" CssClass="form-control">
                                                        </asp:DropDownList>
                                                        <asp:Label ID="lblValidSessionName" runat="server" Style="color: red;" Text="* Enter Session name..." Visible="false"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="text-success">Video Name : </label>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="description" style="width: 100%; border: 1px solid #ddd; padding: 10px; border-radius: 5px">
                                            <div class="row">
                                                <div class="col-md-12" id="Div1">
                                                    <div class="input-group">
                                                        <asp:TextBox ID="txtVideoName" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <asp:Label ID="lblValidVideoname" runat="server" Style="color: red;" Text="* Enter Video name..." Visible="false"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>


                            <div class="form-group">
                                <label class="text-success">Video Description : </label>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="description" style="width: 100%; border: 1px solid #ddd; padding: 10px; border-radius: 5px">
                                            <div class="row">
                                                <div class="col-md-12" id="Div2">
                                                    <div class="input-group">
                                                        <asp:TextBox ID="txtVideoDescription" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>


                            <div class="form-group">
                                <label class="text-success">Video Image : </label>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="description" style="width: 100%; border: 1px solid #ddd; padding: 10px; border-radius: 5px">
                                            <div class="row">
                                                <div class="col-md-12" id="Div2">

                                                    <video id="VideoPlayer" runat="server" poster="Video/Youtube.jpg" controls="true" height="120px" width="200px" loop="true" />
                                                    <%-- <asp:Image ID="image2" runat="server" Height="120px" Width="200px" ImageUrl="~/Images/images1.jpg" />--%>
                                                    <div class="input-group">
                                                        <asp:FileUpload ID="FileUpload1" runat="server" Style="color: blue" /><asp:Label ID="Label1" runat="server" Style="color: red;" Text="* Note : video should be upload like mp4, mkv, 3gp, fav etc, " Visible="false"></asp:Label>
                                                        <asp:Label ID="lblFoodImages" runat="server" Style="color: red;" Text="* upload Canteen Video..." Visible="false"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>


                            <div class="form-group">
                                <label class="text-success">Video Status : </label>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="description" style="width: 100%; border: 1px solid #ddd; padding: 10px; border-radius: 5px">
                                            <div class="row">
                                                <div class="col-md-1" id="Div2">
                                                    <div class="input-group" style="margin-left: 25px;">
                                                        <%--<asp:CheckBox ID="chkQuoteStatus" Style="margin-left:17px !important" runat="server" Text="Quote Status" CssClass="checkbox" />--%>
                                                        <asp:RadioButton ID="rbshow" Text="show" runat="server" GroupName="FoodStatus" />

                                                    </div>
                                                </div>
                                                <div class="col-md-10" id="Div2">
                                                    <div class="input-group" style="margin-left: 25px;">
                                                        <asp:RadioButton ID="rbside" Text="hide" runat="server" GroupName="FoodStatus" />
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
    <%-- </ContentTemplate>
        <Triggers>
        </Triggers>
         </asp:UpdatePanel>--%>
</asp:Content>
