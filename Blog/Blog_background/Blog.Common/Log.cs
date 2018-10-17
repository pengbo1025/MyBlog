using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

/// <summary>
/// 辅助
/// </summary>
public class Log
{
    /// <summary>
    /// 日志(错误)
    /// </summary>
    /// <param name="log"></param>
    public static void e(string pre, string log, params object[] args)
    {
        StreamWriter sw = File.AppendText(HttpContext.Current.Server.MapPath("~/log/" + pre + "_ERROR_" + DateTime.Now.ToString("yyyyMMdd HHmm") + ".log"));

        sw.WriteLine("--" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "--   ");
        sw.WriteLine(log);

        if (args != null)
        {
            foreach (object arg in args)
            {
                sw.Write(arg.ToString() + ",");
            }
        }
        sw.WriteLine(string.Empty);
        sw.Close();
        sw.Dispose();
    }

    /// <summary>
    /// 日志(信息)
    /// </summary>
    /// <param name="log"></param>
    public static void i(string pre, string log, params object[] args)
    {
        StreamWriter sw = File.AppendText(HttpContext.Current.Server.MapPath("~/log/" + pre + "_" + DateTime.Now.ToString("yyyyMMdd HHmm") + ".log"));

        sw.WriteLine("--" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "--   ");
        sw.WriteLine(log);
        
        if (args != null)
        {
            foreach (object arg in args)
            {
                sw.Write(arg.ToString() + ",");
            }
        }
        sw.WriteLine(string.Empty);
        sw.Close();
        sw.Dispose();
    }


    /// <summary>
    /// 日志(警告)
    /// </summary>
    /// <param name="log"></param>
    public static void w(string pre, string log, params object[] args)
    {
        StreamWriter sw = File.AppendText(HttpContext.Current.Server.MapPath("~/log/" + pre + "_WARNING_" + DateTime.Now.ToString("yyyyMMdd HHmm") + ".log"));

        sw.WriteLine("--" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "--   ");
        sw.WriteLine(log);

        if (args != null)
        {
            foreach (object arg in args)
            {
                sw.Write(arg.ToString() + ",");
            }
        }
        sw.WriteLine(string.Empty);
        sw.Close();
        sw.Dispose();
    }


    /// <summary>
    /// 输出页面
    /// </summary>
    /// <param name="log"></param>
    public static void WritePage(object obj)
    {
        HttpContext.Current.Response.ContentType = "text/plain";
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.Write(obj);
    }


    public static void name(string name)
    {
        StreamWriter sw = File.AppendText(HttpContext.Current.Server.MapPath("~/appname/" + DateTime.Now.ToString("yyyy-MM-dd") + ".log"));
        sw.WriteLine(Environment.NewLine);
        sw.Write(name);
        sw.Close();
        sw.Dispose();
    }

}