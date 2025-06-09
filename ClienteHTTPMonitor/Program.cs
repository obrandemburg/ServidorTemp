using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using ClienteHTTPMonitor;
using System.Diagnostics; // Para Stopwatch

class ClienteHttp
{
    static async Task Main()
    {
        using var cliente = new HttpClient();
        string url = "http://localhost:5098/temperatura/celsius";
        List<Temperatura> temperaturas = new();

        var cronometro = Stopwatch.StartNew(); // começa a contar o tempo

        while (cronometro.Elapsed < TimeSpan.FromMinutes(1))
        {
            var resposta = await cliente.GetStringAsync(url);
            Temperatura temp = JsonSerializer.Deserialize<Temperatura>(resposta);

            temperaturas.Add(temp);

            Console.WriteLine($"Capturada: {temp}");

            await Task.Delay(1000); // espera 1 segundo entre as leituras
        }

        Console.WriteLine("\nLeituras finalizadas. Temperaturas capturadas:");
        foreach (Temperatura temp in temperaturas)
        {
            Console.WriteLine(temp);
        }
    }
}
