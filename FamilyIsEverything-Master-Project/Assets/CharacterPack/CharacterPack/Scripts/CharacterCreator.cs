using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;





public partial class CharacterCreator : EditorWindow
{

  #region Base character
  bool baseCreated;
  GameObject characterCreated;
  GameObject characterCreatedBody;
  public enum Genre { Female, Male };
  public Genre chaGenre;

  public enum Rig { DefaultRig, Mecanim };
  public Rig chaRig;

  #endregion

  #region Skin
  List<string> skinList = new List<string>();
  List<Object> skinMatList = new List<Object>();
  int skinIndex = 0;
  bool skinImported;
  bool skinCreated;
  Material skinMat;
  #endregion

  #region Items
  bool itemsLoaded;

  //Head
  List<string> headList = new List<string> { "none" };
  List<Object> headItemList = new List<Object> { null };
  int headIndex = 0;
  GameObject headGO;
  List<Texture> headTextures = new List<Texture>();

  //Eyes
  List<string> eyesList = new List<string> { "none" };
  List<Object> eyesItemList = new List<Object> { null };
  int eyesIndex = 0;
  GameObject eyesGO;
  List<Texture> eyesTextures = new List<Texture>();


  //Face
  List<string> faceList = new List<string> { "none" };
  List<Object> faceItemList = new List<Object> { null };
  int faceIndex = 0;
  GameObject faceGO;
  List<Texture> faceTextures = new List<Texture>();


  //Top
  List<string> topList = new List<string> { "none" };
  List<Object> topItemList = new List<Object> { null };
  int topIndex = 0;
  GameObject topGO;
  List<Texture> topTextures = new List<Texture>();


  //Hands
  List<string> handsList = new List<string> { "none" };
  List<Object> handsItemList = new List<Object> { null };
  int handsIndex = 0;
  GameObject handsGO;
  List<Texture> handsTextures = new List<Texture>();


  //Back
  List<string> backList = new List<string> { "none" };
  List<Object> backItemList = new List<Object> { null };
  int backIndex = 0;
  GameObject backGO;
  List<Texture> backTextures = new List<Texture>();


  //Legs
  List<string> legsList = new List<string> { "none" };
  List<Object> legsItemList = new List<Object> { null };
  int legsIndex = 0;
  GameObject legsGO;
  List<Texture> legsTextures = new List<Texture>();


  //Footwear
  List<string> footwearList = new List<string> { "none" };
  List<Object> footwearItemList = new List<Object> { null };
  int footwearIndex = 0;
  GameObject footwearGO;
  List<Texture> footwearTextures = new List<Texture>();




  #endregion


  Editor gameObjectEditor;


  [MenuItem("Component/Character Creator")]
  public static void ShowWindow()
  {
    GetWindow<CharacterCreator>(false, "Creator", true);
    //GetWindowWithRect<CharacterCreator>(new Rect(0, 0, 400, 256));

  }


  void ImportSkin()
  {
    Object[] baseTexFolder;

    switch (chaGenre)
    {
      case Genre.Female:
        baseTexFolder = Resources.LoadAll("Base/Materials/Female", typeof(Material));
        break;

      case Genre.Male:
        baseTexFolder = Resources.LoadAll("Base/Materials/Male", typeof(Material));
        break;

      default:
        baseTexFolder = null;
        break;
    }


    if (baseTexFolder != null)
    {
      foreach (Object o in baseTexFolder)
      {
        skinList.Add(o.name);
        skinMatList.Add(o);
      }
      skinImported = true;
    }
  }

