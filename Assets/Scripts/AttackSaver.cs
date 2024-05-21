using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSaver : MonoBehaviour
{
    public List<SaveActions> characterActionsContainer = new List<SaveActions>();

    //save the move used by the character 
    public void SaveMove(CharacterTemplate user, Moves move, CharacterTemplate target){
        //characterActionsContainer.Add(new SaveActions(user, move, target));
        Debug.Log($"Actions in the container: {characterActionsContainer.Count}");
        Debug.Log($"Move saved. {user.characterData.CharaStatList.CharacterName} used {move.MoveName} on {target.characterData.CharaStatList.CharacterName}");
    }

    //save the item used by the character
    public void SaveItem(CharacterTemplate user, ItemObject item, CharacterTemplate target){
        characterActionsContainer.Add(new SaveActions(user, item, target));
        Debug.Log($"Actions in the container: {characterActionsContainer.Count}");
        Debug.Log($"Item saved. {user.characterData.CharaStatList.CharacterName} used {item.ItemName} on {target.characterData.CharaStatList.CharacterName}");
    }

    //class that saves the actions of the characters in battle
    public class SaveActions{
        public Moves move;
        public ItemObject item;
        public CharacterTemplate user;
        public CharacterTemplate target;

        //constructor for saving moves
        public SaveActions(CharacterTemplate _user, Moves _move, CharacterTemplate _target){
            user = _user;
            move = _move;
            target = _target;
        }

        //constructor for saving items
        public SaveActions(CharacterTemplate _user, ItemObject _item, CharacterTemplate _target){
            user = _user;
            item = _item;
            target = _target;
        }
    }
}


