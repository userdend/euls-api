## /api/html

- This endpoint requests HTML content (via web scraping) from the official campus website.
- It filters all `<option>` tags, extracting their `value` properties (containing specific subject URLs in XML format) and their inner text (subject title).
- The extracted values are mapped into the "Subject" model and appended to the list of subjects (`List<Subject>`) before being sent as a response from this endpoint.

## /api/pdf

- The purpose of this endpoint is to generate timetables for selected schedules by the user.
- It receives a list of selected subjects, each with its own unique URL in XML format.
- The endpoint then requests XML content from each URL.
- The received XML content, in string form, is converted to nodes using the .NET library.
- These nodes are filtered to extract valuable information such as time, places, lecturer, etc.
- The filtered information is mapped into the "Timetable" model and appended to the list of timetables (`List<Timetable>`) before being sent as a response from this endpoint.