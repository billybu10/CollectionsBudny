using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionsBudny.Models
{
    public class Item
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public string State { get; set; }
        public int Rating { get; set; }
        public string Image { get; set; }
        public string Collection { get; set; }
        public string Comment { get; set; }

        public Item()
        {
            ID = Guid.NewGuid().ToString("N");
            Name = string.Empty;
            Price = 0;
            State = string.Empty;
            Rating = 0;
            Image = string.Empty;
            Collection = string.Empty;
            Comment = string.Empty;
        }

        public void Save()
        {
            if (!File.Exists(Path.Combine(FileSystem.AppDataDirectory, Collection + ".items.txt")))
            {
                FileStream newFileFilestream = File.Create(Path.Combine(FileSystem.AppDataDirectory, Collection + ".items.txt"));
                newFileFilestream.Close();
            }

            string tempDataString = ID + ";" + Name + ";" + Price.ToString() + ";" + State + ";" + Rating.ToString() + ";" + Image + ";" + Collection + ";" + Comment + ";\n";

            var lines = File.ReadLines(Path.Combine(FileSystem.AppDataDirectory, Collection + ".items.txt"));
            if (lines.Any(x => x.Split(",")[0] == ID))
            {
                string oldline = lines.Where(x => x.Split(",")[0] == ID).Single();
                string tempFileContent = File.ReadAllText(Path.Combine(FileSystem.AppDataDirectory, Collection + ".items.txt"));
                tempFileContent = tempFileContent.Replace(oldline, tempDataString);
                File.WriteAllText(Path.Combine(FileSystem.AppDataDirectory, Collection + ".items.txt"), tempFileContent);
            }
            else
            {
                File.AppendAllText(Path.Combine(FileSystem.AppDataDirectory, Collection + ".items.txt"), tempDataString);
            }
        }

        public void Delete()
        {
            string tempDataString = ID + ";" + Name + ";" + Price.ToString() + ";" + State + ";" + Rating.ToString() + ";" + Image + ";" + Collection + ";" + Comment + ";\n";
            string tempFileContent = File.ReadAllText(Path.Combine(FileSystem.AppDataDirectory, Collection + ".items.txt"));
            tempFileContent = tempFileContent.Replace(tempDataString, "");
            File.WriteAllText(Path.Combine(FileSystem.AppDataDirectory, Collection + ".items.txt"), tempFileContent);
        }

        public static Item Load(string ID)
        {
            string appDataPath = FileSystem.AppDataDirectory;
            var files = Directory.EnumerateFiles(appDataPath, "*.items.txt");
            foreach (var file in files)
            {
                var lines = File.ReadLines(file);
                if (lines.Any(x => x.Split(";")[0] == ID))
                {
                    string line = lines.Where(x => x.Split(";")[0] == ID).Single();
                    string[] fields = line.Split(";");
                    return
                        new()
                        {
                            ID = ID,
                            Name = fields[1],
                            Price = (float)Double.Parse(fields[2]),
                            State = fields[3],
                            Rating = Int32.Parse(fields[4]),
                            Image = fields[5],
                            Collection = fields[6],
                            Comment = fields[7],
                        };
                }
            }

            throw new FileNotFoundException("Unable to find item in local storage.");


        }

        public static IEnumerable<Item> LoadAll()
        {
            string appDataPath = FileSystem.AppDataDirectory;
            var items = new List<Item>();

            foreach (var filename in Directory.EnumerateFiles(appDataPath, "*.items.txt"))
            {
                var lines = File.ReadLines(filename);
                foreach (var line in lines)
                {
                    string[] fields = line.Split(";");
                    if (fields.Length > 2)
                    {
                        items.Add(
                        new Item()
                        {
                            ID = fields[0],
                            Name = fields[1],
                            Price = (float)Double.Parse(fields[2]),
                            State = fields[3],
                            Rating = Int32.Parse(fields[4]),
                            Image = fields[5],
                            Collection = fields[6],
                            Comment = fields[7],
                        });
                    }
                    else
                    {
                        continue;
                    }

                }
            }

            return items.OrderBy(item => item.State == "Sold").ThenBy(item => item.Name);
        }


    }
}
