using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Niantic.ARDK;
using Niantic.ARDK.AR;
using Niantic.Lightship.AR.Extensions;
using Niantic.Lightship.AR.ARFoundation;

public class SemanticSky : MonoBehaviour
{
    public ARSemanticSegmentationManager _segManager;
    private Texture _segTexture;

    // Start is called before the first frame update
    void OnEnable()
    {
        _segManager.SemanticModelIsReady += onSegementationReady;
    }

    void onSegementationReady(ARSemanticModelReadyEventArgs args)
    {

    }

    private void OnDisable()
    {
        _segManager.SemanticModelIsReady -= onSegementationReady;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
