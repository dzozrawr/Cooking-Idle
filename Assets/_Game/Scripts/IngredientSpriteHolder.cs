using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ingredients;
using System;

public class IngredientSpriteHolder : MonoBehaviour
{
    [Serializable]
    public class IngrTypeSprite
    {
        public IngredientType ingrType;
        public Sprite sprite;
    }

    public List<IngrTypeSprite> ingrTypeSpriteList = null;
    private Dictionary<IngredientType, Sprite> ingrSprites = new Dictionary<IngredientType, Sprite>();

    public Dictionary<IngredientType, Sprite> IngrSprites { get => ingrSprites; set => ingrSprites = value; }

    private void Awake()
    {
        foreach (IngrTypeSprite item in ingrTypeSpriteList)
        {
            ingrSprites.Add(item.ingrType, item.sprite);
        }
    }

    public Sprite GetIngredientSprite(IngredientType ingrType)
    {
        return ingrSprites[ingrType];
    }
}
