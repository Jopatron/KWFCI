
$(document).ready(function(){
    $(".inactive").on("click", function () {
        preventDefault();
    });

    $("#interactionTable").click(function (event) {
        var target = $(event.target);

        //console.log(target);
        var entityID = target.attr("data-id");
        
        $(".addID").attr("value", entityID);

    });
});
