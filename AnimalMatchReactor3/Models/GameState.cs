namespace AnimalMatchReactor.Models;

public class GameState
{
    public record AnimalButton(string Emoji, bool Selected);

    private readonly List<string> _animalEmojis =
    [
        "🦆", "🦆", "🦅", "🦅", "🐜", "🐜", "🦇", "🦇",
        "🦣", "🦣", "🐿", "🐿", "🐞", "🐞", "🐢", "🐢"
    ];

    private readonly Random _rng = new();
    private AnimalButton? _lastClicked;
    private int? _lastIndex = null;
    private bool _findingMatch;

    public List<AnimalButton> AnimalButtons { get; private set; } = [];
    public int MatchesFound { get; private set; }

    public GameState() => ResetGame();

    public void ResetGame()
    {
        var shuffledEmojis = _animalEmojis.OrderBy(_ => _rng.Next()).ToList();
        AnimalButtons = shuffledEmojis.Select(e => new AnimalButton(e, false)).ToList();
        MatchesFound = 0;
        _lastClicked = null;
        _lastIndex = null;
        _findingMatch = false;
    }

    public bool ClickButton(int index)
    {
        if (index < 0 || index >= AnimalButtons.Count)
            return false;

        var button = AnimalButtons[index];
        if (string.IsNullOrWhiteSpace(button.Emoji) || button.Selected)
            return false; // Ignore clicks on empty or already selected buttons

        if (!_findingMatch)
        {
            _lastClicked = button;
            _lastIndex = index;
            _findingMatch = true;
            AnimalButtons[index] = button with { Selected = true };
            return false;
        }

        bool isMatch = button.Emoji == _lastClicked!.Emoji && index != _lastIndex;
        if (isMatch)
        {
            AnimalButtons[index] = new AnimalButton(" ", false);
            AnimalButtons[(int)_lastIndex!] = new AnimalButton(" ", false);
            MatchesFound++;
        }
        else
        {
            AnimalButtons[index] = button with { Selected = false };
            AnimalButtons[(int)_lastIndex!] = _lastClicked with { Selected = false };
        }

        _findingMatch = false;
        _lastClicked = null;
        _lastIndex = null;

        return MatchesFound == _animalEmojis.Count / 2; // Return true if game is won
    }
}
