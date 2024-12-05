using UnityEngine;
using Player.Model;

namespace Player.View
{
    [System.Serializable]
    public class PlayerView
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private SpriteRenderer _defaultRenderer;
        [SerializeField] private SpriteRenderer _climbRenderer;
        [SerializeField] private SpriteRenderer _deathRenderer;

        private SpriteRenderer _currentSpriteRenderer;
        private PlayerState _currentState = PlayerState.None;

        private bool _canUpdate = true;

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
            if(!_canUpdate) 
                return;

            _currentSpriteRenderer.flipY = direction.y > 0f;

            float yPos = _defaultRenderer.flipY ? 0.27f: 1.78f;
            _defaultRenderer.transform.localPosition = new Vector3(_defaultRenderer.transform.localPosition.x, yPos, 0f);
        }

        public void PlayAnimation(PlayerState state)
        {
            if(!_canUpdate)
                return;

            UpdateRenderer(state);
            
            //ResetAnimator();
            _animator.ResetTrigger(_currentState.ToString());
            _currentState = state;
            _animator.SetTrigger(_currentState.ToString());
        }

        //Work around
        private void ResetAnimator()
        {
            if(_currentState == PlayerState.Move)
                _animator.ResetTrigger(PlayerState.Fall.ToString());

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
                    
                case PlayerState.Dead:
                    _defaultRenderer.gameObject.SetActive(false);
                    _climbRenderer.gameObject.SetActive(false);
                    _deathRenderer.gameObject.SetActive(true);

                    _currentSpriteRenderer = _deathRenderer;
                    _canUpdate = false;
                    break;

                default:
                    _defaultRenderer.gameObject.SetActive(true);
                    _climbRenderer.gameObject.SetActive(false);

                    if (_currentSpriteRenderer != null)
                    {
                        _defaultRenderer.flipX = _currentSpriteRenderer.flipX;
                        _defaultRenderer.flipY = _currentSpriteRenderer.flipY;
                    }

                    _currentSpriteRenderer = _defaultRenderer;
                    break;
            }
        }
    }
}
