using UnityEngine;

//this is an editor script to genrate cells eveningly, it doesn't effect the game
public class CellGen : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    [SerializeField] Vector3 start;
    public float yShift = 1.27f;
    public float xShift = 1.27f;
    Vector3 lastCell;

    [ContextMenu("Test")]
    public void test()
    {
        Debug.Log(GetComponent<SpriteRenderer>().sprite.bounds);
    }

    [ContextMenu("gen")]
    public void Gen()
    {
        xShift = GetComponent<SpriteRenderer>().sprite.bounds.size.x / 15;
        yShift = GetComponent<SpriteRenderer>().sprite.bounds.size.y / 15;
        Transform parentObject = new GameObject().transform;
        for (int i = 0; i < 6; i++)
        {
            Vector3 v = new Vector3(start.x, start.y + yShift * i, start.z);
            GameObject g = Instantiate(prefab, parentObject);
            g.transform.position = v;
            lastCell = g.transform.position;
        }

        lastCell = new Vector3(lastCell.x, lastCell.y + yShift, lastCell.z);

        for (int i = 1; i <= 6; i++)
        {
            Vector3 v = new Vector3(lastCell.x - xShift, lastCell.y, lastCell.z);
            GameObject g = Instantiate(prefab, parentObject);
            g.transform.position = v;
            lastCell = g.transform.position;
        }

        for (int i = 1; i <= 2; i++)
        {
            Vector3 v = new Vector3(lastCell.x, lastCell.y + yShift, lastCell.z);
            GameObject g = Instantiate(prefab, parentObject);
            g.transform.position = v;
            lastCell = g.transform.position;
        }

        for (int i = 1; i <= 5; i++)
        {
            Vector3 v = new Vector3(lastCell.x + xShift, lastCell.y, lastCell.z);
            GameObject g = Instantiate(prefab, parentObject);
            g.transform.position = v;
            lastCell = g.transform.position;
        }

        lastCell = new Vector3(lastCell.x + xShift, lastCell.y, lastCell.z);

        for (int i = 1; i <= 6; i++)
        {
            Vector3 v = new Vector3(lastCell.x , lastCell.y + yShift, lastCell.z);
            GameObject g = Instantiate(prefab, parentObject);
            g.transform.position = v;
            lastCell = g.transform.position;
        }

        for (int i = 1; i <= 2; i++)
        {
            Vector3 v = new Vector3(lastCell.x + xShift, lastCell.y, lastCell.z);
            GameObject g = Instantiate(prefab, parentObject);
            g.transform.position = v;
            lastCell = g.transform.position;
        }

        for (int i = 1; i <= 5; i++)
        {
            Vector3 v = new Vector3(lastCell.x, lastCell.y - yShift, lastCell.z);
            GameObject g = Instantiate(prefab, parentObject);
            g.transform.position = v;
            lastCell = g.transform.position;
        }

        lastCell = new Vector3(lastCell.x , lastCell.y - yShift, lastCell.z);

        for (int i = 1; i <= 6; i++)
        {
            Vector3 v = new Vector3(lastCell.x + xShift, lastCell.y, lastCell.z);
            GameObject g = Instantiate(prefab, parentObject);
            g.transform.position = v;
            lastCell = g.transform.position;
        }

        for (int i = 1; i <= 2; i++)
        {
            Vector3 v = new Vector3(lastCell.x, lastCell.y - yShift, lastCell.z);
            GameObject g = Instantiate(prefab, parentObject);
            g.transform.position = v;
            lastCell = g.transform.position;
        }

        for (int i = 1; i <= 5; i++)
        {
            Vector3 v = new Vector3(lastCell.x - xShift, lastCell.y , lastCell.z);
            GameObject g = Instantiate(prefab, parentObject);
            g.transform.position = v;
            lastCell = g.transform.position;
        }

        lastCell = new Vector3(lastCell.x - xShift, lastCell.y, lastCell.z);

        for (int i = 1; i <= 6; i++)
        {
            Vector3 v = new Vector3(lastCell.x, lastCell.y - yShift, lastCell.z);
            GameObject g = Instantiate(prefab, parentObject);
            g.transform.position = v;
            lastCell = g.transform.position;
        }

        for (int i = 1; i <= 1; i++)
        {
            Vector3 v = new Vector3(lastCell.x - xShift, lastCell.y, lastCell.z);
            GameObject g = Instantiate(prefab, parentObject);
            g.transform.position = v;
            lastCell = g.transform.position;
        }

        for (int i = 1; i <= 5; i++)
        {
            Vector3 v = new Vector3(lastCell.x, lastCell.y + yShift, lastCell.z);
            GameObject g = Instantiate(prefab, parentObject);
            g.transform.position = v;
            lastCell = g.transform.position;
        }
    }

    /*
    [ContextMenu("gen")]
    public void Gen()
    {
        for(int i =1; i <= 6; i++)
        {
            Vector3 v = new Vector3(start.x, start.y + (float)1.27*i, start.z);
            GameObject g = Instantiate(prefab);
            g.transform.position = v;
        }
    }*/
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
