using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

abstract public class PlotWindow : Window {
    Image image;
    WriteableBitmap Bmp;
    
    public int PixelWidth { get { return Convert.ToInt32( SystemParameters.PrimaryScreenWidth ); } }
    public int PixelHeight { get { return Convert.ToInt32( SystemParameters.PrimaryScreenHeight ); } }

    protected abstract void DoPlot();

    public PlotWindow( ) {
        this.WindowState = System.Windows.WindowState.Maximized;
        this.WindowStyle = System.Windows.WindowStyle.None;
        image = new Image( );
        this.Content = image;
        Bmp = new WriteableBitmap( PixelWidth, PixelHeight, 96, 96, PixelFormats.Bgra32, null );
        image.Source = Bmp;
        DoPlot();
    }

    protected void DrawPixels(double[,] plotLevel)
    {
        var buffer = new byte[PixelWidth * PixelHeight * 4];
        for (int y = 0; y < PixelHeight; ++y)
        {
            for (int x = 0; x < PixelWidth; ++x)
            {
                var level = Convert.ToByte((1.0 - plotLevel[x, y]) * 255.0);
                var color = Color.FromRgb(level, level, level);
                var bufferOffset = (y * PixelWidth + x) * 4;
                buffer[bufferOffset] = color.B;
                buffer[bufferOffset + 1] = color.G;
                buffer[bufferOffset + 2] = color.R;
                buffer[bufferOffset + 3] = color.A;
            }
        }
        Bmp.WritePixels(new Int32Rect(0, 0, PixelWidth, PixelHeight), buffer, 4 * PixelWidth, 0, 0);
    }

}
