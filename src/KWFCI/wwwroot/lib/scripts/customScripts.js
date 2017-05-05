
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
        

        if (event.target.nodeName == "DIV" || event.target.nodeName == "BUTTON")
            var entityID = target.attr("data-id");
        else if (event.target.nodeName == "SPAN")
        {
            if(target[0].parentNode.nodeName == "BUTTON")
            {
                var entityID = target.closest("button").attr("data-id");
            }
            else if (target[0].parentNode.nodeName == "DIV")
            {
                var entityID = target.closest("div").attr("data-id");
            }
        }
        else if (event.target.nodeName == "I")
            var entityID = target.closest("button").attr("data-id");
        else if (event.target.nodeName == "INPUT")
            var entityID = target.attr("data-id");
           
        console.log(entityID);
        $(".addID").attr("value", entityID);
        







        if (event.target.nodeName == "DIV")
        {
            var text = target.closest('a').text().replace(/\n/g, "").trim();
            
            $(target.closest('a').attr('data-target') + " textarea").val(text);
        }
        else if (event.target.nodeName == "SPAN")
        {
            if(target[0].parentNode.nodeName == "DIV")
            {
                var text = target.closest('a').text().replace(/\n/g, "").trim();
                $(target.closest('a').attr('data-target') + " textarea").val(text);
            }
            else if(target[0].parentNode.nodeName == "BUTTON")
            {
                var text = target.closest('td').find($('.view-TaskMessage')).val();
                $('#editKWTaskModal textarea').val(text);

                var dateDueFull = target.closest('td').find($('.view-TaskDateDue')).val();
                var dateDueMonth = dateDueFull.split(" ")[0];
                $('#editKWTaskModal .modal-TaskDateDue').val(dateDueMonth);

                var alertDateFull = target.closest('td').find($('.view-TaskAlertDate')).val();
                var alertDateMonth = alertDateFull.split(" ")[0];
                $('#editKWTaskModal .modal-TaskAlertDate').val(alertDateMonth);

                var priority = target.closest('td').find($('.view-TaskPriority')).val();
                $('#editKWTaskModal .modal-TaskPriority').val(priority);

                var taskID = target.closest('td').find($('.view-TaskKWTaskID')).val();
                $('#editKWTaskModal .modal-TaskKWTaskID').val(taskID);
            }
        }
        else if (event.target.nodeName == "I") //if they click on the <i> tag
        {
            if (target.closest('button').attr("data-target") == "#newKWTaskFromInteractionModal") //Should this be for new task or edit task
            {
                var text = target.closest('td').children('.col-xs-10').children('a').text().replace(/\n/g, "").trim(); //Find parent table cell, find child <a> tag, grab its contents, put it in the modal textarea field
                $('#newKWTaskFromInteractionModal textarea').val(text);
            }
            else if (target.closest('button').attr("data-target") == "#editKWTaskFromInteractionModal" ) //Same as above but for edit modal, more values to populate
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
            if (target.attr("data-target") == "#editKWTaskFromInteractionModal" || target.attr("data-target") == "#editKWTaskModal") //Identical to if they clicked the <i> tag
            {
                var text = target.closest('td').find($('.view-TaskMessage')).val();
                console.log(text);
                $(target.attr("data-target") +' textarea').val(text);
                
                var dateDueFull = target.closest('td').find($('.view-TaskDateDue')).val();
                var dateDueMonth = dateDueFull.split(" ")[0];
                console.log(dateDueMonth);
                $(target.attr("data-target") + ' .modal-TaskDateDue').val(dateDueMonth);
                
                var alertDateFull = target.closest('td').find($('.view-TaskAlertDate')).val();
                var alertDateMonth = alertDateFull.split(" ")[0];
                console.log(alertDateMonth);
                $(target.attr("data-target") + ' .modal-TaskAlertDate').val(alertDateMonth);
                
                var priority = target.closest('td').find($('.view-TaskPriority')).val();
                console.log(priority);
                $(target.attr("data-target") + ' .modal-TaskPriority').val(priority);
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

    

    $('.modal-dialog').on('click', function (ev) { //If I click inside the modal
        if ($(ev.target).hasClass('modal-TaskAlertDate')) //If the target is the alert date field
        {
            var $alertDate = $('#'+ $(ev.target).closest('div[id]').attr('id') + ' .modal-TaskAlertDate');
            console.log($(ev.target).closest('div[id]').attr('id'));
            //Grab the target's closest parent with an id, grab the id value, and create a new jquery object with a selector of the ID value + .modal-TaskAlertDate,
            //set a function for when it changes to hide/show the subsequent .priorityRow
            
            $alertDate.on('change', function () {
                console.log('event fired');
                var $priorityRow = $('#' + $(ev.target).closest('div[id]').attr('id') + ' .priorityRow');
                if($alertDate.val() != "")
                {
                    if($priorityRow.hasClass('hidden'))
                        $priorityRow.removeClass("hidden");
                }
                else
                {
                    if (!$priorityRow.hasClass("hidden"))
                         $priorityRow.addClass("hidden");
                }

            });
        }
    });
    


    $("#buttonSelector").click(function () {
        $(this).button('loading');
    });
  
    $('.editTable').DataTable();


});
