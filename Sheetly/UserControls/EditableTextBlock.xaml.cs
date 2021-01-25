using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Sheetly.UserControls
{
    /// <summary>
    /// Interaction logic for EditableTextBlock.xaml
    /// </summary>
    public partial class EditableTextBlock : UserControl
    {
        public EditableTextBlock()
        {
            InitializeComponent();
        }

        public object Value
        {
            get { return GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public new object FontSize
        {
            get { return GetValue(FontSizeProperty); }
            set { SetValue(FontSizeProperty, value); }
        }

        public object EditMode
        {
            get { return GetValue(EditModeProperty); }
            set {
                if(value.ToString() == "Text" || value.ToString() == "Dropdown")
                    SetValue(EditModeProperty, value);
                else
                    Console.WriteLine("{0} is not a valid EditMode.", EditModeProperty);
            }
        }

        public object DropdownItems
        {
            get { return GetValue(DropdownItemsProperty); }
            set {
                SetValue(DropdownItemsProperty, value.ToString().Split(' '));
            }
        }

        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(object), typeof(EditableTextBlock), new PropertyMetadata(""));
        public static new readonly DependencyProperty FontSizeProperty = DependencyProperty.Register("FontSize", typeof(object), typeof(EditableTextBlock), new PropertyMetadata(""));
        public static readonly DependencyProperty EditModeProperty = DependencyProperty.Register("EditMode", typeof(string), typeof(EditableTextBlock), new PropertyMetadata(""));
        public static readonly DependencyProperty DropdownItemsProperty = DependencyProperty.Register("DropdownItems", typeof(string), typeof(EditableTextBlock), new PropertyMetadata(""));

        private void edit_text(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            (FindName("label_text") as TextBlock).Visibility = Visibility.Hidden;
            switch (EditMode)
            {
                case "Text":
                    (FindName("editable_text") as TextBox).Visibility = Visibility.Visible;
                    break;
                case "Dropdown":
                    (FindName("dropdown") as ComboBox).Visibility = Visibility.Visible;
                    break;
            }
        }

        private void save_text(object sender, RoutedEventArgs e)
        {
            switch (EditMode)
            {
                case "Text":
                    (FindName("editable_text") as TextBox).Visibility = Visibility.Hidden;
                    break;
                case "Dropdown":
                    (FindName("dropdown") as ComboBox).Visibility = Visibility.Hidden;
                    break;
            }
            (FindName("label_text") as TextBlock).Visibility = Visibility.Visible;
        }

    }
}
