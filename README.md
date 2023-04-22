# SportResultsNotifier

Windows service scrapping basketball results from [Basketball-Reference](https://www.basketball-reference.com/boxscores/),
and sending the results of the day inside an email.

# Features
## Windows Service
- The application runs as a service in the background, sending a mail once a day

## Web Scrapping
- The application uses Agility Pack to scrap the data from the website

# How to Use
To use this application you will need to create an App.Config file and set those variables:
- key="SenderEmail" value="SENDER S EMAIL"
- key="SenderPassword" value="SENDER S EMAIL PASSWORD" 
- key="ReceiverEmail" value="RECEIVER S EMAIL" 
- key="SmtpAddress" value="smtp.office365.com" 
- key="PortNumber" value="587" 

# Resources
- Microsoft documentation
- [Agility Pack Doc](https://html-agility-pack.net/documentation)
- [The C# Academy](https://www.thecsharpacademy.com/)
- StackOverflow
