<%--<%@ Page Title="" Language="C#" MasterPageFile="~/html/neon/master.Master" AutoEventWireup="true" CodeBehind="Notification.aspx.cs" Inherits="Committee.Forms.WebForm1" %>--%>
<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/master.Master" AutoEventWireup="true" CodeBehind="~/Views/Forms/Notification.aspx.cs" Inherits="Committee.Views.Forms.Notification" %>
<asp:Content ID="ContentPlaceHolder1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

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
		
									<a href="Committe.aspx">التنبيهات</a>
							</li>
						<li class="active">
		
									<strong> ارسال تنبيه</strong>
							</li>
							</ol>
					
		<h2>ارسال تنبيه جديد</h2>
		<br />
		
		
		<div class="row col-md-12">
			<div class="col-md-12">
				
				<div class="panel panel-primary" data-collapsed="0">
				
					<div class="panel-heading">
						<div class="panel-title">
							تنبيه
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
								<label for="field-1" class="col-sm-3 control-label">اللجنة المختصة :</label>
								
								<div class="col-sm-5">
										 <asp:DropDownList ID="ddlCommitteeSpecified" Font-Size="Medium" runat="server" CssClass="form-control">
                            <asp:ListItem Value="NULL">إختر من القائمة</asp:ListItem>
                        </asp:DropDownList>
								</div>
							</div>
								<div class="form-group">
								<label for="field-1" class="col-sm-3 control-label">نص الرسالة :</label>
								
								<div class="col-sm-5">
							 <asp:TextBox ID="textMessage" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>

								</div>
							</div>
							<div class="form-group">
								<label for="field-2" class="col-sm-3 control-label">ارسال عن طريق :</label>
								
								<div class="col-sm-offset-3 col-sm-5 center-block">
                                       <asp:CheckBoxList ID="ChkSentWay" runat="server">
                                <asp:ListItem Value="1">بريد إلكترونى</asp:ListItem>
                                <asp:ListItem Value="2">رسالة نصية قصيرة</asp:ListItem>
                                <asp:ListItem Value="3">تنبيه على تطبيق الجوال</asp:ListItem>
                       </asp:CheckBoxList>
									<div class="checkbox">
									</div>
								</div>
							</div>
                      
                        
                   
                               
							
                     <div class="form-group">
                  <div class="col-sm-3 control-label">
                 <asp:Button ID="btnSend" runat="server" Font-Size="Medium" CssClass="btn btn-green" Text="ارسال" OnClick="btnSend_Click"  />

                  </div>
                  <div class="col-sm-3 control-label">
                 <asp:Button ID="btnCancel" runat="server" Font-Size="Medium" CssClass="btn btn-blue" Text="الغاء"/>

                  </div>
                 </div>
                    </div>
                            

	</div>
                </div>
            
            
		</div>
         
             </div> 
            </ContentTemplate>
        </asp:UpdatePanel>
     
                                
    </asp:Content>