using UnityEditor;
using System.Linq;

class DoBuild
{
	static void PerformBuild ()
	{

		string path = "../builds/";
		string gameName = "Discovering Colors";

		string[] scenes = { 
			"Assets/_scenes/sceneBonusCri.unity",
			"Assets/_scenes/sceneBonusEspace.unity",
			"Assets/_scenes/sceneBonusPingouin.unity",
			"Assets/_scenes/sceneBonusPoisson.unity",
			"Assets/_scenes/sceneChat.unity",
			"Assets/_scenes/sceneChouette.unity",
			"Assets/_scenes/sceneCoccinelle.unity",
			"Assets/_scenes/sceneCrabe.unity",
			"Assets/_scenes/sceneEscargot.unity",
			"Assets/_scenes/sceneGiraffe.unity",
			"Assets/_scenes/sceneGrenouille.unity",
			"Assets/_scenes/sceneHerisson.unity",
			"Assets/_scenes/scenePapillon.unity",
			"Assets/_scenes/scenePieuvre.unity",
			"Assets/_scenes/sceneTortue.unity",
			"Assets/_scenes/sceneVague.unity",
			"Assets/_scenes/sceneLibre.unity",
		};
		string[] scenesDesktop = (new string[] {"Assets/_scenes/_menu_nouveau.unity"}).Concat(scenes).ToArray();
		string[] scenesMobile = (new string[] {"Assets/_scenes/_menu_nouveau_iOS.unity"}).Concat(scenes).ToArray();
		string[] scenesDesktopSteam = (new string[] {"Assets/_scenes/_steammanager.unity"}).Concat(scenesDesktop).ToArray();



		// PC - DRM free
		EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTarget.StandaloneWindows);
		BuildPipeline.BuildPlayer(scenesDesktop, path + "pc_drmfree/Discovering Colors/" + gameName + ".exe", BuildTarget.StandaloneWindows, BuildOptions.None);

		// PC - Steam
		EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTarget.StandaloneWindows);
		BuildPipeline.BuildPlayer(scenesDesktopSteam, path + "pc_steam/" + gameName + ".exe", BuildTarget.StandaloneWindows, BuildOptions.None);

		// Mac - DRM free
		EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTarget.StandaloneOSXIntel);
		BuildPipeline.BuildPlayer(scenesDesktop, path + "mac_drmfree/" + gameName + ".app", BuildTarget.StandaloneOSXIntel, BuildOptions.None);

		// Mac - Steam
		EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTarget.StandaloneOSXIntel);
		BuildPipeline.BuildPlayer(scenesDesktopSteam, path + "mac_steam/" + gameName + ".app", BuildTarget.StandaloneOSXIntel, BuildOptions.None);

		// Mac - MacAppStore
		EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTarget.StandaloneOSXIntel);
		PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone, "MACAPPSTORE");
		PlayerSettings.useMacAppStoreValidation = true;
		BuildPipeline.BuildPlayer(scenesDesktop, path + "mac_macappstore/" + gameName + ".app", BuildTarget.StandaloneOSXIntel, BuildOptions.None);
		// reset prefs - on remet les prefs comme elles etaient avant car elles seront sauvegardées !
		PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone, "");
		PlayerSettings.useMacAppStoreValidation = false;


		// iOS - AppStore
		EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTarget.iOS);
		BuildPipeline.BuildPlayer(scenesMobile, path + "ios/", BuildTarget.iOS, BuildOptions.None);

	}
}
