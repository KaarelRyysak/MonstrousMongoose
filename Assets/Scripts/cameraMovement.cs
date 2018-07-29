namespace Assets
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class cameraMovement : MonoBehaviour
    {
        private Camera cam;
        public int enemyCount;

        private void Start()
        {
            cam = GlobalObjectScript.Instance.cam;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.name.Equals("p_player(Clone)"))
            {
                Debug.Log("Entered New Room");
                cam.transform.SetParent(GetComponentInParent<NullCatch>().gameObject.transform);
                cam.transform.Translate(cam.transform.parent.transform.position - cam.transform.position);
            }
            if (other.gameObject.CompareTag("Enemy"))
            {
                if (other.gameObject.name.Equals("p_shooter(Clone)") && (other.gameObject.GetComponent<ShooterScript>().counted != true))
                {
                    other.gameObject.GetComponent<ShooterScript>().parentRoom = gameObject;
                    enemyCount++;
                }
                if (other.gameObject.name.Equals("p_walker(Clone)") && (other.gameObject.GetComponent<WalkerScript>().counted != true))
                {
                    other.gameObject.GetComponent<WalkerScript>().parentRoom = gameObject;
                    enemyCount++;
                }
            }
        }

        private void Update()
        {
            if (GetComponentInChildren<NullDoor>())
            {
                if (enemyCount <= 0)
                {
                    GetComponentInChildren<NullDoor>().gameObject.SetActive(false);
                }
                if (enemyCount > 0)
                {
                    GetComponentInChildren<NullDoor>().gameObject.SetActive(true);
                }
            }

        }
    }
}
