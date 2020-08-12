<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="4C_Add4CFeedback.aspx.cs" Inherits="FourCModule_4C_Add4CFeedback" Title="4C Add Feedback" %>
<%@ PreviousPageType VirtualPath="~/4CLogin.aspx"  %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content" ContentPlaceHolderID="cphMainContent" Runat="Server">

 
<script src="JavaScript/jquery.js" type="text/javascript"></script>

<script language="javascript" type="text/javascript">

    var retVal;
    (function($) {
        $(document).ready(function() {
            window._jqueryPostMessagePolyfillPath = "/postmessage.htm";
            $(window).on("message", function(e) {
                if (e.data != undefined) {
                    retVal = e.data;
                }
            });

            $('#ctl00_cphMainContent_btnSubmitRating').click(function() {
                $(this).val("Please Wait..");
                $(this).attr('disabled', 'disabled');

                //$(this).parents('#form').submit();
            });

            $('#ctl00_cphMainContent_btnSendForReview').click(function() {
                $(this).val("Please Wait..");
                $(this).attr('disabled', 'disabled');
            });
        });
    })(jQuery);

    function $(v) { return document.getElementById(v); }

    setbgToTab('ctl00_tab4C', 'ctl00_spanAddFeedback');
    //setbgToTab('ctl00_tabHome', '');

    function Open4CDetailPopUp(strEmpId, departmentId, projectId, month, year, fbId, FourCRole, loginEmailId, AllowDirectSubmit, AllowToEdit, empName) {
//        var sharedObject = {};
//        sharedObject.EmpId = strEmpId;
//        sharedObject.projectId = projectId;
//        sharedObject.month = month;
//        sharedObject.year = year;
        //        sharedObject.FourCRole = FourCRole;

//        var retunrVal = window.showModalDialog("4CEmpDetailsPopup.aspx?EmpId=" + strEmpId + "&departmentId=" + departmentId + "&projectId=" + projectId + "&month=" + month + "&year=" + year + "&FBID=" + fbId + "&FourCRole= " + FourCRole + "&LoginEmailId= " + loginEmailId + "&AllowDirectSubmit=" + AllowDirectSubmit + "&AllowToEdit=" + AllowToEdit + "&EmpName=" + empName, null, 'dialogHeight:500px; dialogWidth:1270px; center:yes;');
//        
//        var items = new Array();
//        if (retunrVal != undefined)
//            items = retunrVal.split("_");

//        if (items.length != 0) { 
//            document.getElementById('<%=hdPopUpReturnProjectId.ClientID%>').value = items[0];
//            document.getElementById('<%=hdPopupReturnMonth.ClientID%>').value = items[1];
//            document.getElementById('<%=hdPopupReturnYear.ClientID%>').value = items[2];
//        }

        window.location = '<%= ResolveUrl("~/4CEmpAction.aspx") %>';
        
        
        //javascript: __doPostBack('', '');
        //var obj_Panel = document.getElementById(objPanelToUpdate);
        //__doPostBack(obj_Panel, '');

        //window.showModalDialog("4CEmpDetailsPopup.aspx", sharedObject, 'dialogHeight:450px; dialogWidth:1270px; center:yes;');
    }

    function checkAll(objRef) {
        var GridView = objRef.parentNode.parentNode.parentNode;
        var inputList = GridView.getElementsByTagName("input");
        for (var i = 0; i < inputList.length; i++) {
            //Get the Cell To find out ColumnIndex
            var row = inputList[i].parentNode.parentNode;
            if (inputList[i].type == "checkbox" && objRef != inputList[i] && inputList[i].disabled == false) {
                //&& inputList[i].isDisable == false
                if (objRef.checked) {

                    inputList[i].checked = true;
                }
                else {
                   
                        inputList[i].checked = false;
                    
                }
            }
        }

    }


    function Check_Click(objRef) {
        //Get the Row based on checkbox
        var row = objRef.parentNode.parentNode;

        //Get the reference of GridView
        var GridView = row.parentNode;

        //Get all input elements in Gridview
        var inputList = GridView.getElementsByTagName("input");

        for (var i = 0; i < inputList.length; i++) {
            //The First element is the Header Checkbox
            var headerCheckBox = inputList[0];

            //Based on all or none checkboxes
            //are checked check/uncheck Header Checkbox
            var checked = true;
            if (inputList[i].type == "checkbox" && inputList[i] != headerCheckBox) {
                if (!inputList[i].checked) {
                    checked = false;
                    break;
                }
            }
        }
        headerCheckBox.checked = checked;


    }

    function CheckIfRowChecked() {
        var valid = false;
        var gv = document.getElementById("<%=grdEmpDetails.ClientID%>");
        if (gv != null) {
            for (var i = 0; i < gv.all.length; i++) {
                var node = gv.all[i];
                if (node != null && node.type == "checkbox" && node.checked && node.disabled == false) {
                    valid = true;
                    break;
                }
            }
            if (!valid) {
                alert("Please select at least one record.");
                
                return valid;
            }
        } else {
            alert("Please select at least one record.");
            return valid;
        }
    }
    
    
    
    </script>

