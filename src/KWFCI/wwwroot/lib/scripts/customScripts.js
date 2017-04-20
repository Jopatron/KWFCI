
$(document).ready(function(){
    $(".inactive").on("click", function () {
        preventDefault();
    });

    $("#interactionTable").click(function (event) {
        var target = $(event.target);
        
        var interactionID = target.attr("data-id");
        
        $(".addInteractionID").attr("value", interactionID);
    });
});
