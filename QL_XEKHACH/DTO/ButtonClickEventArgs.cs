using System;

namespace QL_XEKHACH.DTO
{
    public class ButtonClickEventArgs : EventArgs
    {
        public string ButtonName { get; }

        public ButtonClickEventArgs(string buttonName)
        {
            ButtonName = buttonName;
        }
    }
}
