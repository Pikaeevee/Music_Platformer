using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuffIconManager : MonoBehaviour
{
    public GameObject[] buffIcons;
    public AudioClip buffSound;
    public AudioClip debuffSound;
    private AudioSource player;

    void Start() {
        player = GetComponent<AudioSource>();
    }
    
    IEnumerator startIconTimer(int iconIndex, float buffTime) {
        player.clip = buffSound;
        player.Play();
        buffIcons[iconIndex].SetActive(true);
        buffIcons[iconIndex].GetComponent<Animator>().ResetTrigger("FadeIcon");
        buffIcons[iconIndex].GetComponent<Animator>().ResetTrigger("BlinkIcon");
        yield return new WaitForSeconds(buffTime * 0.7f);
        buffIcons[iconIndex].GetComponent<Animator>().SetTrigger("BlinkIcon");
        yield return new WaitForSeconds(buffTime * 0.3f);
        buffIcons[iconIndex].GetComponent<Animator>().SetTrigger("FadeIcon");
        yield return new WaitForSeconds(0.5f);
        buffIcons[iconIndex].SetActive(false);
        player.clip = debuffSound;
        player.Play();
    }

    public void showIcon(int iconIndex, float buffTime) {
      StartCoroutine(startIconTimer(iconIndex, buffTime));
    }
}
