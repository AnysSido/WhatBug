class QuillEditorComponent {
    constructor(options) {
        this.container = options.container;
        this.copyContentsTo = options.copyContentsTo;
        this.readOnlyEditor = options.readOnlyEditor;

        this.editor = new Quill(this.container[0], this.readOnlyEditor ? this.#GetReadOnlyEditorOptions() : this.#GetEditorOptions() );

        // Fix to stop LastPass browser plugin throwing errors when using Quill editor.
        this.container.on('keydown', (e) => {
            e.stopPropagation();
        });

        if (this.copyContentsTo) {
            this.container.focusout(() => {
                this.copyContentsTo.val(this.ToJson());
            });
        }

        // TODO: Deal with this
        // if (quillContent.val() != '') {
        //     quill.editor.setContents(JSON.parse(quillContent.val()));
        // }
    }

    IsEmpty = () => {
        return this.editor.getLength() == 1;
    }

    ToJson = () => {
        return JSON.stringify(this.editor.getContents());
    }

    #GetReadOnlyEditorOptions() {
        return {
            theme: 'snow',
            readOnly: true,
            modules: {
                toolbar: false,
                syntax: true
            }
        }
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