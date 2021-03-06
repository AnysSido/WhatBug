$(function ($) {
    // SweetAlert2 toast config
    const Toast = Swal.mixin({
        toast: true,
        position: 'top-end',
        iconColor: 'white',
        showConfirmButton: false,
        timer: 1500,
        timerProgressBar: true,
        customClass: {
            popup: 'colored-toast'
        },
    });

    // Make the priority list sortable using JQuery-UI Sortable
    $('#priorityList').sortable({
        placeholder: 'sort-highlight',
        handle: '.js-handle',
        forcePlaceholderSize: true,
        zIndex: 999999,
        update: function() {
            const priorities = $('#priorityList').sortable('toArray', {attribute: 'key'});
            $.ajax({
                type: 'post',
                url: '/admin/priorities/updatepriorityorder',
                data: JSON.stringify(priorities),
                dataType: 'json',
                contentType: "application/json; charset=utf-8",
                success: function(response) {
                    if (response.success) {
                        Toast.fire({ icon: 'success', title: 'Priority order updated successfully.' });
                    } else {
                        Toast.fire({ icon: 'error', title: response.text });
                    }
                },
                error: function() {
                    Toast.fire({ icon: 'error', title: 'Whoops! Something went wrong.' });
                }
            });
        }
    });

    // Delete priority functionality
    $('.js__delete-priority').click(function() {
        const priorityId = $(this).data('priorityid');

        $.get('/admin/priorities/getdeleteconfirmpartial', {priorityId: priorityId}).done((modal) => {
            this.modal = $('<div id="DeletePriorityModal" class="modal fade"></div>')
            this.modal.html(modal);
            $('body').append(this.modal);

            this.modal.on('hidden.bs.modal', () => {
                this.modal.remove();
            });

            this.modal.modal('show');
        });
    });
});