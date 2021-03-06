﻿#region Usings
using System;
using Shield.Framework.Services.IO;
#endregion

namespace Shield.Framework.Platform.IO.FileSystems
{
    public interface IPrivateApplicationFileSystem : IFileSystem, IEquatable<IPrivateApplicationFileSystem> { }
}