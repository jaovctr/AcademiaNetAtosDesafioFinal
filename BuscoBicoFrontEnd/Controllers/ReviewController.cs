using BuscoBicoFrontEnd.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace BuscoBicoFrontEnd.Controllers
{
    public class ReviewController : Controller
    {
        string baseurl = "https://localhost:7111/";

        // GET: ReviewController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ReviewController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ReviewController/CriarReview
        public async Task<IActionResult> CadastrarReview(int id)
        {
            PrestadorModel? prestador = new PrestadorModel();
            ReviewModel? review = new ReviewModel();
            PrestadorReviewModel viewModel = new PrestadorReviewModel();
            List<ClienteModel>? listaClientes = new List<ClienteModel>();
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
                    review.Prestador = prestador;
                    viewModel.Prestador = prestador;
                    viewModel.Review = review;
                }
                responseMessage = await httpClient.GetAsync("api/Clientes");
                if (responseMessage.IsSuccessStatusCode)
                {
                    var dados = responseMessage.Content.ReadAsStringAsync().Result;
                    listaClientes = JsonConvert.DeserializeObject < List < ClienteModel > > (dados);
                    viewModel.ListaClientes = listaClientes;
                }
            }
            return View(viewModel);
        }

        // POST: ReviewController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CadastrarReview(PrestadorReviewModel reviewCriada)
        {
            try
            {
                using(var httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(baseurl);
                    httpClient.DefaultRequestHeaders.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));

                    ReviewModel review = new ReviewModel();
                    HttpResponseMessage responseMessage = await httpClient.GetAsync("api/Prestadores/" + reviewCriada.IdPrestador);
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        var dados = responseMessage.Content.ReadAsStringAsync().Result;
                        review.Prestador = JsonConvert.DeserializeObject<PrestadorModel>(dados);
                    }
                    responseMessage = await httpClient.GetAsync("api/Clientes/" + reviewCriada.IdCliente);
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        var dados = responseMessage.Content.ReadAsStringAsync().Result;
                        review.Autor = JsonConvert.DeserializeObject<ClienteModel>(dados);
                    }
                    review.Comentario = reviewCriada.Review.Comentario;
                    review.Avaliacao = reviewCriada.Review.Avaliacao;

                    responseMessage = await httpClient.PostAsJsonAsync(
                        baseurl + "api/Reviews", review);
                    
                }
                return RedirectToAction("ListarPrestador", "Prestador");
            }
            catch
            {
                return View();
            }
        }

        // GET: ReviewController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ReviewController/Edit/5
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

        // GET: ReviewController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ReviewController/Delete/5
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
