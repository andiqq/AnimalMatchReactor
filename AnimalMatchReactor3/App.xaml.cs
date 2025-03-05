using MauiReactor.Startup.Components;

namespace MauiReactor.Startup;

public partial class App
{
    public App(IServiceProvider serviceProvider)
        :base(serviceProvider)
    {
        InitializeComponent();
    }
}

public abstract class MauiReactorApplication(IServiceProvider serviceProvider)
    : ReactorApplication<HomePage>(serviceProvider);
