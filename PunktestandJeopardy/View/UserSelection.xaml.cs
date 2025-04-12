using System.Windows;
using System.Windows.Controls;

namespace PunktestandJeopardy.View;

public partial class UserSelection : Window
{
    private readonly List<string> _selectedUsers = [];

    private UserSelection (List<string> usernames)
    {
        InitializeComponent();
        InitializeUsers(usernames);
    }

    private void InitializeUsers (List<string> usernames)
    {
        Users.ItemsSource = usernames;
    }

    private void Confirm_Click (object sender, RoutedEventArgs e)
    {
        Close();
    }

    private void SelectedUsers_Changed (object sender, SelectionChangedEventArgs e)
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


    public static List<string> Show (List<string> usernames, Window owner)
    {
        var window = new UserSelection(usernames)
        {
            Owner = owner
        };
        window.ShowDialog();
        return window._selectedUsers;
    }
}