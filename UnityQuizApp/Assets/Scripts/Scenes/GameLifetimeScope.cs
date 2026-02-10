namespace HyperCasualGame.Scripts.Scenes
{
    using GameFoundationCore.Scripts;
    using GameFoundationCore.Scripts.DI.VContainer;
    using HyperCasualGame.Scripts.Services;
    using HyperCasualGame.Scripts.Services.Auth;
    using UITemplate.Scripts;
    using UnityEngine;
    using VContainer;
    using VContainer.Unity;

    public class GameLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterGameFoundation(this.transform);
            builder.RegisterUITemplate();

            builder.Register<ApiClient>(Lifetime.Singleton);
            builder.Register<AuthService>(Lifetime.Singleton).As<IAuthService>();
        }
    }
}