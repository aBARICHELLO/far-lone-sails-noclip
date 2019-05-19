using System;
using System.Collections;
using Okomotive.FarLoneSails.SaveGame;
using Okomotive.SideScrollerCharacterController;
using Okomotive.Toolset;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityStandardAssets.ImageEffects;

namespace Okomotive.FarLoneSails {
    public sealed partial class CameraEngine : MonoBehaviour {
        void Update() {
            if (Input.GetKeyDown(KeyCode.Numlock)) {
                this.noclip = !this.noclip;
                if (noclip) {
                    // TODO: Save starting FOV
                } else {
                    // TODO: Restore starting FOV
                }
            }

            if (noclip) {
                this.Noclip();
            }
        }

        void FixedUpdate() {
            // (...)
            if (this.noclip) {
                return;
            }
            // (...)
        }

        void Noclip() {
            // FOV
            if (Input.GetAxis("Mouse ScrollWheel") < 0f) {
                this.camera.fov -= this.fovChange;
            }
            if (Input.GetAxis("Mouse ScrollWheel") > 0f) {
                this.camera.fov += this.fovChange;
            }
            Mathf.Clamp(this.camera.fov, 0f, 300f);

            // Camera Rotation
            this.cameraXAngle += 0.5f * Input.GetAxis("Mouse X");
            this.cameraYAngle -= 0.5f * Input.GetAxis("Mouse Y");
            Mathf.Clamp(this.cameraYAngle, -50f, 50f);
            this.camera.transform.eulerAngles = new Vector3(this.cameraYAngle, this.cameraXAngle, 0f);

            // Position
            float speedModifier = 1f;
            if (Input.GetKey(KeyCode.Keypad0)) { // boost
                speedModifier = 8f;
            }
            if (Input.GetKey(KeyCode.Home)) { // forwards
                Vector3 direction = camera.transform.forward;
                direction.Normalize();
                camera.transform.position += Time.deltaTime * speedModifier * noclipSpeed * direction;
            }
            if (Input.GetKey(KeyCode.End)) { // backwards
                Vector3 direction = camera.transform.forward;
                direction.Normalize();
                camera.transform.position -= Time.deltaTime * speedModifier * noclipSpeed * direction;
            }
            if (Input.GetKey(KeyCode.Delete)) { // left
                Vector3 direction = camera.transform.right;
                direction.Normalize();
                camera.transform.position -= Time.deltaTime * speedModifier * noclipSpeed * direction;
            }
            if (Input.GetKey(KeyCode.PageDown)) { // right
                Vector3 direction = camera.transform.right;
                direction.Normalize();
                camera.transform.position += Time.deltaTime * speedModifier * noclipSpeed * direction;
            }
            if (Input.GetKey(KeyCode.Insert)) { // down
                Vector3 direction = camera.transform.up;
                direction.Normalize();
                camera.transform.position -= Time.deltaTime * speedModifier * noclipSpeed * direction;
            }
            if (Input.GetKey(KeyCode.PageUp)) { // up
                Vector3 direction = camera.transform.up;
                direction.Normalize();
                camera.transform.position += Time.deltaTime * speedModifier * noclipSpeed * direction;
            }
        }

        void SaveStartPos() {}

        void RestoreSavedPos() {}

        Camera camera;

        // (...)

        bool noclip = true;
        float fovChange = 500f * Time.deltaTime;
        float noclipSpeed = 50f;
    }
}
