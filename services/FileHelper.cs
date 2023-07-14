using System.Text.Encodings.Web;
using System.Text.Json;

namespace Project.WebApi;

public class FileHelper
{
    private static string pathToJSON = "Resources/articles.json";
    public static IList<Article> GetJson()
    {
        try
        {
            string jsonString = File.ReadAllText(pathToJSON);

            if (!string.IsNullOrWhiteSpace(jsonString))
            {
                var myObjects = JsonSerializer.Deserialize<Article[]>(jsonString);
                return myObjects.ToList<Article>();
            }

            return null;
        }
        catch (JsonException ex)
        {
            throw new Exception("Ошибка десериализации JSON", ex);
        }
        catch (IOException ex)
        {
            throw new Exception("Ошибка чтения из файла", ex);
        }
    }

    public static bool updateJson(IList<Article> chaingedArticles)
    {
        try
        {
            string jsonString = JsonSerializer.Serialize(chaingedArticles, new JsonSerializerOptions {Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping});
            File.WriteAllText(pathToJSON, jsonString);
            return true;
        }
        catch (JsonException ex)
        {
            throw new Exception("Ошибка сериализации JSON", ex);
        }
        catch (IOException ex)
        {
            throw new Exception("Ошибка записи файла", ex);
        } 
    }
}