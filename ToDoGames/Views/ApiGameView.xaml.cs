using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ToDoGames.ViewModel;

namespace ToDoGames.Views
{
    /// <summary>
    /// Lógica de interacción para ApiGameView.xaml
    /// </summary>
    public partial class ApiGameView : UserControl
    {
        public ApiGameView()
        {
            InitializeComponent();
            ApiGameViewModel vm = new ApiGameViewModel();
            DataContext = vm;
        }
    }
}
