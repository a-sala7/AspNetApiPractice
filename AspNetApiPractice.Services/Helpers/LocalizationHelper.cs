﻿using AspNetApiPractice.Models.Interfaces;
using System.Reflection;
using System.Text.RegularExpressions;

namespace AspNetApiPractice.Services.Helpers;

public static class LocalizationHelper
{
    public static void LocalizeProperties(ILocalizable src, ref object target, string langCode)
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
    private static string GetUnlocalizedPropName(string s)
    {
        return s.Substring(0, s.Length - 3);
    }
}