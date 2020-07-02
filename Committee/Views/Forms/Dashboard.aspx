<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/master.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="Committee.Views.Forms.Dashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        	
                    <div class="col-sm-12">
                        <div class="well">
                            <h1>
                                <asp:Label ID="lblDate" runat="server" Visible="false"></asp:Label></h1>
                            <h3>مرحبا بك  <strong>
                                <asp:Label ID="lblUserName2" runat="server"></asp:Label></strong></h3>
                        </div>
                    </div>
                </div>
    <div class="col-sm-4 col-xs-6">
		
				<div class="tile-stats tile-red">
					<div class="icon"><i class="entypo-chart-bar"></i></div>
					<div class="num" data-start="0" data-end="lblMembersCount" data-postfix="" data-duration="1500" data-delay="600">
                        <asp:Label ID="lblMembersCount" runat="server" Font-Size="Large"></asp:Label></div>
		
					<h3>عدد الأعضاء</h3>
				
				</div>
		
			</div>
    <div class="col-sm-4 col-xs-6">
<div class="tile-stats tile-blue">
					<div class="icon"><i class="entypo-chart-bar"></i></div>
					<div class="num" data-start="0" data-end="lblCommitteeCount" data-postfix="" data-duration="1500" data-delay="600">
                        <asp:Label ID="lblCommitteeCount" runat="server" Font-Size="Large"></asp:Label></div>
		
					<h3>عدد اللجان</h3>
				
				</div>
        </div>
     <div class="col-sm-4 col-xs-6">
  <div class="tile-stats tile-green">
					<div class="icon"><i class="entypo-chart-bar"></i></div>
					<div class="num" data-start="0" data-end=lblDeptCount data-postfix="" data-duration="1500" data-delay="600">
                        <asp:Label ID="lblDeptCount" runat="server" Font-Size="Large"></asp:Label></div>
		
					<h3>عدد الإدارات</h3>
				
				</div>
 </div>
  	<div class="col-sm-4">
		
						<div class="panel panel-default">
							<div class="panel-heading">
								<div class="panel-title">احصائية  نشاط اللجان</div>
		
								<div class="panel-options">
									<a href="#sample-modal" data-toggle="modal" data-target="#sample-modal-dialog-1" class="bg"><i class="entypo-cog"></i></a>
									<a href="#" data-rel="collapse"><i class="entypo-down-open"></i></a>
									<a href="#" data-rel="reload"><i class="entypo-arrows-ccw"></i></a>
									<a href="#" data-rel="close"><i class="entypo-cancel"></i></a>
								</div>
							</div>
							<div class="panel-body">
								<center> <div id="chart_div"></div></center>
							</div>
						</div>
		
					</div>
    <div class="col-sm-4">
		
						<div class="panel panel-default">
							<div class="panel-heading">
								<div class="panel-title">اعداد اللجان لكل ادارة</div>
		
								<div class="panel-options">
									<a href="#sample-modal" data-toggle="modal" data-target="#sample-modal-dialog-1" class="bg"><i class="entypo-cog"></i></a>
									<a href="#" data-rel="collapse"><i class="entypo-down-open"></i></a>
									<a href="#" data-rel="reload"><i class="entypo-arrows-ccw"></i></a>
									<a href="#" data-rel="close"><i class="entypo-cancel"></i></a>
								</div>
							</div>
							<div class="panel-body">
								<center> <div id="deptCommitteechart_div"></div></center>
							</div>
						</div>
		
					</div>
     	<div class="col-sm-4">
		
						<div class="panel panel-default">
							<div class="panel-heading">
								<div class="panel-title">احصائية نوع اللجان</div>
		
								<div class="panel-options">
									<a href="#sample-modal" data-toggle="modal" data-target="#sample-modal-dialog-1" class="bg"><i class="entypo-cog"></i></a>
									<a href="#" data-rel="collapse"><i class="entypo-down-open"></i></a>
									<a href="#" data-rel="reload"><i class="entypo-arrows-ccw"></i></a>
									<a href="#" data-rel="close"><i class="entypo-cancel"></i></a>
								</div>
							</div>
							<div class="panel-body">
								<center> <div id="CommitteeTypechart_div"></div></center>
							</div>
						</div>
		
					</div>
    <div class="col-sm-4">
		
						<div class="panel panel-default">
							<div class="panel-heading">
								<div class="panel-title">احصائية قبول ورقض المحاضر</div>
		
								<div class="panel-options">
									<a href="#sample-modal" data-toggle="modal" data-target="#sample-modal-dialog-1" class="bg"><i class="entypo-cog"></i></a>
									<a href="#" data-rel="collapse"><i class="entypo-down-open"></i></a>
									<a href="#" data-rel="reload"><i class="entypo-arrows-ccw"></i></a>
									<a href="#" data-rel="close"><i class="entypo-cancel"></i></a>
								</div>
							</div>
							<div class="panel-body">
								<center> <div id="Minutechart_div"></div></center>
							</div>
						</div>
		
					</div>
    
       
    
   
      <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>

    <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
    <script type="text/javascript">
      // Load the Visualization API and the corechart package.
      google.charts.load('current', {'packages':['corechart']});
      // Set a callback to run when the Google Visualization API is loaded.
        google.charts.setOnLoadCallback(drawChart);
        google.charts.setOnLoadCallback(drawChartCommitteeType);
         google.charts.setOnLoadCallback(drawChartMeetingMinute);
                 google.charts.setOnLoadCallback(drawDepartmentsOfEachCommittee);

      // Callback that creates and populates a data table,
      // instantiates the pie chart, passes in the data and
      // draws it.
      function drawChart() {
        // Create the data table.
          $.ajax({
            type: "POST",
            url: "Dashboard.aspx/GetChartData",
            data: '{}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
              success: function (r) {
             var chartdata = new google.visualization.DataTable();
        chartdata.addColumn('string', 'Topping');
          chartdata.addColumn('number', 'Slices');
       
           chartdata.addRows( r.d);

                         // Set chart options
        var options = {'title':'اللجان',
                       'width':400,
                       'height':300};

        // Instantiate and draw our chart, passing in some options.
        var chart = new google.visualization.PieChart(document.getElementById('chart_div'));
        chart.draw(chartdata, options);
             },
            failure: function (r) {
                alert(r.d);
            },
            error: function (r) {
                alert(r.d);
            }
          });
        }
        function drawChartCommitteeType() {
        // Create the data table.
          $.ajax({
            type: "POST",
            url: "Dashboard.aspx/GetTypeCommittee",
            data: '{}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
              success: function (r) {
             var chartdata = new google.visualization.DataTable();
        chartdata.addColumn('string', 'Topping');
          chartdata.addColumn('number', 'Slices');
       
           chartdata.addRows( r.d);

                         // Set chart options
        var options = {'title':'اللجان',
                       'width':400,
                       'height':300};

        // Instantiate and draw our chart, passing in some options.
        var chart = new google.visualization.PieChart(document.getElementById('CommitteeTypechart_div'));
        chart.draw(chartdata, options);
             },
            failure: function (r) {
                alert(r.d);
            },
            error: function (r) {
                alert(r.d);
            }
          });
        }
        function drawChartMeetingMinute() {
        // Create the data table.
          $.ajax({
            type: "POST",
            url: "Dashboard.aspx/AcceptedAndRejectedCount",
            data: '{}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
              success: function (r) {
             var chartdata = new google.visualization.DataTable();
        chartdata.addColumn('string', 'Topping');
          chartdata.addColumn('number', 'Slices');
       
           chartdata.addRows( r.d);

                         // Set chart options
        var options = {'title':'اللجان',
                       'width':400,
                       'height':300};

        // Instantiate and draw our chart, passing in some options.
        var chart = new google.visualization.PieChart(document.getElementById('Minutechart_div'));
        chart.draw(chartdata, options);
             },
            failure: function (r) {
                alert(r.d);
            },
            error: function (r) {
                alert(r.d);
            }
          });
        }
        function drawDepartmentsOfEachCommittee() {
           
        // Create the data table.
          $.ajax({
            type: "POST",
            url: "Dashboard.aspx/GetDepartmentsOfEachCommittee",
            data: '{}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
              success: function (r) { var chartdata = new google.visualization.DataTable();
        chartdata.addColumn('string', 'Topping');
         chartdata.addColumn('number', '');
       
           chartdata.addRows( r.d);

                         // Set chart options
        var options = {'title':'الإدارات',
                       'width':400,
                       'height':300};

        // Instantiate and draw our chart, passing in some options.
        var chart = new google.visualization.ColumnChart(document.getElementById('deptCommitteechart_div'));
        chart.draw(chartdata, options);
             },
            failure: function (r) {
                alert(r.d);
            },
            error: function (r) {
                alert(r.d);
            }
          });
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
