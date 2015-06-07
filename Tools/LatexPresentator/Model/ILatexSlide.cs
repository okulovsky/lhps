﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace LatexPresentator.Model
{
    public interface ISlideModel
    {
        Control GetControl();
        bool GoToNextState();
        bool GoToPreviousState();
    }
}
