using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomMove : MonoBehaviour
{

	public Vector2 cameraChange;
	public Vector3 playerChange;
	private CamMove cam;

	public bool needText;
	public string placeName;
	public GameObject text;

	public Text placeText;


    // Start is called before the first frame update


	void Start(){
		cam = Camera.main.GetComponent<CamMove>();
	}
    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
    	

    	if(other.CompareTag("Player") && !other.isTrigger){
    		cam.minPosition += cameraChange;
    		cam.maxPosition += cameraChange;
    		other.transform.position += playerChange;

    		if(needText){
    			StartCoroutine(placeNameCo());
    		}
    	}
    }

    private IEnumerator placeNameCo(){
    	text.SetActive(true);
    	placeText.text = placeName;
    	placeText.GetComponent<Text>().CrossFadeAlpha(0, 3f, false); // Adds fade to text
    	yield return new WaitForSeconds(4f);
    	text.SetActive(false);
    }

}
