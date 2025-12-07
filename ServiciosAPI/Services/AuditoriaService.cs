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
    public class AuditoriaService : IAuditoriaService
    {
        private HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public AuditoriaService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<List<Auditoria>> DeleteAuditoriaAsync(int id)
        {
            var apiUrl = _configuration["ApiUrls:ServiciosAPI"];
            var response = await _httpClient.DeleteAsync($"{apiUrl}/api/Auditoria/{id}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<Auditoria>>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }) ?? new List<Auditoria>();
            }
            throw new Exception($"Error al eliminar la auditoría con ID {id} desde la API.");
        }

        public async Task<Auditoria> GetAuditoriaByIdAsync(int id)
        {
            var apiUrl = _configuration["ApiUrls:ServiciosAPI"];
            var response = await _httpClient.GetAsync($"{apiUrl}/api/Auditoria/{id}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<Auditoria>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
            throw new Exception($"Error al obtener la auditoría con ID {id} desde la API.");
        }

        public async Task<List<Auditoria>> GetAuditoriasAsync()
        {
            var apiUrl = _configuration["ApiUrls:ServiciosAPI"];
            var response = await _httpClient.GetAsync($"{apiUrl}/api/Auditoria");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<Auditoria>>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
            throw new Exception("Error al obtener las auditorías desde la API.");
        }

        public async Task<List<Auditoria>> PostAuditoriaAsync(Auditoria auditoria)
        {
            var apiUrl = _configuration["ApiUrls:ServiciosAPI"];
            var json = JsonSerializer.Serialize(auditoria);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{apiUrl}/api/Auditoria", content);
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var auditorias = JsonSerializer.Deserialize<List<Auditoria>>(responseContent, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                return auditorias ?? new List<Auditoria>();
            }
            throw new Exception("Error al crear la auditoría en la API.");
        }

        public async Task<List<Auditoria>> PutAuditoriaAsync(Auditoria auditoria)
        {
            var apiUrl = _configuration["ApiUrls:ServiciosAPI"];
            var json = JsonSerializer.Serialize(auditoria);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{apiUrl}/api/Auditoria/{auditoria.AuditoriaID}", content);
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var auditorias = JsonSerializer.Deserialize<List<Auditoria>>(responseContent, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                return auditorias ?? new List<Auditoria>();
            }
            throw new Exception($"Error al actualizar la auditoría con ID {auditoria.AuditoriaID} en la API.");
        }
    }
}
