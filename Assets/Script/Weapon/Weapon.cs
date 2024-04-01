using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] Transform Firepoint;
    [SerializeField] private float _speed;
    [SerializeField] private Transform _gunPoint;
    [SerializeField] private GameObject _bulletTrail;
    [SerializeField] private float _weaponRange = 10f;
    [SerializeField] private LayerMask _layerMask;
    //[SerializeField] private Animator _muzzleFlashAnimator;
    [SerializeField] private int _gunStore;
    [SerializeField] private float _reloadTime;
     float reload = 0;
    // Start is called before the first frame update
    void Start()
    {
        /*   while (true)
           {
               yield return new WaitForSeconds(1);
               var colision = Physics2D.Raycast(Firepoint.position, transform.right);
               Shoot();
           }
           */
    }

    // Update is called once per frame
    void Update()
    {   
         if (reload > 0)
    {
        reload -= Time.deltaTime;
    }
    
    // Проверяем, нужно ли начать перезарядку оружия
    if (reload <= 0 && _gunStore <= 0)
    {
        reload = _reloadTime;
        _gunStore = 10; // Возможно, это должно быть задано в другом месте, в зависимости от логики вашей игры
    }
        
        
        /*      
               // Получаем позицию курсора на экране
               Vector3 cursorScreenPosition = Input.mousePosition;

               // Преобразуем позицию курсора в трехмерные координаты в пространстве игры
               Vector3 cursorWorldPosition = Camera.main.ScreenToWorldPoint(cursorScreenPosition);
               cursorWorldPosition.z = 0f; // Устанавливаем z-координату на нулевое значение, чтобы объект оставался на плоскости игры

               // Определяем вектор, указывающий на позицию курсора относительно объекта
               Vector3 direction = cursorWorldPosition - transform.position;

               // Вычисляем угол между направлением курсора и направлением вправо (1, 0)
               float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

               // Поворачиваем объект курсора
               transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
      */
    }

    public void Shoot()
    {

        //_muzzleFlashAnimator.SetTrigger("Shoot");
        if (reload > 0){
            return;
        }
        
        
        if (_gunStore <= 0 )
        {
            reload = _reloadTime;
            return;
        }
        
        var hit = Physics2D.Raycast(_gunPoint.position, transform.right, _weaponRange, _layerMask);
        var trail = Instantiate(_bulletTrail, _gunPoint.position, Quaternion.identity);
        var trailScript = trail.GetComponent<BulletTrail>();
        _gunStore = _gunStore - 1;


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

    private void OnTriggerStay2D(Collider2D other)
    {
        var hittable = other.transform.GetComponent<IHittable>();
        if (hittable != null)
        {
            Vector3 direction = other.transform.position - transform.position;

            // Вычисляем угол между направлением курсора и направлением вправо (1, 0)
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // Поворачиваем объект курсора
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

    }
}
