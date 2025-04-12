using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using BoardJeopardy.Model;

namespace PunktestandJeopardy.Viewmodel;

public class QuestionWindowViewmodel : BaseViewmodel
{
    private readonly string _question;
    private readonly QuestionType _questionType;

    public MediaPlayer MediaPlayer { get; } = new();

    public bool IsText => _questionType  == QuestionType.Text;
    public bool IsAudio => _questionType == QuestionType.Audio;
    public bool IsImage => _questionType == QuestionType.Image;


    public string TextContent => _question;


    public string StatusContent => MediaPlayer.Source != null
        ? $@"{MediaPlayer.Position:mm\:ss} / {MediaPlayer.NaturalDuration.TimeSpan:mm\:ss}"
        : "0:00 / 0:00";


    public QuestionWindowViewmodel (QuestionType questionType, string question)
    {
        _questionType = questionType;
        _question = question;

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

                Width = 300;
                Height = 150;

                break;
            }
            case QuestionType.Audio:
            {
                Text.Visibility = Visibility.Collapsed;
                Audio.Visibility = Visibility.Visible;
                Image.Visibility = Visibility.Collapsed;

                MediaPlayer.Open(new Uri(_question));

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
        if (MediaPlayer.Source != null)
        {
            OnPropertyChanged(nameof(StatusContent));
        }
    }

    private void Play (object? sender, EventArgs e)
    {
        MediaPlayer.Play();
    }

    private void Stop (object sender, RoutedEventArgs e)
    {
        MediaPlayer.Stop();
    }
}