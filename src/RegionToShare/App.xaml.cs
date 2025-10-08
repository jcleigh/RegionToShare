using System.Windows;
using System.Windows.Media;

namespace RegionToShare;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App
{
    public App()
    {
        // Disable hardware acceleration to ensure compatibility with screen capture
        RenderOptions.ProcessRenderMode = System.Windows.Interop.RenderMode.SoftwareOnly;
        
        InitializeComponent();

        if (!RegionToShare.MainWindow.ValidateSettings())
        {
            Shutdown();
        }
    }
}