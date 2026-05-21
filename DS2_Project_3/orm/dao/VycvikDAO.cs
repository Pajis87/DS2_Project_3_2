using Oracle.ManagedDataAccess.Client;

namespace DS2_Project_3.orm.dao {
    public class VycvikDAO {
        private static string SqlZiskejPodleId =
        """
            SELECT
                vId,
                poznamky,
                od,
                do,
                pocetMist,
                pocetVolnychMist,
                trener
            FROM nf_vycvik
            WHERE
                vId = :vId
        """;

        public static VycvikDTO? ZiskejPodleId(Database db, int vId) {
            OracleCommand command = db.CreateCommand(SqlZiskejPodleId);
            command.Parameters.Add("vId", vId);
            OracleDataReader reader = db.Select(command);

            List<VycvikDTO> list = new List<VycvikDTO>();
            while (reader.Read()) {
                VycvikDTO vycvik = new VycvikDTO();
                vycvik.vId = reader.GetInt32(0);
                if (!reader.IsDBNull(1)) {
                    vycvik.poznamky = reader.GetString(1);
                }
                vycvik.cas_od = reader.GetDateTime(2);
                vycvik.cas_do = reader.GetDateTime(3);
                vycvik.pocetMist = reader.GetInt32(4);
                vycvik.pocetVolnychMist = reader.GetInt32(5);
                vycvik.trenerId = reader.GetInt32(6);
                list.Add(vycvik);
            }
            reader.Close();

            if (list.Count == 0)
                return null;
            return list[0];
        }
    }
}
