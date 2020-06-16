﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using ImageFiltersWPF.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpPost("[action]")]
        public IActionResult GetGauss([FromBody] GaussTransferModel data)
        {
            var returnObject = data;
            var image = ByteArrayToBitmapImage(data.ImageByteArray);

            var kernel = data.Parameters as GaussFilterParams;

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

            var returnImage = BitmapImage.Create(
                image.PixelWidth,
                image.PixelHeight,
                image.DpiX,
                image.DpiY,
                image.Format,
                image.Palette,
                outputImagePixels,
                stride);

            returnObject.ImageByteArray = BitmapSourceToByteArray(returnImage);
            return Ok(returnObject);
        }
        [HttpPost("[action]")]
        public IActionResult GetBinarization([FromBody] BinaryTransferModel data)
        {
            var returnObject = data;
            var image = ByteArrayToBitmapImage(data.ImageByteArray);

            var obj = data.Parameters;

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
            var resultImage = BitmapImage.Create(
                image.PixelWidth,
                image.PixelHeight,
                image.DpiX,
                image.DpiY,
                image.Format,
                image.Palette,
                pixels,
                stride);

            returnObject.ImageByteArray = BitmapSourceToByteArray(resultImage);
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StringContent(JsonConvert.SerializeObject(returnObject), Encoding.UTF8, "application/json");
            return Ok(returnObject);
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
        public byte[] BitmapSourceToByteArray(BitmapSource bitmapImage)
        {
            byte[] data;
            var encoder = new PngBitmapEncoder();

            encoder.Frames.Add(BitmapFrame.Create(bitmapImage));
            using (MemoryStream ms = new MemoryStream())
            {
                encoder.Save(ms);
                data = ms.ToArray();
            }

            return data;
        }

        public BitmapImage ByteArrayToBitmapImage(byte[] array)
        {
            using (var ms = new System.IO.MemoryStream(array))
            {
                var image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.StreamSource = ms;
                image.EndInit();
                return image;
            }
        }
    }
}
