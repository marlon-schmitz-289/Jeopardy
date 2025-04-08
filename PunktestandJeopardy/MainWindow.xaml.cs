using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PunktestandJeopardy;

public partial class MainWindow : Window
{
    public MainWindow (IEnumerable<string> usernames)
    {
        InitializeComponent();
        InitializeUsers(usernames);
    }

    
    private void InitializeUsers (IEnumerable<string> usernames)
    {
        var users = usernames.ToArray();
        PlayerOne.Content = users[0];
        PlayerTwo.Content = users[1];
        PlayerThree.Content = users[2];
    }

    
    private void Label_MouseLeftButtonDown (object sender, MouseButtonEventArgs e)
    {
        if (sender is not Label { Content: string s } l)
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

    
    private void Label_MouseRightButtonDown (object sender, MouseButtonEventArgs e)
    {
        if (sender is not Label { Content: string s } l)
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