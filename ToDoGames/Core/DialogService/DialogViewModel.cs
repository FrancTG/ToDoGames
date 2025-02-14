using BodegaRV.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using ToDoGames.Model;

namespace ToDoGames.Core.DialogService
{
    public class DialogViewModel : ObservableObject
    {
        public PlayedGame PlayedGame { get; set; }

        public RelayCommand RateGameCommand { get; set; }
        public DialogViewModel(PlayedGame game)
        {
            PlayedGame = game;

            RateGameCommand = new RelayCommand(o =>
            {
                PlayedGame.AddedDate = DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + DateTime.Now.Day;
                (o as Window).Close();
            });
        }
    }
}
