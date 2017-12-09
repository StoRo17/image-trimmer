using System.Drawing;
using System.Drawing.Imaging;

namespace ImageTrimmer
{   
    /// <summary>
    /// Class for handling Bitmap image.
    /// 
    /// Contains a bitmap getter from file name, image trimming,
    /// pixels transparenter. Also can save an image on disk.
    /// </summary>
    class ImageHandler
    { 
        /// <summary>
        /// Returns a bitmap image from the given file name.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>
        /// A bitmap image.
        /// </returns>
        public Bitmap GetBitmapImage(string fileName)
        {
            return Bitmap.FromFile(fileName) as Bitmap;
        }

        /// <summary>
        /// Trims the edges of the image that have the same or similar RGB color.
        /// 
        /// For example if an image has white edges (RGB lower than 250) and 
        /// any non-white figure, this method trims this edges and
        /// returns only the non-white figure.
        /// </summary>
        /// <param name="image">An image that should be trimmed.</param>
        /// <param name="r">R(red) in RGB.</param>
        /// <param name="g">G(green) in RGB.</param>
        /// <param name="b">B(blue) in RGB.</param>
        /// <returns>
        /// A trimmed image without edges.
        /// </returns>
        public Bitmap Trim(Bitmap image, byte r = 250, byte g = 250, byte b = 250)
        {
            Point minLeftTopPixel = new Point(int.MaxValue, int.MaxValue);
            Point maxRightBottomPixel = new Point(int.MinValue, int.MinValue);
            unsafe
            {
                // Lock image bits
                BitmapData bmpData = image.LockBits(new Rectangle(0, 0, image.Width, image.Height),
                    ImageLockMode.ReadWrite,
                    image.PixelFormat);

                // Get a pointer to the first pixel of the image
                byte* ptrFirstPixel = (byte*)bmpData.Scan0;

                // Get the number of bytes per pixel that have image format
                int bytesPerPixel = Bitmap.GetPixelFormatSize(image.PixelFormat) / 8;
                int heightInPixels = bmpData.Height;
                int widthInBytes = bmpData.Width * bytesPerPixel;
                
                int x = 0;
                int y = 0;
                for (int i = 0; i < heightInPixels; i++)
                {
                    // Calculate current bytes of pixels line to handle it 
                    byte* currentLine = ptrFirstPixel + (i * bmpData.Stride);
                    for (int j = 0; j < widthInBytes; j = j + bytesPerPixel)
                    {
                        // Checking the color of the pixel is not greater than  B, G, R accordingly.
                        // currentLine[j] is the value of the BLUE color of the pixel,
                        // currentLine[j + 1] is the value of the GREEN color of the pixel,
                        // currentLine[j + 2] is the value of the RED color of the pixel.
                        // currentLine[j + 2] == R, currentLine[j + 1] == G, currentLine[j] == B in RGB.
                        if (!(currentLine[j] > b && currentLine[j + 1] > g && currentLine[j + 2] > r))
                        {
                            if (x < minLeftTopPixel.X)
                                minLeftTopPixel.X = x;

                            if (y < minLeftTopPixel.Y)
                                minLeftTopPixel.Y = y;

                            if (x > maxRightBottomPixel.X)
                                maxRightBottomPixel.X = x;

                            if (y > maxRightBottomPixel.Y)
                                maxRightBottomPixel.Y = y;
                        }
                        x++;
                    }
                    y++;
                    x = 0;
                }
                image.UnlockBits(bmpData);
            }

            return Crop(image, minLeftTopPixel, maxRightBottomPixel);
        }

        /// <summary>
        /// Makes pixels that has the color in RGB
        /// in the given range transparent.
        /// </summary>
        /// <param name="image">A bitmap image</param>
        /// <param name="from">From what color (RGB) value pixels should be transparent.</param>
        /// <param name="to">To what color (RGB) value pixels should be transparent.</param>
        public void MakeTransparent(Bitmap image, int from = 250, int to = 255)
        {
            for (int i = from; i <= to; i++)
            {
                image.MakeTransparent(Color.FromArgb(i, i, i));
            }
        }

        /// <summary>
        /// Crops the image from the left top pixel to the right bottom pixel.
        /// </summary>
        /// <param name="image">A bitmap image.</param>
        /// <param name="leftTopPixel">A left top pixel point.</param>
        /// <param name="rightBottomPixel">A right bottom pixel point.</param>
        /// <returns>
        /// Cropped bitmap image.
        /// </returns>
        public Bitmap Crop(Bitmap image, Point leftTopPixel, Point rightBottomPixel)
        {
            Rectangle cropRectangle = new Rectangle(
                    leftTopPixel.X,
                    leftTopPixel.Y,
                    rightBottomPixel.X - leftTopPixel.X,
                    rightBottomPixel.Y - leftTopPixel.Y
                );

            Bitmap newBitmap = new Bitmap(cropRectangle.Width, cropRectangle.Height);
            Graphics g = Graphics.FromImage(newBitmap);
            g.DrawImage(image, 0, 0, cropRectangle, GraphicsUnit.Pixel);

            return newBitmap;
        }

        /// <summary>
        /// Saves the image on disk.
        /// </summary>
        /// <param name="image">A bitmap image.</param>
        /// <param name="path">Path where to save the image.</param>
        /// <param name="name">Name of the image file.</param>
        /// <param name="extension">Extension of the image file.</param>
        public void SaveImage(Image image, string path = "C:\\", string name = "image", string extension = "png")
        {
            image.Save($"{path}\\{name}.{extension}");
        }
    }
}
