using Oracle.ManagedDataAccess.Client;

namespace DS2_Project_3.orm.dao {
    public static class TransakceUPDAO {
        public static bool MojeTransakceUP(Database pDb, int user_id, int traning_id, int[] dog_ids, string coupon) {
            Database db = Database.Connect(pDb);
            Database.Close(pDb, db);
            return false;
        }
    }
}
