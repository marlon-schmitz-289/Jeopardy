using System.IO;
using System.Windows;
using BoardJeopardy.Model;

namespace BoardEditorJeopardy;

/// <summary>
///     Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
#region Constructor

    public MainWindow()
    {
        InitializeComponent();

        _questions =
        [
            new Question("", ""), new Question("", ""), new Question("", ""), new Question("", ""), new Question("", ""),
            // Category Two
            new Question("", ""), new Question("", ""), new Question("", ""), new Question("", ""), new Question("", ""),
            // Category Three
            new Question("", ""), new Question("", ""), new Question("", ""), new Question("", ""), new Question("", ""),
            // Category Four
            new Question("", ""), new Question("", ""), new Question("", ""), new Question("", ""), new Question("", ""),
            // Category Five
            new Question("", ""), new Question("", ""), new Question("", ""), new Question("", ""), new Question("", ""),
            // Category Six
            new Question("", ""), new Question("", ""), new Question("", ""), new Question("", ""), new Question("", "")
        ];
        _categories = ["Category One", "Category Two", "Category Three", "Category Four", "Category Five", "Category Six"];
        _users = ["Player One", "Player Two", "Player Three"];

        ReadUsernames();
        ReadCategories();
        SetCategories();
        ReadQuestions();
    }

#endregion


#region Edit Users

    private void EditUsers_OnClick (object sender, RoutedEventArgs e)
    {
        _users = UserEditorWindow.ShowDialog(_users, this);
    }

#endregion


#region Fields

    private static readonly string _FOLDER_PATH = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "CoolJeopardy");
    private static readonly string _USERNAMES_PATH = Path.Combine(_FOLDER_PATH, "users.txt");
    private static readonly string _CATEGORIES_PATH = Path.Combine(_FOLDER_PATH, "categories.txt");
    private static readonly string _QUESTIONS_PATH = Path.Combine(_FOLDER_PATH, "questions.txt");

    private readonly List<Question> _questions;
    private readonly List<string> _categories;

    private List<string> _users;

#endregion


#region Category One

    private void ColTitleOne_OnClick (object sender, RoutedEventArgs e)
    {
        _categories[0] = CategoryEditorWindow.Show(_categories[0], this);
        SetCategories();
    }

    private void CatOneOne_Click (object sender, EventArgs e)
    {
        _questions[0] = QuestionEditorWindow.Show(_questions[0], 0, this);
    }

    private void btnCatOneTwo_Click (object sender, EventArgs e)
    {
        _questions[1] = QuestionEditorWindow.Show(_questions[1], 1, this);
    }

    private void CatOneThree_Click (object sender, EventArgs e)
    {
        _questions[2] = QuestionEditorWindow.Show(_questions[2], 2, this);
    }

    private void CatOneFour_Click (object sender, EventArgs e)
    {
        _questions[3] = QuestionEditorWindow.Show(_questions[3], 3, this);
    }

    private void CatOneFive_Click (object sender, EventArgs e)
    {
        _questions[4] = QuestionEditorWindow.Show(_questions[4], 4, this);
    }

#endregion


#region Category Two

    private void ColTitleTwo_OnClick (object sender, RoutedEventArgs e)
    {
        _categories[1] = CategoryEditorWindow.Show(_categories[1], this);
        SetCategories();
    }

    private void CatTwoOne_Click (object sender, EventArgs e)
    {
        _questions[5] = QuestionEditorWindow.Show(_questions[5], 5, this);
    }

    private void CatTwoTwo_Click (object sender, EventArgs e)
    {
        _questions[6] = QuestionEditorWindow.Show(_questions[6], 6, this);
    }

    private void CatTwoThree_Click (object sender, EventArgs e)
    {
        _questions[7] = QuestionEditorWindow.Show(_questions[7], 7, this);
    }

    private void CatTwoFour_Click (object sender, EventArgs e)
    {
        _questions[8] = QuestionEditorWindow.Show(_questions[8], 8, this);
    }

    private void CatTwoFive_Click (object sender, EventArgs e)
    {
        _questions[9] = QuestionEditorWindow.Show(_questions[9], 9, this);
    }

#endregion


#region Category Three

    private void ColTitleThree_OnClick (object sender, RoutedEventArgs e)
    {
        _categories[2] = CategoryEditorWindow.Show(_categories[2], this);
        SetCategories();
    }

    private void CatThreeOne_Click (object sender, EventArgs e)
    {
        _questions[10] = QuestionEditorWindow.Show(_questions[10], 10, this);
    }

    private void CatThreeTwo_Click (object sender, EventArgs e)
    {
        _questions[11] = QuestionEditorWindow.Show(_questions[11], 11, this);
    }

    private void CatThreeThree_Click (object sender, EventArgs e)
    {
        _questions[12] = QuestionEditorWindow.Show(_questions[12], 12, this);
    }

    private void CatThreeFour_Click (object sender, EventArgs e)
    {
        _questions[13] = QuestionEditorWindow.Show(_questions[13], 13, this);
    }

    private void CatThreeFive_Click (object sender, EventArgs e)
    {
        _questions[14] = QuestionEditorWindow.Show(_questions[14], 14, this);
    }

#endregion


