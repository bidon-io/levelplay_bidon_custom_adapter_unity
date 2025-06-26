// ReSharper disable CheckNamespace

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEngine;
using Bidon.LevelPlayCustomAdapter.Utilities.Editor;

namespace Bidon.LevelPlayCustomAdapter.PluginRemover.Editor
{
    public static class RemoveHelper
    {
        public static void RemovePlugin()
        {
            if (!EditorUtility.DisplayDialog("Bidon Adapter Unity Plugin",
                    "Are you sure you want to remove this plugin from your project?",
                    "Yes",
                    "Cancel")) return;

            var items = ReadXML();
            foreach (var item in items)
            {
                string fullItemPath = $"{Application.dataPath}/{item.path}";

                bool isChecked = !item.checkIfEmpty;
                if (!isChecked) isChecked = IsFolderEmpty(fullItemPath);
                if (!isChecked) continue;

                if (String.IsNullOrEmpty(item.filter))
                {
                    FileUtil.DeleteFileOrDirectory(fullItemPath);
                    FileUtil.DeleteFileOrDirectory(fullItemPath + ".meta");
                    continue;
                }

                bool isDirectoryExists = Directory.Exists(fullItemPath);
                if (!isDirectoryExists) continue;

                var filePaths = new List<string>(Directory.GetFiles(fullItemPath, "*", SearchOption.TopDirectoryOnly));
                filePaths.Where(filePath => Regex.IsMatch(Path.GetFileName(filePath), item.filter, RegexOptions.IgnoreCase)).ToList().ForEach(filePath =>
                {
                    FileUtil.DeleteFileOrDirectory(filePath);
                    FileUtil.DeleteFileOrDirectory(filePath + ".meta");
                });

                if (!IsFolderEmpty(fullItemPath)) continue;
                FileUtil.DeleteFileOrDirectory(fullItemPath);
                FileUtil.DeleteFileOrDirectory(fullItemPath + ".meta");
            }

            Client.Remove(EditorConstants.PackageName);
        }

        private static IEnumerable<ItemToRemove> ReadXML()
        {
            var itemsToRemove = new List<ItemToRemove>();

            var xmlDoc = new XmlDocument();
            xmlDoc.Load(EditorConstants.PackageRemoveListFilePath);

            var xmlRoot = xmlDoc.DocumentElement;
            if (xmlRoot == null) return itemsToRemove;

            foreach (XmlNode node in xmlRoot)
            {
                var itemToRemove = new ItemToRemove();
                foreach (XmlNode childNode in node.ChildNodes)
                {
                    if (childNode.Name.Equals("name"))
                    {
                        itemToRemove.name = childNode.InnerText;
                    }

                    if (childNode.Name.Equals("path"))
                    {
                        itemToRemove.path = childNode.InnerText;
                    }

                    if (childNode.Name.Equals("filter"))
                    {
                        itemToRemove.filter = childNode.InnerText;
                    }

                    if (childNode.Name.Equals("description"))
                    {
                        itemToRemove.description = childNode.InnerText;
                    }

                    if (childNode.Name.Equals("check_if_empty"))
                    {
                        if (childNode.InnerText.Equals("true"))
                        {
                            itemToRemove.checkIfEmpty = true;
                        }
                        else if (childNode.InnerText.Equals("false"))
                        {
                            itemToRemove.checkIfEmpty = false;
                        }
                    }

                    if (childNode.Name.Equals("is_confirmation_required"))
                    {
                        if (childNode.InnerText.Equals("true"))
                        {
                            itemToRemove.isConfirmationRequired = true;
                        }
                        else if (childNode.InnerText.Equals("false"))
                        {
                            itemToRemove.isConfirmationRequired = false;
                        }
                    }

                    if (childNode.Name.Equals("perform_only_if_total_remove"))
                    {
                        if (childNode.InnerText.Equals("true"))
                        {
                            itemToRemove.performOnlyIfTotalRemove = true;
                        }
                        else if (childNode.InnerText.Equals("false"))
                        {
                            itemToRemove.performOnlyIfTotalRemove = false;
                        }
                    }
                }

                itemsToRemove.Add(itemToRemove);
            }

            return itemsToRemove;
        }

        private static bool IsFolderEmpty(string path)
        {
            if (!Directory.Exists(path)) return false;
            string[] filesPaths = Directory.GetFiles(path, "*", SearchOption.TopDirectoryOnly);
            return filesPaths.Count(filePath => !filePath.Contains(".DS_Store")) == 0;
        }
    }
}
