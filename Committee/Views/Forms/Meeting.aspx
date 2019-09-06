
<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/master.Master" AutoEventWireup="true" CodeBehind="~/Views/Forms/Meeting.aspx.cs" EnableSessionState="True"  Inherits="Committee.Views.Forms.Meeting" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="ContentPlaceHolder1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <form id="form1" runat="server" role="form" class="form-horizontal form-groups-bordered">
      <asp:ScriptManager ID="ScriptManager2" EnableCdn="False" runat="server"></asp:ScriptManager>
     
  
              <%--  <asp:UpdatePanel ID="UpdatePaneltypeOfservice" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>--%>
   <div class="main-content col-md-12">
	
		
		<hr />
		
					<ol class="breadcrumb bc-3" >
								<li>
						<a href="Dashboard.aspx"><i class="fa-home"></i>الشاشات</a>
					</li>
							<li>
		
									<a href="Committe.aspx">الاجتماعات</a>
							</li>
						<li class="active">
		
									<strong>إضافة اجتماع</strong>
							</li>
							</ol>
					
		<h2>إضافة اجتماع جديد</h2>
		<br />
       	<div class="row col-md-12">
			<div class="col-md-12">
				
				<div class="panel panel-primary" data-collapsed="0">
                    	<div class="panel-heading">

                            <div class="panel-title">
							اجتماع
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
          <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-white" PostBackUrl="~/Views/Forms/MeetingMangement.aspx" > الغاء</asp:LinkButton>

            </div>
								<div class="col-sm-1">
<%--                                    <asp:Button ID="btnAdd1" runat="server"  Text="حفظ" OnClick="btnAdd1_Click" ValidationGroup="a" CssClass="btn btn-success" />--%>
                                   <asp:LinkButton ID="btnAdd1" Font-Size="Medium" runat="server" Text="حفظ" OnClick="btnAdd1_Click" ValidationGroup="a" CssClass="btn btn-success">حفظ</asp:LinkButton>

								</div>

							</div>
							<div class="form-group">
								<label for="field-1" class="col-sm-3 control-label">عنوان الاجتماع :</label>
								
								<div class="col-sm-5">
									<asp:RequiredFieldValidator ID="RequiredFieldValidator2" Font-Size="Medium" runat="server" ErrorMessage="هذا الحقل مطلوب" ValidationGroup="a" ForeColor="Red" ControlToValidate="txtMeetingName"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="txtMeetingName"  runat="server"  CssClass="form-control" ValidationGroup="a"></asp:TextBox>

								</div>
							</div>
                        <div class="form-group">
								<label for="field-2" class="col-sm-3 control-label">تاريخ الاجتماع :</label>
								
								<div class="col-sm-5">
				 <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Font-Size="Medium" ErrorMessage="هذا الحقل مطلوب" ValidationGroup="a" ForeColor="Red" ControlToValidate="txtMeetingDate" Display="Dynamic"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="txtMeetingDate" runat="server" ValidationGroup="a"  class="form-control" ></asp:TextBox>
								</div>
							</div>
                        <div class="form-group">
								<label for="field-1" class="col-sm-3 control-label">وقت الاجتماع :</label>
								
								<div class="col-sm-5">
					 <asp:TextBox ID="txtMeetingTime" runat="server" CssClass="form-control" ValidationGroup="a"></asp:TextBox>


								</div>
							</div>
                        <div class="form-group">
								<label for="field-1" class="col-sm-3 control-label">حالة الاجتماع :</label>
								
								<div class="col-sm-5">
								<asp:TextBox ID="txtStatus"  runat="server"  CssClass="form-control" ValidationGroup="a"></asp:TextBox>


								</div>
							</div>
                        <div class="form-group">
								<label for="field-ta" class="col-sm-3 control-label">وصف الاجتماع: </label>
								
								<div class="col-sm-5">
                                 <asp:TextBox ID="txtMeetingTopic" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
								</div>
							</div>
                        <div class="form-group">
								<asp:Label ID="LabeddlCommitteeSecrtary" runat="server" CssClass="col-sm-3 control-label"> اللجنة المختصة :</asp:Label>
								
								<div class="col-sm-5">
									     <asp:DropDownList ID="ddlCommitteeSpecified" runat="server" CssClass="form-control">
                            <asp:ListItem Value="NULL">إختر من القائمة</asp:ListItem>
                        </asp:DropDownList>
								</div>
							</div>
                        <div class="form-group">
								<label class="col-sm-3 control-label">إضافة أجندة : </label>
								
								<div class="col-sm-5">
                                      <asp:Button ID="btnOpenPopUp"  Font-Size="Medium"  class="btn-success" runat="server" text="إضافة أجندة" OnClick="btnOpenPopUp_Click1" />

								</div>
							</div>
                        <div class="form-group">
								<label for="field-1" class="col-sm-3 control-label">موقع الاجتماع :</label>
								
								<div class="col-sm-5">
								<asp:TextBox ID="txtMeetingLocation"   runat="server"  CssClass="form-control" ValidationGroup="a"></asp:TextBox>
                                    <div class="col-sm-5">  <button id="linkMap" onclick="ShowMap()" style="font-size:medium" type="button">
                                 <img src="../../MasterPage/img/download (7).jpg" width="40px" height="40px"></button> 
                                 <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                                 </ContentTemplate></asp:UpdatePanel>
                                 <div id="dvMap" style="width: 300px; height: 300px">
   </div> 

								</div>
							</div>
                    </div>
                              <div class="form-group">
								<label class="col-sm-3 control-label">محضر الاجتماع : </label>
								
								<div class="col-sm-5">
									<asp:FileUpload ID="MinutesOfMeetingUpload" runat="server"  CssClass="form-control file2 inline btn btn-primary" AllowMultiple="false" data-label="<i class='glyphicon glyphicon-circle-arrow-up'></i> &nbsp;اختار محضر اجتماع " />

								</div>
							</div>
                               <div class="form-group">
               
								<div class="col-sm-5">
                <asp:HiddenField ID="lat" runat="server" />
								<%--<asp:TextBox ID="lat" runat="server" style="direction:ltr" CssClass="form-control" ValidationGroup="a" Visible="false"></asp:TextBox>--%>
								</div>
                <div class="col-sm-5">
                     <asp:HiddenField ID="lng" runat="server" />
