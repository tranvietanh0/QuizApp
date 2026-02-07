namespace HyperCasualGame.Scripts.StateMachines.Game.States
{
    using GameFoundationCore.Scripts.UIModule.ScreenFlow.Manager;
    using HyperCasualGame.Scripts.Models.Controller;
    using HyperCasualGame.Scripts.Scenes.Screen;
    using HyperCasualGame.Scripts.StateMachines.Game.Interfaces;

    public class WelcomeState : IGameState, IHaveStateMachine
    {
        private readonly IScreenManager         screenManager;
        private readonly AppLocalDataController appLocalDataController;

        public WelcomeState(
            IScreenManager         screenManager,
            AppLocalDataController appLocalDataController
        )
        {
            this.screenManager          = screenManager;
            this.appLocalDataController = appLocalDataController;
        }

        public void Enter()
        {
            if (this.appLocalDataController.IsFirstLaunch)
            {
                this.screenManager.OpenScreen<CheerScreenPresenter>();
                // this.appLocalDataController.SetFirstLaunchFalse();
            }
            else
            {
                this.StateMachine.TransitionTo<GameHomeState>();
            }
        }

        public void Exit()
        {
        }

        public GameStateMachine StateMachine { get; set; }
    }
}