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
                'color': 'black'
            });
            $('section').css({
                'background-color': '#333',
            });
            $('.card').css({
                'background-color': '#333',
                'color': 'white'
            });
            $('table').css({
                'color': 'white'
            });
        } else {
            // Revert to the light mode styles
            $('body').css({
                'background-color': '#f5f5f5',
                'color': 'black'
            });
            $('section').css({
                'background-color': '#f5f5f5',
            });
            $('.card').css({
                'background-color': '#f5f5f5',
                'color': 'black'
            });
            $('table').css({
                'color': 'black'
            });
        }
    });
});