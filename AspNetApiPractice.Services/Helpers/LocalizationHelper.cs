using AspNetApiPractice.Helpers;
using AspNetApiPractice.Models.Interfaces;

namespace AspNetApiPractice.Services.Helpers;

public static class LocalizationHelper
{
    public static void LocalizeProperties(IEnumerable<ILocalizable> src,
        object[] target)
    {
        if (src is null || target is null)
            return;
        
        var langCode = HttpRequestHelper.GetHeaderValue("lang");
        
        if (string.IsNullOrEmpty(langCode))
            langCode = "en";

        for(int i = 0; i < src.Count(); i++)
        {
            LocalizeProperties(src.ElementAt(i), target[i], langCode);
        }
    }
    public static void LocalizeProperties(ILocalizable src, object target, string langCode)
    {
        if (src is null || target is null)
            return;
                
        if (string.IsNullOrEmpty(langCode))
            langCode = "en";

        var srcType = src.GetType();
        var targetType = target.GetType();
        var localizablePropsFromSource =
            srcType
            .GetProperties()
            .Where(p => p.Name.ToLower().EndsWith('_' + langCode));

        foreach(var prop in localizablePropsFromSource)
        {
            targetType?
                .GetProperty(GetUnlocalizedPropName(prop.Name))?
                .SetValue(target, prop.GetValue(src));
        }
    }

    public static void LocalizeProperties(ILocalizable src, object target)
    {
        string langCode = HttpRequestHelper.GetHeaderValue("lang");    
        if (string.IsNullOrEmpty(langCode))
            langCode = "en";

        if (src is null || target is null)
            return;

        var srcType = src.GetType();
        var targetType = target.GetType();
        var localizablePropsFromSource =
            srcType
            .GetProperties()
            .Where(p => p.Name.ToLower().EndsWith('_' + langCode));

        foreach(var prop in localizablePropsFromSource)
        {
            targetType?
                .GetProperty(GetUnlocalizedPropName(prop.Name))?
                .SetValue(target, prop.GetValue(src));
        }
    }
    private static string GetUnlocalizedPropName(string s)
    {
        return s.Substring(0, s.Length - 3);
    }
}