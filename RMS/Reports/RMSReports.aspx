<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RMSReports.aspx.cs" Inherits="RMS.Reports.RMSReports" EnableEventValidation="false" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Training Module</title>
    <link href="~/favicon.ico" rel="shortcut icon" type="image/x-icon" />
    <link href="../Content/css/bootstrapcss/bootstrap.min.css" rel="stylesheet" />
    <meta name="viewport" content="width=device-width" />
    <script src="http://cdn.datatables.net/1.10.2/js/jquery.dataTables.min.js"></script>
    <link rel="stylesheet" href="http://cdn.datatables.net/1.10.2/css/jquery.dataTables.min.css" />


    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <link href="../Content/css/StyleSheet.css" rel="stylesheet" />
    <script src="https://oss.maxcdn.com/html5shiv/3.7.2/html5shiv.min.js"></script>
    <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>

</head>

<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <header>
            <div class="header-right-image img-responsive margin-b5">
                <div class="pull-left logo">
                    <img src="../Content/css/Images/Rave-logo.png" class="img-responsive" />
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

            <div class="row">
                <div class="col-md-12">
                    <div class=" training-title margin-b-10">
                        Report : 
                        <asp:Label ID="lblReportName" runat="server" Text=""></asp:Label>
                        <img src="../Content/css/Images/down-arrow.png" alt="down" />
                    </div>
                </div>
            </div>

            <div class="row">
                <rsweb:ReportViewer ID="rpvReports" runat="server" Height="968px" Width="100%"
                    ShowZoomControl="True" ShowRefreshButton="False" ShowExportControls="true" ShowParameterPrompts="true"
                    ShowPromptAreaButton="False" ShowPrintButton="False" ShowDocumentMapButton="False"
                    ShowFindControls="False" ShowPageNavigationControls="False">
                </rsweb:ReportViewer>
            </div>

        </div>
        <footer>
            <div class="content-wrapper">
                <div class="float-left">
                </div>
            </div>
        </footer>
        <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.3/jquery.min.js"></script>
        <script src="js/bootstrap.min.js"></script>
    </form>
</body>
</html>

