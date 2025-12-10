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
    public class RolService : IRolService
    {
        #region Campos
        private HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        #endregion

        #region Constructor
        public RolService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }
        #endregion

        #region Métodos GET
        public async Task<List<Rol>> GetRolesAsync()
        {
            var apiUrl = _configuration["ApiUrls:ServiciosAPI"];
            var response = await _httpClient.GetAsync($"{apiUrl}/api/Rol");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<Rol>>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
            throw new Exception("Error al obtener los roles desde la API.");
        }

        public async Task<Rol> GetRolByIdAsync(int id)
        {
            var apiUrl = _configuration["ApiUrls:ServiciosAPI"];
            var response = await _httpClient.GetAsync($"{apiUrl}/api/Rol/{id}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<Rol>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
            throw new Exception($"Error al obtener el rol con ID {id} desde la API.");
        }
        #endregion

        #region Métodos POST
        public async Task<List<Rol>> PostRolAsync(Rol rol)
        {
            var apiUrl = _configuration["ApiUrls:ServiciosAPI"];
            var json = JsonSerializer.Serialize(rol);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{apiUrl}/api/Rol", content);
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var roles = JsonSerializer.Deserialize<List<Rol>>(responseContent, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                return roles ?? new List<Rol>();
            }
            throw new Exception("Error al crear el rol en la API.");
        }
        #endregion

        #region Métodos PUT
        public async Task<List<Rol>> PutRolAsync(Rol rol)
        {
            var apiUrl = _configuration["ApiUrls:ServiciosAPI"];
            var json = JsonSerializer.Serialize(rol);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{apiUrl}/api/Rol/{rol.RolID}", content);
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var roles = JsonSerializer.Deserialize<List<Rol>>(responseContent, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                return roles ?? new List<Rol>();
            }
            throw new Exception($"Error al actualizar el rol con ID {rol.RolID} en la API.");
        }
        #endregion

        #region Métodos DELETE
        public async Task<List<Rol>> DeleteRolAsync(int id)
        {
            var apiUrl = _configuration["ApiUrls:ServiciosAPI"];
            var response = await _httpClient.DeleteAsync($"{apiUrl}/api/Rol/{id}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<Rol>>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }) ?? new List<Rol>();
            }
            throw new Exception($"Error al eliminar el rol con ID {id} desde la API.");
        }
        #endregion
    }
}
