using Map.Data;
using UnityEngine;

namespace Map.Object
{
    public class Block : MonoBehaviour
    {
        [SerializeField]
        private GameObject[] _walls;

        [SerializeField]
        private GameObject _debugFloor;

        public void SetActive(bool isActive) => gameObject.SetActive(isActive);

        public void SetDebugFloorActive(bool isActive) => _debugFloor.SetActive(isActive);

        public void Setup(BlockData data)
        {
            _walls[WallSide.TOP].SetActive(data.WallActiveSides[WallSide.TOP]);
            _walls[WallSide.BOTTOM].SetActive(data.WallActiveSides[WallSide.BOTTOM]);
            _walls[WallSide.LEFT].SetActive(data.WallActiveSides[WallSide.LEFT]);
            _walls[WallSide.RIGHT].SetActive(data.WallActiveSides[WallSide.RIGHT]);
        }

        public void HideAllWalls()
        {
            foreach(var wall in _walls)
            {
                wall.SetActive(false);
            }
        }
    }
}