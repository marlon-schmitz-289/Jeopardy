using System.IO;
using System.Windows;
using System.Windows.Controls;
using BoardJeopardy;
using Microsoft.Win32;

namespace BoardEditorJeopardy;

public partial class QuestionEditorWindow : Window
{
    private static readonly string _FOLDER_PATH = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "CoolJeopardy");

    private readonly Question _question;
    private readonly int _questionIndex;

    private QuestionEditorWindow (Question question, int questionIndex)
    {
        InitializeComponent();

        _questionIndex = questionIndex;
        _question = question;

        QuestionType.ItemsSource = Enum.GetValues(typeof(QuestionType));
        QuestionType.SelectedItem = _question.QuType;

        Question.Text = _question.Questi;
        Answer.Text = _question.Answer;
    }

    private void Save_Click (object sender, RoutedEventArgs e)
    {
        _question.QuType = (QuestionType)QuestionType.SelectedItem;
        _question.Questi = Question.Text;
        _question.Answer = Answer.Text;

        Close();
    }

    public static Question Show (Question question, int questionIndex, Window owner)
    {
        var window = new QuestionEditorWindow(question, questionIndex)
        {
            Owner = owner
        };
        window.ShowDialog();
        return window._question;
    }

    private void FileSelect_Click (object sender, RoutedEventArgs e)
    {
        var fileType = _question.QuType switch
        {
            BoardJeopardy.QuestionType.Image => "Image files (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg",
            BoardJeopardy.QuestionType.Audio => "Audio files (*.mp3;*.wav)|*.mp3;*.wav"
        };

        var fileDialog = new OpenFileDialog
        {
            Filter = fileType,
            Title = "Select a file",
            InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
        };

        if (fileDialog.ShowDialog() is not true)
        {
            return;
        }

        var fileName = Path.Combine(Path.GetDirectoryName(fileDialog.FileName), fileDialog.FileName);
        var filePath = Path.Combine(_FOLDER_PATH, $"{_questionIndex}.{fileDialog.FileName.Split('.').Last()}");

        Question.Text = filePath;

        if (File.Exists(filePath))
        {
            File.Copy(fileName, filePath, true);
            return;
        }

        File.Copy(fileName, filePath);
    }

    private void QuestionType_Changed (object sender, SelectionChangedEventArgs e)
    {
        _question.QuType = (QuestionType)QuestionType.SelectedItem;
        if (_question.QuType is BoardJeopardy.QuestionType.Image or BoardJeopardy.QuestionType.Audio)
        {
            FileSelect.Visibility = Visibility.Visible;
            Question.Visibility = Visibility.Collapsed;
        }
        else
        {
            FileSelect.Visibility = Visibility.Collapsed;
            Question.Visibility = Visibility.Visible;
        }
    }
}