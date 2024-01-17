# XML to JSON Converter API

## Overview

This application allows converting XML files to JSON format. It exposes an API endpoint to upload XML files and convert them to JSON. 

## Prerequisites

- The application needs to be deployed and running on a server.
- Users need the URL for the API endpoint.

## Using the Application

1. User prepares an XML file to convert to JSON.

2. User makes a POST request to the `/api/XmlConvertor/ConvertXmlFile` endpoint. 

3. The request body is multipart/form-data containing:

   - `Name` - The file name. Should end with `.xml`.

   - `File` - The XML file to convert.
   
4. The application receives the request, validates the input.

5. The XML file gets converted to JSON using the internal services.

6. The JSON output gets written to a file on server with the same name and '.json' extension. 

7. API returns 200 OK response if successful.