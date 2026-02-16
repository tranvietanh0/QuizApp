namespace HyperCasualGame.Scripts.Scenes.Popups
{
    using Cysharp.Threading.Tasks;
    using GameFoundationCore.Scripts.Signals;
    using GameFoundationCore.Scripts.UIModule.ScreenFlow.BaseScreen.Presenter;
    using GameFoundationCore.Scripts.UIModule.ScreenFlow.BaseScreen.View;
    using UniT.Logging;
    using UnityEngine;
    using UnityEngine.UI;

    public class RegisterFailPopupView : BaseView
    {
        [field: SerializeField] public Button BtnClose { get; private set; }
    }

    [PopupInfo(nameof(RegisterFailPopupView))]
    public class RegisterFailPopupPresenter : BasePopupPresenter<RegisterFailPopupView>
    {
        public RegisterFailPopupPresenter(
            SignalBus      signalBus,
            ILoggerManager loggerManager
        ) : base(signalBus, loggerManager) { }

        protected override void OnViewReady()
        {
            base.OnViewReady();
            this.View.BtnClose.onClick.AddListener(this.OnBtnCloseClicked);
        }

        public override UniTask BindData()
        {
            return UniTask.CompletedTask;
        }

        private void OnBtnCloseClicked()
        {
            this.CloseViewAsync().Forget();
        }
    }
}