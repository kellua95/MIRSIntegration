import React, { useState } from 'react';
import axios from 'axios';

const CreateIssue = ({ onIssueCreated }) => {
    const [title, setTitle] = useState('');
    const [description, setDescription] = useState('');

    const handleSubmit = (event) => {
        event.preventDefault();
        axios.post('/api/issues', { title, description })
            .then(response => {
                onIssueCreated(response.data);
                setTitle('');
                setDescription('');
            })
            .catch(error => {
                console.error('There was an error creating the issue!', error);
            });
    };

    return (
        <form onSubmit={handleSubmit}>
            <h3>Create a new issue</h3>
            <div>
                <label>Title</label>
                <input type="text" value={title} onChange={(e) => setTitle(e.target.value)} />
            </div>
            <div>
                <label>Description</label>
                <textarea value={description} onChange={(e) => setDescription(e.target.value)} />
            </div>
            <button type="submit">Create</button>
        </form>
    );
};

export default CreateIssue;
