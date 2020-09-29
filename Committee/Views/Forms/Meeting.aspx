

<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/master.Master" AutoEventWireup="true" CodeBehind="~/Views/Forms/Meeting.aspx.cs" EnableSessionState="True"  Inherits="Committee.Views.Forms.Meeting" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="ContentPlaceHolder1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <header>
          <meta http-equiv="content-type" content="text/html; charset=UTF8">
      </header> 

<style>
#ContentPlaceHolder1_gvCommitteeMembers > tbody > tr > td{
     padding: 8px;
    line-height: 1.42857143;
    vertical-align: top;
    border-top: 1px solid #ebebeb;
    text-align: left;
 }
body
{
    margin: 0;
    padding: 0;
    height: 100%;
}
.modal
{
    display: none;
    position: absolute;
    top: 0px;
    left: 0px;
    background-color: black;
    z-index: 100;
    opacity: 0.8;
    filter: alpha(opacity=60);
    -moz-opacity: 0.8;
    min-height: 100%;
}
#divImage
{
    display: none;
    z-index: 1000;
    position: fixed;
    top: 0;
    left: 0;
    background-color: White;
    height: 550px;
    width: 600px;
    padding: 3px;
    border: solid 1px black;
}
</style>

   
      <asp:ScriptManager ID="ScriptManager2" EnableCdn="False" runat="server"></asp:ScriptManager>
     
  
                <asp:UpdatePanel ID="UpdatePaneltypeOfservice" runat="server" UpdateMode="Conditional">
   <ContentTemplate>
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
		
									<strong>
       <asp:Label ID="lblAddMeeting" runat="server" Text="Label"></asp:Label></strong>
							</li>
							</ol>
					
		<h2>
           <asp:Label ID="lblAddMeetingh2" runat="server" Text="Label"></asp:Label></h2>
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
          <asp:LinkButton ID="lnkCancel" runat="server" CssClass="btn btn-white" PostBackUrl="~/Views/Forms/MeetingMangement.aspx" > الغاء</asp:LinkButton>

            </div>
								<div class="col-sm-1">
<%--                                    <asp:Button ID="btnAdd1" runat="server"  Text="حفظ" OnClick="btnAdd1_Click" ValidationGroup="a" CssClass="btn btn-success" />--%>
                                   <asp:LinkButton ID="btnAdd1" Font-Size="Medium" runat="server" Text="حفظ" OnClick="btnAdd1_Click" ValidationGroup="a" CssClass="btn btn-success">حفظ</asp:LinkButton>

								</div>

							</div>
							<div class="form-group">
								<label for="field-1" class="col-sm-3 control-label">عنوان الاجتماع :</label>
									<asp:RequiredFieldValidator ID="RequiredFieldValidator2" Font-Size="Medium" runat="server" ErrorMessage="هذا الحقل مطلوب" ValidationGroup="a" ForeColor="Red" ControlToValidate="txtMeetingName"></asp:RequiredFieldValidator>
								
								<div class="col-sm-5">
                        <asp:TextBox ID="txtMeetingName"  runat="server"  CssClass="form-control" ValidationGroup="a"></asp:TextBox>

								</div>
							</div>
                        <div class="form-group">
								<label for="field-2" class="col-sm-3 control-label">تاريخ الاجتماع :</label>
				 <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Font-Size="Medium" ErrorMessage="هذا الحقل مطلوب" ValidationGroup="a" ForeColor="Red" ControlToValidate="txtMeetingDate" Display="Dynamic"></asp:RequiredFieldValidator>
								
								<div class="col-sm-5">
                        <asp:TextBox ID="txtMeetingDate" runat="server" ValidationGroup="a"  class="form-control" ></asp:TextBox>
                                    <asp:HiddenField ID="txtMeetingDateHidden" runat="server" />

								</div>
							</div>
                        <div class="form-group">
                          
								<label for="field-1" class="col-sm-3 control-label">وقت الاجتماع :</label>
                            <div  class="col-sm-5">
                            <div class="input-group clockpicker  data-placement="left" data-align="top" data-autoclose="true" >
                      <span class="input-group-addon">
        <span class="glyphicon glyphicon-time"></span>
    </span>   <asp:TextBox ID="txtMeetingTime" runat="server" ValidationGroup="a"  class="form-control col-sm-5" > </asp:TextBox>
                                                          <asp:HiddenField ID="txtmeetingTimeHiddenfField" runat="server" />

   
