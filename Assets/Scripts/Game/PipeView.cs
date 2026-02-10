using UnityEngine;
using Zenject;

public class PipeView : MonoBehaviour
{
    [SerializeField] private float _speed = 3f;

    private void Update()
    {
        transform.Translate(Vector3.left * _speed * Time.deltaTime);
        
        if (transform.position.x < -10f)
        {
            Destroy(gameObject);
        }
    }
    
    public class Factory : PlaceholderFactory<PipeView> { }
}