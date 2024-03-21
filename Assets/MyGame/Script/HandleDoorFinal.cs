using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class HandleDoorFinal : MonoBehaviour
{
    [SerializeField] private List<SpriteRenderer> sprites;
    [SerializeField] private SpriteRenderer curSprite;
    [SerializeField] private BoxCollider2D curCollider2D;
    [SerializeField] private CapsuleCollider2D colliderInteract2D;

    [SerializeField] private PlayableDirector playableDirector;


    [SerializeField] private Player player;


    private void Awake()
    {
        player = GameObject.FindObjectOfType<Player>();
        curCollider2D = GetComponent<BoxCollider2D>();
        colliderInteract2D = GetComponent<CapsuleCollider2D>();
        curSprite = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        curCollider2D.enabled = false;
        colliderInteract2D.isTrigger = true;
        curSprite.sprite = sprites[0].sprite;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {   
        if(collision.gameObject == player.gameObject)
        {
            playableDirector.Play();

            curCollider2D.enabled = true;
            colliderInteract2D.enabled = false;
            curSprite.sprite = sprites[1].sprite;
        }
      
    }
}
