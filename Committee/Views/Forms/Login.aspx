<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Committee.Forms.Login" %>
<!DOCTYPE html>
<html lang="ar" dir="rtl">
<head>
	<title>تسجيل دخول</title>
	<meta charset="UTF-8">
	<meta name="viewport" content="width=device-width, initial-scale=1">
<!--===============================================================================================-->	
	<link rel="icon" type="image/png" href="../../MasterPage/img/icons/favicon.ico"/>
<!--===============================================================================================-->
	<link rel="stylesheet" type="text/css" href="../../MasterPage/vendor/bootstrap/css/bootstrap.min.css">
<!--===============================================================================================-->
	<link rel="stylesheet" type="text/css" href="../../MasterPage/fonts/font-awesome-4.7.0/css/font-awesome.min.css">
<!--===============================================================================================-->
	<link rel="stylesheet" type="text/css" href="../../MasterPage/fonts/Linearicons-Free-v1.0.0/icon-font.min.css">
<!--===============================================================================================-->
	<link rel="stylesheet" type="text/css" href="../../MasterPage/vendor/animate/animate.css">
<!--===============================================================================================-->	
	<link rel="stylesheet" type="text/css" href="../../MasterPage/vendor/css-hamburgers/hamburgers.min.css">
<!--===============================================================================================-->
	<link rel="stylesheet" type="text/css" href="../../MasterPage/vendor/animsition/css/animsition.min.css">
<!--===============================================================================================-->
	<link rel="stylesheet" type="text/css" href="../../MasterPage/vendor/select2/select2.min.css">
<!--===============================================================================================-->	
	<link rel="stylesheet" type="text/css" href="../../MasterPage/vendor/daterangepicker/daterangepicker.css">
<!--===============================================================================================-->
	<link rel="stylesheet" type="text/css" href="../../MasterPage/css/util.css">
	<link rel="stylesheet" type="text/css" href="../../MasterPage/css/main.css">
<!--===============================================================================================-->
</head>
<body>
	
	<div class="limiter">
		<div class="container-login100" style="background-image: url('../../MasterPage/img/bg-01.jpg');">
			<div class="wrap-login100 p-t-10 p-b-50 d-flex flex-column">
                <div class="text-center mb-5" >
                <img src="../../MasterPage/img/whitelogo.png" class="img-responsive h-100" />

                </div>
				<span class="login100-form-title p-b-20">
					تسجيل دخول
				</span>
				<form class="login100-form validate-form p-b-33 p-t-5" id="f1" runat="server">

					<div class="wrap-input100 validate-input" data-validate = "Enter username">
<%--						<input class="input100" type="text" name="username" placeholder="User name">--%>
                         
                         <asp:TextBox ID="txtuserName" runat="server" ValidationGroup="login" CssClass="input100" placeholder="اسم المستخدم"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtuserName" ErrorMessage="*" ForeColor="#FF3300" CssClass="reqStar" ValidationGroup="login"></asp:RequiredFieldValidator>
						<span class="focus-input100" data-placeholder="&#xe82a;"></span>
					</div>

					<div class="wrap-input100 validate-input" data-validate="Enter password">
<%--						<input class="input100" type="password" name="pass" placeholder="Password">--%>
                         <asp:TextBox ID="txtPass" runat="server" EnableViewState="False" TextMode="Password" ValidationGroup="login" CssClass="input100"  placeholder="الرقم السري"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPass" ErrorMessage="*" ForeColor="#FF3300"  CssClass="reqStar" ValidationGroup="login"></asp:RequiredFieldValidator>
						<span class="focus-input100" data-placeholder="&#xe80f;"></span>
					</div>

					<div class="container-login100-form-btn m-t-32">
                         <asp:Button ID="btnLogin" runat="server" OnClick="Button1_Click" Text="دخول" ValidationGroup="دخول" CssClass="login100-form-btn" />
						<%--<button class="login100-form-btn">
							Login
						</button>--%>
					</div>

				</form>
			</div>
		</div>
	</div>
	

	<div id="dropDownSelect1"></div>
	
<!--===============================================================================================-->
	<script src="../../MasterPage/vendor/jquery/jquery-3.2.1.min.js"></script>
<!--===============================================================================================-->
	<script src="../../MasterPage/vendor/animsition/js/animsition.min.js"></script>
<!--===============================================================================================-->
	<script src="../../MasterPage/vendor/bootstrap/js/popper.js"></script>
	<script src="../../MasterPage/vendor/bootstrap/js/bootstrap.min.js"></script>
<!--===============================================================================================-->
	<script src="../../MasterPage/vendor/select2/select2.min.js"></script>
<!--===============================================================================================-->
	<script src="../../MasterPage/vendor/daterangepicker/moment.min.js"></script>
	<script src="../../MasterPage/vendor/daterangepicker/daterangepicker.js"></script>
<!--===============================================================================================-->
	<script src="../../MasterPage/vendor/countdowntime/countdowntime.js"></script>
<!--===============================================================================================-->
	<script src="../../MasterPage/js/main.js"></script>

</body>
</html>