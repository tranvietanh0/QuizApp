namespace HyperCasualGame.Scripts.Scenes.Screen
{
    using Cysharp.Threading.Tasks;
    using GameFoundationCore.Scripts.Signals;
    using GameFoundationCore.Scripts.UIModule.ScreenFlow.BaseScreen.Presenter;
    using GameFoundationCore.Scripts.UIModule.ScreenFlow.BaseScreen.View;
    using UniT.Logging;
    using UnityEngine;
    using UnityEngine.UI;

    public class RegisterScreenView : BaseView
    {
        [field: SerializeField] public Button BtnBack { get; private set; }
        [field: SerializeField] public Button BtnRegister { get; private set; }
    }
    [ScreenInfo(nameof(RegisterScreenView))]
    public class RegisterScreenPresenter : BaseScreenPresenter<RegisterScreenView>
    {
        public RegisterScreenPresenter(SignalBus signalBus, ILoggerManager loggerManager) : base(signalBus, loggerManager) { }
        public override UniTask BindData()
        {
            return UniTask.CompletedTask;
        }

        protected override void OnViewReady()
        {
            base.OnViewReady();
            this.View.BtnBack.onClick.AddListener(this.OnClickBack);
        }

        private void OnClickBack()
        {

        }
    }
}