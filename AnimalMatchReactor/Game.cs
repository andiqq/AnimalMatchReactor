namespace AnimalMatchReactor;

public class Game
{
    public int TimeElapsed { get; set; }
    public record AnimalButton(string Emoji, bool Selected, bool IsMatched);
    public List<AnimalButton> AnimalButtons { get; private set; } = [];
    public bool GameWon { get; private set; } = true;

    private readonly List<string> animalEmojis =
    [
        "🦆", "🦅", "🐜", "🦇", "🦣", "🐿", "🐞",  "🐢" 
    ];

    private readonly Random random = new();
    private int? lastClicked;
    private int matchesFound;
    
    public void ResetGame()
    {
        AnimalButtons = animalEmojis
            .SelectMany(emoji => new[] { emoji, emoji })        // make Emoji pairs
            .OrderBy(_ => random.Next())                        // shuffle them
            .Select(e => new AnimalButton(e, false, false))     // put them in AnimalButton records
            .ToList();
        matchesFound = 0;
        lastClicked = null;
        GameWon = false;
        TimeElapsed = 0;
    }

    public void ButtonClicked(int index)
    {
       if (lastClicked is null)
       {
           lastClicked = index;
           AnimalButtons[index] = AnimalButtons[index] with { Selected = true };
           return;
       }
 
       if (AnimalButtons[index].Emoji == AnimalButtons[(int)lastClicked].Emoji && index != lastClicked)
       {
           AnimalButtons[index] = AnimalButtons[index] with { IsMatched = true };
           AnimalButtons[(int)lastClicked] = AnimalButtons[(int)lastClicked] with { IsMatched = true };
           matchesFound++;
       }
       else
       {
           AnimalButtons[index] = AnimalButtons[index] with { Selected = false };
           AnimalButtons[(int)lastClicked] = AnimalButtons[(int)lastClicked] with { Selected = false };
       }

       lastClicked = null;
        
       if (matchesFound == animalEmojis.Count) GameWon = true;
    }
}
