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
        public Vector2 Position, origin;
        public float speed = 0.5f;
        public float gravity ;
        public Rectangle mainBody, topofHead;
        float[] xcoords = new float[4];
        float[] ycoords = new float[4]; 
        Terrain terrain = new Terrain();
        public bool isDead;
        public SpriteAnimation anim;
        public bool ispatroling = false;
        public RedEnemy(GameRoot root)
        {
            Position = new Vector2(-500, 350);
            origin = new Vector2(-500, 350);
        }
        public void initialize()
        {
            topofHead = new Rectangle((int)Position.X, (int)Position.Y -10, 30, 1);
            mainBody = new Rectangle((int)Position.X, (int)Position.Y, 40, 44);

            xcoords[0] = origin.X - 100;
            player.Initialize();
        }
        public void Update(GameTime gameTime)
        {
            // SinuSoid Motion
            //Enemy.pos.X -= Enemy.speed;
            // Enemy.pos.Y = (float)MathF.Sin((float)gameTime.TotalGameTime.TotalMilliseconds / 100);

            // Circular Motion
            //Position.X -= (float)MathF.Cos((float)gameTime.TotalGameTime.TotalMilliseconds / 900) * speed;
            //Position.Y += (float)MathF.Sin((float)gameTime.TotalGameTime.TotalMilliseconds / 900) * speed;




            // Calculates the distance betweeen player and Enemy
            float distance = MathHelper.Distance(player.position.X, Position.X);

            
            float distancebtwnX = MathHelper.Distance(Position.X, origin.X);
            float distancebtwnY = MathHelper.Distance(Position.Y, origin.Y);
            Debug.WriteLine(distancebtwnX);
             if (distancebtwnX < 100)
            {
                Position.X -= speed;
            }

          else  if (distancebtwnX >= 100){
                Position.Y -= speed + 2;
                Position.X -= 0;
            }
           
            //if (distance > -100)
            //{
            //    ispatroling = true;
            //    Position.X -= speed;
                
            //    Debug.WriteLine(Position);
            //}  


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

            if (topofHead.Intersects(player.playerRect ) && player.hasjumped)
            {
                gravity = 0;
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
