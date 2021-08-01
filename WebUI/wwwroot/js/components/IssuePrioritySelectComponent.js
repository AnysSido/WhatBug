class IssuePrioritySelectComponent {
    constructor(container, projectId) {
        this.container = container;
        this.Load(projectId);
    }

    Load = (projectId) => {       
       $.get('/components/getissuepriorityselectcomponent', { projectId: projectId }).done((modal) => {
            this.container.html(modal);
            this.#StyleSelect();
        });
    }

    #StyleSelect = () => {
        function templating(iconElement) {
            if (!iconElement.id) {
                return iconElement.text;
            }
            return $('<span><i class="' + iconElement.element.dataset.class +'"></i>' + iconElement.text + '</span>');
        }
        
        this.container.find('.select2').select2({
            width: '100%',
            theme: 'bootstrap4',
            templateSelection: templating,
            templateResult: templating
        });
    }
}