// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.
using System;
using System.IO;
using Mono.Unix;
using Serilog;

namespace AttackSurfaceAnalyzer.Collectors.FileSystem
{
    public class LinuxFileSystemUtils
    {

        protected internal static long GetFileOwner(FileSystemInfo fileInfo)
        {
            var filename = fileInfo.FullName;
            UnixUserInfo owner = default(UnixUserInfo);

            if (fileInfo is FileInfo)
            {
                try
                {
                    var ufi = new UnixFileInfo(filename);
                    owner = ufi.OwnerUser;
                }
                catch (Exception ex)
                {
                    Log.Warning("Unable to get access control for {0}: {1}", fileInfo.FullName, ex.Message);
                }
            }
            else if (fileInfo is DirectoryInfo)
            {
                try
                {
                    var udi = new UnixDirectoryInfo(filename);
                    owner = udi.OwnerUser;
                }
                catch (Exception ex)
                {
                    Log.Warning("Unable to get access control for {0}: {1}", fileInfo.FullName, ex.Message);
                }
            }
            else
            {
                return -1;
            }

            return owner.UserId;
        }

        protected internal static long GetFileGroup(FileSystemInfo fileInfo)
        {
            var filename = fileInfo.FullName;
            UnixGroupInfo group = default(UnixGroupInfo);

            if (fileInfo is FileInfo)
            {
                try
                {
                    var ufi = new UnixFileInfo(filename);
                    group = ufi.OwnerGroup;
                }
                catch (Exception ex)
                {
                    Log.Warning("Unable to get access control for {0}: {1}", fileInfo.FullName, ex.Message);
                }
            }
            else if (fileInfo is DirectoryInfo)
            {
                try
                {
                    var udi = new UnixDirectoryInfo(filename);
                    group = udi.OwnerGroup;
                }
                catch (Exception ex)
                {
                    Log.Warning("Unable to get access control for {0}: {1}", fileInfo.FullName, ex.Message);
                }
            }
            else
            {
                return -1;
            }

            return group.GroupId;
        }

        protected internal static string GetFilePermissions(FileSystemInfo fileInfo)
        {
            var filename = fileInfo.FullName;

            FileAccessPermissions permissions = default(FileAccessPermissions);
            UnixGroupInfo group = default(UnixGroupInfo);

            if (fileInfo is FileInfo)
            {
                try
                {
                    var ufi = new UnixFileInfo(filename);
                    permissions = ufi.FileAccessPermissions;
                    owner = ufi.OwnerUser;
                    group = ufi.OwnerGroup;
                }
                catch (Exception ex)
                {
                    Log.Warning("Unable to get access control for {0}: {1}", fileInfo.FullName, ex.Message);
                }
            }
            else if (fileInfo is DirectoryInfo)
            {
                try
                {
                    var udi = new UnixDirectoryInfo(filename);
                    permissions = udi.FileAccessPermissions;
                    owner = udi.OwnerUser;
                    group = udi.OwnerGroup;
                }
                catch (Exception ex)
                {
                    Log.Warning("Unable to get access control for {0}: {1}", fileInfo.FullName, ex.Message);
                }
            }
            else
            {
                return null;
            }

            return permissions.ToString();
        }
    }
}