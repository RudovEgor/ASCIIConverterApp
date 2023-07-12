using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASCIIConverterApp
{
    public static class Extensions
    {
        /// <summary>
        /// переводим изображение в монохром
        /// </summary>
        public static void ToGrayscale(this Bitmap bitmap)
        {
            for (int y = 0; y < bitmap.Height; y++)
            {
                for (int x = 0; x < bitmap.Width; x++)
                {
                    var pixel = bitmap.GetPixel(x, y);//получаем пиксель
                    int avg = (pixel.R + pixel.G + pixel.B) / 3;//находим среднее значение(цвет) пикселя
                    bitmap.SetPixel(x, y, Color.FromArgb(pixel.A, avg, avg, avg));//подставляем новый цвет пикселя(pixel.A - альфа канал(прозрачность), его не трогаем)
                }
            }
        }
    }
}
