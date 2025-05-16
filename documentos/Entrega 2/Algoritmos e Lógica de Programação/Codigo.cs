using System;
using System.Collections.Generic;

class Program
{
    // Constante: preço por kWh (ajuste conforme necessário)
    const decimal PRECO_POR_KWH = 0.75m;

    // Classe para armazenar dados do consumo
    class RegistroConsumo
    {
        public DateTime Data { get; set; }
        public decimal ConsumoKWh { get; set; }

        public decimal CalcularValorEmReais()
        {
            return ConsumoKWh * PRECO_POR_KWH;
        }
    }

    // Procedimento para exibir relatório da semana
    static void MostrarGastosDaSemana(List<RegistroConsumo> registros)
    {
        Console.WriteLine(" Relatório Semanal de Consumo:");

        decimal totalSemana = 0;

        foreach (var registro in registros)
        {
            decimal valorDia = registro.CalcularValorEmReais();
            totalSemana += valorDia;

            Console.WriteLine($"{registro.Data:dd/MM/yyyy} - {registro.ConsumoKWh} kWh - R$ {valorDia:F2}");
        }

        Console.WriteLine($"\n Total da Semana: R$ {totalSemana:F2}");
    }

    static void Main(string[] args)
    {
        // Simulação de entradas manuais (poderia vir de banco de dados ou interface)
        List<RegistroConsumo> semana = new List<RegistroConsumo>();

        for (int i = 0; i < 7; i++)
        {
            Console.WriteLine($"\nEntrada {i + 1}:");
            
            Console.Write("Informe a data (dd/MM/yyyy): ");
            DateTime data = DateTime.Parse(Console.ReadLine());

            Console.Write("Informe o consumo em kWh: ");
            decimal kwh = decimal.Parse(Console.ReadLine());

            semana.Add(new RegistroConsumo { Data = data, ConsumoKWh = kwh });
        }

        // Ordena por data (opcional, para organizar a exibição)
        semana.Sort((a, b) => a.Data.CompareTo(b.Data));

        // Exibe o relatório final
        MostrarGastosDaSemana(semana);
    }
}
