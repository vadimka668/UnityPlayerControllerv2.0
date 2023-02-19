using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform target; // игрок, за которым следит кот
    public float moveSpeed = 2f; // скорость перемещения кота

    void Update()
    {
        // вычисляем вектор направления от кота до игрока
        Vector3 direction = target.position - transform.position;

        // нормализуем вектор направления, чтобы получить единичный вектор
        direction = direction.normalized;

        // перемещаем кота в направлении игрока
        transform.position += direction * moveSpeed * Time.deltaTime;
    }
}
