function ShowCreateIssueComponent() {
    new CreateIssueComponent();
}

class CreateIssueComponent {
    constructor() {
        this.createIssueComponent = $('<div id="CreateIssueComponent" class="modal fade"></div>')
        $.get('/components/getcreateissuecomponent').done((modal) => {
            this.#BuildComponent(modal);
        });
    }

    #BuildComponent(modal) {
        this.createIssueComponent.html(modal);
        this.#SetVars();
        this.#RegisterEvents();
        this.#LoadComponents();
        this.createIssueComponent.modal('show');
    }

    #SetVars = () => {
        // Main modal
        this.createIssueModal = this.createIssueComponent.find('.createIssueModel');
        this.cancelIssueButton = this.createIssueModal.find('.modal-cancel');
        this.submitIssueButton = this.createIssueModal.find('.submit');
        this.mainForm = this.createIssueModal.find('form');
        this.issueSummary = this.createIssueModal.find('.issueSummary');
        this.issueDescription = this.createIssueModal.find('.issueDescription')

        // Project selector
        this.projectSelector = this.createIssueModal.find('.projectselector');
        this.selectedProjectId = this.createIssueModal.find('.selectedProjectId');

        // Cancel confirm modal
        this.confirmModal = this.createIssueModal.find('.confirmCancelModel');
        this.cancelConfirm = this.confirmModal.find('.cancelConfirm');
        this.cancelGoBack = this.confirmModal.find('.cancelGoBack');

        // Component containers
        this.prioritySelectComponentContainer = this.createIssueModal.find('.issuePriorityComponentContainer');
        this.assigneeComponentContainer = this.createIssueModal.find('.assigneeComponentContainer');
        this.reporterComponentContainer = this.createIssueModal.find('.reporterComponentContainer')

        // Select2
        this.selectLists = this.createIssueModal.find('.select2');        

        // Quill Editor
        this.quillEditor = this.createIssueModal.find('.quill-editor');
    }

    #LoadComponents = () => {
        this.prioritySelectComponent = new IssuePrioritySelectComponent(
            this.prioritySelectComponentContainer, {
            prefix: "priority"
        }).Load(this.selectedProjectId.val());

        this.assigneeComponent = new UserSelectorComponent(
            this.assigneeComponentContainer, {
            prefix: "assignee"
        }).Load(this.selectedProjectId.val());

        this.reporterComponent = new UserSelectorComponent(
            this.reporterComponentContainer, {
            prefix: "reporter"
        }).Load(this.selectedProjectId.val());

        this.quill = new QuillEditorComponent({
            container: this.quillEditor,
            copyContentsTo: this.issueDescription
        });

        new Select2Component({
            container: this.selectLists,
            template: 'IconAndText'
        });
    }

    #RegisterEvents = () => {       
        this.createIssueComponent.on('hidden.bs.modal', () => {
            this.createIssueComponent.remove();
        });

        this.cancelIssueButton.click(() => {
            if (this.#HasChanges()) {
                $('body').append(this.confirmModal);
                this.confirmModal.modal('show');
            } else {
                this.createIssueComponent.modal('hide');
            }
        });

        this.cancelConfirm.on('click', () => {
            this.confirmModal.modal('hide');
            this.createIssueComponent.modal('hide');
        });

        this.cancelGoBack.on('click', () => {
            this.confirmModal.modal('hide');
        });

        this.projectSelector.on('select2:select', (e) => {
            var projectId = e.params.data.id;
            this.prioritySelectComponent.Load(projectId, { ignoreSelectedValue: true });
            this.assigneeComponent.Load(projectId, { ignoreSelectedValue: true });
            this.reporterComponent.Load(projectId, { ignoreSelectedValue: true });
        });

        this.submitIssueButton.click((e) => {
            e.preventDefault();
            $.post('/components/createissue', this.mainForm.serialize())
            .done((modal) => {
                if (modal.success) {
                    this.createIssueComponent.modal('hide');
                } else {
                    this.#BuildComponent(modal);
                }
            })
            .fail(() => {
                console.log("error");
            });
        });
    }

    #HasChanges = () => {
        return !this.quill.IsEmpty() || this.issueSummary.val().length > 0;
    }
}