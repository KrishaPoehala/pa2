using pa4.Algorithm;
using pa4.Algorithm.Settings;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace pa4.Presentation;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly ConfigurationValidation _validation = new();
    private readonly DispatcherTimer _drawTimer = new();
    private readonly DispatcherTimer _stepTimer = new();
    private Search _search;
    public MainWindow()
    {
        InitializeComponent();
        _stepTimer.Tick += _stepTimer_Tick;
        _stepTimer.Interval = TimeSpan.FromMilliseconds(10);

        _drawTimer.Tick += _drawTimer_Tick;
        _drawTimer.Interval = TimeSpan.FromMilliseconds(100);
    }

    private void _drawTimer_Tick(object? sender, EventArgs e)
    {
        ReDrawCadr();
    }

    private void _stepTimer_Tick(object? sender, EventArgs e)
    {
        _search.UpdateRoades();
    }

    void ReDrawCadr()
    {
        canvas.Children.Clear();
        for (int i = 0; i < _search.Points.Count; i++)
        {
            for (int j = 0; j < _search.Points.Count; ++j)
            {
                if (i == j && _search.Pheramones[i, j] <= 0.0001)
                {
                    continue;
                }

                var line = GetGraphLine(i, j);

                canvas.Children.Add(line);
            }
        }

        for (int i = 0; i < _search.Points.Count; i++)
        {
            var eclipse = GetGraphPoint();

            Canvas.SetTop(eclipse, _search.Points[i].Y - 5);
            Canvas.SetLeft(eclipse, _search.Points[i].X - 5);
            canvas.Children.Add(eclipse);
        }
    }

    private static Ellipse GetGraphPoint()
    {
        return new()
        {
            Fill = new SolidColorBrush(Colors.Green),
            Width = 10,
            Height = 10
        };
    }

    private Line GetGraphLine(int i, int j)
    {
        return new()
        {
            X1 = _search.Points[i].X,
            Y1 = _search.Points[i].Y,
            X2 = _search.Points[j].X,
            Y2 = _search.Points[j].Y,
            Opacity = 2 * Math.Atan(_search.Pheramones[i, j]) / Math.PI,
            Stroke = new SolidColorBrush(Colors.Black),
            StrokeThickness = 1,
        };
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        generateB.IsEnabled = false;
        _stepTimer.Start();
        _drawTimer.Start();
    }

    private (double, double, double, int, int) ParseInitialValues(out bool success)
    {
        try
        {
            var a = double.Parse(aT.Text.Trim().Replace('.', ','));
            var b = double.Parse(bT.Text.Trim().Replace('.', ','));
            var p = double.Parse(pT.Text.Trim().Replace('.', ','));
            var pointsCount = int.Parse(pointsT.Text);
            var antsCount = int.Parse(antsT.Text);
            success = true;
            return (a, b, p, pointsCount, antsCount);
        }
        catch (Exception)
        {
            success = false;
            return default;
        }
    }

    private void generateB_Click(object sender, RoutedEventArgs e)
    {
        var (a, b, p, points, ants) = ParseInitialValues(out var success);
        if (!success)
        {
            output.Text = "Values are not in a correct format!";
            return;
        }

        var conf = new AlgorithmConfiguration(a, b, p, points, ants);
        var validationResult = _validation.Validate(conf);
        if (validationResult.IsValid)
        {
            _search = new(new RandomService(), conf);
            ReDrawCadr();
            generateB.IsEnabled = false;
            startB.IsEnabled = true;
            stopB.IsEnabled = true;
            output.Text = "";
        }
        else
        {
            output.Text = string.Join("\n", validationResult.Errors.Select(x => x.ErrorMessage));
        }
    }

    private void Button_Click_1(object sender, RoutedEventArgs e)
    {
        generateB.IsEnabled = true;
        _drawTimer.Stop();
        _stepTimer.Stop();
    }
}