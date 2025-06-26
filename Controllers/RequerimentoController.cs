using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using LockAiMvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LockAiMvc.Controllers
{
    [Route("[controller]")]
    public class RequerimentoController : Controller
    {
        public string uriBase = "xyz/Requerimento/"; //xyz --- enderco da minha API.

        private readonly ILogger<RequerimentoController> _logger;

        public RequerimentoController(ILogger<RequerimentoController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View("CadastrarRequerimento");
        }

        [HttpPost]
        public async Task<ActionResult> RegistrarAsync(RequerimentoViewModel r)
        {
            try
            {
                //proximo cod aqui
            }
            catch (System.Exception ex)
            {
                TempData["MensagemErro"] = ex.Message;
                return RedirectToAction("Index");
            }
            
        }
    }
}