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
        this.#LoadQuill();        
    }

    #SetVars = () => {
        // Main modal
        this.issueDetailModal = this.issueDetailComponent.find('.createIssueModel');

        // Data
        this.issueId = this.issueDetailComponent.find('.issueId');
        this.issueDescription = this.issueDetailComponent.find('.issueDescription')
    }

    #LoadQuill = () => {
        this.quill = new QuillEditorComponent({
            container: this.issueDetailModal.find('.quill-editor'),
            isDynamic: true,
            copyContentsTo: this.issueDescription
        });

        $(this.quill).on('save', () => {
            $.post('issuedetailcomponent/updatedescription', { 
                issueId: this.issueId.val(),
                description: this.issueDescription.val(), 
            });
        });

        // new Select2Component({
        //     container: this.selectLists
        // });
    }
}