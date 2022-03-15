using UnityEngine;
using UniRx;

public class Wavering : MonoBehaviour
{
    [SerializeField] private float _frequency = 1f;
    [SerializeField] private float _magnitude = 0.2f;

    public void StartWaver()
    {
        Observable.EveryUpdate().Subscribe(_ =>
           {
               transform.localPosition = Vector3.right * Mathf.Sin(Time.time * _frequency) * _magnitude;
           }).AddTo(this);
    }
}
