using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace Sheetly.Animation
{
    public static class StoryboardHelpers
    {
        /// <summary>
        /// Add a slide animation to the storyboard
        /// </summary>
        /// <param name="storyboard">Story which will recevie the animation</param>
        /// <param name="seconds">Time, in seconds, of the animation</param>
        /// <param name="offset">The amount of slide offset desired</param>
        /// <param name="decelerationRatio">How quickly the animation decelerates</param>
        public static void AddSlideFromLeft(this Storyboard storyboard, float seconds, double offset, float decelerationRatio = 0.9f)
        {
            var slideAnimation = new ThicknessAnimation
            {
                Duration = new Duration(TimeSpan.FromSeconds(seconds)),
                From = new Thickness(-offset, 0, offset, 0),
                To = new Thickness(0),
                DecelerationRatio = decelerationRatio
            };

            Storyboard.SetTargetProperty(slideAnimation, new PropertyPath("Margin"));
            storyboard.Children.Add(slideAnimation);
        }
    }
}
