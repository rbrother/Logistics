using System;
using System.Windows.Media;
using System.Numerics;
using System.Timers;
using System.Diagnostics;

public class LogisticsWindow : PlotWindow {

    double r_min = 3.5;
    double r_max = 4.0;
    double step = 0.0000001;

    protected override void DoPlot( ) {
        var start = DateTime.Now;
        double x = 0.1;
        double[,] plotLevel = new double[PixelWidth,PixelHeight];
        for (double r = r_min; r < r_max; r += step)
        {
            x = x * r * (1 - x);
            double relativeScreenX = (r - r_min) / (r_max - r_min);
            int screenX = Math.Min( Convert.ToInt32(relativeScreenX * PixelWidth), PixelWidth-1);
            int screenY = Math.Min( Convert.ToInt32( PixelHeight * (1-x)), PixelHeight-1);
            plotLevel[screenX, screenY] = Math.Min(plotLevel[screenX, screenY] + 0.1, 1.0);
            var level = Convert.ToByte((1.0-plotLevel[screenX, screenY]) * 255.0);
            DrawPixel(screenX, screenY, Color.FromRgb(level, level, level));
        }
        Debug.WriteLine($"Duration: {DateTime.Now - start}");
    }

} // class
