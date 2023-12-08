using System;
using UnityEngine;
using Common;
using TeamsSystem;
using HealthSystem;
using Game;

namespace ShootingSystem
{
    internal sealed class Bullet : MonoBehaviour
    {
        private const int DEFAULT_BULLET_LAYER = 0;

        internal event Action<Bullet> OnEndOfLife;
        private int _damage;
        public Team Team { get; private set;}
        private BulletBoundary _bulletBoundary;
        private bool _isBulletActive = false;
        private Vector3 _velocityBuffer;

        internal void Activate(Vector2 direction, Vector3 position, BulletConfig bulletConfig, BulletBoundary bulletBoundary)
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = direction * bulletConfig.speed;
            Team = bulletConfig.team;
            _damage = bulletConfig.damage;
            _bulletBoundary = bulletBoundary;
            gameObject.layer = GetTeamLayer(bulletConfig.team);
            transform.position = position;
            gameObject.GetComponent<SpriteRenderer>().color = bulletConfig.color;
            _isBulletActive= true;
        }

        internal void Deactivate()
        {
            _isBulletActive = false;
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        }

        private int GetTeamLayer(Team team)
        { 
            switch (team)
            {
                case Team.ENEMY:
                    return (int)PhysicsLayer.ENEMY_BULLET;
                case Team.PLAYER:
                    return (int)PhysicsLayer.PLAYER_BULLET;
                default:
                    return DEFAULT_BULLET_LAYER;
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {            
            if (CheckCollisionWithBullets(collision)) return;
            if (CheckCollisionWithHealth(collision)) return;
        }

        private bool CheckCollisionWithBullets(Collision2D collision)
        {
            var bullet = collision.gameObject.GetComponent<Bullet>();
            if (!bullet) return false;

            if (bullet.Team == Team) return false;
            this.OnEndOfLife?.Invoke(this);
            return true;
        }

        private bool CheckCollisionWithHealth(Collision2D collision)
        {
            var health = collision.gameObject.GetComponent<HealthComponent>();
            if (!health) return false;

            var teamMember = collision.gameObject.GetComponent<TeamMember>();
            if (teamMember && Team == teamMember.Team) return false;

            health.TakeDamage(_damage);
            this.OnEndOfLife?.Invoke(this);
            return true;
        }

        public void OnFixedUpdate()
        {
            if (!enabled) return;
            if (!_bulletBoundary.InBoundaries(this.transform.position))
            {
                this.OnEndOfLife?.Invoke(this);
            }
        }

        public void OnGameResume()
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = _velocityBuffer;
            enabled = true;
        }

        public void OnGamePause()
        {
            enabled = false;
            _velocityBuffer = gameObject.GetComponent<Rigidbody2D>().velocity;
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        }
    }
}
