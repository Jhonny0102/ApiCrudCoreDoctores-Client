using ApiCrudCoreDoctores_Client.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json.Serialization;

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

        //Metodos de accion
        public async Task InsertDoctorAsync(int id , string apellido, string especialidad , int salario , int idhospital)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "api/doctor";
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                Doctor doctor = new Doctor();
                doctor.IdDoctor = id;
                doctor.Apellido = apellido;
                doctor.Especialidad = especialidad;
                doctor.Salario = salario;
                doctor.IdHospital = idhospital;
                string json = JsonConvert.SerializeObject(doctor);
                StringContent content = new StringContent(json,Encoding.UTF8, "application/json");
                HttpResponseMessage reponse = await client.PostAsync(request,content);
            }
        }

        public async Task UpdateDoctorAsync(int id, string apellido, string especialidad, int salario, int idhospital)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "api/doctor";
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                Doctor doctor = new Doctor();
                doctor.IdDoctor = id;
                doctor.Apellido = apellido;
                doctor.Especialidad = especialidad;
                doctor.Salario = salario;
                doctor.IdHospital = idhospital;
                string json = JsonConvert.SerializeObject(doctor);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage reponse = await client.PutAsync(request, content);
            }
        }

        public async Task DeleteDoctorAsync(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "api/doctor/"+id;
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                HttpResponseMessage response = await client.DeleteAsync(request);
            }
        }
    }
}
