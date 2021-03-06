﻿using System;

namespace Shield.Framework.Platform.Interop.User32 {
    [Flags]
    public enum RedrawWindowFlags
    {
        /// <summary>
        ///     Invalidates lprcUpdate or hrgnUpdate (only one may be non-NULL). If both are NULL, the entire window is
        ///     invalidated.
        /// </summary>
        RDW_INVALIDATE = 0x1,

        /// <summary>
        ///     Causes a WM_PAINT message to be posted to the window regardless of whether any portion of the window is invalid.
        /// </summary>
        RDW_INTERNALPAINT = 0x2,

        /// <summary>
        ///     Causes the window to receive a WM_ERASEBKGND message when the window is repainted. The RDW_INVALIDATE flag must
        ///     also be specified; otherwise, RDW_ERASE has no effect.
        /// </summary>
        RDW_ERASE = 0x4,

        /// <summary>
        ///     Validates lprcUpdate or hrgnUpdate (only one may be non-NULL). If both are NULL, the entire window is validated.
        ///     This flag does not affect internal WM_PAINT messages.
        /// </summary>
        RDW_VALIDATE = 0x8,

        /// <summary>
        ///     Suppresses any pending internal WM_PAINT messages. This flag does not affect WM_PAINT messages resulting from a
        ///     non-NULL update area.
        /// </summary>
        RDW_NOINTERNALPAINT = 0x10,

        /// <summary>
        ///     Suppresses any pending WM_ERASEBKGND messages.
        /// </summary>
        RDW_NOERASE = 0x20,

        /// <summary>
        ///     Excludes child windows, if any, from the repainting operation.
        /// </summary>
        RDW_NOCHILDREN = 0x40,

        /// <summary>
        ///     Includes child windows, if any, in the repainting operation.
        /// </summary>
        RDW_ALLCHILDREN = 0x80,

        /// <summary>
        ///     Causes the affected windows (as specified by the RDW_ALLCHILDREN and RDW_NOCHILDREN flags) to receive WM_NCPAINT,
        ///     WM_ERASEBKGND, and WM_PAINT messages, if necessary, before the function returns.
        /// </summary>
        RDW_UPDATENOW = 0x100,

        /// <summary>
        ///     Causes the affected windows (as specified by the RDW_ALLCHILDREN and RDW_NOCHILDREN flags) to receive WM_NCPAINT
        ///     and WM_ERASEBKGND messages, if necessary, before the function returns. WM_PAINT messages are received at the
        ///     ordinary time.
        /// </summary>
        RDW_ERASENOW = 0x200,

        /// <summary>
        ///     Causes any part of the nonclient area of the window that intersects the update region to receive a WM_NCPAINT
        ///     message. The RDW_INVALIDATE flag must also be specified; otherwise, RDW_FRAME has no effect. The WM_NCPAINT message
        ///     is typically not sent during the execution of RedrawWindow unless either RDW_UPDATENOW or RDW_ERASENOW is
        ///     specified.
        /// </summary>
        RDW_FRAME = 0x400,

        /// <summary>
        ///     Suppresses any pending WM_NCPAINT messages. This flag must be used with RDW_VALIDATE and is typically used with
        ///     RDW_NOCHILDREN. RDW_NOFRAME should be used with care, as it could cause parts of a window to be painted improperly.
        /// </summary>
        RDW_NOFRAME = 0x800
    }
}