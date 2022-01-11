$(function() {
    BuildIssueStatusChart();
    BuildIssueTypesChart();
    BuildIssuePriorityChart();

    function BuildIssuePriorityChart()
    {
        let priorityNames = issuePriorities.map(p => p.name);
        let priorityValues = issuePriorities.map(p => p.issueCount);
        let priorityColors = $(":input[name^='priority-']").map((x,y) => $(y).css('color'));
    
        new Chart($('#issuePriorityChart'), {
            type: 'pie',
            data: {
                labels: priorityNames,
                datasets: [{
                    data: priorityValues,
                    backgroundColor: Object.values(priorityColors),
                    hoverOffset: 4
                }]
            }
        });
    }

    function BuildIssueTypesChart()
    {
        let issueTypeNames = issueTypes.map(p => p.name);
        let issueTypeValues = issueTypes.map(p => p.issueCount);
        let issueTypeColors = $(":input[name^='issueType-']").map((x,y) => $(y).css('color'));

        new Chart($('#issueTypesChart'), {
            type: 'doughnut',
            data: {
                labels: issueTypeNames,
                datasets: [{
                  data: issueTypeValues,
                  backgroundColor: Object.values(issueTypeColors),
                  hoverOffset: 4
                }]
            }
        });
    }

    function BuildIssueStatusChart()
    {
        let statusNames = issueStatuses.map(p => p.name);
        let statusValues = issueStatuses.map(p => p.issueCount);
        let statusColors = { red: '#dc3545', yellow: '#ffc107', blue: '#007bff', green: '#28a745' }

        new Chart($('#issueStatusChart'), {
            type: 'bar',
            data: {
                labels: statusNames,
                datasets: [{
                    data: statusValues,
                    backgroundColor: Object.values(statusColors)
                }]
            },
            options: {
                maintainAspectRatio : false,
                responsive : true,
                scales: {
                    y: {
                      beginAtZero: true
                    }
                },
                plugins: {
                    legend: {
                      display: false
                    }
                }
            }
        });
    }
});