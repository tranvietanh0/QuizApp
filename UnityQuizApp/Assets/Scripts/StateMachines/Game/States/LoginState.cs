namespace HyperCasualGame.Scripts.StateMachines.Game.States
{
    using GameFoundationCore.Scripts.UIModule.ScreenFlow.Manager;
    using HyperCasualGame.Scripts.Scenes.Screen;
    using HyperCasualGame.Scripts.StateMachines.Game.Interfaces;

    public class LoginState : IGameState
    {
        #region Inject
        private readonly IScreenManager screenManager;

        public LoginState(IScreenManager screenManager)
        {
            this.screenManager = screenManager;
        }
        #endregion

        public void Enter()
        {
            this.screenManager.OpenScreen<LoginScreenPresenter>();
        }
        public void Exit()
        {
        }
    }
}