using UnityEngine;

public class RoadMover : MonoBehaviour, IGameStartListener, IGamePausedListener, IGameResumeListener, IGameFinishListener
{
    [SerializeField] private Transform _segment1;
    [SerializeField] private Transform _segment2;

    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _replacementOffset;
    [SerializeField] private float _segmentSize;

    private bool _isMoving;

    public void OnGameFinished()
    {
        _isMoving = false;
    }

    public void OnGamePaused()
    {
        _isMoving = false;
    }

    public void OnGameResumed()
    {
        _isMoving = true;
    }

    public void OnGameStarted()
    {
        _isMoving = true;
    }

    private void Awake()
    {
        GameManager.Shared.AddListener(this);
    }

    private void Update()
    {
        if (!_isMoving) return;

        MoveRoads();
    }

    private void MoveRoads()
    {
        Vector3 translationVector = new Vector3(0f, 0f, _moveSpeed * Time.deltaTime);
        _segment1.Translate(translationVector);
        _segment2.Translate(translationVector);

        if (_segment1.position.z < _replacementOffset)
        {
            _segment1.position = new Vector3(_segment2.position.x, _segment2.position.y, _segment2.position.z + _segmentSize);
            (_segment1, _segment2) = (_segment2, _segment1);
        }
    }
}
