namespace HyperCasualGame.Scripts.Services
{
    using System.Text;
    using Cysharp.Threading.Tasks;
    using Newtonsoft.Json;
    using UnityEngine;
    using UnityEngine.Networking;

    public class ApiResult<T>
    {
        public bool   IsSuccess    { get; set; }
        public T      Data         { get; set; }
        public string ErrorMessage { get; set; }
    }

    public class ApiClient
    {
        private string authToken;

        public void SetAuthToken(string token)
        {
            this.authToken = token;
        }

        public async UniTask<ApiResult<TResponse>> PostAsync<TRequest, TResponse>(string endpoint, TRequest body)
        {
            var url  = $"{ApiConfig.BaseUrl}/{endpoint}";
            var json = JsonConvert.SerializeObject(body);

            using var request = new UnityWebRequest(url, UnityWebRequest.kHttpVerbPOST);
            request.uploadHandler   = new UploadHandlerRaw(Encoding.UTF8.GetBytes(json));
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            if (!string.IsNullOrEmpty(this.authToken))
            {
                request.SetRequestHeader("Authorization", $"Bearer {this.authToken}");
            }

            await request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                var errorMsg = !string.IsNullOrEmpty(request.downloadHandler.text)
                    ? request.downloadHandler.text
                    : request.error;

                Debug.LogError($"[ApiClient] POST {endpoint} failed: {errorMsg}");
                return new ApiResult<TResponse> { IsSuccess = false, ErrorMessage = errorMsg };
            }

            var responseData = JsonConvert.DeserializeObject<TResponse>(request.downloadHandler.text);
            return new ApiResult<TResponse> { IsSuccess = true, Data = responseData };
        }

        public async UniTask<ApiResult<TResponse>> GetAsync<TResponse>(string endpoint)
        {
            var url = $"{ApiConfig.BaseUrl}/{endpoint}";

            using var request = UnityWebRequest.Get(url);

            if (!string.IsNullOrEmpty(this.authToken))
            {
                request.SetRequestHeader("Authorization", $"Bearer {this.authToken}");
            }

            await request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                var errorMsg = !string.IsNullOrEmpty(request.downloadHandler.text)
                    ? request.downloadHandler.text
                    : request.error;

                Debug.LogError($"[ApiClient] GET {endpoint} failed: {errorMsg}");
                return new ApiResult<TResponse> { IsSuccess = false, ErrorMessage = errorMsg };
            }

            var responseData = JsonConvert.DeserializeObject<TResponse>(request.downloadHandler.text);
            return new ApiResult<TResponse> { IsSuccess = true, Data = responseData };
        }
    }
}