#region Category Four

    private void ColTitleFour_OnClick (object sender, RoutedEventArgs e)
    {
        _categories[3] = CategoryEditorWindow.Show(_categories[3], this);
        SetCategories();
    }

    private void CatFourOne_Click (object sender, EventArgs e)
    {
        _questions[15] = QuestionEditorWindow.Show(_questions[15], 15, this);
    }

    private void CatFourTwo_Click (object sender, EventArgs e)
    {
        _questions[16] = QuestionEditorWindow.Show(_questions[16], 16, this);
    }

    private void CatFourThree_Click (object sender, EventArgs e)
    {
        _questions[17] = QuestionEditorWindow.Show(_questions[17], 17, this);
    }

    private void CatFourFour_Click (object sender, EventArgs e)
    {
        _questions[18] = QuestionEditorWindow.Show(_questions[18], 18, this);
    }

    private void CatFourFive_Click (object sender, EventArgs e)
    {
        _questions[19] = QuestionEditorWindow.Show(_questions[19], 19, this);
    }

#endregion


#region Category Five

    private void ColTitleFive_OnClick (object sender, RoutedEventArgs e)
    {
        _categories[4] = CategoryEditorWindow.Show(_categories[4], this);
        SetCategories();
    }

    private void CatFiveOne_Click (object sender, EventArgs e)
    {
        _questions[20] = QuestionEditorWindow.Show(_questions[20], 20, this);
    }

    private void CatFiveTwo_Click (object sender, EventArgs e)
    {
        _questions[21] = QuestionEditorWindow.Show(_questions[21], 21, this);
    }

    private void CatFiveThree_Click (object sender, EventArgs e)
    {
        _questions[22] = QuestionEditorWindow.Show(_questions[22], 22, this);
    }

    private void CatFiveFour_Click (object sender, EventArgs e)
    {
        _questions[23] = QuestionEditorWindow.Show(_questions[23], 23, this);
    }

    private void CatFiveFive_Click (object sender, EventArgs e)
    {
        _questions[24] = QuestionEditorWindow.Show(_questions[24], 24, this);
    }

#endregion


#region Category Six

    private void ColTitleSix_OnClick (object sender, RoutedEventArgs e)
    {
        _categories[5] = CategoryEditorWindow.Show(_categories[5], this);
        SetCategories();
    }

    private void CatSixOne_Click (object sender, EventArgs e)
    {
        _questions[25] = QuestionEditorWindow.Show(_questions[25], 25, this);
    }

    private void CatSixTwo_Click (object sender, EventArgs e)
    {
        _questions[26] = QuestionEditorWindow.Show(_questions[26], 26, this);
    }

    private void CatSixThree_Click (object sender, EventArgs e)
    {
        _questions[27] = QuestionEditorWindow.Show(_questions[27], 27, this);
    }

    private void CatSixFour_Click (object sender, EventArgs e)
    {
        _questions[28] = QuestionEditorWindow.Show(_questions[28], 28, this);
    }

    private void CatSixFive_Click (object sender, EventArgs e)
    {
        _questions[29] = QuestionEditorWindow.Show(_questions[29], 29, this);
    }

#endregion


#region Read and Set stuff

    private void ReadUsernames()
    {
        if (!File.Exists(_USERNAMES_PATH))
        {
            return;
        }

        _users.Clear();
        using var sr = new StreamReader(_USERNAMES_PATH);
        while (!sr.EndOfStream)
        {
            _users.Add(sr.ReadLine());
        }

        sr.Close();
    }


    private void ReadCategories()
    {
        if (!File.Exists(_CATEGORIES_PATH))
        {
            return;
        }

        _categories.Clear();
        using var sr = new StreamReader(_CATEGORIES_PATH);
        while (!sr.EndOfStream)
        {
            _categories.Add(sr.ReadLine());
        }

        sr.Close();
    }


    private void SetCategories()
    {
        ColTitleOne.Content = _categories[0];
        ColTitleTwo.Content = _categories[1];
        ColTitleThree.Content = _categories[2];
        ColTitleFour.Content = _categories[3];
        ColTitleFive.Content = _categories[4];
        ColTitleSix.Content = _categories[5];
    }


    private void ReadQuestions()
    {
        if (!File.Exists(_QUESTIONS_PATH))
        {
            return;
        }

        _questions.Clear();
        using var sr = new StreamReader(_QUESTIONS_PATH);
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

            _questions.Add(new Question(line[0], line[1],
                                        File.Exists(line[0])
                                            ? line[0].Split('.').Last() is "mp3" or "wav" ? QuestionType.Audio : QuestionType.Image
                                            : QuestionType.Text));
        }

        sr.Close();
    }

#endregion


#region Save stuff

    private void Save_OnClick (object sender, RoutedEventArgs e)
    {
        if (!Directory.Exists(_FOLDER_PATH))
        {
            Directory.CreateDirectory(_FOLDER_PATH);
        }

        SaveUsers();
        SaveCategories();
        SaveQuestions();

        Close();
    }


    private void SaveUsers()
    {
        if (!File.Exists(_USERNAMES_PATH))
        {
            File.Create(_USERNAMES_PATH).Close();
        }

        using var sw = new StreamWriter(_USERNAMES_PATH, false);
        foreach (var user in _users)
        {
            sw.WriteLine(user);
        }

        sw.Close();
    }


    private void SaveCategories()
    {
        if (!File.Exists(_CATEGORIES_PATH))
        {
            File.Create(_CATEGORIES_PATH).Close();
        }

        using var sw = new StreamWriter(_CATEGORIES_PATH, false);
        foreach (var category in _categories)
        {
            sw.WriteLine(category);
        }

        sw.Close();
    }


    private void SaveQuestions()
    {
        if (!File.Exists(_QUESTIONS_PATH))
        {
            File.Create(_QUESTIONS_PATH).Close();
        }

        using var sw = new StreamWriter(_QUESTIONS_PATH, false);
        foreach (var question in _questions)
        {
            sw.WriteLine($"{question.Questi}|{question.Answer}");
        }

        sw.Close();
    }

#endregion
}