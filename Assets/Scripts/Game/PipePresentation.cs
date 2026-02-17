using UnityEngine;
using Zenject;
using Flappy.Core;

public class PipePresentation : MonoBehaviour
{
    private GameConfig.PipeSettings _settings;

    [Inject]
    public void Construct(GameConfig.PipeSettings settings)
    {
        _settings = settings;
    }
    
    private void Update()
    {
        transform.Translate(Vector3.left * _settings.Speed * Time.deltaTime);
    }
    
    public class Pool : MonoMemoryPool<PipePresentation> { }

    [Inject]
    public void Construct()
    {
        transform.localScale = Vector3.one;
    }
}