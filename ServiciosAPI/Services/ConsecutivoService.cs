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
    public class ConsecutivoService : IConsecutivoService
    {
        private HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public ConsecutivoService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<List<Consecutivo>> DeleteConsecutivoAsync(int id)
        {
            var apiUrl = _configuration["ApiUrls:ServiciosAPI"];
            var response = await _httpClient.DeleteAsync($"{apiUrl}/api/Consecutivo/{id}");
            if (response.IsSuccessStatusCode)
            {
                // La API devuelve NoContent (204) sin cuerpo, así que solo verificamos el éxito
                // Si hay contenido, intentamos deserializarlo, si no, retornamos lista vacía
                var content = await response.Content.ReadAsStringAsync();
                if (!string.IsNullOrWhiteSpace(content))
                {
                    return JsonSerializer.Deserialize<List<Consecutivo>>(content, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    }) ?? new List<Consecutivo>();
                }
                return new List<Consecutivo>();
            }
            throw new Exception($"Error al eliminar el consecutivo con ID {id} desde la API.");
        }

        public async Task<Consecutivo> GetConsecutivoByIdAsync(int id)
        {
            var apiUrl = _configuration["ApiUrls:ServiciosAPI"];
            var response = await _httpClient.GetAsync($"{apiUrl}/api/Consecutivo/{id}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<Consecutivo>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
            throw new Exception($"Error al obtener el consecutivo con ID {id} desde la API.");
        }

        public async Task<List<Consecutivo>> GetConsecutivosAsync()
        {
            var apiUrl = _configuration["ApiUrls:ServiciosAPI"];
            var response = await _httpClient.GetAsync($"{apiUrl}/api/Consecutivo");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<Consecutivo>>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
            throw new Exception("Error al obtener los consecutivos desde la API.");
        }

        public async Task<List<Consecutivo>> GetAllConsecutivosAsync()
        {
            var apiUrl = _configuration["ApiUrls:ServiciosAPI"];
            var response = await _httpClient.GetAsync($"{apiUrl}/api/Consecutivo/All");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<Consecutivo>>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
            throw new Exception("Error al obtener todos los consecutivos desde la API.");
        }

        public async Task<Consecutivo> PostConsecutivoAsync(Consecutivo consecutivo)
        {
            var apiUrl = _configuration["ApiUrls:ServiciosAPI"];
            var json = JsonSerializer.Serialize(consecutivo);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{apiUrl}/api/Consecutivo", content);
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var consecutivoCreado = JsonSerializer.Deserialize<Consecutivo>(responseContent, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                return consecutivoCreado ?? consecutivo;
            }
            throw new Exception("Error al crear el consecutivo en la API.");
        }

        public async Task<List<Consecutivo>> PutConsecutivoAsync(Consecutivo consecutivo)
        {
            var apiUrl = _configuration["ApiUrls:ServiciosAPI"];
            var json = JsonSerializer.Serialize(consecutivo);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{apiUrl}/api/Consecutivo/{consecutivo.ConsecutivoID}", content);
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var consecutivos = JsonSerializer.Deserialize<List<Consecutivo>>(responseContent, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                return consecutivos ?? new List<Consecutivo>();
            }
            throw new Exception($"Error al actualizar el consecutivo con ID {consecutivo.ConsecutivoID} en la API.");
        }
    }
}
