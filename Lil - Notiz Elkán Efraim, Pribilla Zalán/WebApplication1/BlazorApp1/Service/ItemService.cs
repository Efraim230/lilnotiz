using CommonLibrary;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text;
using static System.Net.WebRequestMethods;

namespace BlazorApp1.Service
{
    public class ItemService
    {
        public async Task<bool> RemoveNotizIdAsync(string felhasznaloNev, string notizId)
        {
            var content = new StringContent(JsonSerializer.Serialize(notizId), Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"api/felhasznalok2/removePart/{felhasznaloNev}", content);

            return response.IsSuccessStatusCode;
        }

      


        public async Task<bool> AddFelhasznaloNotizAsync(string felhasznaloNev, string ujNotizId)
        {
            // Először lekérjük az adott felhasználó adatait
            var felhasznaloLista = await GetItems();
            var felhasznalo = felhasznaloLista.FirstOrDefault(f => f.felhasznaloNev == felhasznaloNev);

            if (felhasznalo == null)
            {
                return false; // Ha nincs ilyen felhasználó, akkor nincs mit frissíteni
            }

            // Ha már van érték, akkor hozzáfűzzük az új NotizID-t
            string aktualisNotizId = felhasznalo.FELHASZNALONOTIZID ?? "";
            string frissitettNotizId = aktualisNotizId + ujNotizId; // Stringek összefűzése

            // Az API-nak küldött új adat
            var requestBody = new { FELHASZNALONOTIZID = frissitettNotizId };

            var response = await _httpClient.PutAsJsonAsync($"api/felhasznalok2/notiz/{felhasznaloNev}", requestBody);

            return response.IsSuccessStatusCode;
        }


        public string? Username { get; set; }
       public string? ProjectName { get; set; }

        public string? ModositaniKivantNotizIdProp { get; set; }


        private readonly HttpClient _httpClient;

        public ItemService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        public async Task<List<Felhasznalok2>> GetItems()
        {
           
            var response= await _httpClient.GetFromJsonAsync<List<Felhasznalok2>>("api/Felhasznalok2");
            return response ?? new List<Felhasznalok2>();
           
        }


        //Adatbázisba feltölti a módosított projektek részleteit
        public async Task<bool> UpdateNotiz(Notiz notiz)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/notizs/{notiz.ID}", notiz);
                return response.IsSuccessStatusCode;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Hiba a notiz frissítése közben: {ex.Message}");
                return false;
            }
        }



        //Lekér egy notizid-t az apiból
        public async Task<Notiz?> GetNotizById(string id)
        {
            var response = await _httpClient.GetAsync($"api/notizs/{id}");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Notiz>();
            }

            return null;


        }



        public async Task AddItem(Felhasznalok2 felhasznalo)
        {
        

            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/felhasznalok2", felhasznalo);
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"valami hiba történt: {ex.Message}");
                throw;
            }
        }

        public async Task<List<Notiz>> GetNotiz()
        {
            var response = await _httpClient.GetFromJsonAsync<List<Notiz>>("api/notizs");
            return response ?? new List<Notiz>();
            
        }

        public async Task AddNotiz(Notiz notiz)
        {


            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/notizs", notiz);
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"valami hiba történt: {ex.Message}");
                throw;
            }
        }


    }
}
