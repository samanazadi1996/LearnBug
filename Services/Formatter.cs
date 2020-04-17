using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class Formatter
{
    public static string formatter(string text)
    {
        text = string.Format(text, GetName(), GetGendertype());
        return text;
    }

    private static string GetGendertype()
    {
        return "آقای";
    }
    private static string GetName()
    {
        return "سامان آزادی";
    }
}