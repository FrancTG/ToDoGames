using ToDoGames.Model;

namespace ToDoGames.Core.DialogService
{
    public static class DialogService
    {
        public static PlayedGame OpenDialog(DialogViewModel vm)
        {
            DialogWindow win = new DialogWindow();
            win.DataContext = vm;
            win.ShowDialog();
            PlayedGame pg = (win.DataContext as DialogViewModel).PlayedGame;
            return pg;
        }
    }
}
