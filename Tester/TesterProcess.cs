using Emgu.CV;
using Emgu.CV.Structure;
using EmguClass.Args;
using EmguClass.Resources;
using EmguClass.Results.Types;
using EmguClass.Tools;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmguClass.Tester
{
    public class TesterProcess
    {
        protected Image<Bgr, byte> _imageOriginal {  get; set; }
        protected Image<Bgr, byte> _imageProcessed { get; set; }
        protected Image<Bgr, byte> _imageTested {  get; set; }
        protected Image<Bgr, byte> _imageTemplate {  get; set; }
        protected Image<Bgr, byte> _train { get; set; } = new Image<Bgr, byte>(0, 0);

        protected Image<Bgr, byte> gettingFile(string Path) 
        {
            return new Image<Bgr,byte>(Path);
        }



        //protected Image<Bgr, byte> PatternMatch(List<Tools.TellTale> t, List<Test> b, Image<Bgr, byte> image, int MinScore, int MaxScore)
        //{
        //    foreach (Test item in b)
        //    {
        //        Tools.TellTale d = t.Where(x => x.TestName == item.TestName).First();
        //        int resultScore = 0;
        //        Image<Bgr, byte> train = new Image<Bgr, byte>(0, 0);
        //        VisionClass.PatternMatch(image, d.Template, MinScore, item.Rectangle, d.Name, false, out resultScore, ref train);
        //        TestResults.AddLast(new NumericResult(TestResults.Count, $"{d.TestName} - Pattern Match", MinScore, MaxScore, resultScore, "Pattern Score"));
        //        OnReportReached(new ProcessEventArgs(TestResults.Last.Value)); ;
        //        if (train.Width > 0 && train.Height > 0)
        //        {
        //            string key = string.Empty;
        //            float acc = 0;
        //            model.Test(VisionClass.ImageToByteArray(train), ref key, ref acc);
        //            TestResults.AddLast(new StringResult(TestResults.Count, $"{d.TestName} - ML Result", d.Name, key, "IA result"));
        //            OnReportReached(new ProcessEventArgs(TestResults.Last.Value));
        //            TestResults.AddLast(new NumericResult(TestResults.Count, $"{d.TestName} - ML Result Percentage", 80, 100, acc * 100, "Pattern Score"));
        //            OnReportReached(new ProcessEventArgs(TestResults.Last.Value));
        //            if (acc < .8)
        //            {
        //                save(train, d.Name);
        //            }
        //        }
        //    }
        //    return image;
        //}

    }
}
