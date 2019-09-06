<%--<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Member.aspx.cs" Inherits="Committee.Forms.Member" %>--%>
<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/master.Master" AutoEventWireup="true" CodeBehind="~/Views/Forms/Member.aspx.cs" Inherits="Committee.Views.Forms.Member" %>




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
		
									<a href="Committe.aspx">الاعضاء</a>
							</li>
						<li class="active">
		
									<strong>إضافة عضو</strong>
							</li>
							</ol>
					
		<h2>إضافة عضو جديد</h2>
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
          <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-white" PostBackUrl="~/Views/Forms/MemberMangement.aspx" > الغاء</asp:LinkButton>

            </div>
								<div class="col-sm-1">
<%--                                    <asp:Button ID="btnAdd1" runat="server"  Text="حفظ" OnClick="btnAdd1_Click" ValidationGroup="a" CssClass="btn btn-success" />--%>
                                   <asp:LinkButton ID="btnAdd1" Font-Size="Medium" runat="server" Text="حفظ" OnClick="btnAdd1_Click" ValidationGroup="a" CssClass="btn btn-success">حفظ</asp:LinkButton>

								</div>

							</div>

					

							<div class="form-group">
								<label for="field-1" class="col-sm-3 control-label">الإسم :</label>
								
								<div class="col-sm-5">
									 <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="هذا الحقل مطلوب" Font-Size="Large" ValidationGroup="a" ForeColor="Red" ControlToValidate="txtMemberName"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="txtMemberName"  runat="server" CssClass="form-control" ValidationGroup="a"></asp:TextBox>
								</div>
							</div>
							
							<div class="form-group">
								<label for="field-2" class="col-sm-3 control-label">رقم الجوال :</label>
								
								<div class="col-sm-5">
									  <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="هذا الحقل مطلوب" Font-Size="Large" ValidationGroup="a" ForeColor="Red" ControlToValidate="txtPhoneNumber"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1"  ValidationGroup="a" runat="server" ControlToValidate="txtPhoneNumber" ErrorMessage="Invalid phone number" ValidationExpression="^(9665|9665|\+9665|05|5)(5|0|3|6|4|9|1|8|7)([0-9]{7})$" ForeColor="Red"></asp:RegularExpressionValidator>

                        <asp:TextBox ID="txtPhoneNumber" runat="server"  CssClass="form-control" ValidationGroup="a"></asp:TextBox>
								</div>
							</div>
                        <div class="form-group">
								<label for="field-1" class="col-sm-3 control-label">البريد الالكترونى : </label>
								
								<div class="col-sm-5">
									 <asp:RegularExpressionValidator ID="RegularExpressionValidator2"  ValidationGroup="a" runat="server" ControlToValidate="txtMemberEmail" ErrorMessage="Inalid Email" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ForeColor="Red"></asp:RegularExpressionValidator>

                        <asp:TextBox ID="txtMemberEmail" runat="server"  CssClass="form-control" ValidationGroup="a"></asp:TextBox>
								</div>
							</div>
                        <div class="form-group">
								<label for="field-1" class="col-sm-3 control-label">العنوان : </label>
								
								<div class="col-sm-5">

                     <asp:TextBox ID="txtAddress"  runat="server"  CssClass="form-control" ValidationGroup="a"></asp:TextBox>

								</div>
							</div>
                        <div class="form-group">
								<label for="field-1" class="col-sm-3 control-label">الوظيفة : </label>
								
								<div class="col-sm-5">

                     <asp:TextBox ID="txtMemberJob"  runat="server"  CssClass="form-control" ValidationGroup="a"></asp:TextBox>	

								</div>
							</div>
                        <div class="form-group">
								<label for="field-ta" class="col-sm-3 control-label">جهة العمل :</label>
								
								<div class="col-sm-5">
                  <asp:TextBox ID="txtWorkSide" runat="server" TextMode="MultiLine"  CssClass="form-control" ></asp:TextBox>
								</div>
							</div>
                            	<div class="form-group">
								<label class="col-sm-3 control-label">نوع العضو :</label>
								
								<div class="col-sm-5">
									 <asp:DropDownList ID="ddlMemberType" runat="server" CssClass="form-control">
                            <asp:ListItem Value="0">إختر من القائمة</asp:ListItem>
                            <asp:ListItem Value="ذكر">ذكر</asp:ListItem>
                            <asp:ListItem Value="أنثى">أنثى</asp:ListItem>
                        </asp:DropDownList>
								</div>
							</div>
                        <div class="form-group">
								<label class="col-sm-3 control-label">الدور :</label>
								
								<div class="col-sm-5">
                                      <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
									<asp:DropDownList ID="ddlMemberRole" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlMemberRole_SelectedIndexChanged">
                                         <asp:ListItem Value="6">عضو</asp:ListItem>
                            <asp:ListItem Value="1">مدير نظام</asp:ListItem>
                            <asp:ListItem Value="2">سكرتير نظام</asp:ListItem>
                            <asp:ListItem Value="3">مستخدم</asp:ListItem>
<asp:ListItem Value="4">مدير إدارة</asp:ListItem>
                                         <asp:ListItem Value="5">مدير عام</asp:ListItem>
                        </asp:DropDownList>
                </ContentTemplate></asp:UpdatePanel>
								</div>
							</div>
                            <div class="form-group" visible="false" id="divUserNameandpass" runat="server">
                             
                                <div class="col-sm-5">
                <asp:Label ID="Label4" class="col-sm-3 control-label" runat="server" Text="اسم المستخدم:"></asp:Label>

                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*" ValidationGroup="a" ForeColor="Red" ControlToValidate="txtUserName"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="txtUserName" runat="server" Font-Size="Medium" style="direction:ltr" CssClass="form-control" ValidationGroup="a"></asp:TextBox>
                                </div>
                                <div class="col-sm-5">
                          <asp:Label ID="Label5" class="col-sm-3 control-label" runat="server" Text="الرقم السري:"></asp:Label>

                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*" ValidationGroup="a" ForeColor="Red" ControlToValidate="txtUserName"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="txtPass" runat="server" Font-Size="Medium" style="direction:ltr" CssClass="form-control" ValidationGroup="a" TextMode="Password"></asp:TextBox>
                                </div>
                </div>
                        <div class="form-group" visible="false" id="divDept" runat="server">
								<label class="col-sm-3 control-label">الإدارة :</label>
								
								<div class="col-sm-5">
									<asp:DropDownList ID="ddlCommitteeDept"  runat="server" CssClass="form-control">
                           
                        </asp:DropDownList>
								</div>
							</div>
                         
                        <div class="form-group">
								<label class="col-sm-3 control-label">الصورة : </label>
								
								<div class="col-sm-5">
							<asp:FileUpload ID="ImgUpload" runat="server"  CssClass="form-control file2 inline btn btn-primary" AllowMultiple="false" data-label="<i class='glyphicon glyphicon-circle-arrow-up'></i> &nbsp;Browse Files" />
                            <asp:Image ID="ImgUser" runat="server" Visible="false" Width="200px" Height="200px" />
								</div>
							</div>
                        
                   
                               
							</div>
                            

	</div>
                </div>
            
            <div class="form-group">
                         <div class="col-sm-5">
                             </div>
                        
                    </div>
		</div>
         
             </div> 
            </ContentTemplate>
        </asp:UpdatePanel>
          </form>
       
</asp:Content>

