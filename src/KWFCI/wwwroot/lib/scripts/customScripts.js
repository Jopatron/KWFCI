
$(document).ready(function(){
    $(".inactive").on("click", function () {
        preventDefault();
    });

    $(".editTable").click(function (event) {
        var target = $(event.target);
        
        var entityID = target.attr("data-id");
        
        $(".addID").attr("value", entityID);


        $(".editTable .interactionDate").on("change", function () {
            $(".addDate").attr("value", $(this).val());
            $(".submitButton").trigger("click");
        });
    });
    var $dateDue = $("#taskDateDue");
    $dateDue.on("change", function () {
        if ($dateDue.val() != "")
        {
            if($("#taskPriority").hasClass("hidden"))
            {
                $("#taskPriority").removeClass("hidden");
            }
            else
            {
                $("#taskPriority").addClass("hidden");
            }
        }
    });
});
