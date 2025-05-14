using System;
using UnityEngine;
using DivineSkies.Modules.UI;

namespace DivineSkies.Modules.Popups
{
    /// <summary>
    /// Use this Popup to display a set of Items
    /// </summary>
    public abstract class GenericSelectionPopup<TItem, TData> : NotificationPopup where TItem : UiItemBase
    {
        [SerializeField] private Transform _selectionParent;

        private PooledListDisplay<TItem, TData> _selectionList;

        protected override void OnCreation()
        {
            base.OnCreation();
            for (int i = 0; i < _selectionParent.childCount; i++)
            {
                Destroy(_selectionParent.GetChild(0).gameObject);
            }

            _selectionList = new PooledListDisplay<TItem, TData>(_selectionParent, SetupSelectionItem);
        }
        public void Init(string title, string content, TData[] datas, Action onClose = null, bool openAfterwards = true)
        {
            Init(title, content, openAfterwards, onClose);

            _selectionList.SetData(datas);
        }

        protected abstract void SetupSelectionItem(TItem item, TData data);
    }
}