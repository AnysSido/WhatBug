$(function() {
    $('.js__delete-priorityscheme').click(function() {
        const schemeId = $(this).data('schemeid');

        $.get('/admin/priority-schemes/getdeleteconfirmpartial', {schemeId: schemeId}).done((modal) => {
            this.modal = $('<div id="DeletePrioritySchemeModal" class="modal fade"></div>')
            this.modal.html(modal);
            $('body').append(this.modal);

            this.modal.on('hidden.bs.modal', () => {
                this.modal.remove();
            });

            this.modal.modal('show');
        });
    });
});