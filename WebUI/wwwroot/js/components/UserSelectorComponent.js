class UserSelectorComponent {
    constructor(container, options) {
        this.container = container;
        this.options = options;
        this.#Load();
    }

    #Load = () => {
        $.get('/components/getuserselectorcomponent', this.options).done((modal) => {
            this.container.html(modal);
            this.container.find(".select2").select2({
                theme: 'bootstrap4',            
            });
        });
    }
}