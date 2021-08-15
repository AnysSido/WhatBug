class Select2Component {
    constructor(options) {
        this.container = options.container;

        $.extend(options, {
            width: '100%',
            theme: 'bootstrap4',
            templateSelection: this.Template,
            templateResult: this.Template
        });

        this.container.select2(options);
    }

    Template(item) {        
        if (!item.id || !item.element.dataset) {
            return item.text;
        }

        var data = item.element.dataset

        if (data.icon) {
            return $('<span><i class="' + data.icon + ' ' + data.iconColor +'"></i>' + item.text + '</span>');
        }

        return item.text;
    }
}