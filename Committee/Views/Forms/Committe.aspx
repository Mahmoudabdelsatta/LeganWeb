<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/master.Master" AutoEventWireup="true" CodeBehind="Committe.aspx.cs" Inherits="Committee.Views.Forms.Committe" %>


<asp:Content ID="ContentPlaceHolder1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

	<form role="form" class="form-horizontal form-groups-bordered" runat="server" id="f1">
    			  <asp:ScriptManager ID="ScriptManager1" EnableCdn="False" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePaneltypeOfservice" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
    <div class="main-content col-md-12">
	
		
		<hr />
		
					<ol class="breadcrumb bc-3" >
								<li>
						<a href="Dashboard.aspx"><i class="fa-home"></i>الشاشات</a>
					</li>
							<li>
		
									<a href="Committe.aspx">اللجان</a>
							</li>
						<li class="active">
		
									<strong>إضافة لجنة</strong>
							</li>
							</ol>
					
		<h2>إضافة لجنة جديده</h2>
		<br />
		
		
		<div class="row col-md-12">
			<div class="col-md-12">
				
				<div class="panel panel-primary" data-collapsed="0">
				
					<div class="panel-heading">
						<div class="panel-title">
							لجنة
						</div>
						
						<div class="panel-options">
							<a href="#sample-modal" data-toggle="modal" data-target="#sample-modal-dialog-1" class="bg"><i class="entypo-cog"></i></a>
							<a href="#" data-rel="collapse"><i class="entypo-down-open"></i></a>
							<a href="#" data-rel="reload"><i class="entypo-arrows-ccw"></i></a>
							<a href="#" data-rel="close"><i class="entypo-cancel"></i></a>
						</div>
					</div>
					
					<div class="panel-body">
						
					<div class="form-group">
								<%--<label for="field-1" class="col-sm-3 control-label">حفظ :</label>--%>
      <div class="col-sm-4">

      </div>
         <div class="col-sm-4">
            </div>
          <div class="col-sm-1">
          <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-white" PostBackUrl="~/Views/Forms/CommitteeMangement.aspx" > الغاء</asp:LinkButton>

            </div>
								<div class="col-sm-1">
                                   <asp:LinkButton D="btnAdd1" Font-Size="Medium" runat="server" Text="حفظ" OnClick="btnAdd1_Click" ValidationGroup="a" CssClass="btn btn-success" ID="btnAdd1" OnCommand="btnAdd1_Command">حفظ</asp:LinkButton>

								</div>

							</div>

							<div class="form-group">
								<label for="field-1" class="col-sm-3 control-label">اسم اللجنة :</label>
								
								<div class="col-sm-5">
									 <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="هذا الحقل مطلوب" Font-Size="Large" ValidationGroup="a" ForeColor="Red" ControlToValidate="txtCommitteeName" Display="Dynamic"></asp:RequiredFieldValidator>
                                    <asp:TextBox ID="txtCommitteeName"  runat="server" placeholder="اسم اللجنه" CssClass="form-control" ValidationGroup="a" OnTextChanged="txtCommitteeName_TextChanged"></asp:TextBox>
								</div>
							</div>
							
							<div class="form-group">
								<label for="field-2" class="col-sm-3 control-label">تاريخ اللجنة :</label>
								
								<div class="col-sm-5">
									  <asp:RequiredFieldValidator ID="RequiredFieldValidator55" runat="server" ErrorMessage="هذا الحقل مطلوب" Font-Size="Large" ValidationGroup="a" ForeColor="Red" ControlToValidate="txtCommitteeDate" Display="Dynamic"></asp:RequiredFieldValidator>

                                    <asp:TextBox ID="txtCommitteeDate" placeholder="تاريخ اللجنه"  runat="server" ValidationGroup="a"  CssClass="form-control" OnTextChanged="txtCommitteeDate_TextChanged"></asp:TextBox>
								</div>
							</div>
                            	<div class="form-group">
								<label class="col-sm-3 control-label">الإدارة :</label>
								
								<div class="col-sm-5">
									 <asp:DropDownList ID="ddlCommitteeDept" runat="server" CssClass="form-control">
                                    </asp:DropDownList>
								</div>
							</div>
                            <div class="form-group">
								<label class="col-sm-3 control-label">تصنيف اللجنة : </label>
								
								<div class="col-sm-5">
									 <asp:DropDownList  ID="ddlCommitteeClassification" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="1">عسكرية</asp:ListItem>
                                        <asp:ListItem Value="2">مدنية</asp:ListItem>
                                    </asp:DropDownList>
								</div>
							</div>
                                  <div class="form-group">
								<label class="col-sm-3 control-label">حال اللجنة : </label>
								
								<div class="col-sm-5">
									 <asp:DropDownList Style="text-align: right" ID="ddlcommitteeStatus" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="1">نشطة</asp:ListItem>
                                        <asp:ListItem Value="2">غير نشطة</asp:ListItem>
                                    </asp:DropDownList>
								</div>
							</div>
                               <div class="form-group">
								<label class="col-sm-3 control-label">مستوى الأهمية :</label>
								
								<div class="col-sm-5">
									   <asp:DropDownList ID="ddlCommitteeImportancy"  runat="server" CssClass="form-control">
                                        <asp:ListItem Value="1">عادية</asp:ListItem>
                                        <asp:ListItem Value="2">هامة</asp:ListItem>
                                    </asp:DropDownList>
								</div>
							</div>
							<div class="form-group">
								<asp:Label ID="LabeddlCommitteeSecrtary" runat="server" CssClass="col-sm-3 control-label"> سكرتير اللجنة :</asp:Label>
								
								<div class="col-sm-5">
									    <asp:DropDownList ID="ddlCommitteeSecrtary"  runat="server" CssClass="form-control">
                                    </asp:DropDownList>
								</div>
							</div>
                            <div class="form-group">
								<asp:Label ID="LabelddlCommitteepresident" runat="server" CssClass="col-sm-3 control-label" > رئيس اللجنة :</asp:Label>
								
								<div class="col-sm-5">
									    <asp:DropDownList ID="ddlCommitteepresident"  runat="server" CssClass="form-control">
                                    </asp:DropDownList>
								</div>
							</div>
                            <div class="form-group">
								<label for="field-ta" class="col-sm-3 control-label">موضوع اللجنة :</label>
								
								<div class="col-sm-5">
                                    <asp:TextBox ID="txtCommitteeTopic" runat="server"  TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
								</div>
							</div>
                            <div class="form-group">
								<label for="field-ta" class="col-sm-3 control-label">الأمر المستند عليه : </label>
								
								<div class="col-sm-5">
                                    <asp:TextBox ID="txtCommitteeBasedON"  runat="server"  TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
								</div>
							</div>
                            <div class="form-group">
								<label for="field-ta" class="col-sm-3 control-label">جهة الوارد : </label>
								
								<div class="col-sm-5">
                                    <asp:TextBox ID="txtInboxSide" runat="server"  TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
								</div>
							</div>
                            <div class="form-group">
								<label for="field-1" class="col-sm-3 control-label">سنة القيد : </label>
								
								<div class="col-sm-5">
									  <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="هذا الحقل مطلوب" Font-Size="Large" ValidationGroup="a" ForeColor="Red" ControlToValidate="txtEnrollmentDate"></asp:RequiredFieldValidator>
                                    <asp:TextBox ID="txtEnrollmentDate" runat="server" CssClass="form-control" ValidationGroup="a"></asp:TextBox>
								</div>
							</div>
                            <div class="form-group">
								<label for="field-1" class="col-sm-3 control-label">رقم القيد : </label>
								
								<div class="col-sm-5">
									      <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="هذا الحقل مطلوب" Font-Size="Large" ValidationGroup="a" ForeColor="Red" ControlToValidate="txtEnrollmentNumber"></asp:RequiredFieldValidator>
                                    <asp:TextBox ID="txtEnrollmentNumber" runat="server" CssClass="form-control" ValidationGroup="a"></asp:TextBox>
								</div>
							</div>
                             <div class="form-group" id="divAddMembers" runat="server" visible="false">
                                <div class="col-sm-5">
                                    <label for="field-ta" class="col-sm-3 control-label">اضافة أعضاء اللجنة</label>

                                    <asp:DropDownList ID="ddlMemberSelect"  runat="server" CssClass="form-control">
                                    </asp:DropDownList>
                                    <asp:Button ID="btnAdd" runat="server" Font-Size="Medium" Text="إضافة" CssClass="btn-green btn" OnClick="btnAdd_Click" />
                                </div>
                            </div>
                               <div id="divAddMember" class="form-group" runat="server" visible="false">

                    <label for="field-3" class="col-sm-3 control-label" >اضافة عضو للجنه</label>

                    <div class="col-sm-5">

                        <asp:DropDownList ID="ddlMemberChange" runat="server" CssClass="form-control">
                        </asp:DropDownList>
                        <asp:Button ID="btnAddChange" runat="server" Font-Size="Medium" Text="إضافة" CssClass="btn-green btn" OnClick="btnAddChange_Click" />

                    </div>
                    <div class="col-sm-5">
                    </div>
                </div>

	</div>
                </div></div>
         <div class="form-group col-md-10">
                                </div>

                            </div>
                        </div>
                    </div>
              <div class="row">

                        <div class="col-md-4">
                        </div>
                        <div class="col-sm-5">
                        </div>
                    </div>
                    
                    <div runat="server" class="form-group" id="divMembers">
                        <asp:GridView ID="gvMembersOfCommittee" runat="server" AutoGenerateDeleteButton="True" CssClass="table table-striped" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" OnDataBound="gvMembersOfCommittee_DataBound" OnRowDataBound="gvMembersOfCommittee_RowDataBound" OnRowDeleting="gvMembersOfCommittee_RowDeleting" AllowPaging="True" OnPageIndexChanging="gvMembersOfCommittee_PageIndexChanging" OnRowUpdating="gvMembersOfCommittee_RowUpdating">
                            <AlternatingRowStyle BackColor="#CCCCCC" />
                            <FooterStyle BackColor="#CCCCCC" />
                            <HeaderStyle Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                            <SortedAscendingCellStyle BackColor="#F1F1F1" />
                            <SortedAscendingHeaderStyle BackColor="#808080" />
                            <SortedDescendingCellStyle BackColor="#CAC9C9" />
                            <SortedDescendingHeaderStyle BackColor="#383838" />
                        </asp:GridView>
                    </div>
               </div> </div>
    
            </ContentTemplate>
        </asp:UpdatePanel>
          </form>
       <script type="text/javascript">
                    jQuery(document).ready(function ($) {

                        $('#<%= txtCommitteeDate.ClientID %>').calendarsPicker({
                            calendar: $.calendars.instance('islamic','ar'),
                            monthsToShow: [1, 1],
                            showOtherMonths: true,
                        });
                        $('#<%= RequiredFieldValidator55.ClientID %>').hide();
                    });
                </script>
</asp:Content>