</div>
                            </div>
							</div>
                        <div class="form-group">
								<label for="field-1" class="col-sm-3 control-label">حالة الاجتماع :</label>
								
								<div class="col-sm-5">
								<%--<asp:TextBox ID="txtStatus"  runat="server"  CssClass="form-control" ValidationGroup="a"></asp:TextBox>--%>
                                    <asp:DropDownList ID="ddlMeetingStatus" runat="server" CssClass="form-control">
                           
                                        <asp:ListItem Value="new">جديد</asp:ListItem>
                                        <asp:ListItem Value="minute uploaded">تم رفع المحضر</asp:ListItem>
                                        <asp:ListItem Value="closed">مغلق</asp:ListItem>
                           
                        </asp:DropDownList>

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
                        <div class="form-group" id="addAgenda" runat="server">
								<label class="col-sm-3 control-label">إضافة أجندة : </label>
								
								<div class="col-sm-5">
                                     <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                           <ContentTemplate>
                                      <asp:Button ID="btnOpenPopUp"  Font-Size="Medium"  class="btn-success" runat="server" text="إضافة أجندة" OnClick="btnOpenPopUp_Click" />
                                           
                                 </ContentTemplate>
 <Triggers>
 <asp:PostBackTrigger ControlID="btnOpenPopUp" />
        <asp:PostBackTrigger ControlID="btnOk" />
        <asp:PostBackTrigger ControlID="btnCancel" />
</Triggers>
                                     </asp:UpdatePanel>
								</div>


							</div>
                        <div class="form-group">
                            <div class="col-sm-3"></div>
								<div class="col-sm-5">
                     	<asp:TextBox ID="txtMeetingAgendaLoader" Width="430px" Height="200px" Visible="false" Text="" runat="server" TextMode="MultiLine" CssClass="form-control" onKeyup="onTextBox1Change(this)" onclick="txtMeetingAgenda_ClientClicked(this)" OnTextChanged="txtMeetingAgendaLoader_TextChanged"></asp:TextBox>			
</div>
                        </div>


                        <div class="form-group">
								<label for="field-1" class="col-sm-3 control-label">موقع الاجتماع :</label>
								
								<div class="col-sm-5">
								<asp:TextBox ID="txtMeetingLocation"   runat="server"  CssClass="form-control" ValidationGroup="a"></asp:TextBox>
                                    <div class="col-sm-5" runat="server" id="dvMap">  <button id="linkMap" onclick="ShowMap()" style="font-size:medium" type="button">
                                 <img src="../../MasterPage/img/download (7).jpg" width="40px" height="40px"></button> 
                                
                
                                 <div id="dvMap" style="width: 300px; height: 300px">
   </div> 

								</div>
							</div>
                    </div>
                              <div class="form-group">
								<label class="col-sm-3 control-label">محضر الاجتماع : </label>
								
								<div class="col-sm-5">
                                     <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
									<asp:FileUpload ID="MinutesOfMeetingUpload"  runat="server"  CssClass="form-control file2 inline btn btn-primary" AllowMultiple="false" data-label="<i class='glyphicon glyphicon-circle-arrow-up'></i> &nbsp;اختار محضر اجتماع " />
        <asp:LinkButton ID="btnDownloadMinutesOfMeetingUpload" runat="server" Font-Size="Medium" ForeColor="Blue" Font-Underline="true" Text=""
            OnClick="btnDownload_OnClick" />
       <div id="shpwPdf" runat="server" visible="false">
    <embed src="data:application/pdf;base64,<%=filenamepdf%>"  type="application/pdf" width="100%" height="500" />
        </div>
         </ContentTemplate>
    <Triggers>
 <asp:PostBackTrigger ControlID="btnAdd1" />
        <asp:PostBackTrigger ControlID="btnOk" />
        <asp:PostBackTrigger ControlID="btnCancel" />
          <asp:PostBackTrigger ControlID="btnOk" />
        <asp:PostBackTrigger ControlID="gvAgenda" />
          <asp:PostBackTrigger ControlID="gvStatus" />
                  <asp:PostBackTrigger ControlID="btnAgendaUpdate" />
                  <asp:PostBackTrigger ControlID="Button1" />
        <asp:PostBackTrigger ControlID="btnDownloadMinutesOfMeetingUpload" />
     
