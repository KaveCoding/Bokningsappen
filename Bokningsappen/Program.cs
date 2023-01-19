using Bokningsappen.Models;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.Arm;
using System.Xml;

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
        AdminKonton,
        Avsluta,
        
    }
    enum Uppdatera
    {
        Rum,
        Sällskap,
        Aktivitet,
        Adminkonto,

        Avsluta
    }
    enum TaBort
    {
        Rum,
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
                Console.WriteLine("Välkommen till spelfaktoriet!\nAdmin login A\nBoka tid B\nVisa Kalender C\nVisa queries D\nAdminuppgifterna är\nNamn: Admin \nLösen: Superdifficultpassword1337!");
                var input = Console.ReadLine().ToLower();
                switch (input)
                {
                    case "a":
                        Adminlogin();
                        break;
                    case "b":
                        Metoder.Uppdatera.BokaTid();
                        break;
                    case "c":
                        Metoder.Visa.VisaKalender();
                        break;
                    case "d":
                        Metoder.Visa.Queries();
                        break;
                    default:
                        Console.WriteLine("felinmatning");
                        break;
                }
            }
        }

        static void Adminlogin()
        {
            Console.WriteLine("Ange användarnamn: ");
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
                    Metoder.LäggTill.CreateNewRoom();
                    break;
                case LäggTill.RumiKalendern:
                    Metoder.LäggTill.LäggTillRumIKalendern();
                    break;
                case LäggTill.Aktivitet:
                    Metoder.LäggTill.LäggtillAktivitet();
                    break;
                case LäggTill.Adminkonto:
                    Metoder.LäggTill.LäggtillAdminkonto();
                    break;
                case LäggTill.Avsluta:
                    break;
            }
            Console.ReadKey();
            Console.Clear();
        }
        static void VisaCase()
        {
            foreach (int i in Enum.GetValues(typeof(Visa)))
            {
                Console.WriteLine($"{i}. {Enum.GetName(typeof(Visa), i).Replace('_', ' ')}");
            }
            Visa menu = (Visa)99;
            int nr;
            if (int.TryParse(Console.ReadKey(true).KeyChar.ToString(), out nr))
            {
                menu = (Visa)nr;
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
                case Visa.Rum:
                    Metoder.Visa.VisaRum();
                    break;
                case Visa.Sällskap:
                    Metoder.Visa.VisaSällskap();
                    break;
                case Visa.Kalender:
                    Metoder.Visa.VisaKalender();
                    break;
                case Visa.AdminKonton:
                    Metoder.Visa.VisaAdminKonton(); //ej klar
                    break;
                case Visa.Avsluta:
                    break;
            }
            Console.ReadKey();
            Console.Clear();
        }
        static void UppdateraCase()
            {
            foreach (int i in Enum.GetValues(typeof(LäggTill)))
            {
                Console.WriteLine($"{i}. {Enum.GetName(typeof(Uppdatera), i).Replace('_', ' ')}");
            }
            Uppdatera menu = (Uppdatera)99;
            int nr;
            if (int.TryParse(Console.ReadKey(true).KeyChar.ToString(), out nr))
            {
                menu = (Uppdatera)nr;
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
                    case Uppdatera.Sällskap:
                    Metoder.Uppdatera.UppdateraSällskap();
                        break;
                    case Uppdatera.Aktivitet:
                    Metoder.Uppdatera.UppdateraAktiviteter();
                        break;
                case Uppdatera.Rum:
                    Metoder.Uppdatera.UppdateraRum();
                        break;
                    case Uppdatera.Adminkonto:
                    Metoder.Uppdatera.UppdateraAdminkonto();
                        break;
                }
            Console.ReadKey();
            Console.Clear();
        }
        static void TaBortCase()
        {
            foreach (int i in Enum.GetValues(typeof(TaBort)))
            {
                Console.WriteLine($"{i}. {Enum.GetName(typeof(TaBort), i).Replace('_', ' ')}");
            }
            TaBort menu = (TaBort)99;
            int nr;
            if (int.TryParse(Console.ReadKey(true).KeyChar.ToString(), out nr))
            {
                menu = (TaBort)nr;
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
                case TaBort.Rum:
                    Metoder.TaBort.TaBortRum();
                    break;
                case TaBort.Aktivitet:
                    Metoder.TaBort.TabortAktivitet();
                    break;
                case TaBort.Sällskap:
                    Metoder.TaBort.TabortSällskap();
                    break;
            }
            Console.ReadKey();
            Console.Clear();
        }  
    }
}

    



    