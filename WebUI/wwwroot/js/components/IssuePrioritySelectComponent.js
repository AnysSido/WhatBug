class IssuePrioritySelectComponent {
    constructor(container, options) {
        this.container = container;
        this.prefix = options.prefix;
    }

    Load = (projectId, options) => {       
       $.get('/components/getissuepriorityselectcomponent', {
           prefix: this.prefix,
           projectId: projectId
       }).done((modal) => {
            this.container.html(modal);

            var select = this.container.find('.select2');
            var selectedValue = this.container.data('selectedvalue');
            var ignoreSelectedValue = options && options.ignoreSelectedValue;

            if (!ignoreSelectedValue && selectedValue) {
                select.val(selectedValue);
            }

            new Select2Component({
                container: select,
                template: 'IconAndText'
            });
        });

        return this;
    }
}