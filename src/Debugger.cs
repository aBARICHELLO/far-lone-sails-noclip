using System;
using System.Collections.Generic;
using UnityEngine;

namespace Okomotive.FarLoneSails {
    public class Debugger : MonoBehaviour {
        void Awake() {
            // if (!Application.isEditor && Debug.isDebugBuild)
            this.SetOn(true);
        }

        void Update() {
            if (Input.GetKeyDown(KeyCode.Quote)) {
                this.debug = !this.debug;
                this.SetOn(this.debug);
            }

            if (Application.isEditor && this.onGameUpdateLoop != null) {
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

        public KeyCode refillEnergyKey = KeyCode.F1;
        public KeyCode repositionKey = KeyCode.F2;
        public KeyCode switchCameraDistance = KeyCode.F3;
        public KeyCode updateWeatherKey = KeyCode.F4;
        public KeyCode switchLevelHelpersKey = KeyCode.F5;
        public KeyCode toogleGizmos = KeyCode.F6;
        public KeyCode enableDisableCoverKey = KeyCode.F7;
        public KeyCode switchVisualModeKey = KeyCode.F8;
        public KeyCode selectFrontModulesKey = KeyCode.F9;
        public KeyCode selectBackModulesKey = KeyCode.F10;

        bool debug = true;
    }
}
