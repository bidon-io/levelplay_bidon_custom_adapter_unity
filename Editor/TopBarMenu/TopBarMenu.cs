// ReSharper disable CheckNamespace

using UnityEditor;
using UnityEngine;
using Bidon.LevelPlayCustomAdapter.PluginRemover.Editor;
using Bidon.LevelPlayCustomAdapter.Utilities.Editor;

namespace Bidon.LevelPlayCustomAdapter.TopBarMenu.Editor
{
    internal class TopBarMenu : ScriptableObject
    {
        [MenuItem("Ads Mediation/Bidon Adapter/Documentation", false, 42)]
        public static void OpenDocumentation()
        {
            Application.OpenURL(EditorConstants.PluginDocumentationLink);
        }

        [MenuItem("Ads Mediation/Bidon Adapter/Remove Plugin", false, 43)]
        public static void RemovePlugin()
        {
            RemoveHelper.RemovePlugin();
        }
    }
}
