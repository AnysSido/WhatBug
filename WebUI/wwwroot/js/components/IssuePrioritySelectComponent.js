class IssuePrioritySelectComponent {
    constructor(parent, projectId) {
        this.myParent = parent;
        this.Refresh(projectId);
    }

    Refresh = (projectId) => {       
       $.get('/components/getissuepriorityselectcomponent', { projectId: projectId }).done((modal) => {
            this.myParent.html(modal);
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
        
        $('#CreateIssueModal .select2').select2({
            width: '100%',
            theme: 'bootstrap4',
            dropdownParent: $('#CreateIssueModal .modal-body'),
            templateSelection: templating,
            templateResult: templating
        });
    }
}