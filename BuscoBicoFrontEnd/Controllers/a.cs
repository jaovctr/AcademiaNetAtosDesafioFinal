using BuscoBicoFrontEnd.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Net.Http.Headers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BuscoBicoFrontEnd.Controllers
{
    public class a : Controller
    {
        string baseurl = "https://localhost:7111/";

        //ClienteController/ListarReview 
        //public async Task<ActionResult> ListarReview()
        //{
           
        //        return View();
            
        //}
        //GET: ClienteController/DetalharReview/5
        //public async Task<IActionResult> DetalharReview(int id)
        //{

        //    return View();
        //}

        //GET: ClienteController/CriarReview
        public async Task<IActionResult> CadastrarReview(int id)
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

        //POST: ClienteController/CriarReview 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CadastrarReview(int id, ReviewModel review)
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
                        prestador.Reviews.Add(review);
                        responseMessage = await httpClient.PutAsJsonAsync(
                             baseurl + "api/Prestadores/" + id, prestador);
                    }
                }
            
                return RedirectToAction(nameof(CadastrarReview));
            }
            catch
            {
                return View();
            }
        }

        //GET: ClienteController/EditarReview/5 
        //public async Task<IActionResult> EditarReview(int id)
        //{

        //    return View();
        //}

        // POST: ClienteController/EditarReview/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> EditarReview(int id, ReviewModel reviewEditada)
        //{
        //    try
        //    {
                
        //        return RedirectToAction(nameof(ListarReview));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        // GET: ClienteController/DeletarReview/5 
        //public async Task<IActionResult> DeletarReview(int id)
        //{
           
        //    return View();
        //}

        // POST: ClienteController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeletarCliente(int id, ReviewModel reviewApagando)
        //{
        //    try
        //    {

                
        //        return RedirectToAction(nameof(ListarReview));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
