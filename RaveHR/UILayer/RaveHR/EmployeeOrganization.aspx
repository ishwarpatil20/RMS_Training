<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true"
    CodeFile="EmployeeOrganization.aspx.cs" Inherits="EmployeeOrganization" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register TagPrefix="ksa" TagName="BubbleControl" Src="~/EmployeeMenuUC.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMainContent" runat="Server">

    <script type="text/javascript">
  setbgToTab('ctl00_tabEmployee', 'ctl00_SpanEmployeeProfile');
    </script>

    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
        <%-- Sanju:Issue Id 50201:Added new class so that header display in other browsers also--%>
            <td align="center"  class="header_employee_profile" style="filter: progid:DXImageTransform.Microsoft.Gradient(endColorstr='#7590C8', startColorstr='#0C359A', gradientType='1');">
          <%--  Sanju:Issue Id 50201:End--%>
                <asp:Label ID="lblEmployeeDetails" runat="server" Text="Employee Profile" CssClass="header"></asp:Label>
            </td>
              <%-- Sanju:Issue Id 50201: Added background color property so that all browsers display header--%>
            <td align="right" style="height: 25px; width: 20%;background-color:#7590C8; filter: progid:DXImageTransform.Microsoft.Gradient(endColorstr='#7590C8', startColorstr='#7590C8', gradientType='0');">
              <%--  Sanju:Issue Id 50201:Issue Id 50201:End--%>
                <asp:Label ID="lblempName" runat="server" CssClass="header"> </asp:Label>
            </td>
        </tr>
        <tr>
            <td style="height: 1pt">
            </td>
        </tr>
    </table>
    <asp:Table ID="tblMain" runat="server" Width="100%" BorderColor="AliceBlue" BorderStyle="Solid"
        BorderWidth="2" Height="100%">
        <asp:TableRow ID="TableRow1" runat="server" Width="100%" VerticalAlign="Top">
            <asp:TableCell ID="tcellIndex" Width="15%" Height="100%" runat="server" BorderColor="Beige"
                BorderWidth="1">
                <!-- Panel for user control -->
                <asp:Panel ID="pnlUserControl" runat="server">
                    <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
                </asp:Panel>
            </asp:TableCell>
            <asp:TableCell ID="tcellContent" Width="85%" Height="100%" runat="server">

                <script type="text/javascript">
                 setbgToTab('ctl00_tabEmployee', 'ctl00_SpanEmployeeProfile');
                    
                    function $(v){return document.getElementById(v);}

                    function ButtonClickValidate()
                    {
                    
                        var lblMandatory;
                        var spanlist = "";

                        var CompanyName = $('<%=txtCompanyName.ClientID %>').id
                        var PositionHeld = $( '<%=txtPositionHeld.ClientID %>').id
                        var MonthSince = $('<%=ddlMonthSince.ClientID %>').value;
                        var YearSince = $('<%=ddlYearSince.ClientID %>').value;
                        var MonthTill = $('<%=ddlMonthTill.ClientID %>').value;
                        var YearTill = $('<%=ddlYearTill.ClientID %>').value;
                        
                        if(MonthSince == "" || MonthSince == "Month")
                        {
                            var sMonthSince = $('<%=spanzMonthSince.ClientID %>').id;
                            spanlist = sMonthSince +",";
                        } 
                        if(YearSince == "" || YearSince == "Year")
                        {
                            var sYearSince = $('<%=spanzYearSince.ClientID %>').id;
                            spanlist = spanlist + sYearSince +",";
                        } 
                        if(MonthTill == "" || MonthTill == "Month")
                        {
                            var sMonthTill = $('<%=spanzMonthTill.ClientID %>').id;
                            spanlist = spanlist + sMonthTill +",";
                        } 
                        if(YearTill == "" || YearTill == "Year")
                        {
                            var sYearTill = $('<%=spanzYearTill.ClientID %>').id;
                            spanlist = spanlist + sYearTill +",";
                        }
                        
                        if(spanlist == "")
                        {
                            var controlList =  CompanyName  +","+PositionHeld;
                        }
                        else
                        {
                            var controlList = spanlist +  CompanyName  +","+PositionHeld;
                        }
                        
                        if(ValidateControlOnClickEvent(controlList) == false)
                        {
                            lblError = $( '<%=lblError.ClientID %>')
                            lblError.innerText = "Please fill all mandatory fields.";
                            lblError.style.color = "Red";
                        }
                        
                        return ValidateControlOnClickEvent(controlList);
                    }
                    
                     function RowButtonClickValidate()
                    {
                        var lblMandatory;
                        var spanlist = "";

                        var CompanyName = $('<%=txtRCompanyName.ClientID %>').id
                        var PositionHeld = $( '<%=txtRPositionHeld.ClientID %>').id
                        var MonthSince = $('<%=ddlRMonthSince.ClientID %>').value;
                        var YearSince = $('<%=ddlRYearSince.ClientID %>').value;
                        var MonthTill = $('<%=ddlRMonthTill.ClientID %>').value;
                        var YearTill = $('<%=ddlRYearTill.ClientID %>').value;
                        
                        if(MonthSince == "" || MonthSince == "Month")
                        {
                            var sMonthSince = $('<%=spanzRMonthSince.ClientID %>').id;
                            spanlist = sMonthSince +",";
                        } 
                        if(YearSince == "" || YearSince == "Year")
                        {
                            var sYearSince = $('<%=spanzRYearSince.ClientID %>').id;
                            spanlist = spanlist + sYearSince +",";
                        } 
                        if(MonthTill == "" || MonthTill == "Month")
                        {
                            var sMonthTill = $('<%=spanzRMonthTill.ClientID %>').id;
                            spanlist = spanlist + sMonthTill +",";
                        } 
                        if(YearTill == "" || YearTill == "Year")
                        {
                            var sYearTill = $('<%=spanzRYearTill.ClientID %>').id;
                            spanlist = spanlist + sYearTill +",";
                        }
                         
                        if(spanlist == "")
                        {
                            var controlList =  CompanyName  +","+PositionHeld;
                        }
                        else
                        {
                            var controlList = spanlist +  CompanyName  +","+PositionHeld;
                        }
                        
                        if(ValidateControlOnClickEvent(controlList) == false)
                        {
                            lblError = $( '<%=lblError.ClientID %>')
                            lblError.innerText = "Please fill all mandatory fields.";
                            lblError.style.color = "Red";
                        }
                        
                        return ValidateControlOnClickEvent(controlList);
                    }
                    
                    function JavScriptFn(IsFresher,PreviousPage) {

                      if (IsFresher == "1") {
                          //Mohamed : Issue 40221 : 31/01/2013 : Starts                        			  
                          //Desc :Error message should be ""Since you are a fresher you cannot add Experience Summary."" instead of ""Since you are fresher you cannot add Experience Summary."""

                          //alert('Since you are fresher you cannot add Experience Summary.');
                          alert('Since you are a fresher you cannot add Experience Summary.');
                          //Mohamed : Issue 40221 : 31/01/2013 : Ends				                      
                        window.location=PreviousPage;

                        return false; 
                      }
                      else
                      return true;
                       
                    }
                    
                     function SaveButtonClickValidate()
                    {
                     
                        var lblMandatory;
                        var ReleventYears = $('<%=txtReleventYears.ClientID %>').id
                        var ReleventMonths = $( '<%=txtReleventMonths.ClientID %>').id
                 
                        var controlList = ReleventYears + "," + ReleventMonths;

                        if(ValidateControlOnClickEvent(controlList) == false)
                        {
                            lblError = $( '<%=lblError.ClientID %>')
                            lblError.innerText = "Please fill all mandatory fields.";
                            lblError.style.color = "Red";
                            
                            return ValidateControlOnClickEvent(controlList);
                        }
                        else
                        {
                            var CompanyName = $('<%=txtCompanyName.ClientID %>').value;
                            var PositionHeld = $( '<%=txtPositionHeld.ClientID %>').value;
                            var MonthSince = $('<%=ddlMonthSince.ClientID %>').value;
                            var YearSince = $('<%=ddlYearSince.ClientID %>').value;
                            var MonthTill = $('<%=ddlMonthTill.ClientID %>').value;
                            var YearTill = $('<%=ddlYearTill.ClientID %>').value;
                     
                            var AllControlEmpty;

                            if(CompanyName =="" &&  PositionHeld == "" && MonthSince =="Month" && YearSince == "Year" && MonthTill == "Month" && YearTill == "Year")
                            {
                               AllControlEmpty = "Yes";
                            }
                            
                            if(AllControlEmpty != "Yes")         
                            {
                                if($('<%=btnAdd.ClientID %>')== null)
                                {
                                    alert("To save the Relevant Experience details entered, kindly click on Update");
                                    return false;
                                }
                                else
                                {
                                    alert("To save the Relevant Experience details entered, kindly click on Add row");
                                    return false;
                                }   
                            }  
                            else
                            {
                                 $('<%=divNonReleventDetail.ClientID %>')

                                if($('<%=divNonReleventDetail.ClientID %>') != null)
                                {
                                    var CompanyName = $('<%=txtRCompanyName.ClientID %>').value;
                                    var PositionHeld = $( '<%=txtRPositionHeld.ClientID %>').value;
                                    var MonthSince = $('<%=ddlRMonthSince.ClientID %>').value;
                                    var YearSince = $('<%=ddlRYearSince.ClientID %>').value;
                                    var MonthTill = $('<%=ddlRMonthTill.ClientID %>').value;
                                    var YearTill = $('<%=ddlRYearTill.ClientID %>').value;
                             
                                    var AllRelevantControlEmpty;

                                    if(CompanyName ==""  && PositionHeld == "" && MonthSince =="Month" && YearSince == "Year" && MonthTill == "Month" && YearTill == "Year")
                                    {
                                       AllRelevantControlEmpty = "Yes";
                                    }
                                    
                                    if(AllRelevantControlEmpty != "Yes")         
                                    {
                                        if($('<%=btnAddRow.ClientID %>')== null)
                                        {
                                            alert("To save the Non Relevant Experience details entered, kindly click on Update");
                                            return false;
                                        }
                                        else
                                        {
                                            alert("To save the Non Relevant Experience details entered, kindly click on Add row");
                                            return false;
                                        }   
                                    }
                                    else
                                    {
                                        return true;
                                    }
                                }
                                else
                                {
                                   return true;
                                } 
                            }
                       }
                        
                    }
                    
                </script>

                <!-- Dump aspx code here -->
                <div class="detailsborder">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <table width="100%">
                                <tr>
                                    <td>
                                        <asp:Label ID="lblMessage" runat="server" CssClass="Messagetext"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblError" runat="server" Font-Names="Verdana" Font-Size="9pt" ForeColor="Red"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblMandatory" runat="server" Font-Names="Verdana" Font-Size="9pt">Fields marked with <span class="mandatorymark">*</span> are mandatory.</asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblMsg" runat="server" Font-Names="Verdana" Font-Size="9pt" CssClass="Messagetext">Please click on the <span style="font-weight:bold">Save All</span> button to save experience summary.</asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <table width="100%">
                                <tr id="ExperienceRow" runat="server">
                                    <td style="width: 25%" class="txtstyle">
                                        <asp:Label ID="lblTotalExperience" runat="server" Text="External Experience"></asp:Label> <%--Mahendra Temp Fix 28109 STRAT   Text="Total Experience" --%>
                                        <label class="mandatorymark">
                                            *</label>
                                    </td>
                                    <td style="width: 25%">
                                        <asp:TextBox ID="txtTotalYears" runat="server" CssClass="mandatoryField" Width="35px">
                                        </asp:TextBox>&nbsp;Yrs. <span id="spanTotalYears" runat="server">
                                            <img id="imgTotalYears" runat="server" src="Images/cross.png" alt="" style="display: none;
                                                border: none;" />
                                        </span>
                                        <asp:TextBox ID="txtTotalMonths" runat="server" CssClass="mandatoryField" Width="35px">
                                        </asp:TextBox>&nbsp;Mths. <span id="spanTotalMonths" runat="server">
                                            <img id="imgTotalMonths" runat="server" src="Images/cross.png" alt="" style="display: none;
                                                border: none;" />
                                        </span>
                                    </td>
                                    <td style="width: 25%" class="txtstyle">
                                        <asp:Label ID="lblReleventExperience" runat="server" Text="External Relevant Experience"></asp:Label>  <%--Mahendra Temp Fix 28109 STRAT   Text="Relevant Experience" --%>
                                        <label class="mandatorymark">
                                            *</label>
                                    </td>
                                    <td style="width: 25%">
                                        <asp:TextBox ID="txtReleventYears" runat="server" CssClass="mandatoryField" Width="35px">
                                        </asp:TextBox>&nbsp;Yrs. <span id="spanReleventYears" runat="server">
                                            <img id="imgReleventYears" runat="server" src="Images/cross.png" alt="" style="display: none;
                                                border: none;" />
                                        </span>
                                        <asp:TextBox ID="txtReleventMonths" runat="server" CssClass="mandatoryField" Width="35px">
                                        </asp:TextBox>&nbsp;Mths. <span id="spanReleventMonths" runat="server">
                                            <img id="imgReleventMonths" runat="server" src="Images/cross.png" alt="" style="display: none;
                                                border: none;" />
                                        </span>
                                    </td>
                                </tr>
                              <%--Mahendra Temp Fix 28109 STRAT--%>
                                <tr id="Tr1" runat="server">
                                    <td style="width: 25%" class="txtstyle">
                                        <asp:Label ID="lblRaveExperience" runat="server" Text="Rave Experience"></asp:Label>
                                        <label class="mandatorymark">
                                            *</label>
                                    </td>
                                    <td style="width: 25%">
                                        <asp:TextBox ID="txtRaveExperienceYear" runat="server" CssClass="mandatoryField" Width="35px" Enabled="false">
                                        </asp:TextBox>&nbsp;Yrs. <span id="span1" runat="server">
                                            <img id="img3" runat="server" src="Images/cross.png" alt="" style="display: none;
                                                border: none;" />
                                        </span>
                                        <asp:TextBox ID="txtRaveExperienceMonths" runat="server" CssClass="mandatoryField" Width="35px" Enabled="false">
                                        </asp:TextBox>&nbsp;Mths. <span id="span3" runat="server">
                                            <img id="img4" runat="server" src="Images/cross.png" alt="" style="display: none;
                                                border: none;" />
                                        </span>
                                    </td>
                                    <td style="width: 25%" class="txtstyle">
                                        <asp:Label ID="Label3" runat="server" Text="Total Relevant Experience"></asp:Label>
                                        <label class="mandatorymark">
                                            *</label>
                                    </td>
                                    <td style="width: 25%">
                                        <asp:TextBox ID="txtTotalRaveExternalRelevantYear" runat="server" CssClass="mandatoryField" Width="35px" Enabled="false">
                                        </asp:TextBox>&nbsp;Yrs. <span id="span5" runat="server">
                                            <img id="img5" runat="server" src="Images/cross.png" alt="" style="display: none;
                                                border: none;" />
                                        </span>
                                        <asp:TextBox ID="txtTotalRaveExternalRelevantMonths" runat="server" CssClass="mandatoryField" Width="35px" Enabled="false">
                                        </asp:TextBox>&nbsp;Mths. <span id="span6" runat="server">
                                            <img id="img6" runat="server" src="Images/cross.png" alt="" style="display: none;
                                                border: none;" />
                                        </span>
                                    </td>
                                </tr>
                                <%--Mahendra Temp Fix 28109 END--%>
                                
                                <tr>
                                    <td style="width: 25%">
                                    </td>
                                    <td style="width: 25%">
                                    </td>
                                    <td style="width: 25%">
                                    </td>
                                    <td style="width: 25%">
                                    </td>
                                </tr>
                            </table>
                            <div id="divReleventDetail" runat="server">
                                <table width="100%" class="detailsbg">
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblOrganizationGroup" runat="server" Text="Relevant Experience" CssClass="detailsheader"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <asp:Panel ID="Releventdetails" runat="server">
                                    <table width="100%">
                                        <tr>
                                            <td style="width: 25%" class="txtstyle">
                                                <asp:Label ID="lblCompanyName" runat="server" Text="Company Name"></asp:Label>
                                                <label class="mandatorymark">
                                                    *</label>
                                            </td>
                                            <td style="width: 25%">
                                                <asp:TextBox ID="txtCompanyName" runat="server" ToolTip="Enter Company Name" MaxLength="30"
                                                    CssClass="mandatoryField"></asp:TextBox>
                                                <span id="spanCompanyName" runat="server">
                                                    <img id="imgCompanyName" runat="server" src="Images/cross.png" alt="" style="display: none;
                                                        border: none;" />
                                                </span>
                                            </td>
                                            <td style="width: 25%" class="txtstyle">
                                                <asp:Label ID="lblPositionHeld" runat="server" Text="Position Held"></asp:Label>
                                                <label class="mandatorymark">
                                                    *</label>
                                            </td>
                                            <td style="width: 25%">
                                                <asp:TextBox ID="txtPositionHeld" runat="server" CssClass="mandatoryField" ToolTip="Enter Position Held"
                                                    MaxLength="50"></asp:TextBox>
                                                <span id="spanPositionHeld" runat="server">
                                                    <img id="imgPositionHeld" runat="server" src="Images/cross.png" alt="" style="display: none;
                                                        border: none;" />
                                                </span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 25%" class="txtstyle">
                                                <asp:Label ID="lblWorkingSince" runat="server" Text="Working Since" CssClass="mandatoryField"></asp:Label>
                                                <label class="mandatorymark">
                                                    *</label>
                                            </td>
                                            <td style="width: 25%">
                                                <span id="spanzMonthSince" runat="server">
                                                    <asp:DropDownList ID="ddlMonthSince" runat="server" CssClass="mandatoryField" Width="90px"
                                                        OnSelectedIndexChanged="ddlMonthSince_SelectedIndexChanged" AutoPostBack="true">
                                                    </asp:DropDownList>
                                                </span><span id="spanMonthSince" runat="server">
                                                    <img id="imgMonthSince" runat="server" src="Images/cross.png" alt="" style="display: none;
                                                        border: none;" />
                                                </span><span id="spanzYearSince" runat="server">
                                                    <asp:DropDownList ID="ddlYearSince" runat="server" CssClass="mandatoryField" OnSelectedIndexChanged="ddlYearSince_SelectedIndexChanged"
                                                        AutoPostBack="true" Width="57px">
                                                    </asp:DropDownList>
                                                </span><span id="spanYearSince" runat="server">
                                                    <img id="imgYearSince" runat="server" src="Images/cross.png" alt="" style="display: none;
                                                        border: none;" />
                                                </span>
                                            </td>
                                            <td style="width: 25%" class="txtstyle">
                                                <asp:Label ID="lblWorkingTill" runat="server" Text="Working Till"></asp:Label>
                                                <label class="mandatorymark">
                                                    *</label>
                                            </td>
                                            <td style="width: 25%">
                                                <span id="spanzMonthTill" runat="server">
                                                    <asp:DropDownList ID="ddlMonthTill" runat="server" Width="90px" CssClass="mandatoryField"
                                                        OnSelectedIndexChanged="ddlMonthTill_SelectedIndexChanged" AutoPostBack="true">
                                                    </asp:DropDownList>
                                                </span><span id="spanMonthTill" runat="server">
                                                    <img id="imgMonthTill" runat="server" src="Images/cross.png" alt="" style="display: none;
                                                        border: none;" />
                                                </span><span id="spanzYearTill" runat="server">
                                                    <asp:DropDownList ID="ddlYearTill" runat="server" CssClass="mandatoryField" OnSelectedIndexChanged="ddlYearTill_SelectedIndexChanged"
                                                        AutoPostBack="true" Width="57px">
                                                    </asp:DropDownList>
                                                </span><span id="spanYearTill" runat="server">
                                                    <img id="imgYearTill" runat="server" src="Images/cross.png" alt="" style="display: none;
                                                        border: none;" />
                                                </span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 25%" class="txtstyle">
                                                <asp:Label ID="lblExperience" runat="server" Text="Experience"></asp:Label>
                                                <label class="mandatorymark">
                                                    *</label>
                                            </td>
                                            <td style="width: 25%">
                                                <asp:TextBox ID="txtExperienceYear" runat="server" CssClass="mandatoryField" Width="35px">
                                                </asp:TextBox>&nbsp;Yrs. </span><span id="span4" runat="server">
                                                    <img id="img2" runat="server" src="Images/cross.png" alt="" style="display: none;
                                                        border: none;" />
                                                </span>
                                                <asp:TextBox ID="txtExperienceMonth" runat="server" CssClass="mandatoryField" Width="35px">
                                                </asp:TextBox>&nbsp;Mths. <span id="span2" runat="server">
                                                    <img id="img1" runat="server" src="Images/cross.png" alt="" style="display: none;
                                                        border: none;" />
                                                </span>
                                            </td>
                                            <td colspan="2" align="right">
                                                <asp:Button ID="btnUpdate" TabIndex="14" runat="server" Text="Update" CssClass="button"
                                                    OnClick="btnUpdate_Click" Visible="false"></asp:Button>
                                                <asp:Button ID="btnCancel1" TabIndex="14" runat="server" Text="Cancel" CssClass="button"
                                                    OnClick="btnCancel_Click" Visible="false"></asp:Button>
                                                <asp:Button ID="btnAdd" runat="server" CssClass="button" OnClick="btnAdd_Click" TabIndex="14"
                                                    Text="Add" />
                                            </td>
                                        </tr>
                                    </table>
                                    <div id="divGVOrganisation">
                                        <asp:GridView ID="gvOrganisation" runat="server" Width="100%" AutoGenerateColumns="False"
                                            OnRowDeleting="gvOrganisation_RowDeleting" OnRowEditing="gvOrganisation_RowEditing">
                                            <HeaderStyle CssClass="addrowheader" />
                                            <AlternatingRowStyle CssClass="alternatingrowStyle" />
                                            <RowStyle Height="20px" CssClass="textstyle" />
                                            <Columns>
                                                <asp:BoundField HeaderText="Company Name" DataField="CompanyName"></asp:BoundField>
                                                <asp:BoundField HeaderText="Position Held" DataField="Designation"></asp:BoundField>
                                                <asp:BoundField HeaderText="Working Since" DataField="WorkingSince"></asp:BoundField>
                                                <asp:BoundField HeaderText="Working Till" DataField="WorkingTill"></asp:BoundField>
                                                <asp:BoundField HeaderText="Experience" DataField="Experience"></asp:BoundField>
                                                <asp:TemplateField Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="OrganisationId" runat="server" MaxLength="50" Text='<%# DataBinder.Eval(Container, "DataItem.OrganisationId") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Mode" runat="server" MaxLength="50" Text='<%# DataBinder.Eval(Container, "DataItem.Mode") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="MonthSince" runat="server" MaxLength="50" Text='<%# DataBinder.Eval(Container, "DataItem.MonthSince") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="YearSince" runat="server" MaxLength="50" Text='<%# DataBinder.Eval(Container, "DataItem.YearSince") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="MonthTill" runat="server" MaxLength="50" Text='<%# DataBinder.Eval(Container, "DataItem.MonthTill") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="YearTill" runat="server" MaxLength="50" Text='<%# DataBinder.Eval(Container, "DataItem.YearTill") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="true">
                                                    <ItemTemplate>
                                                        <asp:ImageButton runat="server" ID="imgBtnEdit" CausesValidation="false" CommandName="Edit"
                                                            ImageUrl="Images/Edit.gif" ToolTip="Edit Organisation Detail" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="true">
                                                    <ItemTemplate>
                                                        <asp:ImageButton runat="server" ID="imgBtnDelete" CausesValidation="false" CommandName="Delete"
                                                            ImageUrl="Images/Delete.gif" ToolTip="Delete Organisation Detail" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="ExperienceMonth" runat="server" MaxLength="50" Text='<%# DataBinder.Eval(Container, "DataItem.ExperienceMonth") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="ExperienceYear" runat="server" MaxLength="50" Text='<%# DataBinder.Eval(Container, "DataItem.ExperienceYear") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                        <asp:HiddenField ID="EMPId" runat="server" />
                                        <asp:HiddenField ID="hfExperienceTotalMonths" runat="server" />
                                        <asp:HiddenField ID="hfExperienceTotalYears" runat="server" />
                                        <asp:HiddenField ID="HfIsDataModified" runat="server" />
                                    </div>
                                </asp:Panel>
                               </div>
                            <div id="buttonControl">
                                <br />
                                <table width="100%">
                                    <tr id="RelevantRow" runat="server">
                                        <td style="width: 28%" class="txtstyle">
                                            <asp:Label ID="lblNonReleventExperience" runat="server" Text="Do you have Non Relevant Experience?"></asp:Label>
                                        </td>
                                        <td style="width: 60%">
                                            <asp:RadioButtonList ID="rblNonReleventExperience" runat="server" RepeatDirection="Horizontal"
                                                AutoPostBack="True" OnSelectedIndexChanged="rblNonReleventExperience_SelectedIndexChanged"
                                                Width="80px">
                                                <asp:ListItem Value="True">Yes</asp:ListItem>
                                                <asp:ListItem Selected="True" Value="False">No</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div id="divNonReleventDetail" runat="server" visible="false">
                                <table width="100%" class="detailsbg">
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label1" runat="server" Text="Non Relevant Experience" CssClass="detailsheader"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <asp:Panel ID="NonReleventDetails" runat="server">
                                    <table width="100%">
                                        <tr>
                                            <td style="width: 25%" class="txtstyle">
                                                <asp:Label ID="lblRCompanyName" runat="server" Text="Company Name"></asp:Label>
                                                <label class="mandatorymark">
                                                    *</label>
                                            </td>
                                            <td style="width: 25%">
                                                <asp:TextBox ID="txtRCompanyName" runat="server" ToolTip="Enter Company Name" MaxLength="30"
                                                    CssClass="mandatoryField"></asp:TextBox>
                                                <span id="spanRCompanyName" runat="server">
                                                    <img id="imgRCompanyName" runat="server" src="Images/cross.png" alt="" style="display: none;
                                                        border: none;" />
                                                </span>
                                            </td>
                                            <td style="width: 25%" class="txtstyle">
                                                <asp:Label ID="lblRPositionHeld" runat="server" Text="Position Held"></asp:Label>
                                                <label class="mandatorymark">
                                                    *</label>
                                            </td>
                                            <td style="width: 25%">
                                                <asp:TextBox ID="txtRPositionHeld" runat="server" CssClass="mandatoryField" ToolTip="Enter Position Held"
                                                    MaxLength="20"></asp:TextBox>
                                                <span id="spanRPositionHeld" runat="server">
                                                    <img id="imgRPositionHeld" runat="server" src="Images/cross.png" alt="" style="display: none;
                                                        border: none;" />
                                                </span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 25%" class="txtstyle">
                                                <asp:Label ID="lblRWorkingSince" runat="server" Text="Working Since"></asp:Label>
                                                <label class="mandatorymark">
                                                    *</label>
                                            </td>
                                            <td style="width: 25%">
                                                <span id="spanzRMonthSince" runat="server">
                                                    <asp:DropDownList ID="ddlRMonthSince" runat="server" CssClass="mandatoryField" Width="90px"
                                                        OnSelectedIndexChanged="ddlRMonthSince_SelectedIndexChanged" AutoPostBack="true">
                                                    </asp:DropDownList>
                                                </span><span id="spanRMonthSince" runat="server">
                                                    <img id="imgRMonthSince" runat="server" src="Images/cross.png" alt="" style="display: none;
                                                        border: none;" />
                                                </span><span id="spanzRYearSince" runat="server">
                                                    <asp:DropDownList ID="ddlRYearSince" runat="server" CssClass="mandatoryField" Width="57px"
                                                        OnSelectedIndexChanged="ddlRYearSince_SelectedIndexChanged" AutoPostBack="true">
                                                    </asp:DropDownList>
                                                </span><span id="spanRYearSince" runat="server">
                                                    <img id="imgRYearSince" runat="server" src="Images/cross.png" alt="" style="display: none;
                                                        border: none;" />
                                                </span>
                                            </td>
                                            <td style="width: 25%" class="txtstyle">
                                                <asp:Label ID="lblRWorkingTill" runat="server" Text="Working Till"></asp:Label>
                                                <label class="mandatorymark">
                                                    *</label>
                                            </td>
                                            <td style="width: 25%">
                                                <span id="spanzRMonthTill" runat="server">
                                                    <asp:DropDownList ID="ddlRMonthTill" runat="server" Width="90px" CssClass="mandatoryField"
                                                        OnSelectedIndexChanged="ddlRMonthTill_SelectedIndexChanged" AutoPostBack="true">
                                                    </asp:DropDownList>
                                                </span><span id="spanRMonthTill" runat="server">
                                                    <img id="imgRMonthTill" runat="server" src="Images/cross.png" alt="" style="display: none;
                                                        border: none;" />
                                                </span><span id="spanzRYearTill" runat="server">
                                                    <asp:DropDownList ID="ddlRYearTill" runat="server" Width="57px" CssClass="mandatoryField"
                                                        OnSelectedIndexChanged="ddlRYearTill_SelectedIndexChanged" AutoPostBack="true">
                                                    </asp:DropDownList>
                                                </span><span id="spanRYearTill" runat="server">
                                                    <img id="imgRYearTill" runat="server" src="Images/cross.png" alt="" style="display: none;
                                                        border: none;" />
                                                </span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 25%" class="txtstyle">
                                                <asp:Label ID="lblRExperience" runat="server" Text="Experience"></asp:Label>
                                                <label class="mandatorymark">
                                                    *</label>
                                            </td>
                                            <td style="width: 25%">
                                                <asp:TextBox ID="txtRExperienceYear" runat="server" CssClass="mandatoryField" ToolTip="Enter Experience"
                                                    MaxLength="3" ReadOnly="True" Width="35px"></asp:TextBox>&nbsp;Yrs. <span id="spanRExperienceYear"
                                                        runat="server">
                                                        <img id="imgRExperienceYear" runat="server" src="Images/cross.png" alt="" style="display: none;
                                                            border: none;" />
                                                    </span>
                                                <asp:TextBox ID="txtRExperienceMonth" runat="server" CssClass="mandatoryField" ToolTip="Enter Experience"
                                                    MaxLength="3" ReadOnly="True" Width="35px"></asp:TextBox>&nbsp;Mths. <span id="spanRExperienceMonth"
                                                        runat="server">
                                                        <img id="imgRExperienceMonth" runat="server" src="Images/cross.png" alt="" style="display: none;
                                                            border: none;" />
                                                    </span>
                                            </td>
                                            <td colspan="2" align="right">
                                                <asp:Button ID="btnUpdateRow" TabIndex="14" runat="server" Text="Update" CssClass="button"
                                                    OnClick="btnUpdateRow_Click" Visible="false"></asp:Button>
                                                <asp:Button ID="btnCancelRow" TabIndex="14" runat="server" Text="Cancel" CssClass="button"
                                                    OnClick="btnCancelRow_Click" Visible="false"></asp:Button>
                                                <asp:Button ID="btnAddRow" runat="server" CssClass="button" OnClick="btnAddRow_Click"
                                                    TabIndex="14" Text="Add Row" />
                                            </td>
                                        </tr>
                                    </table>
                                    <div id="divNonReleventDetails">
                                        <asp:GridView ID="gvOrgNonReleventDetails" runat="server" Width="100%" AutoGenerateColumns="False"
                                            OnRowDeleting="gvOrgNonReleventDetails_RowDeleting" OnRowEditing="gvOrgNonReleventDetails_RowEditing">
                                            <HeaderStyle CssClass="addrowheader" />
                                            <AlternatingRowStyle CssClass="alternatingrowStyle" />
                                            <RowStyle Height="20px" CssClass="textstyle" />
                                            <Columns>
                                                <asp:BoundField HeaderText="Company Name" DataField="CompanyName"></asp:BoundField>
                                                <asp:BoundField HeaderText="Position Held" DataField="Designation"></asp:BoundField>
                                                <asp:BoundField HeaderText="Working Since" DataField="WorkingSince"></asp:BoundField>
                                                <asp:BoundField HeaderText="Working Till" DataField="WorkingTill"></asp:BoundField>
                                                <asp:BoundField HeaderText="Experience" DataField="Experience"></asp:BoundField>
                                                <asp:TemplateField Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="OrganisationId" runat="server" MaxLength="50" Text='<%# DataBinder.Eval(Container, "DataItem.OrganisationId") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Mode" runat="server" MaxLength="50" Text='<%# DataBinder.Eval(Container, "DataItem.Mode") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="MonthSince" runat="server" MaxLength="50" Text='<%# DataBinder.Eval(Container, "DataItem.MonthSince") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="YearSince" runat="server" MaxLength="50" Text='<%# DataBinder.Eval(Container, "DataItem.YearSince") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="MonthTill" runat="server" MaxLength="50" Text='<%# DataBinder.Eval(Container, "DataItem.MonthTill") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="YearTill" runat="server" MaxLength="50" Text='<%# DataBinder.Eval(Container, "DataItem.YearTill") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="true">
                                                    <ItemTemplate>
                                                        <asp:ImageButton runat="server" ID="imgBtnEdit" CausesValidation="false" CommandName="Edit"
                                                            ImageUrl="Images/Edit.gif" ToolTip="Edit Organisation Detail" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="true">
                                                    <ItemTemplate>
                                                        <asp:ImageButton runat="server" ID="imgBtnDelete" CausesValidation="false" CommandName="Delete"
                                                            ImageUrl="Images/Delete.gif" ToolTip="Delete Organisation Detail" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="ExperienceMonth" runat="server" MaxLength="50" Text='<%# DataBinder.Eval(Container, "DataItem.ExperienceMonth") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="ExperienceYear" runat="server" MaxLength="50" Text='<%# DataBinder.Eval(Container, "DataItem.ExperienceYear") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                        <asp:HiddenField ID="hfTotalNonExperience" runat="server" />
                                    </div>
                                 </asp:Panel>
                                    
                               
                            </div>
                            <table width="100%">
                                <tr align="right">
                                    <td style="width: 90%">
                                    </td>
                                    <td style="width: 10%">
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                            <div id="Div2">
                                <table width="100%">
                                    <tr align="right">
                                        <td style="width: 30%">
                                        </td>
                                        <td style="width: 70%" align="right">
                                            <asp:Button ID="btnSave" runat="server" CausesValidation="true" CssClass="button"
                                                OnClick="btnSave_Click" TabIndex="18" Text="Save All" />
                                            <asp:Button ID="btnEdit" runat="server" CssClass="button" Text="Edit" OnClick="btnEdit_Click" />
                                            <asp:Button ID="btnCancel" runat="server" CssClass="button" Text="Cancel" OnClick="CancelDetails_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</asp:Content>
