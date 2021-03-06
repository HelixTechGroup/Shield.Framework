﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using Shield.Framework.Platform.Interop.User32;
using Shield.Framework.Threading;
using static Shield.Framework.Platform.Interop.User32.Methods;
using static Shield.Framework.Platform.Interop.Kernel32.Methods;

namespace Shield.Framework.Platform
{
    public abstract class WindowsProcessHook : Disposable
    {
        protected IWindowsProcess m_process;
        protected HookProc m_hookProc;
        protected INativeHandle m_hookHandle;
        protected WindowHookType m_hookType;

        protected WindowsProcessHook(IWindowsProcess process, WindowHookType hookType)
        {
            m_process = process;
            m_hookProc = HookProc;
            m_hookType = hookType;
            var hhnd = IntPtr.Zero;
            var mhnd = IntPtr.Zero;
            //using var proc = Process.GetCurrentProcess().MainModule;
            using var module = Process.GetCurrentProcess().MainModule;
            var threadId = GetWindowThreadProcessId(m_process.Handle.Pointer, IntPtr.Zero);
            //foreach (ProcessModule module in proc.Modules)
            //{
            //    mhnd = GetModuleHandle(module.ModuleName);
            //    //hhnd = SetWindowsHookEx(m_hookType, m_hookProc, mhnd, 0);
            //    if (mhnd == IntPtr.Zero || hhnd == IntPtr.Zero)
            //        continue;
            //}

            mhnd = GetModuleHandle(module?.ModuleName);
            hhnd = SetWindowsHookEx(m_hookType, m_hookProc, mhnd, threadId);

            if (hhnd == IntPtr.Zero)
                throw new Win32Exception();

            m_hookHandle = new NativeHandle(hhnd, "");
        }

        private IntPtr HookProc(WindowHookCode nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode < 0)
                return CallNextHookEx(m_hookHandle.Pointer, nCode, wParam, lParam);

            var wMsg = new WindowMessage();
            switch (m_hookType)
            {
                case WindowHookType.WH_CALLWNDPROCRET:
                    var msgRet = (CwpRetStruct)Marshal.PtrToStructure(lParam, typeof(CwpRetStruct));
                    wMsg = new WindowMessage
                                {
                                    Id = (WindowsMessage)msgRet.Message,
                                    WParam = msgRet.WParam,
                                    LParam = msgRet.LParam,
                                    Result = msgRet.LResult,
                                    Hwnd = msgRet.Hwnd
                                };
                    return OnProcRet(wMsg);
                case WindowHookType.WH_JOURNALRECORD:
                    break;
                case WindowHookType.WH_JOURNALPLAYBACK:
                    break;
                case WindowHookType.WH_KEYBOARD:
                    break;
                case WindowHookType.WH_GETMESSAGE:
                    var msg = (Message)Marshal.PtrToStructure(lParam, typeof(Message));
                    wMsg = new WindowMessage
                           {
                               Id = (WindowsMessage)msg.Value,
                               WParam = msg.WParam,
                               LParam = msg.LParam,
                               Result = IntPtr.Zero,
                               Hwnd = msg.Hwnd
                           };
                    return OnGetMsg(wMsg);
                case WindowHookType.WH_CALLWNDPROC:
                    break;
                case WindowHookType.WH_CBT:
                    break;
                case WindowHookType.WH_SYSMSGFILTER:
                    break;
                case WindowHookType.WH_MOUSE:
                    break;
                case WindowHookType.WH_HARDWARE:
                    break;
                case WindowHookType.WH_DEBUG:
                    break;
                case WindowHookType.WH_SHELL:
                    break;
                case WindowHookType.WH_FOREGROUNDIDLE:
                    break;
                case WindowHookType.WH_KEYBOARD_LL:
                    break;
                case WindowHookType.WH_MOUSE_LL:
                    break;
            }

            return CallNextHookEx(m_hookHandle.Pointer, nCode, wParam, lParam);
        }

        protected virtual IntPtr OnGetMsg(WindowMessage message) { return IntPtr.Zero; }

        protected virtual IntPtr OnProcRet(WindowMessage message) { return IntPtr.Zero; }

        /// <inheritdoc />
        protected override void DisposeManagedResources()
        {
            UnhookWindowsHookEx(m_hookHandle.Pointer);
        }
    }
}
