class CreateIssueComponent {
    constructor() {
        $.get('/components/getcreateissuecomponent').done((modal) => {
            $('body').append(modal);
            this.#PrepareModal();
            this.#CreateEditor();
            $('#CreateIssueModal').modal('show');
        });
    }

    #PrepareModal = () => {
        $('#CreateIssueModal .selectpicker').selectpicker();

        $('#CreateIssueModal').on('hidden.bs.modal', () => {
            this.#DestryModals();
        });

        $('.modal-cancel').click(() => {
            if (this.#ChangesMade()) {
                $('body').append($('#ConfirmModal'));
                $('#ConfirmModal').modal('show');
            } else {
                $('#CreateIssueModal').modal('hide');
            }
        });

        $('#CancelConfirm').on('click', () => {
            $('#ConfirmModal').modal('hide');
            $('#CreateIssueModal').modal('hide');
        })

        $('#CancelGoBack').on('click', () => {
            $('#ConfirmModal').modal('hide');
        })        
    }

    #DestryModals = () => {
        $('#CreateIssueModal').remove();
        $('#ConfirmModal').remove();
    }

    #ChangesMade = () => {
        return this.quill.getLength() > 1 || $('#CreateIssueSummary').val().length > 0;
    }

    #CreateEditor = () => {
        var quillForm = $('.quill-form');
        var quillContent = $('.quill-content');

        this.quill = new Quill('.quill-editor', {
            theme: 'snow',
            modules: {
                syntax: true,
                toolbar: [
                    ['bold', 'italic', 'underline', 'strike'],
                    ['blockquote', 'code-block'],

                    [{ 'header': 1 }, { 'header': 2 }],
                    [{ 'list': 'ordered' }, { 'list': 'bullet' }],
                    [{ 'script': 'sub' }, { 'script': 'super' }],
                    [{ 'indent': '-1' }, { 'indent': '+1' }],
                    [{ 'direction': 'rtl' }],

                    [{ 'size': ['small', false, 'large', 'huge'] }],
                    [{ 'header': [1, 2, 3, 4, 5, 6, false] }],

                    [{ 'color': [] }, { 'background': [] }],
                    [{ 'font': [] }],
                    [{ 'align': [] }],

                    ['clean']
                ]
            }
        });

        if (quillContent.val() != '') {
            quill.setContents(JSON.parse(quillContent.val()));
        }

        quillForm.on('submit', function () {
            var text = JSON.stringify(quill.getContents());
            quillContent.val(text);
        });
    }
}