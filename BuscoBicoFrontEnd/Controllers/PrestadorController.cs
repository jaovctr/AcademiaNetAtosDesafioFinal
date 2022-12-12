using BuscoBicoFrontEnd.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
namespace BuscoBicoFrontEnd.Controllers
{
    public class PrestadorController : Controller
    {
        string baseurl = "https://localhost:7111/";
        // GET: PrestadorController
        public async Task<IActionResult> ListarPrestador()
        {
            List<PrestadorModel>? prestadores = new List<PrestadorModel>();
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(baseurl);
                httpClient.DefaultRequestHeaders.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await httpClient.GetAsync("api/Prestadores");
                if (response.IsSuccessStatusCode)
                {
                    var dados = response.Content.ReadAsStringAsync().Result;
                    prestadores = JsonConvert.DeserializeObject<List<PrestadorModel>>(dados);

                }
                return View(prestadores);
            }
        }

        // GET: PrestadorController/Details/5
        public ActionResult DetalharPrestador(int id)
        {
            return View();
        }

        // GET: PrestadorController/Create
        public ActionResult CadastrarPrestador()
        {
            return View();
        }

        // POST: PrestadorController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CadastrarPrestador(PrestadorModel prestador)
        {
            prestador.Reviews = new List<ReviewModel>();            
            try
            {
                using(var httpClient = new HttpClient())
                {
                    HttpResponseMessage responseMessage = await httpClient.PostAsJsonAsync(
                        baseurl + "api/Prestadores", prestador);
                }
                return RedirectToAction(nameof(CadastrarPrestador));
            }
            catch
            {
                return View();
            }
        }

        // GET: PrestadorController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PrestadorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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

        // GET: PrestadorController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PrestadorController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
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
    }
}
