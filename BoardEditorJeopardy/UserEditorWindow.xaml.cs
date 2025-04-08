using System.IO;
using System.Windows;

namespace BoardEditorJeopardy;

public partial class UserEditorWindow : Window
{
    private List<string> _users;
    
    
    public UserEditorWindow (List<string> users)
    {
        InitializeComponent();

        _users = users;
        Users.ItemsSource = _users;
    }
    
    
    private void SaveButton_Click (object sender, RoutedEventArgs e)
    {
        Close();
    }
    
    
    public static List<string> ShowDialog (List<string> users)
    {
        var dialog = new UserEditorWindow(users);
        dialog.ShowDialog();
        return dialog._users;
    }
}