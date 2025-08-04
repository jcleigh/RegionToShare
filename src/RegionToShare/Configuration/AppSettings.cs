using System.ComponentModel;

namespace RegionToShare.Configuration;

public class AppSettings : INotifyPropertyChanged
{
    private string _windowPlacement = string.Empty;
    private int _framesPerSecond = 15;
    private bool _drawShadowCursor = false;
    private string _themeColor = "SteelBlue";
    private bool _startActivated = false;

    public string WindowPlacement
    {
        get => _windowPlacement;
        set
        {
            if (_windowPlacement != value)
            {
                _windowPlacement = value;
                OnPropertyChanged(nameof(WindowPlacement));
            }
        }
    }

    public int FramesPerSecond
    {
        get => _framesPerSecond;
        set
        {
            if (_framesPerSecond != value)
            {
                _framesPerSecond = value;
                OnPropertyChanged(nameof(FramesPerSecond));
            }
        }
    }

    public bool DrawShadowCursor
    {
        get => _drawShadowCursor;
        set
        {
            if (_drawShadowCursor != value)
            {
                _drawShadowCursor = value;
                OnPropertyChanged(nameof(DrawShadowCursor));
            }
        }
    }

    public string ThemeColor
    {
        get => _themeColor;
        set
        {
            if (_themeColor != value)
            {
                _themeColor = value;
                OnPropertyChanged(nameof(ThemeColor));
            }
        }
    }

    public bool StartActivated
    {
        get => _startActivated;
        set
        {
            if (_startActivated != value)
            {
                _startActivated = value;
                OnPropertyChanged(nameof(StartActivated));
            }
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}