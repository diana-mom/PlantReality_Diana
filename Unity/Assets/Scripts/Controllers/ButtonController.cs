using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    [SerializeField]
    Text text;
    Ray ray;
    RaycastHit hit;
    Animator anim;

    // Time (in seconds) to read the text.
    public float time = 5; 

    // Start is called before the first frame update.
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame.
    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (Input.GetMouseButtonDown(0))
            {
                
                anim.SetTrigger("ButtonClick");
                StartCoroutine(Buy());
                anim.SetTrigger("ButtonRelease");
            }
        }
    }
    
    // Send post request to server, which sets purchase in the database.
    IEnumerator Buy()
    {
        WWWForm form = new WWWForm();
        form.AddField("amount", 1);
        form.AddField("auction_flower_id", 1);
        form.AddField("pincode", "1234");

        // Price of Clock item.
        form.AddField("price", 100); 
        form.AddField("usercode", "1234");

        UnityWebRequest request = UnityWebRequest.Post("http://localhost:8080/plantreality/purchase.php", form);
        yield return request.SendWebRequest();

        // Request send and received, check for errors or use data.
        Debug.Log(request.downloadHandler.text);

        // Check that gives some text when the request succeedes.
        if (request.downloadHandler.isDone)
        {
            text.text = "Uw aankoop is geslaagd";
            yield return new WaitForSeconds(time);
            text.text = "";
        }
        else
        {
            text.text = "Er gaat iets fout, probeert u het later nog een keer";
            yield return new WaitForSeconds(time);
            text.text = "";
        }

    }

    // Get currently Active auction item. 
    string GetCurrentAuctionItem(int auction_id)
    {
        WWWForm form = new WWWForm();
        form.AddField("auction_id", auction_id);
        UnityWebRequest request = UnityWebRequest.Post("http://localhost:8080/plantreality/getCurrentAuctionFlower.php?", form);
        request.SendWebRequest();
        return request.downloadHandler.text;
    }
}
