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
    public int currentPos = 0;
    //Start and end position for the blue pieces
    public int startPos = 1;
    public int endPos = 57;

    public GameObject player;
    //click blocker to prevent double inputs
    public GameObject clickBlocker;

    private bool canPlayerMove = false;

    //pity counter for the start of the game, the player will get a 6 by the 3rd roll  
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
        //The reset moves the piece to the start position and set the current position to zero
        currentPos = 0;
        rollPity = 0;
        MoveToStartTween();
    }

    [ContextMenu("Roll")]
    public void Roll()
    {
        StartCoroutine(GetRollResult());
        //MoveTween(ApiHandler.Instance.lastResult);
    }

    //Enables the click blocker, request a roll form the api, and then handle the roll result
    IEnumerator GetRollResult()
    {
        clickBlocker.SetActive(true);
        yield return StartCoroutine(ApiHandler.Instance.SendRequest());
        rollResult = ApiHandler.Instance.lastResult;

        //handles the case of the starting position for the piece
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
        }//handles the final roll for the piece
        else if(currentPos + rollResult == endPos)
        {
            StartCoroutine(RollDiceAnimation(false, rollResult, true));
        }//handles the case where the result of the roll exceeds the available cells for the piece
        else if(currentPos + rollResult > endPos)
        {
            StartCoroutine(RollDiceAnimation(true, rollResult));
        }//handles the normal case for the piece
        else
        {
            StartCoroutine(RollDiceAnimation(false, rollResult));
        }
    }

    int moveLength = 0;

    //The Roll animation is changing between the 6 sides of the dice
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
        //the final side is the number given by the api
        diceRend.sprite = diceSides[rollResult - 1];

        //In case the Roll failed (not a six at the start, or the given value is too big at the end of the track) 
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
