using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ExcuseManager
{
    class Excuse
    {
        public string Description { get; set; }
        public string Results { get; set; }
        public DateTime LastUsed { get; set; }
        public string ExcusePath { get; set; }

        public Excuse()
        {
            ExcusePath = "";
        }

        public Excuse(string filename)
        {
            Open(filename);
        }

        public Excuse(Random random, string folderToUse)
        {
            string[] files = Directory.GetFiles(folderToUse, "*.txt");
            Open(files[random.Next(files.Length)]);
        }

        public void Open(string filename)
        {
            this.ExcusePath = filename;
            using (StreamReader reader = new StreamReader(filename))
            {
                Description = reader.ReadLine();
                Results = reader.ReadLine();
                LastUsed = Convert.ToDateTime(reader.ReadLine());
            }
        }

        public void Save(string filename)
        {
            using (StreamWriter writer = new StreamWriter(filename))
            {
                writer.WriteLine(Description);
                writer.WriteLine(Results);
                writer.WriteLine(LastUsed);
            }
        }

        
    }
}
