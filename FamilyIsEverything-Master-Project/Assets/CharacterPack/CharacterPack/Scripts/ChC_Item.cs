using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public enum ItemPosition { Head, Eyes, Face, Top, Hands, Back, Legs, Footwear };

public enum GenreItem { Common, Female, Male };

[ExecuteInEditMode]
public class ChC_Item : MonoBehaviour
{


  public ItemPosition itemPos;
  public GenreItem genreItem;

  private GameObject item;
  public List<Texture> textures;
  private Renderer itemRenderer;

  private int currentTexture;

  public int CurrentTexture
  {
    get
    {
      return currentTexture;
    }

    set
    {
      currentTexture = value;
      if (currentTexture > (textures.Count - 1))
        currentTexture = 0;
    }
  }




  // Use this for initialization
  //   void OnEnable () {
  //       if (textures != null)
  //       {
  //           for (int i = 0; i < textures.Count; i++)
  //           {
  //               textures[i].name = transform.name + "_" + i;
  //           }
  //       }
  //}

  public void EliminateItem()
  {
    if (gameObject != null)
    {
      AssetDatabase.DeleteAsset( "Assets/CharacterPack/CharacterPack/Resources/Materials/Temp/" + GetComponentInChildren<Renderer>().sharedMaterial.name + ".mat" );
      //AssetDatabase.Refresh();
      DestroyImmediate(gameObject);
    }
  }

  //private void OnDestroy()
  //{
  //  //AssetDatabase.DeleteAsset( "Assets/CharacterPack/CharacterPack/Resources/Materials/Temp/" + GetComponentInChildren<Renderer>().sharedMaterial.name + ".mat" );
  //  //AssetDatabase.Refresh();
  //}
}
