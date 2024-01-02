<%@ Page Title="" Language="C#" MasterPageFile="~/Admin_Master.Master" AutoEventWireup="true" CodeBehind="SMS_Upload_label_File.aspx.cs" Inherits="SMS.SMS_Upload_label_File" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Upload lable Files</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <br />
    <div class="page-content-wrapper" style="margin-bottom: -16px;">
        <div class="page-content">
            <h3 class="page-title bold">Upload Badge Files</h3>
            <div class="modal-content" runat="server">
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
                                <a class="nav-link" data-toggle="tab" href="#ContentPlaceHolder1_divPersonalDetails" id="aPersonalDetails" runat="server">Upload Badge Files</a>
                            </li>
                        </ul>

                        <!-- Tab panes -->
                        <div class="tab-content">
                            <div class="tab-pane container active" id="divPersonalDetails" runat="server">
                                <div class="row ">
                                    <div class="col-md-6 col-sm-6">
                                        <div class="portlet-body">
                                            <div class="form-body">
                                                <div class="form-group">
                                                    <label class="col-md-3 control-label">Upload Student Badge File : </label>
                                                    <div class="col-md-6">
                                                        <label class="btn btn-default">
                                                            <asp:FileUpload ID="fstudent" runat="server" Style="color: blue" />
                                                            <asp:Label ID="lblstudenterror" runat="server" Style="color:red;" Text="*Invalid File extension" Visible="false"></asp:Label>
                                                        </label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-6 col-sm-6">
                                        <div class="portlet-body">
                                            <div class="form-body">
                                                <div class="form-group">
                                                    <label class="col-md-3 control-label">Upload Visitor Badge File : </label>
                                                    <div class="col-md-6">
                                                        <label class="btn btn-default">
                                                            <asp:FileUpload ID="fvisitor" runat="server" Style="color: blue" />
                                                            <asp:Label ID="lblvisitorerror" runat="server" Style="color:red;" Text="*Invalid File extension" Visible="false"></asp:Label>
                                                        </label>
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
                                                    <div class="col-md-12">
                                                        <asp:LinkButton ID="lnkupload" runat="server" Text="Click here to upload files" class="btn green-meadow" OnClick="lnkupload_Click" />
                                                        
                                                        <asp:Label ID="lblsuccess" runat="server" Style="color:green;" Text="*Upload successfully" Visible="false"></asp:Label>
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
                                                    <div class="col-md-12">
                                                       <span style="color:red;font-size:larger;">Note : only upload .label file</span>
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
