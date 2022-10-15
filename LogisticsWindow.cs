using System;
using System.Windows.Media;
using System.Numerics;
using System.Timers;
using System.Diagnostics;

public class LogisticsWindow : PlotWindow
{
    readonly double r_min = 3.4, r_max = 4.0;
    readonly double x_min = 0.0, x_max = 1.0;
    double[,] plotLevel;

    double Intensify(double level) => level + (1.0 - level) * 0.2;

    protected override void DoPlot()
    {
        int totalIterations = 0;
        double x = 0.1;
        double r = r_min;
        double column_r_step = (r_max - r_min) / PixelWidth;
        plotLevel = new double[PixelWidth+1, PixelHeight+1];
        for (int screenColumn = 0; screenColumn < PixelWidth; screenColumn++)
        {
            var colFinished = false;
            while(!colFinished) {
                totalIterations++;
                x = x * r * (1 - x);
                colFinished = AddPoint(screenColumn, x);
            }
            r += column_r_step;
        }
        DrawPixels(plotLevel);
        Console.WriteLine($"totalIterations: {totalIterations}");
    }

    private bool AddPoint(int screenColumn, double x)
    {
        var relativeScreenY = (x - x_min) / (x_max - x_min);
        var screenY = Convert.ToInt32(PixelHeight * (1 - relativeScreenY));
        if (screenY >= 0 && screenY < PixelHeight)
        {
            var newLevel = Intensify(plotLevel[screenColumn, screenY]);
            plotLevel[screenColumn, screenY] = newLevel;
            if (newLevel > 0.99) return true;
        }
        return false;
    }
} // class
