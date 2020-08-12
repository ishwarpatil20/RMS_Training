<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AdvancedSearch.aspx.cs" Inherits="AdvancedSearch" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Advanced Search</title>
    <script type="text/javascript" src="JavaScript/CommonValidations.js"></script>
    <link href="CSS/MRFCommon.css" rel="stylesheet" />
    <script type = "text/javascript">
        function ValidateDateFormat(sender, args)
        {
            if(validateDate(trim(args.Value)))
            {
                args.IsValid = false;
                return;
            }
        }
        
        function ValidateYear(sender, args)
        {
            var year = trim(args.Value);
            if(!onlyNumerics(year))
            {
                args.IsValid = false;
                return;
            }
            else
            {
                if((year.length != 4) || (year == 0))
                {
                    args.IsValid = false;
                    return;    
                }
            }
            
            //--return true
            args.IsValid = true;
            return;    
        }
        
        function ValidateMonth(server, args)
        {
            var month = trim(args.Value);
            if(!onlyNumerics(month))
            {
                args.IsValid = false;
                return;
            }
            else
            {
                if(month == 0)
                {
                    args.IsValid = false;
                    return;    
                }
            }
            
            //--return true
            args.IsValid = true;
            return;    
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManagerMaster" runat="server"></asp:ScriptManager>
    <asp:Label runat = "server" ID = "lblMessage" Visible = "false" ForeColor=red></asp:Label> 
    <div runat = "server" id = "divProjectSearch">
        <table cellpadding = "0" cellspacing = "0" border = "0" width = "100%">
            <tr>
                <td>
                    <table cellpadding = "0" cellspacing = "0" border = "0" width = "70%">
                        <tr>
                            <td width = "30%"><asp:LinkButton runat = "server" ID = "lnkbtnProject" Text = "Project"></asp:LinkButton> </td>
                            <td width = "30%"><asp:LinkButton runat = "server" ID = "lnkbtnMRF" Text = "MRF"></asp:LinkButton></td>
                            <td width = "30%"><asp:LinkButton runat = "server" ID = "lnkbtnEmployee" Text = "Employee"></asp:LinkButton> </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr><td>&nbsp;</td></tr>
            <tr>
                <td>
                    <table cellpadding = "0" cellspacing = "0" border = "0" width = "70%">
                        <tr>
                            <td>Keyword</td>
                            <td>&nbsp;:&nbsp;</td>
                            <td><asp:TextBox runat = "server" ID = "txtKeyword"></asp:TextBox></td>
                        </tr>
                    </table> 
                </td>
            </tr>
            <tr><td>&nbsp;</td></tr>
            <tr>
                <td>
                    <table cellpadding = "0" cellspacing = "0" border = "0" width = "70%">
                        <tr>
                            <td colspan = "3">Categories</td>
                        </tr>
                        <tr>
                            <td style="height: 19px">
                                <table cellpadding = "0" cellspacing = "0" border = "0" width = "100%">
                                    <asp:Repeater runat = "server" ID = "rpCategories" OnItemDataBound = "rpCategories_OnItemDataBound">
                                        <ItemTemplate>
                                            <tr>
                                                <td width = "10%"><asp:Label runat = "server" ID = "lblCategory" Text = '<%#Eval("CategoryName") %>'></asp:Label><asp:Label runat = "server" ID = "lblCategoryID" Text = '<%#Eval("ID") %>' Visible = "false"></asp:Label> </td>        
                                                <td width = "5%">&nbsp;:&nbsp;</td>
                                                <td width = "85%"><asp:ListBox runat = "server" ID = "lbCategory" Width = "100px" SelectionMode = "Multiple"></asp:ListBox></td>
                                            </tr>    
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr><td>&nbsp;</td></tr>
            <%--<tr>
                <td>
                    <table cellpadding = "0" cellspacing = "0" border = "0" width = "70%">
                        <tr>
                            <td>Domain</td>
                            <td>&nbsp;:&nbsp;</td>
                            <td><asp:ListBox runat = "server" ID = "lbDomain" Width = "100px" OnSelectedIndexChanged="lbDomain_SelectedIndexChanged" AutoPostBack = "true" ></asp:ListBox></td>
                            <td>Sub Domain</td>
                            <td>&nbsp;:&nbsp;</td>
                            <td><asp:ListBox runat = "server" ID = "lbSubDomain" Width = "100px"></asp:ListBox></td>
                        </tr>
                    </table>                
                </td>
            </tr>
            <tr><td>&nbsp;</td></tr>--%>
            <tr>
                <td>
                    <table cellpadding = "0" cellspacing = "0" border = "0" width = "70%">
                        <tr>
                            <td>Date From</td>
                            <td>&nbsp;:&nbsp;</td>
                            <td><asp:TextBox runat = "server" ID = "txtDateFrom"></asp:TextBox>
                                <asp:ImageButton ID="imgCalDateFrom" runat="server" ImageUrl="Images/Calendar_scheduleHS.png" CausesValidation="false" ImageAlign="AbsMiddle" />
                                <cc1:CalendarExtender ID="calendarStartDate" runat="server" PopupButtonID="imgCalDateFrom" TargetControlID="txtDateFrom" Format="dd/MM/yyyy"></cc1:CalendarExtender>
                                <asp:CustomValidator runat = "server" ID = "csvDateFrom" ControlToValidate = "txtDateFrom" Display = "Dynamic" ClientValidationFunction = "ValidateDateFormat" Text = "*" ErrorMessage = "InCorrect From Date Format" SetFocusOnError = "true" ValidationGroup = "valProjectSearch"></asp:CustomValidator>
                                <asp:RegularExpressionValidator runat="server" ID="revDateFrom" ControlToValidate="txtDateFrom" ValidationGroup="valProjectSearch" Display="Dynamic" ValidationExpression="(0[1-9]|[12][0-9]|3[01])[//.](0[1-9]|1[012])[//.](19|20)\d\d" SetFocusOnError="true"></asp:RegularExpressionValidator>
                            </td>
                            <td>Date To</td>
                            <td>&nbsp;:&nbsp;</td>
                            <td><asp:TextBox runat = "server" ID = "txtDateTo"></asp:TextBox>
                                <asp:ImageButton ID="imgCalDateTo" runat="server" ImageUrl="Images/Calendar_scheduleHS.png" CausesValidation="false" ImageAlign="AbsMiddle" />
                                <cc1:CalendarExtender ID="calDateTo" runat="server" PopupButtonID="imgCalDateTo" TargetControlID="txtDateTo" Format="dd/MM/yyyy"></cc1:CalendarExtender>
                                <asp:CustomValidator runat = "server" ID = "csvDateTo" ControlToValidate = "txtDateTo" Display = "Dynamic" ClientValidationFunction = "ValidateDateFormat" Text = "*" ErrorMessage = "InCorrect To Date Format" SetFocusOnError = "true" ValidationGroup = "valProjectSearch"></asp:CustomValidator>
                                <asp:RegularExpressionValidator runat="server" ID="revDateTo" ControlToValidate="txtDateTo" ValidationGroup="valProjectSearch" Display="Dynamic" ValidationExpression="(0[1-9]|[12][0-9]|3[01])[//.](0[1-9]|1[012])[//.](19|20)\d\d" SetFocusOnError="true"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                    </table>                
                </td>
            </tr>
            <tr><td>&nbsp;</td></tr>
            <tr>
                <td>
                    <table cellpadding = "0" cellspacing = "0" border = "0" width = "70%">
                        <tr>
                            <td width = "30%">Year</td>
                            <td width = "5%">&nbsp;:&nbsp;</td>
                            <td width = "65%">
                                <asp:TextBox runat = "server" ID = "txtYear"></asp:TextBox>
                                <asp:CustomValidator runat = "server" ID = "csvYear" ControlToValidate = "txtYear" Display = "Dynamic" ClientValidationFunction = "ValidateYear" Text = "*" ErrorMessage = "InCorrect Year" SetFocusOnError = "true" ValidationGroup = "valProjectSearch"></asp:CustomValidator>
                            </td>
                        </tr>   
                        <tr>
                            <td>Months</td>
                            <td>&nbsp;:&nbsp;</td>
                            <td>
                                <asp:TextBox runat = "server" ID = "txtMonth"></asp:TextBox>
                                <asp:CustomValidator runat = "server" ID = "csvMonth" ControlToValidate = "txtMonth" Display = "Dynamic" ClientValidationFunction = "ValidateMonth" Text = "*" ErrorMessage = "InCorrect Month" SetFocusOnError = "true" ValidationGroup = "valProjectSearch"></asp:CustomValidator>
                            </td>
                        </tr>                        
                    </table>
                </td>
            </tr>
            <tr><td>&nbsp;</td></tr>
            <tr>
                <td>
                    <table cellpadding = "0" cellspacing = "0" border = "0" width = "70%">
                        <tr>
                            <td width = "30%" align = "right" style="height: 24px"><asp:Button runat = "server" ID = "btnSearch" Text = "Search" OnClick="btnSearch_Click" ValidationGroup = "valProjectSearch"/></td>
                            <td width = "5%" style="height: 24px"><asp:ValidationSummary runat = "server" ID = "valProjectSearchResult" ValidationGroup = "valProjectSearch" DisplayMode = "BulletList" ShowMessageBox = "true" ShowSummary = "false"  /> </td>
                            <td width = "65%" align = "left" style="height: 24px"><asp:Button runat = "server" ID = "btnReset" Text = "Reset" OnClick="btnReset_Click" /></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <div runat = "server" id = "divProjectSearchResult" visible = "false">
        <table cellpadding = "0" cellspacing = "0" border = "0" width = "100%">
            <tr>
                <td align = "center"><asp:Button runat = "server" ID = "btnBackToSearch" Text = "Back" OnClick = "btnBackToSearch_Click" /></td>
            </tr>
            <tr >
                <td>
                    <table cellpadding = "0" cellspacing = "0" border = "1" width = "70%">
                        <asp:Repeater runat = "server" ID = "rpProjectSearchResult" OnItemDataBound ="rpProjectSearchResult_OnItemDataBound">
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <table cellpadding = "0" cellspacing = "0" border = "0" width = "70%">
                                            <tr>
                                                <td colspan = "3"><asp:HyperLink runat = "server" ID = "hypProject" CssClass="HeaderLink" Style ="cursor:hand;"></asp:HyperLink></td>
                                            </tr>
                                            <tr>
                                                <td width="8%">Technology</td>
                                                <td width = "2">&nbsp;:&nbsp;</td>
                                                <td width = "90%"><asp:Label runat = "server" ID = "Label2" Text = '<%#Eval("technologyName") %>' ></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td>Start Date</td>
                                                <td>&nbsp;:&nbsp;</td>
                                                <td><asp:Label runat = "server" ID = "Label3" Text='<%# Eval("startDate", "{0:dd-MM-yyyy}") %>'></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td>End Date</td>
                                                <td>&nbsp;:&nbsp;</td>
                                                <td><asp:Label runat = "server" ID = "Label4" Text='<%# Eval("endDate", "{0:dd-MM-yyyy}") %>'></asp:Label></td>
                                            </tr>
                                            <tr><td>&nbsp;</td></tr> 
                                        </table>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
