using System.IO;
using System.Windows;
using BoardJeopardy.Model;
using BoardJeopardy.View;

namespace BoardJeopardy;

/// <summary>
///     Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
#region constructor

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

#endregion


#region loaded

    private void MainWindow_OnLoaded (object sender, RoutedEventArgs e)
    {
        var pmw = new PunktestandJeopardy.View.MainWindow(ReadUsernames())
        {
            Owner = this,
            Title = "",
            WindowStartupLocation = WindowStartupLocation.CenterOwner
        };
        pmw.Closed += (_, _) => Application.Current.Shutdown(-1);
        pmw.Show();
    }

#endregion


#region fields

    private static readonly string _FOLDER_PATH = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "CoolJeopardy");
    private static readonly string _USERNAMES_PATH = Path.Combine(_FOLDER_PATH, "users.txt");
    private static readonly string _CATEGORIES_PATH = Path.Combine(_FOLDER_PATH, "categories.txt");
    private static readonly string _QUESTIONS_PATH = Path.Combine(_FOLDER_PATH, "questions.txt");

    private readonly List<Question> _questions;

#endregion


#region read and set stuff

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
        ColTitleOne.Content = categories[0];
        ColTitleTwo.Content = categories[1];
        ColTitleThree.Content = categories[2];
        ColTitleFour.Content = categories[3];
        ColTitleFive.Content = categories[4];
        ColTitleSix.Content = categories[5];
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
                                 !File.Exists(line[0]) ? QuestionType.Text :
                                 line[0].Split('.').Last() is "mp3" or "wav" ? QuestionType.Audio : QuestionType.Image));
        }

        sr.Close();
        return ret;
    }

#endregion


#region Category One

    private void CatOneOne_Click (object sender, EventArgs e)
    {
        QuestionWindow.Show(_questions[0], $"{ColTitleOne.Content} - {CatOneOne.Content}", this);
        CatOneOne.IsEnabled = false;
    }

    private void CatOneTwo_Click (object sender, EventArgs e)
    {
        QuestionWindow.Show(_questions[1], $"{ColTitleOne.Content} - {CatOneTwo.Content}", this);
        CatOneTwo.IsEnabled = false;
    }

    private void CatOneThree_Click (object sender, EventArgs e)
    {
        QuestionWindow.Show(_questions[2], $"{ColTitleOne.Content} - {CatOneThree.Content}", this);
        CatOneThree.IsEnabled = false;
    }

    private void CatOneFour_Click (object sender, EventArgs e)
    {
        QuestionWindow.Show(_questions[3], $"{ColTitleOne.Content} - {CatOneFour.Content}", this);
        CatOneFour.IsEnabled = false;
    }

    private void CatOneFive_Click (object sender, EventArgs e)
    {
        QuestionWindow.Show(_questions[4], $"{ColTitleOne.Content} - {CatOneFive.Content}", this);
        CatOneFive.IsEnabled = false;
    }

#endregion


#region Category Two

    private void CatTwoOne_Click (object sender, EventArgs e)
    {
        QuestionWindow.Show(_questions[5], $"{ColTitleTwo.Content} - {CatTwoOne.Content}", this);
        CatTwoOne.IsEnabled = false;
    }

    private void CatTwoTwo_Click (object sender, EventArgs e)
    {
        QuestionWindow.Show(_questions[6], $"{ColTitleTwo.Content} - {CatTwoTwo.Content}", this);
        CatTwoTwo.IsEnabled = false;
    }

    private void CatTwoThree_Click (object sender, EventArgs e)
    {
        QuestionWindow.Show(_questions[7], $"{ColTitleTwo.Content} - {CatTwoThree.Content}", this);
        CatTwoThree.IsEnabled = false;
    }

    private void CatTwoFour_Click (object sender, EventArgs e)
    {
        QuestionWindow.Show(_questions[8], $"{ColTitleTwo.Content} - {CatTwoFour.Content}", this);
        CatTwoFour.IsEnabled = false;
    }

    private void CatTwoFiveClick (object sender, EventArgs e)
    {
        QuestionWindow.Show(_questions[9], $"{ColTitleTwo.Content} - {CatTwoFive.Content}", this);
        CatTwoFive.IsEnabled = false;
    }

#endregion


