using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestCodeEX2 : MonoBehaviour
{
    [SerializeField]
    private Canvas FadeCanvas;

    [SerializeField]
    private RawImage FadeImage;

    [SerializeField]
    private float FadeSpeed;

    public static TestCodeEX2 Instance;

    // Awake is called when the script instance is being loaded.
    private void Awake()
    {
        // FadeCanvas = new GameObject().AddComponent<Canvas>();
        // FadeCanvas.gameObject.AddComponent<CanvasScaler>();
        // FadeCanvas.gameObject.AddComponent<GraphicRaycaster>();
        // FadeCanvas.gameObject.name = "FadeCanvas";
        // FadeCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
        // FadeCanvas.transform.parent = this.transform;

        // FadeImage = new GameObject().AddComponent<RawImage>();
        // FadeImage.transform.parent = FadeCanvas.transform;
        // FadeImage.gameObject.name = "FadeImage";
        // RectTransform Rect = (RectTransform)FadeImage.transform;
        // //Rect.sizeDelta;
        // FadeImage.transform.parent = FadeCanvas.transform;


        
        //FadeImage.gameObject.RectTransform.sizeDelta = new Vecotor4(0f,0f,0f,0f);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
