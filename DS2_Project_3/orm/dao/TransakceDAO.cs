using Oracle.ManagedDataAccess.Client;

namespace DS2_Project_3.orm.dao {
    public static class TransakceDAO {
        public static bool MojeTransakce(Database pDb, int user_id, int traning_id, int[] dog_ids, string coupon) {
            Database db = Database.Connect(pDb);
            bool ret = true;
            try {
                db.BeginTransaction();

                VycvikDTO? vycvik = VycvikDAO.ZiskejPodleId(pDb, traning_id);
                if (vycvik == null) {
                    throw new Exception("Výcvik s tímto ID neexistuje.");
                }

                TrenerDTO? trener = TrenerDAO.ZiskejPodleId(pDb, vycvik.trenerId);
                if (trener == null) {
                    throw new Exception("U výcviku je nastavené ID trenéra, který neexistuje.");
                }

                int pocetVolnychMist = vycvik.pocetMist - UcastDAO.ZisekPocetUcastiNaVycviku(pDb, traning_id);
                if (pocetVolnychMist != vycvik.pocetMist) {
                    Console.WriteLine("Počet volných míst (redundantí atribut) u výcviku byl nastaven špatně.");
                    vycvik.pocetMist = pocetVolnychMist;
                }

                if (DateTime.Now > vycvik.cas_od) {
                    throw new Exception("Vybraný výcvik již proběhl, nejde se na něj přihlásit.");
                }


                db.EndTransaction();
            } catch (Exception e) {
                Console.Error.WriteLine("ERROR: " + e.Message);
                db.Rollback();
                ret = false;
            } finally {
                Database.Close(pDb, db);
            }
            return ret;
        }



    }
}
