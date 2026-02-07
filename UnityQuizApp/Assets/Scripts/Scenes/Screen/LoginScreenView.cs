namespace HyperCasualGame.Scripts.Scenes.Screen
{
    using Cysharp.Threading.Tasks;
    using GameFoundationCore.Scripts.Signals;
    using GameFoundationCore.Scripts.UIModule.ScreenFlow.BaseScreen.Presenter;
    using GameFoundationCore.Scripts.UIModule.ScreenFlow.BaseScreen.View;
    using UniT.Logging;
    using UnityEngine;
    using UnityEngine.UI;

    public class LoginScreenView : BaseView
    {
        [field: SerializeField] public Button BtnBack           { get; private set; }
        [field: SerializeField] public Button ForgotPasswordBtn { get; private set; }
        [field: SerializeField] public Button LoginBtn          { get; private set; }
    }

    [ScreenInfo(nameof(LoginScreenView))]
    public class LoginScreenPresenter : BaseScreenPresenter<LoginScreenView>
    {
        public LoginScreenPresenter(
            SignalBus      signalBus,
            ILoggerManager loggerManager
        ) : base(signalBus, loggerManager)
        {
        }

        public override UniTask BindData()
        {
            return UniTask.CompletedTask;
        }
    }
}