using Mango.Web.Models;
using Mango.Web.Service.IService;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;
using static Mango.Web.Utility.SD;

namespace Mango.Web.Service
{
    public class BaseService : IBaseService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public BaseService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<ResponseDto?> SendAsync(RequestDto requestDto)
        {
            try
            {


                HttpClient httpClient = _httpClientFactory.CreateClient("MangoAPI");
                HttpRequestMessage httpRequestMessage = new();
                httpRequestMessage.Headers.Add("Accept", "application/json");

                //token

                httpRequestMessage.RequestUri = new Uri(requestDto.Url);

                if (requestDto.Data != null)
                {
                    httpRequestMessage.Content = new StringContent(JsonConvert.SerializeObject(requestDto.Data), Encoding.UTF8, "application/json");

                }
                HttpResponseMessage? apiResponse = null;
                switch (requestDto.ApiType)
                {
                    case ApiType.POST:
                        httpRequestMessage.Method = HttpMethod.Post;
                        break;
                    case ApiType.DELETE:
                        httpRequestMessage.Method = HttpMethod.Delete;
                        break;
                    case ApiType.PUT:
                        httpRequestMessage.Method = HttpMethod.Put;
                        break;
                    default:
                        httpRequestMessage.Method = HttpMethod.Get;
                        break;
                }

                apiResponse = await httpClient.SendAsync(httpRequestMessage);

                switch (apiResponse.StatusCode)
                {
                    case System.Net.HttpStatusCode.NotFound:
                        return new() { IsSuccess = false, Messasge = "Not Found" };
                    case System.Net.HttpStatusCode.Forbidden:
                        return new() { IsSuccess = false, Messasge = "Forbidden" };
                    case System.Net.HttpStatusCode.Unauthorized:
                        return new() { IsSuccess = false, Messasge = "Unauthorized" };
                    case System.Net.HttpStatusCode.InternalServerError:
                        return new() { IsSuccess = false, Messasge = "Internal Server Error" };
                    default:
                        var apiContent = await apiResponse.Content.ReadAsStringAsync();
                        var apiResponseDto = JsonConvert.DeserializeObject<ResponseDto>(apiContent);
                        return apiResponseDto;
                }
            }catch(Exception ex)
            {
                var dto = new ResponseDto
                {
                    Messasge = ex.Message,
                    IsSuccess = false
                };
                return dto;
            }
        }
    }
}
