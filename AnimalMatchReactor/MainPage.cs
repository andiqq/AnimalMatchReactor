using Microsoft.Maui.Layouts;
using MauiControls = Microsoft.Maui.Controls;

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
                            .OnClicked(PlayAgainButtonClicked)
                            .IsVisible(State.GameWon)
                            .FontSize(24),
                        FlexLayout(
                                State.AnimalButtons.Select((button, index) =>
                                    GameButton(button)
                                        .OnClicked((clickedButton, _) =>
                                        {
                                            State.ButtonClicked(index);
                                            AnimateButton((MauiControls.Button)clickedButton!);
                                        })
                                )
                            )
                            .Wrap(FlexWrap.Wrap)
                            .MaximumWidthRequest(400)
                            .IsVisible(!State.GameWon)
                    )
                    .Spacing(25)
                    .Padding(15, 10)
            )
        );

    private static MauiReactor.Button GameButton(Game.AnimalButton button)
        => Button(button.Emoji)
            .BackgroundColor(button.Selected ? Colors.Purple : Colors.LightBlue)
            .BorderColor(Colors.Black)
            .BorderWidth(1)
            .HeightRequest(90)
            .WidthRequest(90)
            .FontSize(56)
            .Opacity(button.IsMatched ? 0 : 1)
            .WithAnimation();

    // ReSharper disable once AsyncVoidMethod
    private static async void AnimateButton(MauiControls.Button button)
    {
        await button.ScaleTo(0.8, 50, Easing.Linear);
        await button.ScaleTo(1, 500, Easing.SpringOut);
    }

    private void PlayAgainButtonClicked() => SetState(s => s.ResetGame());
}