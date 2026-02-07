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

    public class CheerScreenView : BaseView
    {
        [field: SerializeField] public Button          BtnNext         { get; private set; }
        [field: SerializeField] public TextMeshProUGUI TxtCheerMessage { get; private set; }
        [field: SerializeField] public TextMeshProUGUI TxtTitle        { get; private set; }
    }

    [ScreenInfo(nameof(CheerScreenView))]
    public class CheerScreenPresenter : BaseScreenPresenter<CheerScreenView>
    {
        private readonly GameStateMachine gameStateMachine;

        public CheerScreenPresenter(
            SignalBus      signalBus,
            ILoggerManager loggerManager,
            GameStateMachine gameStateMachine
        ) : base(signalBus, loggerManager)
        {
            this.gameStateMachine = gameStateMachine;
        }

        protected override void OnViewReady()
        {
            base.OnViewReady();
            this.View.BtnNext.onClick.AddListener(this.OnBtnNextClicked);
        }

        public override UniTask BindData()
        {
            return UniTask.CompletedTask;
        }
        private void OnBtnNextClicked()
        {
            this.gameStateMachine.TransitionTo<LoginState>();
        }
    }
}