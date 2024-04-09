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

        public async Task<IActionResult> Create() 
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Doctor doc)
        {
            await this.service.InsertDoctorAsync(doc.IdDoctor,doc.Apellido,doc.Especialidad,doc.Salario,doc.IdHospital);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            Doctor doctor = await this.service.FindDoctorAsync(id);
            return View(doctor);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Doctor doctor)
        {
            await this.service.UpdateDoctorAsync(doctor.IdDoctor,doctor.Apellido,doctor.Especialidad,doctor.Salario,doctor.IdHospital);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            await this.service.DeleteDoctorAsync(id);
            return RedirectToAction("Index");
        }
    }
}
