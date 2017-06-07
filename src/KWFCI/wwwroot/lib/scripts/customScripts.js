
$(document).ready(function () {
    //This code is to remove carriage returns from the beginning line when adding the clicked-on text to the modal textarea in interactions
    $.valHooks.textarea = {
        get: function (elem) {
            return elem.value.replace(/\r?\n/g, "\r\n");
        }
    };

    $(".inactive").on("click", function (ev) {
        ev.preventDefault();
    });

    $(".editTable").click(function (event) {
        var target = $(event.target);
        //console.log(target);
        if (event.target.nodeName == "DIV" || event.target.nodeName == "BUTTON")
        {
            var entityID = target.attr("data-id");
            if(target.hasClass('alignCell'))
            {
                $('#changeBrokerModal .changeBroker-oldBrokerID').val(target.attr('data-current-broker'));
                $("#changeBrokerModal .addID").attr("value", entityID);
            }
        }
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
                if (window.location.pathname == '/Tasks')
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
                //console.log(text);
                $('#editKWTaskFromInteractionModal textarea').val(text);
            
                var dateDueFull = target.closest('td').find($('.view-TaskDateDue')).val();
                var dateDueMonth = dateDueFull.split(" ")[0];
                //console.log(dateDueMonth);
                $('#editKWTaskFromInteractionModal .modal-TaskDateDue').val(dateDueMonth);
            
                var alertDateFull = target.closest('td').find($('.view-TaskAlertDate')).val();
                var alertDateMonth = alertDateFull.split(" ")[0];
                //console.log(alertDateMonth);
                $('#editKWTaskFromInteractionModal .modal-TaskAlertDate').val(alertDateMonth);
            
                var priority = target.closest('td').find($('.view-TaskPriority')).val();
                //console.log(priority);
                $('#editKWTaskFromInteractionModal .modal-TaskPriority').val(priority);
            
                var taskID = target.closest('td').find($('.view-TaskKWTaskID')).val();
                //console.log(taskID);
                $('#editKWTaskFromInteractionModal .modal-TaskKWTaskID').val(taskID);
            }
        }
        //Populate the modal with established values
        else if (event.target.nodeName == "BUTTON") //if they click on the <button> tag
        {
            if (target.attr("data-target") == "#editKWTaskFromInteractionModal" || target.attr("data-target") == "#editKWTaskModal") //Identical to if they clicked the <i> tag
            {
                var text = target.closest('td').find($('.view-TaskMessage')).val();
                //console.log(text);
                $(target.attr("data-target") +' textarea').val(text);
                
                var dateDueFull = target.closest('td').find($('.view-TaskDateDue')).val();
                var dateDueMonth = dateDueFull.split(" ")[0];
                //console.log(dateDueMonth);
                $(target.attr("data-target") + ' .modal-TaskDateDue').val(dateDueMonth);
                
                var alertDateFull = target.closest('td').find($('.view-TaskAlertDate')).val();
                var alertDateMonth = alertDateFull.split(" ")[0];
                //console.log(alertDateMonth);
                $(target.attr("data-target") + ' .modal-TaskAlertDate').val(alertDateMonth);
                
                var priority = target.closest('td').find($('.view-TaskPriority')).val();
                //console.log(priority);
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
            $("#date-created-form .submitButton").trigger("click");
        });
    });

    
    
    //$('#NewKWTask_AlertDate').on('click', function (ev) { //If I click inside the modal
    //    var $alertDate = $('#' + $(ev.target).closest('div[id]').attr('id') + ' .modal-TaskAlertDate');
        
    //    //Grab the target's closest parent with an id, grab the id value, and create a new jquery object with a selector of the ID value + .modal-TaskAlertDate,
    //    //set a function for when it changes to hide/show the subsequent .priorityRow

    //    $alertDate.on('change', function () {
    //        var $priorityRow = $('#' + $(ev.target).closest('div[id]').attr('id') + ' .priorityRow');
    //        if ($alertDate.val() != "") {
    //            if ($priorityRow.hasClass('hidden'))
    //                $priorityRow.removeClass("hidden");
    //        }
    //        else {
    //            if (!$priorityRow.hasClass("hidden"))
    //                $priorityRow.addClass("hidden");
    //        }

    //    });
    //});
    
    $(".editTable").on('click', "#assign-task", function (ev) {
        var taskID = $(this).find(".assign-staff-task").attr("data-id");
        $("#assignStaffModal .addTaskID").val(taskID);
    });
    $('#assignStaffModal').on('click', 'li', function (ev) {
        var $staffName = $(this).attr('data-value');
        
        $('#assignStaffForm .addStaffProfileID').val($staffName);
        $('#assignStaffForm .submitButton').trigger('click');
    });

    


    $("#MessageForm").submit(function (e) {
        if ($('input[type=checkbox]:checked').length == 0) {
            e.preventDefault();
            alert("ERROR! Please select at least one checkbox");
            $(this).valid() = false;
            
        }
        if ($(this).valid()) {
            $('#buttonSelector').button('loading');
        }
        
    });
  
    

    $('#changeBrokerModal .list-group li').on('click', function () {
        var $newBroker = $(this).find('span').text();
        $('.changeBroker-newBroker').val($newBroker);
        $(this).closest('.col-xs-12').find('.submitButton').trigger('click');
    });

    $('#addKWTaskModal .dropdown-menu li').on('click', function () {
        var $name = $(this).find('.name').text().trim();
        var $staffID = $(this).attr('data-id');
        
        $('#addKWTaskModal .dropdown-toggle').text($name);
        $('#addKWTaskModal .dropdown-toggle').append('<span class="caret" style="margin-left: 10px;"></span>');
        $('#addKWTaskModal [name="StaffProfileID"]').val($staffID);
    });
    
    //List.js code
    var options = {
        valueNames: ['name', 'email']
    };
    var brokersList = new List('changeBrokerList', options);
    var staffList = new List('assignStaffList', options);
    var taskStaffList = new List('testList', options);
    var staffList2 = new List('assignStaffList2', options);
    //End List.js code

    //Apply filter text to KWTask table filter for critical alerts

    $('.grab-url').on('click', function () {
        var url = window.location.pathname;
        $('.return-url').val(url);
    });

    //Code for grabbing and setting edit broker modal values, admin or staff
    $('.editTable').on('click', 'button', function (ev) {
        
        
        if ($(this).attr("data-target") == ("#editBroker") || $(this).attr("data-target") == ("#adminEditBroker"))
        {
            var dataTarget = $(this).attr("data-target");
            var element = $(ev.target);

            console.log(dataTarget);
            
            if (ev.target.nodeName == "SPAN")
            {
                var id = element.closest('button').attr('data-id');
            }
            else
                var id = element.attr('data-id');

            if (dataTarget == "#editBroker")
            {
                var name = element.closest('.broker-parentElem').find('.broker-name').text().trim();
                var splitname = name.split(' ');
                var first = splitname[0];
                var last = splitname[1];
                var status = element.closest('.broker-parentElem').find('.broker-status').val();
                var notifications = element.closest('.broker-parentElem').find('.broker-notifications').text().trim();
            }
            else
            {
                //Should only occur in #adminEditBrokerModal
                var first = element.closest('.broker-parentElem').find('.broker-firstName').text().trim();
                var last = element.closest('.broker-parentElem').find('.broker-lastName').text().trim();
                var status = element.closest('.broker-parentElem').find('.broker-status').text().trim();
                var notifications = element.closest('.broker-parentElem').find('.broker-notifications').val();
            }

            var email = element.closest('.broker-parentElem').find('.broker-email').text().trim();          
            var type = element.closest('.broker-parentElem').find('.broker-type').text().trim();
           
            //console.log(id);

            $(dataTarget + " #FirstName").val(first);
            $(dataTarget + " #LastName").val(last);
            $(dataTarget + " #Email").val(email);
            if (notifications == 'Yes')
            {
                $(dataTarget + " #EmailNotifications").prop('checked', true);
            }
            else if (notifications == 'No')
            {
                $(dataTarget + " #EmailNotifications").prop('checked', false);
            }
            else
            {
                //Should only occur in #adminEditBrokerModal
                $(dataTarget + " #EmailNotifications").prop('checked', notifications);
            }
            $(dataTarget + " #Type").val(type);
            $(dataTarget + " #Status").val(status);
            $(dataTarget + ' .broker-id').val(id);
        }
    })

    //edit for admin staff
    $('.editTable').on('click', 'button', function (ev) {

        if ($(this).attr("data-target") == ("#adminEditStaff")) {
            var element = $(ev.target);

            if (ev.target.nodeName == "SPAN") {
                var id = element.closest('button').attr('data-id');
            }
            else
                var id = element.attr('data-id');

            var first = element.closest('.staff-parentElem').find('.staff-firstName').text().trim();
            var last = element.closest('.staff-parentElem').find('.staff-lastName').text().trim();
            var email = element.closest('.staff-parentElem').find('.staff-email').text().trim();
            var role = element.closest('.staff-parentElem').find('.staff-role').text().trim();

            $("#adminEditStaff #FirstName").val(first);
            $("#adminEditStaff #LastName").val(last);
            $("#adminEditStaff #Email").val(email);
            $("#adminEditStaff #Role").val(role);
            $('#adminEditStaff .staff-id').val(id);
        }
    })

    //edit for admin interactions
    $('.editTable').on('click', 'button', function (ev) {

        if ($(this).attr("data-target") == ("#adminEditInteraction")) {
            var element = $(ev.target);

            if (ev.target.nodeName == "SPAN") {
                var id = element.closest('button').attr('data-id');
            }
            else
                var id = element.attr('data-id');

            var notes = element.closest('.interaction-parentElem').find('.interaction-notes').text().trim();
            var date = element.closest('.interaction-parentElem').find('.interaction-date').text().trim();
            var nextStep = element.closest('.interaction-parentElem').find('.interaction-nextStep').text().trim();
            var status = element.closest('.interaction-parentElem').find('.interaction-status').text().trim();

            $("#adminEditInteraction #Notes").val(notes);
            $("#adminEditInteraction #DateCreated").val(date);
            $("#adminEditInteraction #NextStep").val(nextStep);
            $("#adminEditInteraction #Status").val(status);
            $('#adminEditInteraction .interaction-id').val(id);
        }
    });

    $('.resetPassword').click(function () {
        alert("A reset link has been sent to your email account");
    });

    //This code detects when the due date field is changed, grabs its value, and makes it the end-date of the alert date field
    $('.modal-TaskDateDue').on('change', function () {
        console.log('fired');
        var form = $(this).closest('form');
        $(form.find('.modal-TaskAlertDate')).attr('data-date-end-date', $(this).val());
    });


});
$(window).on("load", function () {
    $('.editTable').DataTable();
    if (window.location.pathname == "/Tasks") {
        var filter = $('body').find('.filter').text();
        setTimeout(function () {
            $('.dataTables_filter').find('input').val(filter).trigger("input").trigger("change");
        }, 10);
    }
});