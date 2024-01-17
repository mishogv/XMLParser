import React from 'react';
import './App.css';
import FileUploader from './components/FileUploader';

const App = () => {
  return (
    <div className="App">
      <FileUploader apiUrl='http://localhost:5255/api/XmlConvertor/ConvertXmlFile' />
    </div>
  );
}

export default App;
