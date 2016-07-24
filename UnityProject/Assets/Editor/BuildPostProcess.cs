using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;
using System;
using System.IO;

public class BuildPostProcess
{
    [PostProcessBuild]
    static void OnPostprocessBuild(BuildTarget buildTarget, string path)
    {
    	Debug.Log("== OnPostprocessBuild : " + buildTarget + " path : " + path);
        if (buildTarget == BuildTarget.iOS) {
			UpdateiOSInfoPlist(path);
		} 
		if (buildTarget == BuildTarget.StandaloneOSXIntel || buildTarget == BuildTarget.StandaloneOSXIntel64 || buildTarget == BuildTarget.StandaloneOSXUniversal) {
			UpdatemacOSInfoPlist(path, buildTarget);
        }

    }


	private static void UpdateiOSInfoPlist(string path) {

    Debug.Log("== UpdateiOSInfoPlist");

		// MODIFICATION DU PLIST
    var plistPath = Path.Combine(path, "Info.plist");

    var plist = new PlistDocument();
    plist.ReadFromFile(plistPath);

    // ajout des localisations
		PlistElementArray CFBundleLocalizations = plist.root.CreateArray("CFBundleLocalizations");
		CFBundleLocalizations.AddString ("en");
		CFBundleLocalizations.AddString ("fr");
		CFBundleLocalizations.AddString ("de");
		CFBundleLocalizations.AddString ("ja");
		CFBundleLocalizations.AddString ("es");

		// le numero du build
		plist.root.SetString("CFBundleVersion", DateTime.Now.ToString("yyyyMMddHHmmss"));

    plist.WriteToFile(plistPath);

	}

	private static void UpdatemacOSInfoPlist(string path, BuildTarget buildTarget) {

    Debug.Log("== UpdatemacOSInfoPlist");
		// infos complementaires : https://gentlymad.org/blog/post/deliver-mac-store-unity

		// MODIFICATION DU PLIST
		//string buildFolderPath = EditorUserBuildSettings.GetBuildLocation(buildTarget);
		string plistPath = path + "/Contents/Info.plist";

    var plist = new PlistDocument();
    plist.ReadFromFile(plistPath);

    // ajout des localisations
		PlistElementArray CFBundleLocalizations = plist.root.CreateArray("CFBundleLocalizations");
		CFBundleLocalizations.AddString ("en");
		CFBundleLocalizations.AddString ("fr");
		CFBundleLocalizations.AddString ("de");
		CFBundleLocalizations.AddString ("ja");
		CFBundleLocalizations.AddString ("es");


		// le numero de la version
		plist.root.SetString("CFBundleShortVersionString", PlayerSettings.bundleVersion);

		// le numero du build
		plist.root.SetString("CFBundleVersion", DateTime.Now.ToString("yyyyMMddHHmmss"));

		// le nom du bundle
		plist.root.SetString("CFBundleName", "Discovering Colors");

		// plus d'infos
		plist.root.SetString("CFBundleGetInfoString", "Discovering Colors "+PlayerSettings.bundleVersion+" (c) Frogames. All rights reserved.");

		// la signature (4 lettres)
		plist.root.SetString("CFBundleSignature", "FROG");

		// la categorie principale du jeu
		plist.root.SetString("LSApplicationCategoryType", "public.app-category.kids-games");


		// uniquement pour la version MacAppStore
		#if MACAPPSTORE

			// version particuliere du CFBundleIdentifier
			plist.root.SetString("CFBundleIdentifier", "com.frogames.coloringanimalsmac");

		#endif



    plist.WriteToFile(plistPath);


	}
}
