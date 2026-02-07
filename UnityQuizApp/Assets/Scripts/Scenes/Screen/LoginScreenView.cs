namespace HyperCasualGame.Scripts.Scenes.Screen
{
    using Cysharp.Threading.Tasks;
    using GameFoundationCore.Scripts.Signals;
    using GameFoundationCore.Scripts.UIModule.ScreenFlow.BaseScreen.Presenter;
    using GameFoundationCore.Scripts.UIModule.ScreenFlow.BaseScreen.View;
    using UniT.Logging;

    public class LoginScreenView : BaseView
    {
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