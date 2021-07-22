using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;


namespace MiniMan.Content.Objects
{
    public class Particle
    {
        public Texture2D Image { get; set; }        
        public Vector2 Position { get; set; }       
        public Vector2 Velocity { get; set; }        
        public float Angl { get; set; }            
        public float AngularVelocity { get; set; }   
        public Color Color { get; set; }            
        public float Size { get; set; }                
        public int LifeTime { get; set; }  

        public Particle(Texture2D image, Vector2 pos, Vector2 velocity,
            float angl, float angularVelocity, Color color, float size, int lifeTime)
        {
           Image = image;
            Position = pos;
            Velocity = velocity;
            Angl = angl;
            AngularVelocity = angularVelocity;
            Color = color;
            Size = size;
            LifeTime = lifeTime;
        }

        public void Update()
        {
            LifeTime--;
            Position += Velocity;
            Angl += AngularVelocity;
        }


        public void Draw(SpriteBatch sprite)
        {
            Rectangle sourceRec = new Rectangle(0, 0, Image.Width, Image.Height);
            Vector2 origin = new Vector2(Image.Width / 2, Image.Height / 2);

            sprite.Draw(Image, Position, sourceRec, Color,
                Angl, origin, Size, SpriteEffects.None, 0f);
        }

    }
    public class ParticleEngine
    {
        private Random rand;
        public Vector2 EmitterLoc { get; set; }
        private List<Particle> particles;
        private List<Texture2D> images;


        public ParticleEngine(List<Texture2D> images, Vector2 emitterLoc)
        {
            EmitterLoc =  emitterLoc;
            this.images = images;
            this.particles = new List<Particle>();
            rand = new Random();
        }

        public void Update()
        {
            int maxParticles = 2;

            for (int i = 0; i < maxParticles; i++)
            {
                particles.Add(NewParticle());
            }

            for (int particle = 0; particle < particles.Count; particle++)
            {
                particles[particle].Update();
                if (particles[particle].LifeTime <= 0)
                {
                    particles.RemoveAt(particle);
                    particle--;
                }
            }
        }

        private Particle NewParticle()
        {
            Texture2D texture = images[rand.Next(images.Count)];
            Vector2 emitterLocation = EmitterLoc;
            Vector2 velocity = new Vector2(
                                    1f * (float)(rand.NextDouble() * 1 - 1),
                                    1f * (float)(rand.NextDouble() * 1 - 1));
            float angle = 1;
            float angularVelocity = 0.1f * (float)(rand.NextDouble() * 1 - 1);
            Color color = new Color(
                        (float)rand.NextDouble(),
                        (float)rand.NextDouble(),
                        (float)rand.NextDouble());
            float size = (float)rand.NextDouble();
            int LifeTime = 6 + rand.Next(5);
            return new Particle(texture, emitterLocation, velocity, angle, angularVelocity, color, size, LifeTime);
        }

        public void Draw(SpriteBatch sprite)
        {
            for (int index = 0; index < particles.Count; index++)
            {
                particles[index].Draw(sprite);
            }
        }
    }
}
