using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public Text VerticalText;
    private int _verticalValue = 0;

    public Text HorisontalText;
    private int _horisontalValue = 0;

    public Text DiagonalText;
    private int _diagonalValue = 0;

    public Text ScoreText;
    private int _scoresNeeded = GObjAllocator.WinScore;
    private int _currentScores = 0;

    public GameObject WinPopup;


    private void Awake()
    {
        Messenger<Figure>.AddListener(InstanceEvents.OnAdding.ToString(), OnAdding);

        Messenger<Figure>.AddListener(InstanceEvents.OnDeath.ToString(), OnDeath);

        WinPopup.SetActive(false);

        UpdateText();
    }
    private void OnDestroy()
    {
        Messenger<Figure>.RemoveListener(InstanceEvents.OnAdding.ToString(), OnAdding);

        Messenger<Figure>.RemoveListener(InstanceEvents.OnDeath.ToString(), OnDeath);
    }

    private void OnAdding(Figure figure)
    {
        switch (figure.moveType)
        {
            case MoveType.diagonal: _diagonalValue++; break;
            case MoveType.horisontal: _horisontalValue++; break;
            case MoveType.vertical: _verticalValue++; break;
        }

        UpdateText();
    }

    private void OnDeath(Figure figure)
    {
        switch (figure.moveType)
        {
            case MoveType.diagonal: _diagonalValue--; break;
            case MoveType.horisontal: _horisontalValue--; break;
            case MoveType.vertical: _verticalValue--; break;
        }


        UpdateText();
    }

    private void UpdateText()
    {
        if ((VerticalText==null)||(HorisontalText == null) || (DiagonalText == null) || (ScoreText == null)) return;

        _currentScores = System.Math.Min(_diagonalValue,_horisontalValue);
        _currentScores = System.Math.Min(_currentScores, _verticalValue);

        VerticalText.text = _verticalValue.ToString();
        HorisontalText.text = _horisontalValue.ToString();
        DiagonalText.text = _diagonalValue.ToString();

        ScoreText.text = _currentScores.ToString() + "/" + _scoresNeeded.ToString();

        if (_scoresNeeded == _currentScores)
        {
            Debug.Log("You Win!");

            WinPopup.SetActive(true);
           // Messenger.Broadcast(GameEvents.Win.ToString());
        }
    }

    public void Refresh()
    {
        SceneManager.LoadScene(0);
    }

}