<style type="text/css">
.modalBackground {
            background-color:Gray;
            filter:alpha(opacity=70);
            opacity:0.7;
}
 </style>

<%-- <asp:ToolkitScriptManager ID="ScriptManager1" runat="server">
    </asp:ToolkitScriptManager>--%>

 <div style="font-family: Verdana; font-size: 9pt;">
 
    <table width="100%">
            <tr>
                <td align="center" style="height: 25px; filter: progid:DXImageTransform.Microsoft.Gradient(endColorstr='#7590C8', startColorstr='#0C359A', gradientType='1'); background-color: #7590C8">
                    <span class="header">4C Feedback</span>
                </td>
            </tr>
            <tr>
                <td style="height: 1pt">
                </td>
            </tr>
        </table>
    
   
    

    
   
    
    <table width="100%" border=0>
          <tr >
          <td align="right" style="width:20%;">
              <asp:LinkButton ID="lnkExportToExcel" runat="server" OnClick="lnkExportToExcel_Click" Visible="false" ForeColor="Black" Font-Bold="true" Font-Underline="true">Export To Excel All Sent For Review Ratings</asp:LinkButton>
          </td>
            <td  align="center" style="width:50%; padding-left:90px;" >
             <div runat="server" id="tblAVP" visible="false">
                 <div style="width:65%;" >
                    <table  border=0>
            <tr class="detailsbg" >
            <td class="detailsborder" style="height:30px;" align="right">
                <asp:RadioButtonList ID="rbAVPView" runat="server" RepeatDirection="Horizontal" Font-Bold="true" AutoPostBack="true" OnSelectedIndexChanged="rbAVPView_SelectedIndexChanged" >
                    <asp:ListItem Text="Add 4C Feedback" Value="ADD" Selected="True"></asp:ListItem>  
                    <asp:ListItem Text="Review 4C Feedback" Value="Review"></asp:ListItem>  
                 </asp:RadioButtonList>
                 
            </td>
            </tr>
            </table> 
                </div>
                 </div>
            </td>
            <td align="right" style="width:30%"  >
            
            <div id="divMonthYearDisplay" runat="server" >
            <table border=0 class="detailsborder">
                    <tr >
                     <td style="width:40%" align="center" >
                         <asp:Label ID="lblFeedbackFor" runat="server" Text="FeedBack For Month :" Font-Bold="true"></asp:Label>
                        </td>
                        <td style="width:10%" align="center">
                                <asp:ImageButton ID="imgPrevious" runat="server" onclick="imgPrevious_Click" ImageUrl="~/Images/LeftArrow.jpg" Width="25px" Height="25px" />        
                        </td>
                        
                        <td style="width:35%" align="center">
                        <asp:Label ID="lblDate" runat="server" Font-Bold="true"></asp:Label>
                        </td>
                        <td style="width:10%">
                            <asp:ImageButton ID="imgNext" runat="server" onclick="imgNext_Click" ImageUrl="~/Images/RightArrow.jpg"  Width="25px" Height="25px"/>
                         </td>
                    </tr>
                </table>   
                </div>
            </td>
          </tr>
    </table>    
    
   
    
     <br />
     <table width="100%">
      <tr class="detailsbg">     
      
            <td style="width:18%">
               <asp:Label ID="lblDepartment" runat="server" Text="Department - " Font-Bold="true"></asp:Label> &nbsp;&nbsp;
            
                <asp:DropDownList ID="ddlDepartment" runat="server" Width="150px" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged" AutoPostBack="true"> 
                </asp:DropDownList>
            </td>
      
            <td style="width:25%">
                 <asp:Label ID="lblProj" runat="server" Text="Project Name -" Font-Bold="true" ></asp:Label> &nbsp;&nbsp;
            
                 <asp:DropDownList ID="ddlProjectList" runat="server" Width="250px" AutoPostBack="true" Enabled="false" OnSelectedIndexChanged="ddlProjectList_SelectedIndexChanged">
                 </asp:DropDownList>
                 <asp:HiddenField ID="hdPopUpReturnDepartmentId" runat="server" />
                <asp:HiddenField ID="hdPopUpReturnProjectId" runat="server" />
                <asp:HiddenField ID="hdPopupReturnMonth" runat="server" />
                <asp:HiddenField ID="hdPopupReturnYear" runat="server" />
                 
            </td>
             <td style="width:25%" runat="server" id="tdEmployee" align="center">
                    <asp:Label ID="lblEmployee" runat="server" Text="Employee Name -" Font-Bold="true" > </asp:Label> &nbsp;&nbsp;
                    <asp:DropDownList ID="ddlEmployee" runat="server" Width="250px" AutoPostBack="true" OnSelectedIndexChanged="ddlEmployee_SelectedIndexChanged">
                    </asp:DropDownList>
            </td>  
            
            <td style="width:15%">
            <asp:Label ID="lblManager" runat="server" Text="Creator -" Font-Bold="true" ></asp:Label> &nbsp;&nbsp;               
                 <asp:Label ID="lblManagerName" runat="server" ></asp:Label>
            </td>
            <td style="width:18%">
            <asp:Label ID="lblReviewer" runat="server" Text="Reviewer -" Font-Bold="true" ></asp:Label> &nbsp;&nbsp;               
                 <asp:Label ID="lblReviewerName" runat="server" ></asp:Label>
            </td>
            
           
        </tr>
      </table>
      
    <br />
    <br />
    
