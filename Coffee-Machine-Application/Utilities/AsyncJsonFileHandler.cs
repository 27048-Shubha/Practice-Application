using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Coffee_Machine_Application.Utilities
{
    public static class AsyncJsonFileHandler<T>
    {
        private static readonly string BaseDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");
        
        private static string GetFilePath(string fileName)
        {
            Directory.CreateDirectory(BaseDirectory);
            return Path.Combine(BaseDirectory, fileName);
        }

        public static async Task CreateFile(string fileName)
        {
            string filePath = GetFilePath(fileName);
            if (!File.Exists(filePath))
            {
                await File.WriteAllTextAsync(filePath, "[]");
            }
        }

        public static async Task<List<T>> ReadData(string fileName)
        {
            string filePath = GetFilePath(fileName);

            await CreateFile(filePath);

            string json = await File.ReadAllTextAsync(filePath);

            return JsonSerializer.Deserialize<List<T>>(json) ?? new List<T>();
        }

        public static async Task WriteData(string fileName, List<T> list)
        {
            await CreateFile(fileName);
            string filePath = GetFilePath(fileName);
            string json = JsonSerializer.Serialize(list, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(filePath, json);
        }
    }
}
