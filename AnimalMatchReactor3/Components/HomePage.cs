using Microsoft.Maui.Layouts;
using System.Collections.ObjectModel;

namespace MauiReactor.Startup.Components
{
    public class HomePageState
    {
        public bool IsVisibleNewGameButton { get; set; }
        public bool IsVisibleAnimalButtons { get; set; }
    }

    public class HomePage : Component<HomePageState>
    {
        private readonly List<string> _animalEmojis =
        [
            "🦆", "🦆",
            "🦅", "🦅",
            "🐜", "🐜",
            "🦇", "🦇",
            "🦣", "🦣",
            "🐿", "🐿",
            "🐞", "🐞",
            "🐢", "🐢"
        ];

        private List<string>? _emojis;

        protected override void OnMounted()
        {
            State.IsVisibleNewGameButton = true;
            State.IsVisibleAnimalButtons = false;
            _emojis = new List<string>(_animalEmojis);
            base.OnMounted();
        }
        
        Microsoft.Maui.Controls.Button lastClicked;
        bool findingMatch = false;
        int matchesFound = 0;

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
                                        Button(SetEmoji())
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

        private string SetEmoji()
        {
            var index = Random.Shared.Next(_emojis.Count);
            var nextEmoji = _emojis[index];
            _emojis.RemoveAt(index);
            return nextEmoji;
        }

        private void PlayAgainButton_OnClicked()
        {
            _emojis = new List<string>(_animalEmojis); 
            SetState(s => s.IsVisibleAnimalButtons = true);
            SetState(s => s.IsVisibleNewGameButton = false);
            Invalidate();
        }
        
        private void ButtonClicked(object? sender, EventArgs e)
        {
            if (sender is Microsoft.Maui.Controls.Button button)
            {
                if (!string.IsNullOrWhiteSpace(button.Text) && findingMatch == false)
                {
                    button.BackgroundColor = Colors.Red;
                    lastClicked = button;
                    findingMatch = true;
                }
                else
                {
                    if (button != lastClicked && button.Text == lastClicked.Text)
                    {
                        matchesFound++;
                        lastClicked.Text = " ";
                        button.Text = " ";
                    }

                    lastClicked.BackgroundColor = Colors.LightBlue;
                    button.BackgroundColor = Colors.LightBlue;
                    findingMatch = false;
                }
            }

            if (matchesFound == 8)
            {
                _emojis = new List<string>(_animalEmojis);
                    matchesFound = 0;
                    SetState(s => s.IsVisibleAnimalButtons = false);
                    SetState(s => s.IsVisibleNewGameButton = true);
                    Invalidate();
            }
        }
        
    }
}
