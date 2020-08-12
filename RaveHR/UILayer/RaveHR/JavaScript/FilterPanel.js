//********************
//GENERAL INFO
//------------
//The Shelf v1.1
//By Digital Pen
//LICENSE STATEMENT
//-----------------
//The following code is made available free of cost for non-commercial use and is governed by a GPL license which can be viewed at http://www.gnu.org/licenses/gpl.txt.
//********************

//****************************************************************************************************************
//Modified Date         Modified By         Remarks
//10-Jun-09             Firoz Udaipurwala   Added "focus" to 1st textbox in panel
//****************************************************************************************************************

//GLOBAL VARIABLES
var shelf_status = "collapsed"

//GENERAL FUNCTIONS
function curr_top(e) {
	var the_top = parseInt(e.offsetTop);
	
	return the_top;
	}			

function curr_height(e) {
	var the_height = parseInt(e.offsetHeight);
	
	return the_height;
	}

//SHELF FUNCTION
	function activate_shelf(action) {
	   
	if (action) {
		if (action == "expand") {
			expand_shelf();
			}
		else if (action == "collapse") {
			collapse_shelf();
			}
		}
	else {
		if (shelf_status == "collapsed") {
			expand_shelf();
			}
		else if (shelf_status == "expanded") {
			collapse_shelf();
			}
		}
	}

//EXPANSION FUNCTION
function expand_shelf() {
	//Establish object variables
	var object_shelf = document.getElementById('shelf');
	var object_shelf_contents = document.getElementById('shelf_contents');
	
	//Modified by Firoz
	object_shelf_contents.style.visibility = "visible";	
	
	//Shelf calculations
	var curr_height_without_padding = curr_height(object_shelf) - 10; //Do not include the padding calculations.
	var height_to_set = curr_height_without_padding + 10;
//	Sanju:Issue Id 50201: Concatenated px so that it takes height in other browsers also
	var height_to_set_string = height_to_set + "px";
	//Sanju:Issue Id 50201 End
	
	var height_limit = curr_top(object_shelf_contents) + curr_height(object_shelf_contents) + 10;
	
	//The shelf loop: continue expansion until all the shelf content is displayed.
	if (height_to_set < height_limit) {
		//Implement height change
		if (navigator.appName == "Microsoft Internet Explorer") {
			object_shelf.style.pixelHeight = height_to_set;
			}
		else {
		    object_shelf.style.height = height_to_set_string;
			}
	
		setTimeout('expand_shelf()', 1);
		}
	else {
		shelf_status = "expanded";
		
		//Modified by Firoz
		//Start			
		SetFocusInFirstTextbox();		
	    //End
		}	
	}

//COLLAPSING FUNCTION
function collapse_shelf() {
	//Establish object variables
	var object_shelf = document.getElementById('shelf');
	var object_control_link = document.getElementById('control_link');
	
	//Shelf calculations		
	var curr_height_without_padding = curr_height(object_shelf) - 10; //Do not include the padding in calculations.
	var height_to_set = curr_height_without_padding - 10;
	var height_to_set_string = height_to_set + "px"
	
	//Sanju:Issue Id 50201 Changed pixels height(chrome issue)
	var height_limit = curr_top(object_control_link) + curr_height(object_control_link) - 6;
	//sanju End
	
	//The shelf loop: continue expansion until all the shelf content is displayed.
	if (height_to_set > height_limit) {
		//Implement height change
		if (navigator.appName == "Microsoft Internet Explorer") {
			object_shelf.style.pixelHeight = height_to_set;
			}
		else {
			object_shelf.style.height = height_to_set_string;
			}
	
		setTimeout('collapse_shelf()', 1);
		}
	else {
		shelf_status = "collapsed";
		
		//Modified by Firoz
		//Start
		object_shelf.style.pixelHeight = 18; //reset to min. height(18px).
		
		var object_shelf_contents = document.getElementById('shelf_contents');
		object_shelf_contents.style.visibility = "hidden";
		//End
		}
	}
	
	
//Set focus to 1st textbox in Panel
function SetFocusInFirstTextbox()
{
    var divShelfContent = document.getElementById('shelf_contents');
    var table = divShelfContent.firstChild;

    var inputs = document.body.getElementsByTagName('input');
//Sanju:Issue Id 50201 Added condition for table not equal to undefined
    if (table.rows != undefined) {
//Sanju:Issue Id 50201 End
        for (i = 1; i < table.rows.length; i++) {
            var row = table.rows[i];
            var textBoxCell = row.cells[0];
            if (textBoxCell.firstChild != null) {
                var textbox = textBoxCell.firstChild;

                if (textbox.type == 'text') {
                    textbox.focus();
                }
            }
        }
    }
    //Sanju:Issue Id 50201  Added else so that focus on textbox work in FF, chrome, IE10, 11
    else {
        for (i = 1; i < inputs.length; i++) {
            if (inputs[i].type == 'text') {
                var textbox = inputs[i].id;
                if (inputs[i].maxLength>4)
                document.getElementById(textbox).focus();
                break;
                }
            }
       
    
    }
}