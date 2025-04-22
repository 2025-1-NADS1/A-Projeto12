using System;

class DashboardTemperatura
{
    static void Main()
    {
                // Definindo os cômodos e horas de leitura
        string[] comodos = { "Sala", "Quarto", "Banheiro" };
        int horas = 3;

        // Criando a matriz de temperaturas (comodos x horas)
        double[,] temperaturas = new double[comodos.Length, horas];

        // Preenchendo a matriz com dados (simulados ou via usuário)
        Console.WriteLine("Digite as temperaturas registradas:");
        for (int i = 0; i < comodos.Length; i++)
        {
            for (int j = 0; j < horas; j++)
            {
                Console.Write($"Temperatura em {comodos[i]} na hora {j + 1}: ");
                temperaturas[i, j] = Convert.ToDouble(Console.ReadLine());
            }
        }

        Console.WriteLine("\nRelatório de Temperaturas:");
        double[] medias = new double[comodos.Length];

        for (int i = 0; i < comodos.Length; i++)
        {
            double soma = 0;
            for (int j = 0; j < horas; j++)
            {
                soma += temperaturas[i, j];
            }
            medias[i] = soma / horas;
            Console.WriteLine($"Média em {comodos[i]}: {medias[i]:F1}°C");

            // Estrutura de decisão para alertar temperaturas críticas
            if (medias[i] > 30)
            {
                Console.WriteLine($"[ALERTA] Temperatura alta detectada em {comodos[i]}!");
            }
            else if (medias[i] < 15)
            {
                Console.WriteLine($"[ALERTA] Temperatura muito baixa detectada em {comodos[i]}!");
            }
        }

        Console.WriteLine("\nAnálise concluída.");
    }
}