using BodegaRV.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using ToDoGames.Core;
using ToDoGames.Core.DialogService;
using ToDoGames.interfaces;
using ToDoGames.Model;

namespace ToDoGames.ViewModel
{
    public class PlayedViewModel : IPage
    {
        public ObservableCollection<PlayedGame> PlayedGames { get; set; }
        public string PageTitle { get; set; }

        public ICommand RemoveGameCommand { get; set; }
        public ICommand EditGameCommand { get; set; }

        public PlayedViewModel()
        {
            PlayedGames = new ObservableCollection<PlayedGame>();

            RemoveGameCommand = new RelayCommand(o =>
            {
                DBConnection.RemovePlayedGame((o as PlayedGame).Id);
                GetPlayedGames();
            });

            EditGameCommand = new RelayCommand(o =>
            {
                PlayedGame game = o as PlayedGame;
                DialogViewModel vm = new DialogViewModel(game);
                PlayedGame pg = DialogService.OpenDialog(vm);
                DBConnection.UpdatePlayedGame(pg);
                GetPlayedGames();
            });
        }

        public void GetPlayedGames()
        {
            List<PlayedGame> listPlayedGames = DBConnection.GetPlayedGames();

            PlayedGames.Clear();

            foreach (PlayedGame game in listPlayedGames)
            {
                PlayedGames.Add(game);
            }
        }

        
    }

    
}
