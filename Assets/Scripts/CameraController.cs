using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private float speed = 10.0F;

    [SerializeField]
    private Transform target;

    private void Awake()
    {
        if (!target) target = FindObjectOfType<Move>().transform; // слежка за юнитом
    } // проверка условия про движении героя

    private void Update() // задание позиции камеры
    {
        Vector3 position = target.position;
        position.z = -10.0F;
        transform.position = Vector3.Lerp(transform.position, position, speed * Time.deltaTime);
    }
}

