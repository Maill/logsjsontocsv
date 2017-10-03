using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace logsJSONtoCSV
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Parameters param = Parameters.Instance();
                param.ParseArguments(args);

                if(param.srcLogsPath == null && param.outPath == null)
                {
                    Console.WriteLine("Invalid parameters.");
                }

                String contentFile = File.ReadAllText(param.srcLogsPath);

                JObject JsonToObject = JObject.Parse(contentFile);
                JArray arrayEntries = JArray.Parse(JsonToObject.Last.Last.ToString());

                StringBuilder toWrite = new StringBuilder();

                foreach (JObject line in arrayEntries)
                {
                    toWrite.Append(line.GetValue("code").ToString() + ";");
                    toWrite.Append(line.GetValue("host").ToString() + ";");
                    toWrite.Append(line.GetValue("path").ToString() + ";");
                    toWrite.Append(line.GetValue("durationMs").ToString());
                    toWrite.Append('\n');
                }

                string fileName = Path.GetFileNameWithoutExtension(param.srcLogsPath);
                StreamWriter fileToWrite = File.CreateText(param.outPath + fileName + "-" + DateTime.Now.ToString("yyyyMMdd-HHMM") + ".csv");
                fileToWrite.Write(toWrite.ToString());
                fileToWrite.Flush();
                fileToWrite.Dispose();
            } catch (Exception exp)
            {
                Console.WriteLine("An error occured.\nMessage : " + exp.Message + "\nStrackTrace : " + exp.StackTrace);
            }
            
        }
    }
}
