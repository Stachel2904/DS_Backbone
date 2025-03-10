using System;
using System.Collections;
using System.Collections.Generic;
using DivineSkies.Modules.SaveGame;

namespace DivineSkies.Modules
{
    /// <summary>
    /// Inherit from this class to use save/load logic
    /// </summary>
    public abstract class SaveGameModuleBase<TSaveGame, TModule> : ModuleBase<TModule> where TSaveGame : SaveGameBase, new() where TModule : Core.ModuleBase
    {
        /// <summary>
        /// This will be invoked after <see cref="SetDirty"/> was called
        /// </summary>
        public event Action OnDataChanged;

        /// <summary>
        /// Must be inialized after <see cref="SaveGameController"/>
        /// </summary>
        public override int InitPriority => 2;

        /// <summary>
        /// You may access your Data here
        /// </summary>
        protected TSaveGame Data { get; private set; }

        /// <summary>
        /// Create your start Data here
        /// </summary>
        protected abstract TSaveGame CreateDefaultSaveGame();

        public override IEnumerator InitializeAsync()
        {
            IEnumerator<TSaveGame> loadRoutine = SaveGameController.Main.Load<TSaveGame>(this);
            while (loadRoutine.MoveNext())
            {
                yield return loadRoutine.Current;
            }

            Data = loadRoutine.Current ?? CreateDefaultSaveGame();
            Initialize();
            SetDirty();
            yield return null;
        }

        /// <summary>
        /// Call this after you made any changes to <see cref="Data"/>
        /// </summary>
        protected void SetDirty()
        {
            SaveGameController.Main.RegisterDirtySavegame(this, Data);
            OnDataChanged?.Invoke();
        }
    }
}
