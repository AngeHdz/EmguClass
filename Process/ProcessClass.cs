using Emgu.CV.Structure;
using Emgu.CV;
using EmguClass.Results;
using EmguClass.Results.Types;
using EmguClass.Tools;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MLClass;
using System.Drawing.Imaging;
using EmguClass.Args;
using EmguClass.TimerTool;

namespace EmguClass.Process
{
    public abstract class ProcessClass
    {
        private MLModel model;
        public LinkedList<ResultClass> TestResults = new LinkedList<ResultClass>();
        public List<ImageResult> imageResults = new List<ImageResult>();

        public event EventHandler<ProcessEventArgs> OnReport;
        public event EventHandler<ProcessOnFinishEventArgs> OnFinish;
        public event EventHandler<ImageEventArgs> OnReportImage;
        public event EventHandler<TimerEventArgs> OnReportTime;
        protected timerClass timer = new timerClass();

        protected ProcessClass()
        {
            model = new MLModel();
            timer.OnReportTime += Timer_OnReportTime;
        }

        private void Timer_OnReportTime(object? sender, TimerEventArgs e)
        {
            OnReportTimeReached(e);
        }

        /// <summary>
        /// PatternMatch function to implement IA
        /// </summary>
        /// <param name="t"></param>
        /// <param name="b"></param>
        /// <param name="image"></param>
        /// <param name="MinScore"></param>
        /// <param name="MaxScore"></param>
        /// <returns></returns>
        protected Image<Bgr, byte> PatternMatch(List<Tools.TellTale> t, List<Test> b, Image<Bgr, byte> image, int MinScore, int MaxScore)
        {
            foreach (Test item in b)
            {
                Tools.TellTale d = t.Where(x => x.TestName == item.TestName).First();
                int resultScore = 0;
                Image<Bgr, byte> train = new Image<Bgr, byte>(0,0);
                VisionClass.PatternMatch(image, d.Template, MinScore, item.Rectangle, d.Name, false, out resultScore,ref train);
                TestResults.AddLast(new NumericResult(TestResults.Count,$"{d.TestName} - Pattern Match" , MinScore, MaxScore, resultScore, "Pattern Score"));
                OnReportReached(new ProcessEventArgs(TestResults.Last.Value));;
                if(train.Width > 0 && train.Height > 0) 
                {
                    string key = string.Empty;
                    float acc = 0;
                    model.Test(VisionClass.ImageToByteArray(train),ref key,ref acc);
                    TestResults.AddLast(new StringResult(TestResults.Count, $"{d.TestName} - ML Result", d.Name, key, "IA result"));
                    OnReportReached(new ProcessEventArgs(TestResults.Last.Value));
                    TestResults.AddLast(new NumericResult(TestResults.Count, $"{d.TestName} - ML Result Percentage", 80, 100, acc * 100, "Pattern Score"));
                    OnReportReached(new ProcessEventArgs(TestResults.Last.Value));
                    if (acc < .8) 
                    {
                        save(train, d.Name);
                    }
                }
            }
            return image;
        }
        /// <summary>
        /// Add Result to ResultImage List
        /// </summary>
        /// <param name="name"></param>
        /// <param name="bitmap"></param>
        protected void addresult(string name, Bitmap bitmap)
        {
            imageResults.Add(new ImageResult { Name = name, Bitmap = bitmap });
            OnImageReached(new ImageEventArgs(name, bitmap));
        }

        protected static void save(Image<Bgr, byte> train, string name) 
        {
            DateTime currentTime = DateTime.Now;
            string path = $"C:\\Image\\ReTraining\\{name}\\";
            if (!Directory.Exists(path)) 
            {
                Directory.CreateDirectory(path);
            }
            train.Save($"{path}{currentTime.ToString("yyyyMMdd_HHmmss")}.jpg");
        }

        //protected static byte[] ImageToByteArray(Image<Bgr, byte> image)
        //{
        //    using (MemoryStream ms = new MemoryStream())
        //    {
        //        // Guardar la imagen en formato JPEG en el MemoryStream
        //        image.ToBitmap().Save(ms, ImageFormat.Jpeg);

        //        // Convertir el MemoryStream a un array de bytes
        //        return ms.ToArray();
        //    }
        //}

        protected virtual void OnReportReached(ProcessEventArgs e) 
        {
            OnReport.Invoke(this, e);
        }

        protected virtual void OnFinishReached(ProcessOnFinishEventArgs e)
        {
            OnFinish.Invoke(this, e);
        }
        protected virtual void OnImageReached(ImageEventArgs e)
        {
            OnReportImage.Invoke(this, e);
        }
        protected virtual void OnReportTimeReached(TimerEventArgs e)
        {
            OnReportTime.Invoke(this, e);
        }

        protected void start() 
        {
            TestResults.Clear();
            imageResults.Clear();
        }

        protected void finish() 
        {
            var result = new ProcessOnFinishEventArgs(ResultType.Fail);
            int NoPass = TestResults.ToList().Where(x => x.ResultType != ResultType.Pass).Count();
            if(NoPass == 0) result.ResultType = ResultType.Pass;
            OnFinishReached(result);
        }
    }
}

