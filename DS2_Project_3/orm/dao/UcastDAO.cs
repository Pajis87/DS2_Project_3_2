using Oracle.ManagedDataAccess.Client;

namespace DS2_Project_3 {
    public class UcastDAO {
        private static string SqlZiskejPodleVycviku =
        """
            SELECT
                ucId,
                stav,
                vycvik,
                pes,
                celkovaCena
            FROM nf_ucast
            WHERE
                vycvik = :vycvik
        """;
        private static string SqlZisekPocetUcastNaVycviku =
        """
            SELECT
                count(*)
            FROM
                nf_ucast
            WHERE
                vycvik = :vycvik
        """;

        public static List<UcastDTO> ZiskejPodleVycviku(Database pDb, int vId) {
            Database db = Database.Connect(pDb);
            OracleCommand command = db.CreateCommand(SqlZiskejPodleVycviku);
            command.Parameters.Add(":vycvik", vId);
            OracleDataReader reader = db.Select(command);

            List<UcastDTO> list = new List<UcastDTO>();
            while (reader.Read()) {
                UcastDTO ucast = new UcastDTO();
                ucast.UcId = reader.GetInt32(0);
                ucast.Stav = reader.GetInt32(1);
                ucast.Vycvik = reader.GetInt32(2);
                ucast.Pes = reader.GetInt32(2);
                ucast.CelkovaCena = reader.GetDouble(0);
                list.Add(ucast);
            }
            reader.Close();

            Database.Close(pDb, db);
            return list;
        }

        public static int ZisekPocetUcastiNaVycviku(Database pDb, int vId) {
            Database db = Database.Connect(pDb);
            OracleCommand command = db.CreateCommand(SqlZisekPocetUcastNaVycviku);
            command.Parameters.Add(":vycvik", vId);

            int result = db.ExecuteScalar(command);

            Database.Close(pDb, db);
            return result;
        }
    }
}
