using System;
using UnityEngine;
using DivineSkies.Modules.UI;

namespace DivineSkies.Modules.Popups
{
    /// <summary>
    /// Use this Popup to display a set of Items
    /// </summary>
    public class GenericSelectionPopup : NotificationPopup
    {
        [SerializeField] private Transform _selectionParent;

        public void Init<TItem, TData>(string title, string content, TData[] datas, Action<TItem, TData> onSetup, Action onClose = null, bool openAfterwards = true) where TItem : UiItemBase
        {
            Init(title, content, openAfterwards, onClose);

            PooledListDisplay<TItem, TData> list = new PooledListDisplay<TItem, TData>(_selectionParent, onSetup);
            list.SetData(datas);
        }
    }
}