using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProyectoCrudDepartamentos.Data;
using ProyectoCrudDepartamentos.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoCrudDepartamentos.Controllers
{
    public class HomeController : Controller
    {
        DepartamentosContext context;

        public HomeController()
        {
            this.context = new DepartamentosContext();
        }
        public IActionResult Index()
        {
            List<Departamento> depar = this.context.GetDepartamentos();
            return View(depar);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Departamento dept)
        {
            this.context.CreateDepartamento(dept.IdDepartamento, dept.Nombre, dept.Localidad);
            return RedirectToAction("Index");
        }

        public IActionResult Details(int iddept)
        {
            Departamento dept = this.context.GetDepartamento(iddept);
            return View(dept);
        }

        public IActionResult Edit(int iddept)
        {
            Departamento dept = this.context.GetDepartamento(iddept);
            return View(dept);
        }

        [HttpPost]
        public IActionResult Edit(Departamento dept)
        {
            this.context.UpdateDepartamento(dept.IdDepartamento, dept.Nombre, dept.Localidad);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int iddept)
        {
            this.context.DeleteDepartamento(iddept);
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
