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

        // GET: ReviewController/Edit/5 testar
        public async Task<IActionResult> EditarReview(int id)
        {
            PrestadorReviewModel? reviewModel = new PrestadorReviewModel();
            ReviewModel? review = new ReviewModel();
            using(var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(baseurl);
                httpClient.DefaultRequestHeaders.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage responseMessage = await httpClient.GetAsync("api/reviews/" + id);
                if (responseMessage.IsSuccessStatusCode)
                {
                    var dados = responseMessage.Content.ReadAsStringAsync().Result;
                    review = JsonConvert.DeserializeObject<ReviewModel>(dados);
                    reviewModel.Review = review;
                }

                return View(reviewModel);

            }
       
        }

        // POST: ReviewController/Edit/5 testar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditarReview(int id, PrestadorReviewModel reviewModel)
        {
            try
            {
                ReviewModel? review = new ReviewModel();
                using(var httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(baseurl);
                    httpClient.DefaultRequestHeaders.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage responseMessage = await httpClient.GetAsync("api/Reviews/" + id);
                    if(responseMessage.IsSuccessStatusCode)
                    {
                        var dados = responseMessage.Content.ReadAsStringAsync().Result;
                        review = JsonConvert.DeserializeObject<ReviewModel>(dados);
                        review.Avaliacao = reviewModel.Review.Avaliacao;
                        review.Comentario = reviewModel.Review.Comentario;
                    }
                    responseMessage = await httpClient.GetAsync("api/Prestadores" + reviewModel.IdPrestador);
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        var dados = responseMessage.Content.ReadAsStringAsync().Result;
                        review.Prestador = JsonConvert.DeserializeObject<PrestadorModel>(dados);
                    }
                    responseMessage = await httpClient.GetAsync("api/Clientes/" + reviewModel.IdCliente);
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        var dados = responseMessage.Content.ReadAsStringAsync().Result;
                        review.Autor = JsonConvert.DeserializeObject<ClienteModel>(dados);
                    }
                    responseMessage = await httpClient.PutAsJsonAsync(
                        baseurl + "api/Reviews/" + id, review);
                }
                return RedirectToAction("ListarPrestador", "Prestador");
            }
            catch
            {
                return View();
            }
        }

        // GET: ReviewController/Delete/5
        public ActionResult DelletarReview(int id)
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
