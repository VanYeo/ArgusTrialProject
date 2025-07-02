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
    }
}
