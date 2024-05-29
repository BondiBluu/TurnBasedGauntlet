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
        DamagingMoves damagingMove = (DamagingMoves)move;
        int damage = 0;

        switch(damagingMove.AtkType){
            case DamagingMoves.AttackType.Physical:
                damage = (int)Math.Ceiling(user.currentAttack * damagingMove.MovePower / (2 * Mathf.Max(1, target.currentDefense)));
                break;
            case DamagingMoves.AttackType.Magical:
                damage = (int)Math.Ceiling(user.currentMagic * damagingMove.MovePower / (2 * Mathf.Max(1, target.currentResistance)));
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

    public (int hp, int mp) CalcPotions(CharacterTemplate user, RestorativeObject potion, CharacterTemplate target){
        int hp = 0;
        int mp = 0;

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

        switch(move.MovesType){
            case Moves.MoveType.Supplementary:
                SupplementaryMoves supplementaryMove = (SupplementaryMoves)move;

                if(supplementaryMove.Buffs.Length > 0){
                    for(int i = 0; i < supplementaryMove.Buffs.Length; i++){
                    message += $" {supplementaryMove.Buffs[i]} increased!";
                    }
                }
            if(supplementaryMove.Debuffs.Length > 0){
                for(int i = 0; i < supplementaryMove.Debuffs.Length; i++){
                    message += $" {supplementaryMove.Debuffs[i]} decreased!";
                    }
                }
                break;
            case Moves.MoveType.Healing:
                HealingMoves healingMove = (HealingMoves)move;

                if(healingMove.Debuffs.Length > 0){
                    for(int i = 0; i < healingMove.Debuffs.Length; i++){
                    message += $" {healingMove.Debuffs[i]} back to normal!";
                    }
                }
                break;
            case Moves.MoveType.Damaging:
                DamagingMoves damagingMove = (DamagingMoves)move;
                if(damagingMove.Debuffs.Length > 0){
                    for(int i = 0; i < damagingMove.Debuffs.Length; i++){
                    message += $" {damagingMove.Debuffs[i]} decreased!";
                    }
                }
                break;
        }
                 
        return message; 
    }

        //putting everything together for the attack 
    public void OnAttack(CharacterTemplate user, Moves move, CharacterTemplate target){
        DamagingMoves damagingMove = (DamagingMoves)move;
       //if the user is friendly, mp loss and mp bar update
       
        //user animation plays
        //target pain animation plays
        //target takes damage, but if the move is drain, the target heals as well
      
        //update the attacker health bar, if draining move, update the target and user health bars
       
        
        //debuff application if any
       

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

        //status effects, if any
    }

    //only player heals
    public void OnHeal(CharacterTemplate user, Moves move, CharacterTemplate target){
        //mp loss and mp bar update
        user.TakeMP(CalcMPLoss(move));
        uiManager.UpdateMP(user); //going to be decremented
        uiManager.UpdateMPPanel(user);

        //user anim plays
        //target healed (?)anim plays
        //target heals
        target.HealDamage(CalcHealingMove(user, move, target), 0);
        //update the target health bar
        uiManager.UpdateHP(target);
        uiManager.UpdateHPPanel(target);

        //cure status, if status is matches the cure status

    
        //seconds pass
    }

    public void OnPotion(CharacterTemplate user, ItemObject item, CharacterTemplate target){
        //item object will now be restorative object
        RestorativeObject potion = (RestorativeObject)item;
        //user used this
        //user anim plays
        //target healed (?)anim plays
        //target heals
        (int hp, int mp) = CalcPotions(user, potion, target);
        target.HealDamage(hp, mp);
        //update the target health bar
        uiManager.UpdateHP(target);
        uiManager.UpdateHPPanel(target);
        uiManager.UpdateMP(target);
        uiManager.UpdateMPPanel(target);

        //cure status, if status is matches the cure status


        //can revive downed characters
    }

    public void OnStatus(CharacterTemplate user, Moves move, CharacterTemplate target){
        //mp loss and mp bar update
        user.TakeMP(CalcMPLoss(move));
        uiManager.UpdateMP(user); //going to be decremented
        uiManager.UpdateMPPanel(user);

        //user anim plays
        //if move type is supplementary target is buffed or debuffed


        //status applied to target
        //cures debuffs if the move is a cure move
        //cure status, if status is matches the cure status
        //seconds pass
    }

    public void OnStatusCure(CharacterTemplate user, Moves move, CharacterTemplate target){
        //mp loss and mp bar update


        //user anim plays
        //target healed (?)anim plays
        //target is cured of debuffs



        //cure status, if status is matches the cure status
    
        //seconds pass
    }
}
