function ShowCreateIssueComponent() {
    new CreateIssueComponent();
}

class CreateIssueComponent {
    constructor() {
        $.get('/components/getcreateissuecomponent').done((modal) => {
            $('body').append(modal);
            this.#PrepareModal();
            this.#LoadComponents();
            this.#BuildSelectPickers();
            this.#CreateEditor();
            $('#CreateIssueModal').modal('show');
        });
    }

    #PrepareModal = () => {       
        $('#CreateIssueModal').on('hidden.bs.modal', () => {
            this.#DestryModals();
        });

        $('.modal-cancel').click(() => {
            if (this.#HasChanges()) {
                $('body').append($('#ConfirmModal'));
                $('#ConfirmModal').modal('show');
            } else {
                $('#CreateIssueModal').modal('hide');
            }
        });

        $('#CancelConfirm').on('click', () => {
            $('#ConfirmModal').modal('hide');
            $('#CreateIssueModal').modal('hide');
        });

        $('#CancelGoBack').on('click', () => {
            $('#ConfirmModal').modal('hide');
        });
    }

    #LoadComponents = () => {
        this.prioritySelectComponent = new IssuePrioritySelectComponent(
            $('#IssuePrioritySelectComponent'), $('#CreateIssue-SelectedProjectId').val());
        this.assigneeUserSelector = new UserSelectorComponent(
            $('#AssigneeUserSelectComponent'), {
            prefix: "assignee",
            projectId: $('#CreateIssue-SelectedProjectId').val()
        });
        this.reporterUserSelector = new UserSelectorComponent(
            $('#ReporterUserSelectComponent'), {
            prefix: "reporter"
        });
    }

    #DestryModals = () => {
        $('#CreateIssueModal').remove();
        $('#ConfirmModal').remove();
    }

    #HasChanges = () => {
        return this.quill.getLength() > 1 || $('#CreateIssueSummary').val().length > 0;
    }

    #BuildSelectPickers = () => {
        function templating(iconElement) {
            if (!iconElement.id) {
                return iconElement.text;
            }
            return $('<span><i class="' + iconElement.element.dataset.class +'"></i>' + iconElement.text + '</span>');
        }
        
        $('#CreateIssueModal .select2').select2({
            width: '100%',
            theme: 'bootstrap4',
            dropdownParent: $('#CreateIssueModal .modal-body'),
            templateSelection: templating,
            templateResult: templating
        });

        $('#CreateIssueModal .projectselector').on('select2:select', (e) => {
            this.prioritySelectComponent.Refresh(e.params.data.id);
        });
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