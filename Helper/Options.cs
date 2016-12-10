using System;
using CommandLine;
using CommandLine.Text;

namespace HubstaffReportGenerator.Helper
{
    public class Options
    {
        [Option('s', "start", Required = true,
          HelpText = "Start Date")]
        public DateTime StartDate { get; set; }

        [Option('e', "end", Required = true,
          HelpText = "Start Date")]
        public DateTime EndDate { get; set; }
        [HelpOption]
        public string GetUsage()
        {
            return HelpText.AutoBuild(this,
              (HelpText current) => HelpText.DefaultParsingErrorsHandler(this, current));
        }
    }
}
