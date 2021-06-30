using Microsoft.Xna.Framework;
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
        public Rectangle enemyRect;
        public Enemy()
        {
            
  
        }

        public static List<Enemy> enemies = new List<Enemy>();

        public Enemy(Rectangle rectangle, float Speed)
        {
            enemyRect = rectangle;
            
        }

        public void Update (GameTime gameTime)
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
        
        public RedEnemy()
        {
            Position = new Vector2(200, 300);
           
        }
        public void Initialize()
        {
            
            enemyRect = new Rectangle((int)Position.X, (int)Position.Y, 48, 33);
        }
      public void Update(GameTime gameTime)
        {
           
        }
     
    }

    public class BlueEnemy: Enemy
    {
        
        public Rectangle Body;
        
        public void Update(GameTime gameTime)
        {

           
            // float distance = MathHelper.Distance(playerFunctions.position.X, Position.X);
            //if (distance < 20)
            //{
            //    //add patrol logic
            //}


        }
    }
    public class SamuraiBoss: Enemy
    {
        public Vector2 Position;
        public int health = 3;
        public Rectangle SwordAttack;
        Timer timer;
        public void OnHit()
        {

        }
    }


}
