using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Coffee_Machine_Application.Utilities
{
    public static class JsonFileHandler<T>
    {
        private static readonly string BaseDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");
        public static async Task CreateFile(string filePath)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(filePath));

            if (!File.Exists(filePath))
            {
                File.WriteAllText(filePath, "[]");
            }
        }

        public static List<T> ReadData(string filePath)
        {
            if (!File.Exists(filePath))
            {
                File.Create("{}");
                return new List<T>();
            }

            string json = File.ReadAllText(filePath);

            return JsonSerializer.Deserialize<List<T>>(json) ?? new List<T>();
        }

        public static void WriteData(string filePath, List<T> list)
        {
            string json = JsonSerializer.Serialize(list, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
        }
    }
}
