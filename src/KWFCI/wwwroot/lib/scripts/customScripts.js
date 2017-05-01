
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
        if(target.attr("data-target") == "#editInteractionNextStep")
        {
            var text = target.html().replace(/\n/g, "");
            $('#editInteractionNextStep textarea').val(text);
        }
        else if (target.attr("data-target") == "#editInteractionNotes")
        {
            var text = target.html().replace(/\n/g, "");
            $('#editInteractionNotes textarea').val(text);
        }
        else if (event.target.nodeName == "I") //if they click on the <i> tag
        {
            if (target.closest('button').attr("data-target") == "#newKWTaskModal")
            {
                var text = target.closest('td').children('.col-xs-10').children('a').html().replace(/\n/g, "");
                $('#newKWTaskModal textarea').val(text);
            }
            else if (target.closest('button').attr("data-target") == "#editKWTaskModal")
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
            if (target.attr("data-target") == "#editKWTaskModal")
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
