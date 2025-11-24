using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace MonoGameLibrary.Graphics;

public class AnimatedSprite : Sprite
{
    private int _currentFrame;
    private TimeSpan _elapsed;
    private Animation _animation; //current animation

    private Dictionary<string, Animation> _animations = new();


    private string _currentAnimationName;
    public string CurrentAnimationName => _currentAnimationName;

    public AnimatedSprite() { }

    // construct with a single named animation
    public AnimatedSprite(Animation animation, string name = "default")
    {
        if (animation != null)
        {
            AddAnimation(name, animation);
            Play(name);
        }
    }

    // construct with a map of animations and start one
    public AnimatedSprite(Dictionary<string, Animation> animations, string start = null)
    {
        if (animations != null)
        {
            foreach (var kv in animations)
                AddAnimation(kv.Key, kv.Value);

            if (!string.IsNullOrEmpty(start) && _animations.ContainsKey(start))
                Play(start);
        }
    }

    // Add or replace an animation
    public void AddAnimation(string name, Animation animation)
    {
        if (string.IsNullOrEmpty(name)) throw new ArgumentException("name");
        if (animation == null) throw new ArgumentNullException(nameof(animation));

        _animations[name] = animation;
    }

    public bool RemoveAnimation(string name)
    {
        if (string.IsNullOrEmpty(name)) return false;
        return _animations.Remove(name);
    }

    /// <summary>
    /// Updates this animated sprite.
    /// </summary>
    /// <param name="gameTime">A snapshot of the game timing values provided by the framework.</param>
    public void Update(GameTime gameTime)
    {
        if (_animation == null || _animation.Frames == null || _animation.Frames.Count == 0)
            return;

        _elapsed += gameTime.ElapsedGameTime;

        while (_elapsed >= _animation.Delay)
        {
            _elapsed -= _animation.Delay;
            _currentFrame++;

            if (_currentFrame >= _animation.Frames.Count)
            {
                if (_animation.IsLooping)
                    _currentFrame = 0;
                else
                    _currentFrame = _animation.Frames.Count - 1;
            }

            Region = _animation.Frames[_currentFrame];
        }
    }

    public void Play(Animation animation)
    {
        if (animation == null) throw new ArgumentNullException(nameof(animation));
        _animation = animation;
        _currentFrame = 0;
        _elapsed = TimeSpan.Zero;

        if (_animation.Frames != null && _animation.Frames.Count > 0)
            Region = _animation.Frames[0];
    }

    public void Play(string name)
    {
        if (string.IsNullOrEmpty(name)) throw new ArgumentException("name");
        if (!_animations.ContainsKey(name)) throw new KeyNotFoundException($"Animation '{name}' not found.");

        // avoid resetting if the same animation is already playing
        if (_currentAnimationName == name) return;

        Play(_animations[name]);
        _currentAnimationName = name;
    }

}
