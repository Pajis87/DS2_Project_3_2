using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace DS2_Project_3.orm.dao {
    public static class TransakceUPDAO {
        public static bool MojeTransakceUP(Database pDb, int id_uzivatele, int id_vycviku, int[] id_psu, string? kupon) {
            Database db = Database.Connect(pDb);

            OracleCommand command = db.CreateCommand("MojeTransakce");
            command.CommandType = CommandType.StoredProcedure;

            OracleParameter p1 = new OracleParameter("p_id_zakaznika", id_uzivatele);
            OracleParameter p2 = new OracleParameter("p_id_vycviku", id_vycviku);


            OracleParameter p3 = new OracleParameter("p_id_psu", OracleDbType.Int32);
            p3.Direction = ParameterDirection.Input;

            p3.CollectionType = OracleCollectionType.PLSQLAssociativeArray;
            p3.Value = id_psu;
            p3.Size = id_psu.Length;


            OracleParameter p4 = new OracleParameter("p_kupon", kupon);
            command.Parameters.Add(p1);
            command.Parameters.Add(p2);
            command.Parameters.Add(p3);
            command.Parameters.Add(p4);

            try {
                db.ExecuteNonQuery(command);
            } catch (Exception ex) {
                Console.WriteLine("Error MojeTransakce: " + ex.Message.Split('\n')[0]);
                Database.Close(pDb, db);
                return false;
            }


            Database.Close(pDb, db);
            return true;
        }
    }
}
