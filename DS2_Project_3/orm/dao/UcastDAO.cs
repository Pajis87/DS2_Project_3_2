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
        private static string SqlZiskejPocetUcastNaVycviku =
        """
            SELECT
                count(*)
            FROM
                nf_ucast
            WHERE
                vycvik = :vycvik
        """;
        private static string SqlZiskejPocetKuponu =
        """
            SELECT
                count(*)
            FROM
                nf_ucast
            WHERE
                kupon = :kupon
        """;
        private static string SqlPridejNovouUcast =
        """
            INSER INTO nf_ucast(stav, vycvik, pes, kupon, celkovaCena)
            OUTPUT Inserted.ucId
            VALUES (:stav, :vycvik, :pes, :kupon, :celkovaCena)
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

        public static int ZiskejPocetUcastiNaVycviku(Database pDb, int vId) {
            Database db = Database.Connect(pDb);
            OracleCommand command = db.CreateCommand(SqlZiskejPocetUcastNaVycviku);
            command.Parameters.Add(":vycvik", vId);

            int result = db.ExecuteScalar(command);

            Database.Close(pDb, db);
            return result;
        }
        public static int ZiskejPocetKuponu(Database pDb, string kupon) {
            Database db = Database.Connect(pDb);
            OracleCommand command = db.CreateCommand(SqlZiskejPocetKuponu);
            command.Parameters.Add(":kupon", kupon);

            int result = db.ExecuteScalar(command);

            Database.Close(pDb, db);
            return result;
        }
        public static int PridejNovouUcast(Database pDb, UcastDTO ucast) {
            Database db = Database.Connect(pDb);
            OracleCommand command = db.CreateCommand(SqlPridejNovouUcast);
            command.Parameters.Add(":stav", ucast.Stav);
            command.Parameters.Add(":vycvik", ucast.Vycvik);
            command.Parameters.Add(":pes", ucast.Pes);
            command.Parameters.Add(":kupon", ucast.Kupon);
            command.Parameters.Add(":celkovaCena", ucast.CelkovaCena);

            int id = db.ExecuteNonQuery(command);

            Database.Close(pDb, db);
            return id;
        }
    }
}
