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

    #BuildComponent(modal, firstLoad) {
        this.createIssueComponent.html(modal);
        this.#SetVars();
        this.#RegisterEvents();
        this.#RegisterModalEvents();
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
        this.confirmModal = this.createIssueComponent.find('.confirmCancelModel');
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

    #RegisterModalEvents = () => {
        // Intercept the main modal closing with changes and show the confirm modal if not already shown
        this.createIssueComponent.on('hide.bs.modal', (e) => {
            if (this.#HasChanges() && !this.confirmModal.hasClass('show')) {
                e.preventDefault();
                $('body').append(this.confirmModal);
                this.confirmModal.modal('show');
            }
        });

        // Hack to close the main modal after the confirm modal because for some reason
        // they won't both close if 'hide' is called at the same time on both.
        this.cancelConfirm.on('click', () => {
            this.confirmModal.one('hide.bs.modal', () => {
                this.createIssueComponent.modal('hide');
            });
            this.confirmModal.modal('hide');
        });

        // Hide the confirm modal if cancel is clicked
        this.cancelGoBack.on('click', () => {
            this.confirmModal.modal('hide');
        });

        // When the main modal has closed, remove the whole element
        this.createIssueComponent.one('hidden.bs.modal', () => {
            this.createIssueComponent.remove();
        }); 
    }

    #RegisterEvents = () => {      
        this.cancelIssueButton.click(() => {
            this.createIssueComponent.modal('hide');
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
                    this.confirmModal.addClass('show'); // See hack above
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