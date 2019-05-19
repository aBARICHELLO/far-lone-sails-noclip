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
                    this.SaveCameraTransform();
                    this.camera.fov = noclipFOV;
                } else {
                    this.LoadCameraTransform();
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
            this.camera.fov = Mathf.Clamp(this.camera.fov, 0f, 130f);

            // Camera Rotation
            this.cameraXAngle += 0.5f * Input.GetAxis("Mouse X");
            this.cameraYAngle -= 0.5f * Input.GetAxis("Mouse Y");
            this.cameraYAngle = Mathf.Clamp(this.cameraYAngle, -50f, 50f);
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

        void SaveCameraTransform() {
            this.savedPosition = this.camera.transform.position;
            this.savedRotation = this.camera.transform.rotation;
            this.savedFOV = this.camera.fov;
        }

        void LoadCameraTransform() {
            this.camera.transform.SetPositionAndRotation(this.savedPosition, this.savedRotation);
            this.camera.fov = this.savedFOV;
        }

        // (...)

        Camera camera;

        bool noclip = false;
        float fovChange = 500f * Time.deltaTime;

        const float noclipSpeed = 50f;
        const float noclipFOV = 80f;

        Vector3 savedPosition;
        Quaternion savedRotation;
        float savedFOV = 0f;
    }
}
