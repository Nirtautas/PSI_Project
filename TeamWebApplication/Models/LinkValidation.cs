using System.Net;
using System.Text.RegularExpressions;

namespace TeamWebApplication.Models
{
    public class LinkValidation
    {
        public static string ValidateAndReplaceLinks(string TextContent)
        {
            string pattern = @"https?://\S+";

            //replacing URLs with clickable links
            string TextContentWithValidLinks = Regex.Replace(TextContent, pattern, match =>
            {
                string url = match.Value;

                if (Uri.TryCreate(url, UriKind.Absolute, out Uri uriResult))//validating URL structure
                {
                    using (HttpClient client = new HttpClient())//making HTTP request to URL to check whether URL exists or resource at that URL is accessible
                    {
                        try
                        {
                            HttpResponseMessage response = client.GetAsync(uriResult).Result;
                            if (response.StatusCode == HttpStatusCode.OK)//checking whether HTTP is responsive to that URL
                            {
                                return $"<a href=\"{uriResult}\">{uriResult}</a>";
                            }
                            else
                            {
                                return url;//returning original URL if it's not valid
                            }
                        }
                        catch (HttpRequestException)
                        {
                            return url;
                        }
                    }
                }
                else
                {
                    return url; 
                }
            });
            return TextContentWithValidLinks;
        }
    }
}