<%--			<asp:TextBox ID="lng" runat="server" style="direction:ltr" CssClass="form-control" ValidationGroup="a" Visible="false"></asp:TextBox>--%>
								</div>
							</div>
                    <div class="form-group">
               
								<div class="col-sm-5">
     <asp:Label ID="lblHidden" runat="server" Text=""></asp:Label>
        <ajaxToolkit:ModalPopupExtender ID="mpePopUp" runat="server" TargetControlID="lblHidden" PopupControlID="divPopUp" BackgroundCssClass="modalBackground"></ajaxToolkit:ModalPopupExtender>


                                    </div>
                 <div class="col-sm-5"></div>
                 </div>
                
                                <div class="col-md-4" style="text-align: right">

            

         

</div>
</div></div>
       </div>

             
         <%--   <asp:LinkButton ID="linkAgenda" runat="server" CssClass="link user-link">Agenda</asp:LinkButton>--%>
       
           
<div id="divPopUp" class="panel-danger table-bordered "  style="background-color:darkgray;table-layout:fixed;width:800px;height:260px">
<div class="form-group" id="divAgendaText">
							<%--	<label class="col-sm-3 control-label" style="color:black"> اجندة الاجتماع</label>--%>
     <asp:Label ID="Label16" CssClass="col-sm-3 control-label"  Font-Size="Medium" Font-Bold="true" ForeColor="Black" runat="server" Text="اجندة الاجتماع"></asp:Label>
								
							
								<asp:TextBox ID="txtMeetingAgenda" runat="server" Width="500px" Height="150px" TextMode="MultiLine" CssClass="form-control" onKeyup="onTextBox1Change(this)" onclick="txtMeetingAgenda_ClientClicked(this)"></asp:TextBox>			
						
  <%--   <asp:Label ID="Label17" CssClass="col-sm-3 control-label" Font-Size="Medium" runat="server" ForeColor="Black" Text="وقت الاجندة"></asp:Label>--%>

								<%--<label class="col-sm-3 control-label" style="color:black">  وقت الاجندة</label>--%>
								
								
							<%--	<asp:TextBox ID="txtAgendaTime"  Font-Size="Medium"  runat="server" CssClass="form-control"></asp:TextBox>
									--%>
								
							</div>
     <div id="buttons" class="form-group">
          <div class="col-sm-4"> </div>
         <div class="col-sm-4">   <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
             <asp:Button id="btnOk" CssClass="btn-success" Font-Size="Medium" runat="server" text="حفظ" OnClick="btnOk_Click1" />
</ContentTemplate></asp:UpdatePanel>
         </div>
         <div class="col-sm-4">
             <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
              <asp:Button id="btnCancel" CssClass="btn-danger" Font-Size="Medium" runat="server" text="الغاء" OnClick="btnCancel_Click1" />
