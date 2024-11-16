using Player.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.View
{
    public class PlayerView
    {
        private Animator _animator;
        private SpriteRenderer _spriteRenderer;

        public PlayerView(Animator animator, SpriteRenderer spriteRenderer)
        {
            _animator = animator;
            _spriteRenderer = spriteRenderer;
        }

        public void SetDirection(Vector2 direction)
        {
            _animator.SetFloat("X", direction.x);
            _animator.SetFloat("Y", direction.y);

            if(direction.x < 0 && !_spriteRenderer.flipX)
                _spriteRenderer.flipX = !_spriteRenderer.flipX;
        }

        public void PlayAnimation(PlayerState state) => _animator.SetTrigger(state.ToString());
    }
}
