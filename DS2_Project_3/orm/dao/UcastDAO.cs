using Oracle.ManagedDataAccess.Client;
using System.Data;

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
        private static string SqlZiskejPocetUcastiNaVycviku =
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
            INSERT INTO nf_ucast(stav, vycvik, pes, kupon, celkovaCena)
            VALUES (:stav, :vycvik, :pes, :kupon, :celkovaCena)
            RETURNING ucId INTO :id
        """;

        public static List<UcastDTO> ZiskejPodleVycviku(Database db, int vId) {
            OracleCommand command = db.CreateCommand(SqlZiskejPodleVycviku);
            command.Parameters.Add("vycvik", vId);
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

            return list;
        }

        public static int ZiskejPocetUcastiNaVycviku(Database db, int vId) {
            OracleCommand command = db.CreateCommand(SqlZiskejPocetUcastiNaVycviku);
            command.Parameters.Add("vycvik", vId);

            int result = db.ExecuteScalar(command);
            return result;
        }
        public static int ZiskejPocetKuponu(Database db, string kupon) {
            OracleCommand command = db.CreateCommand(SqlZiskejPocetKuponu);
            command.Parameters.Add("kupon", kupon);

            int result = db.ExecuteScalar(command);

            return result;
        }
        public static int PridejNovouUcast(Database db, UcastDTO ucast) {
            OracleCommand command = db.CreateCommand(SqlPridejNovouUcast);
            command.Parameters.Add("stav", ucast.Stav);
            command.Parameters.Add("vycvik", ucast.Vycvik);
            command.Parameters.Add("pes", ucast.Pes);
            command.Parameters.Add("kupon", ucast.Kupon);
            command.Parameters.Add("celkovaCena", ucast.CelkovaCena);

            OracleParameter id = new OracleParameter();
            id.ParameterName = "id";
            id.OracleDbType = OracleDbType.Int64;
            id.Direction = ParameterDirection.Output;

            command.Parameters.Add(id);

            db.ExecuteNonQuery(command);

            return Convert.ToInt32(id.Value.ToString());
        }
    }
}
