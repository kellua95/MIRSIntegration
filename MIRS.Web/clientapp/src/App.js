import React, { useState } from 'react';
import IssueList from './components/IssueList';
import CreateIssue from './components/CreateIssue';

function App() {
  const [issues, setIssues] = useState([]);

  const handleIssueCreated = (newIssue) => {
    setIssues([...issues, newIssue]);
  };

  return (
    <div className="App">
      <header className="App-header">
        <h1>Issue Tracker</h1>
      </header>
      <main>
        <CreateIssue onIssueCreated={handleIssueCreated} />
        <IssueList issues={issues} />
      </main>
    </div>
  );
}

export default App;
