using UnityEngine;

namespace RhythmGame.Interfaces
{
    public interface ILinesSubject
    {
        public void RegisterLine(ILine line);

        public void RemoveLine(ILine line);
        //public Line NotifyListeners(int index);
        public void NotifyListeners();
        public Line NotifyListeners(Vector3 hitPoint, float width, int touchId, bool touchCountChanged);
    }
}