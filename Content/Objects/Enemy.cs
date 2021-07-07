﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace Chapter6Game.Content.Objects
{
    public class Enemy
    {
         
        public float Gravity = 2;
        public Vector2 Position;
       public bool enemyFlip;
        Terrain terrain = new Terrain();

        // public Player playerFunctions = new Player();
        float speed;
        public SpriteAnimation anim;
        public Rectangle enemyRect;
        
        public static List<Enemy> enemies = new List<Enemy>();

        public Enemy(Rectangle rectangle, float Speed)
        {
            enemyRect = rectangle;
            speed = Speed;
          
        }

        public new virtual void Update (GameTime gameTime)
        {
            Position.Y += Gravity;

            HandleEnemyCollisions();
        }

        public void HandleEnemyCollisions() {
            if (enemyRect.Intersects(terrain.collisionRect[2]))
            {
                Gravity = 0;
            }
            if (enemyRect.Intersects(terrain.collisionRect[1]))
            {
                Gravity = 0;
            }
            if (enemyRect.Intersects(terrain.collisionRect[3]))
            {
                Gravity = 0;
            }
            if (enemyRect.Intersects(terrain.collisionRect[4]))
            {
                Gravity = 0;
            }

            if (enemyRect.Intersects(terrain.collisionRect[0]))
            {
                Gravity = 0;
            }
        }

    }

    public class RedEnemy : Enemy
    {
        public Rectangle topofHead;
        SpriteAnimation redAnim;
        public RedEnemy(Rectangle rectangle, float Speed) :base(rectangle, Speed)
        {
            Position = new Vector2(200, 300);
            enemyRect = new Rectangle((int)Position.X, (int)Position.Y,48,12 );
            redAnim = anim;
        }
        public void Initialize()
        {
            
            enemyRect = new Rectangle((int)Position.X, (int)Position.Y, 48, 33);
        }
      public override void Update(GameTime gameTime)
        {
           
        }
     
    }

    public class BlueEnemy : Enemy
    {
        
        public Rectangle Body;
        public BlueEnemy(Rectangle rectangle, float Speed) :base(rectangle, Speed)
        {

        }
        public override void Update(GameTime gameTime)
        {


            // float distance = MathHelper.Distance(playerFunctions.position.X, Position.X);
            //if (distance < 20)
            //{
            //    //add patrol logic
            //}

            base.Update(gameTime);
        }
    }
    public class SamuraiBoss : Enemy
    {
       public SamuraiBoss(Rectangle rectangle, float Speed) :base(rectangle,Speed)
        {

        }
        public void Update(GameTime gameTime)
        {

        }
        public Vector2 Position;
        public int health = 3;
        public Rectangle SwordAttack;
      
        public void OnHit()
        {

        }
    }


}
