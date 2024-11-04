using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class ApiHandler : MonoBehaviour
{
    public static ApiHandler Instance;
    //RandomNumberApi max value is exclusive
    //There is the option of requesting more than one number to minimize api calls, but i decided to keep this part simple
    string url = "https://www.randomnumberapi.com/api/v1.0/random?min=1&max=7&count=1";

    //www.random.org kept giving error code 503, and the website says it's a common problem

    public int lastResult = 0;
    void Start()
    {
        Instance = this;
    }

    //Send the request and stores it
    public IEnumerator SendRequest()
    {
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();
        Debug.Log(request.downloadHandler.text);

        lastResult = request.downloadHandler.text[1] - '0'; 
    }


}
