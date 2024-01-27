using UnityEngine;

[RequireComponent(typeof(Camera))]
public class FullScreenQuad : MonoBehaviour
{
    public Material material;

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        if (material != null)
        {
            Graphics.Blit(src, dest, material);
        }
    }
}