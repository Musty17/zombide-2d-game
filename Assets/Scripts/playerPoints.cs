using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class playerPoints : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textM;
    private AudioSource auido;
    private void Awake()
    {
        auido = GetComponent<AudioSource>();
        _textM.text = score.totalScore.ToString();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("diamond")) 
        {
            Destroy(collision.gameObject);
            score.totalScore++;
            auido.Play();
            _textM.text = score.totalScore.ToString();  //int to string
        }
    }
}
