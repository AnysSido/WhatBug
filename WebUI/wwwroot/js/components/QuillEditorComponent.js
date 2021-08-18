class QuillEditorComponent {
    constructor(options) {
        this.container = options.container;
        this.copyContentsTo = options.copyContentsTo;
        this.readOnlyEditor = options.readOnlyEditor;
        this.editor = new Quill(this.container[0], this.#GetEditorOptions() );
        this.toolbar = this.container.prev();

        if (this.readOnlyEditor) {
            this.MakeReadOnly();
        }

        // Fix to stop LastPass browser plugin throwing errors when using Quill editor.
        this.container.on('keydown', (e) => {
            e.stopPropagation();
        });

        if (this.copyContentsTo) {
            this.container.focusout(() => {
                this.copyContentsTo.val(this.ToJson());
            });
        }

        if (this.copyContentsTo && this.copyContentsTo.val() != '') {
            this.editor.setContents(JSON.parse(this.copyContentsTo.val()));
        }
    }

    ToggleReadOnly = () => {
        this.readOnlyEditor ? this.MakeEditable() : this.MakeReadOnly();
    };

    MakeReadOnly = () => {
        this.readOnlyEditor = true;
        this.editor.disable();
        this.toolbar.addClass('ql-toolbar-readonly');
        this.container.addClass('ql-container-readonly');
    };

    MakeEditable = () => {
        this.readOnlyEditor = false;
        this.editor.enable();
        this.toolbar.removeClass('ql-toolbar-readonly');
        this.container.removeClass('ql-container-readonly');
    };

    IsEmpty = () => {
        return this.editor.getLength() == 1;
    }

    ToJson = () => {
        return JSON.stringify(this.editor.getContents());
    }

    #GetEditorOptions() {
        return {
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
        }
    }
}