using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;


namespace MiniMan.Content.Objects
{
    public class Particle
    {
        public Texture2D Texture { get; set; }        
        public Vector2 Position { get; set; }       
        public Vector2 Velocity { get; set; }        
        public float Angl { get; set; }            
        public float AngularVelocity { get; set; }   
        public Color Color { get; set; }            
        public float Size { get; set; }                
        public int TTL { get; set; }  

        public Particle(Texture2D texture, Vector2 pos, Vector2 velocity,
            float angl, float angularVelocity, Color color, float size, int ttl)
        {
            Texture = texture;
            Position = pos;
            Velocity = velocity;
            Angl = angl;
            AngularVelocity = angularVelocity;
            Color = color;
            Size = size;
            TTL = ttl;
        }

        public void Update()
        {
            TTL--;
            Position += Velocity;
            Angl += AngularVelocity;
        }


        public void Draw(SpriteBatch sprite)
        {
            Rectangle sourceRectangle = new Rectangle(0, 0, Texture.Width, Texture.Height);
            Vector2 origin = new Vector2(Texture.Width / 2, Texture.Height / 2);

            sprite.Draw(Texture, Position, sourceRectangle, Color,
                Angl, origin, Size, SpriteEffects.None, 0f);
        }

    }
    public class ParticleEngine
    {
        private Random rand;
        public Vector2 EmitterLoc { get; set; }
        private List<Particle> particles;
        private List<Texture2D> textures;


        public ParticleEngine(List<Texture2D> textures, Vector2 emitterLoc)
        {
            EmitterLoc =  emitterLoc;
            this.textures = textures;
            this.particles = new List<Particle>();
            rand = new Random();
        }

        public void Update()
        {
            int totalParticles = 3;

            for (int i = 0; i < totalParticles; i++)
            {
                particles.Add(GenerateNewParticle());
            }

            for (int particle = 0; particle < particles.Count; particle++)
            {
                particles[particle].Update();
                if (particles[particle].TTL <= 0)
                {
                    particles.RemoveAt(particle);
                    particle--;
                }
            }
        }

        private Particle GenerateNewParticle()
        {
            Texture2D texture = textures[rand.Next(textures.Count)];
            Vector2 position = EmitterLoc;
            Vector2 velocity = new Vector2(
                                    1f * (float)(rand.NextDouble() * 1 - 1),
                                    1f * (float)(rand.NextDouble() * 1 - 1));
            float angle = 0;
            float angularVelocity = 0.1f * (float)(rand.NextDouble() * 1 - 1);
            Color color = new Color(
                        (float)rand.NextDouble(),
                        (float)rand.NextDouble(),
                        (float)rand.NextDouble());
            float size = (float)rand.NextDouble();
            int ttl = 6 + rand.Next(5);
            return new Particle(texture, position, velocity, angle, angularVelocity, color, size, ttl);
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
