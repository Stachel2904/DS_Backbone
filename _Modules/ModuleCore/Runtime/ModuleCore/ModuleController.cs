﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using DivineSkies.Modules.Core;

namespace DivineSkies.Modules
{
    public class ModuleController : MonoBehaviour
    {
        /// <summary>
        /// This will be called after a scene changed (you can also use <see cref="ModuleBase.OnSceneFullyLoaded"/>)
        /// </summary>
        public static event Action OnSceneChanged;

        public static bool IsReady => _self != null && _holder != null;

        private static ModuleController _self;
        private static ModuleHolderBase _holder;

        private readonly Dictionary<Type, ModuleBase> _constantModules = new();
        private readonly Dictionary<Type, ModuleBase> _sceneModules = new();
        private readonly Dictionary<Type, ModuleBase> _inactiveModules = new();
        private readonly List<ModuleBase> _uninitializedModules = new();
        private readonly List<SceneLoadData> sceneLoadData = new();

        internal static ModuleController Create(ModuleHolderBase holder)
        {
            if (_self != null)
            {
                return _self;
            }

            _self = new GameObject("Modules").AddComponent<ModuleController>();
            DontDestroyOnLoad(_self.gameObject);

            _holder = holder;

            return _self;
        }

        internal void InitializeConstantModules(Action callback)
        {
            foreach (var type in _holder.GetConstantModuleTypes())
            {
                AddModule(type, true);
            }

            StartCoroutine(InitializeAllUninitialized(callback));
        }

        /// <summary>
        /// Restarts the game. Loads scene Default Start and creates a new moduleController.
        /// </summary>
        public static void Restart()
        {
            foreach (ModuleBase module in _self._constantModules.Values.Concat(_self._sceneModules.Values))
            {
                module.BeforeUnregister();
                module.PrintLog("unregistered");
            }

            OnSceneChanged = null;
            _holder = null;
            _self = null;
            Destroy(_self.gameObject);

            SceneManager.LoadScene("DefaultStart");
        }

        public static bool Has<T>() where T : ModuleBase => Has(typeof(T));
        public static bool Has(Type type)
        {
            if(_self._sceneModules.ContainsKey(type) || _self._constantModules.ContainsKey(type))
            {
                return true;
            }

            return _self._constantModules.Values.Concat(_self._sceneModules.Values).Any(m => m.GetType().IsSubclassOf(type));
        }

        public static T Get<T>() where T : ModuleBase => Get(typeof(T)) as T;
        public static ModuleBase Get(Type type)
        {
            if(_self._constantModules.TryGetValue(type, out ModuleBase result) || _self._sceneModules.TryGetValue(type, out result))
            {
                return result;
            }

            result = _self._constantModules.Values.Concat(_self._sceneModules.Values).FirstOrDefault(m => m.GetType().IsSubclassOf(type));

            if (result == null)
            {
                _self.PrintError(type + " is no loaded Module");
            }

            return result;
        }

        public static bool TryGet<T>(out T module) where T : ModuleBase
        {
            bool found = _self._constantModules.TryGetValue(typeof(T), out ModuleBase baseModule) || _self._sceneModules.TryGetValue(typeof(T), out baseModule);

            module = baseModule as T;

            return found && module != null;
        }

        /// <summary>
        /// If you set load Data on scene load, you may retrieve it from here
        /// </summary>
        public static TLoadData GetLoadData<TLoadData>() where TLoadData : SceneLoadData => _self.sceneLoadData.OfType<TLoadData>().FirstOrDefault();

        #region module managing
        private ModuleBase AddModule(Type moduleType, bool isConstant)
        {
            if (Has(moduleType))
            {
                _self.PrintWarning("Module of type " + moduleType.ToString() + " is already added");
                return null;
            }

            if (_self._inactiveModules.TryGetValue(moduleType, out ModuleBase result))
            {
                _inactiveModules.Remove(moduleType);
                result.enabled = true;
            }
            else
            {
                result = _self.gameObject.AddComponent(moduleType) as ModuleBase;
            }

            AddModule(result, isConstant);
            return result;
        }

