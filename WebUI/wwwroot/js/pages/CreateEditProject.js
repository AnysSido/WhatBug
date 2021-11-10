$(function () {
    let selectedPrioritySchemeId = $('#PrioritySchemeId');
    let selectedPermissionSchemeId = $('#PermissionSchemeId');

    let priorityPicker = $('.js-selectpicker.js-prioritypicker');
    let permissionPicker = $('.js-selectpicker.js-permissionpicker');

    $('.js-selectpicker').selectpicker({
        dropupAuto: false
    })
    .on('loaded.bs.select', Update)
    .on('changed.bs.select', Update);

    function Update()
    {
        selectedPrioritySchemeId.val(priorityPicker.selectpicker('val'));
        selectedPermissionSchemeId.val(permissionPicker.selectpicker('val'));
    }
});