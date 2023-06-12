﻿using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using URandom = UnityEngine.Random;

public class Utils
{
    public static NormalItem.eNormalType GetRandomNormalType()
    {
        Array values = Enum.GetValues(typeof(NormalItem.eNormalType));
        NormalItem.eNormalType result = (NormalItem.eNormalType)values.GetValue(URandom.Range(0, values.Length));

        return result;
    }
    
    public static NormalItem.eNormalType GetRandomNormalTypeExcept(NormalItem.eNormalType[] types)
    {
        List<NormalItem.eNormalType> list = Enum.GetValues(typeof(NormalItem.eNormalType)).Cast<NormalItem.eNormalType>().Except(types).ToList();

        int rnd = URandom.Range(0, list.Count);
        NormalItem.eNormalType result = list[rnd];

        return result;
    }

    public static NormalItem.eNormalType GetTheLeastNormalTypeExcept(Board board, Cell cell)
    {
        NormalItem.eNormalType[] types = cell.GetSurroundingCellTypes().ToArray();
        List<NormalItem.eNormalType> listPossibleTypes = Enum.GetValues(typeof(NormalItem.eNormalType)).Cast<NormalItem.eNormalType>().Except(types).ToList();

        board.UpdateTypeNumberSummary();
        var sortedSummary = board.TypeNumberSummary.OrderBy(item => item.Value);
        
        List<NormalItem.eNormalType> listToRandomize = 
            sortedSummary.Select(item => MatchKeyToNormalType(item.Key))
                         .Where(type => listPossibleTypes.Contains(type))
                         .ToList();

        const int priorityWeight = 2;
        int rnd = URandom.Range(0, listToRandomize.Count - priorityWeight);
        return listToRandomize[rnd];
    }

    private static NormalItem.eNormalType MatchKeyToNormalType(string key)
    {
        var result = key switch
        {
            Constants.PREFAB_NORMAL_TYPE_ONE => NormalItem.eNormalType.TYPE_ONE,
            Constants.PREFAB_NORMAL_TYPE_TWO => NormalItem.eNormalType.TYPE_TWO,
            Constants.PREFAB_NORMAL_TYPE_THREE => NormalItem.eNormalType.TYPE_THREE,
            Constants.PREFAB_NORMAL_TYPE_FOUR => NormalItem.eNormalType.TYPE_FOUR,
            Constants.PREFAB_NORMAL_TYPE_FIVE => NormalItem.eNormalType.TYPE_FIVE,
            Constants.PREFAB_NORMAL_TYPE_SIX => NormalItem.eNormalType.TYPE_SIX,
            Constants.PREFAB_NORMAL_TYPE_SEVEN => NormalItem.eNormalType.TYPE_SEVEN,
            _ => NormalItem.eNormalType.TYPE_ONE
        };
        return result;
    }

    public static string GetSpriteNameFromTypeItem(string typeItem)
    {
        string spriteName = typeItem switch
        {
            // Normal items
            Constants.PREFAB_NORMAL_TYPE_ONE => Constants.SPRITE_NORMAL_TYPE_ONE,
            Constants.PREFAB_NORMAL_TYPE_TWO => Constants.SPRITE_NORMAL_TYPE_TWO,
            Constants.PREFAB_NORMAL_TYPE_THREE => Constants.SPRITE_NORMAL_TYPE_THREE,
            Constants.PREFAB_NORMAL_TYPE_FOUR => Constants.SPRITE_NORMAL_TYPE_FOUR,
            Constants.PREFAB_NORMAL_TYPE_FIVE => Constants.SPRITE_NORMAL_TYPE_FIVE,
            Constants.PREFAB_NORMAL_TYPE_SIX => Constants.SPRITE_NORMAL_TYPE_SIX,
            Constants.PREFAB_NORMAL_TYPE_SEVEN => Constants.SPRITE_NORMAL_TYPE_SEVEN,
            
            // Bonus items
            Constants.PREFAB_BONUS_HORIZONTAL => Constants.SPRITE_BONUS_HORIZONTAL,
            Constants.PREFAB_BONUS_VERTICAL => Constants.SPRITE_BONUS_VERTICAL,
            Constants.PREFAB_BONUS_BOMB => Constants.SPRITE_BONUS_BOMB,
            _ => string.Empty
        };

        return spriteName;
    }
}
