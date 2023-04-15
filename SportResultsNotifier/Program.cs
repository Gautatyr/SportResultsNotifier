using SportResultsNotifier;

Mailer mailer = new();
Console.WriteLine("Sending");
mailer.SendEmail();
Console.WriteLine("Sent");

Console.ReadLine();