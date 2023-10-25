using System.Net;
using System.Text.RegularExpressions;
using TeamWebApplication.Data.ExceptionLogger;

namespace TeamWebApplication.Models
{
    public class LinkValidation
    {
        private static IExceptionLogger _logger;
        public LinkValidation(IExceptionLogger logger)
        {
            _logger = logger;
        }
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
                        catch (HttpRequestException ex)
                        {
                            _logger.Log(ex);
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
