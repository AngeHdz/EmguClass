using Emgu.CV.Structure;
using Emgu.CV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Emgu.CV.CvEnum;
using Emgu.CV.Util;
using EmguClass.Tools;
using System.Drawing;
using System.Drawing.Imaging;
using static System.Net.Mime.MediaTypeNames;
using System.Text.RegularExpressions;
using System.ComponentModel;
using static Tensorflow.Summary.Types;

namespace EmguClass 
{
    public class VisionClass
    {
        public VisionClass() { }

        #region Countours

        public static VectorOfVectorOfPoint Countours(Image<Gray, byte> image)
        {
            VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint();
            Image<Gray, byte> gray = image.Convert<Gray, Byte>();
            CvInvoke.FindContours(gray, contours, null, RetrType.Tree, ChainApproxMethod.ChainApproxNone);
            return contours;
        }

        public static List<Rectangle> BoundingRectangleCountours(Image<Gray, byte> image)
        {
            List<Rectangle> rectangles = new List<Rectangle>();
            VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint();
            Image<Gray, byte> gray = image.Convert<Gray, Byte>();
            CvInvoke.FindContours(gray, contours, null, RetrType.Tree, ChainApproxMethod.ChainApproxNone);
            for (int i = 0; i < contours.Size; i++)
            {
                rectangles.Add(CvInvoke.BoundingRectangle(contours[i]));
            }
            return rectangles;
        }

        public static Image<Gray, byte> FindContours(ref Image<Gray, byte> image, int minArea = 0, bool removeMinArea = false)
        {
            VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint();
            VectorOfVectorOfPoint contourfil = new VectorOfVectorOfPoint();

            CvInvoke.FindContours(image, contours, null, RetrType.Tree, ChainApproxMethod.ChainApproxNone);
            List<VectorOfPoint> filteredContours = new List<VectorOfPoint>();
            // Iterar sobre todos los contornos encontrados
            Image<Gray, byte> resultImage = new Image<Gray, byte>(image.Width, image.Height, new Gray(0));
            for (int i = 0; i < contours.Size; i++)
            {
                double area = CvInvoke.ContourArea(contours[i]);
                //Rectangle rect = CvInvoke.BoundingRectangle(contours[i]);
                if (removeMinArea)
                {
                    if (area >= minArea)
                    {
                        filteredContours.Add(contours[i]);
                    }
                }
                else
                {
                    filteredContours.Add(contours[i]);
                }

            }
            VectorOfVectorOfPoint v = new VectorOfVectorOfPoint(filteredContours.ToArray());
            CvInvoke.DrawContours(resultImage, v, -1, new MCvScalar(255), -1);

            return resultImage;
        }

        #endregion

        #region Roi

        public static void DrawROIs(ref List<Test> t, ref Image<Bgr, byte> image)
        {
            foreach (var item in t)
            {
                image.Draw(item.Rectangle, new Bgr(System.Drawing.Color.Green), 3, LineType.Filled);
                CvInvoke.PutText(image, item.TestName, new Point(item.Rectangle.X, item.Rectangle.Y - 10), FontFace.HersheyComplex, 1, new MCvScalar(0, 255, 0), 2);
            }
        }

        public static void extractRois(List<Test> t, Image<Bgr, byte> image, Image<Gray, byte> mask)
        {

            t.ForEach(x =>
            {
                mask.ROI = x.Rectangle;
                image.ROI = x.Rectangle;
                x.ROIMaskReduce = mask.Copy().Resize(.2, Inter.NearestExact);
                x.RoiReduce = image.Copy().Resize(.2, Inter.NearestExact);
                x.ROIMask = mask.Copy();
                x.Roi = image.Copy();
                mask.ROI = new Rectangle();
                image.ROI = new Rectangle();
            });
        }



        #endregion

        #region PatternMatch

        public static Bitmap PatternMatch(Bitmap originalImage, Bitmap templateImage, int MinScore, Rectangle Roi, string name, bool save, out int score, ref Bitmap TrainImage) 
        {
            Image<Bgr, byte> newImage = new Image<Bgr, byte>(0,0);
            Image<Bgr, byte> ori = originalImage.ToImage<Bgr, byte>();
            Image<Bgr, byte> tem = templateImage.ToImage<Bgr, byte>();

            Bitmap res = PatternMatch(ori, tem, MinScore, Roi, name, save, out score,ref newImage).ToBitmap();
            if (score > 800) 
            {
                TrainImage = newImage.ToBitmap();
            } 
            newImage = null;
            ori = null;
            tem = null;
            return res;
        }

