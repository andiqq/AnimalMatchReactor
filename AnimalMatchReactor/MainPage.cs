using Microsoft.Maui.Layouts;

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
                                        .OnClicked(async (sender, _) =>
                                        {
                                            ButtonClicked(index);
                                            var tappedButton = (VisualElement)sender!;
                                            await tappedButton.ScaleTo(0.8, 50, Easing.Linear);
                                            await tappedButton.ScaleTo(1, 500, Easing.SpringOut);
                                        })
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

    private void PlayAgainButton_OnClicked() => SetState(s => s.ResetGame());

    private void ButtonClicked(int index)
    {
        State.Select(index);
    }
}