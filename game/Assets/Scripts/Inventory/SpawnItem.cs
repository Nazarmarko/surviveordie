using UnityEngine.UI;
using UnityEngine;

public class SpawnItem : MonoBehaviour
{
    public GameObject item;
    private Transform player;

    private void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void SpawnDroppedItem()
    {
        Vector2 playerPos = new Vector2(player.position.x, player.position.y + .5f);

        item.GetComponent<SpriteRenderer>().sprite = this.gameObject.GetComponent<Image>().sprite;
        Instantiate(item, playerPos, Quaternion.identity);
    }
}
