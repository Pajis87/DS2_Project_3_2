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

        public static VycvikDTO? ZiskejPodleId(Database pDb, int tId) {
            Database db = Database.Connect(pDb);
            OracleCommand command = db.CreateCommand(SqlZiskejPodleId);
            command.Parameters.Add(":tId", tId);
            OracleDataReader reader = db.Select(command);

            List<VycvikDTO> list = new List<VycvikDTO>();
            while (reader.Read()) {
                VycvikDTO vycvik = new VycvikDTO();
                vycvik.vId = reader.GetInt32(0);
                vycvik.poznamky = reader.GetString(1);
                vycvik.cas_od = reader.GetDateTime(2);
                vycvik.cas_do = reader.GetDateTime(2);
                vycvik.pocetMist = reader.GetInt32(0);
                vycvik.pocetVolnychMist = reader.GetInt32(0); // TODO: nepouzivat pocet volnych mist, ale udelat si select na pocet, to same v pl/sql
                                                              // TODO: nepouzivat pocet volnych mist, ale udelat si select na pocet, to same v pl/sql
                                                              // TODO: nepouzivat pocet volnych mist, ale udelat si select na pocet, to same v pl/sql
                                                              // TODO: nepouzivat pocet volnych mist, ale udelat si select na pocet, to same v pl/sql
                                                              // TODO: nepouzivat pocet volnych mist, ale udelat si select na pocet, to same v pl/sql
                                                              // TODO: nepouzivat pocet volnych mist, ale udelat si select na pocet, to same v pl/sql
                                                              // TODO: nepouzivat pocet volnych mist, ale udelat si select na pocet, to same v pl/sql
                                                              // TODO: nepouzivat pocet volnych mist, ale udelat si select na pocet, to same v pl/sql
                                                              // TODO: nepouzivat pocet volnych mist, ale udelat si select na pocet, to same v pl/sql
                                                              // TODO: nepouzivat pocet volnych mist, ale udelat si select na pocet, to same v pl/sql
                                                              // TODO: nepouzivat pocet volnych mist, ale udelat si select na pocet, to same v pl/sql
                                                              // TODO: nepouzivat pocet volnych mist, ale udelat si select na pocet, to same v pl/sql
                                                              // TODO: nepouzivat pocet volnych mist, ale udelat si select na pocet, to same v pl/sql
                                                              // TODO: nepouzivat pocet volnych mist, ale udelat si select na pocet, to same v pl/sql
                                                              // TODO: nepouzivat pocet volnych mist, ale udelat si select na pocet, to same v pl/sql
                                                              // TODO: nepouzivat pocet volnych mist, ale udelat si select na pocet, to same v pl/sql
                vycvik.trenerId = reader.GetInt32(0);
                list.Add(vycvik);
            }
            reader.Close();

            Database.Close(pDb, db);

            if (list.Count == 0)
                return null;
            return list[0];
        }
    }
}
