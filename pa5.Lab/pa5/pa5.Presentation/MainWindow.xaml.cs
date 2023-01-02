using pa5.Algorithm;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace pa5.Presentation;

public partial class MainWindow : Window
{
    private AntsGraphSearch search = new();
    private readonly DispatcherTimer _timer = new();
    public MainWindow()
    {
        InitializeComponent();
        _timer.Interval = TimeSpan.FromMilliseconds(10);
        _timer.Tick += _timer_Tick; ;
    }

    private void _timer_Tick(object? sender, EventArgs e)
    {
        UpdateMap();
    }

    void UpdateMap()
    {
        search.UpdateRoads();
        search.Results.Iterations++;
        if (search.Results.Iterations < 50)
        {
            ReDrawCadr();
        }
        else if (search.Results.GenerationsLeft > 0)
        {
            search.UpdateScore();
            search.Results.Iterations = 0;
            search.Results.GenerationsLeft--;
            ReDrawCadr();
            search.Init();
        }
        else
        {
            search.UpdateScore();
        }
    }

    public void ReDrawCadr()
    {
        canvas.Children.Clear();
        var lastColor = false;
        string strokeStyle = "#000000";
        for (int i = 0; i < search.PointsCount; i++)
        {
            for (int j = i + 1; j < search.PointsCount; j++)
            {
                var thisColor = search.Points[i].Path == j
                    || search.Points[j].Path == i;
                if (search.Pheramones[i, j] < 0.01 && !thisColor)
                {
                    continue;
                }

                if (lastColor != thisColor)
                {
                    strokeStyle = thisColor ? "#0000ff" : "#000000";
                    lastColor = thisColor;
                }

                var currentColor = GetColor(strokeStyle);
                var line = GetLine(i, j, thisColor, currentColor);
                canvas.Children.Add(line);
            }
        }

        UpdateText();
    }

    private static SolidColorBrush GetColor(string strokeStyle) =>
        new ((Color)ColorConverter.ConvertFromString(strokeStyle));

    private Line GetLine(int i, int j, bool thisColor, SolidColorBrush currentColor)
    {
        return new()
        {
            X1 = search.Points[i].X,
            Y1 = search.Points[i].Y,
            X2 = search.Points[j].X,
            Y2 = search.Points[j].Y,
            Opacity = thisColor ? 1 : Math.Atan(search.Pheramones[i, j]) / Math.PI * 2,
            Stroke = currentColor,
            StrokeThickness = 1,
        };
    }

    private void UpdateText()
    {
        n.Text = $"Iterations: {search.Results.Iterations} Colony: {search.Results.TestsCount - search.Results.GenerationsLeft}" +
            $"/{search.Results.TestsCount} Help: {search.Results.CurrentScore}";
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        _timer.Start();
    }

    private T? Parse<T>(Func<string, T> parser, string input, string parameterName)
    {
        try
        {
            var result = parser(input);
            return result;
        }
        catch (FormatException)
        {
            n.Text += parameterName + " contains some chars or other incorrect symbols";
            throw;
        }
        catch (OverflowException)
        {
            n.Text += parameterName + " is too large";
            throw;
        }
    }

    private (double, double, double, int, int) ParseInitialValues(out bool success)
    {
        try
        {
            var a = Parse(double.Parse, aT.Text.Trim().Replace('.', ','), "alpha");
            var b = Parse(double.Parse, bT.Text.Trim().Replace('.', ','), "beta");
            var p = Parse(double.Parse, pT.Text.Trim().Replace('.', ','), "ro");
            var pointsCount = Parse(int.Parse, pointsT.Text, "points count");
            var antsCount = Parse(int.Parse, antsT.Text, "ants count");
            success = true;
            return (a, b, p, pointsCount, antsCount);
        }
        catch
        {
            success = false;
        }

        return default;
    }

    private readonly ConfigurationValidation _validation = new();
    private void generateB_Click(object sender, RoutedEventArgs e)
    {
        var (a, b, p, points, ants) = ParseInitialValues(out var success);
        if (!success)
        {
            return;
        }

        var conf = new Configuration(ants, points, p, a, b);
        var validationResult = _validation.Validate(conf);
        if (!validationResult.IsValid)
        {
            n.Text = string.Join("\n", validationResult.Errors.Select(x => x.ErrorMessage));
            return;
            
        }

        search = new(conf);
        ReDrawCadr();
        generateB.IsEnabled = false;
        startB.IsEnabled = true;
        stopB.IsEnabled = true;
        n.Text = "";
    }

    private void stopB_Click(object sender, RoutedEventArgs e)
    {
        _timer.Stop();
    }
}
