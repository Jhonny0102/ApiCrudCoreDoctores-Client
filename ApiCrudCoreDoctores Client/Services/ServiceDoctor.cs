using ApiCrudCoreDoctores_Client.Models;
using System.Net.Http.Headers;

namespace ApiCrudCoreDoctores_Client.Services
{
    public class ServiceDoctor
    {
        private string UrlApi;
        private MediaTypeWithQualityHeaderValue header;
        public ServiceDoctor(IConfiguration configuration)
        {
            this.header = new MediaTypeWithQualityHeaderValue("application/json");
            this.UrlApi = configuration.GetValue<string>("ApiUrls:ApiDoctor");
        }

        private async Task<T> CallApiAsync<T>(string request)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                HttpResponseMessage response = await client.GetAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    T data = await response.Content.ReadAsAsync<T>();
                    return data;
                }
                else
                {
                    return default(T);
                }
            }
        }

        public async Task<List<Doctor>> GetDoctoresAsync()
        {
            string request = "api/doctor";
            List<Doctor> data = await CallApiAsync<List<Doctor>>(request);
            return data;
        }

        public async Task<Doctor> FindDoctorAsync(int id)
        {
            string request = "api/doctor/"+id;
            Doctor data = await CallApiAsync<Doctor>(request);
            return data;
        }
    }
}
