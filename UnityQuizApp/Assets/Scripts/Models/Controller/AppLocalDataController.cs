namespace HyperCasualGame.Scripts.Models.Controller
{
    using System.Collections.Generic;
    using System.Linq;
    using GameFoundationCore.Scripts.DI;
    using UITemplate.Scripts.Models.Core.Interface;

    public class AppLocalDataController : IUITemplateControllerData, IInitializable
    {
        #region Inject
        private readonly AppLocalData appLocalData;

        public AppLocalDataController(AppLocalData appLocalData)
        {
            this.appLocalData = appLocalData;
        }
        #endregion
        public void Initialize()
        {

        }

        public bool IsFirstLaunch => this.appLocalData.IsFirstTimeOpenApp;
        public IReadOnlyList<AccountDemo> AccountDemos => this.appLocalData.AccountDemos;

        public void SetFirstLaunchFalse()
        {
            this.appLocalData.IsFirstTimeOpenApp = false;
        }

        public bool CheckAccountDemo(string username)
        {
            if (this.AccountDemos.Count == 0) return false;
            foreach (var acc in this.AccountDemos)
            {
                if (acc.Username.Equals(username))
                {
                    return true;
                }
            }
            return false;
        }

        public void AddAccountDemo(string email, string username, string password)
        {
            this.appLocalData.AccountDemos.Add(new AccountDemo
            {
                Email    = email,
                Username = username,
                Password = password
            });
        }
    }
}