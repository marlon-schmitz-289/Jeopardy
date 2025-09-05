using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using NAudio.Wave;

namespace BoardJeopardy;

public partial class QuestionWindow : Window
{

    private readonly string _question;
    private readonly QuestionType _questionType;
    private AudioFileReader _audioFileReader;
    private IWavePlayer _mediaPlayer;

    private QuestionWindow (Question question, string title, bool isAnswer = false)
    {
        InitializeComponent();
        _questionType = !isAnswer ? question.QuType : QuestionType.Text;
        _question = !isAnswer ? question.Questi : question.Answer;
        Title = title;

        Init(isAnswer);
    }

    private void Init (bool isAnswer)
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

                _audioFileReader = new AudioFileReader(_question);
                _mediaPlayer = new WaveOutEvent();
                _mediaPlayer.Init(_audioFileReader);
                _mediaPlayer.Volume = 0.5f;

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
        try
        {
            if (_audioFileReader != null && _mediaPlayer != null)
            {
                Status.Content = $@"{_audioFileReader.CurrentTime:mm\:ss} / {_audioFileReader.TotalTime:mm\:ss}";
            }
            else
            {
                Status.Content = "0:00 / 0:00";
            }
        }
        catch
        {
            // ignored
        }
    }

    private void Play_Click (object? sender, EventArgs e)
    {
        _mediaPlayer.Play();
    }

    private void Stop_Click (object sender, RoutedEventArgs e)
    {
        _mediaPlayer.Stop();
        _audioFileReader.CurrentTime = TimeSpan.Zero;
    }

    private void DisposeAudio()
    {
        if (_mediaPlayer != null)
        {
            _mediaPlayer.Stop();
            _mediaPlayer.Dispose();
            _mediaPlayer = null;
        }

        if (_audioFileReader != null)
        {
            _audioFileReader.Dispose();
            _audioFileReader = null;
        }
    }


    public static void Show (Question question, string title, Window owner)
    {
        var window = new QuestionWindow(question, title)
        {
            Owner = owner
        };
        window.ShowDialog();
        window.DisposeAudio();

        window = new QuestionWindow(question, title, true)
        {
            Owner = owner
        };
        window.ShowDialog();
        window.DisposeAudio();
    }

    public static void ShowMessage (string message, string title, Window owner)
    {
        var window = new QuestionWindow(new Question(message, message), title, true)
        {
            Owner = owner
        };
        window.ShowDialog();
        window.DisposeAudio();
    }
}