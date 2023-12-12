using System.Net;
using System.Text.RegularExpressions;
using TeamWebApplicationAPI.Data.ExceptionLogger;

namespace TeamWebApplicationAPI.Models
{
    public class LinkValidation
    {
        private static IExceptionLogger _logger;

        public static string ValidateAndReplaceLinks(string TextContent)
        {
            string pattern = @"https?://\S+";

            // Replacing URLs with clickable links
            string TextContentWithValidLinks = Regex.Replace(TextContent, pattern, match =>
            {
                string url = match.Value;
                string removedPunctuation = ""; // If there is punctuation after the link, it must be removed for URL validation
                string urlWithoutPunctuation = RemovePunctuation(url, out removedPunctuation);

                try
                {
                    if (Uri.TryCreate(urlWithoutPunctuation, UriKind.Absolute, out Uri uriResult)) // Validating URL structure
                    {
                        using (HttpClient client = new HttpClient())
                        {
                            try
                            {
                                HttpResponseMessage response = client.GetAsync(uriResult).Result;
                                if (response.StatusCode == HttpStatusCode.NotFound)
                                {
                                    return url;
                                }
                                else if (response.StatusCode == HttpStatusCode.OK) // Checking whether HTTP is responsive to that URL
                                {
                                    return $"<a href=\"{uriResult}\">{uriResult}</a>" + $"{removedPunctuation}";
                                }
                                return url;
                            }
                            catch (HttpRequestException ex)
                            {
                                if (ex.InnerException is WebException webException && webException.Status == WebExceptionStatus.NameResolutionFailure)
                                {
                                    _logger.Log(new Exception("No such host is known: " + uriResult.Host));
                                }
                                else
                                {
                                    _logger.Log(ex);
                                }
                                return url;
                            }
                        }
                    }
                    else
                    {
                        _logger.Log(new Exception($"Invalid URL format: {urlWithoutPunctuation}"));
                        return url;
                    }
                }
                catch (Exception ex)
                {
                    return url;
                }
            });

            return TextContentWithValidLinks;
        }
        public static string RemovePunctuation(string text, out string punctuation)
        {
            string punctuationPlaceholder = "";
            punctuation = string.Join("", Regex.Matches(text, @"[.,;?!](?!\S)").Cast<Match>().Select(match => match.Value));
            text = Regex.Replace(text, @"[.,;?!](?!\S)", punctuationPlaceholder); // Deleting punctuation from the link
            return text;
        }
    }
}