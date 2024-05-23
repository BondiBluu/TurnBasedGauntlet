using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DamageCalculations : MonoBehaviour
{
    public int CalculateDamagingMove(CharacterTemplate user, Moves move, CharacterTemplate target){
        return CalcMove(user, move, target);
    }

    public (int damage, int healing) CalculateDrainMove(CharacterTemplate user, Moves move, CharacterTemplate target){
        int damage = CalcMove(user, move, target);
        int healing = damage / 2;
        return (damage, healing);
    }

    public int CalcMove(CharacterTemplate user, Moves move, CharacterTemplate target){
        int damage = 0;

        switch(move.AttackingType){
            case Moves.AttackType.Physical:
                damage = (int)Math.Ceiling(user.currentAttack * move.MovePower / (2 * Mathf.Max(1, target.currentDefense)));
                break;
            case Moves.AttackType.Magical:
                damage = (int)Math.Ceiling(user.currentMagic * move.MovePower / (2 * Mathf.Max(1, target.currentResistance)));
                break;
        }

        //if damage is less than 1, set it to 1
        if(damage < 1){
            damage = 1;
        }

        return damage;
    }

    public int CalcTool(CharacterTemplate user, ItemObject item, CharacterTemplate target){
        int damage = 0;

        ToolObject tool = (ToolObject)item;

        if(tool.AtkPwr > 0){
            damage = (int)Math.Ceiling(tool.AtkPwr + (.15f * user.currentEfficiency));
        } else if(tool.MagPwr > 0){
            damage = (int)Math.Ceiling(tool.MagPwr + (.15f * user.currentEfficiency));
        }

        //if damage is less than 1, set it to 1
        if(damage < 1){
            damage = 1;
        }

        return damage;
    }

    public (int hp, int mp) CalcPotions(CharacterTemplate user, ItemObject item, CharacterTemplate target){
        int hp = 0;
        int mp = 0;

        RestorativeObject potion = (RestorativeObject)item;

        if(potion.HpRestore > 0){
            hp = (int)Math.Ceiling(potion.HpRestore + (.15f * user.currentEfficiency));
        }

        if(potion.MpRestore > 0){
            mp = (int)Math.Ceiling(potion.MpRestore + (.15f * user.currentEfficiency));
        }
        
        return (hp, mp);
    }

    public int CalcHealingMove(CharacterTemplate user, Moves move, CharacterTemplate target){
        //TODO: add healing move calculations. Talk with Elle
        int healing = 0;
        return healing;
    }

    public int CalcMPLoss(Moves move){
        int mpLoss = (int)move.MPCost;
        return mpLoss;
    }

    public string DescribeBuffsAndDebuffs(CharacterTemplate user, Moves move, CharacterTemplate target){
        string message = "";

        message += $"{user.characterData.name} used {move.MoveName} on {target.characterData.name}! {target.characterData.name}'s";

        if(move.Buffs.Length > 0){
            for(int i = 0; i < move.Buffs.Length; i++){
                message += $" {move.Buffs[i]} increased!";
            }
        }

        if(move.Debuffs.Length > 0){
            for(int i = 0; i < move.Debuffs.Length; i++){
                message += $" {move.Debuffs[i]} decreased!";
            }
        }
                 
        return message; 
    }
}
