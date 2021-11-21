using UnityEngine;

namespace Exercise
{
    public class MovementSpeed : MonoBehaviour
    {
        public float movementSpeed;
        void Update()
        {
            transform.Translate(0f, 0f, movementSpeed * Time.deltaTime);
        }
    }
}
