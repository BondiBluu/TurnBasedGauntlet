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

    //calculating damaging moves
    public (int damage, int healing) CalcDamagingMove(CharacterTemplate user, DamagingMoves dMove, CharacterTemplate target){
        int damage = CalcMove(user, dMove, target);
        int healing = 0;

        //if the move is a drain type, the user heals as well
        if(dMove.DmgType == DamagingMoves.DamagingType.Drain){
            healing = damage / 2;
        }

        Debug.Log("Damage: " + damage);

        return (damage, healing);
    }

    public int CalcMove(CharacterTemplate user, DamagingMoves dMove, CharacterTemplate target){
        int damage = 0;

        switch(dMove.AtkType){
            case DamagingMoves.AttackType.Physical:
                damage = (int)Math.Ceiling(user.currentAttack * dMove.MovePower / (2 * Mathf.Max(1, target.currentDefense)));
                break;
            case DamagingMoves.AttackType.Magical:
                damage = (int)Math.Ceiling(user.currentMagic * dMove.MovePower / (2 * Mathf.Max(1, target.currentResistance)));
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

    public int CalcHealingMove(CharacterTemplate user, HealingMoves move, CharacterTemplate target){
        //TODO: add healing move calculations. Talk with Elle
        int healing = 0;
        return healing;
    }

    public int CalcMPLoss(Moves move){
        int mpLoss = (int)move.MPCost;
        Debug.Log("MP Loss: " + mpLoss);
        return mpLoss;
    }


    //putting everything together for the attack 
    public IEnumerator OnAttack(CharacterTemplate user, Moves move, CharacterTemplate target){

        //calc the mp loss first and update the user mp bar
        if(user.characterType == CharacterTemplate.CharacterType.Friendly){
            user.TakeMP(CalcMPLoss(move));
            yield return StartCoroutine(uiManager.UpdateMP(user));
            uiManager.UpdateMPPanel(user);
        }
        yield return new WaitForSeconds(1f);
        
        DamagingMoves damagingMove = (DamagingMoves)move;
       
        //user animation plays
        //target pain animation plays
        //target takes damage, but if the move is drain, the target heals as well
        (int damage, int healing) = CalcDamagingMove(user, damagingMove, target);
        
      
        //update the attacker health bar, if draining move, update the target and user health bars
        target.TakeDamage(damage);

        //if healing is greater than 0, heal the user
        if(healing > 0){
            user.HealDamage(healing, 0);
        }
        //update the user health bar
        if(user.characterType == CharacterTemplate.CharacterType.Friendly){
            yield return StartCoroutine(uiManager.UpdateHP(user));
            uiManager.UpdateHPPanel(user);
        } else if(user.characterType == CharacterTemplate.CharacterType.Enemy){
            uiManager.UpdateEnemyHPPanel(user);
        }

        //update the target health bar
        if(target.characterType == CharacterTemplate.CharacterType.Friendly){
            yield return StartCoroutine(uiManager.UpdateHP(target));
            uiManager.UpdateHPPanel(target);
        } else if(target.characterType == CharacterTemplate.CharacterType.Enemy){
            uiManager.UpdateEnemyHPPanel(target);
        }

        yield return new WaitForSeconds(1f);
       
        
        //debuff application if any
        if(damagingMove.Debuffs.Length > 0){
            target.ApplyDebuff(damagingMove.Debuffs, damagingMove.DebuffValue);
        }       

        //TODO: add status to the target if any

        //seconds pass
    }

    public IEnumerator OnAttack(CharacterTemplate user, ItemObject item, CharacterTemplate target){
        ToolObject tool = (ToolObject)item;
        //user used this
        //user anim plays
        //target pain anim plays
        //damage to target
        target.TakeDamage(CalcTool(user, tool, target));
        //update the target health bar
        if(target.characterType == CharacterTemplate.CharacterType.Friendly){
            yield return StartCoroutine(uiManager.UpdateHP(target));
            uiManager.UpdateHPPanel(target);
        } else if(target.characterType == CharacterTemplate.CharacterType.Enemy){
            uiManager.UpdateEnemyHPPanel(target);
        }

        //debuff application if any
        if(tool.Debuffs.Length > 0){
            target.ApplyDebuff(tool.Debuffs.Cast<DamagingMoves.Debuff>().ToArray(), tool.DebuffAmount);
        }

        //status effects, if any
    }

    //only player heals
    public IEnumerator OnHeal(CharacterTemplate user, Moves move, CharacterTemplate target){
        //mp loss and mp bar update
        user.TakeMP(CalcMPLoss(move));
        yield return StartCoroutine(uiManager.UpdateMP(user));
        uiManager.UpdateMPPanel(user);

        HealingMoves healingMove = (HealingMoves)move;

        //user anim plays
        //target healed (?)anim plays
        //if healing type is revival, revive the target from downed status
        if(healingMove.HealTypes == HealingMoves.HealType.Revive){
            target.characterStatus = CharacterTemplate.CharacterStatus.Normal;
        } 
        //target heals
        int healing = CalcHealingMove(user, healingMove, target);
        target.HealDamage(healing, 0);
        //update the target health bar
        yield return StartCoroutine(uiManager.UpdateHP(target));
        uiManager.UpdateHPPanel(target);

        //reverts debuffs if any
        if(healingMove.Debuffs.Length > 0){
            target.RemoveDebuffs(healingMove.Debuffs);
        }

        //cure status, if status is matches the cure status

    
        //seconds pass
    }

    public IEnumerator OnPotion(CharacterTemplate user, ItemObject item, CharacterTemplate target){
        //item object will now be restorative object
        RestorativeObject potion = (RestorativeObject)item;
        yield return new WaitForSeconds(1f);
        //user used this
        //user anim plays
        //target healed (?)anim plays
        //target heals
        (int hp, int mp) = CalcPotions(user, potion, target);
        target.HealDamage(hp, mp);
        
        //update the target health bar
        yield return StartCoroutine(uiManager.UpdateHP(target));
        uiManager.UpdateHPPanel(target);
        yield return StartCoroutine(uiManager.UpdateMP(target));
        uiManager.UpdateMPPanel(target);

        //cure status, if status is matches the cure status


        //can revive downed characters
    }

    public IEnumerator OnStatus(CharacterTemplate user, Moves move, CharacterTemplate target){
        //mp loss and mp bar update
        user.TakeMP(CalcMPLoss(move));

        yield return StartCoroutine(uiManager.UpdateMP(user));
        uiManager.UpdateMPPanel(user);

        //user anim plays
        //if move type is supplementary target is buffed or debuffed
        SupplementaryMoves supplementaryMove = (SupplementaryMoves)move;
        if(supplementaryMove.Debuffs.Length > 0){
            target.ApplyDebuff(supplementaryMove.Debuffs.Cast<DamagingMoves.Debuff>().ToArray(), supplementaryMove.DebuffValue);
        } else if(supplementaryMove.Buffs.Length > 0){
            
        }


        //status applied to target
        //cures debuffs if the move is a cure move
        //cure status, if status is matches the cure status
        //seconds pass
    }
}
