using ImageFiltersWPF.Models;
using ImageFiltersWPF.ViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;

namespace ImageFiltersWPF.ViewModels.Services
{
    public class GaussFilterConsumer : IFilterParamsConsumer
    {
        public BitmapSource Consume(BitmapSource image, FilterParamsBase parameters)
        {

            var kernel = parameters as GaussFilterParams;

            int height = image.PixelHeight,
                width = image.PixelWidth;



            int stride = image.PixelWidth * 4;
            int size = image.PixelHeight * stride;
            byte[] outputImagePixels = new byte[size];
            byte[] inputImagePixels = new byte[size];
            image.CopyPixels(outputImagePixels, stride, 0);
            image.CopyPixels(inputImagePixels, stride, 0);


            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {

                    float leftTop, midTop, rightTop, leftMid, mid, rightMid, leftBot, midBot, rightBot;

                    for (int rgbParam = 0; rgbParam < 3; rgbParam++)
                    {
                        //Left Top
                        if (j - 1 < 0 || i - 1 < 0 || i - 1 < 0)
                            leftTop = inputImagePixels[(i * stride + 4 * j) + rgbParam] * kernel.LeftTop;
                        else
                            leftTop = inputImagePixels[((i - 1) * stride + 4 * (j - 1)) + rgbParam] * kernel.LeftTop;
                        //Mid Top
                        if (i - 1 < 0)
                            midTop = inputImagePixels[(i * stride + 4 * j) + rgbParam] * kernel.MidTop;
                        else
                            midTop = inputImagePixels[((i - 1) * stride + 4 * j) + rgbParam] * kernel.MidTop;
                        //Right Top 
                        if (i - 1 < 0 || j >= width - 1)
                            rightTop = inputImagePixels[(i * stride + 4 * j) + rgbParam] * kernel.RightTop;
                        else
                            rightTop = inputImagePixels[((i - 1) * stride + 4 * (j + 1)) + rgbParam] * kernel.RightTop;
                        //Left Mid
                        if (j - 1 < 0)
                            leftMid = inputImagePixels[(i * stride + 4 * j) + rgbParam] * kernel.LeftMid;
                        else
                            leftMid = inputImagePixels[(i * stride + 4 * (j - 1)) + rgbParam] * kernel.LeftMid;
                        //Mid
                        mid = inputImagePixels[(i * stride + 4 * j) + rgbParam] * kernel.Mid;
                        //Right Mid
                        if (j >= width - 1)
                            rightMid = inputImagePixels[(i * stride + 4 * j) + rgbParam] * kernel.RightMid;
                        else
                            rightMid = inputImagePixels[(i * stride + 4 * (j + 1)) + rgbParam] * kernel.RightMid;
                        //Left Bot
                        if (j - 1 < 0 || i >= height - 1)
                            leftBot = inputImagePixels[(i * stride + 4 * j) + rgbParam] * kernel.LeftBot;
                        else
                            leftBot = inputImagePixels[((i + 1) * stride + 4 * (j - 1)) + rgbParam] * kernel.LeftBot;
                        //Mid Bot
                        if (i >= height - 1)
                            midBot = inputImagePixels[(i * stride + 4 * j) + rgbParam] * kernel.MidBot;
                        else
                            midBot = inputImagePixels[((i + 1) * stride + 4 * j) + rgbParam] * kernel.MidBot;
                        //Right Bot
                        if (i >= height - 1 || j >= width - 1)
                            rightBot = inputImagePixels[(i * stride + 4 * j) + rgbParam] * kernel.RightBot;
                        else
                            rightBot = inputImagePixels[((i + 1) * stride + 4 * (j + 1)) + rgbParam] * kernel.RightBot;

                        var tempResult = (leftTop + midTop + rightTop + leftMid + mid + rightMid + leftBot + midBot + rightBot) / kernel.KernelSum;
                        outputImagePixels[(i * stride + 4 * j) + rgbParam] = (byte)tempResult;
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
                outputImagePixels,
                stride);
        }
    }
}
