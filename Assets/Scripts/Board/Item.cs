using System;
using UnityEngine;
using DG.Tweening;
using ScriptableObjects;
using UnityEngine.U2D;

[Serializable]
public class Item
{
    public Cell Cell { get; private set; }

    public Transform View { get; private set; }


    public virtual void SetViewAndSprite()
    {
        string typeItem = GetTypeItem();

        if (!string.IsNullOrEmpty(typeItem))
        {
            GameObject prefab = Resources.Load<GameObject>(Constants.PREFAB_ITEM);
            if (prefab)
            {
                View = GameObject.Instantiate(prefab).transform;
                GetSpriteNameFromTypeItem(typeItem);
                SetSprite(typeItem);
            }
        }
    }

    private void GetSpriteNameFromTypeItem(string typeItem)
    {
        // Get the sprite name from the type item
        string spriteName = string.Empty;
        
        switch (typeItem)
        {
            case Constants.PREFAB_NORMAL_TYPE_ONE:
                spriteName = Constants.SPRITE_NORMAL_TYPE_ONE;
                break;
            case Constants.PREFAB_NORMAL_TYPE_TWO:
                spriteName = Constants.SPRITE_NORMAL_TYPE_TWO;
                break;
            case Constants.PREFAB_NORMAL_TYPE_THREE:
                spriteName = Constants.SPRITE_NORMAL_TYPE_THREE;
                break;
            case Constants.PREFAB_NORMAL_TYPE_FOUR:
                spriteName = Constants.SPRITE_NORMAL_TYPE_FOUR;
                break;
            case Constants.PREFAB_NORMAL_TYPE_FIVE:
                spriteName = Constants.SPRITE_NORMAL_TYPE_FIVE;
                break;
            case Constants.PREFAB_NORMAL_TYPE_SIX:
                spriteName = Constants.SPRITE_NORMAL_TYPE_SIX;
                break;
            case Constants.PREFAB_NORMAL_TYPE_SEVEN:
                spriteName = Constants.SPRITE_NORMAL_TYPE_SEVEN;
                break;
            case Constants.PREFAB_BONUS_HORIZONTAL:
                spriteName = Constants.SPRITE_BONUS_HORIZONTAL;
                break;
            case Constants.PREFAB_BONUS_VERTICAL:
                spriteName = Constants.SPRITE_BONUS_VERTICAL;
                break;
            case Constants.PREFAB_BONUS_BOMB:
                spriteName = Constants.SPRITE_BONUS_BOMB;
                break;
        }
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
            View.DOScale(0.1f, 0.1f).OnComplete(
                () =>
                {
                    GameObject.Destroy(View.gameObject);
                    View = null;
                }
                );
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
            GameObject.Destroy(View.gameObject);
            View = null;
        }
    }
}
