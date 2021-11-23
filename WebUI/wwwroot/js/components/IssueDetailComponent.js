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
            $.post('/issuedetailcomponent/addcomment', { issueId: this.issueId.val(), comment: this.commentContent.val() })
            .done((result) => {
                if (result.success) {
                    this.commentContent.val("");
                    this.#LoadComments();
                }
            });
        });

        this.issueDetailComponent.on('hidden.bs.modal', () => {
            this.issueDetailComponent.remove();
        });
    };

    #LoadComments = () => {
        $.get('/issuedetailcomponent/getcommentspartial', { issueId: this.issueId.val() })
            .done((resp) => {
                this.commentsContainer.html(resp);
            });
    };

    #LoadQuill = () => {
        this.quill = new QuillEditorComponent({
            container: this.issueDetailModal.find('.quill-editor'),
            isDynamic: true,
            copyContentsTo: this.issueDescription
        });

        $(this.quill).on('save', () => {
            $.post('/issuedetailcomponent/updatedescription', { 
                issueId: this.issueId.val(),
                description: this.issueDescription.val(), 
            });
        });
    }

    #LoadDropzone = () => {
        var previewTemplate = $(".attachment-thumbnail-container").html();
        $(".attachment-thumbnail-container").html("");

        var dropzone = new Dropzone('.issue-detail-body', {
            url: '/attachments/create',
            params: { issueId: this.issueId.val() },
            thumbnailHeight: 125,
            thumbnailWidth: 156,
            previewTemplate: previewTemplate,
            previewsContainer: '.attachment-thumbnail-container',
            init: (() => {
                $.get('/attachments/getattachments', { issueId: this.issueId.val() })
                .done((attachments) => {
                    attachments = jQuery.parseJSON(attachments);

                    $.each(attachments, (_, attachment) => {
                        var str = attachment.OriginalFileName;
                        if (str.length > 20) {
                            attachment.OriginalFileName = str.substr(0, 10) + '...' + (str.substr(str.length-10, str.length))
                        }

                        var mockFile = { 
                            name: attachment.OriginalFileName,
                            size: attachment.FileSize,
                            dataURL: '/attachments/get/' + attachment.FileName
                        };

                        dropzone.emit('addedfile', mockFile);
                        dropzone.createThumbnailFromUrl(mockFile,
                            dropzone.options.thumbnailWidth,
                            dropzone.options.thumbnailHeight,
                            dropzone.options.thumbnailMethod, true, function(thumbnail) {
                                dropzone.emit('thumbnail', mockFile, thumbnail)
                            });
                        dropzone.emit('complete', mockFile);
                    });                    
                });
            })
        });

        // dropzone.on('dragstart', () => {
        //     $('.issue-detail-body').addClass('issue-detail-body-attaching');
        // });
        // dropzone.on('dragleave', () => {
        //     $('.issue-detail-body').removeClass('issue-detail-body-attaching');
        // })
    }
}