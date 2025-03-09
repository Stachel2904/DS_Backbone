using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using DivineSkies.Modules.ResourceManagement;

namespace DivineSkies.Modules.UI
{
    /// <summary>
    /// Useful Class to Create and Pool Elements for Datas
    /// </summary>
    public class PooledListDisplay<TItem, TData> where TItem : UiItemBase
    {
        private readonly Transform _wrapper;
        private readonly List<TItem> _children;

        private Action<TItem, TData> _updateAction;

        private TData[] _datas;

        /// <param name="wrapper">In this transform will the items be created</param>
        /// <param name="onUpdate"></param>
        public PooledListDisplay(Transform wrapper, Action<TItem, TData> onUpdate)
        {
            _wrapper = wrapper;
            _children = new();
            _updateAction = onUpdate;
        }

        /// <summary>
        /// Use this to reset Data
        /// </summary>
        /// <param name="datas"></param>
        public void SetData(TData[] datas)
        {
            _datas = datas;
            Refresh();
        }

        public TItem GetItem(int index) => _children[index];

        public TData GetData(int index) => _datas[index];

        public TData GetSpecificData(Predicate<TData> match) => _datas.FirstOrDefault(d => match(d));

        /// <summary>
        /// Force Refresh items (maybe data was changed from outside)
        /// </summary>
        public void Refresh()
        {
            for (int i = 0; i < _datas.Length; i++)
            {
                if(_children.Count <= i)
                {
                    _children.Add(ResourceController.Main.LoadAndInstatiatePrefab<TItem>(_wrapper));
                }

                _updateAction?.Invoke(_children[i], _datas[i]);
            }

            while(_children.Count > _datas.Length)
            {
                TItem child = _children[^1];
                _children.Remove(child);
                UnityEngine.Object.Destroy(child.gameObject);
            }

            LayoutRebuilder.ForceRebuildLayoutImmediate(_wrapper as RectTransform);
        }
    }
}