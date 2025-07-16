namespace backend.DTOs.Dashboard
{
    public class SearchResponseDto
    {
        public int ClientID { get; set; }
        public string CompanyName { get; set; } = string.Empty;
        public string TradingName { get; set; } = string.Empty;
        public string Contact { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Mobile { get; set; } = string.Empty;
        public string LoginEmail { get; set; } = string.Empty;
        public int Connections { get; set; }
        public DateTime? StartDate { get; set; }
        public string ContractTerm { get; set; } = string.Empty;
        public int CustomValue { get; set; }
        public bool ActiveAccount { get; set; }
    }
}
