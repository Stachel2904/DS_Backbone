using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using DivineSkies.Modules.Mogi;
using DivineSkies.Modules;
using DivineSkies.Modules.GameWorld;
using FEA.UI;
using DivineSkies.Modules.Popups;
using DivineSkies.Modules.UI;
using DivineSkies.Modules.UI.Particles;

namespace DivineSkies.Modules.ResourceManagement
{
    /// <summary>
    /// Load Resources when you acces them and saves it, so you don't have to load them again.
    /// </summary>
    public class ResourceController : ModuleBase<ResourceController>
    {
        private readonly Dictionary<FeaValueType, Sprite[]> _loadedIcons = new();
        private readonly Dictionary<Type, UiItemBase> _loadedUiItems = new();
        private readonly Dictionary<string, Texture> _loadedMogiSprites = new();
        private readonly Dictionary<UiEffectType, UiParticle> _loadedEffectTemplates = new();

        #region static Prefabs
        private FeaIcon _iconPrefab;
        public FeaIcon IconPrefab
        {
            get
            {
                _iconPrefab ??= Resources.Load<FeaIcon>("Prefabs/UI/Icon");
                return _iconPrefab;
            }
        }
        #endregion
        
        public override void OnSceneFullyLoaded()
        {
            ClearCache();
        }

        public void ClearCache()
        {
            _loadedUiItems.Clear();
            _loadedMogiSprites.Clear();
            _loadedIcons.Clear();
            _loadedEffectTemplates.Clear();
        }

        public T LoadItem<T>(Transform parent) where T : UiItemBase
        {
            string path = "Prefabs/UI/Items/" + typeof(T).ToString().Split('.').Last();
            if (!_loadedUiItems.TryGetValue(typeof(T), out UiItemBase prefab))
            {
                prefab = Resources.Load<T>(path);
                if (prefab == null || !prefab is T)
                {
                    this.PrintError("Could not load item of type " + typeof(T) + " at " + path);
                    return null;
                }
                _loadedUiItems.Add(typeof(T), prefab);
            }
            return Instantiate((T)prefab, parent);
        }

        public T LoadPopup<T>() where T : PopupBase
        {
            string path = "Prefabs/UI/Popups/" + typeof(T).ToString().Split('.').Last();
            T prefab = Resources.Load<T>(path);
            if (prefab == null)
                this.PrintError("Could not load popup of type " + typeof(T) + " at " + path);
            return Instantiate(prefab, ModuleController.ConstantUiParent);
        }

        public Sprite LoadIconSprite(FeaValueType data, int subData)
        {
            string parentPath = "Sprites/UI/Icons/" + data switch
            {
                FeaValueType.Loot => "UI_Icons_Gifts",
                FeaValueType.Resource => "UI_Icons_Resources",
                FeaValueType.Money => "UI_Icons_Money",
                _ => ""
            };

            string iconName = data.GetSubEnumValue(subData);

            if (!_loadedIcons.TryGetValue(data, out Sprite[] icons))
            {
                icons = Resources.LoadAll<Sprite>(parentPath);
                _loadedIcons.Add(data, icons);
            }

            return icons.First(s => s.name == iconName);
        }

        public ResourceRequest LoadOnDemandResourceAsync<TObject>(string path) where TObject : UnityEngine.Object => Resources.LoadAsync<TObject>("OnDemand/" + path);

        public Texture LoadMogiSprite(MogiSpecie specie, string cloth = null)
        {
            cloth ??= "base";
            string key = specie.ToString() + "/" + cloth;
            if (!_loadedMogiSprites.TryGetValue(key, out var sprite))
            {
                sprite = Resources.Load<Texture>("MogiSprites/" + key);
                _loadedMogiSprites.Add(key, sprite);
            }
            return sprite;
        }

        public ResourceRequest LoadBiomeTilesAsync(BiomeType biome) => Resources.LoadAsync<Texture2D>("Biomes/" + biome.ToString().ToLower());

        public UiParticle LoadUiEffect(UiEffectType type)
        {
            if (!_loadedEffectTemplates.TryGetValue(type, out UiParticle template))
            {
                template = Resources.Load<UiParticle>("Prefabs/UI/Effects/" + type);
                if (template == null)
                    this.PrintError("Could not find prefab for effect " + type);
                _loadedEffectTemplates.Add(type, template);
            }

            return template;
        }
    }
}