using System.IO;
using System.Windows;

namespace BoardEditorJeopardy;

public partial class UserEditorWindow : Window
{
    private string _userOne;
    private string _userTwo;
    private string _userThree;
    
    
    public UserEditorWindow (string userOne, string userTwo, string userThree)
    {
        InitializeComponent();
        
        _userOne = userOne;
        _userTwo = userTwo;
        _userThree = userThree;
        
        UserOneTextBox.Text = _userOne;
        UserTwoTextBox.Text = _userTwo;
        UserThreeTextBox.Text = _userThree;
    }
    
    
    private void SaveButton_Click (object sender, RoutedEventArgs e)
    {
        _userOne = UserOneTextBox.Text;
        _userTwo = UserTwoTextBox.Text;
        _userThree = UserThreeTextBox.Text;
        
        Close();
    }
    
    
    public static (string, string, string) ShowDialog (string userOne, string userTwo, string userThree)
    {
        var dialog = new UserEditorWindow(userOne, userTwo, userThree);
        dialog.ShowDialog();
        return (dialog._userOne, dialog._userTwo, dialog._userThree);
    }
}