  void ImportItems()
  {
    Object[] baseFolder;
    baseFolder = Resources.LoadAll("Items", typeof(GameObject));
    if (baseFolder != null)
    {
      foreach (Object ob in baseFolder)
      {
        GameObject GO = ob as GameObject;
        ItemPosition itemPos = GO.GetComponent<ChC_Item>().itemPos;
        GenreItem genreIt = GO.GetComponent<ChC_Item>().genreItem;

        if (chaGenre == Genre.Female)
        {
          if (genreIt == GenreItem.Female || genreIt == GenreItem.Common)
          {
            switch (itemPos)
            {
              case ItemPosition.Head:
                headList.Add(ob.name);
                headItemList.Add(ob);
                break;

              case ItemPosition.Eyes:
                eyesList.Add(ob.name);
                eyesItemList.Add(ob);
                break;

              case ItemPosition.Face:
                faceList.Add(ob.name);
                faceItemList.Add(ob);
                break;

              case ItemPosition.Top:
                topList.Add(ob.name);
                topItemList.Add(ob);
                break;

              case ItemPosition.Hands:
                handsList.Add(ob.name);
                handsItemList.Add(ob);
                break;

              case ItemPosition.Back:
                backList.Add(ob.name);
                backItemList.Add(ob);
                break;

              case ItemPosition.Legs:
                legsList.Add(ob.name);
                legsItemList.Add(ob);
                break;

              case ItemPosition.Footwear:
                footwearList.Add(ob.name);
                footwearItemList.Add(ob);
                break;

              default:
                break;

            }
          }
        }
        else if (chaGenre == Genre.Male)
        {
          if (genreIt == GenreItem.Male || genreIt == GenreItem.Common)
          {
            switch (itemPos)
            {
              case ItemPosition.Head:
                headList.Add(ob.name);
                headItemList.Add(ob);
                break;

              case ItemPosition.Eyes:
                eyesList.Add(ob.name);
                eyesItemList.Add(ob);
                break;

              case ItemPosition.Face:
                faceList.Add(ob.name);
                faceItemList.Add(ob);
                break;

              case ItemPosition.Top:
                topList.Add(ob.name);
                topItemList.Add(ob);
                break;

              case ItemPosition.Hands:
                handsList.Add(ob.name);
                handsItemList.Add(ob);
                break;

              case ItemPosition.Back:
                backList.Add(ob.name);
                backItemList.Add(ob);
                break;

              case ItemPosition.Legs:
                legsList.Add(ob.name);
                legsItemList.Add(ob);
                break;

              case ItemPosition.Footwear:
                footwearList.Add(ob.name);
                footwearItemList.Add(ob);
                break;

              default:
                break;
            }
          }
        }

      }

      itemsLoaded = true;
    }
  }


  void CreateBase(Genre gen, Rig rig )
  {
    GameObject character = null;

    //GameObject character = Instantiate(baseGO[op]) as GameObject;
    //character.transform.position = Vector3.zero;
    //character.name = baseGO[op].name;
    //characterCreated = character;
    //baseCreated = true;

    switch (gen)
    {
      case Genre.Female:
        if( rig == Rig.DefaultRig ){
          character = Resources.Load("Base/Female", typeof(GameObject)) as GameObject;
        }
        else if( rig == Rig.Mecanim ){
          character = Resources.Load("Base/Female_Mecanim", typeof(GameObject)) as GameObject;
        }
        break;

      case Genre.Male:
        if( rig == Rig.DefaultRig ){
          character = Resources.Load("Base/Male", typeof(GameObject)) as GameObject;
        }
        else if( rig == Rig.Mecanim ){
          character = Resources.Load("Base/Male_Mecanim", typeof(GameObject)) as GameObject;
        }
        break;

      default:
        break;
    }
    // characterCreated = baseGO[op] as GameObject;


    characterCreated      = Instantiate(character);
    characterCreated.name += " " + System.DateTime.Now.ToString("yyyy-MM-dd hh.mm.ss");
    characterCreatedBody  = characterCreated.transform.Find("Body").gameObject;
    baseCreated = true;

    if( gameObjectEditor != null ){
      DestroyImmediate(gameObjectEditor);
      gameObjectEditor = null;
    }
  }

  void SelectSkin(int op)
  {
    if (skinMat == null)
    {
      skinMat = skinMatList[op] as Material;

      characterCreatedBody.GetComponent<SkinnedMeshRenderer>().sharedMaterial = skinMat;
      skinCreated = true;
    }
    else
    {
      skinMat = skinMatList[op] as Material;
      characterCreatedBody.GetComponent<SkinnedMeshRenderer>().sharedMaterial = skinMat;
    }

    if( gameObjectEditor != null ){
      DestroyImmediate(gameObjectEditor);
      gameObjectEditor = null;
    }
  }

