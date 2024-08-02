#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;


namespace MFolders
{
    class MFoldersMenu
    {

        public static bool hierarchyLinesEnabled { get => EditorPrefs.GetBool("mFolders-hierarchyLinesEnabled", false); set => EditorPrefs.SetBool("mFolders-hierarchyLinesEnabled", value); }
        public static bool clearerRowsEnabled { get => EditorPrefs.GetBool("mFolders-clearerRowsEnabled", false); set => EditorPrefs.SetBool("mFolders-clearerRowsEnabled", value); }
        public static bool minimalModeEnabled { get => EditorPrefs.GetBool("mFolders-minimalModeEnabled", false); set => EditorPrefs.SetBool("mFolders-minimalModeEnabled", value); }
        public static bool zebraStripingEnabled { get => EditorPrefs.GetBool("mFolders-zebraStripingEnabled", false); set => EditorPrefs.SetBool("mFolders-zebraStripingEnabled", value); }
        public static bool contentMinimapEnabled { get => EditorPrefs.GetBool("mFolders-contentMinimapEnabled", false); set => EditorPrefs.SetBool("mFolders-contentMinimapEnabled", value); }
        public static bool autoIconsEnabled { get => EditorPrefs.GetBool("mFolders-autoIconsEnabled", false); set => EditorPrefs.SetBool("mFolders-autoIconsEnabled", value); }
        public static bool foldersFirstEnabled { get => EditorPrefs.GetBool("mFolders-foldersFirstEnabled", false); set => EditorPrefs.SetBool("mFolders-foldersFirstEnabled", value); }

        public static bool toggleExpandedEnabled { get => EditorPrefs.GetBool("mFolders-toggleExpandedEnabled", true); set => EditorPrefs.SetBool("mFolders-toggleExpandedEnabled", value); }
        public static bool collapseEverythingElseEnabled { get => EditorPrefs.GetBool("mFolders-collapseEverythingElseEnabled", true); set => EditorPrefs.SetBool("mFolders-collapseEverythingElseEnabled", value); }
        public static bool collapseEverythingEnabled { get => EditorPrefs.GetBool("mFolders-collapseEverythingEnabled", true); set => EditorPrefs.SetBool("mFolders-collapseEverythingEnabled", value); }

        public static bool pluginDisabled { get => EditorPrefs.GetBool("mFolders-pluginDisabled", false); set => EditorPrefs.SetBool("mFolders-pluginDisabled", value); }




        const string dir = "Tools/mFolders/";

        const string hierarchyLines = dir + "Hierarchy lines";
        const string clearerRows = dir + "Clearer rows";
        const string minimalMode = dir + "Minimal mode";
        const string zebraStriping = dir + "Zebra striping";
        const string autoIcons = dir + "Automatic icons";
        const string contentMinimap = dir + "Content minimap";
        const string foldersFirst = dir + "Sort folders first";

        const string toggleExpanded = dir + "E to expand or collapse";
        const string collapseEverythingElse = dir + "Shift-E to collapse everything else";
        const string collapseEverything = dir + "Ctrl-Shift-E to collapse everything";

        const string disablePlugin = dir + "Disable mFolders";






        [MenuItem(dir + "Features", false, 1)] static void daasddsas() { }
        [MenuItem(dir + "Features", true, 1)] static bool dadsdasas123() => false;

        [MenuItem(hierarchyLines, false, 2)] static void dadsadadsadass() { hierarchyLinesEnabled = !hierarchyLinesEnabled; EditorApplication.RepaintProjectWindow(); }
        [MenuItem(hierarchyLines, true, 2)] static bool dadsaddasaasddsas() { Menu.SetChecked(hierarchyLines, hierarchyLinesEnabled); return !pluginDisabled; }

        [MenuItem(clearerRows, false, 3)] static void dadsadadsadsadass() { clearerRowsEnabled = !clearerRowsEnabled; EditorApplication.RepaintProjectWindow(); }
        [MenuItem(clearerRows, true, 3)] static bool dadsaddasadsaasddsas() { Menu.SetChecked(clearerRows, clearerRowsEnabled); return !pluginDisabled; }

