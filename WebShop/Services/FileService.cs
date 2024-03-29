﻿using System;
using System.IO;

namespace WebShop.Services
{
    public class FileService : IFileService
    {
        public bool Exists(string path)
        {
            try
            {
                return File.Exists(path);
            }
            catch (Exception ex)
            {
                throw new FileServiceException($"Error checking file existence at path: {path}. {ex.Message}", ex);
            }
        }

        public string ReadAllText(string path)
        {
            try
            {
                if (File.Exists(path))
                {
                    return File.ReadAllText(path);
                }
                return "";
            }
            catch (Exception ex)
            {
                throw new FileServiceException($"Error reading file at path: {path}. {ex.Message}", ex);
            }
        }

        public void WriteAllText(string path, string content)
        {
            try
            {
                File.WriteAllText(path, content);
            }
            catch (Exception ex)
            {
                throw new FileServiceException($"Error writing file at path: {path}. {ex.Message}", ex);
            }
        }
    }

    public class FileServiceException : Exception
    {
        public FileServiceException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}