//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

namespace Erinn
{
    internal interface IUIBase
    {
        UILayer Layer { get; }

        UIHideType HideType { get; }

        void Show();

        void Hide();
    }
}