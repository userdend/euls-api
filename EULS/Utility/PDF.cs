using EULS.Model;
using HtmlAgilityPack;

namespace EULS.Utility
{
    public class PDF
    {
        readonly List<Timetable> timetables = new();

        public async Task<List<Timetable>> GeneratePDF(string[] pdfs) {
            // For each subject source path in the list.
            foreach (string url in pdfs) {
                try {
                    // Retrieve the HTML content of the subject.
                    string html = await GetHTMLContent(url);

                    // Convert the HTML content from string to document.
                    HtmlDocument htmlDocument = new();
                    htmlDocument.LoadHtml(html);

                    // Get the event count.
                    var events = htmlDocument.DocumentNode.SelectNodes("//event").Count;

                    for (var index = 0; index < events; index++) {
                        // Obtain all useful information.
                        var subject = htmlDocument.DocumentNode.SelectNodes("//event//resources//module")[index].InnerText;
                        var lecturer = htmlDocument.DocumentNode.SelectNodes("//event//resources//staff")[index].InnerText;
                        var venue = htmlDocument.DocumentNode.SelectNodes("//event//resources//room")[index].InnerText;
                        var day = htmlDocument.DocumentNode.SelectNodes("//event//day")[index].InnerText;
                        var start = htmlDocument.DocumentNode.SelectNodes("//event//starttime")[index].InnerText;
                        var end = htmlDocument.DocumentNode.SelectNodes("//event//endtime")[index].InnerText;

                        // Create new Timetable object, and assign each properties with the corresponding information.
                        timetables.Add(new Timetable
                        {
                            Subject = subject,
                            Lecturer = lecturer,
                            Venue = venue,
                            Day = day,
                            Start = start,
                            End = end
                        });
                    }
                }
                catch (Exception) {
                    // If the HTML content retrieval failed.
                }
            }

            return timetables;
        }

        public async Task<string> GetHTMLContent(string url) {
            using HttpClient client = new();

            HttpResponseMessage response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                throw new HttpRequestException($"Failed to retrieve HTML. Status code: {response.StatusCode}");
            }
        }
    }
}
