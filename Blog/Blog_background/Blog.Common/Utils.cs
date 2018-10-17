using System;
using System.IO;
using System.Xml;
using System.Web;
using System.Text;
using System.Collections;
using System.Configuration;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using System.Net;

/// <summary>
/// 帮助类
/// </summary>
public class Utils
{
    public static string Version = DateTime.Now.ToString("yyyyMMddHHmm").ToString();

    /// <summary>
    /// 获取数据库连接-明文
    /// </summary>
    /// <returns></returns>
    public string getConnectionStr()
    {
        return System.Configuration.ConfigurationManager.ConnectionStrings["SqlConn"].ConnectionString;
    }

    private SymmetricAlgorithm mobjCryptoService;
    private string strDecKey;
    private string strDecIV;

    public Utils()
    {
        mobjCryptoService = new RijndaelManaged();
        strDecKey = ConfigurationManager.AppSettings["DecKey"] + "efun";
        strDecIV = ConfigurationManager.AppSettings["DecIV"] + "efun";
    }

    /// <summary>
    /// 获得初始向量IV
    /// </summary>
    /// <returns>初试向量IV</returns>
    private byte[] GetLegalIV()
    {
        string sTemp = strDecIV;
        mobjCryptoService.GenerateIV();
        byte[] bytTemp = mobjCryptoService.IV;
        int IVLength = bytTemp.Length;
        if (sTemp.Length > IVLength)
            sTemp = sTemp.Substring(0, IVLength);
        else if (sTemp.Length < IVLength)
            sTemp = sTemp.PadRight(IVLength, ' ');
        return ASCIIEncoding.ASCII.GetBytes(sTemp);
    }

    /// <summary>
    /// 获得密钥
    /// </summary>
    /// <returns>密钥</returns>
    private byte[] GetLegalKey()
    {
        string sTemp = strDecKey;
        mobjCryptoService.GenerateKey();
        byte[] bytTemp = mobjCryptoService.Key;
        int KeyLength = bytTemp.Length;
        if (sTemp.Length > KeyLength)
            sTemp = sTemp.Substring(0, KeyLength);
        else if (sTemp.Length < KeyLength)
            sTemp = sTemp.PadRight(KeyLength, ' ');
        return ASCIIEncoding.ASCII.GetBytes(sTemp);
    }

    /// <summary>
    /// 加密方法
    /// </summary>
    /// <param name="Source">待加密的串</param>
    /// <returns>经过加密的串</returns>
    public string Encrypt(string Source)
    {
        byte[] bytIn = UTF8Encoding.UTF8.GetBytes(Source);
        MemoryStream ms = new MemoryStream();
        mobjCryptoService.Key = GetLegalKey();
        mobjCryptoService.IV = GetLegalIV();
        ICryptoTransform encrypto = mobjCryptoService.CreateEncryptor();
        CryptoStream cs = new CryptoStream(ms, encrypto, CryptoStreamMode.Write);
        cs.Write(bytIn, 0, bytIn.Length);
        cs.FlushFinalBlock();
        ms.Close();
        byte[] bytOut = ms.ToArray();
        return Convert.ToBase64String(bytOut);
    }

    /// <summary>
    /// 解密方法
    /// </summary>
    /// <param name="Source">待解密的串</param>
    /// <returns>经过解密的串</returns>
    public string Decrypt(string Source)
    {
        byte[] bytIn = Convert.FromBase64String(Source);
        MemoryStream ms = new MemoryStream(bytIn, 0, bytIn.Length);
        mobjCryptoService.Key = GetLegalKey();
        mobjCryptoService.IV = GetLegalIV();
        ICryptoTransform encrypto = mobjCryptoService.CreateDecryptor();
        CryptoStream cs = new CryptoStream(ms, encrypto, CryptoStreamMode.Read);
        StreamReader sr = new StreamReader(cs);
        return sr.ReadToEnd();
    }

    /// <summary>
    /// 将数据表转换成JSON类型串
    /// </summary>
    /// <param name="dt">要转换的数据表</param>
    /// <returns></returns>
    public static StringBuilder DataTableToJson(System.Data.DataTable dt)
    {
        return DataTableToJson(dt, true);
    }

