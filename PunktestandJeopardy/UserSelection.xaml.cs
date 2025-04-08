using System.Windows;
using System.Windows.Controls;

namespace PunktestandJeopardy;

public partial class UserSelection : Window
{
    private List<string> _selectedUsers = [];
    
    private UserSelection(List<string> usernames)
    {
        InitializeComponent();
        InitializeUsers(usernames);
    }

    private void InitializeUsers(List<string> usernames)
    {
        Users.ItemsSource = usernames;
    }

    private void Confirm_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }

    private void SelectedUsers_Changed(object sender, SelectionChangedEventArgs e)
    {
        if (e.AddedItems.Count > 0)
        {
            _selectedUsers.AddRange(e.AddedItems.OfType<string>().ToList());
        }

        if (e.RemovedItems.Count > 0)
        {
            _selectedUsers.RemoveAll(e.RemovedItems.OfType<string>().ToList().Contains);
        }
    }
    
    
    public static List<string> Show(List<string> usernames)
    {
        var window = new UserSelection(usernames);
        window.ShowDialog();
        return window._selectedUsers;
    }
}