</ContentTemplate></asp:UpdatePanel>
         </div>
 <%--         <asp:LinkButton ID="linkAddAgenda" runat="server" OnClick="linkAddAgenda_Click">اضافة اجندة</asp:LinkButton>
    <asp:LinkButton ID="linkRemoveAgenda" runat="server" OnClick="linkRemoveAgenda_Click">حدف اجنده</asp:LinkButton>--%>
          <div id="DivbtnOK" class="buttonOK form-group"></div>
          <div id="Divbtncancel" class="buttonOK form-group">
             
          </div>
     </div>

            </div>

                           
                 
              

              
                         <div id="divagendaUpdate" runat="server" class="form-group" visible="false">
         <div class="col-sm-3">
             <asp:HiddenField ID="AgendaId" runat="server" />
             								<asp:TextBox ID="txtAgendaUpdate"  Font-Size="Medium"  runat="server" CssClass="form-control"></asp:TextBox>

             </div>
                <div class="col-sm-3">
                    								<asp:TextBox ID="txtTimeUpdate"  Font-Size="Medium"  runat="server" CssClass="form-control"></asp:TextBox>

             </div>
               <div class="col-sm-3">
             <asp:Button ID="btnAgendaUpdate" Font-Size="Medium" runat="server" Text="تعديل"  CssClass="btn btn-success" OnClick="btnAgendaUpdate_Click" />

             </div>
                               <div class="col-sm-3">
             <asp:Button ID="Button1" Font-Size="Medium" runat="server" Text="الغاء"  CssClass="btn btn-success" OnClick="Button1_Click" />

             </div>
                             </div>
            <asp:GridView ID="gvAgenda" runat="server" AutoGenerateDeleteButton="True" CssClass="table table-striped" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" AutoGenerateEditButton="True" AutoGenerateSelectButton="True" OnRowCancelingEdit="gvAgenda_RowCancelingEdit" OnRowDataBound="gvAgenda_RowDataBound" OnRowDeleting="gvAgenda_RowDeleting" OnRowEditing="gvAgenda_RowEditing" OnRowUpdating="gvAgenda_RowUpdating" OnSelectedIndexChanged="gvAgenda_SelectedIndexChanged" OnSelectedIndexChanging="gvAgenda_SelectedIndexChanging" >
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
                        <div id="divStatus" runat="server" class="form-group" visible="false">
         <div class="col-sm-3">
             <asp:HiddenField ID="HistoryId" runat="server" />
             								<asp:TextBox ID="txtHistoryUpdate"  Font-Size="Medium"  runat="server" CssClass="form-control"></asp:TextBox>

             </div>
                <div class="col-sm-3">
                    								<asp:TextBox ID="txtHistoryTime"  Font-Size="Medium"  runat="server" CssClass="form-control"></asp:TextBox>

             </div>
               <div class="col-sm-3">
             <asp:Button ID="btnHistoryUpdate" Font-Size="Medium" runat="server" Text="تعديل"  CssClass="btn btn-success" OnClick="btnHistoryUpdate_Click" />

             </div>
                               <div class="col-sm-3">
             <asp:Button ID="btnHistoryCancel" Font-Size="Medium" runat="server" Text="الغاء"  CssClass="btn btn-success" OnClick="btnHistoryCancel_Click" />

             </div>
                             </div>
            <asp:GridView ID="gvStatus" runat="server" AutoGenerateDeleteButton="True" CssClass="table table-striped" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" AutoGenerateEditButton="True" AutoGenerateSelectButton="True" OnRowCancelingEdit="gvStatus_RowCancelingEdit" OnRowDataBound="gvStatus_RowDataBound" OnRowDeleting="gvStatus_RowDeleting" OnRowEditing="gvStatus_RowEditing" OnRowUpdating="gvStatus_RowUpdating">
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

  </div></div>
       </div>
<%--

  </ContentTemplate>
                    </asp:UpdatePanel>--%>
    </form>
      <script type="text/javascript" src="http://maps.googleapis.com/maps/api/js?sensor=false"></script>
    <script type="text/javascript">
        jQuery(document).ready(function ($) {

           $('#<%= txtMeetingDate.ClientID %>').calendarsPicker({
                calendar: $.calendars.instance('islamic','ar'),
                monthsToShow: [1, 1],
                showOtherMonths: true,
            });
             $('#<%= RequiredFieldValidator4.ClientID %>').hide();
          
        });
       
        </script>
   <script type="text/javascript">
       function onTextBox1Change(ele) {
            
            if (event.keyCode == 13) {
                var lnNumber = (ele.value.match(/\n/g) || []).length;

                ele.value = ele.value + (++lnNumber+"-");
            }
       }
         function txtMeetingAgenda_ClientClicked(ele){
     ele.value = ele.value + (1+"-");
    }

       function ShowMap() {

           var long = 0;
           var lat = 0;
           
               var mapOptions = {
                   center: new google.maps.LatLng(24.774265, 46.738586),
                   zoom: 14,
                   mapTypeId: google.maps.MapTypeId.ROADMAP
               };
               var infoWindow = new google.maps.InfoWindow();
               var latlngbounds = new google.maps.LatLngBounds();
               var map = new google.maps.Map(document.getElementById("dvMap"), mapOptions);
               google.maps.event.addListener(map, 'click', function (e) {
                   long = e.latLng.lng();
                   lat = e.latLng.lat();
                   document.getElementById('<%= lat.ClientID %>').value = lat;
                   document.getElementById('<%= lng.ClientID %>').value = long;
                
               });
           }
       
   </script>
                          
    </asp:Content>

