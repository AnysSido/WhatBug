class IssuePrioritySelectComponent {
    constructor(container, projectId) {
        this.container = container;
        this.Load(projectId);
    }

    Load = (projectId) => {       
       $.get('/components/getissuepriorityselectcomponent', { projectId: projectId }).done((modal) => {
            this.container.html(modal);
            new Select2Component({
                container: this.container.find('.select2'),
                template: 'IconAndText'
            });
        });
    }
}