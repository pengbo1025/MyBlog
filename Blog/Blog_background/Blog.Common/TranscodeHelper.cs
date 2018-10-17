using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Diagnostics;
using System.Web;

namespace Common
{
    public class TranscodeHelper
    {
        //文件路径
        public static string ffmpegtool = "Tool/ffmpeg.exe"; //ConfigurationManager.AppSettings["ffmpeg"];
        public static string mencodertool = ConfigurationManager.AppSettings["mencoder"];
        public static string mplayertool = ConfigurationManager.AppSettings["mplayer"];
        /// <summary>
        /// 原文件地址
        /// </summary>
        public static string sourceFile = ConfigurationManager.AppSettings["sourcefile"] + "/";
        /// <summary>
        /// 视频截图地址
        /// </summary>
        public static string imgFile = ConfigurationManager.AppSettings["imgfile"] + "/";
        /// <summary>
        /// 转码文件地址
        /// </summary>
        public static string transcodeFile = ConfigurationManager.AppSettings["transcodeFile"] + "/";
        /// <summary>
        /// 文件图片大小
        /// </summary>
        public static string sizeOfImg = ConfigurationManager.AppSettings["CatchFlvImgSize"];
        /// <summary>
        /// 文件自定义宽度
        /// </summary>
        public static string widthOfFile = ConfigurationManager.AppSettings["widthSize"];
        /// <summary>
        /// 文件自定义高度
        /// </summary>
        public static string heightOfFile = ConfigurationManager.AppSettings["heightSize"];
        /// <summary>
        /// 获取文件的名字
        /// </summary>
        public static string GetFileName(string fileName)
        {
            int i = fileName.LastIndexOf("\\") + 1;
            string Name = fileName.Substring(i);
            return Name;
        }
        /// <summary>
        /// 获取文件扩展名
        /// </summary>
        /// <returns></returns>
        public static string GetExtension(string fileName)
        {
            int i = fileName.LastIndexOf(".") + 1;
            string Name = fileName.Substring(i);
            return Name;
        }

        #region 运行FFMpeg的视频解码，(这里是绝对路径)
        /// <summary>
        /// 转换文件并保存在指定文件夹下面(这里是绝对路径)
        /// </summary>
        /// <param name="fileName">上传视频文件的路径（原文件）</param>
        /// <param name="playFile">转换后的文件的路径（网络播放文件）</param>
        /// <param name="imgFile">从视频文件中抓取的图片路径</param>
        /// <returns>成功:返回图片虚拟地址; 失败:返回空字符串</returns>
        public string ChangeFilePhy(string fileName, string playFile, string imgFile)
        {
            //取得ffmpeg.exe的路径,路径配置在Web.Config中,如:<add key="ffmpeg" value="E:\51aspx\ffmpeg.exe" /> 
            string ffmpeg = HttpContext.Current.Server.MapPath("ffmpeg.exe");
            if ((!System.IO.File.Exists(ffmpeg)) || (!System.IO.File.Exists(fileName)))
            {
                return "";
            }
            //获得图片和(.flv)文件相对路径/最后存储到数据库的路径
            string flv_file = System.IO.Path.ChangeExtension(playFile, ".mp4");

            //截图的尺寸大小,配置在Web.Config中,如:<add key="CatchFlvImgSize" value="240x180" /> 
            string FlvImgSize = TranscodeHelper.sizeOfImg;
            System.Diagnostics.ProcessStartInfo FilestartInfo = new System.Diagnostics.ProcessStartInfo(ffmpeg);
            FilestartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;

            //FilestartInfo.Arguments = " -i " + fileName + " -ab 56 -ar 22050 -b 500 -r 15 -s " + widthOfFile + "x" + heightOfFile + " " + flv_file;
            FilestartInfo.Arguments = " -i " + fileName + " -y -ab 32 -ar 22050 -qscale 10 -s " + widthOfFile + "x" + heightOfFile + " -r 15 " + flv_file + "";
            FilestartInfo.UseShellExecute = false;
            FilestartInfo.CreateNoWindow = false;

            FilestartInfo.RedirectStandardInput = true;
            FilestartInfo.RedirectStandardOutput = true;
            FilestartInfo.RedirectStandardError = true;

            try
            {
                //转换
                Process process = Process.Start(FilestartInfo);
                process.WaitForExit();
                process.ErrorDataReceived += new DataReceivedEventHandler(process_ErrorDataReceived);

                //截图
                CatchImg(fileName, imgFile);
            }
            catch
            {
                return "";
            }
            return "";
        }

        void process_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            Console.WriteLine(e.Data);
        }
        //
        public string CatchImg(string fileName, string imgFile)
        {
            string ffmpeg = HttpContext.Current.Server.MapPath("~/") + TranscodeHelper.ffmpegtool;
            //string imgFolder = HttpContext.Current.Server.MapPath("~/") + TranscodeHelper.imgFile + _Common.GetCookie("UserID");
            //if (!System.IO.Directory.Exists(imgFolder))
            //{
            //    System.IO.Directory.CreateDirectory(imgFolder);
            //}
            string imFile = imgFile; // imgFolder + "/" + imgFile + ".jpg";

            //Log.i("视频截图", "imgFolder=" + imgFolder);
            //Log.i("视频截图", "imFile=" + imFile);

            string FlvImgSize = TranscodeHelper.sizeOfImg;
            System.Diagnostics.ProcessStartInfo ImgstartInfo = new System.Diagnostics.ProcessStartInfo(ffmpeg);
            ImgstartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;

            string arg = " -i " + fileName + " -y -f image2 -t 1 -s " + FlvImgSize + " " + imFile;

            Log.e("动态视频截图", "参数:" + arg);

            ImgstartInfo.Arguments = arg;
            ImgstartInfo.UseShellExecute = false;

            //Log.i("视频截图", "参数=" + arg);
            try
            {
                System.Diagnostics.Process.Start(ImgstartInfo);
            }
            catch (Exception e)
            {
                Log.e("动态视频截图", "截图失败:" + e.Message);
                return "";
            }
            if (System.IO.File.Exists(imFile))
            {
                //Log.i("视频截图", "成功");
                return imFile;
            }
            //Log.i("视频截图", "失败");
            return "";
        }
        #endregion


