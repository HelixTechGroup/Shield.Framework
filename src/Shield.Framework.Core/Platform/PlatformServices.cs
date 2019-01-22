﻿using Shield.Framework.Platform.IO;
using Shield.Framework.Platform.Logging;
using Shield.Framework.Platform.Threading;

namespace Shield.Framework.Platform
{
	public class PlatformServices : IPlatformServices
	{
		protected IPlatformLogProvider m_logger;
		protected IPlatformDispatcherProvider m_dispatcher;
		protected IPlatformStorageProvider m_storage;

		public PlatformServices(IPlatformLogProvider logger, IPlatformDispatcherProvider dispatcher, IPlatformStorageProvider storage)
		{
			m_logger = logger;
			m_dispatcher = dispatcher;
			m_storage = storage;
		}

		public IPlatformLogProvider Logger
		{
			get { return m_logger; }
		}

		public IPlatformDispatcherProvider Dispatcher
		{
			get { return m_dispatcher; }
		}

		public IPlatformStorageProvider Storage
		{
			get { return m_storage; }
		}
	}
}