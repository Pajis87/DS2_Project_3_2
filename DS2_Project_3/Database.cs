using System.Configuration;
using System.Data;
using Oracle.ManagedDataAccess.Client;

namespace DS2_Project_3 {
    public class Database {
        private OracleConnection Connection { get; set; }
        private OracleTransaction SqlTransaction { get; set; }
        public string Language { get; set; }

        public Database() {
            Connection = new OracleConnection();
            Language = "en";
        }

        public bool Connect(String conString) {
            if (Connection.State != ConnectionState.Open) {
                Connection.ConnectionString = conString;
                Connection.Open();
            }
            return true;
        }

        public bool Connect() {
            bool ret = true;

            if (Connection.State != ConnectionState.Open) {
                ret = Connect(ConfigurationManager.ConnectionStrings["ConnectionStringOracle"].ConnectionString);
            }

            return ret;
        }

        public void Close() {
            Connection.Close();
        }

        public void BeginTransaction() {
            SqlTransaction = Connection.BeginTransaction(IsolationLevel.Serializable);
        }

        public void EndTransaction() {
            // command.Dispose()
            SqlTransaction.Commit();
            Close();
        }

        public void Rollback() {
            SqlTransaction.Rollback();
        }

        public int ExecuteNonQuery(OracleCommand command) {
            int rowNumber = 0;
            rowNumber = command.ExecuteNonQuery();
            return rowNumber;
        }

        public OracleCommand CreateCommand(string strCommand) {
            OracleCommand command = new OracleCommand(strCommand, Connection);

            if (SqlTransaction != null) {
                command.Transaction = SqlTransaction;
            }
            return command;
        }

        public OracleDataReader Select(OracleCommand command) {
            //command.Prepare();
            OracleDataReader sqlReader = command.ExecuteReader();
            return sqlReader;
        }
        public static Database Connect(Database pDb) {
            Database db;
            if (pDb == null) {
                db = new Database();
                db.Connect();
            } else {
                db = pDb;
            }
            return db;
        }
        public static void Close(Database pDb, Database db) {
            if (pDb == null) {
                db.Close();
            }
        }
    }
}
