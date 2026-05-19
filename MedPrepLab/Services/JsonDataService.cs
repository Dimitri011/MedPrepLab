using System.Text.Json;
using System.Text.Json.Serialization;

namespace MedPrepLab.Services
{
    // Citire din JSON, transf in obiecte C#, sa salveze obiectele C# inapoi in JSON
    // Standard ahh shii, not that deep, dont bother
    public class JsonDataService
    {
        private readonly IWebHostEnvironment _environment;

        public JsonDataService(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        
        public List<T> ReadListFromJson<T>(string fileName)
        {
            try
            {
                string filePath = Path.Combine(_environment.ContentRootPath, "Data", fileName);

                if (!File.Exists(filePath))
                {
                    return new List<T>();
                }

                string jsonContent = File.ReadAllText(filePath);

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                
                options.Converters.Add(new JsonStringEnumConverter());

                return JsonSerializer.Deserialize<List<T>>(jsonContent, options) ?? new List<T>();
            }
            catch
            {
                
                return new List<T>();
            }
        }

        public void WriteListToJson<T>(string fileName, List<T> data)
        {
            try
            {
                string filePath = Path.Combine(_environment.ContentRootPath, "Data", fileName);

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };

               
                options.Converters.Add(new JsonStringEnumConverter());

                string jsonContent = JsonSerializer.Serialize(data, options);

                File.WriteAllText(filePath, jsonContent);
            }
            catch
            {
                
            }
        }
    }
}