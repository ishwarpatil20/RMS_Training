
$(document).ready(function () {

    $('.Dropdownstyle').change(function () {
        $('#divSucess').html('');
        debugger;
        RenderList();
    });

    $(document).on("click", ".Headercheckedbox", function () {
        var chkHeader = $(this);
        //Find and reference the GridView.
        var grid = $(this).closest("table");

        //Loop through the CheckBoxes in each Row.
        $("td", grid).find("input[type=checkbox]").each(function () {

            //If Header CheckBox is checked.
            //Then check all CheckBoxes and enable the TextBoxes.
            if (chkHeader.is(":checked")) {
                $(this).attr("checked", "checked");
                var td = $("td", $(this).closest("tr"));
                td.css({ "background-color": "#D8EBF2" });
                $("input[type=text]", td).removeAttr("disabled");
            } else {
                $(this).removeAttr("checked");
                var td = $("td", $(this).closest("tr"));
                td.css({ "background-color": "#FFF" });
                $("input[type=text]", td).attr("disabled", "disabled");
            }
        });
    });

    $(document).on("click", ".checkedbox", function () {
        //Find and reference the GridView.
        var grid = $(this).closest("table");

        //Find and reference the Header CheckBox.
        var chkHeader = $(".Headercheckedbox", grid);

        //If the CheckBox is Checked then enable the TextBoxes in thr Row.
        if (!$(this).is(":checked")) {
            var td = $("td", $(this).closest("tr"));
            td.css({ "background-color": "#FFF" });
            $("input[type=text]", td).attr("disabled", "disabled");
            $("select", td).attr("disabled", "disabled");
        } else {
            var td = $("td", $(this).closest("tr"));
            td.css({ "background-color": "#D8EBF2" });
            $("input[type=text]", td).removeAttr("disabled");
            $("select", td).removeAttr("disabled");
        }

        //Enable Header Row CheckBox if all the Row CheckBoxes are checked and vice versa.
        if ($(".checkedbox", grid).length == $("[id*=chkRow]:checked", grid).length) {
            chkHeader.attr("checked", "checked");
        } else {
            chkHeader.removeAttr("checked");
        }



    });

    function RenderList() {

        var flg = Validation();
        if (flg) {
            $("#divSucess").hide();
            /* Request the partial view with .get request. */
            ProjectId = $('#ddlProjects').val();
            Year = $('#ddlYear').val();
            Month = $('#ddlMonths').val();

      

            ProjectId = $('#ddlProjects').val();
            Year = $('#ddlYear').val();
            Month = $('#ddlMonths').val();

          var  objBudgetModel = { 'Year': Year, 'Month': Month, 'ProjectId': ProjectId };

            debugger;

            url = "../Budget/RenderCostCodeLists/";

            $.ajax({
                url: url
, type: 'POST'
, contentType: 'application/json'
, data: JSON.stringify(objBudgetModel) //stringify is important
, success: function (data) {
    $('#divCostCodeList').html('');
    $('#divCostCodeList').html(data);
    $('#divCostCodeList').show('fold');
    $('#tblCostCodes').dataTable({ "aaSorting": [[2, 'asc']], "paging": false});
},
                fail: function () {
                    $('#divError').html('Some error occured while saving..')
                }
            });


            //$.get('/Budget/RenderCostCodeLists/' , JSON.stringify(objBudgetModel), function (data) {
            //    $('#divCostCodeList').html('');
            //    $('#divCostCodeList').html(data);
            //    $('#divCostCodeList').show('fold');
            //    $('#tblCostCodes').dataTable({ "aaSorting": [[0, 'desc']] });
            //});
        }
        else {
            $('#divCostCodeList').html('');
        }
    }
    function ShowValidation(message)
    {
        $('#divError').html(message);
        $('#divError').show('fold');
        window.setTimeout(function () {
            $('#divError').html('');
            $('#divError').hide('fold');
        }, 4000);

    }
    $(document).on('click', "#btnSave", function () {
     
        Validation();

        var arrCostCode = [];
        var grid = $('#tblCostCodes');
        ProjectId = $('#ddlProjects').val();
        Year = $('#ddlYear').val();
        Month = $('#ddlMonths').val();
        var flgBusinessVertical = false;
        $('.checkedbox').each(function () {
          
            if ($(this).is(":checked")) {              
                var td = $("td", $(this).closest("tr"));

                var textbox = td.find("[type='text']");
                var dropdown = td.find("select");

                var costCodeId = textbox.attr('data-costcodeId');
                var Budget = textbox.val();
                var BusinessVertical = dropdown.val();
                var BudgetId = textbox.attr('data-budgetid');
                if (ProjectId == '-9999' && BusinessVertical == '0')
                {
                    flgBusinessVertical = true
                }

                arrCostCode.push({ CostCodeId: costCodeId, Budget: Budget, Year: Year, Month: Month, ProjectId: ProjectId, BudgetId: BudgetId,
                    BusinessVerticalId: BusinessVertical});
            }
        });
        debugger;
        if (arrCostCode.length == 0) {
            ShowValidation('Please Select Check box and Enter Budget Information');
        }        

        else if (flgBusinessVertical) {
            ShowValidation('Please Select Business Vertical');
        }
        else {

            $.ajax({
                url: "../Budget/SaveBudget"
       , type: 'POST'
       , contentType: 'application/json'
       , data: JSON.stringify(arrCostCode) //stringify is important
       , success: function (data) {
           if (data == 'Saved') {

               $('#divSucess').html("<strong>Success!</strong> Budget Information Saved..");
               $('#divSucess').show('fold');

               window.setTimeout(function () {
                   RenderList();
               }, 4000);
           }
       },
                fail: function () {
                    $('#divError').html('Some error occured while saving..')
                }
            });

        }
    });

   
    $("#ddlMonths option:contains(" + $(hdMonth).val() + ")").attr('selected', 'selected');

    $(document).on('click', "#btnDelete", function () {

        Validation();

        var arrCostCode = [];
        var grid = $('#tblCostCodes');
        ProjectId = $('#ddlProjects').val();
        Year = $('#ddlYear').val();
        Month = $('#ddlMonths').val();
        var flgBusinessVertical = false;
        $('.checkedbox').each(function () {

            if ($(this).is(":checked")) {
                var td = $("td", $(this).closest("tr"));

                var textbox = td.find("[type='text']");
               
                var BudgetId = textbox.attr('data-budgetid');
               

                arrCostCode.push({
                   BudgetId: BudgetId,
                   
                });
            }
        });

        if (arrCostCode.length == 0) {
            ShowValidation('Please select checkbox to delete');
        }
        else {
            $.ajax({
                url: "../Budget/DeleteBudget"
       , type: 'POST'
       , contentType: 'application/json'
       , data: JSON.stringify(arrCostCode) //stringify is important
       , success: function (data) {
           if (data == 'Delete') {

               $('#divSucess').html("<strong> Budget Deleted..</strong>");
               $('#divSucess').show('fold');

               window.setTimeout(function () {
                   RenderList();
               }, 4000);
           }
       },
                fail: function () {
                    $('#divError').html('Some error occured while saving..')
                }
            });

        }
    });
});