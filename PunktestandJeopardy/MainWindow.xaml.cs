using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PunktestandJeopardy;

public partial class MainWindow : Window
{
    public MainWindow(IEnumerable<string> usernames)
    {
        InitializeComponent();
        InitializeUsers(usernames);
    }


    private void InitializeUsers(IEnumerable<string> usernames)
    {
        // TODO: Open new window to select users to use
        var users = UserSelection.Show(usernames.ToList());

        if (users.Count < 2)
        {
            MessageBox.Show("Please select at least 2 users");
            Application.Current.Shutdown();
        }

        if (users.Count > 2)
        {
            PlayerOneThreeUsers.Content = users[0];
            PlayerTwoThreeUsers.Content = users[1];
            PlayerThreeThreeUsers.Content = users[2];
            return;
        }

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
}