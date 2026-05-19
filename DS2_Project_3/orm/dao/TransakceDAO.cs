using Oracle.ManagedDataAccess.Client;
using System.Security.Cryptography;

namespace DS2_Project_3.orm.dao {
    public static class TransakceDAO {
        private static string SqlZiskejKolidujiciUcasti =
        """
            SELECT
                count(1)
            FROM
                ucast u INNER JOIN
                vycvik v ON (u. vycvik = v. vid)
            WHERE
                u. pes = :pId AND
                v. od < :od AND v. do > :do 
        """;


        public static List<int> MojeTransakce(Database pDb, int id_uzivatele, int id_vycviku, int[] id_psu, string? kupon) {
            Database db = Database.Connect(pDb);
            List<int> nove_ucasti = new List<int>();
            try {
                db.BeginTransaction();

                VycvikDTO? vycvik = VycvikDAO.ZiskejPodleId(pDb, id_vycviku);
                if (vycvik == null) {
                    throw new Exception("Výcvik s tímto ID neexistuje.");
                }

                TrenerDTO? trener = TrenerDAO.ZiskejPodleId(pDb, vycvik.trenerId);
                if (trener == null) {
                    throw new Exception("U výcviku je nastavené ID trenéra, který neexistuje.");
                }

                int pocetVolnychMist = vycvik.pocetMist - UcastDAO.ZiskejPocetUcastiNaVycviku(pDb, id_vycviku);
                if (pocetVolnychMist != vycvik.pocetMist) {
                    Console.WriteLine("Počet volných míst (redundantí atribut) u výcviku je nastaven špatně.");
                }

                if (DateTime.Now > vycvik.cas_od) {
                    throw new Exception("Vybraný výcvik již proběhl, nejde se na něj přihlásit.");
                }

                if (id_psu.Length > pocetVolnychMist) {
                    throw new Exception("Na vybraném výcviku není dostatek volných míst.");
                }

                if (kupon != null && UcastDAO.ZiskejPocetKuponu(pDb, kupon) > 0) {
                    throw new Exception("Kupon je již použit");
                }

                foreach (int id_psa in id_psu) {
                    PesDTO? pes = PesDAO.ZiskejPodleId(pDb, id_psa);
                    if (pes == null) {
                        throw new Exception("Pes s ID " + id_psa + " neexistuje.");
                    }

                    if  (pes.MajitelId != id_uzivatele) {
                        throw new Exception("Uživatel s ID " + id_uzivatele + " není majitelem psa s ID " + id_psa + " (" + pes.Jmeno  + ").");
                    }

                    int pocet = ZiskejKolidujiciUcasti(pDb, pes, vycvik);
                    if (pocet > 0) {
                        throw new Exception("Pes s ID " + id_psa + " (" + pes.Jmeno + ") je v době výcviku již zapsán na jiný výcvik.");
                    }

                    UcastDTO ucast = new UcastDTO {
                        Stav = 0,
                        Vycvik = vycvik.vId,
                        Pes = pes.PId,
                        Kupon = kupon,
                        CelkovaCena = (vycvik.cas_do - vycvik.cas_od).Hours * trener.cenaZaHodinu
                    };
                    nove_ucasti.Add(UcastDAO.PridejNovouUcast(pDb, ucast));
                }


                db.EndTransaction();
            } catch (Exception e) {
                Console.Error.WriteLine("ERROR: " + e.Message);
                db.Rollback();
            } finally {
                Database.Close(pDb, db);
            }

            Console.WriteLine("ID nově vložených účastí: [");
            foreach (int id in nove_ucasti) {
                Console.WriteLine("\t" + id);
            }
            Console.WriteLine("]");
            return nove_ucasti;
        }

        public static int ZiskejKolidujiciUcasti(Database pDb, PesDTO pes, VycvikDTO vycvik) {
            Database db = Database.Connect(pDb);
            OracleCommand command = db.CreateCommand(SqlZiskejKolidujiciUcasti);
            command.Parameters.Add(":pId", pes.PId);
            command.Parameters.Add(":od", vycvik.cas_od);
            command.Parameters.Add(":do", vycvik.cas_do);
            OracleDataReader reader = db.Select(command);

            int pocet = 0;
            while (reader.Read()) pocet++;
            reader.Close();

            Database.Close(pDb, db);
            return pocet;
        }
    }
}
