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


            // ----------------- C# transakce


            // Výcvik v minulosti
            TransakceDAO.MojeTransakce(db, 1, 21, [1], null);

            // Nedostatek volných míst
            TransakceDAO.MojeTransakce(db, 1, 22, [1], null);

            // Nedostatek volných míst
            TransakceDAO.MojeTransakce(db, 1, 7, [1], "abc");

            // Zákazník nevlastní vybraného psa
            TransakceDAO.MojeTransakce(db, 1, 7, [2], null);

            // Již zapsán
            TransakceDAO.MojeTransakce(db, 1, 7, [1], null);

            // V pořádku
            TransakceDAO.MojeTransakce(db, 1, 23, [1], null);



            // ----------------- Uložená procedura

            // Výcvik v minulosti
            TransakceUPDAO.MojeTransakceUP(db, 1, 21, [1], null);

            // Nedostatek volných míst
            TransakceUPDAO.MojeTransakceUP(db, 1, 22, [1], null);

            // Nedostatek volných míst
            TransakceUPDAO.MojeTransakceUP(db, 1, 7, [1], "abc");

            // Zákazník nevlastní vybraného psa
            TransakceUPDAO.MojeTransakceUP(db, 1, 7, [2], null);


            db.Close();
            Console.WriteLine("Připojení k databázi bylo uzavřeno.");
        }
    }
}