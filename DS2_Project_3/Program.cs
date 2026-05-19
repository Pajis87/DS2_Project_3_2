using DS2_Project_3.orm.dao;

namespace DS2_Project_3 {

    /*
     
    v sqldeveloper vytvorit sequence
    SELECT nazev_seq nextval;


    ID number default nazev_seq.nextval


    zapnout identity column nastavit column sequence a nemusim pouzivat sekvence

    Takze bud tak nebo tak



    INSERT INTO neco(neco)
    VALUES(neco)
    RETURNING ID INTO vid;

    pprint(vid)




    SELECT name, value
    FROM JSON_TABLE(
        "
            {
                {"name":"neco", "value":10}        
            },
            "$[*]",
            
        "
    )
     
     */


    public class Program {
        public static void Main(string[] args) {
            Database db = new Database();
            bool success = db.Connect();
            if (success) {
                Console.WriteLine("Úspěšně připojeno k databázi.\n");
            }

            //TransakceDAO.MojeTransakce(db, 0, 7, [1], "");
            TransakceUPDAO.MojeTransakceUP(db, 0, 7, [1], "");
            //TransakceDAO.MojeTransakce(db, 0, 1, [], "");

            /*int? id_order = null;
            int id_user = 1;
            int id_staff = 1;

            Order o = new Order();
            o.id_user = id_user;
            o.id_staff = id_staff;
            o.date_order = DateTime.Today;
            o.price = null;

            if (id_order == null)
            {
                OrderDao.Insert(db, o);
            }
            else
            {
                o.id_order = (int)id_order;
            }*/

            db.Close();
            Console.WriteLine("Připojení k databázi bylo uzavřeno.");
        }
    }
}