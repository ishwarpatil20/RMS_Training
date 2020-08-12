<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DatePickerControl.ascx.cs"
    Inherits="UIControls_DatePickerControl" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<script src="../JavaScript/DatePicker.js" type="text/javascript">
  
</script>

<%--<table>
    <tr>
        <td>--%>
            <asp:TextBox ID="txtDate" runat="server" CssClass="mandatoryField" MaxLength="10" 
                ToolTip="Enter Date(dd/MM/yyyy)" BorderStyle="Solid"  Width="185px" onFocus="javascript:vDateType='3'"
                onKeyUp="DateFormat(this,this.value,event,false,'3')" onBlur="DateFormat(this,this.value,event,true,'3')">
            </asp:TextBox>
            &nbsp;
            <asp:ImageButton ID="imgDate" runat="server" ImageUrl="../Images/Calendar_scheduleHS.png"
                CausesValidation="false" ImageAlign="AbsMiddle" />
            <cc1:CalendarExtender ID="calendarDate" runat="server" PopupButtonID="imgDate"
                TargetControlID="txtDate" Format="dd/MM/yyyy">
            </cc1:CalendarExtender>
        <%--</td>
    </tr>
</table>--%>
