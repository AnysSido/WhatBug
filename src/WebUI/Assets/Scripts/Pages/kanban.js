// import dragula from "dragula"
import ShowIssueDetailComponent from "../Components/issue-detail-component"


$(function ($) {
    const Toast = Swal.mixin({
        toast: true,
        position: 'top-end',
        iconColor: 'white',
        showConfirmButton: false,
        timer: 2000,
        timerProgressBar: true,
        customClass: {
            popup: 'colored-toast'
        },
    });

    var drag = dragula($('.drag-container').toArray(), {
        revertOnSpill: true,
        accepts: function(el, target, source) {
            return target != source;
        }
    });

    const animationClass = 'animate__animated animate__rubberBand animate__fast';

    drag.on('drag', (el, source) => {
        $(el).removeClass(animationClass);
        $('.drag-container').not($(source)).addClass('dragula-border');
    });

    drag.on('dragend', (el) => {
        $('.drag-container').removeClass('dragula-border');
    })

    drag.on('over', (el, container, source) => {
        $(container).addClass('dragula-hover');
    })

    drag.on('out', (el, container, source) => {
        $(container).removeClass('dragula-hover');
    });

    drag.on('drop', (el, target, source, sibling) => {
        const issueId = $(el).find('.issueId').val();
        const issueStatusId = $(target).find('.statusId').val();

        $.post('/kanban/set-issue-status', { issueId: issueId, issueStatusId: issueStatusId })
        .done((result) => {
            if (result.success) {
                Toast.fire({ icon: 'success', title: 'Updated issue ' + issueId + '.' });
            } else {
                Toast.fire({ icon: 'error', title: 'Oops! Something went wrong...' });
            }
            $(el).addClass(animationClass);
        })
        .fail(() => {
            Toast.fire({ icon: 'error', title: 'Oops! Something went wrong...' });
        });
    });

    $('.issue-card').on('click', function() {
        const issueId = $(this).find('.issueId').val();
        ShowIssueDetailComponent(issueId);
    });

    // issue var declared in razor if issue was passed as query param
    if(typeof issue !== 'undefined'){
        ShowIssueDetailComponent(issue);
    }

    if (window.matchMedia("(max-width: 1024px)").matches || matchMedia('(hover: none), (pointer: coarse)').matches) {
        $('.kanban-warning').removeClass('d-none');
      }
});