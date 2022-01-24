$(function () {
    let userPicker = $('.js-selectpicker.js-roleuserpicker');

    $('.js-selectpicker').selectpicker({
        dropupAuto: false,
        // liveSearch: true,
        noneSelectedText: 'No users selected'
    });

    $('.js-roleuserform').submit(function(e) {
        let selectedUserIds = userPicker.selectpicker('val');

        selectedUserIds.forEach((t) => {
            $('<input >').attr('type', 'hidden')
                .attr('name', 'UserIds')
                .attr('value', t)
                .appendTo('.js-roleuserform')
        });
    });

    $('.js-unassignuserform').submit(function(e) {
        var checkedBoxes = document.querySelectorAll('input[name=unassigncheck]:checked');
        
        checkedBoxes.forEach((t) => {
            $('<input >').attr('type', 'hidden')
                .attr('name', 'UserIds')
                .attr('value', $(t).val())
                .appendTo('.js-unassignuserform')
        });
    });
});