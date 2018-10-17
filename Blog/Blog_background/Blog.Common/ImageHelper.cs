using System;
using System.Collections.Generic;
using System.Web;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.IO;
using System.Text;
using System.Net;
using System.Collections.Specialized;
namespace Common
{

    /// <summary>
    ///ImageHelper 的摘要说明
    /// </summary>
    public class ImageHelper
    {

        public ImageHelper() { }

        public static ImageHelper GetInstence()
        {
            return new ImageHelper();
        }

        #region 修改尺寸

        /// <summary> 
        /// 缩小图片 
        /// </summary> 
        /// <param name="strOldPic">源图文件名(包括路径)</param> 
        /// <param name="strNewPic">缩小后保存为文件名(包括路径)</param> 
        /// <param name="intWidth">缩小至宽度</param> 
        /// <param name="intHeight">缩小至高度</param> 
        public void SmallPic(string strOldPic, string strNewPic, int intWidth, int intHeight)
        {
            System.Drawing.Bitmap objPic, objNewPic;
            try
            {
                objPic = new System.Drawing.Bitmap(strOldPic);
                objNewPic = new System.Drawing.Bitmap(objPic, intWidth, intHeight);
                objNewPic.Save(strNewPic);
            }
            catch (Exception ex)
            {
                Log.e("UploadError", "图片上传失败 :" + strOldPic + Environment.NewLine + ex.Message + Environment.NewLine + ex.Source, null);
            }
            finally
            {
                objPic = null;
                objNewPic = null;
            }
        }

        /// <summary> 
        /// 按比例缩小图片，自动计算高度 
        /// </summary> 
        /// <param name="strOldPic">源图文件名(包括路径)</param> 
        /// <param name="strNewPic">缩小后保存为文件名(包括路径)</param> 
        /// <param name="intWidth">缩小至宽度</param> 
        public void SmallPicByWidth(string strOldPic, string strNewPic, double intWidth)
        {

            System.Drawing.Bitmap objPic, objNewPic;
            try
            {
                objPic = new System.Drawing.Bitmap(strOldPic);
                double intHeight = intWidth / objPic.Width;
                intHeight = intHeight * objPic.Height;
                objNewPic = new System.Drawing.Bitmap(objPic, Convert.ToInt32(intWidth), Convert.ToInt32(intHeight));
                objNewPic.Save(strNewPic);
            }
            catch (Exception ex)
            {
                Log.e("UploadError", "图片上传失败 :" + strOldPic + Environment.NewLine + ex.Message + Environment.NewLine + ex.Source, null);
            }
            finally
            {
                objPic = null;
                objNewPic = null;
            }
        }


        /// <summary> 
        /// 按比例缩小图片，自动计算宽度 
        /// </summary> 
        /// <param name="strOldPic">源图文件名(包括路径)</param> 
        /// <param name="strNewPic">缩小后保存为文件名(包括路径)</param> 
        /// <param name="intHeight">缩小至高度</param> 
        public void SmallPicByHeight(string strOldPic, string strNewPic, double intHeight)
        {
            System.Drawing.Bitmap objPic = null;
            System.Drawing.Bitmap objNewPic = null;
            try
            {
                objPic = new System.Drawing.Bitmap(strOldPic);
                double intWidth = intHeight / objPic.Height;
                intWidth = intWidth * objPic.Width;
                objNewPic = new System.Drawing.Bitmap(objPic, Convert.ToInt32(intWidth), Convert.ToInt32(intHeight));
                objNewPic.Save(strNewPic);
            }
            catch (Exception ex)
            {
                Log.e("UploadError", "图片上传失败 :" + strOldPic + Environment.NewLine + ex.Message + Environment.NewLine + ex.Source, strOldPic, strNewPic, intHeight);
            }
            finally
            {
                try
                {
                    objPic.Dispose();
                    objNewPic.Dispose();

                    objPic = null;
                    objNewPic = null;
                }
                catch (Exception)
                {

                }
            }
        }

        #endregion


        #region 无损压缩图片

        /// <summary>
        /// 无损压缩图片
        /// </summary>
        /// <param name="sFile">原图片</param>
        /// <param name="dFile">压缩后保存位置</param>
        /// <param name="dHeight">高度</param>
        /// <param name="dWidth">宽度</param>
        /// <param name="flag">压缩质量 1-100</param>
        /// <returns></returns>

