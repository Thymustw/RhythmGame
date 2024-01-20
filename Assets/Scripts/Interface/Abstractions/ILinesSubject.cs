using UnityEngine;

namespace RhythmGame.Interfaces
{
    public interface ILinesSubject
    {
        public void RegisterLine(ILine line);

        public void RemoveLine(ILine line);

        public void NotifyListeners();
        public void NotifyListeners(int touchId, TouchPhase touchPhase, Vector3 hitPoint);
    }
}