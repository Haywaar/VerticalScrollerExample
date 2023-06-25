using System;
using System.Collections.Generic;
using UI.Dialogs;
using UnityEngine;

namespace UI
{
    public class DialogManager
    {
        private const string PrefabsFilePath = "Dialogs/";
        
        // При создании новых окон добавлять их сюда
        private static readonly Dictionary<Type, string> PrefabsDictionary = new Dictionary<Type, string>()
        {
            {typeof(YouLoseDialog),"YouLoseDialog"},
            {typeof(YouWinDialog),"YouWinDialog"},
            {typeof(PurchaseItemDialog),"PurchaseItemDialog"},
            {typeof(MessageDialog),"MessageDialog"},
            {typeof(LoadingDialog),"LoadingDialog"},
            
            {typeof(SelectLevelDialog),"MenuDialogs/SelectLevelDialog"},
            {typeof(MenuDialog),"MenuDialogs/MenuDialog"},
            {typeof(ScoreTableDialog),"MenuDialogs/ScoreTableDialog"},
            {typeof(SettingsDialog),"MenuDialogs/SettingsDialog"},
            {typeof(CustomizeShipDialog),"MenuDialogs/CustomizeShipDialog"},
        };

        public static void ShowDialog<T>() where T : Dialog
        {
            CreateDialog<T>();
        }

        public static T GetDialog<T>() where T : Dialog
        {
            var obj = CreateDialog<T>();
            var component = obj.GetComponent<T>();
        
            return component;
        }
        
        private static GameObject CreateDialog<T>() where T : Dialog
        {
            var go = GetPrefabByType<T>();
            if (go == null)
            {
                Debug.LogError("Show window - object not found");
                return null;
            }

            return GameObject.Instantiate(go, GuiHolder);
        }

        private static GameObject GetPrefabByType<T>() where T : Dialog
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