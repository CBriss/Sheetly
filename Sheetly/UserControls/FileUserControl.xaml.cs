using Sheetly.Animation;
using System;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace Sheetly.UserControls
{
    /// <summary>
    /// Interaction logic for FileUserControl.xaml
    /// </summary>
    public partial class FileUserControl : UserControl
    {
        #region Base Methods

        public FileUserControl()
        {
            InitializeComponent();
            ShowField("delimiterField");
            ShowField("columnField");
        }

        #endregion

        #region Anmiations
        
        /// <summary>
        /// Displays the specified field with a slide animation
        /// </summary>
        /// <param name="fieldName">x:name of the field to show</param>
        private async void ShowField(string fieldName)
        {
            await AnimateIn(fieldName, 1);
        }

        /// <summary>
        /// Animates a field into view from the left
        /// </summary>
        /// <param name="fieldName">x:name of the field</param>
        /// <param name="animationTime">Time, in seconds, of the animation</param>
        /// <returns></returns>
        public async Task AnimateIn(string fieldName, float animationTime)
        {
            EditableTextBlock animatedNode = (EditableTextBlock)FindName(fieldName);
            var storyboard = new Storyboard();
            storyboard.AddSlideFromLeft(animationTime, animatedNode.Width);

            storyboard.Begin(animatedNode);

            animatedNode.Visibility = System.Windows.Visibility.Visible;

            await Task.Delay((int)(animationTime * 1000));

        }

        #endregion


    }
}
