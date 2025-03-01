using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ResponseMatcherFunction : MonoBehaviour
{
    public Response[] responses;
    public static ResponseMatcherFunction Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

    }


    public void UpdateResponse()
    {
        for (int i = 0; i < responses.Length; i++)
        {
            bool matched = false;

            if (CharacterManager.Instance.selectedCharacter != null)
            {
                for (int j = 0; j < responses[i].behaviourTypes.Length; j++)
                {
                    if (CharacterManager.Instance.selectedCharacter.curBehaviour.type == responses[i].behaviourTypes[j])
                    {

                        matched = true;
                        break;
                    }

                }
            }
            

            responses[i].gameObject.SetActive(matched);
        }
     
    }

}
