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

    [Inject]
    public void Construct()
    {
        transform.localScale = Vector3.one;
    }
}