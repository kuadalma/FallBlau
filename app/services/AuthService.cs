namespace app.services
{
    public class AuthService
    {
        private const string AuthStateKey = "AuthState";
        public async Task<bool> IsAuthenticated()
        {
            await Task.Delay(1000);

            var authState = Preferences.Default.Get(AuthStateKey, false);

            return authState;
        }
        public void Login()
        {
            Preferences.Default.Set(AuthStateKey, true);
        }
        public void Logout()
        {
            Preferences.Default.Remove(AuthStateKey);
        }
    }
}
