var drag = dragula($('.drag-container').toArray(), {
    revertOnSpill: true,
    accepts: function(el, target, source) {
        return target != source;
    }
});

const animationClass = 'animate__animated animate__rubberBand animate__fast';

drag.on('drag', (el, source) => {
    $(el).removeClass(animationClass);
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
    const issueId = $(el).find('.issueId').val();
    const issueStatusId = $(target).find('.statusId').val();

    $.post('/kanban/SetIssueStatus', { issueId: issueId, issueStatusId: issueStatusId })
    .done((result) => {
        $(el).addClass(animationClass);
    });
});

$('.issue-card').on('click', function() {
    const issueId = $(this).find('.issueId').val();
    ShowIssueDetailComponent(issueId);
});