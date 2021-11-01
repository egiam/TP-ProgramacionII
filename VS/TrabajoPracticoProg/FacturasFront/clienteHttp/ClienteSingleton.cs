using FacturasBack.dominio;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FacturasFront.clienteHttp
{
    class ClienteSingleton
    {
        private static ClienteSingleton instancia;
        private HttpClient cliente;
        private ClienteSingleton()
        {
            cliente = new HttpClient();
        }

        public static ClienteSingleton GetInstancia()
        {
            if (instancia == null)
                instancia = new ClienteSingleton();
            return instancia;
        }


        public async Task<string> GetAsync(string url)
        {
            var result = await cliente.GetAsync(url);
            var content = "";
            if (result.IsSuccessStatusCode)
                content = await result.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> PostAsync(string url, string data)
        {
            StringContent content = new StringContent(data, Encoding.UTF8,
            "application/json");
            var result = await cliente.PostAsync(url, content);
            var response = "";
            if (result.IsSuccessStatusCode)
                response = await result.Content.ReadAsStringAsync();
            return response;
        }

        public async Task<string> DeleteAsync(string url)
        {
            var result = await cliente.DeleteAsync(url);
            var content = "";
            if (result.IsSuccessStatusCode)
                content = await result.Content.ReadAsStringAsync();
            return content;
        }


        public async Task<bool> GrabarFacturaAsync(string url, string data)
        {
           
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            var result = await cliente.PostAsync(url, content);
            return result.IsSuccessStatusCode;

            //using (HttpClient client = new HttpClient())
            //{
            //    StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            //    var result = await client.PostAsync(url, content);
            //    return (int)result.StatusCode == 200;
            //}
        }

        public async Task<bool> EditarFacturaAsync(string url, string data)
        {

            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            var result = await cliente.PutAsync(url, content);
            return result.IsSuccessStatusCode;
        }

            public async Task<int> AsignarNumeroFacturaAsync(string url)//revisar nombre
        {
            var result = await cliente.GetStringAsync(url);
            return Int32.Parse(result);

        }

        public async Task<List<Articulo>> ConsultarArticulos(string url)//revisar duplicado
        {
          
            var result = await cliente.GetAsync(url);
            var contenido = await result.Content.ReadAsStringAsync();
            List<Articulo> lst = JsonConvert.DeserializeObject<List<Articulo>>(contenido);
            return lst;

        }

        public async Task<List<FormaPago>> ConsultarFormasPago(string url)
        {
            var result = await cliente.GetAsync(url);
            var contenido = await result.Content.ReadAsStringAsync();
            List<FormaPago> lst = JsonConvert.DeserializeObject<List<FormaPago>>(contenido);
            return lst;

        }

    


    }
}
