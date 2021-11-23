using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for PageCount
/// </summary>
public static class PageCount
{
    private static int Count;
    public static void SetCount(int n)
    {
        Count = n;
    }
    public static int GetCount()
    {
        return Count;
    }
}