</Triggers>
</asp:UpdatePanel>    
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
       
           
<div id="divPopUp" class="panel-danger table-bordered "  style="background-color:darkgray;table-layout:fixed;width:800px;height:300px;border-width:medium thick">
<div class="form-group" id="divAgendaText">
							<%--	<label class="col-sm-3 control-label" style="color:black"> اجندة الاجتماع</label>--%>
     <asp:Label ID="Label16" CssClass="col-sm-3 control-label"  Font-Size="Medium" Font-Bold="true" ForeColor="Black" runat="server" Text="اجندة الاجتماع"></asp:Label>
								
							
								<asp:TextBox ID="txtMeetingAgenda" Text="" runat="server" Width="500px" Height="150px" TextMode="MultiLine" CssClass="form-control" onKeyup="onTextBox1Change(this)" onclick="txtMeetingAgenda_ClientClicked(this)"></asp:TextBox>			
						

								
							</div>
     <div id="buttons" class="form-group">
          <div class="col-sm-4"> </div>
         <div class="col-sm-4">  
             <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
             <asp:Button id="btnOk" CssClass="btn-success" Font-Size="Medium" runat="server" text="حفظ" OnClick="btnOk_Click1" />
</ContentTemplate>
                 </asp:UpdatePanel>
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
         <div class="form-group">
             <asp:HiddenField ID="AgendaId" runat="server" />
             	<asp:TextBox ID="txtAgendaUpdate"  Font-Size="Medium"  runat="server" CssClass="form-control" Width="600px" Height="150px" TextMode="MultiLine"></asp:TextBox>

             </div>
               
               <div class="col-sm-3">
             <asp:Button ID="btnAgendaUpdate" Font-Size="Medium" runat="server" Text="تعديل"  CssClass="btn btn-success" OnClick="btnAgendaUpdate_Click" />

             </div>
                               <div  class="col-sm-3">
             <asp:Button ID="Button1" Font-Size="Medium" runat="server" Text="الغاء"  CssClass="btn btn-danger" OnClick="Button1_Click" />

             </div>
                             </div>
                   <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                         <div class="panel-heading" runat="server" id="div2">

                            <div class="panel-title">
						<asp:Label ID="lblgvAgenda" runat="server" Text="" Visible="false"></asp:Label>
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
                             </div>
                         </ContentTemplate>
                       </asp:UpdatePanel>
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
              
                   <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                         <div class="panel-heading" runat="server" id="div1">

                            <div class="panel-title">
						<asp:Label ID="lblgvStatus" runat="server" Text="" Visible="false"></asp:Label>
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
                             </div>
                    </ContentTemplate>
                       </asp:UpdatePanel>
                    <div>
             <div class="panel-heading" runat="server" id="divmeetingmembers">

                            <div class="panel-title">
						<asp:Label ID="lblmeetingmembers" runat="server" Text="" Visible="false"></asp:Label>
						</div>
     <div id="divBackground" class="modal">
</div>
<div id="divImage">
<table style="height: 100%; width: 100%">
    <tr>
        <td valign="middle" align="center">
            <img id="imgLoader" alt="" src="images/loader.gif" />
            <img id="imgFull" alt="" src="" style="display: none; height: 500px; width: 590px" />
        </td>
    </tr>
    <tr>
        <td align="center" valign="bottom">
            <input id="btnClose" type="button" style="background-color:yellowgreen" value="close" onclick="HideDiv()" />
        </td>
    </tr>
