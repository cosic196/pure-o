/*=================== Replace with Prefab ===================
 
Unity Forum Community Thread https://forum.unity.com/threads/replace-game-object-with-prefab.24311/
 
Tested in 2018.4.19f1, 2019.3.6f1
Should work in pre-2018.3 versions with old prefab workflow (Needs testing)
 
Changelog and contributors:
 
v1.0.0 (2010-03) Original CopyComponents by Michael L. Croswell for Colorado Game Coders, LLC
v1.1.0 (2011-06) by Kristian Helle Jespersen
v1.2.0 (2015-04) by Connor Cadellin McKee for Excamedia
v1.3.0 (2015-04) by Fernando Medina (fermmmm)
v1.4.0 (2015-07) by Julien Tonsuso (www.julientonsuso.com)
v1.5.0 (2017-06) by Alex Dovgodko
                 Changed into editor window and added instant preview in scene view
v1.6.0 (2018-03) by ???
                 Made changes to make things work with Unity 5.6.1
v1.7.0 (2018-05) by Carlos Diosdado (hypertectonic)
                 Added link to community thread, booleans to choose if scale and rotation are applied, mark scene as dirty, changed menu item
v1.8.0 (2018-??) by Virgil Iordan
                 Added KeepPlaceInHierarchy
v1.9.0 (2019-01) by Dev Bye-A-Jee, Sanjay Sen & Nick Rodriguez for Ravensbourne University London
                 Added unique numbering identifier in the hierarchy to each newly instantiated prefab, also accounts for existing numbers
v2.0.0 (2020-03-22) by Zan Kievit
                    Made compatible with the new Prefab system of Unity 2018. Made more user friendly and added undo.
v2.1.0 (2020-03-22) by Carlos Diosdado (hypertectonic)
                    Added options to use as a utility window (show from right click in the hierarchy), min/max window size,
                    backwards compatibility for old prefab system, works with prefabs selected in project browser, fixed not replacing prefabs,
                    added version numbers, basic documentation, Community namespace to avoid conflicts, cleaned up code for readability.
                   
Known Errors: Rename - numbering doesn't work (since?)
              Rename - throws error when prefab has multiple spaces in their name
 
============================================================*/

using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using System.Collections;
using System.Collections.Generic;

namespace Community
{
    /// <summary>
    /// An editor tool to replace selected GameObjects with a specified Prefab.
    /// </summary>
    public class ReplaceWithPrefab : EditorWindow
    {
        public GameObject prefab = null;
        public List<GameObject> objectsToReplace = new List<GameObject>();
        public List<GameObject> tempObjects = new List<GameObject>();
        public bool editMode = false;
        public bool keepOriginalNames = false;
        public bool applyRotation = true;
        public bool applyScale = true;
        public bool keepPlaceInHierarchy = true;

        private Vector2 windowMinSize = new Vector2(450, 300);
        private Vector2 windowMaxSize = new Vector2(800, 1000);
        private Vector2 scrollPosition;

        /// <summary>
        /// Gets or creates a new Replace with Prefab window.
        /// </summary>
        [MenuItem("Tools/Community/Replace with Prefab")]
        [MenuItem("GameObject/Replace with Prefab", false, 0)]
        static void StartWindow()
        {
            ReplaceWithPrefab window = (ReplaceWithPrefab)GetWindow(typeof(ReplaceWithPrefab));
            window.Show();

            window.titleContent = new GUIContent("Replace with Prefab");
            window.minSize = window.windowMinSize;
            window.maxSize = window.windowMaxSize;
        }

        /// <summary>
        /// Handles getting the selected objects when the selection changes.
        /// </summary>
        void OnSelectionChange()
        {
            GetSelection();
            Repaint();
        }

