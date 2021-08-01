class Select2Component {
    constructor(options) {
        this.container = options.container;
        this.template = options.template; // Provides both listTemplate and selectedTemplate
        this.listTemplate = options.listTemplate;        
        this.selectedTemplate = options.selectedTemplate;

        this.container.select2({
            width: '100%',
            theme: 'bootstrap4',
            templateSelection: this.#GetTemplate(this.selectedTemplate ?? this.template),
            templateResult: this.#GetTemplate(this.listTemplate ?? this.template)
        });
    }

    #GetTemplate(name) {
        if (name == 'IconAndText') {
            return this.#IconAndTextTemplate;
        } else {
            return this.#TextTemplate;
        }
    }

    #TextTemplate(item) {
        return item.text;
    }

    #IconAndTextTemplate(item) {
        if (!item.id) {
            return item.text;
        }
        return $('<span><i class="' + item.element.dataset.class +'"></i>' + item.text + '</span>');
    }
}