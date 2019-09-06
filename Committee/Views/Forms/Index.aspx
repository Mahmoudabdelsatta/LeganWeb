<%--<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Committee.Forms.Index" %>--%>
<%@ Page Title="" Language="C#" MasterPageFile="~/html/neon/master.Master" AutoEventWireup="true" CodeBehind="~/Forms/Index.aspx.cs"  Inherits="Committee.Forms.Index" %>


<asp:Content ID="ContentPlaceHolder1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <form id="form1" runat="server">
        <div>
            <table class="auto-style1" >
            <tr>
                <td class="auto-style3">
                    <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/Images/Alert1.png" PostBackUrl="~/Forms/theSales.aspx" Width="352px" Height="450px" />
                </td>
                <td class="auto-style3">
                    &nbsp;</td>
                <td class="auto-style3">
                    &nbsp;</td>
                <td class="auto-style3">
                    <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Images/Meeting1.jpg" PostBackUrl="~/Forms/Meeting.aspx" Width="352px" Height="450px" />
                </td>
                <td class="auto-style3">
                    &nbsp;</td>
                <td class="auto-style3">
                    &nbsp;</td>
                <td class="auto-style3">
                    <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/Members.png" PostBackUrl="~/Forms/Member.aspx" Width="352px" Height="450px" />
                </td>
                <td class="auto-style3">
                    &nbsp;</td>
                <td class="auto-style3">
                    &nbsp;</td>
                <td class="auto-style3">
                    <asp:ImageButton ID="ImageButton1" runat="server" Height="450px" ImageUrl="~/Images/Committe.png" PostBackUrl="~/Forms/Committe.aspx" Width="352px" AlternateText="المخازن" />
                </td>
            </tr>
            <tr>
                <td class="auto-style4">
                    التنبيهات</td>
                <td class="auto-style5">
                </td>
                <td class="auto-style5">
                </td>
                <td class="auto-style4">
                    الاجتماعات</td>
                <td class="auto-style5">
                </td>
                <td class="auto-style5">
                </td>
                <td class="auto-style4">
                    الأعضاء</td>
                <td class="auto-style5">
                </td>
                <td class="auto-style5">
                </td>
                <td class="auto-style1">
                    اللجان</td>
            </tr>
            </table>
        </div>
    </form>
  </asp:Content>

