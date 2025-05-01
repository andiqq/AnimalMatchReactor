using Microsoft.Maui.Layouts;
using Button = Microsoft.Maui.Controls.Button;

namespace AnimalMatchReactor;

public class MainPage : Component<Game>
{
    public override VisualNode Render()
        => ContentPage(
            ScrollView(
                VStack(
                        Timer()
                            .Interval(100)
                            .IsEnabled(!State.GameWon)
                            .OnTick(_ => SetState(s => s.TimeElapsed++)),
                        Label($"Time elapsed: {State.TimeElapsed / 10f:0.0}s")
                            .FontSize(24),
                        Button("Play again?")
                            .OnClicked(PlayAgainButton_OnClicked)
                            .IsVisible(State.GameWon)
                            .FontSize(24),
                        FlexLayout(
                                State.AnimalButtons.Select((button, index) =>
                                    Button(button.Emoji)
                                        .BackgroundColor(button.Selected ? Colors.Purple : Colors.LightBlue)
                                        .BorderColor(Colors.Black)
                                        .BorderWidth(1)
                                        .HeightRequest(100)
                                        .WidthRequest(100)
                                        .FontSize(60)
                                        .Opacity(button.IsMatched ? 0 : 1)
                                        .WithAnimation()
                                        .OnClicked((button, _) => OnAnimalButtonClicked(button!, index))
                                        
                                )
                            )
                            .Wrap(FlexWrap.Wrap)
                            .MaximumWidthRequest(400)
                            .IsVisible(!State.GameWon)
                    )
                    .Spacing(25)
                    .Padding(20, 20)
            )
        );
    
    private void OnAnimalButtonClicked(object button, int buttonIndex)
    {
        State.Select(buttonIndex);
        AnimateButton((Button)button);
    }

    // ReSharper disable once AsyncVoidMethod
    private static async void AnimateButton(Button button)
    {
            await button.ScaleTo(0.8, 50, Easing.Linear);
            await button.ScaleTo(1, 500, Easing.SpringOut);
            
    }

    private void PlayAgainButton_OnClicked() => SetState(s => s.ResetGame());
    
}