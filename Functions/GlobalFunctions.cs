using Emgu.CV.Structure;
using Emgu.CV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmguClass.Functions
{
    public static class GlobalFunctions
    {
        public static Image<Bgr, byte> ConvertFloatToByte(Image<Bgr, float> floatImage)
        {
            // Normalizar la imagen de float (0.0 a 1.0) a byte (0 a 255)
            Image<Bgr, byte> byteImage = new Image<Bgr, byte>(floatImage.Width, floatImage.Height);

            // Recorre cada píxel de la imagen y convierte cada valor de float a byte
            for (int y = 0; y < floatImage.Height; y++)
            {
                for (int x = 0; x < floatImage.Width; x++)
                {
                    Bgr floatColor = floatImage[y, x];
                    byteImage[y, x] = new Bgr(
                        floatColor.Blue * 255.0,
                        floatColor.Green * 255.0,
                        floatColor.Red * 255.0
                    );
                }
            }

            return byteImage;
        }
    }
}
