## How this work?
Each schedule is accessed via a unique URL in XML format, containing information such as time, places, etc.
The /html endpoint of the API requests the XML body content. Subsequently, the .NET library extracts values from the relevant XML nodes and maps them into the “Subject” model.
Finally, the API sends the list of subjects as a response.