  void SelectItem(int index, ItemPosition itemPos)
  {

    switch (itemPos)
    {
      case ItemPosition.Head:
        if (headItemList[index] == null)
        {
          headGO.GetComponent<ChC_Item>().EliminateItem();
        }
        else
        {
          if (headGO != null)
          {
            headGO.GetComponent<ChC_Item>().EliminateItem();
          }

          headGO = headItemList[index] as GameObject;

          headGO = Instantiate(headGO);
          headGO.transform.SetParent(characterCreatedBody.transform);
          LoadTextures(headGO);

        }
        break;

      case ItemPosition.Eyes:
        if (eyesItemList[index] == null)
        {
          eyesGO.GetComponent<ChC_Item>().EliminateItem();
        }
        else
        {
          if (eyesGO != null)
          {
            eyesGO.GetComponent<ChC_Item>().EliminateItem();
          }

          eyesGO = eyesItemList[index] as GameObject;
          eyesGO = Instantiate(eyesGO);
          eyesGO.transform.SetParent(characterCreatedBody.transform);
          LoadTextures(eyesGO);
        }
        break;


      case ItemPosition.Face:
        if (faceItemList[index] == null)
        {
          faceGO.GetComponent<ChC_Item>().EliminateItem();
        }
        else
        {
          if (faceGO != null)
          {
            faceGO.GetComponent<ChC_Item>().EliminateItem();
          }

          faceGO = faceItemList[index] as GameObject;
          faceGO = Instantiate(faceGO);
          faceGO.transform.SetParent(characterCreatedBody.transform);
          LoadTextures(faceGO);
        }
        break;

      case ItemPosition.Top:
        if (topItemList[index] == null)
        {
          topGO.GetComponent<ChC_Item>().EliminateItem();
        }
        else
        {
          if (topGO != null)
          {
            topGO.GetComponent<ChC_Item>().EliminateItem();
          }

          topGO = topItemList[index] as GameObject;
          topGO = Instantiate(topGO);
          topGO.transform.SetParent(characterCreatedBody.transform);
          LoadTextures(topGO);
        }
        break;

      case ItemPosition.Hands:
        if (handsItemList[index] == null)
        {
          handsGO.GetComponent<ChC_Item>().EliminateItem();
        }
        else
        {
          if (handsGO != null)
          {
            handsGO.GetComponent<ChC_Item>().EliminateItem();
          }

          handsGO = handsItemList[index] as GameObject;
          handsGO = Instantiate(handsGO);
          handsGO.transform.SetParent(characterCreatedBody.transform);
          LoadTextures(handsGO);
        }
        break;

      case ItemPosition.Back:
        if (backItemList[index] == null)
        {
          backGO.GetComponent<ChC_Item>().EliminateItem();
        }
        else
        {
          if (backGO != null)
          {
            backGO.GetComponent<ChC_Item>().EliminateItem();
          }

          backGO = backItemList[index] as GameObject;
          backGO = Instantiate(backGO);
          backGO.transform.SetParent(characterCreatedBody.transform);
          LoadTextures(backGO);
        }
        break;

      case ItemPosition.Legs:
        if (legsItemList[index] == null)
        {
          legsGO.GetComponent<ChC_Item>().EliminateItem();
        }
        else
        {
          if (legsGO != null)
          {
            legsGO.GetComponent<ChC_Item>().EliminateItem();
          }

          legsGO = legsItemList[index] as GameObject;
          legsGO = Instantiate(legsGO);
          legsGO.transform.SetParent(characterCreatedBody.transform);
          LoadTextures(legsGO);
        }
        break;

      case ItemPosition.Footwear:
        if (footwearItemList[index] == null)
        {
          footwearGO.GetComponent<ChC_Item>().EliminateItem();
        }
        else
        {
          if (footwearGO != null)
          {
            footwearGO.GetComponent<ChC_Item>().EliminateItem();
          }

          footwearGO = footwearItemList[index] as GameObject;
          footwearGO = Instantiate(footwearGO);
          footwearGO.transform.SetParent(characterCreatedBody.transform);
          LoadTextures(footwearGO);
        }
        break;

      default:
        //Debug.Log("No se ha podido cargar items");
        break;
    }

    if( gameObjectEditor != null ){
      DestroyImmediate(gameObjectEditor);
      gameObjectEditor = null;
    }
  }

