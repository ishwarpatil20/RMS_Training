<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true"
    CodeFile="Employee.aspx.cs" Inherits="Employee" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMainContent" runat="Server">

    <script type="text/javascript">
    
    function $(v){return document.getElementById(v);}
    
    function popUpEmployeeEmailPopulate()
    {
        var txtEmailID = $( '<%=txtEmailID.ClientID %>').id 
        
        window.open('EmployeesEmailList.aspx?field=' + txtEmailID +'','EmployeesEmailList','titlebar=no,left=0,top=0,width=500,height=550,resizable=no');                                                     
            
       //window.showModalDialog("Popup.aspx?mode=search",null, "dialogHeight:300px; dialogLeft:50px; dialogWidth:250px;"); 
    }
    
    function popUpEmployeeSearch()
        {
            //debugger;
                                                    
             var txtReportingTo = $( '<%=txtReportingTo.ClientID %>').id   
             var hidReportingTo = $( '<%=hidReportingTo.ClientID %>').id
             var hidReportingToValue = $( '<%=hidReportingTo.ClientID %>').value
                
            window.open('EmployeesList.aspx?field=' + txtReportingTo +'&fieldres='+ hidReportingTo + '&hidFldValue=' + hidReportingToValue + '','ProjectManagerPopup','titlebar=no,left=0,top=0,width=1400,height=450,resizable=yes');                                                     
                
           //window.showModalDialog("Popup.aspx?mode=search",null, "dialogHeight:300px; dialogLeft:50px; dialogWidth:250px;"); 
        }
        
    function popUpEmployeeReleasePopulate(employeeid)
        {
            var empid = employeeid * 1; 
        
            window.open('EmpSetReleaseDate.aspx?field=' + empid +'','EmployeeReleaseDate','titlebar=no,left=0,top=0,width=1000,height=550,resizable=no');                                                     
            
            //window.showModalDialog("Popup.aspx?mode=search",null, "dialogHeight:300px; dialogLeft:50px; dialogWidth:250px;"); 
        }
        
        function ButtonClickValidate()
    {
        
        var lblMandatory;
        var spanlist = "";
        
        var id = "<%=rblSamePerAddress.ClientID%>"; 
        
        //if current & perment address is different
        if(document.getElementById(id+"_"+'1'.toString()).checked==true)  
        {
            var PerAddress = $('<%=txtPAddress.ClientID %>').id
            var PerStreetName = $('<%=txtPStreetName.ClientID %>').id
            var PerCity = $('<%=txtPCity.ClientID %>').id
            var PerPinCode = $('<%=txtPPinCode.ClientID %>').id
              
            // employee details controls 
            var Prefix = $('<%=ddlPrefix.ClientID %>').value;
            var EmployeeCode = $('<%=txtEmployeeCode.ClientID %>').id
            var FirstName = $('<%=txtFirstName.ClientID %>').id
            var LastName = $('<%=txtLastName.ClientID %>').id
            var EmailID = $('<%=txtEmailID.ClientID %>').id
            var Department = $('<%=ddlDepartment.ClientID %>').value;
            var EmployeeType = $('<%=ddlEmployeeType.ClientID %>').value;
            var Band = $('<%=ddlBand.ClientID %>').value;
            var Designation = $('<%=ddlDesignation.ClientID %>').value;
            var JoiningDate = $('<%=txtJoiningDate.ClientID %>').id
            var ReportingTo = $('<%=txtReportingTo.ClientID %>').id
            var Status = $('<%=ddlStatus.ClientID %>').value;
            //        var LastWorkDay = $('<%=txtLastWorkDay.ClientID %>').id
            //        var ResignationDate = $('<%=txtResignationDate.ClientID %>').id
            //        var ResignationReason = $('<%=txtResignationReason.ClientID %>').id
            //        
            // personal details controls       
            var DOB = $('<%=txtDOB.ClientID %>').id
            var Gender = $('<%=ddlGender.ClientID %>').value;
            var MaritalStatus = $('<%=ddlMaritalStatus.ClientID %>').value;
            var FatherName = $('<%=txtFatherName.ClientID %>').id
            var BloodGroup = $('<%=txtBloodGroup.ClientID %>').id
            var CurrentAddress = $('<%=txtCurrentAddress.ClientID %>').id
            var CStreetName = $('<%=txtCStreetName.ClientID %>').id
            var CCity = $('<%=txtCCity.ClientID %>').id
            var CPinCode = $('<%=txtCPinCode.ClientID %>').id
//            var Phone = $('<%=txtPhone.ClientID %>').id
//            var Mobile = $('<%=txtMobile.ClientID %>').id
            var EmergencyContactNo = $('<%=txtEmergencyContactNo.ClientID %>').id
            
                    if(Prefix == "" || Prefix == "SELECT")
                    {
                        var sPrefix = $('<%=spanzPrefix.ClientID %>').id;
                        spanlist = sPrefix +",";
                    }
                    
                    if(Department == "" || Department == "SELECT")
                    {
                        var sDepartment = $('<%=spanzDepartment.ClientID %>').id;
                        spanlist = spanlist + sDepartment +",";
                    }
                    
                    if(EmployeeType == "" || EmployeeType == "SELECT")
                    {
                        var sEmployeeType = $('<%=spanzEmployeeType.ClientID %>').id;
                        spanlist = spanlist + sEmployeeType +",";
                    }
                    
                    if(Band == "" || Band == "SELECT")
                    {
                        var sBand = $('<%=spanzBand.ClientID %>').id;
                        spanlist = spanlist + sBand +",";
                    }
                    
                    if(Designation == "" || Designation == "SELECT")
                    {
                        var sDesignation= $('<%=spanzDesignation.ClientID %>').id;
                        spanlist = spanlist + sDesignation +",";
                    }
                    
                    if(Status == "" || Status == "SELECT")
                    {
                        var sStatus = $('<%=spanzStatus.ClientID %>').id;
                        spanlist = spanlist + sStatus +",";
                    }
                    
                    if(Gender == "" || Gender == "SELECT")
                    {
                        var sGender= $('<%=spanzGender.ClientID %>').id;
                        spanlist = spanlist + sGender +",";
                    }
                    
                    if(MaritalStatus == "" || MaritalStatus == "SELECT")
                    {
                        var sMaritalStatus = $('<%=spanzMaritalStatus.ClientID %>').id;
                        spanlist = spanlist + sMaritalStatus +",";
                    }
                    
                    if(MaritalStatus == "M")
                    {
                        var SpouseName = $('<%=txtSpouseName.ClientID %>').id
                        
                        if(spanlist == "")
                        {
                           //var controlList =EmployeeCode +"," + FirstName +"," + LastName +"," + EmailID +"," + JoiningDate +"," + ReportingTo +"," + DOB +"," + FatherName +"," + BloodGroup +"," + CurrentAddress +"," + CStreetName +"," + CCity +"," + CPinCode +"," + Phone +"," + Mobile +"," + EmergencyContactNo+"," + PerAddress+"," +PerStreetName+"," +PerCity+"," +PerPinCode + ","+SpouseName;
                           var controlList =EmployeeCode +"," + FirstName +"," + LastName +"," + EmailID +"," + JoiningDate +"," + ReportingTo +"," + DOB +"," + FatherName +"," + BloodGroup +"," + CurrentAddress +"," + CStreetName +"," + CCity +"," + CPinCode +"," + EmergencyContactNo+"," + PerAddress+"," +PerStreetName+"," +PerCity+"," +PerPinCode + ","+SpouseName;
                        }
                        else
                        {
                           var controlList = spanlist + EmployeeCode +"," + FirstName +"," + LastName +"," + EmailID +"," + JoiningDate +"," + ReportingTo +"," + DOB +"," + FatherName +"," + BloodGroup +"," + CurrentAddress +"," + CStreetName +"," + CCity +"," + CPinCode +"," + EmergencyContactNo+"," + PerAddress+"," +PerStreetName+"," +PerCity+"," +PerPinCode + ","+SpouseName;
                        }
                    }
                    else
                    {
                        if(spanlist == "")
                        {
                           //var controlList =EmployeeCode +"," + FirstName +"," + LastName +"," + EmailID +"," + JoiningDate +"," + ReportingTo +"," + DOB +"," + FatherName +"," + BloodGroup +"," + CurrentAddress +"," + CStreetName +"," + CCity +"," + CPinCode +"," + Phone +"," + Mobile +"," + EmergencyContactNo+"," + PerAddress+"," +PerStreetName+"," +PerCity+"," +PerPinCode;
                           var controlList =EmployeeCode +"," + FirstName +"," + LastName +"," + EmailID +"," + JoiningDate +"," + ReportingTo +"," + DOB +"," + FatherName +"," + BloodGroup +"," + CurrentAddress +"," + CStreetName +"," + CCity +"," + CPinCode +","  + EmergencyContactNo+"," + PerAddress+"," +PerStreetName+"," +PerCity+"," +PerPinCode;
                        }
                        else
                        {
                           var controlList = spanlist + EmployeeCode +"," + FirstName +"," + LastName +"," + EmailID +"," + JoiningDate +"," + ReportingTo +"," + DOB +"," + FatherName +"," + BloodGroup +"," + CurrentAddress +"," + CStreetName +"," + CCity +"," + CPinCode +","+ EmergencyContactNo+"," + PerAddress+"," +PerStreetName+"," +PerCity+"," +PerPinCode;
                        }
                    }
        }
        else
        {
            // employee details controls 
            var Prefix = $('<%=ddlPrefix.ClientID %>').value;
            var EmployeeCode = $('<%=txtEmployeeCode.ClientID %>').id
            var FirstName = $('<%=txtFirstName.ClientID %>').id
            var LastName = $('<%=txtLastName.ClientID %>').id
            var EmailID = $('<%=txtEmailID.ClientID %>').id
            var Department = $('<%=ddlDepartment.ClientID %>').value;
            var EmployeeType = $('<%=ddlEmployeeType.ClientID %>').value;
            var Band = $('<%=ddlBand.ClientID %>').value;
            var Designation = $('<%=ddlDesignation.ClientID %>').value;
            var JoiningDate = $('<%=txtJoiningDate.ClientID %>').id
            var ReportingTo = $('<%=txtReportingTo.ClientID %>').id
            var Status = $('<%=ddlStatus.ClientID %>').value;
            //        var LastWorkDay = $('<%=txtLastWorkDay.ClientID %>').id
            //        var ResignationDate = $('<%=txtResignationDate.ClientID %>').id
            //        var ResignationReason = $('<%=txtResignationReason.ClientID %>').id
            //        
            // personal details controls       
            var DOB = $('<%=txtDOB.ClientID %>').id
            var Gender = $('<%=ddlGender.ClientID %>').value;
            var MaritalStatus = $('<%=ddlMaritalStatus.ClientID %>').value;
            var FatherName = $('<%=txtFatherName.ClientID %>').id
            var BloodGroup = $('<%=txtBloodGroup.ClientID %>').id
            var CurrentAddress = $('<%=txtCurrentAddress.ClientID %>').id
            var CStreetName = $('<%=txtCStreetName.ClientID %>').id
            var CCity = $('<%=txtCCity.ClientID %>').id
            var CPinCode = $('<%=txtCPinCode.ClientID %>').id
            var Phone = $('<%=txtPhone.ClientID %>').id
            var Mobile = $('<%=txtMobile.ClientID %>').id
            var EmergencyContactNo = $('<%=txtEmergencyContactNo.ClientID %>').id
            
                    if(Prefix == "" || Prefix == "SELECT")
                    {
                        var sPrefix = $('<%=spanzPrefix.ClientID %>').id;
                        spanlist = sPrefix +",";
                    }
                    
                    if(Department == "" || Department == "SELECT")
                    {
                        var sDepartment = $('<%=spanzDepartment.ClientID %>').id;
                        spanlist = spanlist + sDepartment +",";
                    }
                    
                    if(EmployeeType == "" || EmployeeType == "SELECT")
                    {
                        var sEmployeeType = $('<%=spanzEmployeeType.ClientID %>').id;
                        spanlist = spanlist + sEmployeeType +",";
                    }
                    
                    if(Band == "" || Band == "SELECT")
                    {
                        var sBand = $('<%=spanzBand.ClientID %>').id;
                        spanlist = spanlist + sBand +",";
                    }
                    
                    if(Designation == "" || Designation == "SELECT")
                    {
                        var sDesignation= $('<%=spanzDesignation.ClientID %>').id;
                        spanlist = spanlist + sDesignation +",";
                    }
                    
                    if(Status == "" || Status == "SELECT")
                    {
                        var sStatus = $('<%=spanzStatus.ClientID %>').id;
                        spanlist = spanlist + sStatus +",";
                    }
                    
                    if(Gender == "" || Gender == "SELECT")
                    {
                        var sGender= $('<%=spanzGender.ClientID %>').id;
                        spanlist = spanlist + sGender +",";
                    }
                    
                    if(MaritalStatus == "" || MaritalStatus == "SELECT")
                    {
                        var sMaritalStatus = $('<%=spanzMaritalStatus.ClientID %>').id;
                        spanlist = spanlist + sMaritalStatus +",";
                    }
                    
                    if(MaritalStatus == "M")
                    {
                        var SpouseName = $('<%=txtSpouseName.ClientID %>').id
                        
                        if(spanlist == "")
                        {
//                           var controlList =EmployeeCode +"," + FirstName +"," + LastName +"," + EmailID +"," + JoiningDate +"," + ReportingTo +"," + DOB +"," + FatherName +"," + BloodGroup +"," + CurrentAddress +"," + CStreetName +"," + CCity +"," + CPinCode +"," + Phone +"," + Mobile +"," + EmergencyContactNo + ","+SpouseName;
                             var controlList =EmployeeCode +"," + FirstName +"," + LastName +"," + EmailID +"," + JoiningDate +"," + ReportingTo +"," + DOB +"," + FatherName +"," + BloodGroup +"," + CurrentAddress +"," + CStreetName +"," + CCity +"," + CPinCode +"," + EmergencyContactNo + ","+SpouseName;

                        }
                        else
                        {
                           var controlList = spanlist + EmployeeCode +"," + FirstName +"," + LastName +"," + EmailID +"," + JoiningDate +"," + ReportingTo +"," + DOB +"," + FatherName +"," + BloodGroup +"," + CurrentAddress +"," + CStreetName +"," + CCity +"," + CPinCode +"," + EmergencyContactNo + ","+SpouseName;
                        }
                    }
                    else
                    {
                        if(spanlist == "")
                        {
//                            var controlList = EmployeeCode +"," + FirstName +"," + LastName +"," + EmailID +"," + JoiningDate +"," + ReportingTo +"," + DOB +"," + FatherName +"," + BloodGroup +"," + CurrentAddress +"," + CStreetName +"," + CCity +"," + CPinCode +"," + Phone +"," + Mobile +"," + EmergencyContactNo;
                              var controlList = EmployeeCode +"," + FirstName +"," + LastName +"," + EmailID +"," + JoiningDate +"," + ReportingTo +"," + DOB +"," + FatherName +"," + BloodGroup +"," + CurrentAddress +"," + CStreetName +"," + CCity +"," + CPinCode +"," + EmergencyContactNo;
                        }
                        else
                        {
                            var controlList = spanlist + EmployeeCode +"," + FirstName +"," + LastName +"," + EmailID +"," + JoiningDate +"," + ReportingTo +"," + DOB +"," + FatherName +"," + BloodGroup +"," + CurrentAddress +"," + CStreetName +"," + CCity +"," + CPinCode +"," + EmergencyContactNo;
                        }
                    }
                 } 
                 
        if(ValidateControlOnClickEvent(controlList) == false)
        {
            lblMessage = $( '<%=lblMessage.ClientID %>')
            lblMessage.innerText = "Please fill all mandatory fields.";
            lblMessage.style.color = "Red";
            
            lblConfirmMsg = $( '<%=lblConfirmMsg.ClientID %>')
            lblConfirmMsg.innerText="";
        }
        
        return ValidateControlOnClickEvent(controlList);
    }
 
    </script>

    <div class="detailsborder">
        <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
        <table width="100%">
            <tr>
                <td align="center" style="height: 25px; filter: progid:DXImageTransform.Microsoft.Gradient(endColorstr='#7590C8', startColorstr='#0C359A', gradientType='1');">
                    <asp:Label ID="lblEmployeeDetails" runat="server" Text="Employee Details" CssClass="header"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="height: 1pt">
                </td>
            </tr>
        </table>
        <table width="100%">
            <tr>
                <td style="text-align: center">
                    <asp:ValidationSummary ID="valsumEmployee" runat="server" ValidationGroup="vgEmployee"
                        ShowMessageBox="false" ShowSummary="false" DisplayMode="BulletList" />
                    <asp:ValidationSummary ID="valsumCategory" runat="server" ValidationGroup="vgCategory"
                        ShowMessageBox="true" ShowSummary="false" DisplayMode="BulletList" />
                    <asp:ValidationSummary ID="valsumDomain" runat="server" ValidationGroup="vgDomain"
                        ShowMessageBox="true" ShowSummary="false" DisplayMode="BulletList" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblConfirmMsg" runat="server" CssClass="Messagetext"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <%--<asp:Label ID="lblMessage" runat="server" ForeColor="Red" Width="400px"></asp:Label>--%>
                    <asp:Label ID="lblMessage" runat="server" CssClass="text"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblMandatory" runat="server" Font-Names="Verdana" Font-Size="9pt">Fields marked with <span class="mandatorymark">*</span> are mandatory.</asp:Label>
                </td>
            </tr>
        </table>
        <div id="divEmpDetails" class="detailsborder">
            <table width="100%" class="detailsbg">
                <tr>
                    <td>
                        <asp:Label ID="lblEmployeeDetailsGroup" runat="server" Text="Employee Details" CssClass="detailsheader"></asp:Label>
                    </td>
                </tr>
            </table>
            <asp:Panel ID="pnlEmployeeDetails" runat="server">
                <table width="100%">
                    <tr style="width: 100%">
                        <td style="width: 25%">
                            <asp:Label ID="lblPrefix" runat="server" Text="Prefix" CssClass="textstyle"></asp:Label>
                            <label class="mandatorymark">
                                *</label>
                        </td>
                        <td style="width: 25%">
                            <span id="spanzPrefix" runat="server">
                                <asp:DropDownList ID="ddlPrefix" runat="server" Width="160px" ToolTip="Select Location"
                                    CssClass="mandatoryField" TabIndex="11">
                                </asp:DropDownList>
                            </span>
                        </td>
                        <td colspan="2" rowspan="4" align="right">
                            <asp:Image ID="imgEmp" runat="server" Height="100px" Width="100px" />
                            <asp:ImageButton ID="imgbtnUpload" runat="server" ImageUrl="~/Images/upload.JPG"
                                OnClick="imgbtnUpload_Click" ToolTip="Upload Image" />
                        </td>
                    </tr>
                    <tr style="width: 100%">
                        <td style="width: 25%">
                            <asp:Label ID="lblEmployeeCode" runat="server" Text="Employee Code" CssClass="textstyle"></asp:Label>
                            <label class="mandatorymark">
                                *</label>
                        </td>
                        <td style="width: 25%">
                            <asp:TextBox ID="txtEmployeeCode" runat="server" CssClass="mandatoryField" MaxLength="10"
                                ToolTip="Enter Employee Code" BorderStyle="Solid" TabIndex="1" Width="155px">
                            </asp:TextBox>
                            <span id="spanEmployeeCode" runat="server">
                                <img id="imgEmployeeCode" runat="server" src="Images/cross.png" alt="" style="display: none;
                                    border: none;" />
                            </span>
                        </td>
                    </tr>
                    <tr style="width: 100%">
                        <td style="width: 25%">
                            <asp:Label ID="lblFirstName" runat="server" Text="First Name" CssClass="textstyle"></asp:Label>
                            <label class="mandatorymark">
                                *</label>
                        </td>
                        <td style="width: 25%">
                            <asp:TextBox ID="txtFirstName" runat="server" CssClass="mandatoryField" MaxLength="20"
                                ToolTip="Enter First name" BorderStyle="Solid" TabIndex="1" Width="155px">
                            </asp:TextBox>
                            <span id="spanFirstName" runat="server">
                                <img id="imgFirstName" runat="server" src="Images/cross.png" alt="" style="display: none;
                                    border: none;" />
                            </span>
                        </td>
                    </tr>
                    <tr style="width: 100%">
                        <td style="width: 25%">
                            <asp:Label ID="lblLastName" runat="server" Text="Last Name" CssClass="textstyle"></asp:Label>
                            <label class="mandatorymark">
                                *</label>
                        </td>
                        <td style="width: 25%">
                            <asp:TextBox ID="txtLastName" runat="server" CssClass="mandatoryField" MaxLength="20"
                                ToolTip="Enter Last Name" BorderStyle="Solid" TabIndex="1" Width="155px">
                            </asp:TextBox>
                            <span id="spanLastName" runat="server">
                                <img id="imgLastName" runat="server" src="Images/cross.png" alt="" style="display: none;
                                    border: none;" />
                            </span>
                        </td>
                    </tr>
                    <tr style="width: 100%">
                        <td style="width: 25%">
                            <asp:Label ID="lblEmailID" runat="server" Text="Email ID" CssClass="textstyle"></asp:Label>
                            <label class="mandatorymark">
                                *</label>
                        </td>
                        <td style="width: 25%">
                            <asp:TextBox ID="txtEmailID" runat="server" CssClass="mandatoryField" MaxLength="50"
                                ToolTip="Enter Email ID" BorderStyle="Solid" TabIndex="1" Width="155px">
                            </asp:TextBox>
                            <img id="imgEmpEmailPopulate" runat="server" src="Images/find.png" alt="" />
                            <span id="spanEmailID" runat="server">
                                <img id="imgLastUsedDateError" runat="server" src="Images/cross.png" alt="" style="display: none;
                                    border: none;" />
                            </span>
                        </td>
                        <td style="width: 25%">
                            <asp:Label ID="lblBrowseImage" runat="server" Text="Browse Image" CssClass="textstyle"></asp:Label>
                        </td>
                        <td style="width: 25%">
                            <asp:FileUpload ID="FileUpload1" runat="server" Width="250px" />
                        </td>
                    </tr>
                    <tr style="width: 100%">
                        <td style="width: 25%">
                            <asp:Label ID="lblGroup" runat="server" Text="Department" CssClass="textstyle"></asp:Label>
                            <label class="mandatorymark">
                                *</label>
                        </td>
                        <td style="width: 25%">
                            <span id="spanzDepartment" runat="server">
                                <asp:DropDownList ID="ddlDepartment" runat="server" Width="160px" ToolTip="Select Group"
                                    CssClass="mandatoryField" TabIndex="11" AutoPostBack="True" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged">
                                </asp:DropDownList>
                            </span>
                        </td>
                        <td style="width: 25%">
                            <asp:Label ID="lblEmployeeType" runat="server" Text="Employee Type" CssClass="textstyle"></asp:Label><label
                                class="mandatorymark">*</label>
                        </td>
                        <td style="width: 25%">
                            <span id="spanzEmployeeType" runat="server">
                                <asp:DropDownList ID="ddlEmployeeType" runat="server" Width="160px" ToolTip="Select Employee Status"
                                    CssClass="mandatoryField" TabIndex="11" AutoPostBack="True">
                                </asp:DropDownList>
                            </span>
                        </td>
                    </tr>
                    <tr style="width: 100%">
                        <td style="width: 25%">
                            <asp:Label ID="lblBand" runat="server" Text="Band" CssClass="textstyle"></asp:Label>
                            <label class="mandatorymark">
                                *</label>
                        </td>
                        <td style="width: 25%">
                            <span id="spanzBand" runat="server">
                                <asp:DropDownList ID="ddlBand" runat="server" Width="160px" ToolTip="Select Band"
                                    CssClass="mandatoryField" TabIndex="11">
                                </asp:DropDownList>
                            </span>
                        </td>
                        <td style="width: 25%">
                            <asp:Label ID="lblDesignation" runat="server" Text="Designation" CssClass="textstyle"></asp:Label>
                            <label class="mandatorymark">
                                *</label>
                        </td>
                        <td style="width: 25%">
                            <span id="spanzDesignation" runat="server">
                                <asp:DropDownList ID="ddlDesignation" runat="server" Width="160px" ToolTip="Select Designation"
                                    CssClass="mandatoryField" TabIndex="11">
                                </asp:DropDownList>
                            </span>
                        </td>
                    </tr>
                    <tr style="width: 100%">
                        <td style="width: 25%">
                            <asp:Label ID="lblJoiningDate" runat="server" Text="Joining Date" CssClass="textstyle"></asp:Label>
                            <label class="mandatorymark">
                                *</label>
                        </td>
                        <td style="width: 25%">
                            <asp:TextBox ID="txtJoiningDate" runat="server" CssClass="mandatoryField" MaxLength="30"
                                ToolTip="Enter Joining Date" BorderStyle="Solid" TabIndex="1" Width="155px">
                            </asp:TextBox>
                            <asp:ImageButton ID="imgJoiningDate" runat="server" ImageUrl="Images/Calendar_scheduleHS.png"
                                CausesValidation="false" ImageAlign="AbsMiddle" TabIndex="5" />
                            <cc1:CalendarExtender ID="calendarJoiningDate" runat="server" PopupButtonID="imgJoiningDate"
                                TargetControlID="txtJoiningDate" Format="dd/MM/yyyy">
                            </cc1:CalendarExtender>
                            <span id="spanJoiningDate" runat="server">
                                <img id="imgJoiningDateError" runat="server" src="Images/cross.png" alt="" style="display: none;
                                    border: none;" />
                            </span>
                        </td>
                        <td style="width: 25%">
                            <asp:Label ID="lbReportingTo" runat="server" Text="Reporting To" CssClass="textstyle"></asp:Label>
                            <label class="mandatorymark">
                                *</label>
                        </td>
                        <td style="width: 25%">
                            <asp:TextBox ID="txtReportingTo" runat="server" CssClass="mandatoryField" MaxLength="30"
                                ToolTip="Enter Reporting To" BorderStyle="Solid" TabIndex="1" Width="155px" TextMode="MultiLine">
                            </asp:TextBox>
                            <img id="imgReportingToPopulate" runat="server" src="Images/find.png" alt="" />
                            <span id="spanReportingTo" runat="server">
                                <img id="imgReportingTo" runat="server" src="Images/cross.png" alt="" style="display: none;
                                    border: none;" />
                            </span>
                        </td>
                    </tr>
                    <tr style="width: 100%">
                        <td style="width: 25%">
                            <asp:Label ID="lblStatus" runat="server" Text="Status" CssClass="textstyle"></asp:Label>
                            <label class="mandatorymark">
                                *</label>
                        </td>
                        <td style="width: 25%">
                            <span id="spanzStatus" runat="server">
                                <asp:DropDownList ID="ddlStatus" runat="server" Width="160px" ToolTip="Select Status"
                                    CssClass="mandatoryField" TabIndex="11">
                                </asp:DropDownList>
                            </span>
                        </td>
                        <td style="width: 25%">
                            <asp:Label ID="lbLstWrkDay" runat="server" Text="Last Working Day" CssClass="textstyle"></asp:Label>
                        </td>
                        <td style="width: 25%">
                            <asp:TextBox ID="txtLastWorkDay" runat="server" CssClass="mandatoryField" MaxLength="30"
                                ToolTip="Enter Last Working Day" BorderStyle="Solid" TabIndex="1" Width="155px">
                            </asp:TextBox>
                            <asp:ImageButton ID="imgLastWorkDay" runat="server" ImageUrl="Images/Calendar_scheduleHS.png"
                                CausesValidation="false" ImageAlign="AbsMiddle" TabIndex="5" />
                            <cc1:CalendarExtender ID="calendarLastWorkDay" runat="server" PopupButtonID="imgLastWorkDay"
                                TargetControlID="txtLastWorkDay" Format="dd/MM/yyyy">
                            </cc1:CalendarExtender>
                            <span id="spanLastWorkDay" runat="server">
                                <img id="imgLastWorkDayError" runat="server" src="Images/cross.png" alt="" style="display: none;
                                    border: none;" />
                            </span>
                        </td>
                    </tr>
                    <tr style="width: 100%">
                        <td style="width: 25%">
                            <asp:Label ID="lblResignationDate" runat="server" Text="Resignation Date" CssClass="textstyle"></asp:Label>
                        </td>
                        <td style="width: 25%">
                            <asp:TextBox ID="txtResignationDate" runat="server" CssClass="mandatoryField" MaxLength="30"
                                ToolTip="Enter Resignation Date" BorderStyle="Solid" TabIndex="1" Width="155px">
                            </asp:TextBox>
                            <asp:ImageButton ID="imgResignationDate" runat="server" ImageUrl="Images/Calendar_scheduleHS.png"
                                CausesValidation="false" ImageAlign="AbsMiddle" TabIndex="5" />
                            <cc1:CalendarExtender ID="calendarResignationDate" runat="server" PopupButtonID="imgResignationDate"
                                TargetControlID="txtResignationDate" Format="dd/MM/yyyy">
                            </cc1:CalendarExtender>
                            <span id="spanResignationDate" runat="server">
                                <img id="imgResignationDateError" runat="server" src="Images/cross.png" alt="" style="display: none;
                                    border: none;" />
                            </span>
                        </td>
                        <td style="width: 25%">
                            <asp:Label ID="lblResignationReason" runat="server" Text="Reason For Resignation"
                                CssClass="textstyle"></asp:Label>
                        </td>
                        <td style="width: 25%">
                            <asp:TextBox ID="txtResignationReason" runat="server" CssClass="mandatoryField" MaxLength="30"
                                ToolTip="Enter Reason For Resignation" BorderStyle="Solid" TabIndex="1" Width="155px"></asp:TextBox>
                            <span id="spanResignationReason" runat="server">
                                <img id="imgResignationReason" runat="server" src="Images/cross.png" alt="" style="display: none;
                                    border: none;" />
                            </span>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>
        <div>
            <table>
                <tr>
                    <td>
                    </td>
                </tr>
            </table>
        </div>
        <div id="divEmpPersonalDetails" class="detailsborder">
            <table width="100%" class="detailsbg">
                <tr>
                    <td>
                        <asp:Label ID="lbEmpPersonalDetails" runat="server" Text="Personal Details" CssClass="detailsheader"></asp:Label>
                    </td>
                </tr>
            </table>
            <asp:Panel ID="pnlEmpPersonalDetails" runat="server">
                <table width="100%">
                    <tr>
                        <td style="width: 25%">
                            <asp:Label ID="lblDOB" runat="server" Text="Date Of Birth"></asp:Label>
                            <label class="mandatorymark">
                                *</label>
                        </td>
                        <td style="width: 25%">
                            <asp:TextBox ID="txtDOB" runat="server"></asp:TextBox>
                            <asp:ImageButton ID="imgDOB" runat="server" ImageUrl="Images/Calendar_scheduleHS.png"
                                CausesValidation="false" ImageAlign="AbsMiddle" TabIndex="5" />
                            <cc1:CalendarExtender ID="CalendarDOB" runat="server" PopupButtonID="imgDOB" TargetControlID="txtDOB"
                                Format="dd/MM/yyyy">
                            </cc1:CalendarExtender>
                            <span id="spanDOB" runat="server">
                                <img id="imgDOBError" runat="server" src="Images/cross.png" alt="" style="display: none;
                                    border: none;" />
                            </span>
                        </td>
                        <td style="width: 25%">
                            <asp:Label ID="lblGender" runat="server" Text="Gender"></asp:Label>
                            <label class="mandatorymark">
                                *</label>
                        </td>
                        <td style="width: 25%">
                            <span id="spanzGender" runat="server">
                                <asp:DropDownList ID="ddlGender" runat="server" Width="155px">
                                    <asp:ListItem Selected="True" Value="SELECT">SELECT</asp:ListItem>
                                    <asp:ListItem Value="M">Male</asp:ListItem>
                                    <asp:ListItem Value="F">Female</asp:ListItem>
                                </asp:DropDownList>
                            </span>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 25%">
                            <asp:Label ID="lblMaritalStatus" runat="server" Text="Marital Status"></asp:Label>
                            <label class="mandatorymark">
                                *</label>
                        </td>
                        <td style="width: 25%">
                            <span id="spanzMaritalStatus" runat="server">
                                <asp:DropDownList ID="ddlMaritalStatus" runat="server" Width="155px" OnSelectedIndexChanged="ddlMaritalStatus_SelectedIndexChanged"
                                    AutoPostBack="true">
                                    <asp:ListItem Selected="True" Value="SELECT">SELECT</asp:ListItem>
                                    <asp:ListItem Value="M">Married</asp:ListItem>
                                    <asp:ListItem Value="S">Single</asp:ListItem>
                                </asp:DropDownList>
                            </span>
                        </td>
                        <td style="width: 25%">
                            <asp:Label ID="lblFatherName" runat="server" Text="Father Name"></asp:Label>
                            <label class="mandatorymark">
                                *</label>
                        </td>
                        <td style="width: 25%">
                            <asp:TextBox ID="txtFatherName" runat="server" MaxLength="20"></asp:TextBox>
                            <span id="spanFatherName" runat="server">
                                <img id="imgFatherName" runat="server" src="Images/cross.png" alt="" style="display: none;
                                    border: none;" />
                            </span>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 25%">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:Label ID="lblSpouseName" runat="server" Text="Spouse Name"></asp:Label>
                                    <label id="lblmandatorymark" class="mandatorymark" runat="server">
                                        *</label>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ddlMaritalStatus" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                        <td style="width: 25%">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:TextBox ID="txtSpouseName" runat="server" MaxLength="20"></asp:TextBox>
                                    <span id="spanSpouseName" runat="server">
                                        <img id="imgSpouseName" runat="server" src="Images/cross.png" alt="" style="display: none;
                                            border: none;" />
                                    </span>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ddlMaritalStatus" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                        <td style="width: 25%">
                            <asp:Label ID="lblBloodGroup" runat="server" Text="Blood Group"></asp:Label>
                            <label class="mandatorymark">
                                *</label>
                        </td>
                        <td style="width: 25%;">
                            <asp:TextBox ID="txtBloodGroup" runat="server" MaxLength="10"></asp:TextBox>
                            <span id="spanBloodGroup" runat="server">
                                <img id="imgBloodGroup" runat="server" src="Images/cross.png" alt="" style="display: none;
                                    border: none;" />
                            </span>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 25%">
                            <asp:Label ID="lblCurrentAddress" runat="server" Text="Current Address"></asp:Label>
                            <label class="mandatorymark">
                                *</label>
                        </td>
                        <td style="width: 25%">
                            <asp:TextBox ID="txtCurrentAddress" runat="server" MaxLength="25"></asp:TextBox>
                            <span id="spanCurrentAddress" runat="server">
                                <img id="imgCurrentAddress" runat="server" src="Images/cross.png" alt="" style="display: none;
                                    border: none;" />
                            </span>
                        </td>
                        <td style="width: 25%">
                            <asp:Label ID="lblStreetName" runat="server" Text="Street Name"></asp:Label>
                            <label class="mandatorymark">
                                *</label>
                        </td>
                        <td style="width: 25%;">
                            <asp:TextBox ID="txtCStreetName" runat="server" MaxLength="25"></asp:TextBox>
                            <span id="spanCStreetName" runat="server">
                                <img id="imgCStreetName" runat="server" src="Images/cross.png" alt="" style="display: none;
                                    border: none;" />
                            </span>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 25%">
                            <asp:Label ID="lblCity" runat="server" Text="City"></asp:Label>
                            <label class="mandatorymark">
                                *</label>
                        </td>
                        <td style="width: 25%">
                            <asp:TextBox ID="txtCCity" runat="server" MaxLength="10"></asp:TextBox>
                            <span id="spanCCity" runat="server">
                                <img id="imgCCity" runat="server" src="Images/cross.png" alt="" style="display: none;
                                    border: none;" />
                            </span>
                        </td>
                        <td style="width: 25%">
                            <asp:Label ID="lblPinCode" runat="server" Text="Pin Code"></asp:Label>
                            <label class="mandatorymark">
                                *</label>
                        </td>
                        <td style="width: 25%;">
                            <asp:TextBox ID="txtCPinCode" runat="server" MaxLength="7"></asp:TextBox>
                            <span id="spanCPinCode" runat="server">
                                <img id="imgCPinCode" runat="server" src="Images/cross.png" alt="" style="display: none;
                                    border: none;" />
                            </span>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 25%">
                            <asp:Label ID="lblPhone" runat="server" Text="Phone(R)"></asp:Label>
                        </td>
                        <td style="width: 25%">
                            <asp:TextBox ID="txtPhone" runat="server" MaxLength="20"></asp:TextBox>
                            <span id="spanPhone" runat="server">
                                <img id="imgPhone" runat="server" src="Images/cross.png" alt="" style="display: none;
                                    border: none;" />
                            </span>
                        </td>
                        <td style="width: 25%">
                            <asp:Label ID="lblMobile" runat="server" Text="Mobile"></asp:Label>
                        </td>
                        <td style="width: 25%;">
                            <asp:TextBox ID="txtMobile" runat="server" MaxLength="20"></asp:TextBox>
                            <span id="spanMobile" runat="server">
                                <img id="imgMobile" runat="server" src="Images/cross.png" alt="" style="display: none;
                                    border: none;" />
                            </span>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 25%">
                            <asp:Label ID="lblEmergencyContactNo" runat="server" Text="Emergency Contact No."></asp:Label>
                            <label class="mandatorymark">
                                *</label>
                        </td>
                        <td style="width: 25%">
                            <asp:TextBox ID="txtEmergencyContactNo" runat="server" MaxLength="20"></asp:TextBox>
                            <span id="spanEmergencyContactNo" runat="server">
                                <img id="imgEmergencyContactNo" runat="server" src="Images/cross.png" alt="" style="display: none;
                                    border: none;" />
                            </span>
                        </td>
                        <td style="width: 25%">
                            <asp:Label ID="lblIsFresher" runat="server" Text="Are you a Fresher?"></asp:Label>
                            <label class="mandatorymark">
                                *</label>
                        </td>
                        <td style="width: 25%;">
                            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                <ContentTemplate>
                                    <asp:RadioButtonList ID="rblIsFresher" runat="server" RepeatDirection="Horizontal"
                                        Width="75%" AutoPostBack="True">
                                        <asp:ListItem Value="True">Yes</asp:ListItem>
                                        <asp:ListItem Value="False" Selected="True">No</asp:ListItem>
                                    </asp:RadioButtonList>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 25%">
                            <asp:Label ID="lblSamePerAddress" runat="server" Text="Is Permanet Address same as above"></asp:Label>
                        </td>
                        <td style="width: 25%">
                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                <ContentTemplate>
                                    <asp:RadioButtonList ID="rblSamePerAddress" runat="server" RepeatDirection="Horizontal"
                                        Width="75%" AutoPostBack="True" OnSelectedIndexChanged="rblSamePerAddress_SelectedIndexChanged">
                                        <asp:ListItem Selected="True" Value="True">Yes</asp:ListItem>
                                        <asp:ListItem Value="False">No</asp:ListItem>
                                    </asp:RadioButtonList>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td style="width: 25%">
                        </td>
                        <td style="width: 25%">
                        </td>
                    </tr>
                    <%--  <asp:Panel ID = "pnlPermanenetAddr" runat="server">
                <tr>
                    <td style="width: 25%">
                        <asp:Label ID="lblPermanentAddr" runat="server" Text="Permanent Address"></asp:Label>
                    </td>
                    <td style="width: 25%">
                        <asp:TextBox ID="txtPAddress" runat="server"></asp:TextBox>
                    </td>
                    <td style="width: 25%">
                        <asp:Label ID="lblPStreetName" runat="server" Text="Street Name"></asp:Label>
                    </td>
                    <td style="width: 25%;">
                        <asp:TextBox ID="txtPStreetName" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 25%">
                        <asp:Label ID="lblPCity" runat="server" Text="City"></asp:Label>
                    </td>
                    <td style="width: 25%">
                        <asp:TextBox ID="txtPCity" runat="server"></asp:TextBox>
                    </td>
                    <td style="width: 25%">
                        <asp:Label ID="lblPPinCode" runat="server" Text="Pin Code"></asp:Label>
                    </td>
                    <td style="width: 25%;">
                        <asp:TextBox ID="txtPPinCode" runat="server"></asp:TextBox>
                    </td>
                </tr>
                </asp:Panel>--%>
                </table>
            </asp:Panel>
            <asp:Panel ID="pnlPAddress" runat="server">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <asp:Panel ID="pnlPermanenetAddr" runat="server">
                            <table width="100%">
                                <tr>
                                    <td style="width: 25%">
                                        <asp:Label ID="lblPermanentAddr" runat="server" Text="Permanent Address"></asp:Label>
                                        <label class="mandatorymark">
                                            *</label>
                                    </td>
                                    <td style="width: 25%">
                                        <asp:TextBox ID="txtPAddress" runat="server" MaxLength="25"></asp:TextBox>
                                        <span id="spanPAddress" runat="server">
                                            <img id="imgPAddress" runat="server" src="Images/cross.png" alt="" style="display: none;
                                                border: none;" />
                                        </span>
                                    </td>
                                    <td style="width: 25%">
                                        <asp:Label ID="lblPStreetName" runat="server" Text="Street Name"></asp:Label>
                                        <label class="mandatorymark">
                                            *</label>
                                    </td>
                                    <td style="width: 25%;">
                                        <asp:TextBox ID="txtPStreetName" runat="server" MaxLength="25"></asp:TextBox>
                                        <span id="spanPStreetName" runat="server">
                                            <img id="imgPStreetName" runat="server" src="Images/cross.png" alt="" style="display: none;
                                                border: none;" />
                                        </span>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 25%">
                                        <asp:Label ID="lblPCity" runat="server" Text="City"></asp:Label>
                                        <label class="mandatorymark">
                                            *</label>
                                    </td>
                                    <td style="width: 25%">
                                        <asp:TextBox ID="txtPCity" runat="server" MaxLength="25"></asp:TextBox>
                                        <span id="spanPCity" runat="server">
                                            <img id="imgPCity" runat="server" src="Images/cross.png" alt="" style="display: none;
                                                border: none;" />
                                        </span>
                                    </td>
                                    <td style="width: 25%">
                                        <asp:Label ID="lblPPinCode" runat="server" Text="Pin Code"></asp:Label>
                                        <label class="mandatorymark">
                                            *</label>
                                    </td>
                                    <td style="width: 25%;">
                                        <asp:TextBox ID="txtPPinCode" runat="server" MaxLength="7"></asp:TextBox>
                                        <span id="spanPPinCode" runat="server">
                                            <img id="imgPPinCode" runat="server" src="Images/cross.png" alt="" style="display: none;
                                                border: none;" />
                                        </span>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="rblSamePerAddress" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </asp:Panel>
            <asp:HiddenField ID="hidReportingTo" runat="server" />
        </div>
        <%--<div id="divEmergencyDetails" class="detailsborder" runat="server">
            <table width="100%" class="detailsbg">
                <tr>
                    <td>
                        <asp:Label ID="Label1" runat="server" Text="Emergency Details" CssClass="detailsheader"></asp:Label>
                    </td>
                </tr>
            </table>
            <div>
                <asp:GridView ID="gvEmergencyDetails" runat="server" AutoGenerateColumns="False"
                    Width="100%">
                    <HeaderStyle CssClass="addrowheader" />
                    <AlternatingRowStyle CssClass="alternatingrowStyle" />
                    <RowStyle Height="20px" CssClass="textstyle" />
                    <Columns>
                        <asp:TemplateField HeaderText="Person Name">
                            <ItemTemplate>
                                <asp:TextBox ID="txtName" runat="server" MaxLength="50" Text='<%# DataBinder.Eval(Container, "DataItem.ContactName") %>'></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Relation">
                            <ItemTemplate>
                                <asp:TextBox ID="txtRelation" runat="server" MaxLength="50" Text='<%# DataBinder.Eval(Container, "DataItem.Relation") %>'></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Contact No.">
                            <ItemTemplate>
                                <asp:TextBox ID="txtContactNo" runat="server" MaxLength="50" Text='<%# DataBinder.Eval(Container, "DataItem.ContactNumber") %>'></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                       
                    </Columns>
                </asp:GridView>
            </div>
        </div>--%>
        <div>
            <table>
                <tr>
                    <td>
                    </td>
                </tr>
            </table>
        </div>
        <div id="divEmpTabDetails" class="detailsborder">
            <%--<table width="100%">
        <tr>
        <td align="center" style="width:100%">--%>
            <asp:Panel ID="framePanel" runat="server" Width="100%" Height="100%">
                <iframe id="ifrmEmpDetails" frameborder="0" src="EmpPassportDetails.aspx" width="100%"
                    height="100%"></iframe>
            </asp:Panel>
            <%--</td>
        </tr>
        </table>--%>
        </div>
        <div>
            <table>
                <tr>
                    <td>
                    </td>
                </tr>
            </table>
        </div>
        <div class="detailsborder">
            <table width="100%">
                <tr>
                    <td style="width: 10%" align="left">
                        <asp:Button ID="btnPrevious" runat="server" Text="Previous" CssClass="button" OnClick="btnPrevious_Click" />
                    </td>
                    <td style="width: 10%" align="left">
                        <asp:Button ID="btnNext" runat="server" Text="Next" CssClass="button" OnClick="btnNext_Click" />
                    </td>
                    <td style="width: 80%" align="right">
                        <asp:Button ID="btnResume" runat="server" Text="Resume" CssClass="button" OnClick="btnResume_Click" />
                        <asp:Button ID="btnRelease" runat="server" Text="Release" CssClass="button" Visible="false" />
                        <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="button" OnClick="btnEdit_Click"
                            Visible="false" />
                        <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="button" OnClick="btnUpdate_Click"
                            Visible="false" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="button" OnClick="btnCancel_Click"
                            Visible="false" />
                        <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="button" OnClick="btnDelete_Click"
                            Visible="false" />
                    </td>
                </tr>
            </table>
        </div>
        <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
    </div>
</asp:Content>
