// import Dropzone from "dropzone"
import QuillEditorComponent from "./quill-editor-component";
import Select2Component from "./select2-component";

export default function ShowIssueDetailComponent(issueId) {
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
        this.#LoadIssueTypeSelect();
        this.#LoadPrioritySelect();
    }

    #SetVars = () => {
        // Main modal
        this.issueDetailModal = this.issueDetailComponent.find('.issueDetailModal');

        // Data
        this.issueId = this.issueDetailComponent.find('.issueId');
        this.issueDescription = this.issueDetailComponent.find('.issueDescription')
        this.issueTypeId = this.issueDetailComponent.find('.issueTypeId');
        this.priorityId = this.issueDetailComponent.find('.priorityId');

        // Issue Summary
        this.issueSummary = this.issueDetailComponent.find('.js-issue-summary');
        this.issueSummaryDisplay = this.issueDetailComponent.find('.js-summary-display');
        this.issueSummaryEdit = this.issueDetailComponent.find('.js-issue-summary-edit');
        this.issueSummaryInput = this.issueSummaryEdit.find('.js-summary-input');
        this.issueSummaryConfirm = this.issueSummaryEdit.find('.js-summary-confirm');
        this.issueSummaryCancel = this.issueSummaryEdit.find('.js-summary-cancel');

        // Issue Type
        this.issueType = this.issueDetailComponent.find('.js-issue-type');
        this.issueTypeSelectWrapper = this.issueDetailComponent.find('.js-issue-type-select-wrapper');
        this.issueTypeSelect = this.issueDetailComponent.find('.js-issue-type-select');

        // Priority
        this.priority = this.issueDetailComponent.find('.js-priority');
        this.prioritySelectWrapper = this.issueDetailComponent.find('.js-priority-select-wrapper');
        this.prioritySelect = this.issueDetailComponent.find('.js-priority-select');

        // Attachments
        this.requestVerificationToken = this.issueDetailComponent.find('[name="__RequestVerificationToken"]');
        this.dropzoneEmptyContainer = this.issueDetailComponent.find('.attachments-empty');

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

        this.issueSummary.click(() => {
            this.#ShowEditSummary();
        });

        this.issueSummaryEdit.keypress((e) => {
            e = e || window.event;
            if ("key" in e && e.key === "Enter")
            {
                this.#UpdateIssueSummary();
                this.#HideEditSummary();
            }
        });

        this.issueSummaryConfirm.click(() => {
            this.#UpdateIssueSummary();
            this.#HideEditSummary();
        });

        this.issueSummaryCancel.click(() => {
           this.#HideEditSummary();
        });

        this.issueType.click(() => {
            this.#ShowEditIssueType();
        });

        this.issueTypeSelect.on('select2:closing', () => {
            this.#HideEditIssueType();
        });

        this.issueTypeSelect.on('select2:select', (e) => {
            $.post('/issuedetailcomponent/updateissuetype', { issueId: this.issueId.val(), issueTypeId: e.params.data.id})
        });

        this.priority.click(() => {
            this.#ShowEditPriority();
        });

        this.prioritySelect.on('select2:closing', () => {
            this.#HideEditPriority();
        });

        this.prioritySelect.on('select2:select', (e) => {
            $.post('/issuedetailcomponent/updatepriority', { issueId: this.issueId.val(), priorityId: e.params.data.id})
        });

        this.issueDetailComponent.on('hidden.bs.modal', () => {
            this.issueDetailComponent.remove();
        });
    };

    #ShowEditSummary = () => {
        this.issueSummary.addClass('d-none');
        this.issueSummaryEdit.removeClass('d-none');
        this.issueSummaryInput.focus();
    }

    #HideEditSummary = () => {
        this.issueSummaryEdit.addClass('d-none');
        this.issueSummary.removeClass('d-none');
    }

    #ShowEditIssueType = () => {
        this.issueType.addClass('d-none');
        this.issueTypeSelectWrapper.removeClass('d-none');
    }

    #HideEditIssueType = () => {
        this.issueType.removeClass('d-none');
        this.issueTypeSelectWrapper.addClass('d-none');
    }

    #ShowEditPriority = () => {
        this.priority.addClass('d-none');
        this.prioritySelectWrapper.removeClass('d-none');
    }

    #HideEditPriority = () => {
        this.priority.removeClass('d-none');
        this.prioritySelectWrapper.addClass('d-none');
    }

    #UpdateIssueSummary = () => {
        $.post('/issuedetailcomponent/updatesummary', { 
            issueId: this.issueId.val(),
            summary: this.issueSummaryInput.val(), 
        }).done((result) => {
            if (result.success) {
                this.issueSummaryDisplay.html(this.issueSummaryInput.val());
            }
        });
    };

    #LoadIssueTypeSelect = () => {
        new Select2Component({ container: this.issueTypeSelect });
        this.issueTypeSelect.val(this.issueTypeId.val());
        this.issueTypeSelect.trigger('change');
    }

    #LoadPrioritySelect = () => {
        new Select2Component({ container: this.prioritySelect });
        this.prioritySelect.val(this.priorityId.val());
        this.prioritySelect.trigger('change');
    }

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

        if ($('.attachmentCount').val() > 0) {
            this.dropzoneEmptyContainer.hide();
        }

        var dropzone = new Dropzone('.dropzoneNew', {
            url: '/attachments/create',
            params: { issueId: this.issueId.val() },
            headers: {'RequestVerificationToken': this.requestVerificationToken.val() },
            thumbnailHeight: 125,
            thumbnailWidth: 156,
            previewTemplate: previewTemplate,
            previewsContainer: '.attachment-thumbnail-container',
            init: (() => {
                $.get('/attachments/getattachments', { issueId: this.issueId.val(), })
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
            // $('.issue-detail-body').addClass('issue-detail-body-attaching');
        // });
        // dropzone.on('dragleave', () => {
        //     $('.issue-detail-body').removeClass('issue-detail-body-attaching');
        // })
    }
}