  void LoadTextures(GameObject item)
  {
    if (item != null)
    {
      ChC_Item chcItem = item.GetComponent<ChC_Item>();
      List<Texture> ch_hItemList = chcItem.textures;

      switch (chcItem.itemPos)
      {
        case ItemPosition.Head:

          if (headTextures.Count != 0)
            headTextures.RemoveRange(0, (headTextures.Count - 1));


          //Debug.Log(headTextures);

          foreach (Texture tex in ch_hItemList)
          {
            headTextures.Add(tex);
          }
          //Debug.Log(headTextures);
          break;
        case ItemPosition.Eyes:

          if (eyesTextures.Count != 0)
            eyesTextures.RemoveRange(0, (eyesTextures.Count - 1));


          //Debug.Log(eyesTextures);

          foreach (Texture tex in ch_hItemList)
          {
            eyesTextures.Add(tex);
          }
          //Debug.Log(eyesTextures);
          break;

        case ItemPosition.Face:

          if (faceTextures.Count != 0)
            faceTextures.RemoveRange(0, (faceTextures.Count - 1));


          //Debug.Log(faceTextures);

          foreach (Texture tex in ch_hItemList)
          {
            faceTextures.Add(tex);
          }
          //Debug.Log(faceTextures);
          break;

        case ItemPosition.Top:

          if (topTextures.Count != 0)
            topTextures.RemoveRange(0, (topTextures.Count - 1));


          //Debug.Log(topTextures);

          foreach (Texture tex in ch_hItemList)
          {
            topTextures.Add(tex);
          }
          //Debug.Log(topTextures);
          break;

        case ItemPosition.Hands:

          if (handsTextures.Count != 0)
            handsTextures.RemoveRange(0, (handsTextures.Count - 1));


          //Debug.Log(handsTextures);

          foreach (Texture tex in ch_hItemList)
          {
            handsTextures.Add(tex);
          }
          //Debug.Log(handsTextures);
          break;

        case ItemPosition.Back:

          if (backTextures.Count != 0)
            backTextures.RemoveRange(0, (backTextures.Count - 1));


          //Debug.Log(backTextures);

          foreach (Texture tex in ch_hItemList)
          {
            backTextures.Add(tex);
          }
          //Debug.Log(backTextures);
          break;

        case ItemPosition.Legs:

          if (legsTextures.Count != 0)
            legsTextures.RemoveRange(0, (legsTextures.Count - 1));


          //Debug.Log(legsTextures);

          foreach (Texture tex in ch_hItemList)
          {
            legsTextures.Add(tex);
          }
          //Debug.Log(legsTextures);
          break;

        case ItemPosition.Footwear:

          if (footwearTextures.Count != 0)
            footwearTextures.RemoveRange(0, (footwearTextures.Count - 1));


          //Debug.Log(footwearTextures);

          foreach (Texture tex in ch_hItemList)
          {
            footwearTextures.Add(tex);
          }
          //Debug.Log(footwearTextures);
          break;

        default:
          //Debug.Log("Sin texturas");
          break;
      }
    }
    else
    {
      //Debug.Log(item + "es null null");
    }
  }

  void SelectTexture(GameObject go, int indexOffset )
  {
    go.GetComponent<ChC_Item>().CurrentTexture = ( go.GetComponent<ChC_Item>().CurrentTexture + indexOffset ) % go.GetComponent<ChC_Item>().textures.Count;
    int indexTex = go.GetComponent<ChC_Item>().CurrentTexture;
    Texture tex = go.GetComponent<ChC_Item>().textures[indexTex];
    Renderer renderer = go.GetComponentInChildren<Renderer>();

    Material newMaterial = new Material(renderer.sharedMaterial);

    renderer.sharedMaterial = newMaterial;
    renderer.sharedMaterial.mainTexture = tex;
    renderer.sharedMaterial.SetFloat("_Glossiness", 0f);
    renderer.sharedMaterial.SetColor("_Color", Color.white);

    AssetDatabase.CreateAsset(newMaterial, "Assets/CharacterPack/CharacterPack/Resources/Materials/Temp/" + newMaterial.name + ".mat" );    
    //AssetDatabase.Refresh();

    // Update editor
    DestroyImmediate(gameObjectEditor);
    gameObjectEditor = null;
  }

