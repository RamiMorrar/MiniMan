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


        public bool enemyFlip;
        public float speed;
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

        
        public Vector2 position;
        
      public Rectangle topofHead;
     
     public void Initialize()
        {
            enemyRect = new Rectangle((int)position.X, (int)position.Y, 48, 13);
        }
       public void Update()
        {
            
        }
    }

    public class BlueEnemy: Enemy
    {
        
        public Vector2 Position;
        public Rectangle headHorn;
        

      
        public void Update(GameTime gameTime)
        {
           
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
