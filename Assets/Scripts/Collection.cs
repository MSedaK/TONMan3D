using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//This script when attached to collectibles  make them destroy when player touches them.
//It then calls incrementScore from game manager to add scores.



public class Collection : MonoBehaviour
{
    private bool isCollected = false;

    public void OnTriggerEnter(Collider other)
    {
        if (!isCollected && other.CompareTag("Player"))
        {
            isCollected = true;

            // Disable collider immediately to prevent double collection
            if (TryGetComponent<Collider>(out var collider))
            {
                collider.enabled = false;
            }

            GameManager.IncrementScore();
            StartCoroutine(CollectionEffect());
        }
    }

    private IEnumerator CollectionEffect()
    {
        // Disable renderer for visual feedback
        if (TryGetComponent<MeshRenderer>(out var renderer))
        {
            renderer.enabled = false;
        }

        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }
}
