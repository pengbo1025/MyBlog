using System;
using System.Collections.Generic;
using System.Web;
using System.Collections;
using System.Text.RegularExpressions;
using System.Data;
using System.Diagnostics;
using Common;
using System.Reflection;
using System.Web.Caching;

public class _Common
{

    /// <summary>
    /// 测试模式总开关 可用假数据
    /// </summary>
    public const bool isTestMode = true;
    /// <summary>
    /// debug模式 用来控制log输出开关
    /// </summary>
    public const bool isDebug = true;

    /// <summary>
    /// 中文数字转阿拉伯数字
    /// </summary>
    /// <param name="txt"></param>
    /// <returns></returns>
    public static string ConvertNum(string str)
    {
        string strChnNames = "一二三四五六七八九";
        string strNumNames = "123456789";

        char[] chars = str.ToCharArray();

        string returnVal = string.Empty;
        foreach (char item in chars)
        {
            int index = strChnNames.IndexOf(item);
            if (index != -1)
            {
                returnVal += strNumNames[index].ToString();
            }
            else
                returnVal += item;
        }

        return returnVal;

    }

    /// <summary>
    /// int转型 ,失败返回0
    /// </summary>
    /// <param name="intStr"></param>
    /// <returns></returns>
    public static int SafeInt(string intStr)
    {
        int result = 0;
        int.TryParse(intStr, out result);
        return result;
    }

    public static decimal SafeDecimal(string decimalStr)
    {
        decimal result = 0;
        decimal.TryParse(decimalStr, out result);
        return result;
    }

    public static DateTime SafeDatetime(string dt, bool flag)
    {
        DateTime result = new DateTime();

        if (flag)
        {
            if (DateTime.TryParse(dt, out result))
            {
                result = Convert.ToDateTime(result.Year + "-" + result.Month + "-" + result.Day + " 23:59:59");
            }
            else
            {
                result = Convert.ToDateTime("2099-12-29 23:59:59");
            }
        }
        else
        {
            if (DateTime.TryParse(dt, out result))
            {
                result = Convert.ToDateTime(result.Year + "-" + result.Month + "-" + result.Day + " 00:00:00");
            }
            else
            {
                result = Convert.ToDateTime("1990-12-29 00:00:00");
            }
        }

        return result;
    }


