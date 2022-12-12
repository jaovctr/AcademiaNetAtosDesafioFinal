using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.PortableExecutable;
using BuscoBicoFrontEnd.Models;
using System.Net.Http.Json;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace BuscoBicoFrontEnd.Controllers
{
    public class ClienteController : Controller
    {
        string baseurl = "https://localhost:7111/";

        //Index
        public async Task<ActionResult> ListarCliente()
        {
            List<ClienteModel>? clientes = new List<ClienteModel>();

            using (var httpClient = new HttpClient())
            {

                httpClient.BaseAddress = new Uri(baseurl);
                httpClient.DefaultRequestHeaders.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(
                                new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await httpClient.GetAsync("api/Clientes");


                if (response.IsSuccessStatusCode)
                {
                    var dados = response.Content.ReadAsStringAsync().Result;
                    clientes = JsonConvert.DeserializeObject<List<ClienteModel>>(dados);
                }

                return View(clientes);
            }
        }
        // GET: ClienteController/Details/5
        public ActionResult DetalharCliente(int id)
        {
            return View();
        }

        // GET: ClienteController/Create
        public ActionResult CadastrarCliente()
        {
            return View();
        }

        // POST: ClienteController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CadastrarCliente(ClienteModel cliente)
        {
            cliente.Reviews = new List<ReviewModel>();
            try
            {
                using(var httpClient = new HttpClient())
                {
                    HttpResponseMessage responseMessage = await httpClient.PostAsJsonAsync(
                        baseurl + "api/Clientes", cliente);
                }
                return RedirectToAction(nameof(ListarCliente));
            }
            catch
            {
                return View();
            }
        }

        // GET: ClienteController/Edit/5
        public ActionResult EditarCliente(int id)
        {
            return View();
        }

        // POST: ClienteController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarCliente(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ClienteController/Delete/5
        public ActionResult DeletarCliente(int id)
        {
            return View();
        }

        // POST: ClienteController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeletarCliente(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(ListarCliente));
            }
            catch
            {
                return View();
            }
        }
    }
}
