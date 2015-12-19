﻿using System;
using EatMe.UnifiedClasses;
using ECS;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EatMe.Components
{
	public class SpriteRenderer : Component, IDrawableComponent
	{
		public int DrawLayer { get; set; }
		public Texture2D Texture { get; set; }
		public Vector2 Origin { get; set; }
		public Color Color { get; set; }
		public float Transperency { get; set; }

		private Transform _transform;

		public SpriteRenderer()
		{
			Texture = null;
			Color = Color.White;
			Transperency = 1.0f;
		}

		public int Width => Texture?.Width ?? 0;
		public int Height => Texture?.Height ?? 0;

		public override void Start()
		{
			_transform = Entity.GetComponent<Transform>();
		}

		public SpriteRenderer(string filename)
		{
			Texture = Resources.GetTexture(filename);
			Origin = new Vector2(Texture.Width/2, Texture.Height/2);
			Color = Color.White;
			Transperency = 1.0f;
		}

		public void Draw()
		{
			//TODO: Ask about && and function calls
			if(Texture != null && _transform != null && IsInView())
			{
				Main.SpriteBatch.Draw(Texture, _transform.Position, null, Color * Transperency, (float)_transform.Rotation, Origin, _transform.Scale, SpriteEffects.None, 0.1f);
			}
		}

		private bool IsInView()
		{
			var maxDimension = Math.Max(Width, Height);
			Vector2 pos = _transform.Position;
//			if(Main.MainCamera.)
			//TODO: Make Camera check for point in view with transofrmations
			return true;
		}
	}
}
