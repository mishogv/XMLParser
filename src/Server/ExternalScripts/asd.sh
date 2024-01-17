#!/bin/bash

# Specify the file direction to be loaded from
file_path="/home/mihail/repos/XMLParser/src/Server/ExternalScripts/test.xml"

# Function to send the curl request
send_request() {
  local index="$1"
  local new_name="file_${index}.xml"
  curl -X 'POST' \
    'http://localhost:5255/api/XMLConvertor/ConvertXMLFile' \
    -H 'accept: */*' \
    -H 'Content-Type: multipart/form-data' \
    -F "Name=${new_name}" \
    -F "File=@${file_path};type=text/xml"
}

# Set maximum concurrency
max_concurrency=100

# Iterate for 100 times using the same file
for ((index=1; index<=100; index++)); do
  # Run the function in the background
  send_request "$index" &
  
  # Control the concurrency
  current_jobs=$(jobs -p | wc -l)
  if [ "$current_jobs" -ge "$max_concurrency" ]; then
    # Wait for any background job to finish if the limit is reached
    wait -n
  fi
done

# Wait for all background jobs to finish
wait
