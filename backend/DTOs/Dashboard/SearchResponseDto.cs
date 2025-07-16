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
        public string Email { get; set; } = string.Empty;
        public int Connections { get; set; }
        public DateTime? StartDate { get; set; }
        public int? ContractTermMonths { get; set; }
        public bool ActiveAccount { get; set; }
    }
}
