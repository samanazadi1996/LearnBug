using MD.PersianDateTime;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;


public static class Utilty
{

    public static string ToPersianDate(this DateTime dt, string format = "hh:mm  yyyy/MM/dd")
    {
        return new PersianDateTime(dt).ToString(format);
    }
    public static DateTime ToMiladiDate(this string dt)
    {
        return PersianDateTime.Parse(dt).ToDateTime();
    }
    public static string ToPrice(this double dec)
    {
        return dec.ToString("#,##0");
    }
    public static string Encrypt(this string str)
    {
        byte[] encData_byte = new byte[str.Length];
        encData_byte = System.Text.Encoding.UTF8.GetBytes(str);
        return Convert.ToBase64String(encData_byte);
    }
    public static string Decrypt(this string str)
    {
        System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
        System.Text.Decoder utf8Decode = encoder.GetDecoder();
        byte[] todecode_byte = Convert.FromBase64String(str);
        int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
        char[] decoded_char = new char[charCount];
        utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
        return new string(decoded_char);
    }
    public static string GenderToText(this string g)
    {
        string a = "نا مشخص";
        switch (g)
        {
            case "1":
                a = "مرد";
                break;
            case "2":
                a = "زن";
                break;
        }
        return a;

    }

}
