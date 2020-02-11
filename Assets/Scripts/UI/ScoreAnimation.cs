using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreAnimation : MonoBehaviour {
    
    [SerializeField]
    private Color greyColor;
    [SerializeField]
    private Color redColor;
    [SerializeField]
    private TextMeshProUGUI _ones;
    [SerializeField]
    private TextMeshProUGUI _tens;
    [SerializeField]
    private TextMeshProUGUI _hundreds;
    [SerializeField]
    private TextMeshProUGUI _thousands;
    [SerializeField]
    private TextMeshProUGUI _tenThousands;

    private List<TextMeshProUGUI> _scoreNumbers;
    private List<TextMeshProUGUI> _redScoreNumbers = new List<TextMeshProUGUI>();

    [SerializeField]
    private AnimationCurve _animationCurve;
    [SerializeField]
    private Vector3 _maxScale;
    [SerializeField]
    private float _animationSpeed;

    private float _timer = 1f;
    private Vector3 _startScale;
    private List<Transform> _scoreNumberTransforms;

    void Start () {
        _timer = 1f;
        _scoreNumbers = new List<TextMeshProUGUI>
        {
            _ones,
            _tens,
            _hundreds,
            _thousands,
            _tenThousands
        };
        _scoreNumberTransforms = new List<Transform>();
        foreach(var scoreNumber in _scoreNumbers)
        {
            _scoreNumberTransforms.Add(scoreNumber.GetComponent<Transform>());
        }
        _startScale = _scoreNumberTransforms[0].localScale;
        EventManager.StartListening("ScoreChanged", UpdateScore);
	}

    private void UpdateScore(string scoreText)
    {
        int score = int.Parse(scoreText);
        if(score > 99999)
        {
            score = 99999;
            scoreText = "99999";
        }

        UpdateScoreNumber(score, 1, _ones);
        UpdateScoreNumber(score, 10, _tens);
        UpdateScoreNumber(score, 100, _hundreds);
        UpdateScoreNumber(score, 1000, _thousands);
        UpdateScoreNumber(score, 10000, _tenThousands);

        for (int i = 0; i < _redScoreNumbers.Count && i < scoreText.Length; i++)
        {
            _redScoreNumbers[_redScoreNumbers.Count - 1 - i].text = scoreText[i].ToString();
        }
        _timer = 0f;
    }

    private void UpdateScoreNumber(int score, int number, TextMeshProUGUI scoreNumber)
    {
        if(score >= number)
        {
            if(!_redScoreNumbers.Contains(scoreNumber))
                _redScoreNumbers.Add(scoreNumber);
            scoreNumber.color = redColor;
        }
        else
        {
            if(_redScoreNumbers.Contains(scoreNumber))
                _redScoreNumbers.Remove(scoreNumber);
            scoreNumber.color = greyColor;
        }
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.T))
        {
            EventManager.TriggerEvent("ScoreChanged", "1234");
        }
        if(_timer < 1f)
        {
            for (int i = 0; i < _redScoreNumbers.Count; i++)
            {
                _scoreNumberTransforms[i].localScale = Vector3.Lerp(_startScale, _maxScale, _animationCurve.Evaluate(_timer));
            }
            _timer += _animationSpeed * CustomTime.GetDeltaTime();
        }
        else if(_timer > 1f)
        {
            _timer = 1f;
            foreach(var trans in _scoreNumberTransforms)
            {
                trans.localScale = _startScale;
            }
        }
    }
}
