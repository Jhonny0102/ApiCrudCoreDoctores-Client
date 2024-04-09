using ApiCrudCoreDoctores_Client.Models;
using ApiCrudCoreDoctores_Client.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiCrudCoreDoctores_Client.Controllers
{
    public class DoctorController : Controller
    {
        private ServiceDoctor service;
        public DoctorController(ServiceDoctor service)
        {
            this.service = service;
        }
        public async Task<IActionResult> Index()
        {
            List<Doctor> doctores = await this.service.GetDoctoresAsync();
            return View(doctores);
        }

        public async Task<IActionResult> Details(int id)
        {
            Doctor doctor = await this.service.FindDoctorAsync(id);
            return View(doctor);
        }
    }
}
