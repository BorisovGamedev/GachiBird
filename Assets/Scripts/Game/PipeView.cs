using UnityEngine;
using Zenject;

public class PipeView : MonoBehaviour
{
    [SerializeField] private float _speed = 3f;

    private void Update()
    {
        transform.Translate(Vector3.left * _speed * Time.deltaTime);
    }
    
    public class Pool : MonoMemoryPool<PipeView> { }

    private IMemoryPool _pool;

    [Inject]
    public void Construct(PipeView.Pool pool)
    {
        _pool = pool;
        transform.localScale = Vector3.one;
    }
}