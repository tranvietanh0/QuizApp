namespace HyperCasualGame.Scripts.StateMachines.Game
{
    using System.Collections.Generic;
    using GameFoundationCore.Scripts.DI;
    using GameFoundationCore.Scripts.Signals;
    using HyperCasualGame.Scripts.StateMachines.Game.States;
    using UITemplate.Scripts.Others.StateMachine.Controller;
    using UITemplate.Scripts.Others.StateMachine.Interface;
    using UniT.Logging;

    public class GameStateMachine : StateMachine, IInitializable
    {
        public GameStateMachine(
            List<IState>   listState,
            ILoggerManager loggerManager,
            SignalBus      signalBus
        ) : base(listState, loggerManager, signalBus) { }

        public void Initialize()
        {
            this.TransitionTo<GameHomeState>();
        }
    }
}