        /// <summary>
        /// Draws the window content: object list, configuration and execution buttons.
        /// </summary>
        void OnGUI()
        {
            #region Draw Top Buttons
            GUILayout.BeginHorizontal(EditorStyles.toolbar);
            {
                editMode = GUILayout.Toggle(editMode, new GUIContent("Edit", "Start using this feature"), EditorStyles.toolbarButton);
                GUILayout.FlexibleSpace();
                keepOriginalNames = GUILayout.Toggle(keepOriginalNames, "Keep names", EditorStyles.toolbarButton);
                applyRotation = GUILayout.Toggle(applyRotation, "Apply rotation", EditorStyles.toolbarButton);
                applyScale = GUILayout.Toggle(applyScale, "Apply scale", EditorStyles.toolbarButton);
                keepPlaceInHierarchy = GUILayout.Toggle(keepPlaceInHierarchy, "Keep Place In Hierarchy", EditorStyles.toolbarButton);
            }
            GUILayout.EndHorizontal();
            #endregion

            if (GUI.changed)
            {
                if (editMode)
                    GetSelection();
                else
                    ResetPreview();
            }

            GUILayout.Space(5);
            if (editMode)
            {
                ResetPreview();

                #region Draw Prefab and List
                if (prefab != null)
                {
                    GUILayout.Label("Prefab: ", EditorStyles.boldLabel);
                    GUILayout.Label(prefab.name);
                    GUILayout.Space(10);

                    if (objectsToReplace.Count > 0)
                    {
                        GUILayout.Label(new GUIContent("Objects to be Replaced:", "Multi-select all the objects you want to replace with your Prefab"), EditorStyles.boldLabel);
                        objectsToReplace.Sort(NameCompare);

                        scrollPosition = GUILayout.BeginScrollView(scrollPosition, EditorStyles.helpBox);
                        {
                            foreach (GameObject go in objectsToReplace)
                            {
                                GUILayout.BeginHorizontal(EditorStyles.toolbar);
                                GUILayout.Label(go.name);
                                GUILayout.EndHorizontal();
                            }
                            GUILayout.Space(2);
                        }
                        GUILayout.EndScrollView();
                    }
                    else
                    {
                        GUILayout.Label(new GUIContent("Multi-select all the objects you want to replace with your Prefab"), EditorStyles.boldLabel);
                    }
                }
                else
                {
                    GUILayout.Label("Select a Prefab to replace objects with", EditorStyles.boldLabel);
                }
                #endregion

                #region Draw Bottom Buttons
                GUILayout.Space(5);
                GUILayout.BeginHorizontal();
                {
                    if (prefab != null && objectsToReplace.Count > 0)
                    {
                        if (GUILayout.Button("Apply"))
                        {
                            if (!keepOriginalNames)
                            {
                                Rename();
                            }
                            foreach (GameObject go in objectsToReplace)
                            {
                                Replace(go);
                                DestroyImmediate(go);
                            }
                            editMode = false;
                            EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene()); // Important so that we don't forget to save!
                        }
                        else if (GUILayout.Button("Cancel"))
                        {
                            ResetPreview();
                            editMode = false;
                        }
                    }
                }
                GUILayout.EndHorizontal();
                #endregion
            }
            else
            {
                objectsToReplace.Clear();
                tempObjects.Clear();
                prefab = null;
            }
        }

        /// <summary>
        /// Renames the gameObjects, adding numbering following the default Unity naming convention i.e. "Cube (1)".
        /// It checks for already used numbers.
        /// Finds the "space" character in the name to identify where the number is, then strips the brackets.
        /// </summary>
        void Rename()
        {
            int count = 0;
            List<int> ExistingNumbers = new List<int>();

            foreach (GameObject obj in Object.FindObjectsOfType(typeof(GameObject)))
            {
                if (obj.name.Contains(prefab.name))
                {
                    string[] stringSplit = obj.name.Split(' '); // number deliminator, setup for default Unity naming convention i.e "Cube (1)"
                    if (stringSplit.Length > 1)
                    {
                        char[] charsToTrim = { '(', ')' }; // extra characters to trim
                        string num = stringSplit[1].Trim(charsToTrim); // substring which contains number
                        int x = int.Parse(num); // convert string to number
                        ExistingNumbers.Add(x);
                    }
                }
            }

            foreach (GameObject go in tempObjects)
            {
                count++;
                if (ExistingNumbers.Count > 0)
                {
                    int i = 0;
                    while (i < (ExistingNumbers.Count))
                    {
                        if (count == (ExistingNumbers[i]))
                        {
                            count++;
                            i = 0;
                        }
                        else
                        {
                            i++;
                        }
                    }
                }
                go.transform.name = go.transform.name + " (" + count + ")";
            }
        }

