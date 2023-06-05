using UnityEngine;
using UnityEngine.U2D;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "SpriteAtlasRef", menuName = "ScriptableObjects/SpriteAtlasRef", order = 0)]
    public class SpriteAtlasRef : ScriptableObject
    {
        public SpriteAtlas Items;

        public enum TypeOfItem
        {
            Character,
            Fish,
        }
    }
}