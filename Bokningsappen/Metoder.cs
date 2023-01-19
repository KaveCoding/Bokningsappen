using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bokningsappen.Models;
using Microsoft.Data.SqlClient;
using Dapper;

namespace Bokningsappen
{
    internal class Metoder
    {
        public class LäggTill : Metoder
        {
        public static void LäggTillRumIKalendern()
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
        public static void LäggtillAktivitet()
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
        public static void LäggtillAdminkonto()
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
        public static void CreateNewEntries(int RumId, string veckodag, int veckonummer, bool tillgänglig)
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
        public static void CreateNewRoom()
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

        }
        public class Visa : Metoder
        {

        public static void VisaVecka(string Veckodag, int veckonummer)
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
        public static void VisaKalender()
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
        public static void VisaRum()
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
        public static void VisaAdminKonton()
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
        public static void VisaSällskap()
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
        public static void VisaAktiviteter()
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


            public static void Queries()
            {
                string connString = "Server=tcp:eliasanghnaeh.database.windows.net,1433;Initial Catalog=WebbshoppGrupp8Eskilstuna;Persist Security Info=False;User ID=Group8;Password=Ourpasswordis100%secure;MultipleActiveResultSets=True;Encrypt=True;TrustServerCertificate=False;Connection Timeout=5;";

                
                {

                    using (var connection = new SqlConnection(connString))
                    {
                        var flestbokningar = $"SELECT Namn, COUNT(AktivitetId) AS Antal FROM dbo.sällskaper s join Aktiviteter a on s.AktivitetId = a.Id Group By namn ORDER BY Antal desc";

                        var flestbokningarlista = new List<QueryClass>();
                        connection.Open();
                        flestbokningarlista = connection.Query<QueryClass>(flestbokningar).ToList();
                        Console.WriteLine("Person med flest bokningar");
                        foreach (var x in flestbokningarlista)
                        {
                            Console.WriteLine($"Namn: {x.Namn} Antal bokningar: {x.Antal}");
                        }
                        Console.WriteLine("----------------------------------------------");

                        
                        var populärastAktivitet = $"SELECT a.Name as Namn, count(a.Name) as Antal FROM dbo.sällskaper s join Aktiviteter a on s.AktivitetId = a.Id group by a.Name order by Antal desc";
                       
                        var populärastAktivitetlista = new List<QueryClass>();
                        populärastAktivitetlista = connection.Query<QueryClass>(populärastAktivitet).ToList();
                        Console.WriteLine("Aktiviteter med flest bokningar");
                        foreach (var x in populärastAktivitetlista)
                        {
                            Console.WriteLine($"Aktivitet: {x.Namn} Antal bokningar: {x.Antal}");
                        }
                        Console.WriteLine("----------------------------------------------");

                        connection.Close();
                    }
                  







                }
            }
        }



        public class Uppdatera : Metoder
        {
            public static void UppdateraAktiviteter()
                {
                    Visa.VisaAktiviteter();
                    Console.WriteLine("Välj aktivitet att ändra");
                    var input = int.Parse(Console.ReadLine());
                    using (var db = new MyDBContext())
                    {
                        var aktivitet = (from t in db.Aktiviteter
                                         where t.Id == input
                                         select t).SingleOrDefault();
                        Console.WriteLine("Namn: ");
                        var namn = Console.ReadLine();
                        aktivitet.Name = namn;
                        db.SaveChanges();
                        Console.Clear();
                        Console.WriteLine("Lyckad uppdatering av aktivitet");
                        Console.ReadKey();
                        Console.Clear();
                    }
                }
            public static void UppdateraAdminkonto()
                {
                    Visa.VisaAdminKonton();
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
            public static void UppdateraSällskap()
                {
                    Visa.VisaSällskap();
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
            public static void UppdateraRum()
                {
                    Visa.VisaRum();
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
            public static void BokaTid()
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
                    var tid = (from t in db.Bokningar
                               where t.Veckodag == veckodag && t.Veckonummer == vecka && t.RumId == RumId && t.Tillgänglig == true
                               select t).SingleOrDefault();
                    if (tid != null && tid.Tillgänglig == true)
                    {
                        var senastesällskapiD = (from t in db.Sällskaper orderby t.Id descending select t).First().Id;

                        tid.Tillgänglig = false;
                        tid.SällskapId = senastesällskapiD;
                        db.SaveChanges();
                        Console.Clear();
                        Console.WriteLine("Tid bokad!");
                        Console.ReadKey();
                        Console.Clear();
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Den tiden är tyvärr redan bokad");
                        Console.ReadKey();
                        Console.Clear();
                        var senastesällskap = (from t in db.Sällskaper orderby t.Id descending select t).First();
                        var sällskaper = db.Sällskaper;
                        sällskaper.Remove(senastesällskap);
                        db.SaveChanges();
                    }
                }
            }
        }
     


        public class TaBort : Metoder
        {
            

            public static void TaBortRum()
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

                string inmatning = Console.ReadLine();
                int roomDelete;
                if (int.TryParse(inmatning, out roomDelete))
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

            public static void TabortAktivitet()
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
                string inmatning = Console.ReadLine();
                int aktivitetDelete;
                if (int.TryParse(inmatning, out aktivitetDelete))
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

            public static void TabortSällskap()
            {
                Visa.VisaSällskap();

                Console.Write("Ange Id att ta bort: ");
                string inmatning = Console.ReadLine();
                int sällskapdelete;
                if (int.TryParse(inmatning, out sällskapdelete))
                {
                    using (var db = new MyDBContext())
                    {
                        var deleteProdukt = (from t in db.Sällskaper
                                             where t.Id == sällskapdelete
                                             select t).SingleOrDefault();
                        if (deleteProdukt != null)
                        {
                            db.Sällskaper.Remove((Sällskap)deleteProdukt);
                            db.SaveChanges();
                            Console.Clear();
                            Console.WriteLine("Sällskapet har tagits bort");
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
}

