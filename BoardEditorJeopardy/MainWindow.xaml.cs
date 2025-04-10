using System.IO;
using System.Windows;
using BoardJeopardy;

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
        _users = UserEditorWindow.ShowDialog(_users);
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

    private void LblColTitleOne_OnClick (object sender, RoutedEventArgs e)
    {
        _categories[0] = CategoryEditorWindow.Show(_categories[0]);
        SetCategories();
    }

    public void btnCatOneOne_Click (object sender, EventArgs e)
    {
        _questions[0] = QuestionEditorWindow.Show(_questions[0], 0);
    }

    public void btnCatOneTwo_Click (object sender, EventArgs e)
    {
        _questions[1] = QuestionEditorWindow.Show(_questions[1], 1);
    }

    public void btnCatOneThree_Click (object sender, EventArgs e)
    {
        _questions[2] = QuestionEditorWindow.Show(_questions[2], 2);
    }

    public void btnCatOneFour_Click (object sender, EventArgs e)
    {
        _questions[3] = QuestionEditorWindow.Show(_questions[3], 3);
    }

    public void btnCatOneFive_Click (object sender, EventArgs e)
    {
        _questions[4] = QuestionEditorWindow.Show(_questions[4], 4);
    }

#endregion


#region Category Two

    private void LblColTitleTwo_OnClick (object sender, RoutedEventArgs e)
    {
        _categories[1] = CategoryEditorWindow.Show(_categories[1]);
        SetCategories();
    }

    public void btnCatTwoOne_Click (object sender, EventArgs e)
    {
        _questions[5] = QuestionEditorWindow.Show(_questions[5], 5);
    }

    public void btnCatTwoTwo_Click (object sender, EventArgs e)
    {
        _questions[6] = QuestionEditorWindow.Show(_questions[6], 6);
    }

    public void btnCatTwoThree_Click (object sender, EventArgs e)
    {
        _questions[7] = QuestionEditorWindow.Show(_questions[7], 7);
    }

    public void btnCatTwoFour_Click (object sender, EventArgs e)
    {
        _questions[8] = QuestionEditorWindow.Show(_questions[8], 8);
    }

    public void btnCatTwoFive_Click (object sender, EventArgs e)
    {
        _questions[9] = QuestionEditorWindow.Show(_questions[9], 9);
    }

#endregion


#region Category Three

    private void LblColTitleThree_OnClick (object sender, RoutedEventArgs e)
    {
        _categories[2] = CategoryEditorWindow.Show(_categories[2]);
        SetCategories();
    }

    public void btnCatThreeOne_Click (object sender, EventArgs e)
    {
        _questions[10] = QuestionEditorWindow.Show(_questions[10], 10);
    }

    public void btnCatThreeTwo_Click (object sender, EventArgs e)
    {
        _questions[11] = QuestionEditorWindow.Show(_questions[11], 11);
    }

    public void btnCatThreeThree_Click (object sender, EventArgs e)
    {
        _questions[12] = QuestionEditorWindow.Show(_questions[12], 12);
    }

    public void btnCatThreeFour_Click (object sender, EventArgs e)
    {
        _questions[13] = QuestionEditorWindow.Show(_questions[13], 13);
    }

    public void btnCatThreeFive_Click (object sender, EventArgs e)
    {
        _questions[14] = QuestionEditorWindow.Show(_questions[14], 14);
    }

#endregion


#region Category Four

    private void LblColTitleFour_OnClick (object sender, RoutedEventArgs e)
    {
        _categories[3] = CategoryEditorWindow.Show(_categories[3]);
        SetCategories();
    }

    public void btnCatFourOne_Click (object sender, EventArgs e)
    {
        _questions[15] = QuestionEditorWindow.Show(_questions[15], 15);
    }

    public void btnCatFourTwo_Click (object sender, EventArgs e)
    {
        _questions[16] = QuestionEditorWindow.Show(_questions[16], 16);
    }

    public void btnCatFourThree_Click (object sender, EventArgs e)
    {
        _questions[17] = QuestionEditorWindow.Show(_questions[17], 17);
    }

    public void btnCatFourFour_Click (object sender, EventArgs e)
    {
        _questions[18] = QuestionEditorWindow.Show(_questions[18], 18);
    }

    public void btnCatFourFive_Click (object sender, EventArgs e)
    {
        _questions[19] = QuestionEditorWindow.Show(_questions[19], 19);
    }

#endregion


#region Category Five

    private void LblColTitleFive_OnClick (object sender, RoutedEventArgs e)
    {
        _categories[4] = CategoryEditorWindow.Show(_categories[4]);
        SetCategories();
    }

    public void btnCatFiveOne_Click (object sender, EventArgs e)
    {
        _questions[20] = QuestionEditorWindow.Show(_questions[20], 20);
    }

    public void btnCatFiveTwo_Click (object sender, EventArgs e)
    {
        _questions[21] = QuestionEditorWindow.Show(_questions[21], 21);
    }

    public void btnCatFiveThree_Click (object sender, EventArgs e)
    {
        _questions[22] = QuestionEditorWindow.Show(_questions[22], 22);
    }

    public void btnCatFiveFour_Click (object sender, EventArgs e)
    {
        _questions[23] = QuestionEditorWindow.Show(_questions[23], 23);
    }

    public void btnCatFiveFive_Click (object sender, EventArgs e)
    {
        _questions[24] = QuestionEditorWindow.Show(_questions[24], 24);
    }

#endregion


#region Category Six

    private void LblColTitleSix_OnClick (object sender, RoutedEventArgs e)
    {
        _categories[5] = CategoryEditorWindow.Show(_categories[5]);
        SetCategories();
    }

    public void btnCatSixOne_Click (object sender, EventArgs e)
    {
        _questions[25] = QuestionEditorWindow.Show(_questions[25], 25);
    }

    public void btnCatSixTwo_Click (object sender, EventArgs e)
    {
        _questions[26] = QuestionEditorWindow.Show(_questions[26], 26);
    }

    public void btnCatSixThree_Click (object sender, EventArgs e)
    {
        _questions[27] = QuestionEditorWindow.Show(_questions[27], 27);
    }

    public void btnCatSixFour_Click (object sender, EventArgs e)
    {
        _questions[28] = QuestionEditorWindow.Show(_questions[28], 28);
    }

    public void btnCatSixFive_Click (object sender, EventArgs e)
    {
        _questions[29] = QuestionEditorWindow.Show(_questions[29], 29);
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
        lblColTitleOne.Content = _categories[0];
        lblColTitleTwo.Content = _categories[1];
        lblColTitleThree.Content = _categories[2];
        lblColTitleFour.Content = _categories[3];
        lblColTitleFive.Content = _categories[4];
        lblColTitleSix.Content = _categories[5];
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