        private static void AddModule(ModuleBase module, bool isConstant)
        {
            if (isConstant)
            {
                _self._constantModules.Add(module.GetType(), module);
            }
            else
            {
                _self._sceneModules.Add(module.GetType(), module);
            }
            _self._uninitializedModules.Add(module);
            module.Register();
            module.PrintLog("registered");
        }

        private void RemoveSceneModules()
        {
            foreach (ModuleBase module in _sceneModules.Values.Where(m => m.AutoUnregister).ToArray())
            {
                module.BeforeUnregister();
                module.PrintLog("unregistered");
                module.enabled = false;
                _inactiveModules.Add(module.GetType(), module);

                _sceneModules.Remove(module.GetType());
            }
        }

        private IEnumerator InitializeAllUninitialized(Action callback)
        {
            ModuleBase[] modulesToInitialize = _uninitializedModules.OrderByDescending(m => m.InitPriority).ToArray();
            _uninitializedModules.Clear();

            LoadingScreen loadingScreen = Has<LoadingScreen>() ? Get<LoadingScreen>() : null;

            for (int i = 0; i < modulesToInitialize.Length; i++)
            {
                ModuleBase module = modulesToInitialize[i];
                loadingScreen?.ProgressLoading(i, modulesToInitialize.Length, module, 0);
                IEnumerator initRoutine = module.InitializeAsync();
                while (initRoutine.MoveNext())
                {
                    loadingScreen?.ProgressLoading(i, modulesToInitialize.Length, module, module.LoadingProgress);

                    yield return initRoutine.Current;
                }

                module.PrintLog("initialized");

                if (module is ISceneModule sceneModule)
                {
                    sceneModule.Visualization.Initialize();
                    sceneModule.Visualization.PrintLog(sceneModule + "-Visualization initialized");
                }

                loadingScreen?.ProgressLoading(i, modulesToInitialize.Length, module, 1);
            }

            callback?.Invoke();
        }
        #endregion

        #region sceneloading
        /// <summary>
        /// Use this to load a scene
        /// </summary>
        public static void LoadScene(Enum scene, params SceneLoadData[] loadData) => _self.LoadScene(scene.ToString(), loadData);

        internal void LoadScene(string scene, params SceneLoadData[] loadData)
        {
            this.PrintLog("Starting to load scene " + scene);

            RemoveSceneModules();
            sceneLoadData.Clear();

            sceneLoadData.AddRange(loadData);

            SceneManager.sceneLoaded += InitializeLoadedScene;
            SceneManager.LoadSceneAsync(scene);
        }

        internal static void OnSceneLoaded()
        {
            Scene loadedScene = SceneManager.GetActiveScene();
            _self.PrintLog("Loaded scene " + loadedScene.name + " from external");

            _self.RemoveSceneModules();
            _self.sceneLoadData.Clear();

            _self.InitializeLoadedScene(loadedScene, LoadSceneMode.Single);
        }

        private void InitializeLoadedScene(Scene scene, LoadSceneMode mode)
        {
            SceneManager.sceneLoaded -= _self.InitializeLoadedScene;
            this.PrintLog("--- Starting scene " + scene.name + " ---");

            foreach (Type moduleType in _holder.GetSceneModuleTypes(scene.name))
            {
                AddModule(moduleType, false);
            }

            SceneModuleLoader[] loaders = FindObjectsByType<SceneModuleLoader>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
            foreach (SceneModuleLoader loader in loaders)
            {
                //modules should not be destroyed
                DontDestroyOnLoad(loader.gameObject);

                foreach (ModuleBase module in loader.SceneModules)
                {
                    AddModule(module, false);
                }

                //destroy the loader component, it did it job
                Destroy(loader);
            }

            this.PrintLog("Starting initialize routine");
            StartCoroutine(SceneInitializeRoutine());
        }

        private IEnumerator SceneInitializeRoutine()
        {
            IEnumerator initRoutine = InitializeAllUninitialized(null);
            while (initRoutine.MoveNext())
            {
                yield return initRoutine.Current;
            }

            yield return null;

            foreach (ModuleBase module in _constantModules.Values.Concat(_sceneModules.Values))
            {
                module.OnSceneFullyLoaded();
            }

            OnSceneChanged?.Invoke();
        }
#endregion
    }
}