    /// <summary>
    /// 删除cookie
    /// </summary>
    /// <param name="name">名称</param>
    public static void DelCookie(string name)
    {
        string usercartCookie = GetCookie("USER_CART");
        if (usercartCookie != string.Empty)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[name];
            cookie.Expires = DateTime.Now.AddHours(-48);
            HttpContext.Current.Response.Cookies.Add(cookie);
        }
    }



    /// <summary>
    /// 设置Cookie(加密)Encrypt
    /// </summary>
    /// <param name="name">名称</param>
    /// <param name="min">过期（分）</param>
    /// <param name="val">值</param>
    public static void SetCookie(string name, double min, string val)
    {
        val = new Utils().Encrypt(val);

        HttpCookie cookie = new HttpCookie(name);
        //cookie.Path = "/";
        //cookie.Domain = "/";
        cookie.Expires = DateTime.Now.AddMinutes(min);
        cookie.Value = val;
        HttpContext.Current.Response.Cookies.Add(cookie);
    }

    /// <summary>
    /// 设置Cookie
    /// </summary>
    /// <param name="name">名称</param>
    /// <param name="min">过期（分）</param>
    /// <param name="val">值</param>
    public static void SetCookie(string name, double min, string val, bool flag)
    {
        val = HttpUtility.UrlEncode(val);

        HttpCookie cookiedel = HttpContext.Current.Request.Cookies[name];
        if (cookiedel != null)
        {
            cookiedel.Expires = DateTime.Now.AddHours(-48);
            HttpContext.Current.Response.Cookies.Add(cookiedel);
        }

        if (flag)
        {
            val = new Utils().Encrypt(val);
        }

        HttpCookie cookie = new HttpCookie(name);
        //cookie.Path = "/";
        //cookie.Domain = "/";
        cookie.Expires = DateTime.Now.AddMinutes(min);
        cookie.Value = val;
        HttpContext.Current.Response.Cookies.Add(cookie);
    }


    /// <summary>
    /// 获取Cookie(解密)Decrypt
    /// </summary>
    /// <param name="name">名称</param>
    /// <returns>值</returns>
    public static string GetCookie(string name)
    {
        if (HttpContext.Current.Request.Cookies[name] != null)
        {
            string val = HttpContext.Current.Request.Cookies[name].Value;
            if (val == string.Empty)
            {
                return string.Empty;
            }
            val = new Utils().Decrypt(val);
            return val;
        }

        return string.Empty;
    }

    /// <summary>
    /// 获取Cookie
    /// </summary>
    /// <param name="name">名称</param>
    /// <returns>值</returns>
    public static string GetCookie(string name, bool flag)
    {
        if (HttpContext.Current.Request.Cookies[name] != null)
        {
            string val = HttpContext.Current.Request.Cookies[name].Value;
            if (flag)
            {
                if (val != string.Empty)
                {
                    val = new Utils().Decrypt(val);
                }
                else
                {
                    return string.Empty;
                }
            }
            val = HttpUtility.UrlDecode(val);

            return val;
        }
        return string.Empty;
    }

    /// <summary>
    /// 设置session
    /// </summary>
    /// <param name="key"></param>
    /// <param name="val"></param>
    public static void SetSession(string key, object val)
    {
        HttpContext.Current.Session[key] = val;
    }

    /// <summary>
    /// 获取session
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public static object GetSession(string key)
    {
        if (HttpContext.Current.Session == null || HttpContext.Current.Session[key] == null)
        {
            return null;
        }
        else
        {
            return HttpContext.Current.Session[key];
        }
    }

    public static object GetCar(string key)
    {
        if (HttpContext.Current.Session == null || HttpContext.Current.Session[key] == null)
        {
            return null;
        }
        else
        {
            return HttpUtility.UrlDecode(HttpContext.Current.Session[key].ToString());
        }
    }

    ///   <summary>   
    ///    去除HTML标记   
    ///   </summary>   
    ///   <param    name="NoHTML">包括HTML的源码   </param>   
    ///   <returns>已经去除后的文字</returns>   
    public static string NoHTML(string Htmlstring)
    {
        //删除脚本   
        Htmlstring = Regex.Replace(Htmlstring, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
        //删除HTML   
        Htmlstring = Regex.Replace(Htmlstring, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"-->", "", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"<!--.*", "", RegexOptions.IgnoreCase);

        Htmlstring = Regex.Replace(Htmlstring, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"&(nbsp|#160);", "   ", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"&#(\d+);", "", RegexOptions.IgnoreCase);

        Htmlstring.Replace("<", "");
        Htmlstring.Replace(">", "");
        Htmlstring.Replace("\r\n", "");
        Htmlstring.Replace("", "");
        Htmlstring = HttpContext.Current.Server.HtmlEncode(Htmlstring).Trim();

        return Htmlstring;
    }

    /// <summary>
    /// 设置缓存
    /// </summary>
    /// <param name="cacheName">缓存名</param>
    /// <param name="cacheContext">缓存内容</param>
    /// <param name="hours">缓存小时</param>
    public static void SetCache(string cacheName, object cacheContext, double mills, bool flag)
    {
        //改成Cookie
        //cacheContext = HttpUtility.UrlEncode(cacheContext.ToString());
        //HttpCookie cookiedel = HttpContext.Current.Request.Cookies[cacheName];
        //if (cookiedel != null)
        //{
        //    cookiedel.Expires = DateTime.Now.AddHours(-48);
        //    HttpContext.Current.Response.Cookies.Add(cookiedel);
        //}

        //HttpCookie cookie = new HttpCookie(cacheName);
        ////cookie.Path = "/";
        ////cookie.Domain = "/";
        //cookie.Expires = DateTime.Now.AddMinutes(mills);
        //cookie.Value = cacheContext.ToString();
        //HttpContext.Current.Response.Cookies.Add(cookie);

        if (HttpRuntime.Cache[cacheName] == null)
        {
            HttpRuntime.Cache.Add(cacheName, cacheContext, null, DateTime.Now.AddMinutes(mills), TimeSpan.Zero, CacheItemPriority.NotRemovable, null);
        }
    }

    /// <summary>
    /// 设置缓存
    /// </summary>
    /// <param name="cacheName">缓存名</param>
    /// <param name="cacheContext">缓存内容</param>
    /// <param name="hours">缓存小时</param>
    public static void SetCache(string cacheName, object cacheContext, int hours)
    {
        HttpRuntime.Cache.Insert(cacheName, cacheContext, null, DateTime.Now.AddHours(hours), TimeSpan.Zero);
    }
    /// <summary>
    /// 设置缓存
    /// </summary>
    /// <param name="cacheName">缓存名</param>
    /// <param name="cacheContext">缓存内容</param>
    /// <param name="hours">缓存分钟</param>
    public static void SetCache(string cacheName, object cacheContext, double min)
    {
        HttpRuntime.Cache.Insert(cacheName, cacheContext, null, DateTime.Now.AddMinutes(min), TimeSpan.Zero);
    }

    /// <summary>
    /// 获取缓存
    /// </summary>
    /// <param name="cacheName">缓存名</param>
    /// <returns></returns>
    public static object GetCache(string name)
    {
        //改成Cookie

        //if (HttpContext.Current.Request.Cookies[name] != null)
        //{
        //    return HttpContext.Current.Request.Cookies[name].Value;
        //}
        //else
        //{
        //    return null;
        //}
        return HttpRuntime.Cache[name];
    }

    /// <summary>
    /// dump出数据结果的方法
    /// </summary>
    /// <param name="data"></param>
    public static void dumpDataSet(DataSet data)
    {
        foreach (DataTable table in data.Tables)
        {
            Dumper.DumpDataTableToDebug(table);
        }
    }

    public static void dump(string dump)
    {
        Dumper.dumpToDebug(dump);
    }


    public static DataTable ToDataTable<T>(List<T> items)
    {
        var tb = new DataTable(typeof(T).Name);

        PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

        foreach (PropertyInfo prop in props)
        {
            Type t = GetCoreType(prop.PropertyType);
            tb.Columns.Add(prop.Name, t);
        }

        foreach (T item in items)
        {
            var values = new object[props.Length];

            for (int i = 0; i < props.Length; i++)
            {
                values[i] = props[i].GetValue(item, null);
            }

            tb.Rows.Add(values);
        }

        return tb;
    }
    
    public static bool IsNullable(Type t)
    {
        return !t.IsValueType || (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>));
    }

    public static Type GetCoreType(Type t)
    {
        if (t != null && IsNullable(t))
        {
            if (!t.IsValueType)
            {
                return t;
            }
            else
            {
                return Nullable.GetUnderlyingType(t);
            }
        }
        else
        {
            return t;
        }
    }

    public static IList<T> ConvertTo<T>(DataTable table)
    {
        if (table == null)
        {
            return null;
        }

        List<DataRow> rows = new List<DataRow>();

        foreach (DataRow row in table.Rows)
        {
            rows.Add(row);
        }

        return ConvertTo<T>(rows);
    }

    public static IList<T> ConvertTo<T>(IList<DataRow> rows)
    {
        IList<T> list = null;

        if (rows != null)
        {
            list = new List<T>();

            foreach (DataRow row in rows)
            {
                T item = CreateItem<T>(row);
                list.Add(item);
            }
        }

        return list;
    }

    public static T CreateItem<T>(DataRow row)
    {
        T obj = default(T);
        if (row != null)
        {
            obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in row.Table.Columns)
            {
                PropertyInfo prop = obj.GetType().GetProperty(column.ColumnName);
                try
                {
                    object value = row[column.ColumnName];
                    prop.SetValue(obj, value, null);
                }
                catch
                {  //You can log something here     
                    //throw;    
                }
            }
        }

        return obj;
    }

    #region 随机生成Str
    public static string GetRandomStr(int len)
    {
        string randomChars = "BCDFGHJKMPQRTVWXY2346789";
        string password = string.Empty;
        int randomNum;
        Random random = new Random();
        for (int i = 0; i < len; i++)
        {
            randomNum = random.Next(randomChars.Length);
            password += randomChars[randomNum];
        }
        return password.ToLower();
    }
    #endregion
}
/// <summary>
/// DataTable转换为List&lt;Model&gt;
/// </summary>
public static class DataTableToListModel<T> where T : new()
{
    public static IList<T> ConvertToModel(DataTable dt)
    {
        //定义集合
        IList<T> ts = new List<T>();
        T t = new T();
        string tempName = "";
        //获取此模型的公共属性
        PropertyInfo[] propertys = t.GetType().GetProperties();
        foreach (DataRow row in dt.Rows)
        {
            t = new T();
            foreach (PropertyInfo pi in propertys)
            {
                tempName = pi.Name;
                //检查DataTable是否包含此列
                if (dt.Columns.Contains(tempName))
                {
                    //判断此属性是否有set
                    if (!pi.CanWrite)
                        continue;
                    object value = row[tempName];
                    if (value != DBNull.Value)
                        pi.SetValue(t, value, null);
                }
            }
            ts.Add(t);
        }
        return ts;
    }
}