        public static Image<Bgr, byte> PatternMatch(Image<Bgr, byte> originalImage, Image<Bgr, byte> templateImage, int MinScore, Rectangle Roi, string name, bool save, out int score, ref Image<Bgr, byte> TrainImage)
        {
            Image<Bgr, byte> NewImage = originalImage;
            originalImage.ROI = Roi;
            if (originalImage.IsROISet) { }
            score = 0;
            using (Image<Gray, float> result = originalImage.MatchTemplate(templateImage,TemplateMatchingType.CcoeffNormed))
            {
                double[] minValues, maxValues;
                Point[] minLocations, maxLocations;
                originalImage.ROI = new Rectangle(0, 0, 0, 0);
                result.MinMax(out minValues, out maxValues, out minLocations, out maxLocations);

                if (maxLocations.Length < 1)
                {
                    return null;
                }
                // The location of the best match
                Point location = maxLocations[0];
                score = (int)Math.Round(maxValues[0] * 1000);

                // Optionally, if you want to get the location in the original image
                Point locationInOriginalImage = new Point(Roi.X + location.X, Roi.Y + location.Y);

                // Draw a rectangle around the found area
                Rectangle rect = new Rectangle(locationInOriginalImage, templateImage.Size);
                if (score >= MinScore)
                {
                    //if (save) saveForTraining(image: originalImage.Copy(), name, rect);
                    Image<Bgr, byte> train = originalImage.Copy();
                    train.ROI = rect;
                    TrainImage = train.Copy();
                    NewImage.Draw(rect, new Bgr(Color.Red), 4);
                    CvInvoke.PutText(NewImage, name, new Point(rect.X, rect.Y - 10), FontFace.HersheyComplex, 1, new MCvScalar(0, 0, 255), 2);
                }
                NewImage.Draw(Roi, new Bgr(Color.Green), 4);
                return NewImage;
            }
        }
        #endregion

        #region capture

        public static Image<Bgr, byte> capture() 
        {
            VideoCapture capture = new VideoCapture(0);
            Mat frame = new Mat();
            frame = capture.QueryFrame();
            return frame.ToImage<Bgr, byte>();
        }

        public static Bitmap? capture(int CamNum) 
        {
            try
            {
                VideoCapture capture = new VideoCapture(0);
                Mat frame = new Mat();
                frame = capture.QueryFrame();
                return frame.ToImage<Bgr, byte>().ToBitmap();
            }
            catch (Exception e) 
            {
                return null;
            }
        }

        #endregion

        #region ExtractRoi
        public static Bitmap GetRoi(Rectangle rect , Bitmap image) 
        {
            try
            {
                Image<Bgr, byte> originalImage = image.ToImage<Bgr, byte>();
                originalImage.ROI = rect;
                Image<Bgr, byte> TrainImage = originalImage.Copy();
                return TrainImage.AsBitmap();
            }
            catch(Exception e) 
            {
                return null;
            }
        }

        public static void saveRoi(Image<Bgr, byte> train, string name,string path)
        {
            DateTime currentTime = DateTime.Now;
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            train.Save($"{path}\\{name}_{currentTime.ToString("_yyyyMMddHHmmss")}.jpg");
        }
        #endregion

        #region ProcessExecutions

        public static Bitmap Ambar_DarkBackground(Bitmap Image)
        {
            try
            {   Image<Bgr,byte> SnapImage = Image.ToImage<Bgr, byte>();
                Image<Hsv, byte> Snap = Image.ToImage<Hsv, byte>();
                Snap = Snap.SmoothMedian(7);
                ColorE filter = ColorE.GetLimits(ColorType.ambar);
                var Mask = Snap.InRange(filter.Low, filter.High);
                Mask = Mask.Erode(1);
                Mask = Mask.Dilate(1);
                Mask = VisionClass.FindContours(ref Mask, 100, true);

                return Mask.Convert<Bgr, byte>().And(SnapImage).ToBitmap();
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static Bitmap DrawRoi(Bitmap bitmap, Rectangle rect, string Name) 
        {
            if (bitmap == null) return null;
            try 
            {
                Image<Bgr,byte> image = bitmap.ToImage<Bgr,byte>();
                image.Draw(rect, new Bgr(Color.Green), 3, LineType.Filled);
                CvInvoke.PutText(image, Name, new Point(rect.X, rect.Y - 10), FontFace.HersheyComplex, 1, new MCvScalar(0, 255, 0), 2);
                return image.ToBitmap();
            }
            catch (Exception e) 
            {
                return null;
            }
        }

        public static byte[] ImageToByteArray(Image<Bgr, byte> image)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                // Guardar la imagen en formato JPEG en el MemoryStream
                image.ToBitmap().Save(ms, ImageFormat.Jpeg);

                // Convertir el MemoryStream a un array de bytes
                return ms.ToArray();
            }
        }

        #endregion

    }
}
