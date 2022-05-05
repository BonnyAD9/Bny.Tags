using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bny.Tags.ID3v2.ID3v2_3;

public class FrameCollection : IEnumerable<Frame>
{
    /// <summary>
    /// Contains all the frames
    /// </summary>
    private Dictionary<FrameID, Frame> Frames { get; init; } = new();

    /// <summary>
    /// Gets frame with the given id, if the frame is not present, returns null
    /// </summary>
    /// <param name="id">if of the frame</param>
    /// <returns>frame, null if the frame is not present</returns>
    public Frame? this[FrameID id] => Frames.GetValueOrDefault(id);

    /// <summary>
    /// Adds frame, if this frame is already in the collection and only one of them is allowed, replaces it
    /// </summary>
    /// <param name="frame">frame to add</param>
    /// <returns>true if the frame has been replaced</returns>
    public bool Add(Frame frame)
    {
        if (!Frames.ContainsKey(frame.ID))
        {
            Frames.Add(frame.ID, frame);
            return true;
        }

        if (Frames[frame.ID].TryAdd(frame))
            return true;

        Frames[frame.ID] = frame;
        return false;
    }

    /// <summary>
    /// Gets the given frame
    /// </summary>
    /// <typeparam name="T">Type of frame to get</typeparam>
    /// <param name="id">id of the frame to get</param>
    /// <returns>Frame, otherwise null</returns>
    public T? Get<T>(FrameID id) where T : Frame => this[id] as T;

    /// <summary>
    /// Tries to get the given frame
    /// </summary>
    /// <typeparam name="T">Type of the frame to get</typeparam>
    /// <param name="id">id of the frame</param>
    /// <param name="frame">resulting frame, null if this returns false</param>
    /// <returns>true if the frame was found, otherwise false</returns>
    public bool TryGet<T>(FrameID id, [NotNullWhen(true)] out T? frame) where T : Frame => (frame = this[id] as T) is not null;

    /// <summary>
    /// Tries to get the given frame
    /// </summary>
    /// <param name="id">id of the frame to get</param>
    /// <param name="frame">resulting frame, null if this returns false</param>
    /// <returns>true if the frame was found, otherwise false</returns>
    public bool TryGet(FrameID id, [NotNullWhen(true)] out Frame? frame) => (frame = this[id]) is not null;

    public IEnumerator<Frame> GetEnumerator() => Frames.Values.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => Frames.Values.GetEnumerator();
}
