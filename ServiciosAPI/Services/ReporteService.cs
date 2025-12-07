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
    public class ReporteService : IReporteService
    {
        private HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public ReporteService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<List<Reporte>> DeleteReporteAsync(int id)
        {
            var apiUrl = _configuration["ApiUrls:ServiciosAPI"];
            var response = await _httpClient.DeleteAsync($"{apiUrl}/api/Reporte/{id}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<Reporte>>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }) ?? new List<Reporte>();
            }
            throw new Exception($"Error al eliminar el reporte con ID {id} desde la API.");
        }

        public async Task<Reporte> GetReporteByIdAsync(int id)
        {
            var apiUrl = _configuration["ApiUrls:ServiciosAPI"];
            var response = await _httpClient.GetAsync($"{apiUrl}/api/Reporte/{id}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<Reporte>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
            throw new Exception($"Error al obtener el reporte con ID {id} desde la API.");
        }

        public async Task<List<Reporte>> GetReportesAsync()
        {
            var apiUrl = _configuration["ApiUrls:ServiciosAPI"];
            var response = await _httpClient.GetAsync($"{apiUrl}/api/Reporte");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<Reporte>>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
            throw new Exception("Error al obtener los reportes desde la API.");
        }

        public async Task<List<Reporte>> PostReporteAsync(Reporte reporte)
        {
            var apiUrl = _configuration["ApiUrls:ServiciosAPI"];
            var json = JsonSerializer.Serialize(reporte);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{apiUrl}/api/Reporte", content);
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var reportes = JsonSerializer.Deserialize<List<Reporte>>(responseContent, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                return reportes ?? new List<Reporte>();
            }
            throw new Exception("Error al crear el reporte en la API.");
        }

        public async Task<List<Reporte>> PutReporteAsync(Reporte reporte)
        {
            var apiUrl = _configuration["ApiUrls:ServiciosAPI"];
            var json = JsonSerializer.Serialize(reporte);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{apiUrl}/api/Reporte/{reporte.ReporteID}", content);
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var reportes = JsonSerializer.Deserialize<List<Reporte>>(responseContent, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                return reportes ?? new List<Reporte>();
            }
            throw new Exception($"Error al actualizar el reporte con ID {reporte.ReporteID} en la API.");
        }
    }
}
