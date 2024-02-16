using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace YCompanyWebApplication.Pages
{
    [Authorize]
    public class ApiModel : PageModel
    {
        public string Data { get; set; }

        private IHttpClientFactory HttpClientFactory { get; }

        private readonly IConfiguration _configuration;
        private readonly ILogger<ApiModel> _logger; 

        public ApiModel(IHttpClientFactory httpClientFactory, IConfiguration configuration, ILogger<ApiModel> logger)
        {
            HttpClientFactory = httpClientFactory;
            _configuration = configuration;
            Data = string.Empty;
            _logger = logger;
        }
        public async Task OnGet()
        {

            try
            {
                // Your existing HTTP request code here
                //using var httpClient = HttpClientFactory.CreateClient("PaymentsAPI");
                using var httpClient = HttpClientFactory.CreateClient("ThirdPartyAPI");


                // get the access token from the cookie and add it to the default request headers. the save token = true is helpful here as we have the token

                //httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", await HttpContext.GetTokenAsync("access_token"));
                //Data = await httpClient.GetStringAsync("/WeatherForecast");

                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", await HttpContext.GetTokenAsync("access_token"));
                Data = await httpClient.GetStringAsync("/ThirdParty");
            }
            catch (Exception ex)
            {
                // Log or handle the exception appropriately
                // For debugging purposes, you can also inspect 'ex.Message', 'ex.InnerException', and additional details from the HttpResponseMessage
                _logger.LogError(ex,ex.Message);
            }
           

        }
    }
}
