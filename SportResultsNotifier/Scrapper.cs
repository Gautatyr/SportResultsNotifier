using HtmlAgilityPack;

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

        Console.WriteLine(individualSummaries.First().ChildNodes.Count);
    }
}