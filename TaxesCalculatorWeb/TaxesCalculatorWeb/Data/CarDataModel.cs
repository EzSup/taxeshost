using MudBlazor;
using System.ComponentModel.DataAnnotations;
using static TaxesCalculatorWeb.Pages.Index;

namespace TaxesCalculatorWeb.Data
{
    public class CarDataModel
    {
        [Required]
        [Label("Дата реєстрації")]        
        public DateTime? Date { get; set; }
        [Required]
        [Label("Тип двигуна")]
        public EngineType EngType { get; set; }
        [Required]
        [Label("Об'єм двигуна")]
        [Range(0.1, double.MaxValue, ErrorMessage ="Об'єм двигуна повинен бути більшим за 0")]
        public double Cylinder { get; set; }
        [Label("Євро стандарт")]
        public Euro Euro { get; set; }

        public void SolveEuro()
        {
            if (Date < new DateTime(1992, 6, 1))
            {   Euro = Euro.Euro0; return; }
            else if (Date < new DateTime(1996, 1, 1))
            {   Euro = Euro.Euro1; return;     }
            else if (Date < new DateTime(2000, 1, 1))
            { Euro = Euro.Euro2; return; }
            else if (Date < new DateTime(2005, 1, 1))
            { Euro = Euro.Euro3; return; }
            else if (Date < new DateTime(2009, 9, 1))
            { Euro = Euro.Euro4; return; }
            else if (Date < new DateTime(2014, 9, 1))
            { Euro = Euro.Euro5; return; }
            else if (Date > new DateTime(2014, 9, 1))
            { Euro = Euro.Euro6; return; }
            else
            {
                throw new Exception("Помилка у введеній даті");
            }
        }

        public double BasovaStavkaChislo()
        {
            if (EngType == EngineType.Gasoline)
            {
                switch (Euro)
                {
                    case Euro.Euro0: return 24;
                    case Euro.Euro1: return 20;
                    case Euro.Euro2: return 18;
                    case Euro.Euro3: return 16;
                    case Euro.Euro4: return 14;
                    case Euro.Euro5: return 12;
                    case Euro.Euro6: return 10;
                    default: throw new Exception("Не обрано Euro!");
                }
            }
            if (EngType == EngineType.Diesel)
            {
                switch (Euro)
                {
                    case Euro.Euro0: return 36;
                    case Euro.Euro1: return 32;
                    case Euro.Euro2: return 28;
                    case Euro.Euro3: return 26;
                    case Euro.Euro4: return 24;
                    case Euro.Euro5: return 22;
                    case Euro.Euro6: return 20;
                    default: throw new Exception("Не обрано Euro!");
                }
            }
            if (EngType == EngineType.Gas)
            {
                switch (Euro)
                {
                    case Euro.Euro0: return 20;
                    case Euro.Euro1: return 16;
                    case Euro.Euro2: return 14;
                    case Euro.Euro3: return 12;
                    case Euro.Euro4: return 10;
                    case Euro.Euro5: return 8;
                    case Euro.Euro6: return 6;
                    default: throw new Exception("Не обрано Euro!");
                }
            }
            else
            {
                throw new Exception("Не обрано тип двигуна!");
            }
        }

        public double BasovaStavka()
        {
            return 7100 * BasovaStavkaChislo() / 100;
        }

        public double Koef()
        {
            return Cylinder / 1000;
        }

        public double Result()
        {
            return BasovaStavka() * Koef();
        }
    }
}
