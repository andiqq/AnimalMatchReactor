using AnimalMatchReactor.Models;
using Microsoft.Maui.Layouts;

namespace AnimalMatchReactor.Components;

public class HomePageState
{
    public bool IsVisibleNewGameButton { get; set; } = true;
    public bool IsVisibleAnimalButtons { get; set; }
    public int TimeElapsed { get; set; }
    public bool Toggle { get; set; }
}

public class HomePage : Component<HomePageState>
{
    private readonly GameState _gameState = new();

    public override VisualNode Render()
        => ContentPage(
            ScrollView(
                VStack(
                        Timer()
                            .Interval(100)
                            .IsEnabled(!State.IsVisibleNewGameButton)
                            .OnTick(_ => SetState(s => s.TimeElapsed++)),
                        Label($"Time elapsed: {State.TimeElapsed / 10f:0.0}s")
                            .FontSize(24),
                        Button("Play once again?")
                            .OnClicked(PlayAgainButton_OnClicked)
                            .IsVisible(State.IsVisibleNewGameButton)
                            .FontSize(24),
                        FlexLayout(
                                _gameState.AnimalButtons.Select((button, index) =>
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
                            .IsVisible(State.IsVisibleAnimalButtons)
                    )
                    .Spacing(25)
                    .Padding(20, 20)
            )
        );

    private void PlayAgainButton_OnClicked()
    {
        _gameState.ResetGame();
        SetState(s =>
        {
            s.IsVisibleAnimalButtons = true;
            s.IsVisibleNewGameButton = false;
            s.TimeElapsed = 0;
        });
    }

    private void ButtonClicked(int index)
    {
        var gameWon = _gameState.ClickButton(index);
        if (!gameWon) return;
        SetState(s =>
        {
            s.IsVisibleAnimalButtons = false;
            s.IsVisibleNewGameButton = true;
        });
    }
}