        public static bool GetPicThumbnail(string sFile, string dFile, double dHeight_bfb, double dWidth_bfb,
            int flag)
        {
            System.Drawing.Image iSource = System.Drawing.Image.FromFile(sFile);
            ImageFormat tFormat = iSource.RawFormat;
            int sW = 0, sH = 0;

            //按比例缩放
            Size tem_size = new Size(iSource.Width, iSource.Height);


            int dHeight = Convert.ToInt32(tem_size.Height * dHeight_bfb);
            int dWidth = Convert.ToInt32(tem_size.Width * dWidth_bfb);

            if (tem_size.Width > dHeight || tem_size.Width > dWidth) //将**改成c#中的或者操作符号
            {

                if ((tem_size.Width * dHeight) > (tem_size.Height * dWidth))
                {
                    sW = dWidth;
                    sH = (dWidth * tem_size.Height) / tem_size.Width;
                }

                else
                {
                    sH = dHeight;
                    sW = (tem_size.Width * dHeight) / tem_size.Height;
                }
            }
            else
            {
                sW = tem_size.Width;
                sH = tem_size.Height;
            }


            Bitmap ob = new Bitmap(dWidth, dHeight);

            Graphics g = Graphics.FromImage(ob);

            g.Clear(System.Drawing.Color.WhiteSmoke);

            g.CompositingQuality = CompositingQuality.HighQuality;

            g.SmoothingMode = SmoothingMode.HighQuality;

            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            g.DrawImage(iSource, new Rectangle((dWidth - sW) / 2, (dHeight - sH) / 2, sW, sH), 0, 0, iSource.Width, iSource.Height, GraphicsUnit.Pixel);

            g.Dispose();

            //以下代码为保存图片时，设置压缩质量

            EncoderParameters ep = new EncoderParameters();

            long[] qy = new long[1];

            qy[0] = flag; //设置压缩的比例1-100

            EncoderParameter eParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, qy);

            ep.Param[0] = eParam;

            try
            {

                ImageCodecInfo[] arrayICI = ImageCodecInfo.GetImageEncoders();

                ImageCodecInfo jpegICIinfo = null;

                for (int x = 0; x < arrayICI.Length; x++)
                {

                    if (arrayICI[x].FormatDescription.Equals("JPEG"))
                    {

                        jpegICIinfo = arrayICI[x];

                        break;

                    }

                }

                if (jpegICIinfo != null)
                {

                    ob.Save(dFile, jpegICIinfo, ep);//dFile是压缩后的新路径

                }

                else
                {

                    ob.Save(dFile, tFormat);

                }

                return true;

            }

            catch
            {

                return false;

            }

            finally
            {

                iSource.Dispose();

                ob.Dispose();

            }

        }

        #endregion

        public static string UploadImg(string type)
        {
            HttpFileCollection files = null;

            string guid = Guid.NewGuid().ToString();
            if ((files = HttpContext.Current.Request.Files).Count > 0)
            {
                #region 上传图片
                System.IO.Stream stream = HttpContext.Current.Request.Files[0].InputStream;
                byte[] buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);
                string uploadLogStr = new ImageHelper().UploadWebsite(buffer, type, guid);
                //Log.i("upload", uploadLogStr);
                return "http://" + System.Configuration.ConfigurationManager.AppSettings["website"].ToString() + "/upload/"+type+"/source/" + guid + ".jpg";
                #endregion
            }

