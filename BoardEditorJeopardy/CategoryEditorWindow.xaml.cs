﻿using System.Windows;

namespace BoardEditorJeopardy;

public partial class CategoryEditorWindow : Window
{
    private string _category;

    public CategoryEditorWindow (string category)
    {
        InitializeComponent();

        _category = category;
        Category.Text = _category;
    }

    private void Save_Click (object sender, RoutedEventArgs e)
    {
        _category = Category.Text;
        DialogResult = true;
    }

    public static string Show (string category, Window owner)
    {
        var window = new CategoryEditorWindow(category)
        {
            Owner = owner
        };
        window.ShowDialog();
        return window._category;
    }
}