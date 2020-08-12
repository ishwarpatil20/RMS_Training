//------------------------------------------------------------------------------
//
//  File:           URLHelper.cs       
//  Author:         Abhishek.Varma
//  Date written:   4/26/2010 03:30:00 PM
//  Description:    This class contain all method related to validate URL.
//
//  Amendments
//  Date                  Who                Ref     Description
//  ----                  -----------        ---     -----------
//  4/26/2010 03:30:00 PM  Abhishek.Varma    n/a     Created    
//
//------------------------------------------------------------------------------
using System;
using System.Text;
using Microsoft.Win32;
using System.Security.Cryptography;
using System.Collections.Generic;
using Common;

/// <summary>
/// Summary description for URLHelper
/// </summary>
public static class URLHelper
{

    #region Field Members

    private static byte[] key;

    private const string SECRETKEY = "C50B3C89CB21F4F1422FF158A5B42D0E8DB8CB5CDA1742572A487D9401E3400267682B202B746511891C1BAF47F8D25C07F6C39A104696DB51F17C529AD3CABE";

    #endregion Field Members


    #region Member Functions

    static URLHelper()
    {
        key = GetKeyAndConvertIntoByte();
    }

    /// <summary>
    /// Obscure the URL Content
    /// </summary>
    /// <param name="s">This parameter contain URL conent.</param>
    /// <returns>return obscured value to calling function.</returns>
    private static string Obscure(string s)
    {
        byte[] b = Encoding.UTF8.GetBytes(s);
        return URLHelper.ToSafe64(b);
    }

    /// <summary>
    /// Clarify the Obscure URL Content
    /// </summary>
    /// <param name="s">This parameter contain Obscure URL conent.</param>
    /// <returns>return clarifyed value to calling function.</returns>
    public static string Clarify(string s)
    {
        string returnValue = s;
        try
        {
            byte[] b = URLHelper.FromSafe64(s);
            return Encoding.UTF8.GetString(b);
        }
        catch (Exception)
        {
            // Subpressing exception here
        }

        return returnValue;
    }

    /// <summary>
    /// Perform encoding algorithm 
    /// </summary>
    /// <param name="b">This parameter contain's array of byte values.</param>
    /// <returns>return encoded value to calling function.</returns>
    private static string ToSafe64(byte[] b)
    {
        StringBuilder s2 = new StringBuilder(Convert.ToBase64String(b));
        s2.Replace('+', '*');
        s2.Replace('=', '~');
        s2.Replace('/', '|');
        return s2.ToString();
    }

