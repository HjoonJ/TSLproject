using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.OnScreen.OnScreenStick;

public class CharacterManager : MonoBehaviour
{
    public static CharacterManager Instance;

    public Character[] characters; // 게임에서 사용"할 수" 있는 모든 캐릭터 
    public List<Character> usingCharacters = new List<Character>(); //게임에서 사용"중인" 캐릭터 

    public Character selectedCharacter;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        characters = FindObjectsOfType<Character>();

    }

    public void Start()
    {
        for (int i = 0; i < characters.Length; i++)
        {
            usingCharacters.Add(characters[i]);

        }
        
    }

    public Character GetCharacter(CharacterType type)
    {
        for (int i = 0; i < characters.Length; i++)
        {
            if (characters[i].type == type)
            {
                return characters[i];
            }

        }
        return null;
    }

}

public enum CharacterType
{
   Character1,
   Character2, 
   Character3,
}
