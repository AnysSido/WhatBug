function ShowCreateIssueComponent() {
    new CreateIssueComponent();
}

class CreateIssueComponent {
    constructor() {
        $.get('/components/getcreateissuecomponent').done((modal) => {
            $('body').append(modal);
            this.#SetVars();
            this.#RegisterEvents();
            this.#LoadComponents();
            this.#BuildSelectPickers();
            this.#CreateEditor();
            this.createIssueModal.modal('show');
        });
    }

    #SetVars = () => {
        this.createIssueComponent = $('#CreateIssueComponent');

        // Main modal
        this.createIssueModal = this.createIssueComponent.find('.createIssueModel');
        this.cancelIssueButton = this.createIssueModal.find('.modal-cancel');
        this.submitIssueButton = this.createIssueModal.find('.submit');
        this.mainForm = this.createIssueModal.find('form');
        this.issueSummary = this.createIssueModal.find('.issueSummary');

        // Project selector
        this.selectedProjectId = this.createIssueModal.find('.selectedProjectId');

        // Cancel confirm modal
        this.confirmModal = this.createIssueComponent.find('.confirmCancelModel');
        this.cancelConfirm = this.confirmModal.find('.cancelConfirm');
        this.cancelGoBack = this.confirmModal.find('.cancelGoBack');

        // Component containers
        this.prioritySelectComponentContainer = this.createIssueModal.find('.issuePriorityComponentContainer');
        this.assigneeComponentContainer = this.createIssueModal.find('.assigneeComponentContainer');
        this.reporterComponentContainer = this.createIssueModal.find('.reporterComponentContainer')

        // Select2
        this.selectLists = this.createIssueModal.find('.select2');

        // Project selector
        this.projectSelector = this.createIssueModal.find('.projectselector');
    }

    #LoadComponents = () => {
        this.prioritySelectComponent = new IssuePrioritySelectComponent(
            this.prioritySelectComponentContainer, this.selectedProjectId.val());

        this.assigneeUserSelector = new UserSelectorComponent(
            this.assigneeComponentContainer, {
            prefix: "assignee",
            projectId: this.selectedProjectId.val()
        });

        this.reporterUserSelector = new UserSelectorComponent(
            this.reporterComponentContainer, {
            prefix: "reporter"
        });
    }

    #RegisterEvents = () => {       
        this.createIssueModal.on('hidden.bs.modal', () => {
            this.createIssueComponent.remove();
        });

        this.cancelIssueButton.click(() => {
            if (this.#HasChanges()) {
                $('body').append(this.confirmModal);
                this.confirmModal.modal('show');
            } else {
                this.createIssueModal.modal('hide');
            }
        });

        this.cancelConfirm.on('click', () => {
            this.confirmModal.modal('hide');
            this.createIssueModal.modal('hide');
        });

        this.cancelGoBack.on('click', () => {
            this.confirmModal.modal('hide');
        });

        this.projectSelector.on('select2:select', (e) => {
            this.prioritySelectComponent.Load(e.params.data.id);
        });

        this.submitIssueButton.click((e) => {
            e.preventDefault();
            $.post('/components/createissue', this.mainForm.serialize());
        });
    }

    #HasChanges = () => {
        return this.quill.getLength() > 1 || this.issueSummary.val().length > 0;
    }

    #BuildSelectPickers = () => {
        function templating(iconElement) {
            if (!iconElement.id) {
                return iconElement.text;
            }
            return $('<span><i class="' + iconElement.element.dataset.class +'"></i>' + iconElement.text + '</span>');
        }
        
        this.selectLists.select2({
            width: '100%',
            theme: 'bootstrap4',
            templateSelection: templating,
            templateResult: templating
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

        // TODO: Remove
        if (quillContent.val() != '') {
            quill.setContents(JSON.parse(quillContent.val()));
        }

        quillForm.on('submit', function () {
            var text = JSON.stringify(quill.getContents());
            quillContent.val(text);
        });
    }
}