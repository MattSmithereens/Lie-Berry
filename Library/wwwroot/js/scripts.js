$(function() {
    if($("#select-menu").val() > 0) {
        $("#patron-actions").fadeIn();
    }
    else {
        $("#patron-select").change(function(e) {
            $("#patron-actions").fadeIn();
        });
    }

    
});