using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SIM_TP3.Simulacion
{
    public class Montecarlo
    {
        // Atributos de la clase
        public double Dias { get; set; }
        public double StockIncial { get; set; }
        public double PrecioVenta { get; set; }
        public double PrecioDev { get; set; }
        public double CostoPeriodico { get; set; }
        public double CostoSO { get; set; }
        public double CostoFaltantes { get; set; }

        public DataGridView Politica1 { get; set; }
        public DataGridView Politica2 { get; set; }
        public TextBox Resultados { get; set; }

        private List<double> Demanda = new List<double>();
        private List<double> Aleatorio = new List<double>();
        private List<double> Demanda2 = new List<double>();
        private List<double> Aleatorio2 = new List<double>();
        private Random rnd = new Random();

        //Metodos

        /// Nos permite realizar la generación de número aleatorio RND[0,1).
        public double GenerarAleatorio()
        {
            //Genera numeros aleatorios entre 0 y 1 sin llegar a 1
            return rnd.NextDouble();
        }

        public void GenerarDistribucion()
        {
            //Generamos la distibucion de la demanda en base a la probabilidad acumulada para la politica 1
            for (int i = 0; i < Dias; i++)
            {
                double demanda = 0;
                double random = GenerarAleatorio();
                if (random <= 0.04)
                {
                    demanda = 30;
                }
                else if (random > 0.04 && random <= 0.19)
                {
                    demanda = 31;
                }
                else if (random > 0.19 && random <= 0.41)
                {
                    demanda = 32;
                }
                else if (random > 0.41 && random <= 0.79)
                {
                    demanda = 33;
                }
                else if (random > 0.79 && random <= 0.93)
                {
                    demanda = 34;
                }
                else
                {
                    demanda = 35;
                }

                Demanda.Add(demanda);
                Aleatorio.Add(random);
            }

            //Generamos la distibucion de la demanda en base a la probabilidad acumulada para la politica 2
            for (int i = 0; i < Dias; i++)
            {
                double demanda = 0;
                double random = GenerarAleatorio();
                if (random <= 0.04)
                {
                    demanda = 30;
                }
                else if (random > 0.04 && random <= 0.19)
                {
                    demanda = 31;
                }
                else if (random > 0.19 && random <= 0.41)
                {
                    demanda = 32;
                }
                else if (random > 0.41 && random <= 0.79)
                {
                    demanda = 33;
                }
                else if (random > 0.79 && random <= 0.93)
                {
                    demanda = 34;
                }
                else
                {
                    demanda = 35;
                }

                Demanda2.Add(demanda);
                Aleatorio2.Add(random);
            }

            GenerarPolitica1();
            GenerarPolitica2();
            CalcularResultadoPoliticas();
        }

        public void GenerarPolitica1()
        {
            Politica1.Rows.Clear();

            double diaAnterior = 0;
            double demandaAnterior = 0;
            double gananciaAnterior = 0;

            double dia = 0;
            double stockInicial = 0;
            double demanda = 0;
            double stockPostVenta = 0;
            double costoPeriodico = 0;
            double costoStockOut = 0;
            double costoTotal = 0;
            double ingresoVentas = 0;
            double ingresoDevolucion = 0;
            double ingresoTotal = 0;
            double gananciaTotal = 0;
            double gananciaAcumulada = 0;
            double promGanancia = 0;

            for (int i = 0; i < Dias; i++)
            {
                //Calculamos la primera fila y apartir de este generamos el resto

                if (i == 0)
                {
                    dia = i + 1;
                    stockInicial = StockIncial;
                    demanda = Demanda[i];
                    stockPostVenta = stockInicial - demanda;
                    costoPeriodico = stockInicial * CostoPeriodico;
                    if (stockPostVenta < 0)
                    {
                        costoStockOut = stockPostVenta * CostoSO * -1;
                        ingresoVentas = stockInicial * PrecioVenta;
                        ingresoDevolucion = 0;
                    }
                    else
                    {
                        costoStockOut = 0;
                        ingresoVentas = demanda * PrecioVenta;
                        ingresoDevolucion = stockPostVenta * PrecioDev;
                    }
                    costoTotal = costoStockOut + costoPeriodico;
                    ingresoTotal = ingresoVentas + ingresoDevolucion;
                    gananciaTotal = ingresoTotal - costoTotal;
                    gananciaAcumulada = gananciaTotal;
                    promGanancia = gananciaAcumulada / dia;

                    diaAnterior = dia;
                    demandaAnterior = demanda;
                    gananciaAnterior = gananciaAcumulada;
                }

                //calculamos el resto de las filas
                else
                {
                    dia = diaAnterior + 1;
                    stockInicial = demandaAnterior;
                    demanda = Demanda[i];
                    stockPostVenta = stockInicial - demanda;
                    costoPeriodico = stockInicial * CostoPeriodico;
                    if (stockPostVenta < 0)
                    {
                        costoStockOut = stockPostVenta * CostoSO * -1;
                        ingresoVentas = stockInicial * PrecioVenta;
                        ingresoDevolucion = 0;
                    }
                    else
                    {
                        costoStockOut = 0;
                        ingresoVentas = demanda * PrecioVenta;
                        ingresoDevolucion = stockPostVenta * PrecioDev;
                    }
                    costoTotal = costoStockOut + costoPeriodico;
                    ingresoTotal = ingresoVentas + ingresoDevolucion;
                    gananciaTotal = ingresoTotal - costoTotal;
                    gananciaAcumulada = gananciaTotal + gananciaAnterior;
                    promGanancia = gananciaAcumulada / dia;

                    diaAnterior = dia;
                    demandaAnterior = demanda;
                    gananciaAnterior = gananciaAcumulada;
                }

                //Hacemos que los valores sean de 4 decimales antes de agregarlos a la grilla
                dia = Math.Truncate(dia * 10000) / 10000;
                stockInicial = Math.Truncate(stockInicial * 10000) / 10000;
                double aleatorio = Math.Truncate(Aleatorio[i] * 10000) / 10000;
                demanda = Math.Truncate(demanda * 10000) / 10000;
                stockPostVenta = Math.Truncate(stockPostVenta * 10000) / 10000;
                costoPeriodico = Math.Truncate(costoPeriodico * 10000) / 10000;
                costoStockOut = Math.Truncate(costoStockOut * 10000) / 10000;
                costoTotal = Math.Truncate(costoTotal * 10000) / 10000;
                ingresoVentas = Math.Truncate(ingresoVentas * 10000) / 10000;
                ingresoDevolucion = Math.Truncate(ingresoDevolucion * 10000) / 10000;
                ingresoTotal = Math.Truncate(ingresoTotal * 10000) / 10000;
                gananciaTotal = Math.Truncate(gananciaTotal * 10000) / 10000;
                gananciaAcumulada = Math.Truncate(gananciaAcumulada * 10000) / 10000;
                promGanancia = Math.Truncate(promGanancia * 10000) / 10000;

                Politica1.Rows.Add(dia, stockInicial, aleatorio, demanda, stockPostVenta, costoPeriodico, costoStockOut,
                    costoTotal, ingresoVentas, ingresoDevolucion, ingresoTotal, gananciaTotal, gananciaAcumulada, promGanancia);
            }

            //Resalta el valor final de la politica 1
            Politica1.Rows[(Politica1.RowCount - 1)].Cells[(Politica1.ColumnCount - 1)].Style.BackColor = Color.Coral;
        }

        public void GenerarPolitica2()
        {
            Politica2.Rows.Clear();

            double diaAnterior = 0;
            double demandaAnterior = 0;
            double gananciaAnterior = 0;

            double dia = 0;
            double stockInicial = 0;
            double demanda = 0;
            double stockPostVenta = 0;
            double costoPeriodico = 0;
            double costoStockFaltante = 0;
            double costoTotal = 0;
            double ingresoVentas = 0;
            double ingresoDevolucion = 0;
            double ingresoTotal = 0;
            double gananciaTotal = 0;
            double gananciaAcumulada = 0;
            double promGanancia = 0;

            for (int i = 0; i < Dias; i++)
            {
                //Calculamos la primera fila y apartir de este generamos el resto

                if (i == 0)
                {
                    dia = i + 1;
                    stockInicial = StockIncial;
                    demanda = Demanda2[i];
                    stockPostVenta = stockInicial - demanda;
                    costoPeriodico = stockInicial * CostoPeriodico;
                    ingresoVentas = demanda * PrecioVenta;

                    if (stockPostVenta < 0)
                    {
                        costoStockFaltante = stockPostVenta * CostoFaltantes * -1;
                        ingresoDevolucion = 0;
                    }
                    else
                    {
                        costoStockFaltante = 0;
                        ingresoDevolucion = stockPostVenta * PrecioDev;
                    }

                    costoTotal = costoStockFaltante + costoPeriodico;
                    ingresoTotal = ingresoVentas + ingresoDevolucion;
                    gananciaTotal = ingresoTotal - costoTotal;
                    gananciaAcumulada = gananciaTotal;
                    promGanancia = gananciaAcumulada / dia;

                    diaAnterior = dia;
                    demandaAnterior = demanda;
                    gananciaAnterior = gananciaAcumulada;
                }

                //calculamos el resto de las filas
                else
                {
                    dia = diaAnterior + 1;
                    stockInicial = demandaAnterior;
                    demanda = Demanda2[i];
                    stockPostVenta = stockInicial - demanda;
                    costoPeriodico = stockInicial * CostoPeriodico;
                    ingresoVentas = demanda * PrecioVenta;
                    if (stockPostVenta < 0)
                    {
                        costoStockFaltante = stockPostVenta * CostoFaltantes * -1;
                        ingresoDevolucion = 0;
                    }
                    else
                    {
                        costoStockFaltante = 0;
                        ingresoDevolucion = stockPostVenta * PrecioDev;
                    }
                    costoTotal = costoStockFaltante + costoPeriodico;
                    ingresoTotal = ingresoVentas + ingresoDevolucion;
                    gananciaTotal = ingresoTotal - costoTotal;
                    gananciaAcumulada = gananciaTotal + gananciaAnterior;
                    promGanancia = gananciaAcumulada / dia;

                    diaAnterior = dia;
                    demandaAnterior = demanda;
                    gananciaAnterior = gananciaAcumulada;
                }

                //Hacemos que los valores sean de 4 decimales antes de agregarlos a la grilla
                dia = Math.Truncate(dia * 10000) / 10000;
                stockInicial = Math.Truncate(stockInicial * 10000) / 10000;
                double aleatorio = Math.Truncate(Aleatorio2[i] * 10000) / 10000;
                demanda = Math.Truncate(demanda * 10000) / 10000;
                stockPostVenta = Math.Truncate(stockPostVenta * 10000) / 10000;
                costoPeriodico = Math.Truncate(costoPeriodico * 10000) / 10000;
                costoStockFaltante = Math.Truncate(costoStockFaltante * 10000) / 10000;
                costoTotal = Math.Truncate(costoTotal * 10000) / 10000;
                ingresoVentas = Math.Truncate(ingresoVentas * 10000) / 10000;
                ingresoDevolucion = Math.Truncate(ingresoDevolucion * 10000) / 10000;
                ingresoTotal = Math.Truncate(ingresoTotal * 10000) / 10000;
                gananciaTotal = Math.Truncate(gananciaTotal * 10000) / 10000;
                gananciaAcumulada = Math.Truncate(gananciaAcumulada * 10000) / 10000;
                promGanancia = Math.Truncate(promGanancia * 10000) / 10000;

                Politica2.Rows.Add(dia, stockInicial, aleatorio, demanda, stockPostVenta, costoPeriodico, costoStockFaltante,
                    costoTotal, ingresoVentas, ingresoDevolucion, ingresoTotal, gananciaTotal, gananciaAcumulada, promGanancia);
            }

            //Resalta el valor final de la politica 2
            Politica2.Rows[(Politica2.RowCount - 1)].Cells[(Politica2.ColumnCount - 1)].Style.BackColor = Color.Coral;
        }


        //Calculamos los resultados de las evaluaciones y lo agregamos al textbox de resultados
        public void CalcularResultadoPoliticas()
        {
            Resultados.Clear();

            double valorPolitica1 = Convert.ToDouble(Politica1.Rows[(Politica1.RowCount - 1)].Cells[(Politica1.ColumnCount - 1)].Value);
            double valorPolitica2 = Convert.ToDouble(Politica2.Rows[(Politica2.RowCount - 1)].Cells[(Politica2.ColumnCount - 1)].Value);

            Resultados.AppendText("Siendo la politica 1 el tener un costo de Stock-Out de: $" + CostoSO + ", y la politica 2 la de permitir comprar los periodicos faltantes al precio de: $" + CostoFaltantes + 
                ", podemos determinar que la politica conveniente es: ");

            // Comparamos resultados de politicas y determinamos
            if (valorPolitica1 <= valorPolitica2)
            {
                Resultados.Text += "Politica 2";
                Resultados.Text += "\r\nGanancia Diaria Promedio de la politica 1: $" + Math.Truncate(valorPolitica1 * 100) / 100;
                Resultados.Text += "\r\nGanancia Diaria Promedio de la politica 2: $" + Math.Truncate(valorPolitica2 * 100) / 100; ;
            }
            else
            {
                Resultados.Text += "Politica 1";
                Resultados.Text += "\r\nGanancia Diaria Promedio de la politica 1: $" + Math.Truncate(valorPolitica1 * 100) / 100;
                Resultados.Text += "\r\nGanancia Diaria Promedio de la politica 2: $" + Math.Truncate(valorPolitica2 * 100) / 100; ;
            }

        }
    }
}
