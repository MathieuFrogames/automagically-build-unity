#!/bin/sh


# on se place au niveau du script actuel
cd "`dirname "$0"`"

_pwd="$(pwd)"

echo "====================================="
echo "BUILDING DISCOVERING COLORS - ANIMALS"
echo "====================================="



# on fait le menage
rm -R ./builds/ios/
rm -R ./builds/mac_drmfree/
rm -R ./builds/mac_steam/
rm -R ./builds/mac_macappstore/
rm -R ./builds/pc_drmfree/
rm -R ./builds/pc_steam/
mkdir builds/ios
mkdir builds/mac_drmfree
mkdir builds/mac_steam
mkdir builds/mac_macappstore
mkdir builds/pc_drmfree
mkdir builds/pc_drmfree/Discovering\ Colors
mkdir builds/pc_steam
echo "./builds/ - Cleanup OK"


# on lance la construction des builds depuis Unity
/Applications/Unity/Unity.app/Contents/MacOS/Unity \
	-batchmode \
	-projectPath "$_pwd"/Discovering\ Colors\ -\ Animals \
	-executeMethod DoBuild.PerformBuild \
	-logFile build.log \
	-quit
echo "Builds - OK"



echo "================================"
echo "Build iOS"
# rien a faire...
echo "iOS - OK"



echo "================================"
echo "Build MAC - DRM FREE"
# on zip et on supprime le dossier
cd ./builds/mac_drmfree/
zip -qr Discovering\ Colors\ Mac.zip Discovering\ Colors.app
cd "$_pwd"
rm -R ./builds/mac_drmfree/Discovering\ Colors.app
echo "mac_drmfree - OK"



echo "================================"
echo "Build MAC - STEAM"
# rien a faire...
echo "mac_steam - OK"


echo "================================"
echo "Build MAC - MAC APP STORE"
# infos : https://gentlymad.org/blog/post/deliver-mac-store-unity
cd "$_pwd"
# [fait en postProcess maintenant] cp ./builds_elements/mac_macappstore/Info.plist ./builds/mac_macappstore/Discovering\ Colors.app/Contents
cp ./builds_elements/mac_macappstore/PlayerIcon.icns ./builds/mac_macappstore/Discovering\ Colors.app/Contents/Resources
cp ./builds_elements/mac_macappstore/UnityPlayerIcon.png ./builds/mac_macappstore/Discovering\ Colors.app/Contents/Resources
rm -R ./builds/mac_macappstore/Discovering\ Colors.app/Contents/Plugins/CSteamworks.bundle
rm ./builds/mac_macappstore/Discovering\ Colors.app/Contents/Plugins/libCSteamworks.so
rm ./builds/mac_macappstore/Discovering\ Colors.app/Contents/Plugins/libsteam_api.so
chmod -R 777 ./builds/mac_macappstore/Discovering\ Colors.app/

codesign -f -v --deep -s "3rd Party Mac Developer Application: XXXXXXXXX (YYYYYYYY)" --entitlements ./builds_elements/mac_macappstore/Discovering\ Colors.entitlements ./builds/mac_macappstore/Discovering\ Colors.app
codesign --display --entitlements - ./builds/mac_macappstore/Discovering\ Colors.app

productbuild --component ./builds/mac_macappstore/Discovering\ Colors.app /Applications --sign "3rd Party Mac Developer Installer: XXXXXXXXX (YYYYYYYY)" ./builds/mac_macappstore/Discovering\ Colors.pkg
codesign -v --verify ./builds/mac_macappstore/Discovering\ Colors.app




echo "================================"
echo "Build PC - DRM FREE"
# on fait le menage, on zip et on supprime le dossier
rm ./builds/pc_drmfree/Discovering\ Colors/steam_api.dll
rm ./builds/pc_drmfree/Discovering\ Colors/*.pdb
cd ./builds/pc_drmfree/
zip -qr Discovering\ Colors\ PC.zip Discovering\ Colors
cd "$_pwd"
rm -R ./builds/pc_drmfree/Discovering\ Colors
echo "pc_drmfree - OK"


echo "================================"
echo "Build PC - STEAM"
# on fait le menage, on zip et on supprime le dossier
rm ./builds/pc_steam/*.pdb
echo "pc_steam - OK"



# upload STEAM
#./steamcmd.sh +login xxxxxx yyyyyyy +run_app_build ../scripts/app_build_00000.vdf +quit


exit 0
