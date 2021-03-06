﻿#region Usings
using Shield.Framework.Platform.Interop;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using Shield.Framework.Collections;
using Shield.Framework.Platform.Interop.User32;
using Shield.Framework.Platform.Interop.Kernel32;
using static Shield.Framework.Platform.Interop.User32.Methods;
using static Shield.Framework.Platform.Interop.Kernel32.Methods;
#endregion

namespace Shield.Framework.Platform
{
    public class NativeApplication : DisposableInitializable, INativeApplication, IWindowsProcess
    {
        #region Members
        private WindowProc _wndProc;
        private readonly string m_windowClass;
        private INativeHandle m_handle;
        private IList<INativeWindow> m_windows;
        #endregion

        #region Properties
        public INativeHandle Handle
        {
            get { return m_handle; }
        }

        /// <inheritdoc />
        public WindowProc Process
        {
            get { return _wndProc; }
        }

        /// <inheritdoc />
        public IEnumerable<INativeWindow> Windows
        {
            get { return m_windows; }
        }

        /// <inheritdoc />
        public INativeWindow CreateWindow()
        {
            Throw.If(!m_isInitialized).InvalidOperationException();
            return CreateNativeWindow();
        }
        #endregion

        public NativeApplication()
        {
            m_windows = new ConcurrentList<INativeWindow>();
            m_windowClass = "ShieldMessageWindow-" + Guid.NewGuid();
        }

        #region Methods
        protected override void InitializeResources()
        {
            CreateMessageWindow();
            base.InitializeResources();
        }

        private IntPtr WindowProc(IntPtr hWnd, WindowsMessage msg, IntPtr wParam, IntPtr lParam)
        {
            var platform = PlatformManager.CurrentPlatform.Dispatcher as NativeThreadDispatcher;
            switch (msg)
            {
                case WindowsMessage.DISPATCH_WORK_ITEM:
                    if (wParam.ToInt64() == Constants.SignalW &&
                        lParam.ToInt64() == Constants.SignalL) 
                        platform?.Signal();
                    break;
                case WindowsMessage.DESTROY:
                    PostQuitMessage(0);
                    return IntPtr.Zero;
                case WindowsMessage.QUIT:
                    break;
            }

            

            return DefWindowProc(hWnd, msg, wParam, lParam);
        }

        private void CreateMessageWindow()
        {
            // Ensure that the delegate doesn't get garbage collected by storing it as a field.
            _wndProc = WindowProc;
            var hInstance = GetModuleHandle(null);

            var wndClassEx = new WindowClassEx()
                                    {
                                        Size = (uint)Marshal.SizeOf<WindowClassEx>(),
                                        WindowProc = _wndProc,
                                        InstanceHandle = hInstance,
                                        ClassName = m_windowClass
                                    };

            var atom = RegisterClassEx(ref wndClassEx);
            if (atom == 0) throw new Win32Exception();

            var hwnd = CreateWindowEx(0, m_windowClass, null, 
                                      WindowStyles.WS_OVERLAPPED, 
                                      Constants.CW_USEDEFAULT, 
                                      Constants.CW_USEDEFAULT,
                                      Constants.CW_USEDEFAULT,
                                      Constants.CW_USEDEFAULT, 
                                      Constants.HWND_MESSAGE,
                                      IntPtr.Zero, 
                                      hInstance, 
                                      IntPtr.Zero);

            if (hwnd == IntPtr.Zero)
            {
                var error = GetLastError();
                UnregisterClass(m_windowClass, hInstance);
                throw new Win32Exception((int)error);
            }

            m_handle = new NativeHandle(hwnd, "");

            
        }

        /// <inheritdoc />
        protected override void DisposeUnmanagedResources()
        {
            UnregisterClass(m_windowClass, GetModuleHandle(null));
        }

        protected override void DisposeManagedResources()
        {
            
        }

        private INativeWindow CreateNativeWindow()
        {
            var win = new NativeWindow("", 1920, 1080);
            win.Create(this);
            m_windows.Add(win);
            return win;
        }
        #endregion
    }
}