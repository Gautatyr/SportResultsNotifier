﻿using HtmlAgilityPack;
using SportResultsNotifier.Model;

namespace SportResultsNotifier;

public class Scrapper
{
    public void ScrapNSendGames()
    {
        const string WebsiteUrl = "https://www.basketball-reference.com/boxscores/";
        HtmlWeb web = new();
        var htmlDoc = web.Load(WebsiteUrl);

        string subject = $"Basketball report {DateTime.Now.Date}";
        string body = MailHeader(htmlDoc) + GamesOfTheDay(htmlDoc);

        Mailer mailer = new();
        mailer.SendEmail(subject, body);
    }

    private string MailHeader(HtmlDocument htmlDoc)
    {
        var title = htmlDoc.DocumentNode.SelectSingleNode("//h1").InnerText;
        var numberOfGames = htmlDoc.DocumentNode.SelectSingleNode("//h2").InnerText;
        return $"{title}\n{numberOfGames}:";
    }

    private string GamesOfTheDay(HtmlDocument htmlDoc)
    {
        var gameSummaries = htmlDoc.DocumentNode.SelectSingleNode("//div[@class='game_summaries']");

        var individualSummaries = new List<HtmlNode>();
        for (int x = 1; x < gameSummaries.ChildNodes.Count; x += 2)
        {
            individualSummaries.Add(gameSummaries.ChildNodes[x]);
        }

        List<Game> games = new();

        foreach (var summary in individualSummaries)
        {
            var table1 = summary.ChildNodes[1].ChildNodes[1];
            var table2 = summary.ChildNodes[3].ChildNodes[3];
            var table3 = summary.ChildNodes[7];

            var pts = htmlDoc.DocumentNode.SelectSingleNode($"{table3.XPath}/tbody/tr[1]");
            var trb = htmlDoc.DocumentNode.SelectSingleNode($"{table3.XPath}/tbody/tr[2]");

            var teamAScoreNodes = htmlDoc.DocumentNode.SelectNodes($"{table2.XPath}/tr[1]/td[@class='center']");
            var teamBScoreNodes = htmlDoc.DocumentNode.SelectNodes($"{table2.XPath}/tr[2]/td[@class='center']");

            List<string> teamAScores = new();
            foreach (var score in teamAScoreNodes)
            {
                teamAScores.Add(score.InnerText);
            }

            List<string> teamBScores = new();
            foreach (var score in teamBScoreNodes)
            {
                teamBScores.Add(score.InnerText);
            }

            games.Add(new Game
            {
                TeamA = table1.ChildNodes[1].ChildNodes[1].InnerText,
                TeamAFinalScore = table1.ChildNodes[1].ChildNodes[3].InnerText,

                TeamB = table1.ChildNodes[3].ChildNodes[1].InnerText,
                TeamBFinalScore = table1.ChildNodes[3].ChildNodes[3].InnerText,
                TeamAScores = teamAScores,
                TeamBScores = teamBScores,

                Pts = ($"{pts.ChildNodes[3].InnerText} {pts.ChildNodes[5].InnerText}"),
                Trb = ($"{trb.ChildNodes[3].InnerText} {trb.ChildNodes[5].InnerText}")
            });
        }

        string mailMainBody = "";

        foreach (var game in games)
        {
            mailMainBody = $"{mailMainBody}\n{game.GetGameAsMail()}";
        }

        return mailMainBody;
    }
}