            return "";
        }

        public static string UploadImg(string type,int index)
        {
            string guid = Guid.NewGuid().ToString();
            try
            {
                #region 上传图片
                System.IO.Stream stream = HttpContext.Current.Request.Files[index].InputStream;
                byte[] buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);
                string uploadLogStr = new ImageHelper().UploadWebsite(buffer, type, guid);
                return "http://" + System.Configuration.ConfigurationManager.AppSettings["website"].ToString() + "/upload/" + type + "/source/" + guid + ".jpg";
                #endregion

            }catch
            {
                return "";  
            }
            
        }
        /// <summary>
        /// 生成二维码
        /// </summary>
        /// <param name="nr"></param>
        //public void BuildQR(string nr, string shopID)
        //{
        //    QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
        //    qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
        //    qrCodeEncoder.QRCodeScale = 4;
        //    qrCodeEncoder.QRCodeVersion = 8;
        //    qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.H;
        //    System.Drawing.Image image = qrCodeEncoder.Encode(nr);

        //    string filename = shopID;

        //    image.Save(HttpContext.Current.Server.MapPath("~/erweima/") + filename + ".jpg");

        //    filename = filename.Replace(" ", "");
        //    filename = filename.Replace(":", "");
        //    filename = filename.Replace("-", "");
        //    filename = filename.Replace(".", "");
        //    filename = filename.Replace("/", "");
        //    CombinImage(image, HttpContext.Current.Server.MapPath("~/erweima/logo.jpg")).Save(HttpContext.Current.Server.MapPath("~/erweima/") + filename + ".jpg");

        //    //this.Image1.ImageUrl = "~/image/" + filename + ".jpg";
        //}

        /// <summary>    
        /// 调用此函数后使此两种图片合并，类似相册，有个    
        /// 背景图，中间贴自己的目标图片    
        /// </summary>    
        /// <param name="imgBack">粘贴的源图片</param>    
        /// <param name="destImg">粘贴的目标图片</param>    
        private static System.Drawing.Image CombinImage(System.Drawing.Image imgBack, string destImg)
        {
            System.Drawing.Image img = System.Drawing.Image.FromFile(destImg);        //照片图片      
            if (img.Height != 65 || img.Width != 65)
            {
                img = KiResizeImage(img, 65, 65, 0);
            }
            Graphics g = Graphics.FromImage(imgBack);

            g.DrawImage(imgBack, 0, 0, imgBack.Width, imgBack.Height);      //g.DrawImage(imgBack, 0, 0, 相框宽, 相框高);     

            //g.FillRectangle(System.Drawing.Brushes.White, imgBack.Width / 2 - img.Width / 2 - 1, imgBack.Width / 2 - img.Width / 2 - 1,1,1);//相片四周刷一层黑色边框    

            //g.DrawImage(img, 照片与相框的左边距, 照片与相框的上边距, 照片宽, 照片高);    

            g.DrawImage(img, imgBack.Width / 2 - img.Width / 2, imgBack.Width / 2 - img.Width / 2, img.Width, img.Height);
            GC.Collect();
            return imgBack;
        }


        /// <summary>    
        /// Resize图片    
        /// </summary>    
        /// <param name="bmp">原始Bitmap</param>    
        /// <param name="newW">新的宽度</param>    
        /// <param name="newH">新的高度</param>    
        /// <param name="Mode">保留着，暂时未用</param>    
        /// <returns>处理以后的图片</returns>    
        private static System.Drawing.Image KiResizeImage(System.Drawing.Image bmp, int newW, int newH, int Mode)
        {
            try
            {
                System.Drawing.Image b = new Bitmap(newW, newH);
                Graphics g = Graphics.FromImage(b);
                // 插值算法的质量    
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(bmp, new Rectangle(0, 0, newW, newH), new Rectangle(0, 0, bmp.Width, bmp.Height), GraphicsUnit.Pixel);
                g.Dispose();
                return b;
            }
            catch
            {
                return null;
            }
        }


        /// <summary>
        /// 上传文件到website
        /// </summary>
        public string UploadWebsite(byte[] buffer, string power, string filename)
        {
            try
            {
                string boundary = DateTime.Now.Ticks.ToString("x");

                HttpWebRequest uploadRequest = (HttpWebRequest)WebRequest.Create(
                    "http://" + System.Configuration.ConfigurationManager.AppSettings["website"] +
                    "/AdminCallback/Upload.ashx?power=" + power + "&filename=" + filename);

                uploadRequest.ContentType = "multipart/form-data; boundary=" + boundary;
                uploadRequest.Method = "POST";
                uploadRequest.Accept = "*/*";
                uploadRequest.KeepAlive = true;
                uploadRequest.Headers.Add("Accept-Language", "zh-cn");
                uploadRequest.Headers.Add("Accept-Encoding", "gzip, deflate");
                uploadRequest.Credentials = System.Net.CredentialCache.DefaultCredentials;
                uploadRequest.Proxy = null;
                WebResponse reponse;

                //创建一个内存流
                Stream memStream = new MemoryStream();
                //确定上传的文件路径 

                boundary = "--" + boundary;
                //添加上传文件参数格式边界
                string paramFormat = boundary + "\r\nContent-Disposition: form-data; name=\"{0}\";\r\n\r\n{1}\r\n";
                NameValueCollection param = new NameValueCollection();
                param.Add("key", Utils.MD5("8776DE97A1B8B9078C3EE3319B7C0112"));
   
   

                //写上参数
                foreach (string key in param.Keys)
                {
                    string formitem = string.Format(paramFormat, key, param[key]);
                    byte[] formitembytes = System.Text.Encoding.UTF8.GetBytes(formitem);
                    memStream.Write(formitembytes, 0, formitembytes.Length);
                }

                //添加上传文件数据格式边界
                string dataFormat = boundary + "\r\nContent-Disposition: form-data; name=\"{0}\";filename=\"{1}\"\r\nContent-Type:application/octet-stream\r\n\r\n";
                string header = string.Format(dataFormat, "Filedata", filename + ".jpg");
                byte[] headerbytes = System.Text.Encoding.UTF8.GetBytes(header);
                memStream.Write(headerbytes, 0, headerbytes.Length);

                //获取文件内容

                //将文件内容写进内存流
                memStream.Write(buffer, 0, buffer.Length);

                //添加文件结束边界
                byte[] boundarybytes = System.Text.Encoding.UTF8.GetBytes("\r\n\n" + boundary + "\r\nContent-Disposition: form-data; name=\"Upload\"\r\n\nSubmit Query\r\n" + boundary + "--");
                memStream.Write(boundarybytes, 0, boundarybytes.Length);

                //设置请求长度
                uploadRequest.ContentLength = memStream.Length;

                //将内存流数据读取位置归零
                memStream.Position = 0;
                byte[] tempBuffer = new byte[memStream.Length];
                memStream.Read(tempBuffer, 0, tempBuffer.Length);
                memStream.Close();

                //获取请求写入流 
                using (Stream requestStream = uploadRequest.GetRequestStream())
                {
                    //将内存流中的buffer写入到请求写入流 
                    requestStream.Write(tempBuffer, 0, tempBuffer.Length);
                    requestStream.Close();
                }

                //获取到上传请求的响应
                reponse = uploadRequest.GetResponse();

                Stream resultStream = reponse.GetResponseStream();

                resultStream.Read(tempBuffer, 0, 1024);
                return System.Text.Encoding.UTF8.GetString(tempBuffer);


            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

    }
}