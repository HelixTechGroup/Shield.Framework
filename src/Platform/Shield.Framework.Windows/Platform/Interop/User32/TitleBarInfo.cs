﻿using System.Drawing;
using System.Runtime.InteropServices;
using Shield.Framework.Drawing;

namespace Shield.Framework.Platform.Interop.User32 {
    [StructLayout(LayoutKind.Sequential)]
    public struct TitleBarInfo
    {
        public uint Size;
        public Rectangle TitleBarRect;
        public ElementSystemStates TitleBarStates;
        private ElementSystemStates Reserved;
        public ElementSystemStates MinimizeButtonStates;
        public ElementSystemStates MaximizeButtonStates;
        public ElementSystemStates HelpButtonStates;
        public ElementSystemStates CloseButtonStates;
    }
}