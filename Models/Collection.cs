using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionsBudny.Models
{
    public class Collection
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public Collection() 
        {
            ID = Guid.NewGuid().ToString("N");
            Name = string.Empty;
            Description = string.Empty;
        }

        public void Save()
        {
            string tempDataString = Name + "\n" + Description;
            File.WriteAllText(Path.Combine(FileSystem.AppDataDirectory, ID + ".collection.txt"), tempDataString);

        }

        public void Delete()
        {
            File.Delete(Path.Combine(FileSystem.AppDataDirectory, ID + ".collection.txt"));
            if (File.Exists(Path.Combine(FileSystem.AppDataDirectory, ID + ".items.txt")))
                File.Delete(Path.Combine(FileSystem.AppDataDirectory, ID + ".items.txt"));


        }

        public static Collection Load(string ID)
        {
            string filename = Path.Combine(FileSystem.AppDataDirectory, ID + ".collection.txt");

            if (!File.Exists(filename))
                throw new FileNotFoundException("Unable to find file on local storage.", filename);

            return
                new()
                {
                    ID = ID,
                    Name = File.ReadLines(filename).First(),
                    Description = File.ReadLines(filename).Skip(1).Aggregate("", (current, line) => current + line + "\n")
                };
        }

        public static Collection LoadFromFilename(string filename)
        {
            filename = Path.Combine(FileSystem.AppDataDirectory, filename);

            if (!File.Exists(filename))
                throw new FileNotFoundException("Unable to find file on local storage.", filename);

            return
                new()
                {
                    ID = Path.GetFileNameWithoutExtension(filename).Split(".").First(),
                    Name = File.ReadLines(filename).First(),
                    Description = File.ReadLines(filename).Skip(1).Aggregate("", (current, line) => current + line + "\n")
                };
        }

        public static IEnumerable<Collection> LoadAll()
        {
            string appDataPath = FileSystem.AppDataDirectory;

            return Directory

                    .EnumerateFiles(appDataPath, "*.collection.txt")

                    .Select(filename => LoadFromFilename(Path.GetFileName(filename)))

                    .OrderByDescending(collection => collection.Name);

        }
    }
}
