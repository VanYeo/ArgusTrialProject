using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace backend.Entities
{
    [Index(nameof(Email), IsUnique = true)]
    public class Client
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ClientID { get; set; }

        public string? AccountNumber { get; set; }
        public string CompanyName { get; set; } = null!;
        public string? TradingName { get; set; }
        public string? Contact { get; set; }
        public string? Phone { get; set; }
        public string? Mobile { get; set; }
        public int Connections { get; set; }

        public string Email { get; set; } = null!;

        public DateTime StartDate { get; set; }
        public int? ContractTermMonths { get; set; }
        public string? ContractType { get; set; }

        public string? Notes { get; set; }

        public AccountDetail? AccountDetail { get; set; }
        public ICollection<Address> Addresses { get; set; } = new List<Address>();
        public ClientOptions? Options { get; set; }
        public ClientPlugins? Plugins { get; set; }
        public ClientIntegrations? Integrations { get; set; }
        public DefaultPlans? Plans { get; set; }
    }

    public class AccountDetail
    {
        [Key]
        public int ClientID { get; set; }
        public Client? Client { get; set; } = null!;

        public string? AccountName { get; set; }
        public string? AccountEmail { get; set; }
        public string? EmailBillTo { get; set; }
        public string? AccountPhone { get; set; }
        public string? AccountManager { get; set; }
    }

    public class Address
    {
        [Key]
        public int AddressID { get; set; } // Primary key, auto-generated

        [ForeignKey("Client")]
        public int ClientID { get; set; }  // Foreign key to Client

        public Client? Client { get; set; }
        public string Type { get; set; } = null!;
        public string? Street { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Zip { get; set; }
        public string? Country { get; set; }
    }

    public class ClientOptions
    {
        [Key]
        public int ClientID { get; set; }
        public Client? Client { get; set; } = null!;

        public bool ActiveAccount { get; set; }
        public bool MasterAccount { get; set; }
        public bool BillingCSV { get; set; }
        public bool GOG { get; set; }
        public bool EJobsClient { get; set; }
    }

    public class ClientPlugins
    {
        [Key]
        public int ClientID { get; set; }
        public Client? Client { get; set; } = null!;

        public bool SmartRenew { get; set; }
        public bool CustomBranding { get; set; }
        public bool SendMessage { get; set; }
    }

    public class ClientIntegrations
    {
        [Key]
        public int ClientID { get; set; }
        public Client? Client { get; set; } = null!;

        public bool SOSEventPush { get; set; }
        public bool PVBSClient { get; set; }
        public bool VWorkClient { get; set; }
        public bool APIKey { get; set; }
        public bool SSO { get; set; }
    }

    public class DefaultPlans
    {
        [Key]
        public int ClientID { get; set; }
        public Client? Client { get; set; } = null!;

        public string? RoadRedPlan { get; set; }
        public string? IoTPlan { get; set; }
        public string? SoftwarePlan { get; set; }

        public bool AssignPPToAllAULS { get; set; }
        public bool RolloverAgreement { get; set; }
        public bool IsNonBillingAccount { get; set; }
    }
}
