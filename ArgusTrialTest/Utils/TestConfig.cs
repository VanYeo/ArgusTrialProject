namespace ArgusTrialTest.Utils

{
    public static class TestConfig
    {
        // Ideally, load these from environment variables or a config file for security.
        public static string Useremail => "user@gmail.com";
        public static string Userpass => "user123";
        public static string Adminemail => "admin@gmail.com";
        public static string Adminpass => "admin123";
        public static string Invalidemail => "adminuser@gmail.com";
        public static string Invalidpass => "adminuser123";

        public static string Companyname => "Batman Industries";
        public static string Phone => "97643185234";
        public static string Mobile => "98765432123";
        public static string Contact => "Bat Man";

        public static string TradingNameOptional => "Batman Trading";
        public static string TestEmail => "batman123@gmail.com";

        public static string AccountName => "Bat M.";
        public static string AccountEmail => "batman123@batman.com";
        public static string AccountEmailBill => "billing@batman.com";
        public static string AccountPhone => "98432765123";
        public static string AccountCompanyName => "Batman Corp";
        public static string AccountManager => "Bruce Wayne";

        public static string BillingStreet => "123 Gotham Street";
        public static string BillingCity => "Gotham City";
        public static string BillingState => "Gotham State";
        public static string BillingZip => "1234";
        public static string BillingCountry => "Gotham Country";

        public static string ContractStartDate => "01-02-2025";
        public static string ContractDuration => "24";

        public static string Notes => "This is a test note for the client.";
    }
}
