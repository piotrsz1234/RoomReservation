using Flurl;
using Newtonsoft.Json;
using RoomReservation.Application.Helpers;
using RoomReservation.Domain.Services;
using System.Net.Http.Headers;
using System.Text;

namespace RoomReservation.Application.Services
{
    public static class Extensions
    {
        public static IServiceCollection AddApiImplementation(this IServiceCollection services)
        {
            services.AddScoped<HttpClient>(ConfigureClient);
            services.AddScoped<IBuildingService, BuildingService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IEquipmentService, EquipmentService>();
            services.AddScoped<IRoomService, RoomService>();
            services.AddScoped<IReservationService, ReservationService>();
            services.AddScoped<IUserService, UserService>();

            return services;
        }

        private static HttpClient ConfigureClient(IServiceProvider provider)
        {
            var client = new HttpClient();
            
            var sessionHelper = provider.GetService<SessionHelper>();

            var model = sessionHelper.SignInModel;
            
            if (model is null) return client;

            var value = Convert.ToBase64String(Encoding.GetEncoding("iso-8859-1").GetBytes($"{model.Email}:{model.Password}").AsSpan());
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", value);

            return client;
        }

        public static async Task<TResponse?> GetCall<TRequest, TResponse>(this HttpClient client, Uri url, TRequest request)
        {
            var response = await client.GetAsync(url.SetQueryParams(request));

            if (!response.IsSuccessStatusCode)
                return default;

            var json = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<TResponse>(json);
        }
        
        public static async Task<TResponse?> GetCall<TResponse>(this HttpClient client, Uri url)
        {
            var response = await client.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                return default;

            var json = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<TResponse>(json);
        }
        
        public static async Task<TResponse?> PostCall<TResponse, TRequest>(this HttpClient client, Uri url, TRequest request)
        {
            var response = await client.PostAsync(url, JsonContent.Create(request));

            var json = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<TResponse>(json);
        }
        
        public static async Task<bool> PostCall<TRequest>(this HttpClient client, Uri url, TRequest request)
        {
            var response = await client.PostAsync(url, JsonContent.Create(request));

            if (!response.IsSuccessStatusCode)
                return false;

            return true;
        }
    }
}