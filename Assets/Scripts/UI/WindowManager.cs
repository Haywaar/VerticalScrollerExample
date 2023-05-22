using System;
using System.Collections.Generic;
using UI.Dialogs;
using UnityEngine;

namespace UI
{
    public class WindowManager
    {
        private const string PrefabsFilePath = "Dialogs/";
        
        // При создании новых окон добавлять их сюда
        private static readonly Dictionary<Type, string> PrefabsDictionary = new Dictionary<Type, string>()
        {
            {typeof(YouLoseDialog),"YouLoseDialog"},
            {typeof(YouWinDialog),"YouWinDialog"},
            {typeof(SelectLevelDialog),"SelectLevelDialog"},
            {typeof(SettingsDialog),"SettingsDialog"},
            {typeof(CustomizeShipDialog),"CustomizeShipDialog"},
            {typeof(PurchaseItemDialog),"PurchaseItemDialog"},
            {typeof(MessageDialog),"MessageDialog"},
            {typeof(ScoreTableDialog),"ScoreTableDialog"},
        };

        public static void ShowWindow<T>() where T : Window
        {
            CreateWindow<T>();
        }

        public static T GetWindow<T>() where T : Window
        {
            var obj = CreateWindow<T>();
            var component = obj.GetComponent<T>();
        
            return component;
        }
        
        private static GameObject CreateWindow<T>() where T : Window
        {
            var go = GetPrefabByType<T>();
            if (go == null)
            {
                Debug.LogError("Show window - object not found");
                return null;
            }

            return GameObject.Instantiate(go, GuiHolder);
        }

        private static GameObject GetPrefabByType<T>() where T : Window
        {
            var prefabName =  PrefabsDictionary[typeof(T)];
            if (string.IsNullOrEmpty(prefabName))
            {
                Debug.LogError("cant find prefab type of " + typeof(T) + "Do you added it in PrefabsDictionary?");
            }

            var path = PrefabsFilePath + PrefabsDictionary[typeof(T)];
            var windowGO = Resources.Load<GameObject>(path);
            if (windowGO == null)
            {
                Debug.LogError("Cant find prefab at path " + path);
            }

            return windowGO;
        }
    
        public static Transform GuiHolder
        {
            get { return ServiceLocator.Current.Get<GUIHolder>().transform; }
        }
    }
}