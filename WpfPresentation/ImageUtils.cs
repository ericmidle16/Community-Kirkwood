/// <summary>
/// Brodie Pasker
/// Created: 2025/03/07
/// 
/// This class has all the methods needed to load images from the database and into the window and vice versa
/// </summary>
///
/// <remarks>
/// Updater Name
/// Updated: yyyy/mm/dd
/// </remarks>
using System.IO;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Text;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WpfPresentation
{
    public class ImageUtils
    {
        /// <summary>
        /// Brodie Pasker
        /// Created: 2025/03/07
        /// 
        /// Converts an ImageSource to a byte array to store in the database
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// <param name="imageSource">The image that needs to be made into a byte array</param>
        /// <param name="mimeType">The mime type of the image to convert</param>
        public static byte[] ConvertBitmapSourceToByteArray(ImageSource imageSource, string mimeType)
        {
            BitmapEncoder encoder;
            if(mimeType == "image/png")
            {
                encoder = new PngBitmapEncoder();
            }
            else if(mimeType == "image/jpeg")
            {
                encoder = new JpegBitmapEncoder();
            }
            else
            {
                encoder = new PngBitmapEncoder();
            }
            byte[] bytes = null;
            var bitmapSource = imageSource as BitmapSource;

            if (bitmapSource != null)
            {
                encoder.Frames.Add(BitmapFrame.Create(bitmapSource));

                using (var stream = new MemoryStream())
                {
                    encoder.Save(stream);
                    bytes = stream.ToArray();
                }
            }

            return bytes;
        }

        /// <summary>
        /// Brodie Pasker
        /// Created: 2025/03/07
        /// 
        /// Converts a BitmapSource to a byte array to store in the database
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// <param name="image">The BitmapSource image that needs to be made into a byte array</param>
        /// <param name="mimeType">The mime type of the image to convert</param>
        public static byte[] ConvertBitmapSourceToByteArray(BitmapSource image, string mimeType)
        {
            byte[] data;
            BitmapEncoder encoder = null;
            if (mimeType == "image/png")
            {
                encoder = new PngBitmapEncoder();
            }
            else if (mimeType == "image/jpeg")
            {
                encoder = new JpegBitmapEncoder();
            }
            else
            {
                encoder = new PngBitmapEncoder();
            }
            encoder.Frames.Add(BitmapFrame.Create(image));
            using (MemoryStream ms = new MemoryStream())
            {
                encoder.Save(ms);
                data = ms.ToArray();
            }
            return data;
        }

        /// <summary>
        /// Brodie Pasker
        /// Created: 2025/03/07
        /// 
        /// Converts any image to a byte array to store in the database
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// <param name="imageSource">The image that needs to be made into a byte array</param>
        /// <param name="mimeType">The mime type of the image to convert</param>
        public static byte[] ConvertImageSourceToByteArray(ImageSource imageSource, string mimeType)
        {
            var image = imageSource as BitmapSource;
            byte[] data;
            BitmapEncoder encoder = null;
            if (mimeType == "image/png")
            {
                encoder = new PngBitmapEncoder();
            }
            else if (mimeType == "image/jpeg")
            {
                encoder = new JpegBitmapEncoder();
            }
            else
            {
                encoder = new PngBitmapEncoder();
            }
            encoder.Frames.Add(BitmapFrame.Create(image));
            using (MemoryStream ms = new MemoryStream())
            {
                encoder.Save(ms);
                data = ms.ToArray();
            }
            return data;
        }

        /// <summary>
        /// Brodie Pasker
        /// Created: 2025/03/07
        /// 
        /// Converts any image URI to a byte array to store in the database
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// <param name="uri">The image URI that needs to be made into a byte array</param>
        /// <param name="mimeType">The mime type of the image to convert</param>
        public static byte[] ConvertBitmapSourceToByteArray(Uri uri, string mimeType)
        {
            var image = new BitmapImage(uri);
            byte[] data;
            BitmapEncoder encoder = null;
            if (mimeType == "image/png")
            {
                encoder = new PngBitmapEncoder();
            }
            else if (mimeType == "image/jpeg")
            {
                encoder = new JpegBitmapEncoder();
            }
            else
            {
                encoder = new PngBitmapEncoder();
            }
            encoder.Frames.Add(BitmapFrame.Create(image));
            using (MemoryStream ms = new MemoryStream())
            {
                encoder.Save(ms);
                data = ms.ToArray();
            }
            return data;
        }

        /// <summary>
        /// Brodie Pasker
        /// Created: 2025/04/30
        /// 
        /// Loads an embedded image resource from the executing assembly and returns it as a byte array.
        /// </summary>
        /// <param name="resourceName">
        /// The full name of the embedded resource (including the default namespace and any folder names),
        /// for example "WpfPresentation.Images.MyImage.png".
        /// </param>
        /// <returns>
        /// A <see cref="byte"/> array containing the contents of the embedded image file.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown if no resource with the specified <paramref name="resourceName"/> is found.
        /// </exception>
        public static byte[] LoadEmbeddedImageAsBytes(string resourceName)
        {
            var asm = Assembly.GetExecutingAssembly();
            using (Stream stream = asm.GetManifestResourceStream(resourceName))
            {
                if (stream == null)
                    throw new ArgumentException($"Resource '{resourceName}' not found.");

                using (var ms = new MemoryStream())
                {
                    stream.CopyTo(ms);
                    return ms.ToArray();
                }
            }
        }

        /// <summary>
        /// Brodie Pasker
        /// Created: 2025/03/07
        /// 
        /// Converts any image from a filepath to a byte array to store in the database
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// <param name="filepath">The image filepath that needs to be made into a byte array</param>
        /// <param name="mimeType">The mime type of the image to convert</param>
        public static byte[] ConvertBitmapSourceToByteArray(string filepath, string mimeType)
        {
            var image = new BitmapImage(new Uri(filepath));
            byte[] data;
            BitmapEncoder encoder = null;
            if (mimeType == "image/png")
            {
                encoder = new PngBitmapEncoder();
            }
            else if (mimeType == "image/jpeg")
            {
                encoder = new JpegBitmapEncoder();
            }
            else
            {
                encoder = new PngBitmapEncoder();
            }
            encoder.Frames.Add(BitmapFrame.Create(image));
            using (MemoryStream ms = new MemoryStream())
            {
                encoder.Save(ms);
                data = ms.ToArray();
            }
            return data;
        }

        /// <summary>
        /// Brodie Pasker
        /// Created: 2025/03/07
        /// 
        /// Converts a byte array from the Database to a BitmapImage to use elsewhere 
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// <param name="bytes">The byte array that will be converted to a bitmap image</param>
        public static BitmapImage ConvertByteArrayToBitmapImage(Byte[] bytes)
        {
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                var image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad; // here
                image.StreamSource = ms;
                image.EndInit();
                return image;
            }
        }

        /// <summary>
        /// Brodie Pasker
        /// Created: 2025/03/07
        /// 
        /// Gets the mime type of the image from the filepath
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// <param name="filePath">The file path of the image that contains the mime type</param>
        public static string GetMimeTypeFromFilePath(string filePath)
        {
            // Get the file extension and convert it to lowercase for comparison
            string fileExtension = System.IO.Path.GetExtension(filePath).ToLower();

            // Map the file extension to the corresponding MIME type
            switch (fileExtension)
            {
                case ".png":
                    return "image/png";
                case ".jpeg":
                case ".jpg":
                    return "image/jpeg";
                default:
                    return "application/octet-stream"; // Default MIME type for unknown files
            }
        }
    }
}
