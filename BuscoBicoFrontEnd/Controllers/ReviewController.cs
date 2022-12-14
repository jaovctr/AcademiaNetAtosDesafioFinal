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

        // GET: ReviewController/CriarReview OK
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

        // POST: ReviewController/CriarReview OK
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
                        review.Prestador.Reviews = new List<ReviewModel>();
                    }
                    responseMessage = await httpClient.GetAsync("api/Clientes/" + reviewCriada.IdCliente);
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        var dados = responseMessage.Content.ReadAsStringAsync().Result;
                        review.Autor = JsonConvert.DeserializeObject<ClienteModel>(dados);
                        review.Autor.Reviews = new List<ReviewModel>();
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

        // GET: ReviewController/Edit/5 testar OK
        public async Task<IActionResult> EditarReview(int id)
        {
            PrestadorReviewModel? reviewModel = new PrestadorReviewModel();
            using(var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(baseurl);
                httpClient.DefaultRequestHeaders.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage responseMessage = await httpClient.GetAsync("api/Reviews/" + id);
                if (responseMessage.IsSuccessStatusCode)
                {
                    var dados = responseMessage.Content.ReadAsStringAsync().Result;
                    reviewModel.Review = JsonConvert.DeserializeObject<ReviewModel>(dados);
                }
                reviewModel.IdCliente = reviewModel.Review.Autor.Id;
                reviewModel.IdPrestador = reviewModel.Review.Prestador.Id;
                reviewModel.Prestador = reviewModel.Review.Prestador;
                reviewModel.Cliente = reviewModel.Review.Autor;
                return View(reviewModel);

            }
       
        }

        // POST: ReviewController/Edit/5 OK
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
                    responseMessage = await httpClient.GetAsync("api/Prestadores/" + reviewModel.IdPrestador);
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        var dados = responseMessage.Content.ReadAsStringAsync().Result;
                        review.Prestador = JsonConvert.DeserializeObject<PrestadorModel>(dados);
                        review.Prestador.Reviews = new List<ReviewModel>();
                    }
                    responseMessage = await httpClient.GetAsync("api/Clientes/" + reviewModel.IdCliente);
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        var dados = responseMessage.Content.ReadAsStringAsync().Result;
                        review.Autor = JsonConvert.DeserializeObject<ClienteModel>(dados);
                        review.Autor.Reviews = new List<ReviewModel>();
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

        // GET: ReviewController/Delete/5 OK
        public async Task<IActionResult> DeletarReview(int id)
        {
            ReviewModel review = new ReviewModel();
            using(var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(baseurl);
                httpClient.DefaultRequestHeaders.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage responseMessage = await httpClient.GetAsync("api/Reviews/" + id);
                if (responseMessage.IsSuccessStatusCode)
                {
                    var dados = responseMessage.Content.ReadAsStringAsync().Result;
                    review = JsonConvert.DeserializeObject<ReviewModel>(dados);
                }
                return View(review);
            }
        }

        // POST: ReviewController/DeletarReview/5 OK
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletarReview(int id, ReviewModel reviewApagar)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(baseurl);
                    httpClient.DefaultRequestHeaders.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage responseMessage = await httpClient.DeleteAsync("api/Reviews/" + id);

                }
                return RedirectToAction("ListarPrestador", "Prestador");
            }
            catch
            {
                return View();
            }
        }
    }
}
