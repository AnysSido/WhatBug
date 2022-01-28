import ShowCreateIssueComponent from "../Components/create-issue-component"

$(document).ready(function(){   
    $('#createIssueModal').click(function() {
        ShowCreateIssueComponent();
    })
});