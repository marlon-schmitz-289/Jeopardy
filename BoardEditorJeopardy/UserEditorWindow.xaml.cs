using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace BoardEditorJeopardy;

public partial class UserEditorWindow : Window
{
    private List<TextBox> _usersTextBoxes = [];
    private List<string> _users => _usersTextBoxes.Select(x => x.Text).ToList();
    
    
    public UserEditorWindow (List<string> users)
    {
        InitializeComponent();

        _usersTextBoxes.AddRange(users.Select(x => new TextBox
        {
            Text = x,
            MinWidth = 150
        }));
        _usersTextBoxes.ForEach(x => Users.Items.Add(x));
    }
    
    
    private void AddUser_Click (object sender, RoutedEventArgs e)
    {
        var newUser = new TextBox
        {
            Text = "New User",
            MinWidth = 150
        };
        _usersTextBoxes.Add(newUser);
        Users.Items.Add(newUser);
    }
    
    private void RemoveUser_Click (object sender, RoutedEventArgs e)
    {
        if (Users.SelectedItem is not TextBox selectedUser)
        {
            return;
        }
        
        _usersTextBoxes.Remove(selectedUser);
        Users.Items.Remove(selectedUser);
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