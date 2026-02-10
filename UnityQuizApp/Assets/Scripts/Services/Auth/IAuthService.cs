namespace HyperCasualGame.Scripts.Services.Auth
{
    using Cysharp.Threading.Tasks;
    using HyperCasualGame.Scripts.Models;

    public interface IAuthService
    {
        UniTask<ApiResult<AuthResponse>> RegisterAsync(RegisterRequest request);
        UniTask<ApiResult<AuthResponse>> LoginAsync(LoginRequest request);
    }

    public class AuthService : IAuthService
    {
        #region Inject

        private readonly ApiClient    apiClient;
        private readonly UserLocalData userLocalData;

        public AuthService(ApiClient apiClient, UserLocalData userLocalData)
        {
            this.apiClient     = apiClient;
            this.userLocalData = userLocalData;
        }

        #endregion

        public async UniTask<ApiResult<AuthResponse>> RegisterAsync(RegisterRequest request)
        {
            var result = await this.apiClient.PostAsync<RegisterRequest, AuthResponse>("auth/register", request);
            if (result.IsSuccess) this.StoreAuthData(result.Data);
            return result;
        }

        public async UniTask<ApiResult<AuthResponse>> LoginAsync(LoginRequest request)
        {
            var result = await this.apiClient.PostAsync<LoginRequest, AuthResponse>("auth/login", request);
            if (result.IsSuccess) this.StoreAuthData(result.Data);
            return result;
        }

        private void StoreAuthData(AuthResponse response)
        {
            this.userLocalData.AuthToken   = response.Token;
            this.userLocalData.UserId      = response.UserId;
            this.userLocalData.Username    = response.Username;
            this.userLocalData.DisplayName = response.DisplayName;
            this.userLocalData.IsLoggedIn  = true;
            this.apiClient.SetAuthToken(response.Token);
        }
    }
}
