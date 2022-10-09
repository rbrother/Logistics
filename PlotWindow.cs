using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Diagnostics;
using System.Numerics;

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
        image.MouseLeftButtonUp += image_MouseUp;
        Bmp = new WriteableBitmap( PixelWidth, PixelHeight, 96, 96, PixelFormats.Bgra32, null );
        image.Source = Bmp;
        DoPlot();
    }

    void image_MouseUp( object sender, System.Windows.Input.MouseButtonEventArgs e ) {
        var pos = e.MouseDevice.GetPosition( image );
        // Center = Origin + new Complex( pos.X, pos.Y ) * PixelStep;
    }

    protected void DrawPixel( int x, int y, Color color ) {
        byte[] buffer = new byte[4];
        buffer[0] = color.B;
        buffer[1] = color.G;
        buffer[2] = color.R;
        buffer[3] = color.A;
        Bmp.WritePixels(new Int32Rect(0, 0, 1, 1), buffer, 4, 
            Math.Min( x, PixelWidth-1), Math.Min(y, PixelHeight-1));
    }

}
