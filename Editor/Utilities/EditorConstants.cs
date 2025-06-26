// ReSharper disable CheckNamespace

namespace Bidon.LevelPlayCustomAdapter.Utilities.Editor
{
    public static class EditorConstants
    {
        public const string PackageName = "com.unity.services.levelplay.adapters.bidon";
        public const string PackageRootDirectory = "Packages/" + PackageName;

        public const string DependenciesFileName = "ISBidonCustomAdapterDependencies";
        public const string RemoveListFileName = "remove_list";

        public const string PackageDependenciesFilePath = PackageRootDirectory + "/Editor/Dependencies/" + DependenciesFileName + ".txt";
        public const string PackageRemoveListFilePath = PackageRootDirectory + "/Editor/PluginRemover/" + RemoveListFileName + ".xml";

        public const string PluginEditorDirectory = "Assets/LevelPlayBidonCustomAdapter/Editor";
        public const string PluginDependenciesDirectory = PluginEditorDirectory + "/Dependencies";
        public const string PluginDependenciesFilePath = PluginDependenciesDirectory + "/" + DependenciesFileName + ".xml";

        public const string PluginDocumentationLink = "https://docs.bidon.org/docs/level-play/sdk-integration";
    }
}
