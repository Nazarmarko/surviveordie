using UnityEditor;
using UnityEngine;

public class GroundItem : MonoBehaviour, ISerializationCallbackReceiver
{
    public ItemObject item;

    public void OnAfterDeserialize()
    {

    }
    void Start()
    {
        Debug.Log(":");
        GetComponentInChildren<SpriteRenderer>().sprite = item.uiDisplay;
    }
    public void OnBeforeSerialize()
    {
        GetComponentInChildren<SpriteRenderer>().sprite = item.uiDisplay;

        //SetDirty EditorUtility.SetDirty(GetComponentInChildren<SpriteRenderer>()); 
        Undo.RecordObject(GetComponentInChildren<SpriteRenderer>(), null);
    }
}
