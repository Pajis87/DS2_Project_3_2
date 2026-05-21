using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace DS2_Project_3.orm.dao {
    public static class TransakceUPDAO {
        public static List<int> MojeTransakceUP(int id_uzivatele, int id_vycviku, int[] id_psu, string? kupon) {
            List<int> id_pridanych = new List<int>();

            Database db = new Database();
            db.Connect();

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

            OracleParameter pRet = new OracleParameter("p_id_pridanych", OracleDbType.Int32);
            pRet.Direction = ParameterDirection.Output;
            pRet.CollectionType = OracleCollectionType.PLSQLAssociativeArray;
            pRet.Size = id_psu.Length;

            command.Parameters.Add(pRet);

            try {
                db.ExecuteNonQuery(command);

                id_pridanych = ((Oracle.ManagedDataAccess.Types.OracleDecimal[])pRet.Value).Select(x => (int)x.Value).ToList();

                Console.WriteLine("ID nově vložených účastí: [");
                foreach (int id in id_pridanych) {
                    Console.WriteLine("\t" + id);
                }
                Console.WriteLine("]");

            } catch (Exception ex) {
                Console.WriteLine("ERROR: " + ex.Message.Split('\n')[0]);
                db.Close();
                return id_pridanych;
            }
            db.Close();
            return id_pridanych;
        }
    }
}
