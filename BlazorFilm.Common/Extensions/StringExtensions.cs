namespace BlazorFilm.Common.Extensions;

public static class StringExtensions
{
    public static string Truncate(this string value, int maxLength)
    {
        if (value == null) return string.Empty;
        if(value.Length <= maxLength) return value;
        var result =  value.Substring(0, maxLength);
        return $"{result} ...";
    }
}
