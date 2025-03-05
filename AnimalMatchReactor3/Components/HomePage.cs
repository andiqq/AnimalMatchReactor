using Microsoft.Maui.Layouts;

namespace MauiReactor.Startup.Components;

public class HomePageState
{
    public bool IsVisibleNewGameButton { get; set; }
    public bool IsVisibleAnimalButtons { get; set; }
    
}

public class HomePage : Component<HomePageState>
{
    protected override void OnMounted()
    {
        State.IsVisibleNewGameButton = true;
        State.IsVisibleAnimalButtons = false;
        base.OnMounted();
    }

    public override VisualNode Render()
        => ContentPage(
            ScrollView(
                VStack(
                        Button("Play again?")
                            .OnClicked(PlayAgainButton_OnClicked)
                            .IsVisible(State.IsVisibleNewGameButton)
                            .FontSize(24),

                        Label("Time Elapsed: 0.0 seconds")
                            .FontSize(24),

                        FlexLayout(
                                Enumerable.Range(1, 16).Select(_ =>
                                    Button()
                                        .BackgroundColor(Colors.LightBlue)
                                        .BorderColor(Colors.Black)
                                        .BorderWidth(1)
                                        .HeightRequest(100)
                                        .WidthRequest(100)
                                        .FontSize(60)
                                        .OnClicked(ButtonClicked)
                                )
                            )
                            .Wrap(FlexWrap.Wrap)
                            .MaximumWidthRequest(400)
                            .IsVisible(State.IsVisibleAnimalButtons)
                    )
                    .Spacing(25)
                    .Padding(30, 0)
            ));
    private void PlayAgainButton_OnClicked(object? sender, EventArgs e)
    {
        SetState( s => s.IsVisibleAnimalButtons = true);
        SetState( s => s.IsVisibleNewGameButton = false);
    }

    private static void ButtonClicked(object? arg1, EventArgs arg2)
    {
        throw new NotImplementedException();
    }
}