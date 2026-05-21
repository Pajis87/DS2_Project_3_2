using Oracle.ManagedDataAccess.Client;

namespace DS2_Project_3.orm.dao {
    public class TrenerDAO {
        private static string SqlZiskejPodleId =
        """
            SELECT
                tId,
                jmeno,
                prijmeni,
                telefon,
                email,
                datumRegistrace,
                aktivni,
                cenaZaHodinu
            FROM nf_trener
            WHERE
                tId = :tId
        """;


        public static TrenerDTO? ZiskejPodleId(Database db, int tId) {
            OracleCommand command = db.CreateCommand(SqlZiskejPodleId);
            command.Parameters.Add("tId", tId);
            OracleDataReader reader = db.Select(command);

            List<TrenerDTO> list = new List<TrenerDTO>();
            while (reader.Read()) {
                TrenerDTO trener = new TrenerDTO();
                trener.tId = reader.GetInt32(0);
                trener.jmeno = reader.GetString(1);
                trener.prijmeni = reader.GetString(2);
                trener.telefon = reader.GetString(3);
                trener.email = reader.GetString(4);
                trener.datumRegistrace = reader.GetDateTime(5);
                trener.aktivni = reader.GetBoolean(6);
                trener.cenaZaHodinu = reader.GetDouble(7);
                list.Add(trener);
            }
            reader.Close();
            
            if (list.Count == 0)
                return null;
            return list[0];
        }
    }
}
