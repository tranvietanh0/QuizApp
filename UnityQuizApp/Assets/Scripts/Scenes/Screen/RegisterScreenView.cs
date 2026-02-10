namespace HyperCasualGame.Scripts.Scenes.Screen
{
    using System.Text.RegularExpressions;
    using Cysharp.Threading.Tasks;
    using GameFoundationCore.Scripts.Signals;
    using GameFoundationCore.Scripts.UIModule.ScreenFlow.BaseScreen.Presenter;
    using GameFoundationCore.Scripts.UIModule.ScreenFlow.BaseScreen.View;
    using HyperCasualGame.Scripts.Services.Auth;
    using HyperCasualGame.Scripts.StateMachines.Game;
    using HyperCasualGame.Scripts.StateMachines.Game.States;
    using TMPro;
    using UniT.Logging;
    using UnityEngine;
    using UnityEngine.UI;

    public class RegisterScreenView : BaseView
    {
        [field: SerializeField] public Button         BtnBack          { get; private set; }
        [field: SerializeField] public Button         BtnRegister      { get; private set; }
        [field: SerializeField] public TMP_InputField InputAccount     { get; private set; }
        [field: SerializeField] public TMP_InputField InputEmail       { get; private set; }
        [field: SerializeField] public TMP_InputField InputPassword    { get; private set; }
        [field: SerializeField] public TMP_InputField InputConfirmPass { get; private set; }
    }

    [ScreenInfo(nameof(RegisterScreenView))]
    public class RegisterScreenPresenter : BaseScreenPresenter<RegisterScreenView>
    {
        #region Inject

        private readonly GameStateMachine gameStateMachine;
        private readonly IAuthService     authService;

        public RegisterScreenPresenter(
            SignalBus        signalBus,
            ILoggerManager   loggerManager,
            GameStateMachine gameStateMachine,
            IAuthService     authService
        ) : base(signalBus, loggerManager)
        {
            this.gameStateMachine = gameStateMachine;
            this.authService      = authService;
        }

        #endregion

        public override UniTask BindData()
        {
            return UniTask.CompletedTask;
        }

        protected override void OnViewReady()
        {
            base.OnViewReady();
            this.View.BtnBack.onClick.AddListener(this.OnClickBack);
            this.View.BtnRegister.onClick.AddListener(this.OnClickRegister);
        }

        private void OnClickRegister()
        {
            this.RegisterAsync().Forget();
        }

        private async UniTaskVoid RegisterAsync()
        {
            if (!this.CheckValidInput()) return;

            this.View.BtnRegister.interactable = false;

            var request = new RegisterRequest
            {
                Username    = this.View.InputAccount.text.Trim(),
                Email       = this.View.InputEmail.text.Trim(),
                Password    = this.View.InputPassword.text,
                DisplayName = this.View.InputAccount.text.Trim(),
            };

            var result = await this.authService.RegisterAsync(request);

            this.View.BtnRegister.interactable = true;

            if (result.IsSuccess)
            {
                Debug.Log($"[Register] Success! Welcome {result.Data.DisplayName}");
                this.gameStateMachine.TransitionTo<LoginState>();
            }
            else
            {
                Debug.LogWarning($"[Register] Failed: {result.ErrorMessage}");
            }
        }

        private void OnClickBack()
        {
            this.gameStateMachine.TransitionTo<LoginState>();
        }

        private bool CheckValidInput()
        {
            var account  = this.View.InputAccount.text.Trim();
            var email    = this.View.InputEmail.text.Trim();
            var password = this.View.InputPassword.text;
            var confirm  = this.View.InputConfirmPass.text;

            if (string.IsNullOrEmpty(account) || account.Length < 3)
            {
                Debug.LogWarning("[Register] Username must be at least 3 characters.");
                return false;
            }

            if (string.IsNullOrEmpty(email) || !Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                Debug.LogWarning("[Register] Please enter a valid email address.");
                return false;
            }

            if (string.IsNullOrEmpty(password) || password.Length < 6)
            {
                Debug.LogWarning("[Register] Password must be at least 6 characters.");
                return false;
            }

            if (password != confirm)
            {
                Debug.LogWarning("[Register] Passwords do not match.");
                return false;
            }

            return true;
        }
    }
}