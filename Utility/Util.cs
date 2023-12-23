using EULS.Model;
using HtmlAgilityPack;

namespace EULS.Utility
{
    public class Util
    {
        readonly string[] urls = { "https://bpa.ums.edu.my/kuliah/mindex.html", "https://jadual.ums.edu.my/KuliahKKNextSem/mindex.html" };
        readonly List<Subject> subjects = new();

        public async Task<List<Subject>> GetSubjects() {
            return await FilterContent();
        }

        public async Task<List<Subject>> FilterContent() {
            // Read url one at a time.
            foreach (string url in urls)
            {
                try
                {
                    // Retrieve the HTML content of the url.
                    string html = await GetHTMLContent(url);

                    // Convert the HTML content from string to HTML document.
                    HtmlDocument htmlDocument = new();
                    htmlDocument.LoadHtml(html);

                    // Obtain all the <option> under <select>.
                    var optionNodes = htmlDocument.DocumentNode.SelectNodes("//select//option");

                    // Check if the <option> do exist.
                    if (optionNodes != null && optionNodes.Any()) {

                        // For each <option>.
                        foreach (var node in optionNodes) {

                            // Create a new Subject object, and assign its properties with the <option> value and text.
                            subjects.Add(new Subject {
                                Title = node.InnerText,
                                Path = node.GetAttributeValue("value", ""),
                                Base = url,
                            }); 
                        }
                    }
                }
                catch (Exception)
                {
                    // If HTML content retrieval failed.
                }
            }

            // Remove the first object from the object list.
            if (subjects.Count > 0) {
                subjects.RemoveAt(0);
            }

            return subjects;
        }

        public async Task<string> GetHTMLContent(string url)
        {
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
