namespace DS2_Project_3 {
    public class TrenerDTO {
        public int tId { get; set; }
        public string jmeno { get; set; }
        public string prijmeni { get; set; }
        public string telefon { get; set; }
        public string email { get; set; }
        DateTime datumRegistrace { get; set; }
        public bool aktivni { get; set; }
        double cenaNaHodinu { get; set; }
    }
}
