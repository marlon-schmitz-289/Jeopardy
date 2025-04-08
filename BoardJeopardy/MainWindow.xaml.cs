using System.IO;
using System.Windows;

namespace BoardJeopardy;

/// <summary>
///     Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private static readonly string _FOLDER_PATH = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "CoolJeopardy");
    private static readonly string _USERNAMES_PATH = Path.Combine(_FOLDER_PATH, "users.txt");
    private static readonly string _CATEGORIES_PATH = Path.Combine(_FOLDER_PATH, "categories.txt");
    private static readonly string _QUESTIONS_PATH = Path.Combine(_FOLDER_PATH, "questions.txt");

    private readonly List<Question> _questions;


    public MainWindow()
    {
        InitializeComponent();

        try
        {
            SetCategories();
            _questions = ReadQuestions().ToList();
        }
        catch (Exception e)
        {
            MessageBox.Show(e.Message);
            Application.Current.Shutdown(e.HResult);
        }
    }


    #region Category One

    public void btnCatOneOne_Click(object sender, EventArgs e)
    {
        QuestionWindow.Show(_questions[0], $"{lblColTitleOne.Content} - {btnCatOneOne.Content}");
    }

    public void btnCatOneTwo_Click(object sender, EventArgs e)
    {
        QuestionWindow.Show(_questions[1], $"{lblColTitleOne.Content} - {btnCatOneTwo.Content}");
    }

    public void btnCatOneThree_Click(object sender, EventArgs e)
    {
        QuestionWindow.Show(_questions[2], $"{lblColTitleOne.Content} - {btnCatOneThree.Content}");
    }

    public void btnCatOneFour_Click(object sender, EventArgs e)
    {
        QuestionWindow.Show(_questions[3], $"{lblColTitleOne.Content} - {btnCatOneFour.Content}");
    }

    public void btnCatOneFive_Click(object sender, EventArgs e)
    {
        QuestionWindow.Show(_questions[4], $"{lblColTitleOne.Content} - {btnCatOneFive.Content}");
    }

    #endregion


    #region Category Two

    public void btnCatTwoOne_Click(object sender, EventArgs e)
    {
        QuestionWindow.Show(_questions[5], $"{lblColTitleTwo.Content} - {btnCatTwoOne.Content}");
    }

    public void btnCatTwoTwo_Click(object sender, EventArgs e)
    {
        QuestionWindow.Show(_questions[6], $"{lblColTitleTwo.Content} - {btnCatTwoTwo.Content}");
    }

    public void btnCatTwoThree_Click(object sender, EventArgs e)
    {
        QuestionWindow.Show(_questions[7], $"{lblColTitleTwo.Content} - {btnCatTwoThree.Content}");
    }

    public void btnCatTwoFour_Click(object sender, EventArgs e)
    {
        QuestionWindow.Show(_questions[8], $"{lblColTitleTwo.Content} - {btnCatTwoFour.Content}");
    }

    public void btnCatTwoFive_Click(object sender, EventArgs e)
    {
        QuestionWindow.Show(_questions[9], $"{lblColTitleTwo.Content} - {btnCatTwoFive.Content}");
    }

    #endregion


    #region Category Three

    public void btnCatThreeOne_Click(object sender, EventArgs e)
    {
        QuestionWindow.Show(_questions[10], $"{lblColTitleThree.Content} - {btnCatThreeOne.Content}");
    }

    public void btnCatThreeTwo_Click(object sender, EventArgs e)
    {
        QuestionWindow.Show(_questions[11], $"{lblColTitleThree.Content} - {btnCatThreeTwo.Content}");
    }

    public void btnCatThreeThree_Click(object sender, EventArgs e)
    {
        QuestionWindow.Show(_questions[12], $"{lblColTitleThree.Content} - {btnCatThreeThree.Content}");
    }

    public void btnCatThreeFour_Click(object sender, EventArgs e)
    {
        QuestionWindow.Show(_questions[13], $"{lblColTitleThree.Content} - {btnCatThreeFour.Content}");
    }

    public void btnCatThreeFive_Click(object sender, EventArgs e)
    {
        QuestionWindow.Show(_questions[14], $"{lblColTitleThree.Content} - {btnCatThreeFive.Content}");
    }

    #endregion


    #region Category Four

    public void btnCatFourOne_Click(object sender, EventArgs e)
    {
        QuestionWindow.Show(_questions[15], $"{lblColTitleFour.Content} - {btnCatFourOne.Content}");
    }

    public void btnCatFourTwo_Click(object sender, EventArgs e)
    {
        QuestionWindow.Show(_questions[16], $"{lblColTitleFour.Content} - {btnCatFourTwo.Content}");
    }

    public void btnCatFourThree_Click(object sender, EventArgs e)
    {
        QuestionWindow.Show(_questions[17], $"{lblColTitleFour.Content} - {btnCatFourThree.Content}");
    }

    public void btnCatFourFour_Click(object sender, EventArgs e)
    {
        QuestionWindow.Show(_questions[18], $"{lblColTitleFour.Content} - {btnCatFourFour.Content}");
    }

    public void btnCatFourFive_Click(object sender, EventArgs e)
    {
        QuestionWindow.Show(_questions[19], $"{lblColTitleFour.Content} - {btnCatFourFive.Content}");
    }

    #endregion


    #region Category Five

    public void btnCatFiveOne_Click(object sender, EventArgs e)
    {
        QuestionWindow.Show(_questions[20], $"{lblColTitleFive.Content} - {btnCatFiveOne.Content}");
    }

    public void btnCatFiveTwo_Click(object sender, EventArgs e)
    {
        QuestionWindow.Show(_questions[21], $"{lblColTitleFive.Content} - {btnCatFiveTwo.Content}");
    }

    public void btnCatFiveThree_Click(object sender, EventArgs e)
    {
        QuestionWindow.Show(_questions[22], $"{lblColTitleFive.Content} - {btnCatFiveThree.Content}");
    }

    public void btnCatFiveFour_Click(object sender, EventArgs e)
    {
        QuestionWindow.Show(_questions[23], $"{lblColTitleFive.Content} - {btnCatFiveFour.Content}");
    }

    public void btnCatFiveFive_Click(object sender, EventArgs e)
    {
        QuestionWindow.Show(_questions[24], $"{lblColTitleFive.Content} - {btnCatFiveFive.Content}");
    }

    #endregion


    #region Category Six

    public void btnCatSixOne_Click(object sender, EventArgs e)
    {
        QuestionWindow.Show(_questions[25], $"{lblColTitleSix.Content} - {btnCatSixOne.Content}");
    }

    public void btnCatSixTwo_Click(object sender, EventArgs e)
    {
        QuestionWindow.Show(_questions[26], $"{lblColTitleSix.Content} - {btnCatSixTwo.Content}");
    }

    public void btnCatSixThree_Click(object sender, EventArgs e)
    {
        QuestionWindow.Show(_questions[27], $"{lblColTitleSix.Content} - {btnCatSixThree.Content}");
    }

    public void btnCatSixFour_Click(object sender, EventArgs e)
    {
        QuestionWindow.Show(_questions[28], $"{lblColTitleSix.Content} - {btnCatSixFour.Content}");
    }

    public void btnCatSixFive_Click(object sender, EventArgs e)
    {
        QuestionWindow.Show(_questions[29], $"{lblColTitleSix.Content} - {btnCatSixFive.Content}");
    }

    #endregion


    private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
    {
        var pmw = new PunktestandJeopardy.MainWindow(ReadUsernames())
        {
            Owner = this,
            Title = "",
            WindowStartupLocation = WindowStartupLocation.CenterOwner
        };
        pmw.Closed += (_, _) => Application.Current.Shutdown(-1);
        pmw.Show();
    }


    private IEnumerable<string> ReadUsernames()
    {
        if (!File.Exists(_USERNAMES_PATH))
        {
            yield return "";
        }

        using var sr = new StreamReader(_USERNAMES_PATH);
        while (!sr.EndOfStream)
        {
            yield return sr.ReadLine();
        }

        sr.Close();
    }


    private IEnumerable<string> ReadCategories()
    {
        if (!File.Exists(_CATEGORIES_PATH))
        {
            yield return "";
        }

        using var sr = new StreamReader(_CATEGORIES_PATH);
        while (!sr.EndOfStream)
        {
            yield return sr.ReadLine();
        }

        sr.Close();
    }


    private void SetCategories()
    {
        var categories = ReadCategories().ToArray();
        lblColTitleOne.Content = categories[0];
        lblColTitleTwo.Content = categories[1];
        lblColTitleThree.Content = categories[2];
        lblColTitleFour.Content = categories[3];
        lblColTitleFive.Content = categories[4];
        lblColTitleSix.Content = categories[5];
    }


    private IEnumerable<Question> ReadQuestions()
    {
        if (!File.Exists(_QUESTIONS_PATH))
        {
            return [];
        }

        using var sr = new StreamReader(_QUESTIONS_PATH);
        var ret = new List<Question>();
        while (!sr.EndOfStream)
        {
            var line = sr.ReadLine()?.Split('|');
            if (line is not
            {
                Length: 2
            })
            {
                continue;
            }

            ret.Add(new Question(line[0], line[1],
                !File.Exists(line[0]) ? QuestionType.Text : line[0].Split('.').Last() is "mp3" or "wav" ? QuestionType.Audio : QuestionType.Image));
        }

        sr.Close();
        return ret;
    }
}