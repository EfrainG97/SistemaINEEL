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
    public class UsuarioService : IUsuarioService
    {
        private HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public UsuarioService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<List<Usuario>> DeleteUsuarioAsync(int id)
        {
            var apiUrl = _configuration["ApiUrls:ServiciosAPI"];
            var response = await _httpClient.DeleteAsync($"{apiUrl}/api/Usuario/{id}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<Usuario>>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }) ?? new List<Usuario>();
            }
            throw new Exception($"Error al eliminar el usuario con ID {id} desde la API.");
        }

        public async Task<Usuario> GetUsuarioByIdAsync(int id)
        {
            var apiUrl = _configuration["ApiUrls:ServiciosAPI"];
            var response = await _httpClient.GetAsync($"{apiUrl}/api/Usuario/{id}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<Usuario>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
            throw new Exception($"Error al obtener el usuario con ID {id} desde la API.");
        }

        public async Task<List<Usuario>> GetUsuariosAsync()
        {
            var apiUrl = _configuration["ApiUrls:ServiciosAPI"];
            var response = await _httpClient.GetAsync($"{apiUrl}/api/Usuario");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<Usuario>>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
            throw new Exception("Error al obtener los usuarios desde la API.");
        }

        public async Task<List<Usuario>> PostUsuarioAsync(Usuario usuario)
        {
            var apiUrl = _configuration["ApiUrls:ServiciosAPI"];
            var json = JsonSerializer.Serialize(usuario);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{apiUrl}/api/Usuario", content);
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var usuarios = JsonSerializer.Deserialize<List<Usuario>>(responseContent, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                return usuarios ?? new List<Usuario>();
            }
            throw new Exception("Error al crear el usuario en la API.");
        }

        public async Task<List<Usuario>> PutUsuarioAsync(Usuario usuario)
        {
            var apiUrl = _configuration["ApiUrls:ServiciosAPI"];
            var json = JsonSerializer.Serialize(usuario);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{apiUrl}/api/Usuario/{usuario.UsuarioID}", content);
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var usuarios = JsonSerializer.Deserialize<List<Usuario>>(responseContent, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                return usuarios ?? new List<Usuario>();
            }
            throw new Exception($"Error al actualizar el usuario con ID {usuario.UsuarioID} en la API.");
        }
    }
}
