using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using ClienteHTTPMonitor;
using System.Diagnostics;

class ClienteHttp
{
    static async Task Main()
    {
        using var cliente = new HttpClient();
        string url = "http://localhost:5098/temperatura/celsius";
        Temperatura[] temperaturas = new Temperatura[2];
        temperaturas[0] = null;

        var cronometro = Stopwatch.StartNew();

        while (cronometro.Elapsed < TimeSpan.FromMinutes(1))
        {
            var resposta = await cliente.GetStringAsync(url);
            Temperatura temp = JsonSerializer.Deserialize<Temperatura>(resposta);

            if (temperaturas[0] == null)
            {
                temperaturas[0] = temp;
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Primeira temperatura: {temp.valor}°C");
            }
            else
            {
                // Exibe hora e temperatura na mesma linha
                Console.Write($"[{DateTime.Now:HH:mm:ss}] {temp.valor}°C ");

                if (temp.valor < temperaturas[0].valor)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write("Diminuiu");
                }
                else if (temp.valor > temperaturas[0].valor)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Aumentou");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write("Manteve");
                }

                Console.ResetColor();
                Console.WriteLine(); // quebra de linha ao final
                temperaturas[0] = temp;
            }

            await Task.Delay(1000);
        }
    }
}
