
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Drawing;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using EmguClass.Tools;
using static System.Net.Mime.MediaTypeNames;
using EmguClass.Results;
using EmguClass.Results.Types;
using EmguClass.Process;
using MLClass;
using EmguClass.Args;
using Microsoft.ML;

namespace EmguClass
{
    public class TellTale : ProcessClass
    {
        public TellTale()
        {
            
        }

        public void AsyncProcess(string SnapImage, ColorType TelltallecolorType, TestType testType, string PathPattern) 
        {
            Task.Run(() => {
                Process(SnapImage, TelltallecolorType, testType, PathPattern);
            });
        }

        public void Process(string SnapImage, ColorType TelltallecolorType, TestType testType, string PathPattern)
        {
            timer.Start();
            start();
            Image<Hsv, byte> Snap = new Image<Hsv, byte>(SnapImage);
            addresult("Capture", Snap.ToBitmap());
            Snap = Snap.SmoothMedian(7);
            ColorE filter = ColorE.GetLimits(TelltallecolorType);
            var Mask = Snap.InRange(filter.Low, filter.High);
            Mask = Mask.Erode(1);
            Mask = Mask.Dilate(1);
            Mask = VisionClass.FindContours(ref Mask, 100, true);

            addresult("Mask", Mask.ToBitmap());
            Image<Bgr, byte> res = Mask.Convert<Bgr, byte>().And(new Image<Bgr, byte>(SnapImage));
            addresult("Result", res.ToBitmap());

            List<Test> buttons = Test.GetTest(testType);
            VisionClass.extractRois(buttons, res, Mask);
            VisionClass.DrawROIs(ref buttons, ref res);
            addresult("Rois", res.ToBitmap());
            List<Tools.TellTale> telltales = Tools.TellTale.getTelltales(PathPattern);

            List<Rectangle> rects = VisionClass.BoundingRectangleCountours(Mask);

            Image<Bgr, byte> buttonRes = PatternMatch(telltales, buttons, res, 800, 1000);
            addresult("TestButton", buttonRes.ToBitmap());

            finish();
            timer.Stop();
        }
    }
}
