using Bokningsappen.Models;
using EF_Demo_many2many2.Models;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.Arm;

namespace Bokningsappen
{
    enum MenuListAdmin
    {
        Lägg_till,
        Ta_bort,
        Uppdatera,
        Visa,

        Avsluta
    }
    enum LäggTill
    {
        Rum,
        RumiKalendern,
        Aktivitet,
        Adminkonto,
        Avsluta,
    }
    enum Visa
    {
        Kalender = 1,
        Aktivteter,
        Sällskap,
        Rum,
        Avsluta,
        AdminKonton,
    }
    enum Uppdatera
    {
        Rum,
        Bokningstillfälle,
        Sällskap,
        Aktivitet,
        Adminkonto,
        Avsluta
    }
    enum TaBort
    {
        Rum,
        Bokningstillfälle,
        Sällskap,
        Aktivitet,

        Avsluta
    }


    internal class Program
    {
        static void Main(string[] args)
        {

            while (true)
            {
                VälkomstText();
            }
        }

        static void VälkomstText()
        {
            while (true)
            {
                Console.WriteLine("Välkommen till spelfaktoriet!\nAdmin login A\nBoka tid B\nVisa bokade tider C");
                var input = Console.ReadLine().ToLower();
                switch (input)
                {
                    case "a":
                        Adminlogin();
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

        static void ViewWeekday(string Veckodag, int veckonummer)
        {
            using (var db = new MyDBContext())
            {
                var bokningar = db.Bokningar;
                var sällskap = db.Sällskaper;
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
                        var namn = sällskap.Where(sällskap => sällskap.Id == t.SällskapId).SingleOrDefault();
                        Console.Write(namn.Namn + " ");
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
            var aktivitet = int.Parse(Console.ReadLine());
            Console.WriteLine("Vilket Rum?: ");
            var RumId = int.Parse(Console.ReadLine());
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
                    AktivitetId = aktivitet,

                };
                var sällskapList = db.Sällskaper;
                sällskapList.Add(newSällskap);
                db.SaveChanges();
            }

            using (var db = new MyDBContext())
            {
                var updateKund = (from t in db.Bokningar
                                  where t.Veckodag == veckodag && t.Veckonummer == vecka && t.RumId == RumId && t.Tillgänglig == true
                                  select t).SingleOrDefault();
                var senastesällskap = (from t in db.Sällskaper orderby t.Id descending select t);
                var test = senastesällskap.First().Id;


                updateKund.Tillgänglig = false;
                updateKund.SällskapId = test;
                db.SaveChanges();
            }
        }
        static void CreateBookableRoom(int roomid)
        {
            for (int i = 1; i <= 52; i++) //veckonummer
            {
                CreateNewEntries(roomid, "Måndag", i, true);
                CreateNewEntries(roomid, "Tisdag", i, true);
                CreateNewEntries(roomid, "Onsdag", i, true);
                CreateNewEntries(roomid, "Torsdag", i, true);
                CreateNewEntries(roomid, "Fredag", i, true);
            }

        }
        static void CreateNewEntries(int RumId, string veckodag, int veckonummer, bool tillgänglig)
        {
            using (var db = new MyDBContext())
            {
                var nybokning = new Bokning()
                {

                    RumId = RumId,
                    Veckodag = veckodag,
                    Veckonummer = veckonummer,
                    Tillgänglig = tillgänglig

                };
                var bokningar = db.Bokningar;
                bokningar.Add(nybokning);
                db.SaveChanges();
            }
        }
        static void CreateNewRoom()
        {
            Console.WriteLine("Hur många TV har rummet?");
            int tv = int.Parse(Console.ReadLine());

            Console.WriteLine("Hur många bord har rummet?");
            int bord = int.Parse(Console.ReadLine());

            Console.WriteLine("Hur många stolar har rummet?");
            int stolar = int.Parse(Console.ReadLine());

            using (var db = new MyDBContext())
            {
                var nybokning = new Rum()
                {
                    TV = tv,
                    Bord = bord,
                    Stolar = stolar,
                };
                var rum = db.Rooms;
                rum.Add(nybokning);
                db.SaveChanges();
            }
        }

        static void Readmethod()

        {
            Console.WriteLine("Ange veckonummer");
            int veckonummer = int.Parse(Console.ReadLine());
            Console.WriteLine("Vecka : " + veckonummer + "\n \nRum            \n");
            ViewWeekday("Måndag", veckonummer);
            ViewWeekday("Tisdag", veckonummer);
            ViewWeekday("Onsdag", veckonummer);
            ViewWeekday("Torsdag", veckonummer);
            ViewWeekday("Fredag", veckonummer);
        }

        static void Adminlogin()
        {
            Console.WriteLine("Ange epost: ");
            var epost = Console.ReadLine();
            using (var db = new MyDBContext())
            {
                var hittaAdmin = (from t in db.AdminKonton
                                  where t.Namn == epost
                                  select t).SingleOrDefault();

                if (hittaAdmin != null)
                {
                    Console.WriteLine("Ange Lösenord");
                    string lösen = Console.ReadLine();
                    if (lösen == hittaAdmin.Lösen)
                    {
                        foreach (int i in Enum.GetValues(typeof(MenuListAdmin)))
                        {
                            Console.WriteLine($"{i}. {Enum.GetName(typeof(MenuListAdmin), i).Replace('_', ' ')}");
                        }

                        MenuListAdmin menu = (MenuListAdmin)99;
                        int nr;

                        if (int.TryParse(Console.ReadKey(true).KeyChar.ToString(), out nr))
                        {
                            menu = (MenuListAdmin)nr;
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Fel inmatning");
                            Console.ReadKey();
                            Console.Clear();
                        }
                        switch (menu)
                        {
                            case MenuListAdmin.Lägg_till:
                                Läggtillcase();
                                break;
                            case MenuListAdmin.Visa:
                                VisaCase();
                                break;
                            case MenuListAdmin.Uppdatera:
                                UppdateraCase();
                                break;
                            case MenuListAdmin.Ta_bort:
                                TaBortCase();
                                break;
                            case MenuListAdmin.Avsluta:
                                break;
                        }
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Lösenordet är fel");
                        Console.ReadKey();
                        Console.Clear();
                    }

                }
            }

            static void Läggtillcase()
            {
                foreach (int i in Enum.GetValues(typeof(LäggTill)))
                {
                    Console.WriteLine($"{i}. {Enum.GetName(typeof(LäggTill), i).Replace('_', ' ')}");
                }
                LäggTill menu = (LäggTill)99;
                int nr;
                if (int.TryParse(Console.ReadKey(true).KeyChar.ToString(), out nr))
                {
                    menu = (LäggTill)nr;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Fel inmatning");
                    Console.ReadKey();
                    Console.Clear();
                }
                switch (menu)
                {
                    case LäggTill.Rum:
                        CreateNewRoom();
                        break;
                    case LäggTill.RumiKalendern:
                        break;
                    case LäggTill.Aktivitet:
                        break;
                    case LäggTill.Adminkonto:
                        break;
                    case LäggTill.Avsluta:
                        break;
                }
            }

            static void TaBortCase()
            {
                TaBort taBort = (TaBort)99;
                switch (taBort)
                {
                    case TaBort.Rum:
                        break;
                    case TaBort.Aktivitet:
                        break;
                    case TaBort.Sällskap:
                        break;
                }
            }

            static void VisaCase()
            {
                Visa visa = (Visa)99;
                switch (visa)
                {
                    case Visa.Sällskap:
                        break;
                    case Visa.Kalender:
                        break;
                    case Visa.Aktivteter:
                        break;
                    case Visa.AdminKonton:
                        break;
                }
            }

            static void UppdateraCase()
            {
                Uppdatera uppdatera = (Uppdatera)99;
                switch (uppdatera)
                {
                    case Uppdatera.Sällskap:
                        break;
                    case Uppdatera.Aktivitet:
                        break;
                    case Uppdatera.Rum:
                        break;
                    case Uppdatera.Adminkonto:
                        break;
                }
            }



           
        }
    }
}




    