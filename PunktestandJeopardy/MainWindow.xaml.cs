using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PunktestandJeopardy;

public partial class MainWindow : Window
{
    private List<string> _usernames;
    
    public MainWindow(IEnumerable<string> usernames)
    {
        InitializeComponent();
        _usernames = usernames.ToList();
    }


    private void InitializeUsers(IEnumerable<string> usernames)
    {
        var users = UserSelection.Show(usernames.ToList());

        if (users.Count < 2)
        {
            MessageBox.Show("Please select at least 2 users");
            Close();
            return;
        }

        if (users.Count > 2)
        {
            ThreeUsers.Visibility = Visibility.Visible;
            TwoUsers.Visibility = Visibility.Collapsed;

            PlayerOneThreeUsers.Content = users[0];
            PlayerTwoThreeUsers.Content = users[1];
            PlayerThreeThreeUsers.Content = users[2];
            return;
        }

        ThreeUsers.Visibility = Visibility.Collapsed;
        TwoUsers.Visibility = Visibility.Visible;

        PlayerOneTwoUsers.Content = users[0];
        PlayerTwoTwoUsers.Content = users[1];
    }


    private void Label_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if (sender is not Label
        {
            Content: string s
        } l)
        {
            return;
        }

        if (!int.TryParse(s, out var i))
        {
            return;
        }

        i += 200;
        l.Content = i.ToString();
    }


    private void Label_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
    {
        if (sender is not Label
        {
            Content: string s
        } l)
        {
            return;
        }

        if (!int.TryParse(s, out var i))
        {
            return;
        }

        i -= 200;
        l.Content = i.ToString();
    }

    private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
    {
        InitializeUsers(_usernames);
    }
}