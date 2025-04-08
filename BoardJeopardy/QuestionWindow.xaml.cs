using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace BoardJeopardy;

public partial class QuestionWindow : Window
{
    private Question _question;
    private MediaPlayer _mediaPlayer = new MediaPlayer();
    
    private QuestionWindow(Question question, string title)
    {
        InitializeComponent();
        _question = question;
        Title = title;
        
        Init();
    }

    private void Init()
    {
        switch (_question.QuType)
        {
            default:
            case QuestionType.Text:
            {
                Text.Visibility = Visibility.Visible;
                Audio.Visibility = Visibility.Collapsed;
                Image.Visibility = Visibility.Collapsed;
                
                Text.Text = _question.Questi;

                Width = 300;
                Height = 80;
                break;
            }
            case QuestionType.Audio:
            {
                Text.Visibility = Visibility.Collapsed;
                Audio.Visibility = Visibility.Visible;
                Image.Visibility = Visibility.Collapsed;
                
                _mediaPlayer.Open(new Uri(_question.Questi));
                
                Width = 300;
                Height = 80;
                break;
            }
            case QuestionType.Image:
            {
                Text.Visibility = Visibility.Collapsed;
                Audio.Visibility = Visibility.Collapsed;
                Image.Visibility = Visibility.Visible;
                
                Image.Source = new BitmapImage(new Uri(_question.Questi));
                
                Width = 500;
                Height = 500;
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
    
    private void Timer_Tick(object? sender, EventArgs e)
    {
        Status.Content = _mediaPlayer.Source != null ? $@"{_mediaPlayer.Position:mm\:ss} / {_mediaPlayer.NaturalDuration.TimeSpan:mm\:ss}" : "0:00 / 0:00";
    }

    private void Play_Click(object? sender, EventArgs e)
    {
        _mediaPlayer.Play();
    }

    private void Stop_Click(object sender, RoutedEventArgs e)
    {
        _mediaPlayer.Stop();
    }
    
    
    public static void Show(Question question, string title)
    {
        var window = new QuestionWindow(question, title);
        window.ShowDialog();
        window._mediaPlayer.Close();

        MessageBox.Show(question.Answer, window.Title);
    }
}