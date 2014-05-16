//
// FSWPStoryboard.cs
// FSWP Library
//
// The class FSWPStoryboard is compatible with Windows Phone 7.1 and above
// This class is an abstraction of Storyboard class and provides simple ways to create animations
// It is a member of the namespace FSWP.Utils
//
// Copyright (c) 2014 Guillaume Bonnin
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

namespace FSWP.Utils
{
    using System;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Media.Animation;

    public class FSWPStoryboard
    {
        #region Properties

        /// <summary>
        /// Duration of the storyboard (in seconds)
        /// </summary>
        public double Duration
        {
            get { return _storyboard.Duration.TimeSpan.TotalSeconds; }
            set { _storyboard.Duration = new Duration(TimeSpan.FromSeconds(value)); }
        }

        /// <summary>
        /// Begin time of storyboard (in seconds)
        /// </summary>
        public double BeginTime
        {
            get { return ((_storyboard.BeginTime.HasValue) ? _storyboard.BeginTime.Value.TotalSeconds : 0); }
            set { _storyboard.BeginTime = TimeSpan.FromSeconds(value); }
        }

        /// <summary>
        /// Repeat of the storyboard
        /// </summary>
        public bool Repeat
        {
            get { return (_storyboard.RepeatBehavior == RepeatBehavior.Forever); }
            set { _storyboard.RepeatBehavior = (value) ? RepeatBehavior.Forever : new RepeatBehavior(0); }
        }

        /// <summary>
        /// Autoreverse of the storyboard
        /// </summary>
        public bool AutoReverse
        {
            get { return _storyboard.AutoReverse; }
            set { _storyboard.AutoReverse = value; }
        }

