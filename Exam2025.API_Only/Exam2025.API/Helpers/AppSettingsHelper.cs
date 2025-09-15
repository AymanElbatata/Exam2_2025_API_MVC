using Azure.Core;

namespace Exam2025.API.Helpers
{
    public class AppSettingsHelper
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AppSettingsHelper(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;

        }
        public string GetBaseUrl()
        { string baseUrl = string.Empty;
            baseUrl = _configuration["APISettings:BaseApiUrl"];
            if (baseUrl == null)
            {
                var request = _httpContextAccessor.HttpContext?.Request;
                baseUrl = $"{request.Scheme}://{request.Host.Value}/api47c302a9db0a484ba9a838ec6e79/";
                if (baseUrl.Contains("runasp.net"))
                {
                    baseUrl = "https://exam2025.runasp.net/api47c302a9db0a484ba9a838ec6e79/";
                }
                else if (baseUrl.Contains("http://localhost:5015"))
                {
                    baseUrl = "http://localhost/Exam2025/api47c302a9db0a484ba9a838ec6e79/";
                }
                else if (baseUrl.Contains("http://localhost:5107"))
                {
                    baseUrl = "http://localhost:5015/api47c302a9db0a484ba9a838ec6e79/";
                }
            }
            return baseUrl;
        }

    }
}
