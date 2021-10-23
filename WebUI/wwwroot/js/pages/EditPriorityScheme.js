$(function ($) {
    $('#availablePriorityList, #selectedPriorityList').sortable({
        connectWith: '.js-sortablelist'
    });

    $('.js-priorityschemeform').submit(function(e) {
        const selectedPriorityIds = $('#selectedPriorityList').sortable('toArray', { attribute: 'key'});

        selectedPriorityIds.forEach((t) => {
            $('<input >').attr('type', 'hidden')
                .attr('name', 'PriorityIds')
                .attr('value', t)
                .appendTo('.js-priorityschemeform')
        });
        return true;
    });
});