﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/master.Master" AutoEventWireup="true" CodeBehind="MeetingMangement.aspx.cs" Inherits="Committee.Views.Forms.MeetingMangement" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
       <form role="form" class="form-horizontal form-groups-bordered" runat="server" id="f1">
    <div class="form-group">
								<%--<label for="field-1" class="col-sm-3 control-label">حفظ :</label>--%>
        <div class="col-sm-3">
							 <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
								<div class="col-sm-3">
                                    <asp:Button ID="btnSearch" Font-Size="Medium" runat="server" Text="بحث"  CssClass="btn btn-success" OnClick="btnSearch_Click"/>

								</div>
         <div class="col-sm-4">
            </div>
          <div class="col-sm-1">
          <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-white" PostBackUrl="~/Views/Forms/Dashboard.aspx" > الغاء</asp:LinkButton>

            </div>
								<div class="col-sm-1">
                                   <asp:LinkButton ID="btnaddMeeting" runat="server" CssClass="btn btn-blue" OnClick="btnaddMeeting_Click" >اضافة اجتماع جديد</asp:LinkButton>

								</div>

							</div>
    <div class="form-group">
                        <div class="col-sm-5">
                                 <asp:GridView ID="gvMeeting" runat="server" CssClass="table-striped table" AllowPaging="True" AllowSorting="True" AutoGenerateDeleteButton="True" AutoGenerateEditButton="True" AutoGenerateSelectButton="True" CellPadding="4" ForeColor="Black" OnRowDataBound="gvMeeting_RowDataBound" OnRowDeleting="gvMeeting_RowDeleting" OnSelectedIndexChanged="gvMeeting_SelectedIndexChanged" OnPageIndexChanging="gvMeeting_PageIndexChanging" OnRowEditing="gvMeeting_RowEditing" >
                                        <FooterStyle BackColor="#CCCCCC" />
                                        <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Center" />
                                        <RowStyle BackColor="White" />
                                        <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                        <SortedAscendingHeaderStyle BackColor="#808080" />
                                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                        <SortedDescendingHeaderStyle BackColor="#383838" /> 
                   </asp:GridView> 
                        </div>

                    </div>
        </form>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
