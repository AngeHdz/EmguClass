using Emgu.CV;
using Emgu.CV.Structure;
using EmguClass.Args;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmguClass.Camera
{
    public class CaptureArgs(Image<Bgr,byte> Image): EventArgs 
    {
        public Image<Bgr, byte> Image { get; set; } = Image;
        
    }

    public class WebCapture(int cameraID) : IDisposable
    {
        public event EventHandler<CaptureArgs> OnCapture;
        private VideoCapture _capture;
        private bool _captureInProgress;
        Mat frame = new Mat();
        private int CameraID { get; set; } = cameraID;

        public void StartCapture() 
        {
            try
            {
                _capture = new VideoCapture(CameraID);
                _capture.ImageGrabbed += ProcessFrame;
                _capture.Start();
            }
            catch (NullReferenceException e)
            {
                return;
            }
        }

        protected virtual void OnCaptureReached(CaptureArgs e)
        {
            OnCapture.Invoke(this, e);
        }

        private void ProcessFrame(object? sender, EventArgs e)
        {
            _capture.Retrieve(frame, 0);
            OnCaptureReached(new(frame.ToImage<Bgr, byte>()));
        }

        public void StopCapture() 
        {
            if (_capture != null)
            {
                _capture.Stop();
                _capture.Dispose();
            }
        }

        public Bitmap Snap() 
        {
            return frame.ToBitmap();
        }

        public void Dispose()
        {
            StopCapture();
        }
    }
}
