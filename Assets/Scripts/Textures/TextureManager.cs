using System;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.U2D;

namespace Textures
{
    public class TextureManager : MonoBehaviour
    {
        public static TextureManager Instance; 

        [FormerlySerializedAs("normalItemSpriteAtlas")] [HideInInspector] public SpriteAtlas normalItemAtlas;
        [FormerlySerializedAs("bonusItemSpriteAtlas")] [HideInInspector] public SpriteAtlas bonusItemAtlas;

        private void Awake()
        {
            GetInstance();
            SetUp();
        }

        private void SetUp()
        {
            SpriteAtlasRef spriteAtlasRef = Resources.Load<SpriteAtlasRef>(Constants.PATH_SPRITE_ATLAS_REF);

            if (spriteAtlasRef)
            {
                normalItemAtlas = spriteAtlasRef.NormalItems;
                bonusItemAtlas = spriteAtlasRef.BonusItems;
            }

            if (normalItemAtlas == null || bonusItemAtlas == null)
                Debug.LogError("No sprite atlas in Texture Manager!");
        }

        private void GetInstance()
        {
            Instance = this;
        }
    }
}