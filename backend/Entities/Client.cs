using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Entities
{
    public class Address
    {
        public string Street { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string Zip { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
    }

    public class Client
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ClientID { get; set; }
        public int AccountNumber { get; set; }

        public string CompanyName { get; set; } = string.Empty;
        public string TradingName { get; set; } = string.Empty;
        public string Contact { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Mobile { get; set; } = string.Empty;
        public int Connections { get; set; }

        public string LoginEmail { get; set; } = string.Empty;
        public string GeneratedPassword { get; set; } = string.Empty;

        public string AccountName { get; set; } = string.Empty;
        public string AccountEmail { get; set; } = string.Empty;
        public string AccountEmailBill { get; set; } = string.Empty;
        public string AccountPhone { get; set; } = string.Empty;
        public string AccountCompanyName { get; set; } = string.Empty;
        public string AccountManager { get; set; } = string.Empty;

        public Address BillingAddress { get; set; } = new Address();
        public Address DeliveryAddress { get; set; } = new Address();

        public bool ActiveAccount { get; set; }
        public bool MasterAccount { get; set; }
        public bool BillingCsv { get; set; }
        public bool EjobsClient { get; set; }
        public bool Gog { get; set; }
        public bool NonBillingAccount { get; set; }
        public bool PVBSClient { get; set; }
        public bool VworkClient { get; set; }
        public bool Sso { get; set; }
        public bool ApiKey { get; set; }
        public bool AssignPPToAllAVLs { get; set; }
        public bool SendMessage { get; set; }
        public bool SosEventPush { get; set; }
        public bool SmartRenew { get; set; }
        public bool CustomBranding { get; set; }
        public bool WorkClient { get; set; }
        public bool RolloverAgreement { get; set; }

        public string RoadRedPlan { get; set; } = string.Empty;
        public string IotPlan { get; set; } = string.Empty;
        public string SoftwarePlan { get; set; } = string.Empty;

        public string? ContractTerm { get; set; }
        public int CustomValue { get; set; }

        public string Notes { get; set; } = string.Empty;

        public DateTime StartDate { get; set; }
       
    }
}