  void Preview()
  {
    //Debug.Log("Crear nuevo");
    //GameObject go = Instantiate(characterCreated);
  }

  void Randomize()
  {
    SelectSkin(Random.Range(0, skinList.Count - 1));

    if (skinCreated)
    {
      if (!itemsLoaded)
      {
        ImportItems();
      }

      for (ItemPosition i = ItemPosition.Head; i <= ItemPosition.Footwear; ++i)
      {
        if(       i == ItemPosition.Head ){
          int randomIndex = Random.Range( 0, headList.Count );
          if( randomIndex != headIndex)
          {
            headIndex = randomIndex;
            SelectItem(headIndex, i);
            if( headGO ){
              SelectTexture(headGO, Random.Range( 0, 10 ) );
            }
          }
        }
        else if(  i == ItemPosition.Eyes ){
          int randomIndex = Random.Range( 0, eyesList.Count );
          if( randomIndex != eyesIndex)
          {
            eyesIndex = randomIndex;
            SelectItem(eyesIndex, i);
            if( eyesGO ){
              SelectTexture(eyesGO, Random.Range( 0, 10 ));
            }
          }
        }
        else if(  i == ItemPosition.Face ){
          int randomIndex = Random.Range( 0, faceList.Count );
          if( randomIndex != faceIndex)
          {
            faceIndex = randomIndex;
            SelectItem(faceIndex, i);
            if( faceGO ){
              SelectTexture(faceGO, Random.Range( 0, 10 ));
            }
          }
        }
        else if(  i == ItemPosition.Top ){
          int randomIndex = Random.Range( 0, topList.Count );
          if( randomIndex != topIndex)
          {
            topIndex = randomIndex;
            SelectItem(topIndex, i);
            if( topGO ){
              SelectTexture(topGO, Random.Range( 0, 10 ));
            }
          }
        }
        else if(  i == ItemPosition.Hands ){
          int randomIndex = Random.Range( 0, handsList.Count );
          if( randomIndex != handsIndex)
          {
            handsIndex = randomIndex;
            SelectItem(handsIndex, i);
            if( handsGO ){
              SelectTexture(handsGO, Random.Range( 0, 10 ));
            }
          }
        }
        else if(  i == ItemPosition.Back ){
          int randomIndex = Random.Range( 0, backList.Count );
          if( randomIndex != backIndex)
          {
            backIndex = randomIndex;
            SelectItem(backIndex, i);
            if( backGO ){
              SelectTexture(backGO, Random.Range( 0, 10 ));
            }
          }
        }
        else if(  i == ItemPosition.Legs ){
          int randomIndex = Random.Range( 0, legsList.Count );
          if( randomIndex != legsIndex)
          {
            legsIndex = randomIndex;
            SelectItem(legsIndex, i);
            if( legsGO ){
              SelectTexture(legsGO, Random.Range( 0, 10 ));
            }
          }
        }
        else if(  i == ItemPosition.Footwear ){
          int randomIndex = Random.Range( 0, footwearList.Count );
          if( randomIndex != footwearIndex)
          {
            footwearIndex = randomIndex;
            SelectItem(footwearIndex, i);
            if( footwearGO ){
              SelectTexture(footwearGO, Random.Range( 0, 10 ));
            }
          }
        }
        else
        {
          Debug.Log("Unknown item");
        }
      }
    }
  }

  void ResetBase()
  {
    baseCreated = false;
    skinImported = false;
    gameObjectEditor = null;
    skinMat = null;
    skinList.Clear();
    skinMatList.Clear();
    skinIndex = 0;
    skinImported = false;
    skinCreated = false;
    itemsLoaded = false;
    DestroyImmediate(characterCreated);
    characterCreatedBody = null;
    ResetItems();
    //Debug.Log("Reset");


  }

