using System;
using System.Collections.Generic;

class Program
{
    // Constante para o preço do kWh (ajuste conforme sua tarifa)
    const decimal PRECO_POR_KWH = 0.75m;

    // Classe para armazenar o consumo diário
    class RegistroConsumo
    {
        public DateTime Data { get; set; }
        public decimal ConsumoKWh { get; set; }

        public decimal CalcularValorEmReais()
        {
            return ConsumoKWh * PRECO_POR_KWH;
        }
    }

    static void MostrarGastosDaSemana(List<RegistroConsumo> registros)
    {
        Console.WriteLine("\n📅 Relatório Semanal de Consumo:");

        decimal totalSemana = 0;

        foreach (var registro in registros)
        {
            decimal valorDia = registro.CalcularValorEmReais();
            totalSemana += valorDia;

            Console.WriteLine($"{registro.Data:dd/MM/yyyy} - {registro.ConsumoKWh} kWh - R$ {valorDia:F2}");
        }

        Console.WriteLine($"\n💰 Total da Semana: R$ {totalSemana:F2}");
    }

    static void Main()
    {
        List<RegistroConsumo> semana = new List<RegistroConsumo>();

        Console.WriteLine("Digite os dados de consumo para até 7 dias.");

        for (int i = 0; i < 7; i++)
        {
            Console.WriteLine($"\nEntrada {i + 1}:");

            DateTime data;
            while (true)
            {
                Console.Write("Informe a data (dd/MM/yyyy): ");
                if (DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", null, 
                    System.Globalization.DateTimeStyles.None, out data))
                    break;
                Console.WriteLine("Data inválida. Tente novamente.");
            }

            decimal kwh;
            while (true)
            {
                Console.Write("Informe o consumo em kWh: ");
                if (decimal.TryParse(Console.ReadLine(), out kwh) && kwh >= 0)
                    break;
                Console.WriteLine("Valor inválido. Digite um número decimal positivo.");
            }

            semana.Add(new RegistroConsumo { Data = data, ConsumoKWh = kwh });
        }

        // Ordena por data para melhor visualização
        semana.Sort((a, b) => a.Data.CompareTo(b.Data));

        MostrarGastosDaSemana(semana);
    }
}