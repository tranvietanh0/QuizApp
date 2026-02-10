namespace HyperCasualGame.Scripts.Models
{
    using GameFoundationCore.Scripts.Models.Interfaces;

    public class UserLocalData : ILocalData
    {
        public string AuthToken   { get; set; }
        public int    UserId      { get; set; }
        public string Username    { get; set; }
        public string DisplayName { get; set; }
        public bool   IsLoggedIn  { get; set; }

        public void Init()
        {
        }
    }
}