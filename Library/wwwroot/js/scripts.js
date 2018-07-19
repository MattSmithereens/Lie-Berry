$(function() {
    if($("#select-menu").val() > 0) {
        $("#patron-actions").fadeIn();
    }
    else {
        $("#patron-select").change(function() {
            $("#patron-actions").fadeIn();
        });
    }

    $("#return").click(function()
    {
        $("#patron-select").attr("action", "check-ins").attr("method", "post");
        $("#select-patron").click();
    });

    $("#checkout").click(function()
    {
        $("#patron-select").attr("action", "checkouts").attr("method", "post");
        $("#select-patron").click();
    });

    $(".in-stock").click(function() 
    {
       $(this).removeClass("in-stock").addClass("checked-out").text("checked out"); 
    });
});