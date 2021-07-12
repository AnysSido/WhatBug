// Icon selection and color picker for priority create/edit pages
$(function () {
    var iconColor = ($('#SelectedIconColor').val() == '') ? '#495057' : $('#SelectedIconColor').val();

    // Update preview when icon or color change.
    function updateIconPreview() {
        $('#iconPreview .fas').attr('class', $('.selected-icon .fas').attr('class'));
        $('#iconPreview').css('color', iconColor);
    }

    // Init font icon picker
    var iconPicker = $('#SelectedIcon').fontIconPicker({
        theme: 'fip-darkgrey',
        emptyIcon: false,
    });

    // Init color picker
    $('.icon-colorpicker').colorpicker();
    $('.icon-colorpicker').on('colorpickerChange', function (event) {
        iconColor = event.color.toString();
        updateIconPreview();
    })
    $('#SelectedIcon').on('change', function () {
        updateIconPreview();
    });

    if ($('#PrevSelectedIcon').val() != '') {
        iconPicker.setIcon($('#PrevSelectedIcon').val());
    }

    updateIconPreview();
});