<%--    <asp:UpdatePanel ID="upDetails" runat="server" UpdateMode="Always">
    <ContentTemplate>--%>
    
    <div runat="server" id="divReviewerNoData" visible="false">
    
    <table width="100%" >
          <tr >
            <td align="center" >
                 <div style="text-align:justify; width:20%;" class="detailsborder">
                    <table width="100%">
            <tr class="detailsbg">
            <td>
                 <asp:Label ID="lblReviewerNoData" runat="server" Text="Rating not entered by the creator." Font-Bold="true" ></asp:Label> 
            </td>
            </tr>
            </table> 
                </div>
            </td>
          </tr>
    </table>    
    
    </div>
     <table width="100%">
            <tr>
                <td>
                    <asp:Label ID="lblMessage" runat="server" CssClass="Messagetext" Visible ="false"></asp:Label>
                </td>
            </tr>
        </table>
    
    <div id="divGridViewEmpDetails" style="height:300px; width:100%; position:relative; overflow:auto;" runat="server">


    <asp:GridView ID="grdEmpDetails" runat="server"  Width="98%" AllowPaging="True"  PageSize="100" EmptyDataText="No Record Found." AutoGenerateColumns="false" DataKeyNames="EMPId" OnRowDataBound="grdEmpDetails_DataBound" OnSelectedIndexChanged="MyEvent" OnPageIndexChanging="grdEmpDetails_PageIndexChanging">
    <HeaderStyle CssClass="headerStyle" />
    <AlternatingRowStyle CssClass="alternatingrowStyleRP" />
                         <Columns>
                                  <asp:TemplateField HeaderText="Select All" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderStyle-Width="3%" HeaderStyle-Wrap="true">
                                    <HeaderStyle HorizontalAlign="center" VerticalAlign="Top"/>
                                      <HeaderTemplate>
                                        
                                      <asp:CheckBox ID="checkAll" runat="server" onclick="checkAll(this);" Text="Select All" TextAlign="Left"/>

                                      </HeaderTemplate> 

                                      <ItemTemplate>
                                       <%-- <span id="spanChkSelect" runat="server">--%>
                                            <asp:CheckBox ID="chkSelect" runat="server" onclick="Check_Click(this)" OnCheckedChanged="chkSelect_CheckedChanged" AutoPostBack="true"  />  <%--AutoPostBack="true" OnCheckedChanged="chkSelect_CheckedChanged"--%>
                                            <asp:HiddenField ID="hdEmp4CStatus" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"Employee4CStatus") %>'/>
                                            
                                        <%--</span>--%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                  <asp:TemplateField HeaderText="Serial No"  ItemStyle-Width="5%" HeaderStyle-Width="5%">
                                    <ItemTemplate>    
                                         <%# ((GridViewRow)Container).RowIndex + 1%>
                                    </ItemTemplate>
                                    <ItemStyle  HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                     <asp:TemplateField HeaderText="Employee Name" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                     <ItemTemplate>
                                         <asp:HyperLink ID="hypEmpName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"EmployeeName") %>'  CommandName="select" ForeColor="Black" ></asp:HyperLink>
                                         <%--<asp:LinkButton ID="lnkName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"EmployeeName") %>'  CommandName="select" ></asp:LinkButton>--%>
                                         <asp:HiddenField ID="hdEmpId" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"EMPId") %>'/>
                                         <asp:HiddenField ID="hdProjectId" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"ProjectId") %>'/>
                                         <asp:HiddenField ID="hdDepartmentId" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"DepartmentId") %>'/>
                                         <asp:HiddenField ID="hdFBID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"FBID") %>'/>
                                         <asp:HiddenField ID="hdProjectStatus" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"Project4CStatus") %>'/>
                                         <asp:HiddenField ID="hdFlag" runat="server" />
                                     </ItemTemplate>
                                     <HeaderStyle HorizontalAlign="left" />
                                     </asp:TemplateField>
 
                                    <asp:BoundField DataField="Designation" HeaderText="Designation"  ItemStyle-Width="15%" HeaderStyle-Width="15%">
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    
                                     <asp:BoundField  HeaderText="Company Joining Date" DataField="compJoiningDate" DataFormatString="{0:dd-MMM-yyyy}"  ItemStyle-Width="10%" HeaderStyle-Width="10%">
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    
                                    <asp:BoundField  HeaderText="Employement Status" DataField="confirmStatus" ItemStyle-Width="10%" HeaderStyle-Width="10%">
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    
                                     <asp:BoundField  HeaderText="Project Name" DataField="Projectname"  ItemStyle-Width="10%" HeaderStyle-Width="10%">
                                        <HeaderStyle  HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    
                                     <asp:BoundField  HeaderText="Project Joining Date" DataField="projJoiningDate" DataFormatString="{0:dd-MMM-yyyy}"  ItemStyle-Width="10%" HeaderStyle-Width="10%">
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    
                                      <asp:BoundField  HeaderText="Functional Manager" DataField="FunManagerName"  ItemStyle-Width="10%" HeaderStyle-Width="10%">
                                        <HeaderStyle  HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    
                                     <asp:TemplateField HeaderText="Competency" ItemStyle-HorizontalAlign="Center">
                                     <ItemTemplate>
                                            <asp:Label ID="lblGrdCompetency" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Competency") %>'></asp:Label>
                                            <asp:HiddenField ID="hdCompetency" Value='<%# DataBinder.Eval(Container.DataItem,"Competency") %>' runat="server" />                                     
                                     </ItemTemplate>
                                     </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Communication" ItemStyle-HorizontalAlign="Center">
                                     <ItemTemplate>
                                            <asp:Label ID="lblGrdCommunication" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Communication") %>'></asp:Label>
                                           <asp:HiddenField ID="hdCommunication" Value='<%# DataBinder.Eval(Container.DataItem,"Communication") %>' runat="server" />                                          
                                     </ItemTemplate>
                                     </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Commitment" ItemStyle-HorizontalAlign="Center">
                                     <ItemTemplate>
                                            <asp:Label ID="lblGrdCommitment" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Commitment") %>'></asp:Label>
                                            <asp:HiddenField ID="hdCommitment" Value='<%# DataBinder.Eval(Container.DataItem,"Commitment") %>' runat="server" />
                                     </ItemTemplate>
                                     </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Collaboration" ItemStyle-HorizontalAlign="Center">
                                     <ItemTemplate>
                                            <asp:Label ID="lblGrdCollaboration" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Collaboration") %>'></asp:Label>
                                            <asp:HiddenField ID="hdCollaboration" Value='<%# DataBinder.Eval(Container.DataItem,"Collaboration") %>' runat="server" />                                      
                                     </ItemTemplate>
                                     </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Reject Rating" ItemStyle-HorizontalAlign="Center">
                                     <ItemTemplate>
                                            <asp:ImageButton ID="imgReject" Visible="true" runat="server" CausesValidation="false" CommandName="Desc" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"FBID") %>' OnCommand="lnkRejectRating_Click" ImageUrl="~/Images/rejectEnable.bmp" Width="30px" Height="30px" />        
                                     </ItemTemplate>
                                     </asp:TemplateField>
                                     
                           </Columns>
                           
                           <PagerStyle CssClass="tablePager" HorizontalAlign="Center" ForeColor="White"  />
                         
                    </asp:GridView>
   
   </div>   
    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
     <br />
    <table width="100%" border=0>
            <tr>
                <td style="width:70%" align="right">
                    
                </td>
                <td style="width:10%" align="right">
                    <asp:Button ID="btnNotApplicable" runat="server" Text="Not Applicable" CssClass="button" Visible="true" Width="150px" OnClick="btnNotApplicable_Click" OnClientClick="return CheckIfRowChecked()"/>
                </td>
                <td style="width:10%;" align="right">
                        <asp:Button ID="btnSendForReview" runat="server" Text="Send for Review" CssClass="button" Visible="false" Width="150px" OnClick="btnSendForReview_Click" OnClientClick="return CheckIfRowChecked()" />
                        <asp:Button ID="btnSubmitRating" runat="server" Text="Submit Rating" CssClass="button" Visible="false" Width="150px" OnClick="btnSubmitRating_Click" OnClientClick="return CheckIfRowChecked()" />
                </td>
                
                <td style=" width:8%;" align="center">
                          <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="button" OnClick="btnCancel_Click" />
                                 
                </td>
            </tr>    
    </table>
    
    
    
     <asp:Button id="btnShowPopup" runat="server" style="display:none" />

            <asp:ModalPopupExtender 
                ID="mdlPopup" runat="server" TargetControlID="btnShowPopup" PopupControlID="pnlPopup" 
                CancelControlID="btnClose" BackgroundCssClass="modalBackground"  />
            <asp:Panel ID="pnlPopup" runat="server" Width="400px" style="display:none">
                <asp:UpdatePanel ID="updPnlCustomerDetail" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                         <table width="75%" border=3>                              
                                 <tr>
                                     <td align="center" style="height: 25px; filter: progid:DXImageTransform.Microsoft.Gradient(endColorstr='#7590C8', startColorstr='#0C359A', gradientType='1'); background-color: #7590C8">
                                       <span class="header"> 
                                           <asp:Label ID="lblModelPopupHeader" runat="server"></asp:Label>  </span>
                                           <asp:HiddenField ID="hdEmpName" runat="server" />
                                    </td>
                                 </tr>
                                  <tr>
                                        <td>
                                            <asp:Label ID="lblValidate" runat="server" Text="Please enter remarks." ForeColor="Red" Visible="false"></asp:Label>
                                       
                                        </td>
                                </tr>
                                 <tr>
                                        <td>
                                        <asp:TextBox ID="txtDesc" runat="server"  TextMode ="MultiLine" Height ="250px" Width="400px"></asp:TextBox>
                                       
                                        </td>
                                </tr>
                         </table>
                    </ContentTemplate>    
                 
                </asp:UpdatePanel>
                <table width="75%" border=0>                              
                    <tr>
                        <td style="width:70%;" align="right">
                            <asp:Button ID="btnDescSave" runat="server" Text="Save" CssClass="button" OnClick="btnDescSave_Click" Width="50px" />
                        </td>
                        <td style="width:30%" align="left">
                            <asp:Button ID="btnClose" runat="server" CssClass="button" Text="Close" Width="50px" />
                        </td>
                    </tr>
                </table>
                
            </asp:Panel>
    
 
 </div>
 
 

</asp:Content>

