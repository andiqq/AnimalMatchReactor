namespace AnimalMatchReactor;

public class Game
{
    public Game() => ResetGame();
    public int TimeElapsed { get; set; }
    public record AnimalButton(string Emoji, bool Selected, bool IsMatched);
    public List<AnimalButton> AnimalButtons { get; private set; } = [];
    public bool GameWon { get; private set; } = true;

    private readonly List<string> _animalEmojis =
    [
        "🦆", "🦅", "🐜", "🦇", "🦣", "🐿", "🐞",  "🐢" 
    ];

    private readonly Random _random = new();
    private AnimalButton? _lastClicked;
    private int _lastIndex;
    private bool _findingMatch;
    private int _matchesFound;
    
    public void ResetGame()
    {
        AnimalButtons = _animalEmojis
            .SelectMany(emoji => new[] { emoji, emoji })        // make Emoji pairs
            .OrderBy(_ => _random.Next())                       // shuffle them
            .Select(e => new AnimalButton(e, false, false))     // put them in AnimalButton records
            .ToList();
        _matchesFound = 0;
        _findingMatch = false;
        GameWon = false;
        TimeElapsed = 0;
    }

    public void Select(int index)
    {
        if (index < 0 || index >= AnimalButtons.Count) return;

        var button = AnimalButtons[index];
        
        if (!_findingMatch)
        {
            _lastClicked = button;
            _lastIndex = index;
            _findingMatch = true;
            AnimalButtons[index] = button with { Selected = true };
            return;
        }

        var isMatch = button.Emoji == _lastClicked!.Emoji && index != _lastIndex;
        
        if (isMatch)
        {
            AnimalButtons[index] = button with { IsMatched = true };
            AnimalButtons[_lastIndex] = _lastClicked! with { IsMatched = true };
            _matchesFound++;
        }
        else
        {
            AnimalButtons[index] = button with { Selected = false };
            AnimalButtons[_lastIndex] = _lastClicked with { Selected = false };
        }

        _findingMatch = false;
        
        if (_matchesFound == _animalEmojis.Count)
        {
            GameWon = true;
        }
    }
}
