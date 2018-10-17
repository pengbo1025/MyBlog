using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Aliyun.OSS;
using Aliyun.OSS.Common;

namespace zuimei.Common
{
    public class AliyunOSS
    {

        private static string accessKeyId = ConfigurationManager.AppSettings["id"].ToString();
        private static string accessKeySecret = ConfigurationManager.AppSettings["secret"].ToString();
        private static string endpoint = ConfigurationManager.AppSettings["url"].ToString();
        static string bucketName = ConfigurationManager.AppSettings["bucket"].ToString();

        /// <summary>
        /// 上传图片到阿里云OSS服务器
        /// </summary>
        /// <param name="type">目录名称</param>
        /// <returns></returns>
        public static string UploadPictrue(string type)
        {
            ClientConfiguration conf = new ClientConfiguration();
            conf.IsCname = true;

            OssClient client = new OssClient(endpoint, accessKeyId, accessKeySecret, conf);
            string guid = Guid.NewGuid().ToString();

            //try
            //{
            var stream = HttpContext.Current.Request.Files["images[]"].InputStream;
            client.PutObject(bucketName, "upload/" + type + "/source/" + guid + ".jpg", stream);
            return "/upload/" + type + "/source/" + guid + ".jpg";
            //}
            //catch (Exception e)
            //{
            //    return "error";
            //    throw e;

            //}


        }
    }
}
