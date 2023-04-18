namespace SportResultsNotifier.Model;

public class Game
{
    public string TeamA;
    public string TeamB;

    public string TeamAFinalScore;
    public string TeamBFinalScore;

    public List<string> TeamAScores;
    public List<string> TeamBScores;

    public string Pts;
    public string Trb;

    public string GetGameAsMail()
    {
        string mail =
            @$"
{TeamA}: {TeamAFinalScore}
{TeamB}: {TeamBFinalScore}

{TeamA}: | Q1:{TeamAScores[0]} | Q2:{TeamAScores[1]} | Q3:{TeamAScores[2]} | Q4:{TeamAScores[3]} |
{TeamB}: | Q1:{TeamBScores[0]} | Q2:{TeamBScores[1]} | Q3:{TeamBScores[2]} | Q4:{TeamBScores[3]} |

{Pts}
{Trb}
";
        return mail;
    }
}
