namespace DS2_Project_3 {
    public class VycvikDTO {
        public int vId { get; set; }
        public string? poznamky { get; set; } = null;
        public DateTime cas_od { get; set; }
        public DateTime cas_do { get; set; }
        public int pocetMist { get; set; }
        public int pocetVolnychMist { get; set; }
        public int trenerId { get; set; }

    }
}