using Bokningsappen.Models;
using EF_Demo_many2many2.Models;
using System.Runtime.Intrinsics.Arm;

namespace Bokningsappen
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //CreateRows();

            Readmethod();


        }
            static void view_all_Weekdays(string Veckodag, int veckonummer)
                {
                    using (var db = new MyDBContext())
                    {
                        var bokningar = db.Bokningar;
                        var result = bokningar.Where(bokning => bokning.Veckonummer == veckonummer && bokning.Veckodag == Veckodag);
                        Console.Write($"{Veckodag.PadRight(7)} ");
                        foreach (var t in result)
                        {
                            if (t.Tillgänglig == true)
                            {
                                Console.Write("Ledig ");
                            }
                        }
                        Console.WriteLine();
                    }
                }

            static void adminCreateLendableEquipment()
        {

        }
            static void CreateRows()
            {
                for (int i = 1; i <= 52; i++) //veckonummer
                {
                    for (int j = 1; j <= 3; j++) //rum nummer
                    {
                        CreateNewWeekdays(j, "Måndag", i, true);
                        CreateNewWeekdays(j, "Tisdag", i, true);
                        CreateNewWeekdays(j, "Onsdag", i, true);
                        CreateNewWeekdays(j, "Torsdag", i, true);
                        CreateNewWeekdays(j, "Fredag", i, true);
                    }
                }
            }
            static void CreateNewWeekdays( int rum, string veckodag, int veckonummer, bool tillgänglig)
            {
                using (var db = new MyDBContext())
                {
                    var nybokning = new Bokning()
                    {
                       
                        Rum = rum,
                        Veckodag = veckodag,
                        Veckonummer = veckonummer,
                        Tillgänglig = tillgänglig

                    };
                    var bokningar = db.Bokningar;
                    bokningar.Add(nybokning);
                    db.SaveChanges();
                }
            }
            static void Readmethod()
            {
                Console.WriteLine("Ange veckonummer");
                int veckonummer = int.Parse(Console.ReadLine());
                Console.WriteLine("Vecka : " + veckonummer + "\n \nRum        1    2    3\n");

                view_all_Weekdays("Måndag", veckonummer);
                view_all_Weekdays("Tisdag", veckonummer);
                view_all_Weekdays("Onsdag", veckonummer);
                view_all_Weekdays("Torsdag", veckonummer);
                view_all_Weekdays("Fredag", veckonummer);
            }
    }
}
    