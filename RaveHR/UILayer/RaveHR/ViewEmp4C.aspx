<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="ViewEmp4C.aspx.cs" Inherits="ViewEmp4C" Title="View My 4C" %>
<%@ PreviousPageType VirtualPath="~/4CLogin.aspx"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMainContent" Runat="Server">
<%--Ishwar Patil 28012015 Start--%>
<script src="JavaScript/jquery.js" type="text/javascript"></script>

    <script src="JavaScript/skinny.js" type="text/javascript"></script>

    <script src="JavaScript/jquery.modalDialog.js" type="text/javascript"></script>
<%--Ishwar Patil 28012015 End--%>
<script language="javascript" type="text/javascript">

    setbgToTab('ctl00_tabEmployee', 'ctl00_spanViewMy4C');

    //Ishwar Patil 28012015 Start

//    function Open4CActionPopUp(parameters) {
//        //alert(parameters);
//        var retunrVal = window.showModalDialog(parameters, null, 'dialogHeight:500px; dialogWidth:1270px; center:yes;');
//    }

    var retVal;
    (function($) {
        $(document).ready(function() {
            window._jqueryPostMessagePolyfillPath = "/postmessage.htm";
            $(window).on("message", function(e) {
                if (e.data != undefined) {
                    retVal = e.data;
                }
            });
        });
    })(jQuery);

    function Open4CActionPopUp(parameters) {
        //alert(parameters);
        jQuery.modalDialog.create({ url: '4CEmpDetailsPopup.aspx?' + parameters, maxWidth: 1400 }).open();
    }
//Ishwar Patil 28012015 End

    </script>
    
    <div style="font-family: Verdana; font-size: 9pt;">
 
    <table width="100%">
            <tr>
                <td align="center" style="height: 25px; filter: progid:DXImageTransform.Microsoft.Gradient(endColorstr='#7590C8', startColorstr='#0C359A', gradientType='1'); background-color: #7590C8">
                    <span class="header">View My 4C</span>
                </td>
            </tr>
            <tr>
                <td style="height: 1pt">
                </td>
            </tr>
        </table>
        
        </div>
            <table width="100%" border="0" style="border-color:Black;border-width:thin; " >
            <tr>
               <%-- <td align="right" style="width:50%; vertical-align:middle">
                    
                </td>--%>
                <td style="width:100%"; align="center">
                        <table border="1" width="40%" style="background-color:#D4DEFA;" >
                            <tr >
                                <td style="width:20%" align="center">
                                        <asp:ImageButton ID="imgPrevious" runat="server" onclick="imgPrevious_Click" ImageUrl="~/Images/LeftArrow.jpg" Width="30px" Height="30px" />        
                                </td>
                                
                                <td style="width:60%" align="center"><asp:Label ID="lbl4CMonth" runat="server" Text="4C Month :" Font-Bold="true"></asp:Label>
                                <asp:Label ID="lblDate" runat="server" Font-Bold="true"></asp:Label>
                                </td>
                                <td style="width:20%">
                                    <asp:ImageButton ID="imgNext" runat="server" onclick="imgNext_Click" ImageUrl="~/Images/RightArrow.jpg"  Width="30px" Height="30px"/>
                                 </td>
                            </tr>
                        </table>
                </td>
              </tr>
            </table>
        
             
                <br />
                <br />
                <div id="divGridViewEmpDetails" style="height:300px; width:100%; position:relative; overflow:auto;" runat="server">
    
        <asp:GridView ID="grdEmpDetails" runat="server"  Width="98%" AllowPaging="false"  EmptyDataText="No Record Found." AutoGenerateColumns="false" DataKeyNames="EMPId" OnRowDataBound="grdEmpDetails_DataBound">
    <HeaderStyle CssClass="headerStyle" />
    <AlternatingRowStyle CssClass="alternatingrowStyleRP" />
                         <Columns>
                                  
                                  <asp:TemplateField HeaderText="Serial No" ItemStyle-Width="5%" HeaderStyle-Width="5%">
                                    <ItemTemplate>    
                                         <%# ((GridViewRow)Container).RowIndex + 1%>
                                    </ItemTemplate>
                                    <ItemStyle  HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                     <asp:TemplateField HeaderText="Employee Name" HeaderStyle-Width="10%">
                                     <ItemTemplate>
                                    <%--<asp:LinkButton ID="lnkName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"EmployeeName") %>'  CommandName="Select" Font-Underline="True"></asp:LinkButton> --%>
                                    <asp:HyperLink ID="lnkName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"EmployeeName") %>' 
                                        CommandName="Select" Font-Underline="True"></asp:HyperLink>
                                         <asp:HiddenField ID="hdEmpId" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"EMPId") %>'/>
                                         <asp:HiddenField ID="hdProjectId" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"ProjectId") %>'/>
                                         <asp:HiddenField ID="hdFBID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"FBID") %>'/>
                                         <asp:HiddenField ID="hdDepartmentId" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"DepartmentId") %>'/>
                                        
                                     </ItemTemplate>
                                     <HeaderStyle HorizontalAlign="left" />
                                     </asp:TemplateField>
 
                                    <asp:BoundField DataField="Designation" HeaderText="Designation" ItemStyle-Width="10%" HeaderStyle-Width="10%">
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    
                                     <asp:BoundField  HeaderText="Company Joining Date" DataField="compJoiningDate" DataFormatString="{0:dd-MMM-yyyy}" ItemStyle-Width="5%" HeaderStyle-Width="10%">
                                        <HeaderStyle  HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    
                                    <asp:BoundField  HeaderText="Employement Status" DataField="confirmStatus" ItemStyle-Width="10%" HeaderStyle-Width="5%">
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    
                                     <asp:BoundField  HeaderText="Project Name" DataField="Projectname" ItemStyle-Width="10%" HeaderStyle-Width="15%">
                                        <HeaderStyle  HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    
                                     <asp:BoundField  HeaderText="Project Joining Date" DataField="projJoiningDate" DataFormatString="{0:dd-MMM-yyyy}" ItemStyle-Width="5%" HeaderStyle-Width="5%">
                                        <HeaderStyle  HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    
                                       <asp:BoundField  HeaderText="Creator" DataField="Creator"  ItemStyle-Width="10%" HeaderStyle-Width="10%">
                                        <HeaderStyle  HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    
                                    <asp:BoundField  HeaderText="Reviewer" DataField="Reviewer"  ItemStyle-Width="10%" HeaderStyle-Width="10%">
                                        <HeaderStyle  HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    
                                      <asp:BoundField  HeaderText="Functional Manager" DataField="FunManagerName" ItemStyle-Width="10%" HeaderStyle-Width="10%">
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
                           </Columns>
                           <PagerStyle CssClass="tablePager" HorizontalAlign="Center" ForeColor="White"  />
                         
                    </asp:GridView>
         
    </div>

 <br />
    <table width="100%" border=0>
            <tr>
                <td style="width:70%" align="right">
                    
                </td>
                <td style="width:20%;" align="right">
                       <%-- <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="button" Width="150px" OnClick="btnReset_Click" />--%>
                </td>
                
                <td style=" width:10%;" align="center">
                          <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="button" OnClick="btnCancel_Click" />
                                 
                </td>
            </tr>    
    </table>
    
    
</asp:Content>

