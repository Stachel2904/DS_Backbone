using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace DivineSkies.Modules.ResourceManagement
{
    /// <summary>
    /// Load Resources when you acces them and saves it, so you don't have to load them again.
    /// </summary>
    public class ResourceController : ModuleBase<ResourceController>
    {
        private readonly Dictionary<string, UnityEngine.Object> _loadedResources = new();
        private readonly Dictionary<string, Component> _loadedPrefabs = new();
        
        public override void OnSceneFullyLoaded()
        {
            ClearCache();
        }

        public void ClearCache()
        {
            _loadedResources.Clear();
            _loadedPrefabs.Clear();
        }

        public T LoadResource<T>(string name) where T : UnityEngine.Object
        {
            string path = typeof(T).ToString().Split('.').Last() + "/" + name;
            if (!_loadedResources.TryGetValue(path, out UnityEngine.Object resource))
            {
                resource = Resources.Load<T>(path);
                if (resource == null || resource is not T)
                {
                    this.PrintError("Could not load resource of type " + typeof(T) + " at " + path);
                    return null;
                }
                _loadedResources.Add(path, resource);
            }

            return (T)resource;
        }

        public T LoadAndInstatiatePrefab<T>(Transform parent = null, string localPath = "") where T : Component
        {
            string path = "Prefabs/" + localPath + typeof(T).ToString().Split('.').Last();
            if (!_loadedPrefabs.TryGetValue(path, out Component prefab))
            {
                prefab = Resources.Load<T>(path);
                if (prefab == null || prefab is not T)
                {
                    this.PrintError("Could not load prefab of type " + typeof(T) + " at " + path);
                    return null;
                }
                _loadedPrefabs.Add(path, prefab);
            }

            if(parent == null)
            {
                return Instantiate((T)prefab);
            }

            return Instantiate((T)prefab, parent);
        }

        public ResourceRequest LoadOnDemandResourceAsync<TObject>(string path) where TObject : UnityEngine.Object => Resources.LoadAsync<TObject>("OnDemand/" + path);
    }
}