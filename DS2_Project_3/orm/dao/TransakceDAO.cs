using Oracle.ManagedDataAccess.Client;

namespace DS2_Project_3.orm.dao {
    public static class TransakceDAO {
        private static string SqlZiskejKolidujiciUcasti =
        """
            SELECT
                count(1)
            FROM
                nf_ucast u INNER JOIN
                nf_vycvik v ON (u.vycvik = v.vid)
            WHERE
                u.pes = :pId AND
                v.od < :do AND v.do > :od
        """;


        public static List<int> MojeTransakce(int id_uzivatele, int id_vycviku, int[] id_psu, string? kupon) {
            Database db = new Database();
            db.Connect();

            List<int> nove_ucasti = new List<int>();
            try {
                db.BeginTransaction();

                VycvikDTO? vycvik = VycvikDAO.ZiskejPodleId(db, id_vycviku);
                if (vycvik == null) {
                    throw new Exception("Výcvik s tímto ID neexistuje.");
                }

                TrenerDTO? trener = TrenerDAO.ZiskejPodleId(db, vycvik.trenerId);
                if (trener == null) {
                    throw new Exception("U výcviku je nastavené ID trenéra, který neexistuje.");
                }

                int pocetVolnychMist = vycvik.pocetMist - UcastDAO.ZiskejPocetUcastiNaVycviku(db, id_vycviku);

                if (DateTime.Now > vycvik.cas_od) {
                    throw new Exception("Vybraný výcvik již proběhl, nejde se na něj přihlásit.");
                }

                if (id_psu.Length > pocetVolnychMist) {
                    throw new Exception("Na vybraném výcviku není dostatek volných míst.");
                }

                if (kupon != null && UcastDAO.ZiskejPocetKuponu(db, kupon) > 0) {
                    throw new Exception("Kupon je již použit");
                }

                foreach (int id_psa in id_psu) {
                    PesDTO? pes = PesDAO.ZiskejPodleId(db, id_psa);
                    if (pes == null) {
                        throw new Exception("Pes s ID " + id_psa + " neexistuje.");
                    }

                    if  (pes.MajitelId != id_uzivatele) {
                        throw new Exception("Uživatel s ID " + id_uzivatele + " není majitelem psa s ID " + id_psa + " (" + pes.Jmeno  + ").");
                    }

                    int pocet = ZiskejKolidujiciUcasti(db, pes, vycvik);
                    if (pocet > 0) {
                        throw new Exception("Pes s ID " + id_psa + " (" + pes.Jmeno + ") je v době výcviku již zapsán na jiný výcvik.");
                    }

                    UcastDTO ucast = new UcastDTO {
                        Stav = 1,
                        Vycvik = vycvik.vId,
                        Pes = pes.PId,
                        Kupon = kupon,
                        CelkovaCena = (vycvik.cas_do - vycvik.cas_od).Hours * trener.cenaZaHodinu
                    };
                    nove_ucasti.Add(UcastDAO.PridejNovouUcast(db, ucast));
                }


                db.EndTransaction();

            } catch (Exception e) {
                Console.Error.WriteLine("ERROR: " + e.Message);
                db.Rollback();
                return nove_ucasti;
            } finally {
                db.Close();
            }


            Console.WriteLine("ID nově vložených účastí: [");
            foreach (int id in nove_ucasti) {
                Console.WriteLine("\t" + id);
            }
            Console.WriteLine("]");

            return nove_ucasti;
        }

        public static int ZiskejKolidujiciUcasti(Database db, PesDTO pes, VycvikDTO vycvik) {
            OracleCommand command = db.CreateCommand(SqlZiskejKolidujiciUcasti);
            command.Parameters.Add("pId", pes.PId);
            command.Parameters.Add("do", vycvik.cas_do);
            command.Parameters.Add("od", vycvik.cas_od);

            int pocet = db.ExecuteScalar(command);

            return pocet;
        }
    }
}
