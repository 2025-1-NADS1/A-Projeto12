using System.Windows.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;
using MySqlConnector;
using Consumo;
using FrameworkTest.Charts;

namespace Dashboard
{
    public partial class UcHome : UserControl
    {
        private MySqlConnection conexao;
        public UcHome(MySqlConnection conexao)
        {
            InitializeComponent();
            this.conexao = conexao;
            AtualizarConsumoSemanaAtual();
            AtualizarConsumoSemanaPassada();
            AtualizarEconomiaCircularProgress();
            AtualizarEconomiaSemanaPassadaCircularProgress();
            AtualizarGraficoConsumoMensal();
        }
        

        public UcHome()
        {

        }

        private void UcHome_Load(object sender, EventArgs e)
        {

        }

        private void labelRegistrar_Click(object sender, EventArgs e)
        {

        }

        private void pictureAccount_Click(object sender, EventArgs e)
        {

        }

        private void labelLogin_Click(object sender, EventArgs e)
        {

        }

        private void labelSemanaAtual_Click(object sender, EventArgs e)
        {

        }

        private void sataBarraEconomia_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void labelGastoAnt_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void sataPanel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void sataPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void sataSearchBox_Click(object sender, EventArgs e)
        {

        }

        private void labelUserClass_Click(object sender, EventArgs e)
        {

        }

        private void sataGraficoBarra_Load(object sender, EventArgs e)
        {

        }

        private void ttgsTxt_Click(object sender, EventArgs e)
        {

        }

