using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPortal : MonoBehaviour
{
    [SerializeField] private CanvasGroup fade;

    private IEnumerator IELoadDungeon()
    {
        fade.gameObject.SetActive(true);
        StartCoroutine(Helpers.IEFade(fade, 1f, 2f));
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene("Dungeon");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(IELoadDungeon());
        }
    }
}
