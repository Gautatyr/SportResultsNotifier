# SportResultsNotifier

Windows service scrapping basketball results from [Basketball-Reference](https://www.basketball-reference.com/boxscores/),
and sending the results of the day inside an email.

# Features
## Windows Service
- The application runs as a service in the background, sending a mail once a day

## Web Scrapping
- The application uses Agility Pack to scrap the data from the website

# How to Use
To use this application you will need to modify the mailer class:
- Change the stmpAddress (ex: smtp.office365.com)
- Change the port number (ex: 587 for office)
- Change the sender and receiver email as you see fit, and the password for the sender

# Resources
- Microsoft documentation
- [Agility Pack Doc](https://html-agility-pack.net/documentation)
- [The C# Academy](https://www.thecsharpacademy.com/)
- StackOverflow
