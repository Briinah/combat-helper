MANIFEST_PATH="$APPCENTER_SOURCE_DIRECTORY/CombatHelper/CombatHelper.Android/Properties/AndroidManifest.xml"
VERSION_CODE=$((APPCENTER_BUILD_ID))
sed -i "" 's/android:versionCode="[^"]*"/android:versionCode="'$VERSION_CODE'"/' $MANIFEST_PATH
echo "Set versionCode to: '$VERSION_CODE'"