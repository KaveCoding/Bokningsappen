using Bokningsappen.Models;
using EF_Demo_many2many2.Models;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.Arm;

namespace Bokningsappen
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //CreateRows();
            while (true)
            {
                VälkomstText();
            }

        }

        public static void VälkomstText() 
        {
            while (true)
            {
                Console.WriteLine("Välkommen till spelfaktoriet!\nAdmin login A\nBoka tid B\nVisa bokade tider C");
                var input = Console.ReadLine().ToLower();
                switch (input)
                {
                    case "a":
                        Console.WriteLine("Ange epost: ");
                        var epost = Console.ReadLine();
                        Console.WriteLine("Ange lösenord");
                        var lösenord = Console.ReadLine();
                        break;
                    case "b":
                        BokaTid();
                        break;
                    case "c":
                        Readmethod();
                        break;
                    default:
                        Console.WriteLine("felinmatning");
                            break;
                }
            }
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

                    else
                    {
                        Console.Write("Bokad ");
                    }
                }
                Console.WriteLine();
            }
        }
        static void BokaTid()
        {
            Console.WriteLine("Ange ditt namn: ");
            var namn = Console.ReadLine();
            Console.WriteLine("Hur många är ni i sällskapet?: ");
            var antal = int.Parse(Console.ReadLine());
            Console.WriteLine("Skriv in er aktivitet?: ");
            var aktivitet = Console.ReadLine();
            Console.WriteLine("Vilket rum?: ");
            var rum = int.Parse(Console.ReadLine());
            Console.WriteLine("Vilken veckodag?: ");
            var veckodag = (Console.ReadLine());
            Console.WriteLine("Vilken vecka? ");
            var vecka = int.Parse(Console.ReadLine());

            using (var db = new MyDBContext())
            {
                var newSällskap = new Sällskap()
                {
                    Namn = namn,
                    Antal_i_sällskapet = antal,
                    Aktivitet = aktivitet,

                };
                var sällskapList = db.Sällskaper;
                sällskapList.Add(newSällskap);
                db.SaveChanges();
            }

            using (var db = new MyDBContext())
            {
                var updateKund = (from t in db.Bokningar
                                  where t.Veckodag == veckodag && t.Veckonummer == vecka && t.Rum == rum && t.Tillgänglig == true
                                  select t).SingleOrDefault();
                var senastesällskap = (from t in db.Sällskaper orderby t.Id descending select t);
                var test = senastesällskap.First().Id;
                
                
                updateKund.Tillgänglig = false;
                updateKund.SällskapId = test;
                    db.SaveChanges();
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
        static void CreateNewWeekdays(int rum, string veckodag, int veckonummer, bool tillgänglig)
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



    