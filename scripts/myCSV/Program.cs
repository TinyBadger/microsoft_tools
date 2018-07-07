using System;
using System.IO;
using System.Collections.Generic;
using CommandLine;
using CommandLine.Text;

namespace myCSV
{
    class Options
    {
        [Option('r', "read", Required = true, HelpText = "Input files to be processed.")]
        public IEnumerable<string> InputFiles { get; set; }

        // Omitting long name, defaults to name of property, ie "--verbose"
        [Option(Default = false, HelpText = "Prints all messages to standard output.")]
        public bool Verbose { get; set; }

        [Option("stdin", Default = false, HelpText = "Read from stdin")]
        public bool stdin { get; set; }

        [Value(0, MetaName = "offset", HelpText = "File offset.")]
        public long? Offset { get; set; }
    }
    class Program
    {
        public List<string> loadCsvFile(string filePath)
        {
            var reader = new StreamReader(File.OpenRead(filePath));
            List<string> searchList = new List<string>();
            while(!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                searchList.Add(line);
            }
            return searchList;
        }
        static void Main(string[] args)
        {
              CommandLine.Parser.Default.ParseArguments<Options>(args)
             .WithParsed<Options>(opts => RunOptionsAndReturnExitCode(opts))
             .WithNotParsed<Options>((errs) => HandleParseError(errs));
        }
    }
}
