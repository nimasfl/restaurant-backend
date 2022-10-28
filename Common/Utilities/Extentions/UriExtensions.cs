using System.Web;

namespace Restaurant.Common;

public static class UriExtensions
{
    public static Uri AddQuery(this Uri uri, Dictionary<string, string> queries)
    {
        var urlBuilder = new UriBuilder(uri);
        var query = HttpUtility.ParseQueryString(urlBuilder.Query);
        foreach (var keyValuePair in queries)
        {
            var key = keyValuePair.Key;
            var value = keyValuePair.Value;
            query.Add(key, value);
        }

        urlBuilder.Query = query.ToString() ?? "";
        return urlBuilder.Uri;
    }
}