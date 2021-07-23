using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Timers;

namespace Chapter6Game.Content.Objects
{

    
    public class RedEnemy
    {
        
      public Player player = new Player();
        public Vector2 Position;
        public float speed = 2;
        public float gravity = 2;
        public Rectangle mainBody, topofHead;
        Terrain terrain = new Terrain();
        public bool isDead;
        public SpriteAnimation anim;
        public bool ispatroling = false;
        public RedEnemy(GameRoot root)
        {
            Position = new Vector2(-300, 300);
            
        }
        public void initialize()
        {
            topofHead = new Rectangle((int)Position.X, (int)Position.Y -10, 30, 1);
            mainBody = new Rectangle((int)Position.X, (int)Position.Y, 40, 44);
            player.Initialize();
        }
        public void Update(GameTime gameTime)
        {
          // Calculates the distance betweeen player and Enemy
            float distance = MathHelper.Distance(player.position.X, Position.X);
            if (distance > -100)
            {
                ispatroling = true;
                Position.X -= speed;
            }
            mainBody.X = (int)Position.X;
            mainBody.Y = (int)Position.Y;
            topofHead.X = (int)Position.X;
            topofHead.Y = (int)Position.Y;
            if(mainBody.Intersects(player.playerRect))
            {
                player.health--;
                Debug.WriteLine("Collision");
            }
            if (mainBody.Intersects(terrain.collisionRect[0]))
            {

                
                gravity = 0;
            }

            else
            {
                gravity = 2;
            }
            anim.Position = Position;
            anim.Update(gameTime);
            Position.Y += gravity;
        }
        
    }

    public class BlueEnemy
    {
        public Player player = new Player();
        public Rectangle mainBody, topofHead;
        Terrain terrain = new Terrain();
        public Vector2 Position;
        public bool Isdead = false;
        public SpriteAnimation anim;
        public float speed = 2;
        public float gravity = 2;
        public BlueEnemy(GameRoot root)
        {
            Position = new Vector2(400, 300);
        }
        public void initialize()
        {
            topofHead = new Rectangle((int)Position.X, (int)Position.Y - 20, 10, 10);
            mainBody = new Rectangle((int)Position.X, (int)Position.Y, 40, 48);
        }
        public void Update(GameTime gameTime)
        {
           
            anim.Position = Position;
            anim.Update(gameTime);
            Position.Y += gravity;
        }
        
    }

    public class SamuraiBoss
    {
        public float gravity = 2;
        public Player Player = new Player();
        public bool isdead = false; 
        Terrain terrain = new Terrain();
        public int health = 3;
        public SpriteAnimation anim;
        public Vector2 Position;
        public Rectangle mainBody, SamuraiSlash;
        public SamuraiBoss(GameRoot root)
        {

            Position = new Vector2(875, 254);
        }
        public void Initialize()
        {
            mainBody = new Rectangle((int)Position.X, (int)Position.Y, 40, 48);
            SamuraiSlash = new Rectangle((int)Position.X - 10, (int)Position.Y, 20, 15);
        }
        public void Update(GameTime gameTime)
        {
            anim.Position = Position;
            anim.Update(gameTime);
            Position.Y += gravity;
        }
        
    }

}
