using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    //private float length; //startpos;
    private Vector2 length, startpos;
    public GameObject cam;
    public float parallaxEffect;
    [SerializeField] private bool infiniteHorizontal;
    [SerializeField] private bool infiniteVertical;

    void Start() {
        startpos.x = transform.position.x;
        startpos.y = transform.position.y;
        length.x = GetComponent<SpriteRenderer>().bounds.size.x;
        length.y = GetComponent<SpriteRenderer>().bounds.size.y;
    }

    void Update() {
        if(infiniteHorizontal){
            float temp = (cam.transform.position.x * (1 - parallaxEffect));
            float dist = (cam.transform.position.x * parallaxEffect);

            transform.position = new Vector3(startpos.x + dist, transform.position.y, transform.position.z);
            
            if(temp > startpos.x + length.x) startpos.x += length.x;
            else if (temp < startpos.x - length.x) startpos.x -= length.x;
        }

        if(infiniteVertical){
            float temp = (cam.transform.position.y * (1 - parallaxEffect));
            float dist = (cam.transform.position.y * parallaxEffect);

            transform.position = new Vector3(transform.position.x, startpos.y + dist, transform.position.z);
            
            if(temp > startpos.y + length.y) startpos.y += length.y;
            else if (temp < startpos.y - length.y) startpos.y -= length.y;
        }
    }

    /*
    private Transform cameraTransform;
    [SerializeField]
    private Camera mainCamera;
    private Vector3 lastCameraPosition;
    [SerializeField] private Vector2 parallaxEffectMultiplier;
    private float textureUnitSizeX;
    private float textureUnitSizeY;

    // Start is called before the first frame update
    void Start()
    {
        cameraTransform = mainCamera.transform;
        lastCameraPosition = cameraTransform.position;
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        Texture2D texture = sprite.texture;
        textureUnitSizeX = texture.width / sprite.pixelsPerUnit;
        textureUnitSizeY = texture.height / sprite.pixelsPerUnit;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;
        transform.position += new Vector3(deltaMovement.x * parallaxEffectMultiplier.x, deltaMovement.y * parallaxEffectMultiplier.y);
        lastCameraPosition = cameraTransform.position;

        if(Mathf.Abs(cameraTransform.position.x - transform.position.x) >= textureUnitSizeX){
            float offsetPositionX = (cameraTransform.position.x - transform.position.x) % textureUnitSizeX;
            transform.position = new Vector3(cameraTransform.position.x + offsetPositionX, transform.position.y);
        }
        if(Mathf.Abs(cameraTransform.position.y - transform.position.y) >= textureUnitSizeY){
            float offsetPositionY = (cameraTransform.position.y - transform.position.y) % textureUnitSizeY;
            transform.position = new Vector3(transform.position.x, cameraTransform.position.y + offsetPositionY);
        }
    }*/
}
