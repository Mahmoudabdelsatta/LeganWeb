<%@ Page Title="" Language="C#" MasterPageFile="~/html/neon/master.Master" AutoEventWireup="true" CodeBehind="Agenda.aspx.cs" Inherits="Committee.Forms.Agenda" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form1" runat="server">
        <script type="text/javascript">
  jQuery(document).ready( function () {
        $("#append").click( function(e) {
          e.preventDefault();
        $(".inc").append('<div class="controls">\
                <input class="form-control" type="text" name="textbox" placeholder="textbox">\
                <input class="form-control" type="text" name="text" placeholder="text">\
                <a href="#" class="remove_this btn btn-danger">remove</a>\
                <br>\
                <br>\
            </div>');
        return false;
        });

    jQuery(document).on('click', '.remove_this', function() {
        jQuery(this).parent().remove();
        return false;
        });
    $("input[type=submit]").click(function(e) {
      e.preventDefault();
      $(this).next("[name=textbox]")
      .val(
        $.map($(".inc :text"), function(el) {
          return el.value
        }).join(",\n")
      )
    })
            });
            </script>
<div class="form-group" id="divAgendaText">
								<label class="col-sm-3 control-label"> اجندة الاجتماع</label>
								
								<div class="col-sm-5">
								<asp:TextBox ID="txtMeetingAgenda" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
									
								</div>
						

								<label class="col-sm-3 control-label">  وقت الاجندة</label>
								
								<div class="col-sm-5">
								<asp:TextBox ID="txtAgendaTime" runat="server" CssClass="form-control"></asp:TextBox>
									
								</div>
							</div>

    <div class="row">
  <asp:LinkButton ID="linkAddAgenda" runat="server">اضافة اجندة</asp:LinkButton>
    <asp:LinkButton ID="linkRemoveAgenda" runat="server">حدف اجنده</asp:LinkButton>
    </div>
  <div class="row">
<asp:Button ID="btnAgendaSave" runat="server" Text="حفظ" />
        <asp:Button ID="btnCancel" runat="server" Text="الغاء" />
  </div>
    
   <div class="control-group">
        <div class="inc">
            <div class="controls">
                <input type="text" class="form-control" name="textbox" placeholder="textbox"/> 
                <input type="text" class="form-control" name="text" placeholder="text"/>
                <button style="margin-left: 50px" class="btn btn-info" type="submit" id="append" name="append">
                Add Textbox</button>
                <br>
                <br>
            </div>
        </div>
        <input type="submit" class="btn btn-info" name="submit" value="submit"/> 
        <input type="text" class="form-control" name="textbox" placeholder="texbox"/> 
    <input type="text" class="form-control" name="text" placeholder="text"/>
    </div>
    </form>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
