using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Valve.VR;
using Valve.VR.Extras;
//using Valve.XR.Extras; 

namespace TensorFlowLite
{

    public class VRCamInput : MonoBehaviour
    {
        [System.Serializable]
        public class TextureUpdateEvent : UnityEvent<Texture> { }
        public TextureUpdateEvent onTextureUpdate = new TextureUpdateEvent();
        private void OnEnable()
        {
            SteamVR_TrackedCamera.VideoStreamTexture source = SteamVR_TrackedCamera.Source(false);
            source.Acquire();

            if (!source.hasCamera)
                enabled = false;
        }

        private void OnDisable()
        {
            SteamVR_TrackedCamera.VideoStreamTexture source = SteamVR_TrackedCamera.Source(false);
            source.Release();
        }


        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            SteamVR_TrackedCamera.VideoStreamTexture source = SteamVR_TrackedCamera.Source(false);
            Texture2D texture = source.texture;

            onTextureUpdate.Invoke(texture);
        }
    }
}
