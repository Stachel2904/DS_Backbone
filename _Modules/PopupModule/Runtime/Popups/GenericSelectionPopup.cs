using System;
using UnityEngine;
using DivineSkies.Modules.UI;

namespace DivineSkies.Modules.Popups
{
    /// <summary>
    /// Use this Popup to display a set of Items
    /// </summary>
    public abstract class GenericSelectionPopup<TItem, TData> : NotificationPopup where TItem : UiItemBase, ISelectable<TData>
    {
        [SerializeField] private Transform _selectionParent;

        private PooledListDisplay<TItem, TData> _selectionList;
        private TData _currentSelection;

        protected override void OnCreation()
        {
            base.OnCreation();
            for (int i = 0; i < _selectionParent.childCount; i++)
            {
                Destroy(_selectionParent.GetChild(0).gameObject);
            }

            _selectionList = new PooledListDisplay<TItem, TData>(_selectionParent, SetupSelectionItem);
        }

        public void Init(string title, string content, TData[] datas, Action<TData> onSelected, bool openAfterwards = true)
        {
            _currentSelection = default;

            Init(title, content, openAfterwards, () => onSelected?.Invoke(_currentSelection));

            _selectionList.SetData(datas);
        }

        protected virtual void SetupSelectionItem(TItem item, TData data)
        {
            _currentSelection ??= data;
            item.OnSelected -= OnSelected;
            item.OnSelected += OnSelected;
            item.SetData(data, data.Equals(_currentSelection));
        }

        protected virtual void OnSelected(TData data)
        {
            _currentSelection = data;
        }
    }

    public interface ISelectable<TSelectData>
    {
        public event Action<TSelectData> OnSelected;
        public void SetData(TSelectData data, bool isSelected);
    }
}