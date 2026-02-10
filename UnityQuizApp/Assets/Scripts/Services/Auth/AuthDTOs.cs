namespace HyperCasualGame.Scripts.Services.Auth
{
    using Newtonsoft.Json;

    public class RegisterRequest
    {
        [JsonProperty("username")]    public string Username    { get; set; }
        [JsonProperty("email")]       public string Email       { get; set; }
        [JsonProperty("password")]    public string Password    { get; set; }
        [JsonProperty("displayName")] public string DisplayName { get; set; }
    }

    public class LoginRequest
    {
        [JsonProperty("username")] public string Username { get; set; }
        [JsonProperty("password")] public string Password { get; set; }
    }

    public class AuthResponse
    {
        [JsonProperty("userId")]      public int    UserId      { get; set; }
        [JsonProperty("username")]    public string Username    { get; set; }
        [JsonProperty("displayName")] public string DisplayName { get; set; }
        [JsonProperty("token")]       public string Token       { get; set; }
    }
}
