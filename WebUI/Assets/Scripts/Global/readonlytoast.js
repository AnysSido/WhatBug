$(document).ready(function(){   
    // Display warning toast when ajax post actions are attempted in read-only mode.
    let IsReadOnly = $('.wb-readonly-marker').length;
    let IsPostAction = $('.wb-post-marker').length;

    let Toast = Swal.mixin({
        toast: true,
        position: 'top-end',
        iconColor: 'white',
        showConfirmButton: false,
        timer: 2000,
        customClass: {
            popup: 'colored-toast'
        }
    });

    function FireToast() {
        Toast.fire({ icon: 'info', title: 'Read-Only Mode', text: 'Changes not persisted.' });
    }

    // The IsPostAction marker is added to the page by the server when a non-AJAX POST action was performed.
    if (IsReadOnly && IsPostAction) {
        FireToast();
    }

    $(document).ajaxComplete(function(e, jqXHR, ajaxOptions ) {
        if (IsReadOnly && ajaxOptions.type === 'POST') {
            FireToast();
        }
    });
});