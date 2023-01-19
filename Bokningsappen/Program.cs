using Bokningsappen.Models;
using EF_Demo_many2many2.Models;
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
                Console.WriteLine("Välkommen till spelfaktoriet!\nAdmin login A\nBoka tid B\nVisa Kalender C\nAdminuppgifterna är\nNamn: Admin \nLösen: Superdifficultpassword1337!");
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
                        VisaKalender();
                        break;
                    default:
                        Console.WriteLine("felinmatning");
                        break;
                }
            }
        }
        static void VisaVecka(string Veckodag, int veckonummer)
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
                        Console.Write("Ledig   ");
                    }
                    else
                    {
                        var namn = sällskap.Where(sällskap => sällskap.Id == t.SällskapId).SingleOrDefault();
                        Console.Write(namn.Namn.PadRight(4) + "".PadRight(3));
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

            using (var db = new MyDBContext())
            {
                var aktiviteter = (from t in db.Aktiviteter
                                  select t);
                foreach (var a in aktiviteter)
                {
                    Console.WriteLine($"{a.Id} {a.Name}");
                }
            }
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
                Console.Clear();
                Console.WriteLine("Tid bokad!");
                Console.ReadKey();
                Console.Clear();
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
        static void VisaKalender()
        {
            Console.WriteLine("Ange veckonummer");
            int veckonummer = int.Parse(Console.ReadLine());
            int counter = 1;
            Console.WriteLine("Vecka : " + veckonummer);
            using (var db = new MyDBContext())
            {
                var rum = (from t in db.Rooms
                           select t);
                Console.Write("Rum");
                foreach (var rumnummer in rum)
                {
                    Console.Write($"       {rumnummer.Id}");
                }
                Console.WriteLine();
                VisaVecka("Måndag", veckonummer);
                VisaVecka("Tisdag", veckonummer);
                VisaVecka("Onsdag", veckonummer);
                VisaVecka("Torsdag", veckonummer);
                VisaVecka("Fredag", veckonummer);
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
        static void VisaCase()
        {
            foreach (int i in Enum.GetValues(typeof(LäggTill)))
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
                    VisaRum();
                    break;
                case Visa.Sällskap:
                    VisaSällskap();
                    break;
                case Visa.Kalender:
                    VisaKalender();
                    break;
                case Visa.AdminKonton:
                    VisaAdminKonton(); //ej klar
                    break;
                case Visa.Avsluta:
                    break;
            }
        }
        static void LäggTillRumIKalendern()
        {
            Console.WriteLine("AngeRumId");
            int rumId = int.Parse(Console.ReadLine());
            for (int i = 1; i <= 52; i++) //veckonummer
            {
                CreateNewEntries(rumId, "Måndag", i, true);
                CreateNewEntries(rumId, "Tisdag", i, true);
                CreateNewEntries(rumId, "Onsdag", i, true);
                CreateNewEntries(rumId, "Torsdag", i, true);
                CreateNewEntries(rumId, "Fredag", i, true);
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
                    LäggTillRumIKalendern();
                    break;
                case LäggTill.Aktivitet:
                    LäggtillAktivitet();
                    break;
                case LäggTill.Adminkonto:
                    LäggtillAdminkonto();
                    break;
                case LäggTill.Avsluta:
                    break;
            }
        }
        static void LäggtillAktivitet()
        {
            Console.WriteLine("Vad heter aktiviteten?");
            string namn = Console.ReadLine();
            using (var db = new MyDBContext())
            {
                var nyaktivitet = new Aktivitet()
                {
                    Name = namn
                };
                var aktivitetslista = db.Aktiviteter;
                aktivitetslista.Add(nyaktivitet);
                db.SaveChanges();
                Console.Clear();
                Console.WriteLine("Aktivitet tillagd!");
                Console.ReadKey();
            }
        }
        static void LäggtillAdminkonto()
        {
            Console.WriteLine("Ange namn");
            string namn = Console.ReadLine();
            Console.WriteLine("Ange lösen");
            string lösen = Console.ReadLine();
            using (var db = new MyDBContext())
            {
                var nyttkonto = new AdminKonto()
                {
                    Namn = namn,
                    Lösen = lösen
                };
                var kontolista = db.AdminKonton;
                kontolista.Add(nyttkonto);
                db.SaveChanges();
                Console.Clear();
                Console.WriteLine("Konto Tillagd!");
                Console.ReadKey();
            }
        }
        static void VisaRum()
        {
            using (var db = new MyDBContext())
            {
                var rum = (from t in db.Rooms
                           select t);
                foreach (var t in rum)
                {
                    Console.WriteLine($"Rum nummer: {t.Id} Antal bord: {t.Bord} Antal stolar: {t.Stolar} Antal tv: {t.TV}");
                }
            }
        }
        static void VisaAdminKonton()
        {
            using (var db = new MyDBContext())
            {
                var konton = (from t in db.AdminKonton
                           select t);
                foreach (var t in konton)
                {
                    Console.WriteLine($"Id: {t.Id} Namn: {t.Namn} Lösen: {t.Lösen} ");
                }
            }
        } 
        static void VisaSällskap()
        {
            using (var db = new MyDBContext())
            {
                var sällskap = (from t in db.Sällskaper
                              select t);

                foreach (var t in sällskap)
                {
                    var aktivitet = (from a in db.Aktiviteter
                                     where t.AktivitetId == a.Id
                                    select a).SingleOrDefault();

                    Console.WriteLine($"Id: {t.Id} Namn: {t.Namn} Antal i sällskapet: {t.Antal_i_sällskapet} Planerad aktivitet: {aktivitet.Name}");
                }
            }
        }
        static void VisaAktiviteter()
        {
            using (var db = new MyDBContext())
            {
                var aktiviteter = (from t in db.Aktiviteter
                                   select t);

                foreach (var t in aktiviteter)
                {

                    Console.WriteLine($"Id: {t.Id} Namn: {t.Name}");
                }
            }
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
                    UppdateraSällskap();
                        break;
                    case Uppdatera.Aktivitet:
                    UppdateraAktiviteter();
                        break;
                case Uppdatera.Rum:
                    UppdateraRum();
                        break;
                    case Uppdatera.Adminkonto:
                    UppdateraAdminkonto();
                        break;
                }
            }
        static void UppdateraAktiviteter()
        {
            VisaAktiviteter();
            Console.WriteLine("Välj aktivitet att ändra");
            var input = int.Parse(Console.ReadLine());
            using (var db = new MyDBContext())
            {
                var aktivitet = (from t in db.Aktiviteter
                                where t.Id == input
                                select t).SingleOrDefault();
                Console.WriteLine("Namn: ");
                var namn= Console.ReadLine();
                aktivitet.Name= namn;
                db.SaveChanges();
                Console.Clear();
                Console.WriteLine("Lyckad uppdatering av aktivitet");
                Console.ReadKey();
                Console.Clear();
            }
        }
        static void UppdateraAdminkonto()
        {
            VisaAdminKonton();
            Console.WriteLine("Välj konto att ändra");
            var input = int.Parse(Console.ReadLine());
            using (var db = new MyDBContext())
            {
                var konto = (from t in db.AdminKonton
                                 where t.Id == input
                                 select t).SingleOrDefault();
                Console.WriteLine("Namn: ");
                var namn = Console.ReadLine();
                konto.Namn = namn;
                Console.WriteLine("Lösen: ");
                var lösen = Console.ReadLine();
                konto.Lösen = lösen;
                db.SaveChanges();
                Console.Clear();
                Console.WriteLine("Lyckad uppdatering av konto");
                Console.ReadKey();
                Console.Clear();
            }
        }
        static void UppdateraSällskap()
        {
            VisaSällskap();
            Console.WriteLine("Välj sällskap att ändra");
            var input = int.Parse(Console.ReadLine());
            using (var db = new MyDBContext())
            {
                var sällskap = (from t in db.Sällskaper
                                where t.Id == input
                                select t).SingleOrDefault();


                Console.WriteLine("Namn: ");
                var Namn = Console.ReadLine();
                sällskap.Namn = Namn;
                Console.WriteLine("Antal i sällskapet: ");
                var antal = int.Parse(Console.ReadLine());
                sällskap.Antal_i_sällskapet = antal;
                Console.WriteLine("Välj aktivitetId: ");
                var aktivitet = int.Parse(Console.ReadLine());
                sällskap.AktivitetId = aktivitet;
                db.SaveChanges();
                Console.Clear();
                Console.WriteLine("Lyckad uppdatering av sällskap");
                Console.ReadKey();
                Console.Clear();
            }
        }
        static void UppdateraRum()
        {
            VisaRum();
            Console.WriteLine("Välj rum att ändra");
            var input = int.Parse(Console.ReadLine());
            using (var db = new MyDBContext())
            {
                var rum = (from t in db.Rooms
                           where t.Id == input
                           select t).SingleOrDefault();


                Console.WriteLine("Antal TV: ");
                var TV = int.Parse(Console.ReadLine());
                rum.TV = input;
                Console.WriteLine("Antal bord: ");
                var bord = int.Parse(Console.ReadLine());
                rum.Bord = bord;
                Console.WriteLine("Antal stolar: ");
                var stolar = int.Parse(Console.ReadLine());
                rum.Bord = bord;

                db.SaveChanges();
                Console.Clear();
                Console.WriteLine("Lyckad uppdatering av rum");
                Console.ReadKey();
                Console.Clear();
            }
        }
        static void TaBortRum()
            {
                using (var db = new MyDBContext())
                {
                    var rooms = (from t in db.Rooms
                                 select t);
                    foreach (var room in rooms)
                    {
                        Console.WriteLine($"Room #{room.Id}");
                    }
                }

                Console.Write("Ange Id att ta bort: ");
                int roomDelete;
                if (int.TryParse(Console.ReadKey(true).KeyChar.ToString(), out roomDelete))
                {
                    using (var db = new MyDBContext())
                    {
                        var deleteProdukt = (from t in db.Rooms
                                             where t.Id == roomDelete
                                             select t).SingleOrDefault();
                        if (deleteProdukt != null)
                        {
                            db.Rooms.Remove((Rum)deleteProdukt);
                            db.SaveChanges();
                            Console.Clear();
                            Console.WriteLine("Rummet har tagits bort");
                            Console.ReadKey();
                            Console.Clear();
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Existerar inte");
                            Console.ReadKey();
                            Console.Clear();
                        }
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Fel inmatning");
                    Console.ReadKey();
                    Console.Clear();
                }
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
                switch (menu)
                {
                    case TaBort.Rum:
                        TaBortRum();
                        break;
                    case TaBort.Aktivitet:
                        TabortAktivitet();
                        break;
                    case TaBort.Sällskap:
                        break;
                }
            }
        }
        static void TabortAktivitet()
        {
            using (var db = new MyDBContext())
            {
                var aktiviteter = (from t in db.Aktiviteter
                             select t);
                foreach (var aktivitet in aktiviteter)
                {
                    Console.WriteLine($"Aktiviteter #{aktivitet.Id} {aktivitet.Name}");
                }
            }

            Console.Write("Ange Id att ta bort: ");
            int aktivitetDelete;
            if (int.TryParse(Console.ReadKey(true).KeyChar.ToString(), out aktivitetDelete))
            {
                using (var db = new MyDBContext())
                {
                    var deleteProdukt = (from t in db.Rooms
                                         where t.Id == aktivitetDelete
                                         select t).SingleOrDefault();
                    if (deleteProdukt != null)
                    {
                        db.Rooms.Remove((Rum)deleteProdukt);
                        db.SaveChanges();
                        Console.Clear();
                        Console.WriteLine("Rummet har tagits bort");
                        Console.ReadKey();
                        Console.Clear();
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Existerar inte");
                        Console.ReadKey();
                        Console.Clear();
                    }
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Fel inmatning");
                Console.ReadKey();
                Console.Clear();
            }
        }

    }
    }

    



    