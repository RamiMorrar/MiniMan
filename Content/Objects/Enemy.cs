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
        // Used to handle enemies currently active
        //public List<Enemy> enemies = new List<Enemy>();
        public float Gravity = 2;
        public Vector2 Position;
       public bool enemyFlip;
     public   float health;
        public SpriteAnimation anim;
        bool isdead;
        // public Player playerFunctions = new Player();
        public Rectangle enemyRect;
        public Enemy()
        {
            isdead = false;
            
            

        }

        public static List<Enemy> enemies = new List<Enemy>();

        public Enemy(Rectangle rectangle, float Speed)
        {
            this.enemyRect = rectangle;
            this.speed = Speed;
        }

        

        public void Update (GameTime gameTime)
        {
            
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
            Position.Y += Gravity;
        }
     
    }

    public class BlueEnemy: Enemy
    {
        
        public Rectangle headHorn, Body;
        
        public void Update(GameTime gameTime)
        {

            Position.Y += Gravity;
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

        public Rectangle SwordAttack;
        Timer timer;
        public void OnHit()
        {

        }
    }


}
