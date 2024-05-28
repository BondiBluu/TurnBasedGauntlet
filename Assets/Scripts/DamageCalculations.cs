using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class DamageCalculations : MonoBehaviour
{
    UIManager uiManager;
    void Start(){
        uiManager = FindObjectOfType<UIManager>();
    }

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

    public int CalcTool(CharacterTemplate user, ToolObject tool, CharacterTemplate target){
        int damage = 0;

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

        message += $" {target.characterData.name}'s";

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

        //putting everything together for the attack 
    public void OnAttack(CharacterTemplate user, Moves move, CharacterTemplate target){
       //if the user is friendly, mp loss and mp bar update
        if(user.characterType == CharacterTemplate.CharacterType.Friendly){
            uiManager.UpdateMP(user); //going to be decremented
            uiManager.UpdateMPPanel(user);
        }
        //user animation plays
        //target pain animation plays
        //target takes damage, but if the move is drain, the target heals as well
        if(move.MovesType == Moves.MoveType.Drain){
            (int damage, int healing) = CalculateDrainMove(user, move, target);
            target.TakeDamage(damage);
            user.HealDamage(healing, 0);
        } else if (move.MovesType == Moves.MoveType.Damaging){
            target.TakeDamage(CalculateDamagingMove(user, move, target));
        }
        //update the attacker health bar, if draining move, update the target and user health bars
        if(target.characterType == CharacterTemplate.CharacterType.Friendly){
            //target is friendly and user is enemy
            uiManager.UpdateHP(target); //going to be decremented
            uiManager.UpdateHPPanel(target);

            if(move.MovesType == Moves.MoveType.Drain){
                //TODO: add and link enemy health bar to decrement
                uiManager.UpdateEnemyHPPanel(user);
            }
        } else if(target.characterType == CharacterTemplate.CharacterType.Enemy){
            //target is enemy and user is friendly
            uiManager.UpdateEnemyHPPanel(target);

            if(move.MovesType == Moves.MoveType.Drain){
                uiManager.UpdateHP(user);
                uiManager.UpdateHPPanel(user);
                
            }
        }
        //debuff application if any
        target.ApplyBuffandDebuff(move.Debuffs, move.DebuffValue);

        //TODO: add status to the target if any

        //seconds pass
    }

    public void OnAttack(CharacterTemplate user, ItemObject item, CharacterTemplate target){
        ToolObject tool = (ToolObject)item;
        //user used this
        //user anim plays
        //target pain anim plays
        //damage to target
        target.TakeDamage(CalcTool(user, tool, target));
        //update the target health bar
        if(target.characterType == CharacterTemplate.CharacterType.Friendly){
            uiManager.UpdateHP(target);
            uiManager.UpdateHPPanel(target);
        } else if(target.characterType == CharacterTemplate.CharacterType.Enemy){
            uiManager.UpdateEnemyHPPanel(target);
        }

        //debuff application if any
        target.ApplyBuffandDebuff(tool.Debuffs.Cast<Moves.Boost>().ToArray(), tool.DebuffAmount);
        //status effects, if any
    }
}
