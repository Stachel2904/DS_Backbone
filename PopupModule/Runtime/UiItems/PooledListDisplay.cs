using DivineSkies.Modules.ResourceManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace FEA.UI
{
    public class PooledListDisplay<TItem, TData> where TItem : UiItemBase
    {
        private readonly Transform _wrapper;
        private readonly List<TItem> _children;

        private Action<TItem, TData> _updateAction;

        private TData[] _datas;

        public PooledListDisplay(Transform wrapper, Action<TItem, TData> onUpdate)
        {
            _wrapper = wrapper;
            _children = new();
            _updateAction = onUpdate;
        }

        public void SetData(TData[] datas)
        {
            _datas = datas;
            Refresh();
        }

        public TItem GetItem(int index) => _children[index];

        public TData GetData(int index) => _datas[index];

        public TData GetSpecificData(Predicate<TData> match) => _datas.FirstOrDefault(d => match(d));

        public void Refresh()
        {
            for (int i = 0; i < _datas.Length; i++)
            {
                if(_children.Count <= i)
                    _children.Add(ResourceController.Main.LoadAndInstatiatePrefab<TItem>(_wrapper));

                _updateAction?.Invoke(_children[i], _datas[i]);
            }

            while(_children.Count > _datas.Length)
            {
                var child = _children[^1];
                _children.Remove(child);
                UnityEngine.Object.Destroy(child.gameObject);
            }

            LayoutRebuilder.ForceRebuildLayoutImmediate(_wrapper as RectTransform);
        }
    }
}