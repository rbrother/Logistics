using System;
using System.Windows.Media;
using System.Numerics;
using System.Timers;
using System.Diagnostics;

public class LogisticsWindow : PlotWindow
{
    readonly double r_min = 3.5, r_max = 4.0;
    readonly double x_min = 0.0, x_max = 1.0;
    int iterationsPerColumn = 2000;
    double[,] plotLevel;

    double Intensify(double level) => level + (1.0 - level) * 0.2;

    protected override void DoPlot()
    {
        double x = 0.1;
        double step = (r_max - r_min) / PixelWidth / iterationsPerColumn;
        plotLevel = new double[PixelWidth+1, PixelHeight+1];
        for (double r = r_min; r < r_max; r += step)
        {
            x = x * r * (1 - x);
            PlotPoint(x, r);
        }
        DrawPixels(plotLevel);
    }

    private void PlotPoint(double x, double r)
    {
        var relativeScreenX = (r - r_min) / (r_max - r_min);
        var relativeScreenY = (x - x_min) / (x_max - x_min);
        var screenX = Convert.ToInt32(relativeScreenX * PixelWidth);
        var screenY = Convert.ToInt32(PixelHeight * (1 - relativeScreenY));
        if (screenX >= 0 && screenY >= 0 && screenX < PixelWidth && screenY < PixelHeight)
        {
            plotLevel[screenX, screenY] = Intensify(plotLevel[screenX, screenY]);
        }
    }
} // class
