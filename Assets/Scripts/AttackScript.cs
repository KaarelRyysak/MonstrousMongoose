namespace Assets
{
    using System.Collections;
    using UnityEngine;

    public class AttackScript : MonoBehaviour
    {
        private float damage = 10.0f;

        // Use this for initialization
        private void Start()
        {
            StartCoroutine(attack());
        }

        // Update is called once per frame
        private void Update()
        {
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                other.gameObject.GetComponent<WalkerScript>().takeDamage(damage);
                Debug.Log(other.name + other.GetComponent<WalkerScript>().getHealth());
            }
        }

        private IEnumerator attack()
        {
            while (gameObject.transform.localScale.x < 2.5)
            {
                Vector3 size = gameObject.transform.localScale;
                gameObject.transform.localScale += new Vector3(.50f, 0f, .50f);

                yield return new WaitForSeconds(.1f);
            }
            Destroy(gameObject);
        }
    }
}