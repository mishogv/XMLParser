import React, { useState, useCallback } from 'react';
import axios, { AxiosResponse, AxiosError } from 'axios';
import { useDropzone } from 'react-dropzone';

interface FileUploaderProps {
  apiUrl: string;
}

const dropzoneStyle: React.CSSProperties = {
  border: '2px dashed #cccccc',
  borderRadius: '4px',
  padding: '20px',
  textAlign: 'center',
  cursor: 'pointer',
};

const FileUploader: React.FC<FileUploaderProps> = ({ apiUrl }) => {
  const [uploadedData, setUploadedData] = useState<string | null>(null);
  const [error, setError] = useState<string | null>(null);
  const [validationErrors, setValidationErrors] = useState<string[]>([]);
  const [successMessage, setSuccessMessage] = useState<string | null>(null);

  const onDrop = useCallback(async (acceptedFiles: File[]) => {
    try {
      const file = acceptedFiles[0];

      const formData = new FormData();
      formData.append('File', file);
      formData.append('Name', file.name);
      
      await axios.post(apiUrl, formData, {
        headers: {
          'Content-Type': 'multipart/form-data',
        },
      });

      handleSuccess();
    } catch (error: AxiosError | any) {
      handleFailure(error);
    }
  }, [apiUrl]);

  const handleSuccess = () => {
    setUploadedData(null);
    setError(null);
    setValidationErrors([]);
    setSuccessMessage('File uploaded successfully!');
  };

  const handleFailure = (error: AxiosError<any>) => {
    setSuccessMessage(null);

    if (error.response) {
      const responseData = error.response.data;

      if (responseData.status === 400 && responseData.errors) {
        const errors = Object.values(responseData.errors).flat();
        setValidationErrors(errors as string[]);
      } else {
        setError(`Server Error: ${responseData.detail}`);
      }
    } else if (error.request) {
      setError('No response received from the server');
    } else {
      setError(`Error: ${error.message}`);
    }
  };

  const { getRootProps, getInputProps, isDragActive } = useDropzone({ onDrop });

  return (
    <div>
      <div {...getRootProps()} style={dropzoneStyle}>
        <input {...getInputProps()} />
        {isDragActive ? (
          <p>Drop the file here ...</p>
        ) : (
          <p>Drag 'n' drop an XML file here, or click to select one</p>
        )}
      </div>
      {successMessage && <p style={{ color: 'green' }}>{successMessage}</p>}
      {validationErrors.length > 0 && (
        <div style={{ color: 'red' }}>
          <p>Validation Errors:</p>
          <ul>
            {validationErrors.map((error, index) => (
              <li key={index}>{error}</li>
            ))}
          </ul>
        </div>
      )}
      {error && <p style={{ color: 'red' }}>{error}</p>}
      {uploadedData && <p>Uploaded Data: {uploadedData}</p>}
    </div>
  );
};

export default FileUploader;