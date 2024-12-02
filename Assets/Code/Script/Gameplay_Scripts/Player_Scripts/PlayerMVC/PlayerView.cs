using Player.Model;
using Unity.VisualScripting;
using UnityEngine;

namespace Player.View
{
    [System.Serializable]
    public class PlayerView
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private SpriteRenderer _defaultRenderer;
        [SerializeField] private SpriteRenderer _climbRenderer;

        private SpriteRenderer _currentSpriteRenderer;
        private PlayerState _currentState = PlayerState.None;

        public void SetDirection(Vector2 direction)
        {
            _animator.SetFloat("X", direction.x);
            _animator.SetFloat("Y", direction.y);
            
            if (direction.x == 0f)
                return;

            if (direction.x > 0)
                _currentSpriteRenderer.flipX = true;
            else
                _currentSpriteRenderer.flipX = false;
        }

        public void SetRotation(Vector3 direction)
        {
            _defaultRenderer.flipY = direction.y > 0f;
            Vector3 newPos = _defaultRenderer.transform.localPosition;

            if (direction.y > 0f)
                newPos.y = 0.27f;
            else
                newPos.y = 1.78f;

            _defaultRenderer.transform.localPosition = newPos;
        }

        public void PlayAnimation(PlayerState state)
        {
            UpdateRenderer(state);
            
            ResetAnimator();
            _currentState = state;

            _animator.SetTrigger(_currentState.ToString());
            Debug.Log($"Trigger: {_currentState}");
        }

        //Work aroudn
        private void ResetAnimator()
        {
            _animator.ResetTrigger(PlayerState.Idle.ToString());
            _animator.ResetTrigger(PlayerState.Move.ToString());
            _animator.ResetTrigger(PlayerState.Jump.ToString());
            _animator.ResetTrigger(PlayerState.Climb.ToString());
            _animator.ResetTrigger(PlayerState.Fall.ToString());
            _animator.ResetTrigger(PlayerState.Dead.ToString());
        }

        private void UpdateRenderer(PlayerState state)
        {
            if (_currentState == state)
                return;

            switch (state)
            {
                case PlayerState.Climb:
                    _defaultRenderer.gameObject.SetActive(false);
                    _climbRenderer.gameObject.SetActive(true);

                    if(_currentSpriteRenderer != null)
                        _climbRenderer.flipX = _currentSpriteRenderer.flipX;
                    _currentSpriteRenderer = _climbRenderer;
                    break;

                default:
                    _defaultRenderer.gameObject.SetActive(true);
                    _climbRenderer.gameObject.SetActive(false);

                    if (_currentSpriteRenderer != null)
                        _defaultRenderer.flipX = _currentSpriteRenderer.flipX;
                    _currentSpriteRenderer = _defaultRenderer;
                    break;
            }
        }
    }
}