        /// <summary>
        /// Replaces a given gameObject with a previously chosen prefab.
        /// </summary>
        /// <param name="obj">The gameObject to replace.</param>
        void Replace(GameObject obj)
        {
            GameObject newObject;

#if UNITY_2018_2_OR_NEWER
            newObject = PrefabUtility.InstantiatePrefab(PrefabUtility.GetCorrespondingObjectFromSource(prefab)) as GameObject;
#else
            newObject = PrefabUtility.InstantiatePrefab(PrefabUtility.GetPrefabParent(prefab)) as GameObject;
#endif

            if (newObject == null) // if the prefab is chosen from the project browser and not the hierarchy, it is null
            {
                newObject = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
            }

            newObject.transform.SetParent(obj.transform.parent, true);

            newObject.transform.localPosition = obj.transform.localPosition;

            if (applyRotation)
            {
                newObject.transform.localRotation = obj.transform.localRotation;
            }

            if (applyScale)
            {
                newObject.transform.localScale = obj.transform.localScale;
            }

            tempObjects.Add(newObject);

            if (keepOriginalNames)
            {
                newObject.transform.name = obj.transform.name;
            }

            if (keepPlaceInHierarchy)
            {
                newObject.transform.SetSiblingIndex(obj.transform.GetSiblingIndex());
            }

            Undo.RegisterCreatedObjectUndo(newObject, "Replaced Objects");
            Undo.DestroyObjectImmediate(obj);
        }

        /// <summary>
        /// Gets the currently selected game objects.
        /// </summary>
        void GetSelection()
        {
            if (editMode && Selection.activeGameObject != null)
            {
                if (prefab == null) // get the prefab 1st
                {
#if UNITY_2018_3_OR_NEWER
                    PrefabAssetType t = PrefabUtility.GetPrefabAssetType(Selection.activeGameObject);

                    if (t == PrefabAssetType.Regular || t == PrefabAssetType.Variant)
                    {
                        prefab = Selection.activeGameObject;
                    }
#else
                    PrefabType t = PrefabUtility.GetPrefabType(Selection.activeGameObject);
 
                    if (t == PrefabType.Prefab)
                    {
                        prefab = Selection.activeGameObject;
                    }
#endif
                }
                else // get the other objects
                {
                    ResetPreview();
                    objectsToReplace.Clear();
                    foreach (var obj in Selection.gameObjects)
                    {
                        objectsToReplace.Add(obj);
                    }
                }
            }
        }

        /// <summary>
        /// Resets the gameObject preview.
        /// </summary>
        void ResetPreview()
        {
            if (tempObjects != null)
            {
                foreach (GameObject go in tempObjects)
                {
                    DestroyImmediate(go);
                }
            }
            tempObjects.Clear();
        }

        /// <summary>
        /// Handles window destruction.
        /// </summary>
        void OnDestroy()
        {
            ResetPreview();
        }

        /// <summary>
        /// Compares the names of two objects ignoring case and returns a signed int (-1 for less than, 0  equals, 1 for greater than).
        /// </summary>
        /// <param name="a">The first object to compare</param>
        /// <param name="b">The second object to compare</param>
        /// <returns></returns>
        int NameCompare(Object a, Object b)
        {
            return new CaseInsensitiveComparer().Compare(a.name, b.name);
        }
    }
}