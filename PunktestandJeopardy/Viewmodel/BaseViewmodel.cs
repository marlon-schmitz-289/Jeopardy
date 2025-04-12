using System.ComponentModel;

namespace PunktestandJeopardy.Viewmodel;

public class BaseViewmodel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged (string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}