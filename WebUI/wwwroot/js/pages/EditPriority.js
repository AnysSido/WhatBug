$(function () {
    var defaultWhitelist = $.fn.selectpicker.Constructor.DEFAULTS.whiteList;
    defaultWhitelist.div = ['data-color', 'data-icon'];

    $('.js-selectpicker').selectpicker({
        dropupAuto: false
    });

    // Styling fix for bootstrap-select wrapping all options in a text span.
    $('.js-selectpicker').on('show.bs.select', function () {
        $('.dropdown-menu li .text').removeClass('text');
    });

    let colorPicker = $('.js-selectpicker.js-colorpicker');
    let iconPicker = $('.js-selectpicker.js-iconpicker');

    let colorId = $('#ColorId');
    let iconId = $('#IconId');

    colorPicker.selectpicker('val', colorId.val());
    iconPicker.selectpicker('val', iconId.val());

    $('.js-selectpicker').on('changed.bs.select', Update);
    
    Update();

    function Update()
    {
        let selectedColorName = $('.js-colorpicker .filter-option-inner-inner .row').data('color');
        let selectedColorId = colorPicker.selectpicker('val');

        let selectedIconName = $('.js-iconpicker .filter-option-inner-inner .row').data('icon');
        let selectedIconId = iconPicker.selectpicker('val');
        
        colorId.val(selectedColorId);
        iconId.val(selectedIconId);
        
        let exampleIcon = $('.js-priority-icon');
        exampleIcon.removeClass();
        exampleIcon.addClass('js-priority-icon');
        exampleIcon.addClass('icon icon--' + selectedIconName);
        exampleIcon.addClass('wb-color-' + selectedColorName);
        
        let kanbanCard = $('.kanban-card');
        kanbanCard.removeClass();
        kanbanCard.addClass('kanban-card card card-outline');
        kanbanCard.addClass('card-' + selectedColorName);
    }
});