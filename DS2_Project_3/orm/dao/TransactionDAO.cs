using Oracle.ManagedDataAccess.Client;

namespace DS2_Project_3.orm.dao {
    public static class TransactionDAO {
        private static string SqlSelectTrainingInfo =
        """
        
            SELECT
                od,
                do,
                pocetVolnychMist,
                cenaZaHodinu
            FROM
                vycvik v INNER JOIN
                trener t ON ( v . trener = t . tId )
            WHERE vId = @training_id
        
        """;



        public static bool MojeTransakce(Database pDb, int user_id, int traning_id, int[] dog_ids, string coupon) {
            Database db = Database.Connect(pDb);
            bool ret = true;
            try {
                db.BeginTransaction();

                VycvikDTO? vycvik = VycvikDAO.ZiskejPodleId(pDb, traning_id);

                if (vycvik == null) {
                    Console.WriteLine("Spatny trener ve vycviku"); // TODO: change all error messages, aalso change to throw
                    return false;
                }

                TrenerDTO trener = TrenerDAO.ZiskejPodleId(pDb, vycvik.trenerId)[0]; // TODO: move [0] inside the function
                                                                                     // TODO: move [0] inside the function 
                                                                                     // TODO: move [0] inside the function
                                                                                     // TODO: move [0] inside the function
                                                                                     // TODO: move [0] inside the function
                                                                                     // TODO: move [0] inside the function


                if (vycvik.cas_od > DateTime.Now) {
                    Console.WriteLine("Vycvyk jiz zacal");
                    return false;
                }


                db.EndTransaction();
            } catch (OracleException) {
                db.Rollback();
                ret = false;
            } // TODO: check if finally is also called on throw exception
            // TODO: check if finally is also called on throw exception
            // TODO: check if finally is also called on throw exception
            // TODO: check if finally is also called on throw exception
            // TODO: check if finally is also called on throw exception
            // TODO: check if finally is also called on throw exception
            // TODO: check if finally is also called on throw exception
            // TODO: check if finally is also called on throw exception
            // TODO: check if finally is also called on throw exception
            // TODO: check if finally is also called on throw exception
            // TODO: check if finally is also called on throw exception
            // TODO: check if finally is also called on throw exception
            // TODO: check if finally is also called on throw exception
            // TODO: check if finally is also called on throw exception
            // TODO: check if finally is also called on throw exception
            Database.Close(pDb, db);
            return ret;
        }



    }
}