#region Category Three

    private void CatThreeOne_Click (object sender, EventArgs e)
    {
        QuestionWindow.Show(_questions[10], $"{ColTitleThree.Content} - {CatThreeOne.Content}", this);
        CatThreeOne.IsEnabled = false;
    }

    private void CatThreeTwo_Click (object sender, EventArgs e)
    {
        QuestionWindow.Show(_questions[11], $"{ColTitleThree.Content} - {CatThreeTwo.Content}", this);
        CatThreeTwo.IsEnabled = false;
    }

    private void CatThreeThree_Click (object sender, EventArgs e)
    {
        QuestionWindow.Show(_questions[12], $"{ColTitleThree.Content} - {CatThreeThree.Content}", this);
        CatThreeThree.IsEnabled = false;
    }

    private void CatThreeFour_Click (object sender, EventArgs e)
    {
        QuestionWindow.Show(_questions[13], $"{ColTitleThree.Content} - {CatThreeFour.Content}", this);
        CatThreeFour.IsEnabled = false;
    }

    private void CatThreeFive_Click (object sender, EventArgs e)
    {
        QuestionWindow.Show(_questions[14], $"{ColTitleThree.Content} - {CatThreeFive.Content}", this);
        CatThreeFive.IsEnabled = false;
    }

#endregion


#region Category Four

    private void CatFourOne_Click (object sender, EventArgs e)
    {
        QuestionWindow.Show(_questions[15], $"{ColTitleFour.Content} - {CatFourOne.Content}", this);
        CatFourOne.IsEnabled = false;
    }

    private void CatFourTwo_Click (object sender, EventArgs e)
    {
        QuestionWindow.Show(_questions[16], $"{ColTitleFour.Content} - {CatFourTwo.Content}", this);
        CatFourTwo.IsEnabled = false;
    }

    private void CatFourThree_Click (object sender, EventArgs e)
    {
        QuestionWindow.Show(_questions[17], $"{ColTitleFour.Content} - {CatFourThree.Content}", this);
        CatFourThree.IsEnabled = false;
    }

    private void CatFourFour_Click (object sender, EventArgs e)
    {
        QuestionWindow.Show(_questions[18], $"{ColTitleFour.Content} - {CatFourFour.Content}", this);
        CatFourFour.IsEnabled = false;
    }

    private void CatFourFive_Click (object sender, EventArgs e)
    {
        QuestionWindow.Show(_questions[19], $"{ColTitleFour.Content} - {CatFourFive.Content}", this);
        CatFourFive.IsEnabled = false;
    }

#endregion


#region Category Five

    private void CatFiveOne_Click (object sender, EventArgs e)
    {
        QuestionWindow.Show(_questions[20], $"{ColTitleFive.Content} - {CatFiveOne.Content}", this);
        CatFiveOne.IsEnabled = false;
    }

    private void CatFiveTwo_Click (object sender, EventArgs e)
    {
        QuestionWindow.Show(_questions[21], $"{ColTitleFive.Content} - {CatFiveTwo.Content}", this);
        CatFiveTwo.IsEnabled = false;
    }

    private void CatFiveThree_Click (object sender, EventArgs e)
    {
        QuestionWindow.Show(_questions[22], $"{ColTitleFive.Content} - {CatFiveThree.Content}", this);
        CatFiveThree.IsEnabled = false;
    }

    private void CatFiveFour_Click (object sender, EventArgs e)
    {
        QuestionWindow.Show(_questions[23], $"{ColTitleFive.Content} - {CatFiveFour.Content}", this);
        CatFiveFour.IsEnabled = false;
    }

    private void CatFiveFive_Click (object sender, EventArgs e)
    {
        QuestionWindow.Show(_questions[24], $"{ColTitleFive.Content} - {CatFiveFive.Content}", this);
        CatFiveFive.IsEnabled = false;
    }

#endregion


#region Category Six

    private void CatSixOne_Click (object sender, EventArgs e)
    {
        QuestionWindow.Show(_questions[25], $"{ColTitleSix.Content} - {CatSixOne.Content}", this);
        CatSixOne.IsEnabled = false;
    }

    private void CatSixTwo_Click (object sender, EventArgs e)
    {
        QuestionWindow.Show(_questions[26], $"{ColTitleSix.Content} - {CatSixTwo.Content}", this);
        CatSixTwo.IsEnabled = false;
    }

    private void CatSixThree_Click (object sender, EventArgs e)
    {
        QuestionWindow.Show(_questions[27], $"{ColTitleSix.Content} - {CatSixThree.Content}", this);
        CatSixThree.IsEnabled = false;
    }

    private void CatSixFour_Click (object sender, EventArgs e)
    {
        QuestionWindow.Show(_questions[28], $"{ColTitleSix.Content} - {CatSixFour.Content}", this);
        CatSixFour.IsEnabled = false;
    }

    private void CatSixFive_Click (object sender, EventArgs e)
    {
        QuestionWindow.Show(_questions[29], $"{ColTitleSix.Content} - {CatSixFive.Content}", this);
        CatSixFive.IsEnabled = false;
    }

#endregion
}