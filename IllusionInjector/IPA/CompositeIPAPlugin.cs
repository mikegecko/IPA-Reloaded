﻿using IllusionPlugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using Logger = IllusionInjector.Logging.Logger;

namespace IllusionInjector {
#pragma warning disable CS0618 // Type or member is obsolete
    public class CompositeIPAPlugin : IPlugin
    {
        IEnumerable<IPlugin> plugins;

        private delegate void CompositeCall(IPlugin plugin);
        
        public CompositeIPAPlugin(IEnumerable<IPlugin> plugins) {
            this.plugins = plugins;
        }

        public void OnApplicationStart() {
            Invoke(plugin => plugin.OnApplicationStart());
        }

        public void OnApplicationQuit() {
            Invoke(plugin => plugin.OnApplicationQuit());
        }
        
        private void Invoke(CompositeCall callback) {
            foreach (var plugin in plugins) {
                try {
                    callback(plugin);
                }
                catch (Exception ex) {
                    Logger.log.Error($"{plugin.Name}: {ex}");
                }
            }
        }

        public void OnUpdate() {
            Invoke(plugin => plugin.OnUpdate());
        }

        public void OnFixedUpdate() {
            Invoke(plugin => plugin.OnFixedUpdate());
        }
        
        public string Name {
            get { throw new NotImplementedException(); }
        }

        public string Version {
            get { throw new NotImplementedException(); }
        }

        public void OnLateUpdate() {
            Invoke(plugin => {
                if (plugin is IEnhancedBeatSaberPlugin)
                    ((IEnhancedBeatSaberPlugin) plugin).OnLateUpdate();
            });
        }

        public void OnLevelWasLoaded(int level)
        {
            Invoke(plugin => plugin.OnLevelWasLoaded(level));
        }

        public void OnLevelWasInitialized(int level)
        {
            Invoke(plugin => plugin.OnLevelWasInitialized(level));
        }
    }
#pragma warning restore CS0618 // Type or member is obsolete
}