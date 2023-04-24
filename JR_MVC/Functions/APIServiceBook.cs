﻿using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace JR_MVC.Functions
{
    public class APIServiceBook
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

        public static async Task<System.Net.Http.HttpResponseMessage> GetByIDMethod(string url, int id)
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            // Pass the handler to httpclient(from you are calling api)
            HttpClient httpClient = new HttpClient(clientHandler);
            httpClient.Timeout = TimeSpan.FromSeconds(timeout);
            var response = await httpClient.GetAsync(baseurl + $"{url}/{id}");
            return response;
        }

        //METODOS DE LA CLASE BOOK
        public static async System.Threading.Tasks.Task<IEnumerable<JR_DB.Book>> BookGetList(){
            var response = await GetListMethod("Book/GetList");
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<IEnumerable<JR_DB.Book>>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                throw new Exception(response.StatusCode.ToString());
            }
        }

        public static async System.Threading.Tasks.Task<JR_DB.GeneralResult> BookSet(JR_MVC.Models.Book object_to_serialize)
        {
            var json_ = Newtonsoft.Json.JsonConvert.SerializeObject(object_to_serialize);
            var response = await SetMethod("Book/Set", json_);//httpClient.PostAsync(baseurl + "User/Set", content);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<JR_DB.GeneralResult>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                throw new Exception(response.StatusCode.ToString());
            }
        }

        public static async System.Threading.Tasks.Task<bool> GetBookByID(int id)
        {
            var response = await GetByIDMethod("Book/GetByID", id);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<bool>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                throw new Exception(response.StatusCode.ToString());
            }
        }

        public static async System.Threading.Tasks.Task<JR_DB.GeneralResult> BookEdit(JR_DB.User object_to_serialize, int id)
        {
            var json_ = Newtonsoft.Json.JsonConvert.SerializeObject(object_to_serialize);
            var response = await EditMethod("Book/Edit", id, json_);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<JR_DB.GeneralResult>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                throw new Exception(response.StatusCode.ToString());
            }
        }

        public static async System.Threading.Tasks.Task<JR_DB.GeneralResult> BookDelete(int id)
        {
            //var json_ = Newtonsoft.Json.JsonConvert.SerializeObject(object_to_serialize);
            var response = await DeleteMethod("Book/Delete", id);//httpClient.PostAsync(baseurl + "Movies/Set", content);
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