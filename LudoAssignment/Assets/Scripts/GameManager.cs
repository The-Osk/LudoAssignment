using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Cell[] cells;
    public Cell startCell;
    public Cell endCell;
    public int x = 1;
    public int currentPos = 0;
    public int startPos = 1;
    public int endPos = 57;
    public GameObject player;
    public GameObject clickBlocker;

    private bool canPlayerMove = false;

    int rollPity = 0;
    int rollResult;

    [SerializeField] Sprite[] diceSides;
    [SerializeField] SpriteRenderer diceRend;


    private void Start()
    {
        Instance = this;
        clickBlocker.SetActive(false);
    }

    [ContextMenu("Reset")]
    public void Reset()
    {
        currentPos = 0;
        rollPity = 0;
        MoveToStartTween();

        //MoveTween(ApiHandler.Instance.lastResult);
    }

    [ContextMenu("Roll")]
    public void Roll()
    {
        StartCoroutine(GetRollResult());
        //MoveTween(ApiHandler.Instance.lastResult);
    }

    IEnumerator GetRollResult()
    {
        clickBlocker.SetActive(true);
        yield return StartCoroutine(ApiHandler.Instance.SendRequest());
        rollResult = ApiHandler.Instance.lastResult;

        if (currentPos == 0)
        {
            if (ApiHandler.Instance.lastResult == 6)
            {
                StartCoroutine(RollDiceAnimation(false, 1));
            }
            else if (rollPity >= 2)
            {
                rollResult = 6;
                StartCoroutine(RollDiceAnimation(false, 1));
            }
            else
            {
                StartCoroutine(RollDiceAnimation(true, 0));
                rollPity++;
            }
        }
        else if(currentPos + rollResult == endPos)
        {
            StartCoroutine(RollDiceAnimation(false, rollResult, true));
        }
        else if(currentPos + rollResult > endPos)
        {
            StartCoroutine(RollDiceAnimation(true, rollResult));
        }
        else
        {
            StartCoroutine(RollDiceAnimation(false, rollResult));
        }
    }

    int moveLength = 0;
    IEnumerator RollDiceAnimation(bool FailedRoll, int moveLength, bool endingMove = false)
    {
        this.moveLength = moveLength;
        int randomDiceSide = 0;

        diceRend.transform.DOScale(3f, 0.5f).SetEase(Ease.OutSine).OnComplete(() => diceRend.transform.DOScale(2f, 0.5f));
        for (int i = 0; i < 20; i++)
        {
            randomDiceSide = Random.Range(0, 5);

            diceRend.sprite = diceSides[randomDiceSide];

            yield return new WaitForSeconds(0.05f);
        }
        diceRend.sprite = diceSides[rollResult - 1];
        if (!FailedRoll)
        {
            if (endingMove)
            {
                yield return new WaitForSeconds(0.5f);
                MoveToEndTween(moveLength);
            }
            else 
            {
                yield return new WaitForSeconds(0.5f);
                canPlayerMove = true;
                
            }
        }
        clickBlocker.SetActive(false);
    }

    public void MovePlayer()
    {
        if (canPlayerMove)
        {
            canPlayerMove = false;
            MoveTween(moveLength);
        }
    }

    public void MoveTween(int cellsMoved)
    {
        
        if(cellsMoved <= 0)
        {
            return;
        }
        player.transform.DOJump(cells[currentPos + 1].transform.position,  0.4f, 1, 0.5f).OnComplete(() => MoveTween(cellsMoved - 1));
        currentPos += 1;
    }

    void MoveToEndTween(int cellsMoved)
    {
        if (cellsMoved <= 0)
        {
            return;
        }
        player.transform.DOJump(endCell.transform.position, 0.6f, 1, 0.7f).OnComplete(() => clickBlocker.SetActive(false));
        currentPos = endPos;
        Debug.Log("WINNNN");
    }

    void MoveToStartTween()
    {
        player.transform.DOJump(startCell.transform.position, 0.6f, 1, 0.7f).OnComplete(() => clickBlocker.SetActive(false));
    }
}