</table>
</div>
             <div id="dvMembers" style="text-align:left">

             </div>   
               <asp:GridView ID="gvCommitteeMembers" runat="server"  AutoGenerateColumns="false" CssClass="table table-striped" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" OnRowDataBound="gvCommitteeMembers_RowDataBound" SortedDescendingHeaderStyle-VerticalAlign="NotSet" AllowPaging="True" PageIndex="5" OnPageIndexChanging="gvCommitteeMembers_PageIndexChanging">
                <AlternatingRowStyle BackColor="#CCCCCC" />
               
                <FooterStyle BackColor="#CCCCCC" />
                <HeaderStyle Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                <SortedAscendingHeaderStyle BackColor="#808080" />
                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                <SortedDescendingHeaderStyle BackColor="#383838" />
                   <Columns>
        <asp:BoundField DataField="User.Name" HeaderText="الإسم" />
        <asp:BoundField DataField="MemberWillAttend" HeaderText="حالة الحضور"  />
        <asp:BoundField DataField="IsMemberAcceptedMiutesOfCommittee" HeaderText="قبول المحضر"  />
        <asp:BoundField DataField="RejectionReason" HeaderText="سبب الرفض " />


                        <asp:TemplateField HeaderText="صورة_التوقيع" >
                         <ItemTemplate >

                             <img src="data:image/jpeg;base64,<%# Eval("MemberSignature") %>"  Width="75px" Height="75px" Style="cursor: pointer" OnClick="return LoadDiv(this.src);" />
                      </ItemTemplate>
                            </asp:TemplateField>
                   </Columns>
            </asp:GridView>

                  <button id="btnExportPdf" style="font-size:medium" type="button" >استخراج على هيئة Pdf</button>
                 <div>

                 </div>
                  <div class="panel-title">
						<asp:Label ID="lblMeetingImg" runat="server" Text="" Visible="false"></asp:Label>
						</div>
                 <asp:GridView ID="gvUploadImages" runat="server"  AutoGenerateColumns="false" CssClass="table table-striped" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" OnRowDataBound="gvCommitteeMembers_RowDataBound" SortedDescendingHeaderStyle-VerticalAlign="NotSet" AllowPaging="True" PageIndex="5" OnPageIndexChanging="gvCommitteeMembers_PageIndexChanging">
                <AlternatingRowStyle BackColor="#CCCCCC" />
               
                <FooterStyle BackColor="#CCCCCC" />
                <HeaderStyle Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                <SortedAscendingHeaderStyle BackColor="#808080" />
                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                <SortedDescendingHeaderStyle BackColor="#383838" />
                   <Columns>


                        <asp:TemplateField HeaderText="صورة_الإجتماع">
                         <ItemTemplate >

                       <%-- <asp:Image ID="Image1" runat="server" ImageUrl ='<%# "~/MasterPage/Uploads" + Eval("MemberSignature") %>' height="80px" Width="80px" />--%>
                            <img src="data:image/jpeg;base64,<%# Eval("MeetingImage") %>"  Width="75px" Height="75px" Style="cursor: pointer" OnClick="return LoadDiv(this.src);" />
                      </ItemTemplate>
                            </asp:TemplateField>
                   </Columns>
            </asp:GridView>
                            </div>
                     
  <div id="divloadMap"></div> 

  </div></div>
    
   </ContentTemplate>
                  </asp:UpdatePanel>

    <script type="text/javascript">
        jQuery(document).ready(function ($) {
            $("#btnExportPdf").click(function () {
                //var quotes = document.getElementById('dvMembers');

                //html2canvas(quotes, {
                //    onrendered: function (canvas) {

                //        //! MAKE YOUR PDF
                //        var pdf = new jsPDF('p', 'pt', 'letter');

                //        for (var i = 0; i <= quotes.clientHeight / 980; i++) {
                //            //! This is all just html2canvas stuff
                //            var srcImg = canvas;
                //            var sX = 0;
                //            var sY = 980 * i; // start 980 pixels down for every new page
                //            var sWidth = 900;
                //            var sHeight = 980;
                //            var dX = 0;
                //            var dY = 0;
                //            var dWidth = 900;
                //            var dHeight = 980;

                //            window.onePageCanvas = document.createElement("canvas");
                //            onePageCanvas.setAttribute('width', 900);
                //            onePageCanvas.setAttribute('height', 980);
                //            var ctx = onePageCanvas.getContext('2d');
                //            // details on this usage of this function: 
                //            // https://developer.mozilla.org/en-US/docs/Web/API/Canvas_API/Tutorial/Using_images#Slicing
                //            ctx.drawImage(srcImg, sX, sY, sWidth, sHeight, dX, dY, dWidth, dHeight);

                //            // document.body.appendChild(canvas);
                //            var canvasDataURL = onePageCanvas.toDataURL("image/png", 1.0);

                //            var width = onePageCanvas.width;
                //            var height = onePageCanvas.clientHeight;

                //            //! If we're on anything other than the first page,
                //            // add another page
                //            if (i > 0) {
                //                pdf.addPage(612, 791); //8.5" x 11" in pts (in*72)
                //            }
                //            //! now we declare that we're working on that page
                //            pdf.setPage(i + 1);
                //            //! now we add content to that page!
                //            pdf.addImage(canvasDataURL, 'PNG', 20, 40, (width * .62), (height * .62));

                //        }
                //        //! after the for loop is finished running, we save the pdf.
                //        pdf.save('test.pdf');
                //    }
                //});




                html2canvas($('[id*=gvCommitteeMembers]')[0], {
                    onrendered: function (canvas) {
                        var data = canvas.toDataURL();
                        var docDefinition = {
                            content: [{
                                image: data,
                                width: 500
                            }]
                        };
                        pdfMake.createPdf(docDefinition).download("صور الاجتماع.pdf");
                    }
                });
            });


            var value = $('#<%= txtMeetingDate.ClientID %>').val();
            if (value) {
                m = moment($('#<%= txtMeetingDate.ClientID %>').val(), 'iYYYY/iMM/iDD'); // Parse a Hijri date.
                   var x = m.format('iYYYY/iMM/iDD'); // 1410/8/28 is 1990/3/25
                   $('#<%= txtMeetingDate.ClientID %>').val(x);
                $('#<%= txtMeetingDateHidden.ClientID %>').val(m.format('iYYYY/iMM/iDD'))
            }

            $('#<%= txtMeetingDate.ClientID %>').calendarsPicker({
                calendar: $.calendars.instance('islamic', 'ar'),
                monthsToShow: [1, 1],
                showOtherMonths: true,
                onClose: function () {
                    m = moment($('#<%= txtMeetingDate.ClientID %>').val(), 'iYYYY/iMM/iDD'); // Parse a Hijri date.
                    var x = m.format('iYYYY/iMM/iDD'); // 1410/8/28 is 1990/3/25

                       $('#<%= txtMeetingDateHidden.ClientID %>').val(x)

                   }
               });
            $('#<%= RequiredFieldValidator4.ClientID %>').hide();
            $('.clockpicker').clockpicker()
                .find('#txtMeetingTime').change(function () {
                    debugger;
                    $('#<%= txtmeetingTimeHiddenfField.ClientID %>').val(this.value)

             });

        });

        </script>
   <script type="text/javascript">
       function onTextBox1Change(ele) {

           if (event.keyCode == 13) {
               var lnNumber = (ele.value.match(/\n/g) || []).length;

               ele.value = ele.value + (++lnNumber + "-");
           }
       }
       function txtMeetingAgenda_ClientClicked(ele) {

           ele.value = ele.value + (1 + "-");
       }
       function LoadGoogleMAP() {

           var markers = [];
           var map = new google.maps.Map(document.getElementById('divloadMap'), {
               mapTypeId: google.maps.MapTypeId.ROADMAP
           });

           var defaultBounds = new google.maps.LatLngBounds(
               new google.maps.LatLng(-33.8902, 151.1759),
               new google.maps.LatLng(-33.8474, 151.2631));
           map.fitBounds(defaultBounds);

           // Create the search box and link it to the UI element.  
           var input = (document.getElementById('txtsearch'));
           map.controls[google.maps.ControlPosition.TOP_LEFT].push(input);

           var searchBox = new google.maps.places.SearchBox((input));


           // Listen for the event fired when the user selects an item from the  
           // pick list. Retrieve the matching places for that item.  
           google.maps.event.addListener(searchBox, 'places_changed', function () {
               var places = searchBox.getPlaces();

               if (places.length == 0) {
                   return;
               }
               for (var i = 0, marker; marker = markers[i]; i++) {
                   marker.setMap(null);
               }

               // For each place, get the icon, place name, and location.  
               markers = [];
               var bounds = new google.maps.LatLngBounds();
               for (var i = 0, place; place = places[i]; i++) {
                   var image = {
                       url: place.icon,
                       size: new google.maps.Size(71, 71),
                       origin: new google.maps.Point(0, 0),
                       anchor: new google.maps.Point(17, 34),
                       scaledSize: new google.maps.Size(25, 25)
                   };

                   // Create a marker for each place.  
                   var marker = new google.maps.Marker({
                       map: map,
                       icon: image,
                       title: place.name,
                       position: place.geometry.location
                   });

                   markers.push(marker);

                   bounds.extend(place.geometry.location);
               }

               map.fitBounds(bounds);
           });



           // current map's viewport.  
           google.maps.event.addListener(map, 'bounds_changed', function () {
               var bounds = map.getBounds();
               searchBox.setBounds(bounds);
           });
       }

       google.maps.event.addDomListener(window, 'load', LoadGoogleMAP);

       function ShowMap() {

           var long = 0;
           var lat = 0;
           var loc = "";

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

   <%--             var geocoder;
geocoder = new google.maps.Geocoder();
var latlng = new google.maps.LatLng(lat, long);

geocoder.geocode(
    {'latLng': latlng}, 
    function(results, status) {
        if (status == google.maps.GeocoderStatus.OK) {
                if (results[0]) {
                    var add= results[0].formatted_address ;
                    var  value=add.split(",");

                    count=value.length;
                    country=value[count-1];
                    state=value[count-2];
                    city = value[count - 3];
                    document.getElementById('<%= txtMeetingLocation.ClientID %>').value = city;
                }
                else  {
                    alert("address not found");
                }
        }
         else {
            alert("Geocoder failed due to: " + status);
        }
    }
);--%>
           });

       }

   </script>
            

     
 <script type="text/javascript">
     function LoadDiv(url) {
         var img = new Image();
         var bcgDiv = document.getElementById("divBackground");
         var imgDiv = document.getElementById("divImage");
         var imgFull = document.getElementById("imgFull");
         var imgLoader = document.getElementById("imgLoader");
         imgLoader.style.display = "block";
         img.onload = function () {
             imgFull.src = img.src;
             imgFull.style.display = "block";
             imgLoader.style.display = "none";
         };
         img.src = url;
         var width = document.body.clientWidth;
         if (document.body.clientHeight > document.body.scrollHeight) {
             bcgDiv.style.height = document.body.clientHeight + "px";
         }
         else {
             bcgDiv.style.height = document.body.scrollHeight + "px";
         }
         imgDiv.style.left = (width - 650) / 2 + "px";
         imgDiv.style.top = "20px";
         bcgDiv.style.width = "100%";

         bcgDiv.style.display = "block";
         imgDiv.style.display = "block";
         return false;
     }
     function HideDiv() {
         var bcgDiv = document.getElementById("divBackground");
         var imgDiv = document.getElementById("divImage");
         var imgFull = document.getElementById("imgFull");
         if (bcgDiv != null) {
             bcgDiv.style.display = "none";
             imgDiv.style.display = "none";
             imgFull.style.display = "none";
         }
     }
</script>   
     

    </asp:Content>

