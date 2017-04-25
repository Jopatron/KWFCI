
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
        else if (event.target.nodeName == "I")
        {
            var text = $(".next-step").html().replace(/\n/g, "");
            $('#newKWTaskModal textarea').val(text);
            
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
});
