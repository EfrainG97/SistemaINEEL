using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using LibreriaModelos;
using Microsoft.Extensions.Configuration;
using ServiciosAPI.Interfaces;

namespace ServiciosAPI.Services
{
    public class SistemaService : ISistemaService
    {
        #region Campos
        private HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        #endregion

        #region Constructor
        public SistemaService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }
        #endregion

        #region Métodos GET
        public async Task<List<Sistema>> GetSistemasAsync()
        {
            var apiUrl = _configuration["ApiUrls:ServiciosAPI"];
            var response = await _httpClient.GetAsync($"{apiUrl}/api/Sistema");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<Sistema>>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
            throw new Exception("Error al obtener los sistemas desde la API.");
        }

        public async Task<Sistema> GetSistemaByIdAsync(int id)
        {
            var apiUrl = _configuration["ApiUrls:ServiciosAPI"];
            var response = await _httpClient.GetAsync($"{apiUrl}/api/Sistema/{id}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<Sistema>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
            throw new Exception($"Error al obtener el sistema con ID {id} desde la API.");
        }
        #endregion

        #region Métodos POST
        public async Task<List<Sistema>> PostSistemaAsync(Sistema sistema)
        {
            var apiUrl = _configuration["ApiUrls:ServiciosAPI"];
            var json = JsonSerializer.Serialize(sistema);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{apiUrl}/api/Sistema", content);
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var sistemas = JsonSerializer.Deserialize<List<Sistema>>(responseContent, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                return sistemas ?? new List<Sistema>();
            }
            throw new Exception("Error al crear el sistema en la API.");
        }
        #endregion

        #region Métodos PUT
        public async Task<List<Sistema>> PutSistemaAsync(Sistema sistema)
        {
            var apiUrl = _configuration["ApiUrls:ServiciosAPI"];
            var json = JsonSerializer.Serialize(sistema);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{apiUrl}/api/Sistema/{sistema.SistemaID}", content);
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var sistemas = JsonSerializer.Deserialize<List<Sistema>>(responseContent, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                return sistemas ?? new List<Sistema>();
            }
            throw new Exception($"Error al actualizar el sistema con ID {sistema.SistemaID} en la API.");
        }
        #endregion

        #region Métodos DELETE
        public async Task<List<Sistema>> DeleteSistemaAsync(int id)
        {
            var apiUrl = _configuration["ApiUrls:ServiciosAPI"];
            var response = await _httpClient.DeleteAsync($"{apiUrl}/api/Sistema/{id}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<Sistema>>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }) ?? new List<Sistema>();
            }
            throw new Exception($"Error al eliminar el sistema con ID {id} desde la API.");
        }
        #endregion
    }
}
