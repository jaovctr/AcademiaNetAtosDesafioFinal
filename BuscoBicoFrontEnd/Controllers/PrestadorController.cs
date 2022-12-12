using BuscoBicoFrontEnd.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Net.Http.Headers;
namespace BuscoBicoFrontEnd.Controllers
{
    public class PrestadorController : Controller
    {
        string baseurl = "https://localhost:7111/";
        // GET: PrestadorController/ListarPrestador OK
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

        // GET: PrestadorController/DetalharPrestador/5 OK
        public async Task<IActionResult> DetalharPrestador(int id)
        {
            
            PrestadorModel? prestador = new PrestadorModel();
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(baseurl);
                httpClient.DefaultRequestHeaders.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage responseMessage = await httpClient.GetAsync("api/Prestadores/" + id);
                if (responseMessage.IsSuccessStatusCode)
                {
                    var dados = responseMessage.Content.ReadAsStringAsync().Result;
                    prestador = JsonConvert.DeserializeObject<PrestadorModel>(dados);
                }
                responseMessage = await httpClient.GetAsync("api/Reviews/ReviewsDoPrestador/" + id);
                if (responseMessage.IsSuccessStatusCode)
                {
                    var dados = responseMessage.Content.ReadAsStringAsync().Result;
                    prestador.Reviews = JsonConvert.DeserializeObject<List<ReviewModel>>(dados);
                }
                else
                {
                    prestador.Reviews = new List<ReviewModel>();
                }
            }
            return View(prestador);
        }

        // GET: PrestadorController/CriarPrestador OK
        public ActionResult CadastrarPrestador()
        {
            return View();
        }

        // POST: PrestadorController/CriarPrestador OK
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
                return RedirectToAction(nameof(ListarPrestador));
            }
            catch
            {
                return View();
            }
        }

        // GET: PrestadorController/EditarPrestador/5 OK
        public async Task<IActionResult> EditarPrestador(int id)
        {
            PrestadorModel? prestador = new PrestadorModel();
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(baseurl);
                httpClient.DefaultRequestHeaders.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage responseMessage = await httpClient.GetAsync("api/Prestadores/" + id);
                if (responseMessage.IsSuccessStatusCode)
                {
                    var dados = responseMessage.Content.ReadAsStringAsync().Result;
                    prestador = JsonConvert.DeserializeObject<PrestadorModel>(dados);
                }
            }
            return View(prestador);
        }

        // POST: PrestadorController/EditarPrestador/5 OK
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditarPrestador(int id, PrestadorModel prestadorEditado)
        {
            try
            {
                PrestadorModel? prestador = new PrestadorModel();
                using (var httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(baseurl);
                    httpClient.DefaultRequestHeaders.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage responseMessage = await httpClient.GetAsync("api/Prestadores/" + id);
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        var dados = responseMessage.Content.ReadAsStringAsync().Result;
                        prestador = JsonConvert.DeserializeObject<PrestadorModel>(dados);
                        prestador.Nome = prestadorEditado.Nome;
                        prestador.Telefone = prestadorEditado.Telefone;
                        prestador.Email = prestadorEditado.Email;
                        prestador.Localizacao = prestadorEditado.Localizacao;
                        prestador.Funcao = prestadorEditado.Funcao;
                        prestador.Descricao = prestadorEditado.Descricao;
                        prestador.PrecoDiaria = prestadorEditado.PrecoDiaria;

                        responseMessage = await httpClient.PutAsJsonAsync(
                            baseurl + "api/Prestadores/" + id, prestador);
                    }
                }
                return RedirectToAction(nameof(ListarPrestador));
            }
            catch
            {
                return View();
            }
        }

        // GET: PrestadorController/DeletarPrestador/5 OK
        public async Task<IActionResult> DeletarPrestador(int id)
        {
            PrestadorModel? prestador = new PrestadorModel();
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(baseurl);
                httpClient.DefaultRequestHeaders.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage responseMessage = await httpClient.GetAsync("api/Prestadores/" + id);
                if (responseMessage.IsSuccessStatusCode)
                {
                    var dados = responseMessage.Content.ReadAsStringAsync().Result;
                    prestador = JsonConvert.DeserializeObject<PrestadorModel>(dados);
                }
            }
            return View(prestador);
        }

        // POST: PrestadorController/DeletarPrestador/5 OK
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletarPrestador(int id, PrestadorModel prestadorApagando)
        {
            try
            {
                using(var httpClient = new HttpClient()) {
                    httpClient.BaseAddress = new Uri(baseurl);
                    httpClient.DefaultRequestHeaders.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage responseMessage = await httpClient.DeleteAsync("api/Prestadores/" + id);
                }
                return RedirectToAction(nameof(ListarPrestador));
            }
            catch
            {
                return View();
            }
        }
    }
}
