using System.IO;
using UnityEngine;

public class LoadObjectTools
{
  public static T GetObject<T>(string folderNme, string getNme) where T : UnityEngine.Object
  {
    if (File.Exists(string.Format("{0}/{1}", Application.persistentDataPath, folderNme)))
    {
      AssetBundle assetbundle = AssetBundle.LoadFromFile(string.Format("{0}/{1}", Application.persistentDataPath, folderNme));
      AssetBundleManifest am = assetbundle.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
      string[] assetName = am.GetAllAssetBundles();

      assetbundle.Unload(false);

      assetbundle = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/" + assetName[0]);

      return assetbundle.LoadAsset<T>(getNme);
    }
    else
    {
      T ob = Resources.Load<T>(string.Format("{0}/{1}", folderNme, getNme));
      return ob;
    }
  }

  public static T[] GetObject<T>(string getNme) where T : UnityEngine.Object
  {
    T[] data = Resources.LoadAll<T>(getNme);
    return data as T[];
  }

  public static T GetCreateGetObject<T>(string folderNme, string getNme) where T : UnityEngine.Object
  {
    T t = GetObject<T>(folderNme, getNme);
    return (T)UnityEngine.Object.Instantiate(t);
  }

  public static Sprite GetImage(string path, string name)
  {
    Sprite sprite = null;

    Sprite[] spriteArray = Resources.LoadAll<Sprite>(path);

    for (int i = 0; i < spriteArray.Length; i++)
      if (spriteArray[i].name == name)
        sprite = spriteArray[i];

    return sprite;
  }

  public static Sprite[] GetImage(string path)
  {
    Sprite sprite = null;

    Sprite[] spriteArray = Resources.LoadAll<Sprite>(path);

    return spriteArray;
  }
}