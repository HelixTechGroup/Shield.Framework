﻿#region Usings
using System;
using System.IO;
using Shield.Framework.Platform;
using SysEnv = System.Environment;
#endregion

namespace Shield.Framework.Environment
{
    public class ApplicationEnvironment : IApplicationEnvironment
    {
        #region Members
        private readonly IOperatingSystemInformation m_operatingSystem;
        private readonly IRuntimeInformation m_runtime;
        private ApplicationType m_applicationType;
        private bool m_isRuntimeCodeGenerationSupported;
        #endregion

        #region Properties
        /// <inheritdoc />
        public ApplicationType ApplicationType
        {
            get { return m_applicationType; }
        }

        /// <inheritdoc />
        public string CurrentDirectory
        {
            get { return SysEnv.CurrentDirectory; }
        }

        /// <inheritdoc />
        public string DirectorySeparator
        {
            get { return new string(Path.DirectorySeparatorChar, 1); }
        }

        /// <inheritdoc />
        public bool Is64BitProcess
        {
            get { return SysEnv.Is64BitProcess; }
        }

        /// <inheritdoc />
        public bool IsRuntimeCodeGenerationSupported
        {
            get { return m_isRuntimeCodeGenerationSupported; }
        }

        /// <inheritdoc />
        public string NewLine
        {
            get { return SysEnv.NewLine; }
        }

        /// <inheritdoc />
        public IOperatingSystemInformation OperatingSystem
        {
            get { return m_operatingSystem; }
        }

        /// <inheritdoc />
        public string PathSeparator
        {
            get { return new string(Path.PathSeparator, 1); }
        }

        /// <inheritdoc />
        public string RootDirectory
        {
            get { return AppDomain.CurrentDomain.BaseDirectory; }
        }

        /// <inheritdoc />
        public IRuntimeInformation Runtime
        {
            get { return m_runtime; }
        }
        #endregion

        public ApplicationEnvironment()
        {
            m_operatingSystem = new OperatingSystemInformation();
            m_runtime = new RuntimeInformation();
        }

        #region Methods
        /// <inheritdoc />
        public void DetectApplication() { }

        /// <inheritdoc />
        public void DetectPlatform()
        {
            m_operatingSystem.DetectOperatingSystem();
            m_runtime.DetectRuntime();
            PlatformManager.CurrentPlatform.CreatePlatform(m_operatingSystem, m_runtime);
        }
        #endregion
    }
}