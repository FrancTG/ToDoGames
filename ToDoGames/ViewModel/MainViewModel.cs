using BodegaRV.Core;
using System.Collections.Generic;
using ToDoGames.Core;
using ToDoGames.Core.DialogService;
using ToDoGames.interfaces;

namespace ToDoGames.ViewModel
{
    public class MainViewModel : ObservableObject
    {
        public RelayCommand SelectPageCommand { get; set; }
        public RelayCommand ChangeKeyCommand { get; set; }

        public Dictionary<PageName, IPage> Pages { get; }

        private IPage selectedPage;
        public IPage SelectedPage
        {
            get => selectedPage;
            set
            {
                selectedPage = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            DBConnection.StartDB();

            Pages = new Dictionary<PageName, IPage>
            {
                { PageName.PLAYED, new PlayedViewModel() },
                { PageName.NEW, new ApiGameViewModel() },
                { PageName.TOPLAY, new ToPlayViewModel() }
            };

            SelectedPage = Pages[PageName.PLAYED];
            (Pages[PageName.PLAYED] as PlayedViewModel).GetPlayedGames();

            SelectPageCommand = new RelayCommand(o =>
            {
                if (o is PageName pageName && Pages.TryGetValue(pageName, out IPage selectedPage))
                {
                    SelectedPage = selectedPage;

                    if (selectedPage is PlayedViewModel)
                    {
                        (Pages[PageName.PLAYED] as PlayedViewModel).GetPlayedGames();
                    }

                    if (selectedPage is ToPlayViewModel)
                    {
                        (Pages[PageName.TOPLAY] as ToPlayViewModel).GetGamesToPlay();
                    }
                }
            });

            ChangeKeyCommand = new RelayCommand(o =>
            {
                DialogChangeKey win = new DialogChangeKey();
                win.DataContext = new ChangeKeyViewModel();
                win.ShowDialog();
            });

        }
    }
}
