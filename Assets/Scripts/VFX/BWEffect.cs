using UnityEngine;

public class BWEffect : MonoBehaviour
{
    public float intensity;
    public Material PostprocessMaterial;
    public Material SimpleRender;

    public RenderTexture CameraRenderTexture;
    public RenderTexture Buffer;

    public void OnEnable()
    {
        PostprocessMaterial.SetFloat("_bwBlend", intensity);
        CameraRenderTexture = new RenderTexture(Screen.width, Screen.height, 24);
        Buffer = new RenderTexture(Screen.width, Screen.height, 24);

        Camera.main.targetTexture = CameraRenderTexture;
    }

    void OnPostRender()
    {
        Graphics.SetRenderTarget(Buffer);
        GL.Clear(true, true, Color.black);

        Graphics.SetRenderTarget(Buffer.colorBuffer, CameraRenderTexture.depthBuffer);
        Graphics.Blit(CameraRenderTexture, SimpleRender);
        Graphics.Blit(CameraRenderTexture, PostprocessMaterial);

        RenderTexture.active = null;
        Graphics.Blit(Buffer, SimpleRender);
    }

    private void Start()
    {
        EventManager.StartListening("HpChanged", UpdateIntensity);
    }

    private void UpdateIntensity(string hpSlashMaxHp)
    {
        float currentHp = float.Parse(hpSlashMaxHp.Split('/')[0]);
        float maxHp = float.Parse(hpSlashMaxHp.Split('/')[1]);
        intensity = 1 - currentHp / maxHp;
        PostprocessMaterial.SetFloat("_bwBlend", intensity);
    }
}