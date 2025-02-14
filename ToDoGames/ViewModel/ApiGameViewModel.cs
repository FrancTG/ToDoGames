using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using BodegaRV.Core;
using Newtonsoft.Json;
using ToDoGames.Core;
using ToDoGames.Core.DialogService;
using ToDoGames.interfaces;
using ToDoGames.Model;

namespace ToDoGames.ViewModel
{
    public class ApiGameViewModel : ObservableObject, IPage
    {
        public ObservableCollection<Result> ApiGames { get; private set; }
        public ICommand BuscarJuegosApiCommand { get; set; }
        public ICommand OpenRateDialog { get; set; }
        public ICommand AddGameToPlayCommand { get; set; }
        public RelayCommand RateGameCommand { get; set; }
        public string PageTitle { get; set; }

        private string apiKey = "";

        public ApiGameViewModel()
        {
            
            ApiGames = new ObservableCollection<Result>();

            apiKey = GetApiKey();
            
            BuscarJuegosApiCommand = new RelayCommand(o =>
            {
                ApiGames.Clear();
                GetApiGames((o as TextBox).Text);
            });

            RateGameCommand = new RelayCommand(o =>
            {
                PlayedGame game = (o as Result).ToPlayedGame();
                DialogViewModel vm = new DialogViewModel(game);
                PlayedGame pg = DialogService.OpenDialog(vm);
                DBConnection.AddPlayedGame(pg);
            });

            AddGameToPlayCommand = new RelayCommand(o =>
            {
                Result game = o as Result;
                DBConnection.AddGameToPlay(new PlayedGame(game.id, game.name, game.released, game.background_image, DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + DateTime.Now.Day, new Score(0, 0, 0, 0, 0, 0, 0, 0, "")));
            });
        }

        private string GetApiKey()
        {
            StreamReader st = new StreamReader(new FileStream(Environment.CurrentDirectory + "\\key.txt", FileMode.OpenOrCreate));
            string key = st.ReadLine();
            if (key == null)
            {
                key = "";
            }
            st.Close();
            return key;
        }

        private async void GetApiGames(string gameName)
        {
            apiKey = GetApiKey();
            if (apiKey == "")
            {
                MessageBox.Show("Falta la API Key\nPuedes solicitar la API Key en la siguiente dirección: https://rawg.io/apidocs", "Error");
            }
            else
            {
                using (HttpClient client = new HttpClient())
                {
                    try
                    {
                        var response = await client.GetAsync("https://rawg.io/api/games?search=" + gameName + "&key=" + apiKey);
                        response.EnsureSuccessStatusCode();
                        if (response.IsSuccessStatusCode)
                        {
                            ApiResponse result = JsonConvert.DeserializeObject<ApiResponse>(await response.Content.ReadAsStringAsync());

                            foreach (Result hola in result.results)
                            {
                                ApiGames.Add(hola);
                            }
                        }
                    }catch (HttpRequestException)
                    {
                        MessageBox.Show("Error de red o la API Key es incorrecta.\n Posibles soluciones:\n - Comprueba la conexión a internet.\n - Comprueba que la key es correcta.\n - Vuelve a intentarlo.", "Error");
                    }
                    
                }
            }
        }
    }
}
