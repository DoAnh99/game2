using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{    
    private int cherries = 0; 

   [SerializeField] private Text cherriesText;
    [SerializeField] private AudioSource collectionSoundEffect;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Cherry"))
        {
            collectionSoundEffect.Play();
            Destroy(collision.gameObject);
            cherries++;
            cherriesText.text = "Cherries: " + cherries;
         //   Debug.Log("Cherries: " + cherries);
        }
    }
}
//audio sound
// down sound, add component audio source, pig to component, in script khai bao AudioSource, use menthod .Play() , in unity pig component audiosound to object created