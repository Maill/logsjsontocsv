using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace logsJSONtoCSV
{
    class Parameters
    {
        private static Parameters instance;

        public String srcLogsPath;

        public String outPath;

        public static Parameters Instance()
        {
            if(instance == null)
            {
                instance = new Parameters();
                return instance;
            }
            return instance;
        }

        public void ParseArguments(String[] args)
        {
            if (args.Count() == 0 || args.Count() == 1)
            {
                writeHelp();
                return;
            }

            this.srcLogsPath = args[0];
            this.outPath = args[1];

        }

        public void writeHelp()
        {
            Console.WriteLine("Using : jsonlogstocsv.exe json_path out_file_path");
            Console.WriteLine(@"Exemple : jsonlgstosv.exe C:\data\json\logs.json C:\data\out\json\");
        }
    }
}
