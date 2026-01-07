$(document).ready(function () {
    const apiUrl = 'http://localhost:5033/api/issues';

    // Fetch and display issues on page load
    loadIssues();

    // Handle form submission for creating a new issue
    $('#create-issue-form').on('submit', function (e) {
        e.preventDefault();

        var issue = {
            title: $('#title').val(),
            description: $('#description').val()
        };

        $.ajax({
            url: apiUrl,
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(issue),
            success: function () {
                loadIssues();
                $('#create-issue-form')[0].reset();
            },
            error: function (error) {
                console.error('Error creating issue:', error);
            }
        });
    });

    function loadIssues() {
        $.ajax({
            url: apiUrl,
            type: 'GET',
            success: function (issues) {
                var tableBody = $('#issues-table-body');
                tableBody.empty();
                issues.forEach(function (issue) {
                    tableBody.append(
                        '<tr>' +
                        '<td>' + issue.title + '</td>' +
                        '<td>' + issue.description + '</td>' +
                        '<td>' + issue.status + '</td>' +
                        '</tr>'
                    );
                });
            },
            error: function (error) {
                console.error('Error loading issues:', error);
            }
        });
    }
});
