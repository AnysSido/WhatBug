var drag = dragula($('.drag-container').toArray(), {
    revertOnSpill: true,
    accepts: function(el, target, source) {
        return target != source;
    }
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

drag.on('drop', (el, target, source, sibling) => {
    var issueId = $(el).find('.issueId').val();
    var issueStatusId = $(target).find('.statusId').val();

    $.post('/kanban/SetIssueStatus', { issueId: issueId, issueStatusId: issueStatusId });
});