    /// <summary>
    /// 将数据表转换成JSON类型串
    /// </summary>
    /// <param name="dt">要转换的数据表</param>
    /// <param name="dispose">数据表转换结束后是否dispose掉</param>
    /// <returns></returns>
    public static StringBuilder DataTableToJson(System.Data.DataTable dt, bool dt_dispose)
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("[\r\n");

        //数据表字段名和类型数组
        string[] dt_field = new string[dt.Columns.Count];
        int i = 0;
        string formatStr = "{{";
        string fieldtype = "";
        foreach (System.Data.DataColumn dc in dt.Columns)
        {
            dt_field[i] = dc.Caption.ToLower().Trim();
            formatStr += "'" + dc.Caption.ToLower().Trim() + "':";
            fieldtype = dc.DataType.ToString().Trim().ToLower();
            if (fieldtype.IndexOf("int") > 0 || fieldtype.IndexOf("deci") > 0 ||
                fieldtype.IndexOf("floa") > 0 || fieldtype.IndexOf("doub") > 0 ||
                fieldtype.IndexOf("bool") > 0)
            {
                formatStr += "{" + i + "}";
            }
            else
            {
                formatStr += "'{" + i + "}'";
            }
            formatStr += ",";
            i++;
        }

        if (formatStr.EndsWith(","))
            formatStr = formatStr.Substring(0, formatStr.Length - 1);//去掉尾部","号

        formatStr += "}},";

        i = 0;
        object[] objectArray = new object[dt_field.Length];
        foreach (System.Data.DataRow dr in dt.Rows)
        {

            foreach (string fieldname in dt_field)
            {   //对 \ , ' 符号进行转换 
                objectArray[i] = dr[dt_field[i]].ToString().Trim().Replace("\\", "\\\\").Replace("'", "\\'");
                switch (objectArray[i].ToString())
                {
                    case "True":
                        {
                            objectArray[i] = "true"; break;
                        }
                    case "False":
                        {
                            objectArray[i] = "false"; break;
                        }
                    default: break;
                }
                i++;
            }
            i = 0;
            stringBuilder.Append(string.Format(formatStr, objectArray));
        }
        if (stringBuilder.ToString().EndsWith(","))
            stringBuilder.Remove(stringBuilder.Length - 1, 1);//去掉尾部","号

        if (dt_dispose)
            dt.Dispose();

