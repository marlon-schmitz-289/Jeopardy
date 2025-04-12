using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using BoardJeopardy.Model;
using PunktestandJeopardy.Viewmodel;

namespace BoardJeopardy.View;

public partial class QuestionWindow : Window
{
    private readonly QuestionWindowViewmodel _viewmodel;

    private QuestionWindow (Question question, string title, bool isAnswer = false)
    {
        InitializeComponent();

        _viewmodel = new(question.QuType, !isAnswer ? question.Questi : question.Answer);
        DataContext = _viewmodel;

        Title = title;
    }


    public static void Show (Question question, string title, Window owner)
    {
        var window = new QuestionWindow(question, title)
        {
            Owner = owner
        };
        window.ShowDialog();
        window._viewmodel.MediaPlayer.Close();

        window = new QuestionWindow(question, title, true)
        {
            Owner = owner
        };
        window.ShowDialog();
        window._mediaPlayer.Close();
    }

    public static void ShowMessage (string message, string title, Window owner)
    {
        var window = new QuestionWindow(new Question(message, message), title, true)
        {
            Owner = owner
        };
        window.ShowDialog();
        window._mediaPlayer.Close();
    }
}