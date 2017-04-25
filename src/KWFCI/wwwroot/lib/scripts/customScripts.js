
$(document).ready(function(){
    $(".inactive").on("click", function () {
        preventDefault();
    });

    $(".editTable").click(function (event) {
        var target = $(event.target);
        
        var entityID = target.attr("data-id");
        
        $(".addID").attr("value", entityID);


        $(".editTable input").on("change", function () {
            $(".addDate").attr("value", $(this).val());
            $(".submitButton").trigger("click");
        });
    });
});
