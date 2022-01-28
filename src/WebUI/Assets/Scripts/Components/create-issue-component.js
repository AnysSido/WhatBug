import QuillEditorComponent from "./quill-editor-component"
import Select2Component from "./select2-component"

export default function ShowCreateIssueComponent() {
    new CreateIssueComponent();
}

class CreateIssueComponent {
    constructor() {
        $.get('/createissuecomponent/getcomponent').done((modal) => {
            this.createIssueComponent = $('<div id="CreateIssueComponent" class="modal fade"></div>')
            this.#BuildComponent(modal);
            this.createIssueComponent.modal('show');
        });
    }

    #BuildComponent(modal) {
        this.createIssueComponent.html(modal);
        this.#SetVars();
        this.#RegisterEvents();
        this.#LoadComponents();        
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

        // Cancel confirm modal
        this.confirmModal = this.createIssueModal.find('.confirmCancelModel');
        this.cancelConfirm = this.confirmModal.find('.cancelConfirm');
        this.cancelGoBack = this.confirmModal.find('.cancelGoBack');

        // Select2
        this.selectLists = this.createIssueModal.find('.select2');        

        // Quill Editor
        this.quillEditor = this.createIssueModal.find('.quill-editor');
    }

    #LoadComponents = () => {
        this.quill = new QuillEditorComponent({
            container: this.quillEditor,
            copyContentsTo: this.issueDescription
        });

        new Select2Component({
            container: this.selectLists
        });
    }

    #RegisterEvents = () => {       
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
            $.post('/createissuecomponent/refresh', this.mainForm.serialize())
            .done((modal) => {
                this.#BuildComponent(modal);
            });
        });

        this.submitIssueButton.click((e) => {
            e.preventDefault();
            $.post('/createissuecomponent/createissue', this.mainForm.serialize())
            .done((modal) => {
                if (modal.success) {
                    this.createIssueComponent.modal('hide');
                } else {
                    this.#BuildComponent(modal);
                }
            });
        });
    }

    #HasChanges = () => {
        return !this.quill.IsEmpty() || this.issueSummary.val().length > 0;
    }
}