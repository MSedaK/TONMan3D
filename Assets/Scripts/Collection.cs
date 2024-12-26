using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collection : MonoBehaviour
{
    public AudioClip collectSound; // Coin toplama sesi
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>(); // AudioSource bileþenini ekle
        audioSource.clip = collectSound; // Ses klibini ata
        audioSource.playOnAwake = false; // Sesin otomatik baþlamasýný engelle
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            audioSource.Play(); // Coin sesi çal
            GameManager.incrementScore();
            Destroy(gameObject, collectSound.length); // Ses tamamlandýktan sonra objeyi yok et
        }
    }
}
