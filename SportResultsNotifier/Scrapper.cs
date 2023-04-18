using HtmlAgilityPack;
using SportResultsNotifier.Model;
using System.Threading.Tasks.Sources;

namespace SportResultsNotifier;

public static class Scrapper
{
    public static void Init()
    {
        const string html = "https://www.basketball-reference.com/boxscores/";
        HtmlWeb web = new();
        var htmlDoc = web.Load(html);

        var title = htmlDoc.DocumentNode.SelectSingleNode("//h1");
        Console.WriteLine(title.InnerText);

        var nGames = htmlDoc.DocumentNode.SelectSingleNode("//h2");
        Console.WriteLine(nGames.InnerText);

        var summaries = htmlDoc.DocumentNode.SelectSingleNode("//div[@class='game_summaries']");


        //List des games
        var individualSummaries = new List<HtmlNode>();
        for (int x = 1; x < summaries.ChildNodes.Count; x += 2)
        {
            individualSummaries.Add(summaries.ChildNodes[x]);
        }

        List<Game> games = new();

        foreach (var summary in individualSummaries)
        {
            var table1 = summary.ChildNodes[1].ChildNodes[1];
            var table2 = summary.ChildNodes[3].ChildNodes[3];
            var table3 = summary.ChildNodes[7];

            var pts = htmlDoc.DocumentNode.SelectSingleNode($"{table3.XPath}/tbody/tr[1]");
            var trb = htmlDoc.DocumentNode.SelectSingleNode($"{table3.XPath}/tbody/tr[2]");

            var aScoresNodes = htmlDoc.DocumentNode.SelectNodes($"{table2.XPath}/tr[1]/td[@class='center']");
            var bScoresNodes = htmlDoc.DocumentNode.SelectNodes($"{table2.XPath}/tr[2]/td[@class='center']");

            List<string> aScores = new();
            foreach (var score in aScoresNodes)
            {
                aScores.Add(score.InnerText);
            }

            List<string> bScores = new();
            foreach (var score in bScoresNodes)
            {
                bScores.Add(score.InnerText);
            }

            games.Add(new Game
            {
                TeamA = table1.ChildNodes[1].ChildNodes[1].InnerText,
                TeamAFinalScore = table1.ChildNodes[1].ChildNodes[3].InnerText,

                TeamB = table1.ChildNodes[3].ChildNodes[1].InnerText,
                TeamBFinalScore = table1.ChildNodes[3].ChildNodes[3].InnerText,
                TeamAScores = aScores,
                TeamBScores = bScores,


                Pts = ($"{pts.ChildNodes[1].InnerText} {pts.ChildNodes[3].InnerText} {pts.ChildNodes[5].InnerText}"),
                Trb = ($"{trb.ChildNodes[1].InnerText} {trb.ChildNodes[3].InnerText} {trb.ChildNodes[5].InnerText}")
            });
        }

        /*ar testGame = games[4];
        Console.WriteLine( testGame.TeamA );
        Console.WriteLine(testGame.TeamB);
        Console.WriteLine(testGame.TeamAFinalScore);
        Console.WriteLine(testGame.TeamBFinalScore);
        foreach(var score in testGame.TeamAScores)
        {
            Console.Write(" "+ score + " ");
        }
        foreach (var score in testGame.TeamBScores)
        {
            Console.Write(" " + score + " ");
        }
        Console.WriteLine(testGame.Pts);
        Console.WriteLine(testGame.Trb);
*/
        Console.WriteLine(games[0].GetGameAsMail());

    }
}