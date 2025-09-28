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
        private readonly Dictionary<string, Object> _loadedResources = new();
        private readonly Dictionary<string, Component> _loadedPrefabs = new();

        public override void Initialize()
        {
            base.Initialize();
            Application.lowMemory += ClearCache;
        }

        public override void OnSceneFullyLoaded()
        {
            ClearCache();
        }

        /// <summary>
        /// Can be cleared manually but will be cleared automatically after every scene load
        /// </summary>
        public void ClearCache()
        {
            long gcBytes = System.GC.GetTotalMemory(false);
            _loadedResources.Clear();
            _loadedPrefabs.Clear();

            System.GC.Collect();
            gcBytes -= System.GC.GetTotalMemory(false);

            this.PrintLog($"Cleared Cache and saved {(gcBytes / 1024f / 1024):F2}MB of Memory");
        }

        /// <summary>
        /// Load a resource from "Resources/{typeof(T)}/{name}" (like sprites or audio files)
        /// </summary>
        public T LoadResource<T>(string name, string subFolder = "") where T : Object
        {
            string path = typeof(T).ToString().Split('.').Last() + "/" + subFolder + name;
            if (!_loadedResources.TryGetValue(path, out Object resource))
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

        /// <summary>
        /// Loads a prefab from "Resources/Prefabs/{localPath}/{typeof(T)}, creates a copy of it and attaches it to "parent"
        /// </summary>
        public T LoadAndInstatiatePrefab<T>(Transform parent = null, string localPath = "", string fileName = null) where T : Component
        {
            fileName ??= typeof(T).ToString().Split('.').Last();
            string path = "Prefabs/" + localPath + fileName;
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

        /// <summary>
        /// Loads a ressource async from "Resources/OnDemand/{path}
        /// </summary>
        public ResourceRequest LoadOnDemandResourceAsync<TObject>(string path) where TObject : Object => Resources.LoadAsync<TObject>("OnDemand/" + path);
    }
}