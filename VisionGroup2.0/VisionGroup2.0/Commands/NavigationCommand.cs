﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace VisionGroup2._0.Commands
{
    public class NavigationCommand : CommandBase
    {
        private Frame _frame;
        private Type _pageType;
        private Func<bool> _canNavigateFunc;

        public NavigationCommand(Frame frame, Type pageType, Func<bool> canNavigateFunc)
        {
            this._frame = frame;
            this._pageType = pageType;
            this._canNavigateFunc = canNavigateFunc;
        }

        public NavigationCommand(Frame frame, Type pageType)
            : this(frame, pageType, (Func<bool>) (() => true))
        {
        }

        protected override bool CanExecute()
        {
            return this._canNavigateFunc();
        }

        protected override void Execute()
        {
            this._frame.Navigate(this._pageType);
        }
    }
}