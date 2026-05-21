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


            // Výcvik neexistuje
            TransakceDAO.MojeTransakce(1, 1, [1], null);

            // Výcvik v minulosti
            TransakceDAO.MojeTransakce(1, 21, [1], null);

            // Nedostatek volných míst
            TransakceDAO.MojeTransakce(1, 22, [1], null);

            // Kupón použit
            TransakceDAO.MojeTransakce(1, 7, [1], "abc");

            // Zákazník nevlastní vybraného psa
            TransakceDAO.MojeTransakce(1, 7, [2], null);

            // Již zapsán
            TransakceDAO.MojeTransakce(1, 7, [1], null);

            // V pořádku
            TransakceDAO.MojeTransakce(1, 28, new int[] { 1 }, null);

            // V pořádku
            TransakceDAO.MojeTransakce(1, 29, new int[] { 1, 3 }, null);

            Console.WriteLine("");

            // ----------------- Uložená procedura

            // Výcvik v minulosti
            TransakceUPDAO.MojeTransakceUP(1, 21, [1], null);

            // Nedostatek volných míst
            TransakceUPDAO.MojeTransakceUP(1, 22, [1], null);

            // Kupón použit
            TransakceUPDAO.MojeTransakceUP(1, 7, [1], "abc");

            // Zákazník nevlastní vybraného psa
            TransakceUPDAO.MojeTransakceUP(1, 7, [2], null);

            // Zákazník nevlastní vybraného psa
            TransakceUPDAO.MojeTransakceUP(1, 7, [1], null);

            // V pořádku
            TransakceUPDAO.MojeTransakceUP(1, 26, new int[] { 1 }, null);

            // V pořádku
            TransakceUPDAO.MojeTransakceUP(1, 27, new int[] { 1, 3 }, null);


            Console.WriteLine("");
            db.Close();
            Console.WriteLine("Připojení k databázi bylo uzavřeno.");
        }
    }
}