using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.IO.File.Images
{
    /// <summary>
    /// 图片操作
    /// </summary>
    public class ImageOperate : Models.FileInfo
    {
        /// <summary>
        /// 图片判断
        /// </summary>
        /// <param name="filePath">文件</param>
        /// <returns>True:是图片;Flase:不是图片/returns>
        public static bool IsImage(string filePath)
        {
            if (!System.IO.File.Exists(filePath)) throw new FileNotFoundException("请填写正确的文件路径");

            string extension = Path.GetExtension(filePath).Replace(".", "").ToLower();

            return Enum.GetNames(typeof(ImageType)).Contains(extension);
        }

        #region 缩略图
        /// <summary>
        /// 生成缩略图
        /// </summary>
        /// <param name="originalImagePath">源图路径（物理路径）</param>
        /// <param name="thumbnailPath">缩略图路径（物理路径）</param>
        /// <param name="width">缩略图宽度</param>
        /// <param name="height">缩略图高度</param>
        /// <param name="mode">生成缩略图的方式</param>    
        public static void MakeThumbnail(string originalImagePath, string thumbnailPath, int width, int height, string mode)
        {
            Image originalImage = Image.FromFile(originalImagePath);

            int towidth = width;
            int toheight = height;

            int x = 0;
            int y = 0;
            int ow = originalImage.Width;
            int oh = originalImage.Height;

            switch (mode)
            {
                case "HW":  //指定高宽缩放（可能变形）                
                    break;
                case "W":   //指定宽，高按比例                    
                    toheight = originalImage.Height * width / originalImage.Width;
                    break;
                case "H":   //指定高，宽按比例
                    towidth = originalImage.Width * height / originalImage.Height;
                    break;
                case "Cut": //指定高宽裁减（不变形）                
                    if ((double)originalImage.Width / (double)originalImage.Height > (double)towidth / (double)toheight)
                    {
                        oh = originalImage.Height;
                        ow = originalImage.Height * towidth / toheight;
                        y = 0;
                        x = (originalImage.Width - ow) / 2;
                    }
                    else
                    {
                        ow = originalImage.Width;
                        oh = originalImage.Width * height / towidth;
                        x = 0;
                        y = (originalImage.Height - oh) / 2;
                    }
                    break;
                default:
                    break;
            }

            //新建一个bmp图片
            Image bitmap = new Bitmap(towidth, toheight);

            //新建一个画板
            Graphics g = Graphics.FromImage(bitmap);

            //设置高质量插值法
            g.InterpolationMode = InterpolationMode.High;

            //设置高质量,低速度呈现平滑程度
            g.SmoothingMode = SmoothingMode.HighQuality;

            //清空画布并以透明背景色填充
            g.Clear(Color.Transparent);

            //在指定位置并且按指定大小绘制原图片的指定部分
            g.DrawImage(originalImage, new Rectangle(0, 0, towidth, toheight), new Rectangle(x, y, ow, oh), GraphicsUnit.Pixel);

            try
            {
                //以jpg格式保存缩略图
                bitmap.Save(thumbnailPath, ImageFormat.Jpeg);
            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
                originalImage.Dispose();
                bitmap.Dispose();
                g.Dispose();
            }
        }
        #endregion

        #region 图片水印
        /// <summary>
        /// 图片水印处理方法
        /// </summary>
        /// <param name="path">需要加载水印的图片路径（绝对路径）</param>
        /// <param name="waterpath">水印图片（绝对路径）</param>
        /// <param name="location">水印位置（传送正确的代码）</param>
        public static string ImageWatermark(string path, string waterpath, string location)
        {
            string kz_name = Path.GetExtension(path);
            if (kz_name == ".jpg" || kz_name == ".bmp" || kz_name == ".jpeg")
            {
                DateTime time = DateTime.Now;
                string filename = "" + time.Year.ToString() + time.Month.ToString() + time.Day.ToString() + time.Hour.ToString() + time.Minute.ToString() + time.Second.ToString() + time.Millisecond.ToString();
                Image img = Bitmap.FromFile(path);
                Image waterimg = Image.FromFile(waterpath);
                Graphics g = Graphics.FromImage(img);
                ArrayList loca = GetLocation(location, img, waterimg);
                g.DrawImage(waterimg, new Rectangle(int.Parse(loca[0].ToString()), int.Parse(loca[1].ToString()), waterimg.Width, waterimg.Height));
                waterimg.Dispose();
                g.Dispose();
                string newpath = Path.GetDirectoryName(path) + filename + kz_name;
                img.Save(newpath);
                img.Dispose();
                System.IO.File.Copy(newpath, path, true);
                if (System.IO.File.Exists(newpath))
                {
                    System.IO.File.Delete(newpath);
                }
            }
            return path;
        }

        /// <summary>
        /// 图片水印位置处理方法
        /// </summary>
        /// <param name="location">水印位置</param>
        /// <param name="sourceImage">需要添加水印的图片</param>
        /// <param name="waterimg">水印图片</param>
        private static ArrayList GetLocation(string location, Image sourceImage, Image waterimg)
        {
            ArrayList loca = new ArrayList();
            int x = 0;
            int y = 0;

            if (location == "LT")
            {
                x = 10;
                y = 10;
            }
            else if (location == "T")
            {
                x = sourceImage.Width / 2 - waterimg.Width / 2;
                y = sourceImage.Height - waterimg.Height;
            }
            else if (location == "RT")
            {
                x = sourceImage.Width - waterimg.Width;
                y = 10;
            }
            else if (location == "LC")
            {
                x = 10;
                y = sourceImage.Height / 2 - waterimg.Height / 2;
            }
            else if (location == "C")
            {
                x = sourceImage.Width / 2 - waterimg.Width / 2;
                y = sourceImage.Height / 2 - waterimg.Height / 2;
            }
            else if (location == "RC")
            {
                x = sourceImage.Width - waterimg.Width;
                y = sourceImage.Height / 2 - waterimg.Height / 2;
            }
            else if (location == "LB")
            {
                x = 10;
                y = sourceImage.Height - waterimg.Height;
            }
            else if (location == "B")
            {
                x = sourceImage.Width / 2 - waterimg.Width / 2;
                y = sourceImage.Height - waterimg.Height;
            }
            else
            {
                x = sourceImage.Width - waterimg.Width;
                y = sourceImage.Height - waterimg.Height;
            }
            loca.Add(x);
            loca.Add(y);
            return loca;
        }
        #endregion

        #region 文字水印
        /// <summary>
        /// 文字水印处理方法
        /// </summary>
        /// <param name="path">图片路径（绝对路径）</param>
        /// <param name="size">字体大小</param>
        /// <param name="letter">水印文字</param>
        /// <param name="color">颜色</param>
        /// <param name="location">水印位置</param>
        public static string LetterWatermark(string path, int size, string letter, Color color, string location)
        {
            #region

            string kz_name = Path.GetExtension(path);
            if (kz_name == ".jpg" || kz_name == ".bmp" || kz_name == ".jpeg")
            {
                DateTime time = DateTime.Now;
                string filename = "" + time.Year.ToString() + time.Month.ToString() + time.Day.ToString() + time.Hour.ToString() + time.Minute.ToString() + time.Second.ToString() + time.Millisecond.ToString();
                Image img = Bitmap.FromFile(path);
                Graphics gs = Graphics.FromImage(img);
                ArrayList loca = GetLocation(location, img, size, letter.Length);
                Font font = new Font("宋体", size);
                Brush br = new SolidBrush(color);
                gs.DrawString(letter, font, br, float.Parse(loca[0].ToString()), float.Parse(loca[1].ToString()));
                gs.Dispose();
                string newpath = Path.GetDirectoryName(path) + filename + kz_name;
                img.Save(newpath);
                img.Dispose();
                System.IO.File.Copy(newpath, path, true);
                if (System.IO.File.Exists(newpath))
                {
                    System.IO.File.Delete(newpath);
                }
            }
            return path;

            #endregion
        }

        /// <summary>
        /// 文字水印位置的方法
        /// </summary>
        /// <param name="location">位置代码</param>
        /// <param name="img">图片对象</param>
        /// <param name="width">宽(当水印类型为文字时,传过来的就是字体的大小)</param>
        /// <param name="height">高(当水印类型为文字时,传过来的就是字符的长度)</param>
        private static ArrayList GetLocation(string location, Image img, int width, int height)
        {
            #region

            ArrayList loca = new ArrayList();  //定义数组存储位置
            float x = 10;
            float y = 10;

            if (location == "LT")
            {
                loca.Add(x);
                loca.Add(y);
            }
            else if (location == "T")
            {
                x = img.Width / 2 - (width * height) / 2;
                loca.Add(x);
                loca.Add(y);
            }
            else if (location == "RT")
            {
                x = img.Width - width * height;
            }
            else if (location == "LC")
            {
                y = img.Height / 2;
            }
            else if (location == "C")
            {
                x = img.Width / 2 - (width * height) / 2;
                y = img.Height / 2;
            }
            else if (location == "RC")
            {
                x = img.Width - height;
                y = img.Height / 2;
            }
            else if (location == "LB")
            {
                y = img.Height - width - 5;
            }
            else if (location == "B")
            {
                x = img.Width / 2 - (width * height) / 2;
                y = img.Height - width - 5;
            }
            else
            {
                x = img.Width - width * height;
                y = img.Height - width - 5;
            }
            loca.Add(x);
            loca.Add(y);
            return loca;

            #endregion
        }
        #endregion

        #region 调整光暗
        /// <summary>
        /// 调整光暗
        /// </summary>
        /// <param name="image">原始图片</param>
        /// <param name="width">原始图片的长度</param>
        /// <param name="height">原始图片的高度</param>
        /// <param name="val">增加或减少的光暗值</param>
        public Bitmap LDPic(Bitmap image, int width, int height, int val)
        {
            Bitmap bm = new Bitmap(width, height);//初始化一个记录经过处理后的图片对象
            int x, y, resultR, resultG, resultB;//x、y是循环次数，后面三个是记录红绿蓝三个值的
            Color pixel;
            for (x = 0; x < width; x++)
            {
                for (y = 0; y < height; y++)
                {
                    pixel = image.GetPixel(x, y);//获取当前像素的值
                    resultR = pixel.R + val;//检查红色值会不会超出[0, 255]
                    resultG = pixel.G + val;//检查绿色值会不会超出[0, 255]
                    resultB = pixel.B + val;//检查蓝色值会不会超出[0, 255]
                    bm.SetPixel(x, y, Color.FromArgb(resultR, resultG, resultB));//绘图
                }
            }
            return bm;
        }
        #endregion

        #region 反色处理
        /// <summary>
        /// 反色处理
        /// </summary>
        /// <param name="image">原始图片</param>
        /// <param name="width">原始图片的长度</param>
        /// <param name="height">原始图片的高度</param>
        public Bitmap RePic(Bitmap image, int width, int height)
        {
            Bitmap bm = new Bitmap(width, height);//初始化一个记录处理后的图片的对象
            int x, y, resultR, resultG, resultB;
            Color pixel;
            for (x = 0; x < width; x++)
            {
                for (y = 0; y < height; y++)
                {
                    pixel = image.GetPixel(x, y);//获取当前坐标的像素值
                    resultR = 255 - pixel.R;//反红
                    resultG = 255 - pixel.G;//反绿
                    resultB = 255 - pixel.B;//反蓝
                    bm.SetPixel(x, y, Color.FromArgb(resultR, resultG, resultB));//绘图
                }
            }
            return bm;
        }
        #endregion

        #region 浮雕处理
        /// <summary>
        /// 浮雕处理
        /// </summary>
        /// <param name="image">原始图片</param>
        /// <param name="Width">原始图片的长度</param>
        /// <param name="Height">原始图片的高度</param>
        public Bitmap FD(Bitmap image, int Width, int Height)
        {
            Bitmap newBitmap = new Bitmap(Width, Height);
            Color color1, color2;
            for (int x = 0; x < Width - 1; x++)
            {
                for (int y = 0; y < Height - 1; y++)
                {
                    int r = 0, g = 0, b = 0;
                    color1 = image.GetPixel(x, y);
                    color2 = image.GetPixel(x + 1, y + 1);
                    r = Math.Abs(color1.R - color2.R + 128);
                    g = Math.Abs(color1.G - color2.G + 128);
                    b = Math.Abs(color1.B - color2.B + 128);
                    if (r > 255) r = 255;
                    if (r < 0) r = 0;
                    if (g > 255) g = 255;
                    if (g < 0) g = 0;
                    if (b > 255) b = 255;
                    if (b < 0) b = 0;
                    newBitmap.SetPixel(x, y, Color.FromArgb(r, g, b));
                }
            }
            return newBitmap;
        }
        #endregion

        #region 拉伸图片
        /// <summary>
        /// 拉伸图片
        /// </summary>
        /// <param name="image">原始图片</param>
        /// <param name="newWidth">新的宽度</param>
        /// <param name="newHeight">新的高度</param>
        public static Bitmap ResizeImage(Bitmap image, int newWidth, int newHeight)
        {
            try
            {
                Bitmap bap = new Bitmap(newWidth, newHeight);
                Graphics g = Graphics.FromImage(bap);
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.DrawImage(bap, new Rectangle(0, 0, newWidth, newHeight), new Rectangle(0, 0, bap.Width, bap.Height), GraphicsUnit.Pixel);
                g.Dispose();
                return bap;
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region 滤色处理
        /// <summary>
        /// 滤色处理
        /// </summary>
        /// <param name="image">原始图片</param>
        /// <param name="width">原始图片的长度</param>
        /// <param name="height">原始图片的高度</param>
        public Bitmap FilPic(Bitmap image, int width, int height)
        {
            Bitmap bm = new Bitmap(width, height);//初始化一个记录滤色效果的图片对象
            int x, y;
            Color pixel;

            for (x = 0; x < width; x++)
            {
                for (y = 0; y < height; y++)
                {
                    pixel = image.GetPixel(x, y);//获取当前坐标的像素值
                    bm.SetPixel(x, y, Color.FromArgb(0, pixel.G, pixel.B));//绘图
                }
            }
            return bm;
        }
        #endregion

        #region 左右翻转
        /// <summary>
        /// 左右翻转
        /// </summary>
        /// <param name="image">原始图片</param>
        /// <param name="width">原始图片的长度</param>
        /// <param name="height">原始图片的高度</param>
        public Bitmap RevPicLR(Bitmap image, int width, int height)
        {
            Bitmap bm = new Bitmap(width, height);
            int x, y, z; //x,y是循环次数,z是用来记录像素点的x坐标的变化的
            Color pixel;
            for (y = height - 1; y >= 0; y--)
            {
                for (x = width - 1, z = 0; x >= 0; x--)
                {
                    pixel = image.GetPixel(x, y);//获取当前像素的值
                    bm.SetPixel(z++, y, Color.FromArgb(pixel.R, pixel.G, pixel.B));//绘图
                }
            }
            return bm;
        }
        #endregion

        #region 上下翻转
        /// <summary>
        /// 上下翻转
        /// </summary>
        /// <param name="image">原始图片</param>
        /// <param name="width">原始图片的长度</param>
        /// <param name="height">原始图片的高度</param>
        public Bitmap RevPicUD(Bitmap image, int width, int height)
        {
            Bitmap bm = new Bitmap(width, height);
            int x, y, z;
            Color pixel;
            for (x = 0; x < width; x++)
            {
                for (y = height - 1, z = 0; y >= 0; y--)
                {
                    pixel = image.GetPixel(x, y);//获取当前像素的值
                    bm.SetPixel(x, z++, Color.FromArgb(pixel.R, pixel.G, pixel.B));//绘图
                }
            }
            return bm;
        }
        #endregion

        #region 压缩图片
        /// <summary>
        /// 压缩到指定尺寸
        /// </summary>
        /// <param name="sourceFilePath">源文件路径</param>
        /// <param name="newFilePath">新文件路径</param>
        public bool Compress(string sourceFilePath, string newFilePath)
        {
            try
            {
                Image img = Image.FromFile(sourceFilePath);
                ImageFormat thisFormat = img.RawFormat;
                Size newSize = new Size(100, 125);
                Bitmap outBmp = new Bitmap(newSize.Width, newSize.Height);
                Graphics g = Graphics.FromImage(outBmp);
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(img, new Rectangle(0, 0, newSize.Width, newSize.Height), 0, 0, img.Width, img.Height, GraphicsUnit.Pixel);
                g.Dispose();
                EncoderParameters encoderParams = new EncoderParameters();
                long[] quality = new long[1];
                quality[0] = 100;
                EncoderParameter encoderParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);
                encoderParams.Param[0] = encoderParam;
                ImageCodecInfo[] arrayICI = ImageCodecInfo.GetImageEncoders();
                ImageCodecInfo jpegICI = null;
                for (int x = 0; x < arrayICI.Length; x++)
                    if (arrayICI[x].FormatDescription.Equals("JPEG"))
                    {
                        jpegICI = arrayICI[x]; //设置JPEG编码
                        break;
                    }
                img.Dispose();
                if (jpegICI != null) outBmp.Save(newFilePath, System.Drawing.Imaging.ImageFormat.Jpeg);
                outBmp.Dispose();
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region 图片灰度化
        public Color Gray(Color color)
        {
            int rgb = Convert.ToInt32((double)(((0.3 * color.R) + (0.59 * color.G)) + (0.11 * color.B)));
            return Color.FromArgb(rgb, rgb, rgb);
        }
        #endregion

        #region 转换为黑白图片
        /// <summary>
        /// 转换为黑白图片
        /// </summary>
        /// <param name="mybt">要进行处理的图片</param>
        /// <param name="width">图片的长度</param>
        /// <param name="height">图片的高度</param>
        public Bitmap BWPic(Bitmap image, int width, int height)
        {
            Bitmap bmp = new Bitmap(width, height);
            int result; //x,y是循环次数，result是记录处理后的像素值
            Color pixel;
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    pixel = image.GetPixel(x, y);//获取当前坐标的像素值
                    result = (pixel.R + pixel.G + pixel.B) / 3;//取红绿蓝三色的平均值
                    bmp.SetPixel(x, y, Color.FromArgb(result, result, result));
                }
            }
            return bmp;
        }
        #endregion

        #region 获取图片中的各帧
        /// <summary>
        /// 获取图片中的各帧
        /// </summary>
        /// <param name="imagePath">图片路径</param>
        /// <param name="pSavePath">保存路径,根目录</param>
        public void GetFrames(string imagePath, string savedPath)
        {
            Image img = Image.FromFile(imagePath);
            FrameDimension fd = new FrameDimension(img.FrameDimensionsList[0]);
            int count = img.GetFrameCount(fd); //获取帧数(gif图片可能包含多帧，其它格式图片一般仅一帧)
            string fileName = Path.GetFileName(imagePath).Replace(Path.GetExtension(imagePath), "");
            for (int i = 0; i < count; i++)    //以Jpeg格式保存各帧
            {
                img.SelectActiveFrame(fd, i);
                string newFilePath = Path.Combine(savedPath, $"{fileName}{i}{Path.GetExtension(imagePath)}");
                img.Save(newFilePath, ImageFormat.Jpeg);
            }
        }
        #endregion
    }
}
