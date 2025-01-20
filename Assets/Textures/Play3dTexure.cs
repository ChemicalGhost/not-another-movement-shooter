using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Play3dTexure : MonoBehaviour
{

    public Texture3D texture;
    public float alpha = 1;
    public float quality = 1;
    public FilterMode filterMode;
    public bool useColorRamp;
    public bool useCustomColorRamp;

    // We should initialize this gradient before using it as a custom color ramp
    public Gradient customColorRampGradient;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

}

[CanEditMultipleObjects]
[CustomEditor(typeof(Play3dTexure))]
public class Handle : Editor
{
    private void OnSceneViewGUI(SceneView sv)
    {
        Object[] objects = targets;
        foreach (var obj in objects)
        {
            Play3dTexure reference = obj as Play3dTexure;
            if (reference != null && reference.texture != null)
            {
                Handles.matrix = reference.transform.localToWorldMatrix;
                Handles.DrawTexture3DVolume(reference.texture, reference.alpha, reference.quality, reference.filterMode,
                    reference.useColorRamp, reference.useCustomColorRamp ? reference.customColorRampGradient : null);
            }
        }
    }

    void OnEnable()
    {
        SceneView.duringSceneGui += OnSceneViewGUI;
    }

    void OnDisable()
    {
        SceneView.duringSceneGui -= OnSceneViewGUI;
    }
}
