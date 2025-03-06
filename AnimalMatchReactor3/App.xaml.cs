using AnimalMatchReactor.Components;

namespace AnimalMatchReactor;

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