  void SaveCharacter()
  {
    // Create prefab  with object
    GameObject prefab = PrefabUtility.CreatePrefab( "Assets/CharacterPack/CharacterPack/Prefabs/" + characterCreated.name + ".prefab" , characterCreated, ReplacePrefabOptions.ConnectToPrefab );

    EditorUtility.SetDirty( characterCreated );
    EditorUtility.SetDirty( prefab );

    SaveMaterials();

    characterCreated = null;
    characterCreatedBody = null;
  }

  void SaveMaterials()
  {
    if( headGO != null ){                                                                                                            
      Renderer renderer   = headGO.GetComponentInChildren<Renderer>();
      string materialName = renderer.sharedMaterial.name + ".mat";
      string assetName    = "Assets/CharacterPack/CharacterPack/Resources/Materials/Temp/" + materialName ;
      string newName      = characterCreated.name + "_" + materialName;
      AssetDatabase.RenameAsset( assetName, newName );
      AssetDatabase.MoveAsset( "Assets/CharacterPack/CharacterPack/Resources/Materials/Temp/" + newName, "Assets/CharacterPack/CharacterPack/Resources/Materials/" + newName );
    }

    if( eyesGO != null ){                                                                                                            
      Renderer renderer   = eyesGO.GetComponentInChildren<Renderer>();
      string materialName = renderer.sharedMaterial.name + ".mat";
      string assetName    = "Assets/CharacterPack/CharacterPack/Resources/Materials/Temp/" + materialName ;
      string newName      = characterCreated.name + "_" + materialName;
      AssetDatabase.RenameAsset( assetName, newName );
      AssetDatabase.MoveAsset( "Assets/CharacterPack/CharacterPack/Resources/Materials/Temp/" + newName, "Assets/CharacterPack/CharacterPack/Resources/Materials/" + newName );
    }

    if( faceGO != null ){                                                                                                            
      Renderer renderer   = faceGO.GetComponentInChildren<Renderer>();
      string materialName = renderer.sharedMaterial.name + ".mat";
      string assetName    = "Assets/CharacterPack/CharacterPack/Resources/Materials/Temp/" + materialName ;
      string newName      = characterCreated.name + "_" + materialName;
      AssetDatabase.RenameAsset( assetName, newName );
      AssetDatabase.MoveAsset( "Assets/CharacterPack/CharacterPack/Resources/Materials/Temp/" + newName, "Assets/CharacterPack/CharacterPack/Resources/Materials/" + newName );
    }

    if( topGO != null ){                                                                                                            
      Renderer renderer   = topGO.GetComponentInChildren<Renderer>();
      string materialName = renderer.sharedMaterial.name + ".mat";
      string assetName    = "Assets/CharacterPack/CharacterPack/Resources/Materials/Temp/" + materialName ;
      string newName      = characterCreated.name + "_" + materialName;
      AssetDatabase.RenameAsset( assetName, newName );
      AssetDatabase.MoveAsset( "Assets/CharacterPack/CharacterPack/Resources/Materials/Temp/" + newName, "Assets/CharacterPack/CharacterPack/Resources/Materials/" + newName );
    }

    if( handsGO != null ){                                                                                                            
      Renderer renderer   = handsGO.GetComponentInChildren<Renderer>();
      string materialName = renderer.sharedMaterial.name + ".mat";
      string assetName    = "Assets/CharacterPack/CharacterPack/Resources/Materials/Temp/" + materialName ;
      string newName      = characterCreated.name + "_" + materialName;
      AssetDatabase.RenameAsset( assetName, newName );
      AssetDatabase.MoveAsset( "Assets/CharacterPack/CharacterPack/Resources/Materials/Temp/" + newName, "Assets/CharacterPack/CharacterPack/Resources/Materials/" + newName );
    }

    if( backGO != null ){                                                                                                            
      Renderer renderer   = backGO.GetComponentInChildren<Renderer>();
      string materialName = renderer.sharedMaterial.name + ".mat";
      string assetName    = "Assets/CharacterPack/CharacterPack/Resources/Materials/Temp/" + materialName ;
      string newName      = characterCreated.name + "_" + materialName;
      AssetDatabase.RenameAsset( assetName, newName );
      AssetDatabase.MoveAsset( "Assets/CharacterPack/CharacterPack/Resources/Materials/Temp/" + newName, "Assets/CharacterPack/CharacterPack/Resources/Materials/" + newName );
    }

    if( legsGO != null ){                                                                                                            
      Renderer renderer   = legsGO.GetComponentInChildren<Renderer>();
      string materialName = renderer.sharedMaterial.name + ".mat";
      string assetName    = "Assets/CharacterPack/CharacterPack/Resources/Materials/Temp/" + materialName ;
      string newName      = characterCreated.name + "_" + materialName;
      AssetDatabase.RenameAsset( assetName, newName );
      AssetDatabase.MoveAsset( "Assets/CharacterPack/CharacterPack/Resources/Materials/Temp/" + newName, "Assets/CharacterPack/CharacterPack/Resources/Materials/" + newName );
    }

    if( footwearGO != null ){                                                                                                            
      Renderer renderer   = footwearGO.GetComponentInChildren<Renderer>();
      string materialName = renderer.sharedMaterial.name + ".mat";
      string assetName    = "Assets/CharacterPack/CharacterPack/Resources/Materials/Temp/" + materialName ;
      string newName      = characterCreated.name + "_" + materialName;
      AssetDatabase.RenameAsset( assetName, newName );
      AssetDatabase.MoveAsset( "Assets/CharacterPack/CharacterPack/Resources/Materials/Temp/" + newName, "Assets/CharacterPack/CharacterPack/Resources/Materials/" + newName );
    }

    AssetDatabase.Refresh();
    AssetDatabase.SaveAssets();
  }


