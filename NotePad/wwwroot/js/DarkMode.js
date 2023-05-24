$(document).ready(function () {
    // Set the initial styles for the body element
    $('body').css({
        'background-color': '#f5f5f5',
        'color': '#333'
    });

    $('#flexSwitchCheckChecked').change(function () {
        if ($(this).is(':checked')) {
            // Apply the dark mode styles
            $('body').css({
                'background-color': '#333',
                'color': 'white'
            });
            $(".card").css({
                'background-color': 'rgb(21 56 92)',
                'color': 'black'
            });
        } else {
            // Revert to the light mode styles
            $('body').css({
                'background-color': '#f5f5f5',
                'color': 'black'
            });
            $(".card").css({
                'background-color': '#f5f5f5',
            });
        }
    });
});