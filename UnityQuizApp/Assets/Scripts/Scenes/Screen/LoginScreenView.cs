namespace HyperCasualGame.Scripts.Scenes.Screen
{
    using Cysharp.Threading.Tasks;
    using GameFoundationCore.Scripts.Signals;
    using GameFoundationCore.Scripts.UIModule.ScreenFlow.BaseScreen.Presenter;
    using GameFoundationCore.Scripts.UIModule.ScreenFlow.BaseScreen.View;
    using HyperCasualGame.Scripts.StateMachines.Game;
    using HyperCasualGame.Scripts.StateMachines.Game.States;
    using TMPro;
    using UniT.Logging;
    using UnityEngine;
    using UnityEngine.UI;

    public class LoginScreenView : BaseView
    {
        [field: SerializeField] public Button         BtnBack           { get; private set; }
        [field: SerializeField] public Button         ForgotPasswordBtn { get; private set; }
        [field: SerializeField] public Button         LoginBtn          { get; private set; }
        [field: SerializeField] public Button         RegisterBtn       { get; private set; }
        [field: SerializeField] public TMP_InputField EmailInput        { get; private set; }
        [field: SerializeField] public TMP_InputField PasswordInput     { get; private set; }
    }

    [ScreenInfo(nameof(LoginScreenView))]
    public class LoginScreenPresenter : BaseScreenPresenter<LoginScreenView>
    {
        private readonly GameStateMachine gameStateMachine;

        public LoginScreenPresenter(
            SignalBus        signalBus,
            ILoggerManager   loggerManager,
            GameStateMachine gameStateMachine
        ) : base(signalBus, loggerManager)
        {
            this.gameStateMachine = gameStateMachine;
        }

        protected override void OnViewReady()
        {
            base.OnViewReady();
            this.View.RegisterBtn.onClick.AddListener(this.OnClickRegister);
        }

        private void OnClickRegister()
        {
            this.gameStateMachine.TransitionTo<RegisterState>();
        }

        public override UniTask BindData()
        {
            return UniTask.CompletedTask;
        }
    }
}