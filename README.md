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
- key="SenderEmail" value="<Email sending the mail>"
- key="SenderPassword" value="<Password of the email sending the mail>" 
- key="ReceiverEmail" value="<Email receiver the mail>" 
  
These two are dependent of each other, these are my default:
- key="SmtpAddress" value="smtp.office365.com" 
- key="PortNumber" value="587" 

# Resources
- Microsoft documentation
- [Agility Pack Doc](https://html-agility-pack.net/documentation)
- [The C# Academy](https://www.thecsharpacademy.com/)
- StackOverflow
