using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;





partial class CharacterCreator
{
  Vector2 scrollPos;

  void OnGUI()
  {
    scrollPos = EditorGUILayout.BeginScrollView(scrollPos, false, false);
    EditorGUILayout.BeginVertical("box");

    #region --Preview--
    if (characterCreated != null){
      EditorGUILayout.BeginVertical("box");
      GUILayout.Label("Character preview:");
      GUILayout.Space(10f);


      //characterCreated = (GameObject)EditorGUILayout.ObjectField(characterCreated, typeof(GameObject), true);

      GUIStyle bgColor = new GUIStyle();
      bgColor.normal.background = Texture2D.blackTexture;

      if (characterCreated != null)
      {
        if (gameObjectEditor == null)
        {
          gameObjectEditor = Editor.CreateEditor(characterCreated);
        }

        gameObjectEditor.OnInteractivePreviewGUI(GUILayoutUtility.GetRect(256, 256), bgColor);
      }
      EditorGUILayout.EndVertical();

      GUILayout.Space(10f);
    }


    #endregion


    #region --Character name--
    if (baseCreated)
    {
      EditorGUILayout.BeginHorizontal("box");
      GUILayout.Label("Character Name");
      characterCreated.name = GUILayout.TextField(characterCreated.name, 40 );
      GUILayout.EndHorizontal();
    }
    #endregion

    #region --Create Character--

    

    if (!baseCreated)
    {
      EditorGUILayout.BeginVertical("box");

      GUILayout.Label("Select base character to start");
      EditorGUILayout.Separator();
      chaGenre =  (Genre)EditorGUILayout.EnumPopup("Select character base:", chaGenre);

      GUILayout.EndVertical();

      EditorGUILayout.BeginVertical("box");
      GUILayout.Label("Use mecanim rig to use your own mecanim animations");
      EditorGUILayout.Separator();

      chaRig =    (Rig)EditorGUILayout.EnumPopup("Select character rig:", chaRig);

      GUILayout.EndVertical();
      EditorGUILayout.BeginVertical("box");
      
      if (GUILayout.Button("Create")){
        CreateBase( chaGenre, chaRig );
      }

      GUILayout.EndVertical();
    }
    else
    {
      EditorGUILayout.BeginVertical("box");

      GUILayout.Label("Character created");
      if (GUILayout.Button(" Delete  "))
        ResetBase();

      GUILayout.EndVertical();
    }

    
    #endregion

    #region --Randomize Character--
    if (baseCreated)
    {
      EditorGUILayout.BeginVertical("box");
      GUILayout.Label("Randomize character");
      if (GUILayout.Button("Randomize")){
        Randomize();
      }
      GUILayout.EndVertical();
    }
    #endregion

  



    #region --Skin--
    if (baseCreated)
    {
      if (!skinImported)
      {
        ImportSkin();
        SelectSkin(0);
      }
      else
      {
        EditorGUILayout.BeginVertical("box");
        GUILayout.Label("Select skin:");
        int skin_Ind = skinIndex;
        skinIndex = EditorGUILayout.Popup(skinIndex, skinList.ToArray());
        if (skin_Ind != skinIndex)
        {
          SelectSkin(skinIndex);
        }
        EditorGUILayout.EndVertical();


      }
    }


    #endregion

    #region --Items--
    if (skinCreated)
    {
      if (!itemsLoaded)
      {
        ImportItems();
      }
      else
      {
        EditorGUILayout.BeginVertical("box");
        GUILayout.Label("Head:");
        EditorGUILayout.BeginHorizontal();

        int he_ind = headIndex;
        headIndex = EditorGUILayout.Popup(headIndex, headList.ToArray());
        if (he_ind != headIndex)
        {
          SelectItem(headIndex, ItemPosition.Head);
          SelectTexture(headGO, 1);
        }

        GUILayout.FlexibleSpace();

        if (headTextures.Count > 1 && headIndex > 0)
        {
          if (GUILayout.Button("Change Texture"))
          {
            SelectTexture(headGO, 1);
          }
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndVertical();


        EditorGUILayout.BeginVertical("box");
        GUILayout.Label("Eyes:");
        EditorGUILayout.BeginHorizontal();

        int e_ind = eyesIndex;
        eyesIndex = EditorGUILayout.Popup(eyesIndex, eyesList.ToArray());
        if (e_ind != eyesIndex)
        {
          SelectItem(eyesIndex, ItemPosition.Eyes);
          SelectTexture(eyesGO, 1);
        }

        GUILayout.FlexibleSpace();

        if (eyesTextures.Count > 1 && eyesIndex > 0)
        {
          if (GUILayout.Button("Change Texture"))
          {
            SelectTexture(eyesGO, 1);
          }
        }

        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginVertical("box");
        GUILayout.Label("Face:");
        EditorGUILayout.BeginHorizontal();

        int fa_ind = faceIndex;
        faceIndex = EditorGUILayout.Popup(faceIndex, faceList.ToArray());
        if (fa_ind != faceIndex)
        {
          SelectItem(faceIndex, ItemPosition.Face);
          SelectTexture(faceGO, 1);
        }

        GUILayout.FlexibleSpace();

        if (faceTextures.Count > 1 && faceIndex > 0)
        {
          if (GUILayout.Button("Change Texture"))
          {
            SelectTexture(faceGO, 1);
          }
        }

        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginVertical("box");
        GUILayout.Label("Top:");
        EditorGUILayout.BeginHorizontal();

        int t_ind = topIndex;
        topIndex = EditorGUILayout.Popup(topIndex, topList.ToArray());
        if (t_ind != topIndex)
        {
          SelectItem(topIndex, ItemPosition.Top);
          SelectTexture(topGO, 1);
        }

        GUILayout.FlexibleSpace();

        if (topTextures.Count > 1 && topIndex > 0)
        {
          if (GUILayout.Button("Change Texture"))
          {
            SelectTexture(topGO, 1);
          }
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginVertical("box");
        GUILayout.Label("Hands:");
        EditorGUILayout.BeginHorizontal();

        int ha_ind = handsIndex;
        handsIndex = EditorGUILayout.Popup(handsIndex, handsList.ToArray());
        if (ha_ind != handsIndex)
        {
          SelectItem(handsIndex, ItemPosition.Hands);
          SelectTexture(handsGO, 1);
        }

        GUILayout.FlexibleSpace();

        if (handsTextures.Count > 1 && handsIndex > 0)
        {
          if (GUILayout.Button("Change Texture"))
          {
            SelectTexture(handsGO, 1);
          }
        }

        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginVertical("box");
        GUILayout.Label("Back:");
        EditorGUILayout.BeginHorizontal();

        int b_ind = backIndex;
        backIndex = EditorGUILayout.Popup(backIndex, backList.ToArray());
        if (b_ind != backIndex)
        {
          SelectItem(backIndex, ItemPosition.Back);
          SelectTexture(backGO, 1);
        }

        GUILayout.FlexibleSpace();

        if (backTextures.Count > 1 && backIndex > 0)
        {
          if (GUILayout.Button("Change Texture"))
          {
            SelectTexture(backGO, 1);
          }
        }

        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginVertical("box");
        GUILayout.Label("Legs:");
        EditorGUILayout.BeginHorizontal();

        int l_ind = legsIndex;
        legsIndex = EditorGUILayout.Popup(legsIndex, legsList.ToArray());
        if (l_ind != legsIndex)
        {
          SelectItem(legsIndex, ItemPosition.Legs);
          SelectTexture(legsGO, 1);
        }

        GUILayout.FlexibleSpace();

        if (legsTextures.Count > 1 && legsIndex > 0)
        {
          if (GUILayout.Button("Change Texture"))
          {
            SelectTexture(legsGO, 1);
          }
        }

        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginVertical("box");
        GUILayout.Label("Footwear:");
        EditorGUILayout.BeginHorizontal();

        int fw_ind = footwearIndex;
        footwearIndex = EditorGUILayout.Popup(footwearIndex, footwearList.ToArray());
        if (fw_ind != footwearIndex)
        {
          SelectItem(footwearIndex, ItemPosition.Footwear);
          SelectTexture(footwearGO, 1);
        }

        GUILayout.FlexibleSpace();

        if (footwearTextures.Count > 1 && footwearIndex > 0)
        {
          if (GUILayout.Button("Change Texture"))
          {
            SelectTexture(footwearGO, 1);
          }
        }

        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndVertical();

      }


    }


    #endregion

    if (skinCreated)
    {
      GUILayout.Space(20f);



      if (GUILayout.Button("Save"))
      {
        if (characterCreated != null)
        {
          SaveCharacter();
          ResetBase();
        }
        else
        {
          Debug.Log("No character created");
        }
      }
    }

    EditorGUILayout.EndVertical();

    GUILayout.FlexibleSpace();
    EditorGUILayout.EndScrollView();
  }




}
