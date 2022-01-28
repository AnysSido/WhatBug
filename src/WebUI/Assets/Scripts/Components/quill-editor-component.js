export default class QuillEditorComponent {
    constructor(options) {

        $.get('/quilleditorcomponent/getcomponent').done((container) => {
            // Replace the given element with the Quill component
            this.container = $(container);
            options.container.replaceWith(this.container);

            this.copyContentsTo = options.copyContentsTo;
            this.isDynamic = options.isDynamic;

            // Init quill
            const editorContainer = this.container.find('.quill-editor').get(0);
            this.quill = new Quill(editorContainer, this.#GetEditorOptions());
            
            // Collect the quill elements
            this.quillToolbar = this.container.find('.ql-toolbar');
            this.quillContainer = this.container.find('.ql-container');
            this.quillEditor = this.container.find('.ql-editor');

            // Buttons for dynamic editor
            this.buttonsContainer = this.container.find('.quill-savecancel');
            this.saveButton = this.buttonsContainer.find('.quill-save');
            this.cancelButton = this.buttonsContainer.find('.quill-cancel');

            // The quill editor doesn't work with razor binding. We copy
            // the contents to an element that can bind.
            if (this.copyContentsTo) {
                this.container.focusout(() => {
                    this.copyContentsTo.val(this.ToJson());
                });

                // Load any existing content back into the editor
                if (this.copyContentsTo.val() != '') {
                    this.quill.setContents(JSON.parse(this.copyContentsTo.val()));
                }
            }

            // If this is a dynamic editor we begin as readonly and switch
            // to editable with save/cancel buttons when clicked.
            if (this.isDynamic) {
                this.#MakeDynamic();
            }

            // // Fix to stop LastPass browser plugin throwing errors when using Quill editor.
            this.container.on('keydown', (e) => {
                e.stopPropagation();
            });   
        });        
    }

    #MakeDynamic = () => {
        var previousContent = undefined;

        this.MakeReadOnly();

        this.quillEditor.on('click', () => {
            if (this.readOnlyEditor) {
                previousContent = this.ToJson();
                this.MakeEditable();
            }            
        });

        this.cancelButton.on('click', () => {
            this.quill.setContents(JSON.parse(previousContent));
            this.MakeReadOnly();            
        });

        this.saveButton.on('click', () => {
            $(this).trigger('save');
            this.MakeReadOnly();
        });
    };

    MakeReadOnly = () => {
        this.readOnlyEditor = true;
        this.quillContainer.addClass('editable');
        this.quill.disable();
        this.quillToolbar.addClass('ql-toolbar-readonly');
        this.quillContainer.addClass('ql-container-readonly');
        if (this.isDynamic) {
            this.buttonsContainer.addClass('d-none');
        }
    };

    MakeEditable = () => {
        this.readOnlyEditor = false;
        this.quillContainer.removeClass('editable');
        this.quill.enable();
        this.quillToolbar.removeClass('ql-toolbar-readonly');
        this.quillContainer.removeClass('ql-container-readonly');
        if (this.isDynamic) {
            this.buttonsContainer.removeClass('d-none');
        }
    };

    IsEmpty = () => {
        return this.quill.getLength() == 1;
    }

    ToJson = () => {
        return JSON.stringify(this.quill.getContents());
    }

    #GetEditorOptions() {
        return {
            theme: 'snow',
            modules: {
                syntax: false,
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

                    ['link'],

                    ['clean']
                ]
            }
        }
    }
}