        #region //运行FFMpeg的视频解码，(这里是(虚拟)相对路径)
        /// <summary>
        /// 转换文件并保存在指定文件夹下面(这里是相对路径)
        /// </summary>
        /// <param name="fileName">上传视频文件的路径（原文件）</param>
        /// <param name="playFile">转换后的文件的路径（网络播放文件）</param>
        /// <param name="imgFile">从视频文件中抓取的图片路径</param>
        /// <returns>成功:返回图片虚拟地址; 失败:返回空字符串</returns>
        public string ChangeFileVir(string fileName, string playFile, string imgFile)
        {
            //取得ffmpeg.exe的路径,路径配置在Web.Config中,如:<add key="ffmpeg" value="E:\51aspx\ffmpeg.exe" /> 
            string ffmpeg = HttpContext.Current.Server.MapPath(TranscodeHelper.ffmpegtool);
            if ((!System.IO.File.Exists(ffmpeg)) || (!System.IO.File.Exists(fileName)))
            {
                return "";
            }
            //获得图片和(.flv)文件相对路径/最后存储到数据库的路径
            string flv_img = System.IO.Path.ChangeExtension(HttpContext.Current.Server.MapPath(imgFile), ".jpg");
            string flv_file = System.IO.Path.ChangeExtension(HttpContext.Current.Server.MapPath(playFile), ".flv");

            //截图的尺寸大小
            string FlvImgSize = TranscodeHelper.sizeOfImg;
            System.Diagnostics.ProcessStartInfo FilestartInfo = new System.Diagnostics.ProcessStartInfo(ffmpeg);
            System.Diagnostics.ProcessStartInfo ImgstartInfo = new System.Diagnostics.ProcessStartInfo(ffmpeg);
            FilestartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            ImgstartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;

            //此处组合成ffmpeg.exe文件需要的参数即可,此处命令在ffmpeg 0.4.9调试通过 
            //ffmpeg -i F:\01.wmv -ab 56 -ar 22050 -b 500 -r 15 -s 320x240 f:\test.flv
            FilestartInfo.Arguments = " -i " + fileName + " -ab 56 -ar 22050 -b 500 -r 15 -s " + widthOfFile + "x" + heightOfFile + " " + flv_file;
            ImgstartInfo.Arguments = " -i " + fileName + " -y -f image2 -t 0.001 -s " + FlvImgSize + " " + flv_img;

            FilestartInfo.UseShellExecute = false;
            ImgstartInfo.UseShellExecute = false;
            try
            {
                System.Diagnostics.Process.Start(FilestartInfo);
                System.Diagnostics.Process.Start(ImgstartInfo);
            }
            catch
            {
                return "";
            }
            /**/
            ///注意:图片截取成功后,数据由内存缓存写到磁盘需要时间较长,大概在3,4秒甚至更长; 
            ///这儿需要延时后再检测,我服务器延时8秒,即如果超过8秒图片仍不存在,认为截图失败; 
            ///此处略去延时代码.如有那位知道如何捕捉ffmpeg.exe截图失败消息,请告知,先谢过! 
            if (System.IO.File.Exists(flv_img))
            {
                return flv_img;
            }
            return "";
        }

        #endregion


        #region 运行mencoder的视频解码器转换(这里是(绝对路径))
        public string MChangeFilePhy(string sourceFile, string transFile, string imgName)
        {
            string tool = HttpContext.Current.Server.MapPath("~/") + TranscodeHelper.mencodertool;
            //string mplaytool = Server.MapPath(TranscodeHelper.ffmpegtool);
            if ((!System.IO.File.Exists(tool)) || (!System.IO.File.Exists(sourceFile)))
            {
                return "";
            }

            string media_file = System.IO.Path.ChangeExtension(transFile, ".mp4");
            //截图的尺寸大小,配置在Web.Config中,如:<add key="CatchFlvImgSize" value="240x180" /> 
            string FlvImgSize = TranscodeHelper.sizeOfImg;
            System.Diagnostics.ProcessStartInfo FilestartInfo = new System.Diagnostics.ProcessStartInfo(tool);
            FilestartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            string arg = string.Empty;

            //1
            //arg = " " + sourceFile + " -o " + flv_file + @" -of lavf -lavfopts i_certify_that_my_video_stream_does_not_use_b_frames -oac mp3lame -lameopts abr:br=56 -ovc lavc -lavcopts vcodec=mp4:vbitrate=200:mbd=2:mv0:trell:v4mv:cbp:last_pred=1:dia=-1:cmp=0:vb_strategy=1 -vf scale=480:320 -ofps 16 -srate 22050";
            
            //2
            arg = " -srate 22050 -ofps 16 -oac mp3lame -lameopts mode=3:cbr:br=24 -ovc lavc -ffourcc DX50 -lavcopts vcodec=mpeg4:vhq:vbitrate=112 " + sourceFile + " -o " + media_file;

            FilestartInfo.Arguments = arg;

            try
            {
                //转换
                System.Diagnostics.Process.Start(FilestartInfo);
                //截图
                CatchImg(media_file, imgName);
            }
            catch
            {
                return "";
            }
            //
            return "";
        }
        #endregion




    }
}
