using ImageFiltersWPF.Models;
using ImageFiltersWPF.ViewModels.Interfaces;
using System.Windows.Media.Imaging;

namespace ImageFiltersWPF.ViewModels.Services
{
    /// <summary>
    /// Class that consumes binarization filter params
    /// </summary>
    public class BinarizationFilterConsumer : IFilterParamsConsumer
    {
        public BitmapSource Consume(BitmapSource image, FilterParamsBase parameters)
        {
            var obj = parameters as BinarizationFilterParams;

            int precision = obj.PrecisionParameter;
            float adjustment = obj.AdjustmentParameter;
            float rParam = obj.RedParameter;
            float gParam = obj.GreenParameter;
            float bParam = obj.BlueParameter;

            int height = image.PixelHeight,
                width = image.PixelWidth;

            int[,] integralImage = new int[height, width];

            int stride = image.PixelWidth * 4;
            int size = image.PixelHeight * stride;
            byte[] pixels = new byte[size];

            image.CopyPixels(pixels, stride, 0);

            for (int i = 0; i < height; i++)
            {
                int sum = 0;
                for (int j = 0; j < width; j++)
                {
                    sum += GetGrayScale(stride, pixels, i, j, rParam, gParam, bParam);
                    if (i == 0)
                    {
                        integralImage[i, j] = sum;
                    }
                    else
                    {
                        integralImage[i, j] = integralImage[i - 1, j] + sum;
                    }
                }
            }

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    var x1 = i - precision / 2;
                    if (x1 <= 0) x1 = 1;
                    var x2 = i + precision / 2;
                    if (x2 >= height) x2 = height - 1;
                    var y1 = j - precision / 2;
                    if (y1 <= 0) y1 = 1;
                    var y2 = j + precision / 2;
                    if (y2 >= width) y2 = width - 1;

                    int count = (x2 - x1) * (y2 - y1);

                    int sum = integralImage[x2, y2] - integralImage[x2, y1 - 1] - integralImage[x1 - 1, y2] + integralImage[x1 - 1, y1 - 1];

                    if ((GetGrayScale(stride, pixels, i, j, rParam, gParam, bParam) * count) <= (sum * (100 - adjustment) / 100))
                    {
                        int index = i * stride + 4 * j;

                        pixels[index] = 0;
                        pixels[index + 1] = 0;
                        pixels[index + 2] = 0;
                    }
                    else
                    {
                        int index = i * stride + 4 * j;

                        pixels[index] = 255;
                        pixels[index + 1] = 255;
                        pixels[index + 2] = 255;
                    }
                }
            }


            return BitmapImage.Create(
                image.PixelWidth,
                image.PixelHeight,
                image.DpiX,
                image.DpiY,
                image.Format,
                image.Palette,
                pixels,
                stride);
        }

        private static int GetGrayScale(int stride, byte[] pixels, int pixel_height, int pixel_width, float rParam, float gParam, float bParam)
        {
            int index = pixel_height * stride + 4 * pixel_width;

            byte red = pixels[index];
            byte green = pixels[index + 1];
            byte blue = pixels[index + 2];

            var grayScale = rParam * red + gParam * green + bParam * blue;
            return (int)grayScale;
        }
    }
}