using Oracle.ManagedDataAccess.Client;

namespace DS2_Project_3.orm.dao {
    public class PesDAO {
        private static string SqlZiskejPodleId =
        """
            SELECT
                pId,
                jmeno,
                plemeno,
                datumNarozeni,
                poznamky,
                majitel,
                aktivni
            FROM nf_pes
            WHERE
                pId = :pId
        """;

        public static PesDTO? ZiskejPodleId(Database db, int pId) {
            OracleCommand command = db.CreateCommand(SqlZiskejPodleId);
            command.Parameters.Add("pId", pId);
            OracleDataReader reader = db.Select(command);

            List<PesDTO> list = new List<PesDTO>();
            while (reader.Read()) {
                PesDTO pes = new PesDTO();
                pes.PId = reader.GetInt32(0);
                pes.Jmeno = reader.GetString(1);
                pes.Plemeno = reader.GetString(2);
                pes.DatumNarozeni = reader.GetDateTime(3);
                if (!reader.IsDBNull(4)) {
                    pes.Poznamky = reader.GetString(4);
                }
                pes.MajitelId = reader.GetInt32(5);
                pes.Aktivni = reader.GetInt32(6);
                list.Add(pes);
            }
            reader.Close();

            if (list.Count == 0)
                return null;
            return list[0];
        }
    }
}
