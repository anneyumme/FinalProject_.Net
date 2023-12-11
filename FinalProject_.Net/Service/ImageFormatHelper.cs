using System.Drawing;
using FinalProject_.Net.Data;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject_.Net.Service;

public class ImageFormatHelper
{

    public static byte[] ConvertToBitmapByte(IFormFile imageFile)
    {

        byte[] imageData;


        try
        {
            using (var memoryStream = new MemoryStream())
            {
                imageFile.CopyTo(memoryStream);

                using (var bitmap = new Bitmap(memoryStream))
                {
                    using (var bitmapStream = new MemoryStream())
                    {
                        bitmap.Save(bitmapStream, System.Drawing.Imaging.ImageFormat.Bmp);
                        imageData = bitmapStream.ToArray();

                    }
                }
                return imageData;
            }
        }
        catch (Exception)
        {
            return null;

        }
    }
}