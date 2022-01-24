$(function() {
    $('.js__delete-role').click(function() {
        const roleId = $(this).data('roleid');

        $.get('/admin/project-roles/getdeleteroleconfirmpartial', {roleId: roleId}).done((modal) => {
            this.modal = $('<div id="DeleteRoleModal" class="modal fade"></div>')
            this.modal.html(modal);
            $('body').append(this.modal);

            this.modal.on('hidden.bs.modal', () => {
                this.modal.remove();
            });

            this.modal.modal('show');
        });
    });
});