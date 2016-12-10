## Synopsis

This is an automation project to generate [Hubstaff](http://www.hubstaff.com/) reports for multiple user very quickly. It uses [Hubstaff's APIs](https://developer.hubstaff.com/docs/api) to get reports as json, prepares a PDF document for each user and a consolidated excel report. Finally the pdf reports  and the excel sheet are sent to intended recipient as compressed attachment.


## Running instructions

This is console application. It needs to be run with two parameters -s 2016-10-01 -e 2016-10-15.
- -s  Start date in yyyy-MM-dd format
- -e  End date in yyyy-MM-dd format

Please see installation instructions below for settting up the project.

## Motivation

My employer uses Hubstaff for work hours tracking. And every week I needed to download pdf reports and prepare excel report for each of the 5-6 members team. This usually takes an hour to complete. This is reduced to less than a minute with the automation.

## Installation

The project uses binaries from following projects:
- 7za.exe which can be downloaded from [here](http://www.7-zip.org/download.html).
- wkhtmltopdf.exe which can be downloaded from [here](http://www.7-zip.org/download.html).

After downloading please copy both executables in AppData folder.

AppData folder contains various files for the required data for running the appilcation in "Confidential" folder. These filess can be exmained for required format for username and passwords etc. The folder also contains a hubstaff.config files which is used to store sensitive information such as email and passwords. [Click here for more info](https://www.asp.net/identity/overview/features-api/best-practices-for-deploying-passwords-and-other-sensitive-data-to-aspnet-and-azure).


## License

This project is licensed under the MIT License - see the [LICENSE.txt](LICENSE.txt) file for details.

## Acknowledgments
Thanks to the developers behind the following nuget packages
which  have been used in the application.
-ClosedXML
-CommandLineParser
-CsvHelper
-DocumentFormat.OpenXml
-HtmlAgilityPack
-Newtonsoft.Json
-RestSharp

