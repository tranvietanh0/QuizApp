namespace HyperCasualGame.Scripts.Models.Controller
{
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

        public void SetFirstLaunchFalse()
        {
            this.appLocalData.IsFirstTimeOpenApp = false;
        }
    }
}