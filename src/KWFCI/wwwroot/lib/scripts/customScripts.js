
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
        console.log(target);
        if (event.target.nodeName == "DIV" || event.target.nodeName == "SPAN")
        {
            var text = target.closest('a').text().replace(/\n/g, "").trim();
            
            $(target.closest('a').attr('data-target') + " textarea").val(text);
        }
        else if (event.target.nodeName == "I") //if they click on the <i> tag
        {
            if (target.closest('button').attr("data-target") == "#newKWTaskFromInteractionModal") //Should this be for new task or edit task
            {
                var text = target.closest('td').children('.col-xs-10').children('a').text().replace(/\n/g, "").trim(); //Find parent table cell, find child <a> tag, grab its contents, put it in the modal textarea field
                $('#newKWTaskFromInteractionModal textarea').val(text);
            }
            else if (target.closest('button').attr("data-target") == "#editKWTaskFromInteractionModal") //Same as above but for edit modal, more values to populate
            {
                var text = target.closest('td').find($('.view-TaskMessage')).val();
                console.log(text);
                $('#editKWTaskFromInteractionModal textarea').val(text);

                var dateDueFull = target.closest('td').find($('.view-TaskDateDue')).val();
                var dateDueMonth = dateDueFull.split(" ")[0];
                console.log(dateDueMonth);
                $('#editKWTaskFromInteractionModal .modal-TaskDateDue').val(dateDueMonth);

                var alertDateFull = target.closest('td').find($('.view-TaskAlertDate')).val();
                var alertDateMonth = alertDateFull.split(" ")[0];
                console.log(alertDateMonth);
                $('#editKWTaskFromInteractionModal .modal-TaskAlertDate').val(alertDateMonth);

                var priority = target.closest('td').find($('.view-TaskPriority')).val();
                console.log(priority);
                $('#editKWTaskFromInteractionModal .modal-TaskPriority').val(priority);

                var taskID = target.closest('td').find($('.view-TaskKWTaskID')).val();
                console.log(taskID);
                $('#editKWTaskFromInteractionModal .modal-TaskKWTaskID').val(taskID);
            }
        }
        //Populate the modal with established values
        else if (event.target.nodeName == "BUTTON") //if they click on the <button> tag
        {
            if (target.attr("data-target") == "#editKWTaskFromInteractionModal") //Identical to if they clicked the <i> tag
            {
                var text = target.closest('td').find($('.view-TaskMessage')).val();
                console.log(text);
                $('#editKWTaskFromInteractionModal textarea').val(text);

                var dateDueFull = target.closest('td').find($('.view-TaskDateDue')).val();
                var dateDueMonth = dateDueFull.split(" ")[0];
                console.log(dateDueMonth);
                $('#editKWTaskFromInteractionModal .modal-TaskDateDue').val(dateDueMonth);

                var alertDateFull = target.closest('td').find($('.view-TaskAlertDate')).val();
                var alertDateMonth = alertDateFull.split(" ")[0];
                console.log(alertDateMonth);
                $('#editKWTaskFromInteractionModal .modal-TaskAlertDate').val(alertDateMonth);

                var priority = target.closest('td').find($('.view-TaskPriority')).val();
                console.log(priority);
                $('#editKWTaskFromInteractionModal .modal-TaskPriority').val(priority);
            }
            else if (target.attr("data-target") == "#newKWTaskFromInteractionModal")
            {
                var text = target.closest('td').children('.col-xs-10').children('a').text().replace(/\n/g, "").trim(); //Find parent table cell, find child <a> tag, grab its contents, put it in the modal textarea field
                $('#newKWTaskFromInteractionModal textarea').val(text);
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
