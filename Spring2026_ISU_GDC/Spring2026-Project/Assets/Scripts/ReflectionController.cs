using UnityEngine;

public class ReflectionController : MonoBehaviour
{
    //Y position reference for where Game Object is mirrored from
    [SerializeField] float mirrorHeight;

    //Game Object to be mirrored
    public GameObject mirrorTarget;
    private SpriteRenderer targetRenderer;

    //Reference for this Game Object's rendrer
    private SpriteRenderer myRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Get target's renderer for sprite reference
        targetRenderer = mirrorTarget.GetComponent<SpriteRenderer>();

        //Get reference to this Game Object's Sprite Renderer
        myRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //Transform this game object's position to be mirrored across mirrorHeight from the target Game Object
        //Also ensure rotation and scale matches target 
        this.transform.position = new Vector2(mirrorTarget.transform.position.x, mirrorHeight - (mirrorTarget.transform.position.y - mirrorHeight));
        this.transform.localScale = mirrorTarget.transform.lossyScale;
        this.transform.rotation = mirrorTarget.transform.rotation;

        //Change myRenderer's sprite to match the Target Game Object
        //Also ensure target is properly mirrored in the Sprite Renderer
        myRenderer.sprite = targetRenderer.sprite;
        myRenderer.flipX = targetRenderer.flipX;
        myRenderer.flipY = !targetRenderer.flipY;
    }
}