  void ResetItems()
  {
    headGO = null;
    headIndex = 0;
    headTextures.RemoveRange(0, (headTextures.Count));
    headList.RemoveRange(1, (headList.Count - 1));
    headItemList.RemoveRange(1, (headItemList.Count - 1));


    eyesGO = null;
    eyesIndex = 0;
    eyesTextures.RemoveRange(0, (eyesTextures.Count));
    eyesList.RemoveRange(1, (eyesList.Count - 1));
    eyesItemList.RemoveRange(1, (eyesItemList.Count - 1));

    faceGO = null;
    faceIndex = 0;
    faceTextures.RemoveRange(0, (faceTextures.Count));
    faceList.RemoveRange(1, (faceList.Count - 1));
    faceItemList.RemoveRange(1, (faceItemList.Count - 1));

    topGO = null;
    topIndex = 0;
    topTextures.RemoveRange(0, (topTextures.Count));
    topList.RemoveRange(1, (topList.Count - 1));
    topItemList.RemoveRange(1, (topItemList.Count - 1));

    handsGO = null;
    handsIndex = 0;
    handsTextures.RemoveRange(0, (handsTextures.Count));
    handsList.RemoveRange(1, (handsList.Count - 1));
    handsItemList.RemoveRange(1, (handsItemList.Count - 1));

    backGO = null;
    backIndex = 0;
    backTextures.RemoveRange(0, (backTextures.Count));
    backList.RemoveRange(1, (backList.Count - 1));
    backItemList.RemoveRange(1, (backItemList.Count - 1));

    legsGO = null;
    legsIndex = 0;
    legsTextures.RemoveRange(0, (legsTextures.Count));
    legsList.RemoveRange(1, (legsList.Count - 1));
    legsItemList.RemoveRange(1, (legsItemList.Count - 1));

    footwearGO = null;
    footwearIndex = 0;
    footwearTextures.RemoveRange(0, (footwearTextures.Count));
    footwearList.RemoveRange(1, (footwearList.Count - 1));
    footwearItemList.RemoveRange(1, (footwearItemList.Count - 1));

  }

  void OnDestroy()
  {
    if (gameObjectEditor != null)
    {
      DestroyImmediate(gameObjectEditor);
      gameObjectEditor = null;
    }
  }

  void OnDisable()
  {
    if (gameObjectEditor != null)
    {
      DestroyImmediate(gameObjectEditor);
      gameObjectEditor = null;
    }
  }

  void OnApplicationQuit ()
  {
    if( gameObjectEditor != null ){
      DestroyImmediate(gameObjectEditor);
      gameObjectEditor = null;
    }
  }
}
