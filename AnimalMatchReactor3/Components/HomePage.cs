namespace MauiReactor.Startup.Components;

public class HomePageState
{
    
}

public class HomePage : Component<HomePageState>
{
    public override VisualNode Render()
        => ContentPage(
            ScrollView(
                VStack(
                        Button("Play again?")
                            .OnClicked(PlayAgainButton_OnClicked)
                            .FontSize(24),
                        
                        Label("Time Elapsed: 0.0 seconds")
                            .FontSize(24)
                    )
                    .Spacing(25)
                    .Padding(30, 0)
            )
        );
    
    private void PlayAgainButton_OnClicked(object? arg1, EventArgs arg2)
    {
        throw new NotImplementedException();
    }
}