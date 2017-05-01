﻿
$(document).ready(function () {
    //This code is to remove carriage returns from the beginning line when adding the clicked-on text to the modal textarea in interactions
    $.valHooks.textarea = {
        get: function (elem) {
            return elem.value.replace(/\r?\n/g, "\r\n");
        }
    };

    $(".inactive").on("click", function () {
        preventDefault();
    });

    $(".editTable").click(function (event) {
        var target = $(event.target);
        
        var entityID = target.attr("data-id");
        $(".addID").attr("value", entityID);
        
        if (event.target.nodeName == "A")//Clicked an A tag in the editTable table
        {
            var text = target.text().replace(/\n/g, ""); //Get the text inside the <a> tag
            $(target.attr('data-target') + ' textarea').val(text); //Target whatever the value of the data-target is + ' textarea' and set its contents = to the <a> tag's text
        }
        
        //Because 2 fields share this logic, I just made the inside code dynamic
        //Clicked the <img> tag, meaning the field was empty
        else if(event.target.nodeName == "IMG")
        {
            target.closest('a').text().replace(/\n/g, ""); //Whichever field's IMG tag I clicked, find the closest anchor tag and populate the result of whatever the value of that anchor tag's data-target attribute + ' textarea'.val() is.
            console.log(target.closest('a').attr('data-target'));
            $(target.closest('a').attr('data-target') + " textarea").val(text);
        }
        else if (event.target.nodeName == "I") //if they click on the <i> tag
        {
            if (target.closest('button').attr("data-target") == "#newKWTaskModal") //Should this be for new task or edit task
            {
                var text = target.closest('td').children('.col-xs-10').children('a').html().replace(/\n/g, ""); //Find parent table cell, find child <a> tag, grab its contents, put it in the modal textarea field
                $('#newKWTaskModal textarea').val(text);
            }
            else if (target.closest('button').attr("data-target") == "#editKWTaskModal") //Same as above but for edit modal, more values to populate
            {
                var text = target.closest('td').find($('.view-TaskMessage')).val();
                console.log(text);
                $('#editKWTaskModal textarea').val(text);

                var dateDueFull = target.closest('td').find($('.view-TaskDateDue')).val();
                var dateDueMonth = dateDueFull.split(" ")[0];
                console.log(dateDueMonth);
                $('#editKWTaskModal .modal-TaskDateDue').val(dateDueMonth);

                var alertDateFull = target.closest('td').find($('.view-TaskAlertDate')).val();
                var alertDateMonth = alertDateFull.split(" ")[0];
                console.log(alertDateMonth);
                $('#editKWTaskModal .modal-TaskAlertDate').val(alertDateMonth);

                var priority = target.closest('td').find($('.view-TaskPriority')).val();
                console.log(priority);
                $('#editKWTaskModal .modal-TaskPriority').val(priority);
            }
        }
        //Populate the modal with established values
        else if (event.target.nodeName == "BUTTON") //if they click on the <button> tag
        {
            if (target.attr("data-target") == "#editKWTaskModal") //Identical to if they clicked the <i> tag
            {
                var text = target.closest('td').find($('.view-TaskMessage')).val();
                console.log(text);
                $('#editKWTaskModal textarea').val(text);

                var dateDueFull = target.closest('td').find($('.view-TaskDateDue')).val();
                var dateDueMonth = dateDueFull.split(" ")[0];
                console.log(dateDueMonth);
                $('#editKWTaskModal .modal-TaskDateDue').val(dateDueMonth);

                var alertDateFull = target.closest('td').find($('.view-TaskAlertDate')).val();
                var alertDateMonth = alertDateFull.split(" ")[0];
                console.log(alertDateMonth);
                $('#editKWTaskModal .modal-TaskAlertDate').val(alertDateMonth);

                var priority = target.closest('td').find($('.view-TaskPriority')).val();
                console.log(priority);
                $('#editKWTaskModal .modal-TaskPriority').val(priority);
            }
            else if(target.attr("data-target") == "#newKWTaskModal")
            {
                var text = target.closest('td').children('.col-xs-10').children('a').html().replace(/\n/g, ""); //Find parent table cell, find child <a> tag, grab its contents, put it in the modal textarea field
                $('#newKWTaskModal textarea').val(text);
            }
        }

        $(".editTable .interactionDate").on("change", function () {
            $(".addDate").attr("value", $(this).val());
            $(".submitButton").trigger("click");
        });
    });

    //var $dateDue = $("#taskDateDue");
    //$dateDue.on("change", function () {
    //    if ($dateDue.val() != "")
    //    {
    //        if($("#taskPriority").hasClass("hidden"))
    //        {
    //            $("#taskPriority").removeClass("hidden");
    //        }
    //        else
    //        {
    //            $("#taskPriority").addClass("hidden");
    //        }
    //    }
    //});


    $("#buttonSelector").click(function () {
        $(this).button('loading');
    });
  
    $('.editTable').DataTable();

});
