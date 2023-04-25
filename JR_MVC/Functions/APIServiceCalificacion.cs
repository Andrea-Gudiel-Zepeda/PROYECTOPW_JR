using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace JR_MVC.Functions
{
    public class APIServiceCalificacion
    {
        private static int timeout = 30;
        private static string baseurl = "https://localhost:7114/";

        //METODOS PARA EL CRUD - GENERALES
        public static async Task<System.Net.Http.HttpResponseMessage> GetListMethod(string url)
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            // Pass the handler to httpclient(from you are calling api)
            HttpClient httpClient = new HttpClient(clientHandler);
            httpClient.Timeout = TimeSpan.FromSeconds(timeout);
            var response = await httpClient.GetAsync(baseurl + url);
            return response;
        }

        public static async Task<System.Net.Http.HttpResponseMessage> DeleteMethod(string url, int id)
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            // Pass the handler to httpclient(from you are calling api)
            HttpClient httpClient = new HttpClient(clientHandler);
            httpClient.Timeout = TimeSpan.FromSeconds(timeout);
            var response = await httpClient.DeleteAsync(baseurl + $"{url}/{id}");
            return response;
        }

        public static async Task<System.Net.Http.HttpResponseMessage> SetMethod(string url, string json_)
        {
            var content = new StringContent(json_, Encoding.UTF8, "application/json");
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            // Pass the handler to httpclient(from you are calling api)
            HttpClient httpClient = new HttpClient(clientHandler);
            httpClient.Timeout = TimeSpan.FromSeconds(timeout);
            var response = await httpClient.PostAsync(baseurl + url, content);
            return response;
        }

        public static async Task<System.Net.Http.HttpResponseMessage> EditMethod(string url, int id, string json_)
        {
            var content = new StringContent(json_, Encoding.UTF8, "application/json");
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            // Pass the handler to httpclient(from you are calling api)
            HttpClient httpClient = new HttpClient(clientHandler);
            httpClient.Timeout = TimeSpan.FromSeconds(timeout);
            var response = await httpClient.PutAsync(baseurl + $"{url}/{id}", content);
            return response;
        }

        public static async Task<System.Net.Http.HttpResponseMessage> GetByIDMethod(string url, int id, int calificcacion)
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            // Pass the handler to httpclient(from you are calling api)
            HttpClient httpClient = new HttpClient(clientHandler);
            httpClient.Timeout = TimeSpan.FromSeconds(timeout);
            var response = await httpClient.GetAsync(baseurl + $"{url}/{id}");
            return response;
        }

        //METODOS DE LA CLASE CALIFICACION
        public static async System.Threading.Tasks.Task<IEnumerable<JR_DB.Calificacion>> CalificacionGetList() {
            var response = await GetListMethod("Calificacion/GetList");
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<IEnumerable<JR_DB.Calificacion>>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                throw new Exception(response.StatusCode.ToString());
            }
        }

        public static async System.Threading.Tasks.Task<JR_DB.GeneralResult> CalificacionSet(JR_MVC.Models.Calificacion object_to_serialize)
        {
            var json_ = Newtonsoft.Json.JsonConvert.SerializeObject(object_to_serialize);
            var response = await SetMethod("Calificacion/Set", json_);//httpClient.PostAsync(baseurl + "User/Set", content);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<JR_DB.GeneralResult>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                throw new Exception(response.StatusCode.ToString());
            }
        }

        public static async System.Threading.Tasks.Task<bool> GetCalificacionByID(int id, int calificacion)
        {
            var response = await GetByIDMethod("Calificacion/GetByID", id, calificacion);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<bool>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                throw new Exception(response.StatusCode.ToString());
            }
        }

        public static async System.Threading.Tasks.Task<JR_DB.GeneralResult> CalificacionEdit(JR_DB.User object_to_serialize, int id)
        {
            var json_ = Newtonsoft.Json.JsonConvert.SerializeObject(object_to_serialize);
            var response = await EditMethod("Calificacion/Edit", id, json_);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<JR_DB.GeneralResult>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                throw new Exception(response.StatusCode.ToString());
            }
        }

        public static async System.Threading.Tasks.Task<JR_DB.GeneralResult> CalificacionDelete(int id)
        {
            //var json_ = Newtonsoft.Json.JsonConvert.SerializeObject(object_to_serialize);
            var response = await DeleteMethod("Calificacion/Delete", id);//httpClient.PostAsync(baseurl + "Movies/Set", content);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<JR_DB.GeneralResult>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                throw new Exception(response.StatusCode.ToString());
            }
        }
    }
}
