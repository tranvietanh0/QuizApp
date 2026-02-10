namespace HyperCasualGame.Scripts.StateMachines.Game.States
{
    using GameFoundationCore.Scripts.UIModule.ScreenFlow.Manager;
    using HyperCasualGame.Scripts.Scenes.Screen;
    using HyperCasualGame.Scripts.StateMachines.Game.Interfaces;

    public class RegisterState : IGameState
    {
        #region Inject
        private readonly IScreenManager screenManager;

        public RegisterState(IScreenManager screenManager)
        {
            this.screenManager = screenManager;
        }
        #endregion

        public void Enter()
        {
            this.screenManager.OpenScreen<RegisterScreenPresenter>();
        }
        public void Exit()
        {
        }
    }
}