using Firebase.Database;
using Firebase.Database.Query;
using Somnix.DTO.Telemetria;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Somnix.BLL.Services
{
    public class FirebaseService
    {
        private readonly FirebaseClient _firebaseClient;

        public FirebaseService()
        {
            // URL de conexión para la librería FirebaseClient
            _firebaseClient = new FirebaseClient("https://somnix-cfdb1-default-rtdb.firebaseio.com/");
        }

        public async Task InsertarTelemetriaAsync(TelemetriaFirebase datos)
        {
            // Inyecta en el nodo: somnix/telemetriaGorra creando una ID única por POST
            await _firebaseClient
                .Child("somnix")
                .Child("telemetriaGorra")
                .PostAsync(datos);
        }

        public async Task SetComandoAsync(string comando)
        {
            // URL apuntando directamente al nodo JSON para evitar IDs aleatorios
            string firebaseUrl = "https://somnix-cfdb1-default-rtdb.firebaseio.com/Control/Comando.json";

            using var client = new HttpClient();

            // Firebase exige que los strings primitivos viajen serializados (entre comillas dobles)
            var jsonPayload = JsonSerializer.Serialize(comando);
            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            // PUT sobrescribe el nodo actual en lugar de acumular registros
            var response = await client.PutAsync(firebaseUrl, content);

            if (!response.IsSuccessStatusCode)
            {
                var errorBody = await response.Content.ReadAsStringAsync();
                throw new System.Exception($"Error inyectando comando en Firebase: {response.StatusCode} - {errorBody}");
            }
        }
    }
}
