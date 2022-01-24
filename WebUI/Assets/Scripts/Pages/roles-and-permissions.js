$(function() {
    $('.js__GrantRolePermissions').click(function() {
        const schemeId = $(this).data('schemeid');
        const roleId = $(this).data('roleid');

        $.get('/admin/permission-schemes/getgrantrolepermissionspartial', {schemeId: schemeId, roleId: roleId}).done((modal) => {
            this.modal = $('<div id="GrantRolePermissionsModal" class="modal fade"></div>')
            this.modal.html(modal);
            $('body').append(this.modal);

            this.modal.on('hidden.bs.modal', () => {
                this.modal.remove();
            });

            this.modal.modal('show');
        });
    });
});