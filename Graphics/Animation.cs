using System;
using System.Collections.Generic;

namespace MonoGameLibrary.Graphics;

public class Animation
{
    /// <summary>
    /// The texture regions that make up the frames of this animation.  The order of the regions within the collection
    /// are the order that the frames should be displayed in.
    /// </summary>
    public List<TextureRegion> Frames { get; set; }

    /// <summary>
    /// The amount of time to delay between each frame before moving to the next frame for this animation.
    /// </summary>
    public TimeSpan Delay { get; set; }

    /// <summary>
    /// The amount of time to delay between each frame before moving to the next frame for this animation.
    /// </summary>
    public bool IsLooping { get; set; }

    /// <summary>
    /// Creates a new animation.
    /// </summary>
    public Animation()
    {
        Frames = new List<TextureRegion>();
        Delay = TimeSpan.FromMilliseconds(100);
        IsLooping = true;
    }

    /// <summary>
    /// Creates a new animation with the specified frames and delay.
    /// </summary>
    /// <param name="frames">An ordered collection of the frames for this animation.</param>
    /// <param name="delay">The amount of time to delay between each frame of this animation.</param>
    /// <param name="loop">Whether or not this animation should loop.</param>
    public Animation(List<TextureRegion> frames, TimeSpan delay, bool loop)
    {
        Frames = frames;
        Delay = delay;
        IsLooping = loop;        
    }

}
