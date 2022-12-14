
using Microsoft.AspNetCore.Mvc;
using BuscoBicoFrontEnd.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace BuscoBicoFrontEnd.Controllers
{
    public class ClienteController : Controller
    {
        string baseurl = "https://localhost:7111/";

        //ClienteController/ListarCliente OK
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
        // GET: ClienteController/DetalharCliente/5 OK
        public async Task<IActionResult> DetalharCliente(int id)
        {
            ClienteModel? cliente = new ClienteModel();
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(baseurl);
                httpClient.DefaultRequestHeaders.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage responseMessage = await httpClient.GetAsync("api/Clientes/" + id);
                if (responseMessage.IsSuccessStatusCode){
                    var dados = responseMessage.Content.ReadAsStringAsync().Result;
                    cliente = JsonConvert.DeserializeObject<ClienteModel>(dados);
                }
            }
            return View(cliente);
        }

        // GET: ClienteController/CriarCliente OK
        public ActionResult CadastrarCliente()
        {
            return View();
        }

        // POST: ClienteController/CriarCliente OK
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

        // GET: ClienteController/EditarCliente/5 OK
        public async Task<IActionResult> EditarCliente(int id)
        {
            ClienteModel? cliente = new ClienteModel();
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(baseurl);
                httpClient.DefaultRequestHeaders.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage responseMessage = await httpClient.GetAsync("api/Clientes/" + id);
                if (responseMessage.IsSuccessStatusCode)
                {
                    var dados = responseMessage.Content.ReadAsStringAsync().Result;
                    cliente = JsonConvert.DeserializeObject<ClienteModel>(dados);
                }
            }
            return View(cliente);
        }

        // POST: ClienteController/EditarCliente/5 OK
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditarCliente(int id, ClienteModel clienteEditado)
        {
            try
            {
                ClienteModel? cliente = new ClienteModel();
                using (var httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(baseurl);
                    httpClient.DefaultRequestHeaders.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage responseMessage = await httpClient.GetAsync("api/Clientes/" + id);
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        var dados = responseMessage.Content.ReadAsStringAsync().Result;
                        cliente = JsonConvert.DeserializeObject<ClienteModel>(dados);
                        cliente.Nome = clienteEditado.Nome;
                        cliente.Telefone = clienteEditado.Telefone;
                        cliente.Localizacao = clienteEditado.Localizacao;
                        cliente.Reviews = new List<ReviewModel>();
                        responseMessage = await httpClient.PutAsJsonAsync(
                       baseurl + "api/Clientes/"+id, cliente);
                    }
                }
                
                return RedirectToAction(nameof(ListarCliente));
            }
            catch
            {
                return View();
            }
        }

        // GET: ClienteController/DeletarCliente/5 OK
        public async Task<IActionResult> DeletarCliente(int id)
        {
            ClienteModel? cliente = new ClienteModel();
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(baseurl);
                httpClient.DefaultRequestHeaders.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage responseMessage = await httpClient.GetAsync("api/Clientes/" + id);
                if (responseMessage.IsSuccessStatusCode)
                {
                    var dados = responseMessage.Content.ReadAsStringAsync().Result;
                    cliente = JsonConvert.DeserializeObject<ClienteModel>(dados);
                }
            }
            return View(cliente);
        }

        // POST: ClienteController/Delete/5 OK
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletarCliente(int id,ClienteModel clienteApagando)
        {
            try
            {
                
                using (var httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(baseurl);
                    httpClient.DefaultRequestHeaders.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage responseMessage = await httpClient.DeleteAsync("api/Clientes/" + id);
                    
                }
                return RedirectToAction(nameof(ListarCliente));
            }
            catch
            {
                return View();
            }
        }
    }
}
