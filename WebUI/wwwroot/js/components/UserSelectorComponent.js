class UserSelectorComponent {
    constructor(container, options) {
        this.container = container;
        this.options = options;
        this.#Load();
    }

    #Load = () => {
        $.get('/components/getuserselectorcomponent', this.options).done((modal) => {
            this.container.html(modal);
            new Select2Component({ container: this.container.find('.select2') });
        });
    }
}