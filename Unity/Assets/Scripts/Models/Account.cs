namespace PlantReality
{
    public static class Account
    {
        // User info. This information is saved between scenes.
        public static User user;
        public static string accessToken;
        public static string tokenType;
        public static string expiresAt;

        public static void LogOut()
        {
            user = null;
        }
    }
}