        [MenuItem(minimalMode, false, 4)] static void dadsadadsaddsasadass() { minimalModeEnabled = !minimalModeEnabled; EditorApplication.RepaintProjectWindow(); }
        [MenuItem(minimalMode, true, 4)] static bool dadsaddasadsadsaasddsas() { Menu.SetChecked(minimalMode, minimalModeEnabled); return !pluginDisabled; }

        [MenuItem(zebraStriping, false, 5)] static void dadsadaddsasadsadass() { zebraStripingEnabled = !zebraStripingEnabled; EditorApplication.RepaintProjectWindow(); }
        [MenuItem(zebraStriping, true, 5)] static bool dadsaddadassadsaasddsas() { Menu.SetChecked(zebraStriping, zebraStripingEnabled); return !pluginDisabled; }

        [MenuItem(contentMinimap, false, 6)] static void dadsadadasdsadass() { contentMinimapEnabled = !contentMinimapEnabled; EditorApplication.RepaintProjectWindow(); }
        [MenuItem(contentMinimap, true, 6)] static bool dadsadddasasaasddsas() { Menu.SetChecked(contentMinimap, contentMinimapEnabled); return !pluginDisabled; }

        [MenuItem(autoIcons, false, 7)] static void dadsadadsas() { autoIconsEnabled = !autoIconsEnabled; EditorApplication.RepaintProjectWindow(); }
        [MenuItem(autoIcons, true, 7)] static bool dadsaddasadsas() { Menu.SetChecked(autoIcons, autoIconsEnabled); return !pluginDisabled; }
#if UNITY_EDITOR_OSX
        [MenuItem(foldersFirst, false, 8)] static void dadsdsfaadsdadsas() { foldersFirstEnabled = !foldersFirstEnabled; EditorApplication.RepaintProjectWindow(); if (!foldersFirstEnabled) UnityEditor.Compilation.CompilationPipeline.RequestScriptCompilation(); }
        [MenuItem(foldersFirst, true, 8)] static bool dadsasdfdadsdasadsas() { Menu.SetChecked(foldersFirst, foldersFirstEnabled); return !pluginDisabled; }
#endif



        [MenuItem(dir + "Shortcuts", false, 101)] static void dadsas() { }
        [MenuItem(dir + "Shortcuts", true, 101)] static bool dadsas123() => false;

        [MenuItem(toggleExpanded, false, 102)] static void dadsadsadasdsadadsas() => toggleExpandedEnabled = !toggleExpandedEnabled;
        [MenuItem(toggleExpanded, true, 102)] static bool dadsaddsasadadsdasadsas() { Menu.SetChecked(toggleExpanded, toggleExpandedEnabled); return !pluginDisabled; }

        [MenuItem(collapseEverythingElse, false, 103)] static void dadsadsasdadasdsadadsas() => collapseEverythingElseEnabled = !collapseEverythingElseEnabled;
        [MenuItem(collapseEverythingElse, true, 103)] static bool dadsaddsdasasadadsdasadsas() { Menu.SetChecked(collapseEverythingElse, collapseEverythingElseEnabled); return !pluginDisabled; }

        [MenuItem(collapseEverything, false, 104)] static void dadsadsdasadasdsadadsas() => collapseEverythingEnabled = !collapseEverythingEnabled;
        [MenuItem(collapseEverything, true, 104)] static bool dadsaddssdaasadadsdasadsas() { Menu.SetChecked(collapseEverything, collapseEverythingEnabled); return !pluginDisabled; }

        [MenuItem(disablePlugin, false, 10001)] static void dadsadsdasadasdasdsadadsas() { pluginDisabled = !pluginDisabled; UnityEditor.Compilation.CompilationPipeline.RequestScriptCompilation(); }
        [MenuItem(disablePlugin, true, 10001)] static bool dadsaddssdaasadsadadsdasadsas() { Menu.SetChecked(disablePlugin, pluginDisabled); return true; }




        // [MenuItem(dir + "Clear cache", false, 10001)]
        // static void dassaadsdc() => mFoldersCache.Clear();

    }
}
#endif