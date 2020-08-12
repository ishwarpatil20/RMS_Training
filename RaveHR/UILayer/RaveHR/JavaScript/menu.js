
    var submenucontent=new Array()
    var submenuids = new Array()
    var mainmenutabs = "ctl00_tabHome,ctl00_tabProject,ctl00_tabMRF,ctl00_tabRecruitment,ctl00_tabAppraisal,ctl00_tabEmployee,ctl00_tabLeave,ctl00_tabContract,ctl00_tabSeatAllocation,ctl00_tabRMSReports,ctl00_tab4C,ctl00_tabTraining"
    var mainmenutab = mainmenutabs.split(",")
    var mainmenutabIndex

    var submenutabs = "homesubmenu,projectsubmenu,MRFSubMenu,RecruitmentSubMenu,Div3,EmpSubMenu,Div5,contractSubMenu,divSeatAllocationSubMenu,divRMSReports,divFourCSubMenu"
    var tabs = submenutabs.split(",")
    var submenutab, currentpagetab
    for(k = 0; k < tabs.length; k++)
    {
        if(document.getElementById(tabs[k]))
        {
            submenucontent[k] = document.getElementById(tabs[k]).innerHTML
            submenuids[k] = tabs[k]
        }
    }
    //Set delay before submenu disappears after mouse moves out of it (in milliseconds)
    var delay_hide=500

    /////No need to edit beyond here

    var menuobj=document.getElementById? document.getElementById("displaysubmenu") : document.all? document.all.displaysubmenu : document.layers? document.dep1.document.dep2 : ""

    function showit(which)
    {
        clear_delayhide()
        thecontent=(which==-1)? "" : submenucontent[which]
        if (document.getElementById||document.all)
        //Contract -7,Project-1,MRF-2,Recruitment-3,Appraisal-4,Employee-5,Leave-6,SeatAllocation-8,Reports-9
        {
            menuobj.innerHTML=thecontent
            menuobj.style.visibility = "visible";
            
            //--Apply Styles to sub menu
            menuobj.style.position = "absolute";
            menuobj.style.top = 138;
            menuobj.style.zIndex = "100";
            var offsetleft = 8
            
            //--Display submenu
            var onmouseoversubmenu = document.getElementById(mainmenutab[which]);
            if (onmouseoversubmenu && (which != 0))
            //	Sanju:Issue Id 50201: Concatenated px so that it takes height in other browsers also
                menuobj.style.left = onmouseoversubmenu.offsetLeft + offsetleft + "px";
            //Sanju:Issue Id 50201 End
            else menuobj.style.visibility = "hidden";
        }
        else if (document.layers)
        {
            menuobj.document.write(thecontent)
            menuobj.document.close()
        }
        
        //--maintain for menu & submenu bg
        setbgOnmouseover(which)
        resetbgOnmouseout() //--
    }

    function resetit(e)
    {
        if (document.all&&!menuobj.contains(e.toElement))
        delayhide=setTimeout("showit(-1)",delay_hide)
        else if (document.getElementById&&e.currentTarget!= e.relatedTarget&& !contains_ns6(e.currentTarget, e.relatedTarget))
        delayhide=setTimeout("showit(-1)",delay_hide)
        
        //--remove bg for maintabs
        resetbgOnmouseout()
    }

    function clear_delayhide()
    {
        if (window.delayhide)
        clearTimeout(delayhide)
        setbgOnmouseover(mainmenutabIndex)
    }

    function contains_ns6(a, b) 
    {
        while (b.parentNode)
        if ((b = b.parentNode) == a)
        return true;
        return false;
    }

    function setbgToTab(maintab, divSubMenu)
    {
        currentpagetab = maintab
        submenutab = divSubMenu
        var tab = document.getElementById(maintab)
        var subtab = document.getElementById(divSubMenu)
        if (tab) { tab.style.backgroundColor = "#848484"; tab.style.color = "#FFFFFF"; }
        //if (subtab) {subtab.style.backgroundColor = "#CCEEFF";}
    }
    
    function setbgOnmouseover(which)
    {
        mainmenutabIndex = which
        var tab = document.getElementById(mainmenutab[mainmenutabIndex])
        if (tab) { tab.style.backgroundColor = "#848484"; }
        
        var subtab = document.getElementById(submenutab)
        if (subtab) { subtab.style.backgroundColor = "#D8D8D8" }    
    }
    
    function resetbgOnmouseout()
    {
        for(k = 0; k < mainmenutab.length; k++)
        {
            //if(currentpagetab != mainmenutab[k])
            if((currentpagetab != mainmenutab[k]) && (accessdeniedmenus(mainmenutab[k])))
            {
            var maintabbg = document.getElementById(mainmenutab[k])
            if (maintabbg) { maintabbg.style.backgroundColor = "";}
            }
        }    
    }
    
    function setbackcolorToTab(divSubMenu)
    {
        submenutab = divSubMenu
        var subtab = document.getElementById(divSubMenu)
        if(subtab){subtab.style.backgroundColor = "#848484"}
    }
    
    function accessdeniedmenus(menutab)
    {
        var accessdeniedmenus = document.getElementById("ctl00_hhdAmenus").value;
        var tabs = accessdeniedmenus.split(",")
        //--Check if menutab exits in access denied menus
        for(j =0; j < tabs.length; j++)
        {
            if(tabs[j] == menutab)
            {
                return false;
            }
        }
        
       //--
       return true
    }



