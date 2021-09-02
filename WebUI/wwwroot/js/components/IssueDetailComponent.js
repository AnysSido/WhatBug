function ShowIssueDetailComponent(issueId) {
    new IssueDetailComponent(issueId);
}

class IssueDetailComponent {
    constructor(issueId) {
        $.get('/issuedetailcomponent/getcomponent', {issueId: issueId}).done((modal) => {
            this.issueDetailComponent = $('<div id="IssueDetailComponent" class="modal fade"></div>')
            this.#BuildComponent(modal);
            this.issueDetailComponent.modal('show');
        });
    }

    #BuildComponent(modal) {
        $('body').append(this.issueDetailComponent);
        this.issueDetailComponent.html(modal);
        this.#SetVars();
        this.#RegisterEvents();
        this.#LoadComments();
        this.#LoadQuill();
        this.#LoadDropzone();
    }

    #SetVars = () => {
        // Main modal
        this.issueDetailModal = this.issueDetailComponent.find('.issueDetailModal');

        // Data
        this.issueId = this.issueDetailComponent.find('.issueId');
        this.issueDescription = this.issueDetailComponent.find('.issueDescription')

        // Comments
        this.commentForm = this.issueDetailComponent.find('.commentForm');
        this.commentContent = this.commentForm.find('.commentContent');
        this.commentsContainer =  this.issueDetailComponent.find('.issueComments');
    }

    #RegisterEvents = () => {
        this.commentForm.submit((e) => {
            e.preventDefault();          
            $.post('issuedetailcomponent/addcomment', { issueId: this.issueId.val(), comment: this.commentContent.val() })
            .done((result) => {
                if (result.success) {
                    this.commentContent.val("");
                    this.#LoadComments();
                }
            });
        })
    };

    #LoadComments = () => {
        $.get('issuedetailcomponent/getcommentspartial', { issueId: this.issueId.val() })
            .done((resp) => {
                this.commentsContainer.html(resp);
            });
    };

    #LoadQuill = () => {
        this.issueDetailModal.find('.commentContent').on('click', () => { console.log("lol"); });
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
    }

    #LoadDropzone = () => {
        var component = this;
        var dropzone = new Dropzone('.dropzone', {
            url: '/attachments/create',
            addRemoveLinks: true,
            init: (function() {

                $.get('/attachments/getattachments', { issueId: component.issueId.val() })
                .done((attachments) => {
                    attachments = jQuery.parseJSON(attachments);

                    $.each(attachments, (_, attachment) => {
                        var mockFile = { 
                            name: attachment.OriginalFileName,
                            size: attachment.FileSize,
                            dataURL: '/attachments/get/' + attachment.FileName
                        };

                        this.emit('addedfile', mockFile);
                        this.createThumbnailFromUrl(mockFile,
                            dropzone.options.thumbnailWidth,
                            dropzone.options.thumbnailHeight,
                            dropzone.options.thumbnailMethod, true, function(thumbnail) {
                                dropzone.emit('thumbnail', mockFile, thumbnail)
                            });
                        this.emit('complete', mockFile);
                    });                    
                });
            })
        });
    }
}