namespace HOI4Launcher.Utility;

public static class ScriptExtensions
{
    /// <summary>
    /// Formates name of category to more presetable form.
    /// For example: unit_pack to Unit pack 
    /// </summary>
    /// <param name="name">Name of category</param>
    /// <returns></returns>
    public static string FormatCategory(this string name)
    {
        name = name.Replace('_', ' ');
        return name[0].ToString().ToUpper() + name[1..];
    }

    /// <summary>
    /// Extracts value by given key in Paradox file
    /// </summary>
    /// <param name="text">Paradox file content</param>
    /// <param name="key">Key for extracting value</param>
    /// <returns></returns>
    public static string ParseValue(this string text, string key)
    {
        var startPosition = text.IndexOf('\n'+key);
        if(startPosition == -1)
            startPosition = 0;
        var startQuote = text.IndexOf('"', startPosition)+1;
        var endQuote = text.IndexOf('"', startQuote);
        var value = text[startQuote..endQuote];
        return value;
    }


}