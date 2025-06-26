using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using LockAiMvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace LockAiMvc.Controllers
{
    [Route("[controller]")]
    public class RequerimentoController : Controller
    {
        public string uriBase = "http://lauraCabrera20.somee.com/LockAi/Requerimento/"; //xyz --- enderco da minha API.

        private readonly ILogger<RequerimentoController> _logger;

        public RequerimentoController(ILogger<RequerimentoController> logger)
        {
            _logger = logger;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]

        public IActionResult Error()
        {
            return View("Error!");
        }

        [HttpGet]
        public async Task<ActionResult> IndexAsync()
        {
            try
            {
                string uriComplementar = "GetAll"; //1
                HttpClient httpClient = new HttpClient(); //2
                string token = HttpContext.Session.GetString("SessionTokenUsuario"); //3
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token); //4

                HttpResponseMessage response = await httpClient.GetAsync(uriBase + uriComplementar);
                string serialized = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    List<RequerimentoViewModel> listarequerimento = await Task.Run(() => JsonConvert.DeserializeObject<List<RequerimentoViewModel>>(serialized));
                    return View(listarequerimento);
                }
                else
                    throw new System.Exception(serialized);

            }
            catch (System.Exception ex)
            {
                TempData["MensagemErro"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateAsync(RequerimentoViewModel r)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                /*string token = HttpContext.Session.GetString("SessionTokenUsuario");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);*/

                var content = new StringContent(JsonConvert.SerializeObject(r));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                HttpResponseMessage response = await httpClient.PostAsync(uriBase, content);
                string serialized = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    TempData["Mensagem"] = string.Format("Requerimento {0}, Id {1} salvo com sucesso!", r.Observacao, serialized);
                    return RedirectToAction("Index");
                }
                else
                    throw new System.Exception(serialized);
            }
            catch (System.Exception ex)
            {
                TempData["Mensagem"] = ex.Message;
                return RedirectToAction("Create");
            }
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }





        /*
        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }

        [HttpGet("Index")]
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
            
        }*/
    }
}