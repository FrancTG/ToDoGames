using BodegaRV.Core;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ToDoGames.Core;
using ToDoGames.Core.DialogService;
using ToDoGames.interfaces;
using ToDoGames.Model;

namespace ToDoGames.ViewModel
{
    public class ToPlayViewModel : IPage
    {
        public string PageTitle { get; set; }

        public ICommand RateGameCommand { get; set; }
        public ICommand RemoveGameCommand { get; set; }

        public ObservableCollection<PlayedGame> GamesToPlay { get; set; }
        public ToPlayViewModel()
        {
            GamesToPlay = new ObservableCollection<PlayedGame>();

            RateGameCommand = new RelayCommand(o =>
            {
                PlayedGame game = o as PlayedGame;
                DialogViewModel vm = new DialogViewModel(game);
                PlayedGame pg = DialogService.OpenDialog(vm);
                DBConnection.AddPlayedGame(pg);
                DBConnection.RemoveGameToPlay(pg.Id);
                GetGamesToPlay();
            });

            RemoveGameCommand = new RelayCommand(o =>
            {
                PlayedGame pg = o as PlayedGame;
                DBConnection.RemoveGameToPlay(pg.Id);
                GetGamesToPlay();
            });
        }

        public void GetGamesToPlay()
        {
            List<PlayedGame> gamesToPlayList = DBConnection.GetGamesToPlay();

            GamesToPlay.Clear();

            foreach (PlayedGame game in gamesToPlayList)
            {
                GamesToPlay.Add(game);
            }
        }
    }
}
