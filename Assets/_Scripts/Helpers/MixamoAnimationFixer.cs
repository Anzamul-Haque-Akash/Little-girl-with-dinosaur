using UnityEditor;

public class MixamoAnimationFixer
{
    [MenuItem("Lulu/Fix Rig & Animation")]
    public static void FixRigAndAnimation()
    {
        if (Selection.assetGUIDs is { Length: > 0 })
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(Selection.assetGUIDs[0]);
            
            ModelImporter modelImporter = ModelImporter.GetAtPath(assetPath) as ModelImporter;
            modelImporter.animationType = ModelImporterAnimationType.Human;
            modelImporter.avatarSetup = ModelImporterAvatarSetup.CreateFromThisModel;
            
            AssetDatabase.WriteImportSettingsIfDirty(assetPath);
            AssetDatabase.ImportAsset(assetPath, ImportAssetOptions.ForceUpdate);
            
            ModelImporterClipAnimation[] animations = modelImporter.defaultClipAnimations;

            ModelImporterClipAnimation animation = animations[0];
            
            animation.loopTime = true;
            animation.lockRootRotation = true;
            animation.lockRootHeightY = true;
            animation.lockRootPositionXZ = true;
            animation.keepOriginalOrientation = true;
            animation.keepOriginalPositionY = true;
            animation.keepOriginalPositionXZ = true;
            modelImporter.clipAnimations = animations;
            
            AssetDatabase.WriteImportSettingsIfDirty(assetPath);
            AssetDatabase.ImportAsset(assetPath, ImportAssetOptions.ForceUpdate);
        }
    }
}
