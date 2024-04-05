
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Transform _gunPoint;
    [SerializeField] private GameObject _bulletTrail;
    [SerializeField] private float _weaponRange = 10f;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private int _gunStore;
    [SerializeField] private float _reloadTime;
    [SerializeField] private Transform _lHandLimb;
    [SerializeField] private Transform _rHandLimb;

    public Transform LHandLimb => _lHandLimb;
    public Transform RHandLimb => _rHandLimb;

    public Vector2 dir { get; private set; }
    private float reload = 0;
    private List<IHittable> hittables = new();
    public void LookIt(Vector2 vector2){
         float angle = Mathf.Atan2(vector2.y, vector2.x) * Mathf.Rad2Deg;
            // Если враг находится слева от оружия
            if (vector2.x < 0)
            {
                // Поворачиваем оружие на 180 градусов, чтобы смотреть на врага
                transform.eulerAngles = new Vector3(0, 180, 180 - angle);
            }
            else
            {
                // В противном случае поворачиваем оружие в стандартное положение
                transform.eulerAngles = new Vector3(0, 0, angle);
            }
    } 
    public bool HasTarget(){
        if(hittables.Count == 0){
            return false;
        }
        return true;
    }
    void Update()
    {
        if (reload > 0)
        {
            reload -= Time.deltaTime;
        }

        if (reload <= 0 && _gunStore <= 0)
        {
            reload = _reloadTime;
            _gunStore = 10; // Возможно, это должно быть задано в другом месте, в зависимости от логики вашей игры
        }
        if (hittables.Count != 0)
        {   
            var enemy = hittables.First();
            Vector3 direction = enemy.GetTransform().position - transform.position;
            dir = direction.normalized;
            // Вычисляем угол между направлением оружия и направлением к врагу
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            // Если враг находится слева от оружия
            if (dir.x < 0)
            {
                // Поворачиваем оружие на 180 градусов, чтобы смотреть на врага
                transform.eulerAngles = new Vector3(0, 180, 180 - angle);
            }
            else
            {
                // В противном случае поворачиваем оружие в стандартное положение
                transform.eulerAngles = new Vector3(0, 0, angle);
            }
        }
    }

    public void Shoot()
    {
        if (reload > 0)
        {
            return;
        }

        if (_gunStore <= 0)
        {
            reload = _reloadTime;
            return;
        }

        var hit = Physics2D.Raycast(_gunPoint.position, transform.right, _weaponRange, _layerMask);
        var trail = Instantiate(_bulletTrail, _gunPoint.position, Quaternion.identity);
        var trailScript = trail.GetComponent<BulletTrail>();
        _gunStore--;

        if (hit.collider != null)
        {
            trailScript.SetTargetPosition(hit.point);
            var hittable = hit.collider.GetComponent<IHittable>();
            hittable?.OnDamage(1);
        }
        else
        {
            var endPosition = _gunPoint.position + transform.right * _weaponRange;
            trailScript.SetTargetPosition(endPosition);
        }
    }

    private void OnTriggere(Collider2D other)
    {
        var hittable = other.transform.GetComponent<IHittable>();
        if (hittable != null)
        {

        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        var hittable = other.transform.GetComponent<IHittable>();
        if (hittable != null)
        {
            hittables.Add(hittable);
        }

    }
    private void OnTriggerExit2D(Collider2D other)
    {
        var hittable = other.transform.GetComponent<IHittable>();
        if (hittable != null)
        {
            hittables.Remove(hittable);
        }
    }
}
