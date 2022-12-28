namespace pa2.Common;

public class TimerService
{ 

    private readonly System.Timers.Timer _timer;
    private const int TIME_OUT = 10 * 1000;
    private bool isCancelationRequested = false;
    public bool IsCancelationRequested => isCancelationRequested;
    public TimerService()
    {
        _timer = new(TIME_OUT);
        _timer.Elapsed += _timer_Elapsed;
    }

    public void Start()
    {
        _timer.Start();
    }

    private void _timer_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
    {
        isCancelationRequested = true;
    }

    public void ThrowIfCancelationRequested()
    {
        if (isCancelationRequested)
        {
            throw new Exception("Excetution time is out, please try again");
        }
    }
}
