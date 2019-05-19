using System;
using System.Collections.Generic;
using UnityEngine;

namespace Okomotive.FarLoneSails {
    public class Debugger : MonoBehaviour {
        públic Debugger() {
            this.refillEnergyKey = KeyCode.F1;
            this.repositionKey = KeyCode.F2;
            this.switchCameraDistance = KeyCode.F3;
            this.updateWeatherKey = KeyCode.F4;
            this.switchLevelHelpersKey = KeyCode.F5;
            this.toogleGizmos = KeyCode.F6;
            this.enableDisableCoverKey = KeyCode.F7;
            this.switchVisualModeKey = KeyCode.F8;
            this.selectFrontModulesKey = KeyCode.F9;
            this.selectBackModulesKey = KeyCode.F10;
            this.resetAchievements = KeyCode.Comma;
        }

        void Awake() {
            // if (!Application.isEditor && Debug.isDebugBuild)
            this.SetOn(true);
        }

        void Update() {
            if (Input.GetKeyDown(KeyCode.Numlock)) {
                this.debug = !this.debug;
                this.SetOn(this.debug);
            }

            if (this.onGameUpdateLoop != null) {
                this.onGameUpdateLoop();
            }
            if (!this.IsOn()) {
                return;
            }
            if (Input.GetKeyDown(this.refillEnergyKey) && API.Vehicle.IsReady()) {
                API.Vehicle.modules.motor.GetEnergy().IncreaseValue(100f);
            }
            if (Input.GetKeyDown(this.enableDisableCoverKey)) {
                this.ToggleCover();
            }
            if (Input.GetKeyDown(this.switchVisualModeKey)) {
                this.ToggleVisualMode();
            }
            if (Input.GetKeyDown(this.switchLevelHelpersKey)) {
                this.ToggleLevelHelpers();
            }
            // not implemented
            // if (Input.GetKeyDown(this.repositionKey)) {
            //     this.RepositionCharacterVehicleCamera();
            // }
            if (Input.GetKeyDown(this.updateWeatherKey)) {
                this.UpdateWeather();
            }
            this.InGameShortcuts();
        }

        void InGameShortcuts() {
            if (Input.GetKey(KeyCode.LeftShift)) {
                if (Input.GetKeyDown(KeyCode.N)) {
                    base.StartCoroutine(API.SaveManager.Save(API.Vehicle.GetPivot().position.x, "DebuggerTmpSaveGame", false));
                }
                if (Input.GetKeyDown(KeyCode.M)) {
                    API.SaveManager.LoadLatestSavePoint();
                }
                if (Input.GetKeyDown(KeyCode.J)) {
                    API.Vehicle.modules.motor.GetWheels().DamageFrontWheels();
                }
                if (Input.GetKeyDown(KeyCode.K)) {
                    API.Vehicle.GetBack().BreakApart();
                }
                if (Input.GetKeyUp(KeyCode.C)) {
                    API.UIManager.StartCredits();
                }
                if (Input.GetKeyDown(KeyCode.Comma)) {
                    API.AchievementManager.ResetAchievements();
                }
                if (Input.GetKeyDown(KeyCode.I)) {
                    API.Vehicle.GetFront().ResetVehicle();
                }
            }
        }

        // (...)

        public KeyCode refillEnergyKey;
        public KeyCode repositionKey;
        public KeyCode switchCameraDistance;
        public KeyCode updateWeatherKey;
        public KeyCode switchLevelHelpersKey;
        public KeyCode toogleGizmos;
        public KeyCode enableDisableCoverKey;
        public KeyCode switchVisualModeKey;
        public KeyCode selectFrontModulesKey;
        public KeyCode selectBackModulesKey;
        public KeyCode resetAchievements;

        bool debug = true;
    }
}
