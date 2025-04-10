using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace BoardJeopardy;

public partial class QuestionWindow : Window
{
    private readonly MediaPlayer _mediaPlayer = new();
    private readonly string _question;
    private readonly QuestionType _questionType;

    private QuestionWindow (Question question, string title, bool isAnswer = false)
    {
        InitializeComponent();
        _questionType = question.QuType;
        _question = !isAnswer ? question.Questi : question.Answer;
        Title = title;

        Init();
    }

    private void Init()
    {
        switch (_questionType)
        {
            default:
            case QuestionType.Text:
            {
                Text.Visibility = Visibility.Visible;
                Audio.Visibility = Visibility.Collapsed;
                Image.Visibility = Visibility.Collapsed;

                Text.Content = _question;
                Width = 300;
                Height = 150;

                break;
            }
            case QuestionType.Audio:
            {
                Text.Visibility = Visibility.Collapsed;
                Audio.Visibility = Visibility.Visible;
                Image.Visibility = Visibility.Collapsed;

                _mediaPlayer.Open(new Uri(_question));

                Width = 300;
                Height = 90;
                break;
            }
            case QuestionType.Image:
            {
                Text.Visibility = Visibility.Collapsed;
                Audio.Visibility = Visibility.Collapsed;
                Image.Visibility = Visibility.Visible;

                Image.Source = new BitmapImage(new Uri(_question));

                Width = Image.Source.Width   + 20;
                Height = Image.Source.Height + 20;
                break;
            }
        }

        var timer = new DispatcherTimer
        {
            Interval = TimeSpan.FromSeconds(1)
        };
        timer.Tick += Timer_Tick;
        timer.Start();
    }

    private void Timer_Tick (object? sender, EventArgs e)
    {
        Status.Content = _mediaPlayer.Source != null
            ? $@"{_mediaPlayer.Position:mm\:ss} / {_mediaPlayer.NaturalDuration.TimeSpan:mm\:ss}"
            : "0:00 / 0:00";
    }

    private void Play_Click (object? sender, EventArgs e)
    {
        _mediaPlayer.Play();
    }

    private void Stop_Click (object sender, RoutedEventArgs e)
    {
        _mediaPlayer.Stop();
    }


    public static void Show (Question question, string title, Window owner)
    {
        var window = new QuestionWindow(question, title)
        {
            Owner = owner
        };
        window.ShowDialog();
        window._mediaPlayer.Close();

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