using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

public class Advertising
{
    private readonly Image _imageControl;
    private readonly string _imagePath;
    private readonly DispatcherTimer _timer;

    public Advertising(Image imageControl, string imagePath)
    {
        _imageControl = imageControl;
        _imagePath = imagePath;

        _timer = new DispatcherTimer
        {
            Interval = TimeSpan.FromSeconds(5) 
        };
        _timer.Tick += Timer_Tick;
    }

    private void Timer_Tick(object? sender, EventArgs e)
    {
    }

    public void Start()
    {
        _imageControl.Source = new BitmapImage(new Uri(_imagePath, UriKind.RelativeOrAbsolute));
        _timer.Start();
    }

    public void Stop()
    {
        _timer.Stop();
    }
}
