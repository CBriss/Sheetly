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

        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(object), typeof(EditableTextBlock), new PropertyMetadata(""));

        private void edit_text(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            (this.FindName("label_text") as TextBlock).Visibility = Visibility.Hidden;
            (this.FindName("editable_text") as TextBox).Visibility = Visibility.Visible;
        }

        private void save_text(object sender, RoutedEventArgs e)
        {
            (this.FindName("label_text") as TextBlock).Visibility = Visibility.Visible;
            (this.FindName("editable_text") as TextBox).Visibility = Visibility.Hidden;
        }

    }
}
