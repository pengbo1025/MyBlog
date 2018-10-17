using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Common
{
    public class Obj2Json
    {

        //String转Json
        public static string ToJson(string strJson)
        {
            StringBuilder sbuBuilder = new StringBuilder();
            sbuBuilder.Append("{\"Value\":[");
            sbuBuilder.Append("\"");
            sbuBuilder.Append(ReplaceChar(strJson));
            sbuBuilder.Append("\"");
            sbuBuilder.Append("]}");
            return sbuBuilder.ToString();
        }

        //ArrayList转Json
        public static string ToJson(ArrayList aliJson)
        {
            StringBuilder sbuBuilder = new StringBuilder();
            sbuBuilder.Append("{\"Value\":[");
            for (int i = 0; i < aliJson.Count; i++)
            {
                sbuBuilder.Append("\"");
                sbuBuilder.Append(ReplaceChar(aliJson[i].ToString()));
                sbuBuilder.Append("\",");
            }
            if (aliJson.Count > 0)
            {
                sbuBuilder.Remove(sbuBuilder.Length - 1, 1);
            }
            sbuBuilder.Append("]}");
            return sbuBuilder.ToString();
        }

        //DataTable转Json
        public static string ToJson(DataTable dtaJson)
        {
            StringBuilder sbuBuilder = new StringBuilder();
            sbuBuilder.Append("{\"Rows\":[");
            for (int i = 0; i < dtaJson.Rows.Count; i++)
            {
                sbuBuilder.Append("[");
                for (int j = 0; j < dtaJson.Columns.Count; j++)
                {
                    sbuBuilder.Append("\"");
                    sbuBuilder.Append(ReplaceChar(dtaJson.Rows[i][j].ToString()));
                    sbuBuilder.Append("\",");
                }
                sbuBuilder.Remove(sbuBuilder.Length - 1, 1);
                sbuBuilder.Append("],");
            }
            if (dtaJson.Rows.Count > 0)
            {
                sbuBuilder.Remove(sbuBuilder.Length - 1, 1);
            }
            sbuBuilder.Append("]}");
            return sbuBuilder.ToString();
        }

        //DataSet转Json
        public static string ToJson(DataSet dseJson)
        {
            StringBuilder sbuBuilder = new StringBuilder();
            sbuBuilder.Append("{\"Tables\":[");
            foreach (DataTable dtJson in dseJson.Tables)
            {
                sbuBuilder.Append(ToJson(dtJson) + ",");
            }
            sbuBuilder.Remove(sbuBuilder.Length - 1, 1);
            sbuBuilder.Append("]}");
            return sbuBuilder.ToString();
        }

        //替换字符
        private static string ReplaceChar(string strJson)
        {
            StringBuilder sbuJson = new StringBuilder();
            char[] chaJson = strJson.ToCharArray();
            for (int i = 0; i < chaJson.Length; i++)
            {
                char chaReplace = chaJson[i];
                if (chaReplace == '"')
                {
                    sbuJson.Append("\\\"");
                }
                else if (chaReplace == '\'')
                {
                    sbuJson.Append("\\'");
                }
                else if (chaReplace == '\\')
                {
                    sbuJson.Append("\\\\");
                }
                else if (chaReplace == '\b')
                {
                    sbuJson.Append("\\b");
                }
                else if (chaReplace == '\f')
                {
                    sbuJson.Append("\\f");
                }
                else if (chaReplace == '\n')
                {
                    sbuJson.Append("\\n");
                }
                else if (chaReplace == '\r')
                {
                    sbuJson.Append("\\r");
                }
                else if (chaReplace == '\t')
                {
                    sbuJson.Append("\\t");
                }
                else
                {
                    sbuJson.Append(chaReplace);
                }
            }
            return sbuJson.ToString();
        }
    }
}
