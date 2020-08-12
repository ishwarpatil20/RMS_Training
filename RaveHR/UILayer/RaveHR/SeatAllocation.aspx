<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="SeatAllocation.aspx.cs" Inherits="SeatAllocation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMainContent" Runat="Server">

    <script type="text/javascript">
 //Set the back groung color of tab.
 setbgToTab('ctl00_tabSeatAllocation', 'ctl00_spanSeatAllocation');  
     
function CustomContextMenu(Arguments)
{
    //Global variables.
	var Base = Arguments.Base ? Arguments.Base : document.documentElement;
	var Width = Arguments.Width ? Arguments.Width : 200;
	var FontColor = Arguments.FontColor ? Arguments.FontColor : 'black';
	var HoverFontColor = Arguments.HoverFontColor ? Arguments.HoverFontColor : 'white';
	var HoverBackgroundColor = Arguments.HoverBackgroundColor ? Arguments.HoverBackgroundColor : '#2257D5';
	var HoverBorderColor = Arguments.HoverBorderColor ? Arguments.HoverBorderColor : 'orange';
	var ClickEventListener = Arguments.ClickEventListener ? Arguments.ClickEventListener : function(){ return false; };
	
    var ContextMenuDiv = document.createElement('div');
    var ContextMenuTable = document.createElement('table');
    var Index = 0;
    var EventHandlers = new Array();
	
	//Style Context Menu div.
    ContextMenuDiv.id = 'ContextMenu'; 
    ContextMenuDiv.style.position = 'absolute';
    ContextMenuDiv.style.backgroundColor = 'white';
    ContextMenuDiv.style.border = '2px outset white';
    ContextMenuDiv.style.verticalAlign = 'top';
    ContextMenuDiv.style.textAlign = 'left';
	ContextMenuDiv.style.visibility = 'hidden';
	ContextMenuDiv.style.width = (Width + 6) + 'px';
	
	//Styles Context Menu table.
	ContextMenuTable.id = 'ContextMenuTable'; 
	ContextMenuTable.style.width = (Width + 5) + 'px';
	ContextMenuTable.border = 0;
	ContextMenuTable.cellPadding = 0;
	ContextMenuTable.cellSpacing = 0;   
	
	//Append Context Menu table into Context Menu div.
	ContextMenuDiv.appendChild(ContextMenuTable);
	
	//Public method for adding Context Menu Items.
	this.AddItem = function(imgSrc, itemText, isDisabled, commandName)
	{	   
		var Tr = ContextMenuTable.insertRow(Index++);
	    Tr.style.fontFamily = 'Verdana';
	    Tr.style.fontSize = '10pt';
	    Tr.style.fontWeight = 'normal';
	    Tr.style.backgroundColor = 'white';
	    Tr.style.color = isDisabled ? 'gray' : FontColor;
	    Tr.style.cursor = 'default';
		
	    var TdLeft = Tr.insertCell(0);
	    TdLeft.style.width = 25 + 'px';
	    TdLeft.style.height = 25 + 'px';
	    TdLeft.style.textAlign = 'center';
	    TdLeft.style.verticalAlign = 'middle';
	    TdLeft.style.borderTop = '2px solid #E8E3DB';
	    TdLeft.style.borderBottom = '2px solid #E8E3DB';
	    TdLeft.style.borderLeft = '2px solid #E8E3DB';
	    TdLeft.style.backgroundColor = '#E8E3DB';
		
	    var TdCenter = Tr.insertCell(1);
	    TdCenter.style.width = 10 + 'px';
	    TdCenter.style.height = 25 + 'px';
	    TdCenter.innerHTML = '&nbsp;';
	    TdCenter.style.borderTop = '2px solid white';
	    TdCenter.style.borderBottom = '2px solid white';
		
	    var TdRight = Tr.insertCell(2);
	    TdRight.style.width = (Width - 25) + 'px';
	    TdRight.style.height = 25 + 'px';
	    TdRight.style.textAlign = 'left';
	    TdRight.style.verticalAlign = 'middle'; 
	    TdRight.style.fontStyle = isDisabled ? 'italic' : 'normal'; 
	    TdRight.innerHTML = itemText ? itemText : '&nbsp;';
	    TdRight.style.borderTop = '2px solid white';
	    TdRight.style.borderBottom = '2px solid white';
	    TdRight.style.borderRight = '2px solid white';
		
		if(imgSrc)
		{
	        var Img = new Image();	 
	        Img.id = 'Img';    
	        Img.src = imgSrc;
	        Img.style.width = 20 + 'px';	 
	        Img.style.height = 20 + 'px';	  
	        Img.disabled = isDisabled; 
			
	        TdLeft.appendChild(Img);	
	    }
	    else
	        TdLeft.innerHTML = '&nbsp;';
		
	    //Register events.	    
	    if(!isDisabled)
		{	        
			WireUpEventHandler(Tr, 'click', function(){ ClickEventListener(Tr, {CommandName: commandName, Text: itemText, IsDisabled: isDisabled, ImageUrl: Img ? Img.src : ''}) });
			WireUpEventHandler(Tr, 'mouseover', function(){ MouseOver(Tr, TdLeft, TdCenter, TdRight); });
	        WireUpEventHandler(Tr, 'mouseout', function(){ MouseOut(Tr, TdLeft, TdCenter, TdRight); });
	    }
		else
	    {
			WireUpEventHandler(Tr, 'click', function(){ return false; });
	        WireUpEventHandler(TdRight, 'selectstart', function(){ return false; });
	    }
	}	
	
//	this.RemoveRow = function(imgSrc, itemText, isDisabled, commandName)
//	{
//	 var LastRow = ContextMenuTable.lastChild;
//	 ContextMenuTable.removeChild(LastRow);
//	}
	
	//Public method for adding Separator Menu Items.
	this.AddSeparatorItem = function()
	{
	    var Tr = ContextMenuTable.insertRow(Index++);
	    Tr.style.cursor = 'default';
	    
	    var TdLeft = Tr.insertCell(0);
	    TdLeft.style.width = 25 + 'px';
	    TdLeft.style.height = '1px';
	    TdLeft.style.backgroundColor = '#E8E3DB';
		
	    var TdCenter = Tr.insertCell(1);
	    TdCenter.style.width = 10 + 'px';
	    TdCenter.style.height = '1px';
	    TdCenter.style.backgroundColor = 'white';
	    
	    var TdRight = Tr.insertCell(2);
	    TdRight.style.width = (Width - 25) + 'px';
	    TdRight.style.height = '1px';
	    TdRight.style.backgroundColor = 'gray';
	}
	

	
	//Public method for displaying Context Menu.
	this.Display = function(e,element)
	{
	    e = e ? e : window.event;	 
	    document.getElementById('<%=hdfClickedTd.ClientID %>').value = element.id;
        
	    //Get the event clicked position.
	    var xLeft = e.clientX;
	   
	    //If window is scroll horizontally then add scrolled length. 
	    if(document.documentElement.scrollLeft > 0)
	    {
	        xLeft = e.clientX + document.documentElement.scrollLeft;
	    }
	    
	    //If Context menu is out of page then shift the position to left side.
//		if(xLeft + ContextMenuDiv.offsetWidth > Base.offsetWidth)
//			xLeft = Base.offsetWidth - ContextMenuDiv.offsetWidth;
		
		//var yTop = document.body.clientHeight - e.clientY + ContextMenuDiv.offsetWidth;
		//var yTop = e.clientY + document.body.scrollTop - document.body.clientTop;

        ////Get the event clicked top position.
        var  yTop = e.clientY;
        
        //If window is scroll down then add scrolled length. 
        if(document.documentElement.scrollTop > 0)
        {	
		  yTop  =  e.clientY + document.documentElement.scrollTop;
		}
		
		//If Context menu is displaying out of page then shift the position to above side.
//		if(yTop + ContextMenuDiv.offsetHeight > Base.clientHeight)
//    		yTop = Base.clientHeight - ContextMenuDiv.offsetHeight;
		
		//Set the position for ContextMenuDiv.
	    ContextMenuDiv.style.visibility = 'hidden';
	    ContextMenuDiv.style.left = (xLeft) + 'px';
        ContextMenuDiv.style.top = (yTop) + 'px';
        ContextMenuDiv.style.visibility = 'visible';
        
        return false;
	}	
	
	//Public method to hide context Menu.
	this.Hide = function()
	{
	    //oCustomContextMenu.RemoveRow(null, 'Allocate', true, 'Allocate');   
		ContextMenuDiv.style.visibility='hidden';
	}
	
	//Public method Dispose.
	this.Dispose = function()
	{
	    while(EventHandlers.length > 0)
	        DetachEventHandler(EventHandlers.pop());
		//oCustomContextMenu.RemoveRow(null, 'Allocate', true, 'Allocate');  	
	    document.body.removeChild(ContextMenuDiv);
	}
	
	//Public method GetTotalItems.
	this.GetTotalItems = function()
	{
	    return ContextMenuTable.getElementsByTagName('tr').length;
	}
	
	//Mouseover event handler
	var MouseOver = function(Tr, TdLeft, TdCenter, TdRight)
	{	
	     Tr.style.fontWeight = 'bold';
	     Tr.style.color = HoverFontColor;
	     Tr.style.backgroundColor = HoverBackgroundColor;
			
	     TdLeft.style.borderTopColor = HoverBorderColor;
	     TdLeft.style.borderBottomColor = HoverBorderColor;
	     TdLeft.style.borderLeftColor = HoverBorderColor;
	     TdLeft.style.backgroundColor = HoverBackgroundColor;
			
	     TdCenter.style.borderTopColor = HoverBorderColor;
	     TdCenter.style.borderBottomColor = HoverBorderColor;
	        
	     TdRight.style.borderTopColor = HoverBorderColor;
	     TdRight.style.borderBottomColor = HoverBorderColor;
	     TdRight.style.borderRightColor = HoverBorderColor;
	}
	
	//Mouseout event handler
	var MouseOut = function(Tr, TdLeft, TdCenter, TdRight)
	{	
	     Tr.style.fontWeight = 'normal';
	     Tr.style.color = FontColor;
	     Tr.style.backgroundColor = 'white';
	        
	     TdLeft.style.borderTopColor = '#E8E3DB';
	     TdLeft.style.borderBottomColor = '#E8E3DB';
	     TdLeft.style.borderLeftColor = '#E8E3DB';
	     TdLeft.style.backgroundColor = '#E8E3DB';
			
		 TdCenter.style.borderTopColor = 'white';
	     TdCenter.style.borderBottomColor = 'white';
			
	     TdRight.style.borderTopColor = 'white';
	     TdRight.style.borderBottomColor = 'white';
	     TdRight.style.borderRightColor = 'white';
	}
	
	//Private method to wire up event handlers.
	var WireUpEventHandler = function(Target, Event, Listener)
	{
	    //Register event.
	    if(Target.addEventListener)	   
			Target.addEventListener(Event, Listener, false);	    
	    else if(Target.attachEvent)	   
			Target.attachEvent('on' + Event, Listener);
	    else 
	    {
			Event = 'on' + Event;
			Target.Event = Listener;	 
		}
		
	    //Collect event information through object literal.
	    var EVENT = { Target: Target, Event: Event, Listener: Listener }
	    EventHandlers.push(EVENT);
	}
	
	//Private method to detach event handlers.
	var DetachEventHandler = function(EVENT)
	{
	    if(EVENT.Target.removeEventListener)	   
			EVENT.Target.removeEventListener(EVENT.Event, EVENT.Listener, false);	    
	    else if(EVENT.Target.detachEvent)	   
	        EVENT.Target.detachEvent('on' + EVENT.Event, EVENT.Listener);
	    else 
	    {
			EVENT.Event = 'on' + EVENT.Event;
			EVENT.Target.EVENT.Event = null;	 
	    }
	}
	
	//Add Context Menu div on the document.
	document.body.appendChild(ContextMenuDiv);

	//Register events.	
	WireUpEventHandler(Base, 'click', this.Hide);
	WireUpEventHandler(ContextMenuDiv, 'contextmenu', function(){ return false; });
}


    function $(v)
    {return document.getElementById(v);} 
     
   
    var oCustomContextMenu = null;
    var oBase = null; 
    //window.onload=CreateContextMenu('element');
    
	function CreateContextMenu(e,element)
	{
	    
        //oBase = document.getElementById('<%=divSeatDetails.ClientID %>');
        //oBase = document.getElementById(element.id);
        oBase =document.body;
        var Arguments = {
            Base: oBase,
            Width: 200,
            FontColor: null,
            HoverFontColor: null,
            HoverBackgroundColor: null,
            HoverBorderColor: null,
            ClickEventListener: OnClick
        };
        
        if(oCustomContextMenu != null)
        { 
           oCustomContextMenu.Hide();
           oCustomContextMenu.Dispose();
        }
        
        oCustomContextMenu = new CustomContextMenu(Arguments); 
        
        //Shift from
       if(document.getElementById(element.id).bgColor != "#f7fe2e")
         {               
            oCustomContextMenu.AddItem("Images/Shiftfrom.png", 'Shift From', false, 'ShiftFrom');
         }
         else
         {
            oCustomContextMenu.AddItem("Images/Shiftfrom.png", 'Shift From', true, 'ShiftFrom');
         }
        
        //Shif To
        if(($('<%=hdfDragFrom.ClientID %>').value != "")  && ($(element.id).bgColor == "#f7fe2e") )
        {
            oCustomContextMenu.AddItem("Images/shiftTo.png", 'Shift To', false, 'ShiftTo');
        }
        else
        {
             oCustomContextMenu.AddItem("Images/shiftTo.png", 'Shift To', true, 'ShiftTo');    
        }
        
        //Swap
        if(($('<%=hdfDragFrom.ClientID %>').value != "") && ($(element.id).bgColor == "#009900"))
        {
            oCustomContextMenu.AddItem("Images/seatswap.jpg", 'Swap', false, 'Swap');   
        }
        else
        {
            oCustomContextMenu.AddItem("Images/seatswap.jpg", 'Swap', true, 'Swap');   
        }
         
         //Add seprator line.
        oCustomContextMenu.AddSeparatorItem();
       
       //View 
      if(document.getElementById(element.id).bgColor != "#f7fe2e")
        {
              oCustomContextMenu.AddItem("Images/seatview.png", 'View', false, 'View');
        }
      else
        {
              oCustomContextMenu.AddItem("Images/seatview.png", 'View', true, 'View');
        }
        
        //Allocate
      if(document.getElementById(element.id).bgColor == "#f7fe2e")
        {
              oCustomContextMenu.AddItem("Images/SeatAllocate.png", 'Allocate', false, 'Allocate');
        }
        else
        {
              oCustomContextMenu.AddItem("Images/SeatAllocate.png", 'Allocate', true, 'Allocate');
        }
        
        //Add seprator line.
        oCustomContextMenu.AddSeparatorItem();
        //Cancel
        oCustomContextMenu.AddItem("Images/SeatCancel.png", 'Cancel', false, 'Cancel');	
        document.getElementById('<%=hdfClickedTd.ClientID %>').value = element.id;
        oCustomContextMenu.Display(e,element);
       
        return false;
	}
    
    var OnClick = function(Sender, EventArgs)
    {
        switch(EventArgs.CommandName)
        {
        //location From.
            case 'ShiftFrom':
             if( $($('<%=hdfClickedTd.ClientID %>').value).bgColor =="#009900")
                {
                   // $($('<%=hdfClickedTd.ClientID %>').value).bgColor ="Blue";
                    $('<%=hdfDragFrom.ClientID %>').value = $('<%=hdfClickedTd.ClientID %>').value;
                }
             else
                {
                    alert("This Seat is already vacant.Please Select the another Seat,");
                }   
             break;
             
             
             //Location To.   
            case 'ShiftTo':;
                if( $($('<%=hdfClickedTd.ClientID %>').value).bgColor == "#f7fe2e")
                   {
                        if($('<%=hdfDragFrom.ClientID %>').value!='')
                        {
                            $('<%=hdfDragTo.ClientID %>').value = $('<%=hdfClickedTd.ClientID %>').value;
                            __doPostBack($('<%=hdfDragTo.ClientID %>').value,$('<%=hdfDragTo.ClientID %>').value);
                        }
                        else
                        {
                         alert("Please select a 'Shift From' Location.");
                        }
                   }
                else
                    {
                        alert("This Seat is already occupied by another employee.");
                    }
                    $('<%=hdfClickedTd.ClientID %>').value = '';

              break;
              
            case 'Swap':
                    $('<%=hdfDragTo.ClientID %>').value = $('<%=hdfClickedTd.ClientID %>').value;
                    __doPostBack($('<%=hdfDragTo.ClientID %>').value,$('<%=hdfDragTo.ClientID %>').value);
               break;
                
                //View..
            case 'View':
                     //if( $($('<%=hdfClickedTd.ClientID %>').value).bgColor == "#f7fe2e")
                var SeatID = $('<%=hdfClickedTd.ClientID %>').value;
                window.open('SeatInformation.aspx?ViewSeatID = ' + SeatID,+'Seat Information','location=center,toolbar=no,menubar=no,directories=yes,status=yes,resizable=no,scrollbars=no,height=350px,width=460px, top=200,left=290', false);

                break;
             
            case 'Allocate':
                     var SeatID =  $('<%=hdfClickedTd.ClientID %>').value;
                     
                     var BranchID = $('<%=ddlBranch.ClientID %>').value;
                     
                     var SectionID =null;
                     var frm = document.forms[0];
                        for (i=0; i < frm.length; i++)
                          {
                            // itinerate the elements searching "RadioButtons"
                            if (frm.elements[i].type == "radio")
                            {
                             
                              // Unchecked if the RadioButton is != param
                              if (frm.elements[i].checked)
                              {
                                SectionID = frm.elements[i].value;
                                break;
                              }
                            }
                          }
   
                   
                     window.open("SeatInformation.aspx?AllocateSeatID =" + SeatID + "&BranchID=" + BranchID + "&SectionID="+ SectionID ,+ "Ratting","width=490,height=380,left=190,top=160,toolbar=0,status=0,menubar=0,resizable=0");
                    // window.showModalDialog("SeatInformation.aspx?AllocateSeatID = "+ SeatID, + "null,dialogHeight:200px; dialogLeft:80px; dialogWidth:1000px;"); 
               break;   
            
            case 'Cancel':

               break;
        }
        
        oCustomContextMenu.Hide();   
    }
    
    
    //if (document.all){  
    document.onkeydown = function ()
    {  
        showKeyCode(event); 
    }
    //}  
    
    //window.onbeforeunload = function() {return false;};

    
    //avoid page refresh
    var version = navigator.appVersion; 
    function showKeyCode(e)
        {
            var keycode =(window.event) ? event.keyCode : e.keyCode;

            if ((version.indexOf('MSIE') != -1))
                {
                    if(keycode == 116)
                        {
                            event.keyCode = 0;
                            event.returnValue = false;
                            return false;
                        }
                }
            else
                {
                    if(keycode == 116)
                        {
                            return false;
                        }
                       
                }
        } 

    //window.onunload = function(){ oCustomContextMenu.Dispose(); }
    
 </script>
  
   <div style="border-color: #000000; height:88px; width:100%">
    <table style="width: 100%">
        <tr>     
          <%-- Sanju:Issue Id 50201:Added new class so that header display in other browsers also--%>
           <td align="center" class="header_employee_profile"
                style="height: 25px; filter: progid:DXImageTransform.Microsoft.Gradient(endColorstr='#7590C8', startColorstr='#0C359A', gradientType='1'); width: 100%;">
            <%--    Sanju:Issue Id 50201:End--%>
                <asp:Label ID="lblSeatAllocationHeader" runat="server" Text="Seat Allocation" CssClass="header"></asp:Label>
            </td>
        </tr>
    </table>
    
    <table style="width:99% ">
    <tr> <td>&nbsp;</td> 
         <td colspan="2">
            <asp:Label ID="lblMessage" runat="server" CssClass="Messagetext" style="height: 20px"></asp:Label>
         </td>
         <td></td>
         <td style="width:20%">
             <asp:Label ID="lblVacantColor" runat="server" BackColor="#F7FE2E" Width="15px" 
                 Height="15px" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"></asp:Label> &nbsp;
             <asp:Label ID="lblVacantSeat" runat="server" Text=" Vacant Seat" Font-Size ="X-Small" Font-Names="verdana" ></asp:Label>
         </td>
    </tr>
    
    <tr>
        <td style="height: 30px; width:3%" ></td>
        <td style="height: 30px; width:10%">
            <asp:Label ID="lblBranch" runat="server" Text="Branch :" CssClass="txtstyle"></asp:Label>&nbsp;
        </td>
        <td style="height: 30px">
           <asp:DropDownList ID="ddlBranch" runat="server" Height="22px" Width="215px" 
                onselectedindexchanged="ddlBranch_SelectedIndexChanged" AutoPostBack="true" CssClass="txtstyle">
           </asp:DropDownList>
        </td>
        <td></td>
        <td style="height: 30px;width:20%" >
            <asp:Label ID="lblOccupiedColor" runat="server" BackColor="#009900" 
                Width="15px" Height="15px" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"></asp:Label> &nbsp;
            <asp:Label ID="lblOccupiedSeat" runat="server" Text="Occupied Seat" Font-Size ="X-Small" Font-Names="verdana"></asp:Label>
        </td>
    </tr>
    
    <tr>
    <td></td>
    <td>
        <asp:Label ID="lblSection" runat="server" Text="Section :" Visible="false" CssClass="txtstyle"></asp:Label>&nbsp;
    </td>
    <td colspan="2">
        <asp:RadioButtonList ID="RbListSeatAllocation" runat="server" 
            EnableTheming="True" RepeatDirection="Horizontal" CssClass="txtstyle"
            ValidationGroup="SectionGroup" AutoPostBack="True" OnSelectedIndexChanged="RbListSeatAllocation_OnSelectedIndexChanged">
        </asp:RadioButtonList>
      </td>
    <td >
         
     </td>
     </tr>
     
     <tr> <td>&nbsp;</td> <td>&nbsp;</td> <td></td>
     </tr>
   </table>
    </div>  
    <div>
    <asp:HiddenField ID="hdfDragFrom" runat="server" />
    <asp:HiddenField ID="hdfDragTo" runat="server" />
        <asp:HiddenField ID="hdfClickedTd" runat="server" />
        <asp:HiddenField ID="hdfEncryptedQueryString" runat="server" />
    </div>
    
    
   <div id="divSeatDetails" runat="server" 
        style="position:relative;top:37px; left: 0px;">
   </div>


</asp:Content>

