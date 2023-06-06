using System;
using UnityEngine;
using DG.Tweening;
using ScriptableObjects;
using UnityEngine.U2D;
using Utilities;

[Serializable]
public class Item
{
    public Cell Cell { get; private set; }

    public Transform View { get; private set; }


    public virtual void InitViewAndSetSprite()
    {
        string typeItem = GetTypeItem();

        if (!string.IsNullOrEmpty(typeItem))
        {
            GameObject prefab = Resources.Load<GameObject>(Constants.PREFAB_ITEM);
            if (prefab)
            {
                // View = GameObject.Instantiate(prefab).transform;
                
                GameObject item = ObjectPool.SharedInstance.GetPooledObject(); 
                if (item != null) {
                    View = item.transform;
                    item.SetActive(true);
                }
                
                SetSprite(GetSpriteNameFromTypeItem(typeItem));
            }
        }
    }

    private static string GetSpriteNameFromTypeItem(string typeItem)
    {
        string spriteName = typeItem switch
        {
            Constants.PREFAB_NORMAL_TYPE_ONE => Constants.SPRITE_NORMAL_TYPE_ONE,
            Constants.PREFAB_NORMAL_TYPE_TWO => Constants.SPRITE_NORMAL_TYPE_TWO,
            Constants.PREFAB_NORMAL_TYPE_THREE => Constants.SPRITE_NORMAL_TYPE_THREE,
            Constants.PREFAB_NORMAL_TYPE_FOUR => Constants.SPRITE_NORMAL_TYPE_FOUR,
            Constants.PREFAB_NORMAL_TYPE_FIVE => Constants.SPRITE_NORMAL_TYPE_FIVE,
            Constants.PREFAB_NORMAL_TYPE_SIX => Constants.SPRITE_NORMAL_TYPE_SIX,
            Constants.PREFAB_NORMAL_TYPE_SEVEN => Constants.SPRITE_NORMAL_TYPE_SEVEN,
            Constants.PREFAB_BONUS_HORIZONTAL => Constants.SPRITE_BONUS_HORIZONTAL,
            Constants.PREFAB_BONUS_VERTICAL => Constants.SPRITE_BONUS_VERTICAL,
            Constants.PREFAB_BONUS_BOMB => Constants.SPRITE_BONUS_BOMB,
            _ => string.Empty
        };

        return spriteName;
    }

    private void SetSprite(string spriteName)
    {
        // Get reference to the SpriteAtlasRef scriptable object
        SpriteAtlasRef spriteAtlasRef = Resources.Load<SpriteAtlasRef>(Constants.PATH_SPRITE_ATLAS_REF);
        if (spriteAtlasRef)
        {
            // Get the sprite atlas from the scriptable object
            SpriteAtlas spriteAtlas = spriteAtlasRef.Items;
            if (spriteAtlas)
            {
                // Get the sprite from the sprite atlas
                Sprite sprite = spriteAtlas.GetSprite(spriteName);
                if (sprite)
                {
                    // Get the sprite renderer from the view
                    SpriteRenderer spriteRenderer = View.GetComponent<SpriteRenderer>();
                    if (spriteRenderer)
                    {
                        // Set the sprite to the sprite renderer
                        spriteRenderer.sprite = sprite;
                    }
                }
            }
        }
    }

    protected virtual string GetTypeItem() { return string.Empty; }

    public virtual void SetCell(Cell cell)
    {
        Cell = cell;
    }

    internal void AnimationMoveToPosition()
    {
        if (View == null) return;

        View.DOMove(Cell.transform.position, 0.2f);
    }

    public void SetViewPosition(Vector3 pos)
    {
        if (View)
        {
            View.position = pos;
        }
    }

    public void SetViewRoot(Transform root)
    {
        if (View)
        {
            View.SetParent(root);
        }
    }

    public void SetSortingLayerHigher()
    {
        if (View == null) return;

        SpriteRenderer sp = View.GetComponent<SpriteRenderer>();
        if (sp)
        {
            sp.sortingOrder = 1;
        }
    }


    public void SetSortingLayerLower()
    {
        if (View == null) return;

        SpriteRenderer sp = View.GetComponent<SpriteRenderer>();
        if (sp)
        {
            sp.sortingOrder = 0;
        }

    }

    internal void ShowAppearAnimation()
    {
        if (View == null) return;

        Vector3 scale = View.localScale;
        View.localScale = Vector3.one * 0.1f;
        View.DOScale(scale, 0.1f);
    }

    internal virtual bool IsSameType(Item other)
    {
        return false;
    }

    internal virtual void ExplodeView()
    {
        if (View)
        {
            View.DOMoveY(-8, 1f).OnComplete( () =>
                {
                    HideItem();
                    View = null;
                });
        }
    }

    internal void AnimateForHint()
    {
        if (View)
        {
            View.DOPunchScale(View.localScale * 0.1f, 0.1f).SetLoops(-1);
        }
    }

    internal void StopAnimateForHint()
    {
        if (View)
        {
            View.DOKill();
        }
    }

    internal void Clear()
    {
        Cell = null;

        if (View)
        {
            HideItem();
            View = null;
        }
    }

    private void HideItem()
    {
        if (View)
        {
            View.gameObject.SetActive(false);
            View.gameObject.transform.localScale = Vector3.one;
            View.SetParent(ObjectPool.SharedInstance.transform); // To prevent being destroyed when Board is destroyed.
        }
    }
}