        /// <summary>
        /// Storyboard is null ?
        /// </summary>
        public bool IsNull
        {
            get { return (_storyboard == null); }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Create and init a FSWPStoryboard
        /// </summary>
        /// <param name="duration">Duration of the storyboard (in seconds)</param>
        /// <param name="repeat">Storyboard is repeat ?</param>
        public FSWPStoryboard(double? duration = null, bool repeat = false)
        {
            _storyboard = new Storyboard();
            if (duration.HasValue)
                _storyboard.Duration = new Duration(TimeSpan.FromSeconds(duration.Value));
            if (repeat)
                _storyboard.RepeatBehavior = RepeatBehavior.Forever;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Begin the storyboard
        /// </summary>
        public void Begin()
        {
            _storyboard.Begin();
        }

        /// <summary>
        /// Stop the storyboard
        /// </summary>
        public void Stop()
        {
            _storyboard.Stop();
        }

        /// <summary>
        /// Add an opacity animation in the storyboard
        /// </summary>
        /// <param name="target">Target of the animation</param>
        /// <param name="to">To value</param>
        /// <param name="from">From value</param>
        public void AddOpacityAnimation(DependencyObject target, double to, double? from = null)
        {
            _storyboard.Children.Add(CreateDoubleAnimation(target, new PropertyPath("Opacity"), to, from));
        }

        /// <summary>
        /// Add an opacity animation in the storyboard
        /// </summary>
        /// <param name="target">Target of the animation</param>
        /// <param name="duration">Duration of the animation (in seconds)</param>
        /// <param name="beginTime">Begin time of the animation (in seconds)</param>
        /// <param name="to">To value</param>
        /// <param name="from">From value</param>
        public void AddOpacityAnimation(DependencyObject target, double duration, double beginTime, double to, double? from = null)
        {
            _storyboard.Children.Add(CreateDoubleAnimation(target, new PropertyPath("Opacity"), to, from, duration, beginTime));
        }

        /// <summary>
        /// Add a width animation in the storyboard
        /// </summary>
        /// <param name="target">Target of the animation</param>
        /// <param name="to">To value</param>
        /// <param name="from">From value</param>
        public void AddWidthAnimation(DependencyObject target, double to, double? from = null)
        {
            _storyboard.Children.Add(CreateDoubleAnimation(target, new PropertyPath("Width"), to, from));
        }

        /// <summary>
        /// Add a width animation in the storyboard
        /// </summary>
        /// <param name="target">Target of the animation</param>
        /// <param name="duration">Duration of the animation (in seconds)</param>
        /// <param name="beginTime">Begin time of the animation (in seconds)</param>
        /// <param name="to">To value</param>
        /// <param name="from">From value</param>
        public void AddWidthAnimation(DependencyObject target, double duration, double beginTime, double to, double? from = null)
        {
            _storyboard.Children.Add(CreateDoubleAnimation(target, new PropertyPath("Width"), to, from, duration, beginTime));
        }

        /// <summary>
        /// Add an height animation in the storyboard
        /// </summary>
        /// <param name="target">Target of the animation</param>
        /// <param name="to">To value</param>
        /// <param name="from">From value</param>
        public void AddHeightAnimation(DependencyObject target, double to, double? from = null)
        {
            _storyboard.Children.Add(CreateDoubleAnimation(target, new PropertyPath("Height"), to, from));
        }

        /// <summary>
        /// Add an height animation in the storyboard
        /// </summary>
        /// <param name="target">Target of the animation</param>
        /// <param name="duration">Duration of the animation (in seconds)</param>
        /// <param name="beginTime">Begin time of the animation (in seconds)</param>
        /// <param name="to">To value</param>
        /// <param name="from">From value</param>
        public void AddHeightAnimation(DependencyObject target, double duration, double beginTime, double to, double? from = null)
        {
            _storyboard.Children.Add(CreateDoubleAnimation(target, new PropertyPath("Height"), to, from, duration, beginTime));
        }

        /// <summary>
        /// Add a size animation in the storyboard
        /// </summary>
        /// <param name="target">Target of the animation</param>
        /// <param name="to">To value</param>
        /// <param name="from">From value</param>
        public void AddSizeAnimation(DependencyObject target, Size to, Size? from = null)
        {
            if (from.HasValue)
            {
                _storyboard.Children.Add(CreateDoubleAnimation(target, new PropertyPath("width"), to.Width, from.Value.Width));
                _storyboard.Children.Add(CreateDoubleAnimation(target, new PropertyPath("height"), to.Height, from.Value.Height));
            }
            else
            {
                _storyboard.Children.Add(CreateDoubleAnimation(target, new PropertyPath("width"), to.Width, null));
                _storyboard.Children.Add(CreateDoubleAnimation(target, new PropertyPath("height"), to.Height, null));
            }
        }

        /// <summary>
        /// Add a size animation in the storyboard
        /// </summary>
        /// <param name="target">Target of the animation</param>
        /// <param name="duration">Duration of the animation (in seconds)</param>
        /// <param name="beginTime">Begin time of the animation (in seconds)</param>
        /// <param name="to">To value</param>
        /// <param name="from">From value</param>
        public void AddSizeAnimation(DependencyObject target, double duration, double beginTime, Size to, Size? from = null)
        {
            if (from.HasValue)
            {
                _storyboard.Children.Add(CreateDoubleAnimation(target, new PropertyPath("width"), to.Width, from.Value.Width, duration, beginTime));
                _storyboard.Children.Add(CreateDoubleAnimation(target, new PropertyPath("height"), to.Height, from.Value.Height, duration, beginTime));
            }
            else
            {
                _storyboard.Children.Add(CreateDoubleAnimation(target, new PropertyPath("width"), to.Width, null, duration, beginTime));
                _storyboard.Children.Add(CreateDoubleAnimation(target, new PropertyPath("height"), to.Height, null, duration, beginTime));
            }
        }

        /// <summary>
        /// Add an horizontal translate animation in the storyboard
        /// </summary>
        /// <param name="target">Target of the animation</param>
        /// <param name="to">To value</param>
        /// <param name="from">From value</param>
        public void AddHorizontalTranslateAnimation(DependencyObject target, double to, double? from = null)
        {
            _storyboard.Children.Add(CreateDoubleAnimation(target, new PropertyPath("(UIElement.RenderTransform).(CompositeTransform.TranslateX)"), to, from));
        }

        /// <summary>
        /// Add an horizontal translate animation in the storyboard
        /// </summary>
        /// <param name="target">Target of the animation</param>
        /// <param name="duration">Duration of the animation (in seconds)</param>
        /// <param name="beginTime">Begin time of the animation (in seconds)</param>
        /// <param name="to">To value</param>
        /// <param name="from">From value</param>
        public void AddHorizontalTranslateAnimation(DependencyObject target, double duration, double beginTime, double to, double? from = null)
        {
            _storyboard.Children.Add(CreateDoubleAnimation(target, new PropertyPath("(UIElement.RenderTransform).(CompositeTransform.TranslateX)"), to, from, duration, beginTime));
        }

        /// <summary>
        /// Add a vertical translate animation in the storyboard
        /// </summary>
        /// <param name="target">Target of the animation</param>
        /// <param name="to">To value</param>
        /// <param name="from">From value</param>
        public void AddVerticalTranslateAnimation(DependencyObject target, double to, double? from = null)
        {
            _storyboard.Children.Add(CreateDoubleAnimation(target, new PropertyPath("(UIElement.RenderTransform).(CompositeTransform.TranslateX)"), to, from));
        }

        /// <summary>
        /// Add a vertical translate animation in the storyboard
        /// </summary>
        /// <param name="target">Target of the animation</param>
        /// <param name="duration">Duration of the animation (in seconds)</param>
        /// <param name="beginTime">Begin time of the animation (in seconds)</param>
        /// <param name="to">To value</param>
        /// <param name="from">From value</param>
        public void AddVerticalTranslateAnimation(DependencyObject target, double duration, double beginTime, double to, double? from = null)
        {
            _storyboard.Children.Add(CreateDoubleAnimation(target, new PropertyPath("(UIElement.RenderTransform).(CompositeTransform.TranslateX)"), to, from, duration, beginTime));
        }

        /// <summary>
        /// Add a translate animation in the storyboard
        /// </summary>
        /// <param name="target">Target of the animation</param>
        /// <param name="to">To value</param>
        /// <param name="from">From value</param>
        public void AddTranslateAnimation(DependencyObject target, TranslateTransform to, TranslateTransform from = null)
        {
            if (from != null)
            {
                _storyboard.Children.Add(CreateDoubleAnimation(target, new PropertyPath("(UIElement.RenderTransform).(CompositeTransform.TranslateX)"), to.X, from.X));
                _storyboard.Children.Add(CreateDoubleAnimation(target, new PropertyPath("(UIElement.RenderTransform).(CompositeTransform.TranslateY)"), to.Y, from.Y));
            }
            else
            {
                _storyboard.Children.Add(CreateDoubleAnimation(target, new PropertyPath("(UIElement.RenderTransform).(CompositeTransform.TranslateX)"), to.X, null));
                _storyboard.Children.Add(CreateDoubleAnimation(target, new PropertyPath("(UIElement.RenderTransform).(CompositeTransform.TranslateY)"), to.Y, null));
            }
        }

        /// <summary>
        /// Add a translate animation in the storyboard
        /// </summary>
        /// <param name="target">Target of the animation</param>
        /// <param name="duration">Duration of the animation (in seconds)</param>
        /// <param name="beginTime">Begin time of the animation (in seconds)</param>
        /// <param name="to">To value</param>
        /// <param name="from">From value</param>
        public void AddTranslateAnimation(DependencyObject target, double duration, double beginTime, TranslateTransform to, TranslateTransform from = null)
        {
            if (from != null)
            {
                _storyboard.Children.Add(CreateDoubleAnimation(target, new PropertyPath("(UIElement.RenderTransform).(CompositeTransform.TranslateX)"), to.X, from.X, duration, beginTime));
                _storyboard.Children.Add(CreateDoubleAnimation(target, new PropertyPath("(UIElement.RenderTransform).(CompositeTransform.TranslateY)"), to.Y, from.Y, duration, beginTime));
            }
            else
            {
                _storyboard.Children.Add(CreateDoubleAnimation(target, new PropertyPath("(UIElement.RenderTransform).(CompositeTransform.TranslateX)"), to.X, null, duration, beginTime));
                _storyboard.Children.Add(CreateDoubleAnimation(target, new PropertyPath("(UIElement.RenderTransform).(CompositeTransform.TranslateY)"), to.Y, null, duration, beginTime));
            }
        }

        #endregion

        #region Private

        private Storyboard _storyboard;

        private DoubleAnimation CreateDoubleAnimation(DependencyObject target, PropertyPath propertyPath, double to, double? from, double? duration = null, double? beginTime = null)
        {
            DoubleAnimation anim = new DoubleAnimation();
            anim.Duration = (duration.HasValue) ? new Duration(TimeSpan.FromSeconds(duration.Value)) : _storyboard.Duration;
            if (beginTime.HasValue)
                anim.BeginTime = TimeSpan.FromSeconds(beginTime.Value);
            if (from.HasValue)
                anim.From = from.Value;
            anim.To = to;
            Storyboard.SetTarget(anim, target);
            Storyboard.SetTargetProperty(anim, propertyPath);
            return anim;
        }

        #endregion
    }
}