        private void AtualizarConsumoSemanaAtual()
        {
            double totalSemana = 0;
            using (var cmd = new MySqlCommand(@"
        SELECT IFNULL(SUM(valor_kWh), 0) 
        FROM CONSUMO 
        WHERE YEARWEEK(data, 1) = YEARWEEK(CURDATE(), 1)", conexao))
            {
                var result = cmd.ExecuteScalar();
                if (result != null && double.TryParse(result.ToString(), out double valor))
                {
                    totalSemana = valor;
                }
            }
            ttgsTxt.Text = $"{totalSemana:F0} kWh";
        }

        private void AtualizarConsumoSemanaPassada()
        {
            double totalSemanaPassada = 0;
            using (var cmd = new MySqlCommand(@"
        SELECT IFNULL(SUM(valor_kWh), 0) 
        FROM CONSUMO 
        WHERE YEARWEEK(data, 1) = YEARWEEK(CURDATE(), 1) - 1", conexao))
            {
                var result = cmd.ExecuteScalar();
                if (result != null && double.TryParse(result.ToString(), out double valor))
                {
                    totalSemanaPassada = valor;
                }
            }
            // Exemplo: atribua o valor a um label específico para semana passada
            ttgsptxt.Text = $"{totalSemanaPassada:F0} kWh";
        }

        private void AtualizarEconomiaCircularProgress()
        {
            double consumoSemanaAtual = 0;
            double consumoSemanaMesAnterior = 0;

            // Consumo da semana atual
            using (var cmd = new MySqlCommand(@"
        SELECT IFNULL(SUM(valor_kWh), 0) 
        FROM CONSUMO 
        WHERE YEARWEEK(data, 1) = YEARWEEK(CURDATE(), 1)", conexao))
            {
                var result = cmd.ExecuteScalar();
                if (result != null && double.TryParse(result.ToString(), out double valor))
                    consumoSemanaAtual = valor;
            }

            // Consumo da mesma semana do mês anterior
            using (var cmd = new MySqlCommand(@"
        SELECT IFNULL(SUM(valor_kWh), 0)
        FROM CONSUMO
        WHERE YEARWEEK(data, 1) = YEARWEEK(DATE_SUB(CURDATE(), INTERVAL 1 MONTH), 1)", conexao))
            {
                var result = cmd.ExecuteScalar();
                if (result != null && double.TryParse(result.ToString(), out double valor))
                    consumoSemanaMesAnterior = valor;
            }

            // Calcula a porcentagem de economia
            double economia = 0;
            if (consumoSemanaMesAnterior > 0)
            {
                economia = ((consumoSemanaMesAnterior - consumoSemanaAtual) / consumoSemanaMesAnterior) * 100.0;
                if (economia < 0) economia = 0; // Não exibe economia negativa
            }

            // Atualiza o circular progress (ajuste o nome do controle conforme seu projeto)
            gsTxT.percentage = (int)Math.Round(economia);
        }

        private void AtualizarEconomiaSemanaPassadaCircularProgress()
        {
            double consumoSemanaPassada = 0;
            double consumoSemanaPassadaMesAnterior = 0;

            // Consumo da semana passada
            using (var cmd = new MySqlCommand(@"
        SELECT IFNULL(SUM(valor_kWh), 0) 
        FROM CONSUMO 
        WHERE YEARWEEK(data, 1) = YEARWEEK(CURDATE(), 1) - 1", conexao))
            {
                var result = cmd.ExecuteScalar();
                if (result != null && double.TryParse(result.ToString(), out double valor))
                    consumoSemanaPassada = valor;
            }

            // Consumo da mesma semana do mês anterior
            using (var cmd = new MySqlCommand(@"
        SELECT IFNULL(SUM(valor_kWh), 0)
        FROM CONSUMO
        WHERE YEARWEEK(data, 1) = YEARWEEK(DATE_SUB(CURDATE(), INTERVAL 1 MONTH), 1) - 1", conexao))
            {
                var result = cmd.ExecuteScalar();
                if (result != null && double.TryParse(result.ToString(), out double valor))
                    consumoSemanaPassadaMesAnterior = valor;
            }

            // Calcula a porcentagem de economia
            double economia = 0;
            if (consumoSemanaPassadaMesAnterior > 0)
            {
                economia = ((consumoSemanaPassadaMesAnterior - consumoSemanaPassada) / consumoSemanaPassadaMesAnterior) * 100.0;
                if (economia < 0) economia = 0; // Não exibe economia negativa
            }

            // Atualiza o circular progress (ajuste o nome do controle conforme seu projeto)
            gspTxT.percentage = (int)Math.Round(economia);
        }

        private float[] ObterConsumoSemanalMes(int ano, int mes)
        {
            // Descobre a primeira e última semana do mês
            DateTime primeiroDia = new DateTime(ano, mes, 1);
            DateTime ultimoDia = primeiroDia.AddMonths(1).AddDays(-1);

            // O número da semana pode variar conforme o ano, então vamos calcular todas as semanas do mês
            List<int> semanas = new List<int>();
            for (DateTime dt = primeiroDia; dt <= ultimoDia; dt = dt.AddDays(1))
            {
                int semana = System.Globalization.CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(
                    dt, System.Globalization.CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
                if (!semanas.Contains(semana))
                    semanas.Add(semana);
            }

            float[] consumos = new float[semanas.Count];

            for (int i = 0; i < semanas.Count; i++)
            {
                int semana = semanas[i];
                using (var cmd = new MySqlCommand(@"
            SELECT IFNULL(SUM(valor_kWh), 0)
            FROM CONSUMO
            WHERE YEAR(data) = @ano AND MONTH(data) = @mes
              AND WEEK(data, 1) = @semana", conexao))
                {
                    cmd.Parameters.AddWithValue("@ano", ano);
                    cmd.Parameters.AddWithValue("@mes", mes);
                    cmd.Parameters.AddWithValue("@semana", semana);

                    var result = cmd.ExecuteScalar();
                    if (result != null && float.TryParse(result.ToString(), out float valor))
                        consumos[i] = valor;
                    else
                        consumos[i] = 0;
                }
            }
            return consumos;
        }

        private void AtualizarGraficoConsumoMensal()
        {
            DateTime hoje = DateTime.Today;
            int anoAtual = hoje.Year;
            int mesAtual = hoje.Month;

            // Mês anterior
            DateTime mesAnterior = hoje.AddMonths(-1);
            int anoAnterior = mesAnterior.Year;
            int mesAnt = mesAnterior.Month;

            float[] consumosMesAtual = ObterConsumoSemanalMes(anoAtual, mesAtual);
            float[] consumosMesAnterior = ObterConsumoSemanalMes(anoAnterior, mesAnt);

            // Gera os rótulos das semanas dinamicamente
            int qtdSemanas = Math.Max(consumosMesAtual.Length, consumosMesAnterior.Length);
            string[] rotulosSemanas = Enumerable.Range(1, qtdSemanas)
                .Select(i => $"Semana {i}")
                .ToArray();

            var dataSetMesAtual = new FrameworkTest.Charts.SATALineChart.DataSet
            {
                Points = consumosMesAtual,
                LineColor = Color.Blue,
                PointColor = Color.Blue,
                Label = "Mês Atual"
            };

            var dataSetMesAnterior = new FrameworkTest.Charts.SATALineChart.DataSet
            {
                Points = consumosMesAnterior,
                LineColor = Color.Gold,
                PointColor = Color.Gold,
                Label = "Mês Anterior"
            };

            chartConsumo.DataSets = new List<FrameworkTest.Charts.SATALineChart.DataSet> { dataSetMesAtual, dataSetMesAnterior };
            chartConsumo.CustomXAxis = rotulosSemanas; // <-- Aqui você define os nomes das semanas
            chartConsumo.AutoMaxValue = true;
            chartConsumo.ShowGrid = true;
            chartConsumo.ShowLabels = true;
        }


    }
}


