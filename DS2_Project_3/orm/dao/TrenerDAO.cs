using Oracle.ManagedDataAccess.Client;

namespace DS2_Project_3.orm.dao {
    public class TrenerDAO {
        private static string SqlZiskejPodleId =
        """
            SELECT
                tId,
                jmeno
            FROM nf_trener
            WHERE
                tId = :tId
        """;


        public static TrenerDTO? ZiskejPodleId(Database pDb, int tId) {
            Database db = Database.Connect(pDb);
            OracleCommand command = db.CreateCommand(SqlZiskejPodleId);
            command.Parameters.Add(":tId", tId);
            OracleDataReader reader = db.Select(command);

            List<TrenerDTO> list = new List<TrenerDTO>();
            while (reader.Read()) {
                TrenerDTO trener = new TrenerDTO();
                trener.tId = reader.GetInt32(0);
                trener.jmeno = reader.GetString(1);
                list.Add(trener);
            }
            reader.Close();

            Database.Close(pDb, db);
            
            if (list.Count == 0)
                return null;
            return list[0];
        }
    }
}
