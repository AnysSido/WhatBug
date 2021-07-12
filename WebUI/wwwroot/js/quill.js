// Set defaults and event handlers for Quill editor instances
$(function () {
    var quillEditor = '.quill-editor'; // Div to hold the Quill editor
    var quillForm = $('.quill-form'); // Form containing quillEditor
    var quillContent = $('.quill-content'); // Form field to hold editor contents on submit

    var quill = new Quill(quillEditor, {
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
    });

    if (quillContent.val() != '') {
        quill.setContents(JSON.parse(quillContent.val()));
    }

    quillForm.on('submit', function () {
        var text = JSON.stringify(quill.getContents());
        quillContent.val(text);
    });
});