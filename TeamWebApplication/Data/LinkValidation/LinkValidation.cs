using System.Net;
using System.Text.RegularExpressions;
using TeamWebApplication.Data.ExceptionLogger;

namespace TeamWebApplication.Models
{
    public class LinkValidation
    {
        private static IDataLogger _logger;
        public LinkValidation(IDataLogger logger)
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
                string removedPunctuation = "";//if after link there is punctuation, it must be removed for validation of url
                string urlWithoutPunctuationrl = RemovePunctuation(url, out removedPunctuation);

                if (Uri.TryCreate(urlWithoutPunctuationrl, UriKind.Absolute, out Uri uriResult))//validating URL structure
                {
                    using (HttpClient client = new HttpClient())//making HTTP request to URL to check whether URL exists or resource at that URL is accessible
                    {
                        try
                        {
                            HttpResponseMessage response = client.GetAsync(uriResult).Result;
                            if (response.StatusCode == HttpStatusCode.OK)//checking whether HTTP is responsive to that URL
                            {
                                return $"<a href=\"{uriResult}\">{uriResult}</a>" + $"{removedPunctuation}";
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
        public static string RemovePunctuation(string text, out string punctuation)
        {
            string punctuationPlaceholder = "";
            punctuation = string.Join("", Regex.Matches(text, @"[.,;](?!\S)").Cast<Match>().Select(match => match.Value));
            text = Regex.Replace(text, @"[.,;](?!\S)", punctuationPlaceholder);//deleting punctuation from link
            return text;
        }
    }
}