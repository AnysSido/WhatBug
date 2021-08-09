var drag = dragula($('.drag-container').toArray(), {
    revertOnSpill: true,
});

drag.on('drag', (el, source) => {
    $('.drag-container').not($(source)).addClass('dropzone-border');
});

drag.on('dragend', (el) => {
    $('.drag-container').removeClass('dropzone-border');
})

drag.on('over', (el, container, source) => {
    $(container).addClass('dropzone-hover');
})

drag.on('out', (el, container, source) => {
    $(container).removeClass('dropzone-hover');
});