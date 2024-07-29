using UnityEngine;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using UnityEditor;
using System.Threading.Tasks;
#if UNITY_EDITOR
using Sirenix.OdinInspector;
#endif


public partial class BuildToolkitWindow
{
#if UNITY_EDITOR

    [LabelText("构建Addressables"), LabelWidth(30)]
    [Button(ButtonSizes.Medium),  PropertySpace(SpaceBefore = 30, SpaceAfter = 30)]
    [InlineEditor(InlineEditorModes.LargePreview)]
    public void BuildAddressables()
    {
    }

    [LabelText("构建热更DLL"), LabelWidth(30)]
    [Button(ButtonSizes.Medium), PropertySpace(SpaceBefore = 30, SpaceAfter = 30)]
    [InlineEditor(InlineEditorModes.LargePreview)]
    public void BuildHotDll()
    {
    }


    [LabelText("复制热更DLL到StreamingAssets"), LabelWidth(30)]
    [Button(ButtonSizes.Medium),  PropertySpace(SpaceBefore = 30, SpaceAfter = 30)]
    [InlineEditor(InlineEditorModes.LargePreview)]
    public void CopyHotDll()
    {
        string sourcePath = "HybridCLRData/HotUpdateDlls/StandaloneWindows64/HotGame.dll";
        string destinationPath = "Assets/StreamingAssets/HotGame.dll.bytes";

        // 获取绝对路径
        string fullPathSource = Path.GetFullPath(sourcePath);
        string fullPathDestination = Path.GetFullPath(destinationPath);

        // 检查源文件是否存在
        if (File.Exists(fullPathSource))
        {
            // 创建目标文件夹（如果不存在）
            Directory.CreateDirectory(Path.GetDirectoryName(fullPathDestination));

            // 如果目标文件存在，删除它
            if (File.Exists(fullPathDestination))
            {
                File.Delete(fullPathDestination);
            }

            // 复制并重命名文件
            File.Copy(fullPathSource, fullPathDestination);
            Debug.Log("File copied and renamed successfully.");
        }
        else
        {
            Debug.LogError("Source file does not exist: " + fullPathSource);
        }
    }

    [LabelText("复制热更DLL到生成目录"), LabelWidth(30)]
    [Button(ButtonSizes.Medium), PropertySpace(SpaceBefore = 30, SpaceAfter = 30)]
    [InlineEditor(InlineEditorModes.LargePreview)]
    public void CopyHotDllToTarget()
    {
        string sourcePath = "HybridCLRData/HotUpdateDlls/StandaloneWindows64/HotGame.dll";
        string destinationPath = "Output/Drizzle_Data/StreamingAssets/HotGame.dll.bytes";

        // 获取绝对路径
        string fullPathSource = Path.GetFullPath(sourcePath);
        string fullPathDestination = Path.GetFullPath(destinationPath);

        // 检查源文件是否存在
        if (File.Exists(fullPathSource))
        {
            // 创建目标文件夹（如果不存在）
            Directory.CreateDirectory(Path.GetDirectoryName(fullPathDestination));

            // 如果目标文件存在，删除它
            if (File.Exists(fullPathDestination))
            {
                File.Delete(fullPathDestination);
            }

            // 复制并重命名文件
            File.Copy(fullPathSource, fullPathDestination);
            Debug.Log("File copied and renamed successfully.");
        }
        else
        {
            Debug.LogError("Source file does not exist: " + fullPathSource);
        }
    }
#endif
}