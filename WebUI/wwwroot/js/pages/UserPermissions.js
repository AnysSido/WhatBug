$(function() {
    $('.js__grant-user-permission').click(function() {
        const userId = $(this).data('userid');

        $.get('/admin/user-permissions/getgrantuserpermissionspartial', { userId: userId }).done((modal) => {
            this.modal = $('<div id="GrantUserPermissionsModal" class="modal fade"></div>')
            this.modal.html(modal);
            $('body').append(this.modal);
            this.modal.modal('show');
        });
    });
});