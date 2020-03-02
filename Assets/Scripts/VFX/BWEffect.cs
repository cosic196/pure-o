using UnityEngine;

[ExecuteInEditMode]
public class BWEffect : MonoBehaviour
{
    public float intensity;
    private Material material;
    [SerializeField]
    private Shader _bwShader;

    // Creates a private material used to the effect
    void Awake()
    {
        material = new Material(_bwShader);
    }

    // Postprocess the image
    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (intensity == 0)
        {
            Graphics.Blit(source, destination);
            return;
        }

        material.SetFloat("_bwBlend", intensity);
        Graphics.Blit(source, destination, material);
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
    }
}