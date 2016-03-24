using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoldenEggVine.Labels
{
    public static class CLineProcessor
    {
        public static string[] ProcessLine(string line)
        {
            List<string> elements = new List<string>();
            StringBuilder sb = new StringBuilder();
            bool reading = false;
            for(int i = 0; i < line.Length; i++)
            {
                if (line[i] == '\"')
                {
                    reading = !reading;
                    if (reading)
                    {
                        sb = new StringBuilder();
                    }
                    else
                    {
                        elements.Add(sb.ToString());
                    }
                }
                else if(reading)
                {
                    sb.Append(line[i]);
                }
            }
            if(reading)
            {
                throw new Exception("Invalid Line for reading detected:\n" + line);
            }
            return elements.ToArray();
        }
    }
}