    /// <summary>
    /// Perform decoding algorithm
    /// </summary>
    /// <param name="s">This parameter contain encoded value.</param>
    /// <returns>return array of byte values to calling function</returns>
    private static byte[] FromSafe64(string s)
    {
        try
        {
            StringBuilder s2 = new StringBuilder(s);
            s2.Replace('|', '/');
            s2.Replace('~', '=');
            s2.Replace('*', '+');
            return Convert.FromBase64String(s2.ToString());
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    /// <summary>
    /// Read hash key value for resgistry key fro registry 
    /// </summary>
    /// <returns></returns>
    private static byte[] GetKeyAndConvertIntoByte()
    {
        string secret = SECRETKEY;

        byte[] bytes = new byte[secret.Length / 2];

        for (int i = 0; i < bytes.Length; i++)
        {
            bytes[i] = Convert.ToByte(secret.Substring(i * 2, 2), 16);
        }

        return bytes;
    }

    /// <summary>
    /// This method create Signature to avoid URL tampering 
    /// </summary>
    /// <param name="parts">This parameter contain's required URL content for Create Signature.</param>
    /// <returns>return encoded signature to calling function</returns>
    private static string CreateSignature(string[] parts)
    {
        HMACSHA1 hmac = new HMACSHA1(key);
        string s = String.Concat(parts);
        byte[] b = Encoding.UTF8.GetBytes(s);
        byte[] sig = hmac.ComputeHash(b);
        return URLHelper.ToSafe64(sig);
    }

    /// <summary>
    /// This method validate Signature.
    /// </summary>
    /// <param name="signature">This parameter contain obscured value for URL signatue.</param>
    /// <param name="parts">This parameter contain's required URL content for Create Signature.</param>
    /// <returns>return bool value for validation status</returns>
    public static bool ValidateSignature(string signature, string[] parts)
    {
        string newSignature = CreateSignature(parts);
        return signature == Obscure(newSignature);
    }

    public static string SecureParameters(string queryStringName, string queryStringvalue)
    {
        string URLParameter = string.Empty;

        string[] parts = new string[1];

        URLParameter += queryStringName + "=" + URLHelper.Obscure(queryStringvalue) + "&";

        parts[0] = queryStringvalue;

        //string signature = URLHelper.CreateSignature(parts);

        //URLParameter += CommonConstants.SIGNATURE + "=" + URLHelper.Obscure(signature);

        if (URLParameter.EndsWith("&"))

            URLParameter = URLParameter.Substring(0, URLParameter.Length - 1);

        return URLParameter;
    }

    public static string CreateSignature(string parameter1)
    {
        string[] parts = new string[1];
        parts[0] = parameter1;
        return CommonConstants.SIGNATURE + "=" + Obscure(CreateSignature(parts));
    }

    public static string CreateSignature(string parameter1, string parameter2)
    {
        string[] parts = new string[2];
        parts[0] = parameter1;
        parts[1] = parameter2;
        return CommonConstants.SIGNATURE + "=" + Obscure(CreateSignature(parts));
    }

    public static string CreateSignature(string parameter1, string parameter2, string parameter3)
    {
        string[] parts = new string[3];
        parts[0] = parameter1;
        parts[1] = parameter2;
        parts[2] = parameter3;
        return CommonConstants.SIGNATURE + "=" + Obscure(CreateSignature(parts));
    }

    public static string CreateSignature(string parameter1, string parameter2, string parameter3, string parameter4)
    {
        string[] parts = new string[4];
        parts[0] = parameter1;
        parts[1] = parameter2;
        parts[2] = parameter3;
        parts[3] = parameter4;
        return CommonConstants.SIGNATURE + "=" + Obscure(CreateSignature(parts));
    }

    public static string CreateSignature(string parameter1, string parameter2, string parameter3, string parameter4, string parameter5)
    {
        string[] parts = new string[5];
        parts[0] = parameter1;
        parts[1] = parameter2;
        parts[2] = parameter3;
        parts[3] = parameter4;
        parts[4] = parameter5;
        return CommonConstants.SIGNATURE + "=" + Obscure(CreateSignature(parts));
    }

    public static string CreateSignature(string parameter1, string parameter2, string parameter3, string parameter4, string parameter5, string parameter6, string parameter7)
    {
        string[] parts = new string[7];
        parts[0] = parameter1;
        parts[1] = parameter2;
        parts[2] = parameter3;
        parts[3] = parameter4;
        parts[4] = parameter5;
        parts[5] = parameter6;
        parts[6] = parameter7;
        return CommonConstants.SIGNATURE + "=" + Obscure(CreateSignature(parts));
    }

    public static string CreateSignature(string parameter1, string parameter2, string parameter3, string parameter4, string parameter5, string parameter6, string parameter7, string parameter8)
    {
        string[] parts = new string[8];
        parts[0] = parameter1;
        parts[1] = parameter2;
        parts[2] = parameter3;
        parts[3] = parameter4;
        parts[4] = parameter5;
        parts[5] = parameter6;
        parts[6] = parameter7;
        parts[7] = parameter8;
        return CommonConstants.SIGNATURE + "=" + Obscure(CreateSignature(parts));
    }

    public static string CreateSignature(string parameter1, string parameter2, string parameter3, string parameter4, string parameter5, string parameter6, string parameter7, string parameter8, string parameter9)
    {
        string[] parts = new string[9];
        parts[0] = parameter1;
        parts[1] = parameter2;
        parts[2] = parameter3;
        parts[3] = parameter4;
        parts[4] = parameter5;
        parts[5] = parameter6;
        parts[6] = parameter7;
        parts[7] = parameter8;
        parts[8] = parameter9;
        return CommonConstants.SIGNATURE + "=" + Obscure(CreateSignature(parts));
    }

    #endregion Member Functions
}
