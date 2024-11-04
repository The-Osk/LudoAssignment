using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class ApiHandler : MonoBehaviour
{
    public static ApiHandler Instance;
    string url = "https://www.randomnumberapi.com/api/v1.0/random?min=1&max=7&count=1";
    //string url = "https://www.random.org/integers/?num=1&min=1&max=6&format=plain&rnd=new";
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int lastResult = 0;
    void Start()
    {
        Instance = this;
        //StartCoroutine(SendRequest());
    }

    public IEnumerator SendRequest()
    {
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();
        Debug.Log(request.downloadHandler.text);

        lastResult = request.downloadHandler.text[1] - '0'; 
    }


}
