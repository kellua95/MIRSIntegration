import React, { useState, useEffect } from 'react';
import axios from 'axios';

const IssueList = () => {
    const [issues, setIssues] = useState([]);

    useEffect(() => {
        axios.get('/api/issues')
            .then(response => {
                setIssues(response.data);
            })
            .catch(error => {
                console.error('There was an error fetching the issues!', error);
            });
    }, []);

    return (
        <div>
            <h2>Issues</h2>
            <ul>
                {issues.map(issue => (
                    <li key={issue.id}>{issue.title}</li>
                ))}
            </ul>
        </div>
    );
};

export default IssueList;
