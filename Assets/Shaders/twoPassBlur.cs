using UnityEngine;

[ExecuteInEditMode]
public class FullscreenBlur : MonoBehaviour
{
    public Material blurMaterial;
    public float blurSize = 1.0f;

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        if (blurMaterial != null)
        {
            blurMaterial.SetFloat("_BlurSize", blurSize);

            int rtW = src.width;
            int rtH = src.height;

            RenderTexture rtTempA = RenderTexture.GetTemporary(rtW, rtH, 0);

            // First pass: horizontal blur
            Graphics.Blit(src, rtTempA, blurMaterial, 0);

            // Second pass: vertical blur
            Graphics.Blit(rtTempA, dest, blurMaterial, 1);

            RenderTexture.ReleaseTemporary(rtTempA);
        }
        else
        {
            Graphics.Blit(src, dest);
        }
    }
}