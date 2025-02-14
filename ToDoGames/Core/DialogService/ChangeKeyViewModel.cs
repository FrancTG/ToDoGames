using BodegaRV.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows;

namespace ToDoGames.Core.DialogService
{
    public class ChangeKeyViewModel
    {
        public RelayCommand SaveKeyCommand { get; set; }
        public RelayCommand GetNewKeyCommand { get; set; }

        public ChangeKeyViewModel()
        {
            SaveKeyCommand = new RelayCommand(o =>
            {
                FileStream fs = new FileStream(Environment.CurrentDirectory + "\\key.txt", FileMode.OpenOrCreate);
                fs.SetLength(0);
                StreamWriter sw = new StreamWriter(fs);
                sw.Write(o as string);
                sw.Close();
                MessageBox.Show("Llave actualizada correctamente.", "Info.");
            });

            GetNewKeyCommand = new RelayCommand(o =>
            {
                Process.Start("explorer","https://rawg.io/apidocs");
            });
        }
    }
}