        return stringBuilder.Append("\r\n];");
    }

    /// <summary>
    /// 将json转换为DataTable
    /// </summary>
    /// <param name="strJson">得到的json</param>
    /// <returns></returns>
    public static DataTable JsonToDataTable(string strJson)
    {
        //转换json格式
        strJson = strJson.Replace(",\"", "*\"").Replace("\":", "\"#").ToString();
        //取出表名   
        var rg = new Regex(@"(?<={)[^:]+(?=:\[)", RegexOptions.IgnoreCase);
        string strName = rg.Match(strJson).Value;
        DataTable tb = null;
        //去除表名   
        strJson = strJson.Substring(strJson.IndexOf("[") + 1);
        strJson = strJson.Substring(0, strJson.IndexOf("]"));

        //获取数据   
        rg = new Regex(@"(?<={)[^}]+(?=})");
        MatchCollection mc = rg.Matches(strJson);
        for (int i = 0; i < mc.Count; i++)
        {
            string strRow = mc[i].Value;
            string[] strRows = strRow.Split('*');

            //创建表   
            if (tb == null)
            {
                tb = new DataTable();
                tb.TableName = strName;
                foreach (string str in strRows)
                {
                    var dc = new DataColumn();
                    string[] strCell = str.Split('#');

                    if (strCell[0].Substring(0, 1) == "\"")
                    {
                        int a = strCell[0].Length;
                        dc.ColumnName = strCell[0].Substring(1, a - 2);
                    }
                    else
                    {
                        dc.ColumnName = strCell[0];
                    }
                    tb.Columns.Add(dc);
                }
                tb.AcceptChanges();
            }

            //增加内容   
            DataRow dr = tb.NewRow();
            for (int r = 0; r < strRows.Length; r++)
            {
                dr[r] = strRows[r].Split('#')[1].Trim().Replace("，", ",").Replace("：", ":").Replace("\"", "");
            }
            tb.Rows.Add(dr);
            tb.AcceptChanges();
        }

        return tb;
    }

    /// <summary>
    /// MD5加密
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string MD5(string str)
    {
        System.Security.Cryptography.MD5CryptoServiceProvider md5CSP = new System.Security.Cryptography.MD5CryptoServiceProvider();

        byte[] testEncrypt = System.Text.Encoding.Unicode.GetBytes(str);
        byte[] resultEncrypt = md5CSP.ComputeHash(testEncrypt);
        string testResult = System.Text.Encoding.Unicode.GetString(resultEncrypt);
        return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5");
    }


    //SHA1
    public static string SHA1(string str_sha1_in)
    {
        SHA1 sha1 = new SHA1CryptoServiceProvider();
        byte[] bytes_sha1_in = UTF8Encoding.Default.GetBytes(str_sha1_in);
        byte[] bytes_sha1_out = sha1.ComputeHash(bytes_sha1_in);
        string str_sha1_out = BitConverter.ToString(bytes_sha1_out);
        str_sha1_out = str_sha1_out.Replace("-", "");
        return str_sha1_out;
    }

    /// <summary>
    /// 16进制中文转义字符 转换成 正常中文
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string Chinese16ConvertDiaply(string str)
    {
        String MyString = str;

        MyString = MyString.Replace("\\u", "\\");
        MyString = MyString.Replace("\\U", "\\");
        string[] value1 = MyString.Split('\\');
        string tempValue1 = "";
        string tempValue2 = "";
        string strOut = "";
        foreach (string temp in value1)
        {
            if (temp.Length == 0)
            {
                continue;
            }
            try
            {
                tempValue1 = temp;
                tempValue2 = "";
                if (tempValue1.Length > 4)
                {
                    tempValue1 = temp.Substring(0, 4);
                    tempValue2 = temp.Substring(4, temp.Length - 4);
                }
                int intASCII = Convert.ToInt32(tempValue1, 16);
                strOut += ((char)intASCII).ToString() + tempValue2;
            }
            catch
            {
                strOut += temp;
            }
        }
        return strOut;
    }


    /// <summary>
    /// 抓取网页html代码
    /// </summary>
    /// <param name="strUrl">URL</param>
    /// <returns></returns>
    public static string GetStringByUrl(string strUrl)
    {
        //与指定URL创建HTTP请求
        WebRequest wrt = WebRequest.Create(strUrl);
        //获取对应HTTP请求的响应
        WebResponse wrse = wrt.GetResponse();
        //获取响应流
        Stream strM = wrse.GetResponseStream();
        //对接响应流(以"GBK"字符集)
        StreamReader SR = new StreamReader(strM, Encoding.GetEncoding("UTF-8"));
        //获取响应流的全部字符串
        string strallstrm = SR.ReadToEnd();
        //关闭读取流
        SR.Close();
        //返回网页html代码
        return strallstrm;
    }

    public static string ReplaceString(string sContent)
    {
        if (sContent == null) { return sContent; }
        if (sContent.Contains("\\"))
        {
            sContent = sContent.Replace("\\", "\\\\");
        }
        if (sContent.Contains("\'"))
        {
            sContent = sContent.Replace("\'", "\\\'");
        }
        if (sContent.Contains("\""))
        {
            sContent = sContent.Replace("\"", "\\\"");
        }
        //去掉字符串的回车换行符
        sContent = Regex.Replace(sContent, @"[\n\r]", "");
        sContent = sContent.Trim();
        return sContent;
    }

    /// <summary>
    /// SQL注入过滤
    /// </summary>
    /// <param name="InText"></param>
    /// <returns></returns>
    public static bool SqlFilter2(string InText)
    {
        string word="and|exec|insert|select|delete|update|chr|mid|master|or|truncate|char|declare|join";
        if(InText==null)
            return false;
        foreach(string i in word.Split('|'))
        {
            if((InText.ToLower().IndexOf(i+" ")>-1)||(InText.ToLower().IndexOf(" "+i)>-1))
            {
                return true;
            }
        }
        return false;
    }
}