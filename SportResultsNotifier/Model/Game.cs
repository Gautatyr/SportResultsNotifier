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
------------------------------------------
{TeamA}: {TeamAFinalScore}
{TeamB}: {TeamBFinalScore}

{TeamA}: | {TeamAScores[0]} | {TeamAScores[1]} | {TeamAScores[2]} | {TeamAScores[3]} |
{TeamB}: | {TeamBScores[0]} | {TeamBScores[1]} | {TeamBScores[2]} | {TeamBScores[3]} |

PTS:  {Pts}
TRB:  {Trb}
------------------------------------------
";
        return mail;
    }
}