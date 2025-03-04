using System;
using UnityEngine;
using FEA.UI;

namespace DivineSkies.Modules.Popups
{
    public class GenericSelectionPopup : NotificationPopup
    {
        [SerializeField] private Transform _selectionParent;

        public void Init<TItem, TData>(string title, string content, TData[] datas, Action<TItem, TData> onSetup, Action onClose = null, bool openAfterwards = true) where TItem : UiItemBase
        {
            Init(title, content, onClose, openAfterwards);

            var list = new PooledListDisplay<TItem, TData>(_selectionParent, onSetup);
            list.SetData(datas);
        }
    }
}