function ShowIssueDetailComponent(issueId) {
    new IssueDetailComponent(issueId);
}

class IssueDetailComponent {
    constructor(issueId) {
        $.get('/issuedetailcomponent/getcomponent', {issueId: issueId}).done((modal) => {
            this.issueDetailComponent = $('<div id="CreateIssueComponent" class="modal fade"></div>')
            this.#BuildComponent(modal);
            this.issueDetailComponent.modal('show');
        });
    }

    #BuildComponent(modal) {
        this.issueDetailComponent.html(modal);
        this.#SetVars();
        this.#RegisterEvents();
        this.#LoadComponents();        
    }

    #SetVars = () => {
        // Main modal
        this.issueDetailModal = this.issueDetailComponent.find('.createIssueModel');
        this.issueDescription = this.issueDetailComponent.find('.issueDescription')
        this.issueId = this.issueDetailComponent.find('.issueId');

        // Quill Editor
        this.quillEditor = this.issueDetailModal.find('.quill-editor');
        this.quillButtons = this.issueDetailModal.find('.quill-savecancel');
        this.quillSaveButton = this.quillButtons.find('.quill-save');
    }

    #LoadComponents = () => {
        this.quill = new QuillEditorComponent({
            container: this.quillEditor,
            readOnlyEditor: true,
            copyContentsTo: this.issueDescription
        });

        this.quillEditor.on('click', () => {
            if (this.quill.readOnlyEditor) {
                this.quill.MakeEditable();
                this.quillButtons.removeClass('d-none');
            }            
        });

        this.quillSaveButton.on('click', () => {
            this.#UpdateDescription();
        });

        // new Select2Component({
        //     container: this.selectLists
        // });
    }

    #RegisterEvents = () => {       

    }

    #UpdateDescription = () => {
        $.post('issuedetailcomponent/updatedescription', { 
            issueId: this.issueId.val(),
            description: this.issueDescription.val(), 
        }).done((result) => {
            if (result.success) {
                this.quill.MakeReadOnly();
            }
        });
    }
}