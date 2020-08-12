<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RMSReports.aspx.cs" Inherits="RMS.RMSReports" EnableEventValidation="false" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Training Module</title>
    <link href="favicon.ico" rel="shortcut icon" type="image/x-icon" />
    <link href="Content/css/bootstrapcss/bootstrap.min.css" rel="stylesheet" />
    <meta name="viewport" content="width=device-width" />
    <script src="http://cdn.datatables.net/1.10.2/js/jquery.dataTables.min.js"></script>
    <link rel="stylesheet" href="http://cdn.datatables.net/1.10.2/css/jquery.dataTables.min.css" />
    <link href="Content/css/menu.css" type="text/css" rel="stylesheet" />
     <link rel="stylesheet" href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.12.1/themes/base/jquery-ui.css" />
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <link href="Content/css/StyleSheet.css" rel="stylesheet" />
    <script src="https://oss.maxcdn.com/html5shiv/3.7.2/html5shiv.min.js"></script>
    <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
      <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.3/jquery.min.js"></script>
        
    <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.12.1/jquery-ui.min.js"></script>
            <script src="js/bootstrap.min.js"></script>
</head>

<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="1000"></asp:ScriptManager>
        <header>
            <div class="header-right-image img-responsive margin-b5">
                <div class="pull-left logo">
                    <img src="Content/css/Images/Rave-logo.png" class="img-responsive" />
                </div>
                <div class="pull-right rm">
                    Report Training Module
                </div>
            </div>
            <div class="header-strip margin-b-10">
            </div>
        </header>
            <div class="container">

                <div class="row">
                    <div class="col-md-12">
                        <p class="pull-right">
                            Welcome! <span class="UserNameStyle"><i>
                                <asp:Label ID="lblUser" runat="server" Text=""></asp:Label>

                            </i></span>
                        </p>
                    </div>
                </div>
                <div class ="row">
                    <div class="col-md-12">
                    <asp:Menu ID="Menu" runat="server" class="float-right menu-bg" Orientation="Horizontal"
                        StaticMenuItemStyle-ForeColor="White" StaticPopOutImageUrl="none">  
                    </asp:Menu>
                   </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <div class=" training-title margin-b-10">
                            Report : 
                            <asp:Label ID="lblReportName" runat="server" Text=""></asp:Label>
                            <img src="Content/css/Images/down-arrow.png" alt="down" />
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-12">
                    <rsweb:ReportViewer ID="rpvReports" runat="server" Height="968px" Width="100%"
                        ShowZoomControl="True" ShowRefreshButton="False" ShowExportControls="true" ShowParameterPrompts="true"
                        ShowPromptAreaButton="False" ShowPrintButton="False" ShowDocumentMapButton="False"
                        ShowFindControls="False" ShowPageNavigationControls="true">
                    </rsweb:ReportViewer>
                        </div>
                </div>

            </div>
            <footer>
                <div class="content-wrapper">
                    <div class="float-left">
                    </div>
                </div>
            </footer>
          
       
      
    </form>
   <%-- Neelam 13/06/2017 Start chrome datepicker issue--%>
      <script type="text/javascript">

          $(document).ready(function () {
              
              $(function () {

                  var ua = window.navigator.userAgent;
                  var msie = ua.indexOf("MSIE ");

                  if (msie > 0) // If Internet Explorer, return version number
                  {

                  }
                  else  // If another browser, return 0
                  {
                      showDatePicker();
                  }

              });
          });

          function showDatePicker() {
              var parameterRow = $("#ParametersRowrpvReports");
              var innerTable = $(parameterRow).find("table").find("table");
              var span = innerTable.find("span:contains('From Date')");
              if (span) {
                  var innerRow = $(span).parent().parent().parent();
                  var innerCell = innerRow.find("td").eq(1);
                  var textFrom = innerCell.find("input[type=text]");

                  innerCell = innerRow.find("td").eq(4);
                  var textTo = innerCell.find("input[type=text]");

                  $(textFrom).datepicker({
                      defaultDate: "+1w",
                      dateFormat: 'dd/mm/yy',
                      changeMonth: true,
                      numberOfMonths: 1,
                      onClose: function (selectedDate) {
                          $(textTo).datepicker("option", "minDate", selectedDate);
                      }
                  });
                  $(textFrom).focus(function (e) {
                      e.preventDefault();
                      $(textFrom).datepicker("show");
                  });
                  $(textTo).datepicker({
                      defaultDate: "+1w",
                      dateFormat: 'dd/mm/yy',
                      changeMonth: true,
                      numberOfMonths: 1,
                      onClose: function (selectedDate) {
                          $(textFrom).datepicker("option", "maxDate", selectedDate);
                      }
                  });
                  $(textTo).focus(function () {
                      $(textTo).datepicker("show");
                  });
              }
          }
          function pageLoad() { 
              var ua = window.navigator.userAgent;
              var msie = ua.indexOf("MSIE ");

              if (msie > 0) // If Internet Explorer, return version number
              {

              }
              else  // If another browser, return 0
              {
                  showDatePicker();
              }


          } 
    </script>
     <%-- Neelam 13/06/2017 End chrome datepicker issue--%>
</body>
</html>

