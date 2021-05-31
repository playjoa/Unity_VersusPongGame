using UnityEngine;

public class PostFXHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject postProcessingVolume;

    private void OnEnable()
    {
        PostFXToggle.OnPostFXValueChange += ManagePostFX;
    }

    private void OnDisable()
    {
        PostFXToggle.OnPostFXValueChange -= ManagePostFX;
    }

    private void Start()
    {
        ManagePostFX();
    }

    void ManagePostFX() 
    {
        if(postProcessingVolume)
            postProcessingVolume.SetActive(PostFXToggle.PostFXStats);
    }
}