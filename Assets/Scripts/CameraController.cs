using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public float dumping = 1.5F;
    public Vector2 offset = new Vector2(2f, 1f);
    public bool isLeft;
    private Transform character;
    private int lastX;

    void Start() {
        offset = new Vector2(Mathf.Abs(offset.x), offset.y);
        FindCharacter(isLeft);
    }

    void Update()
    {
        if (character)
        {
            int currentX = Mathf.RoundToInt(character.position.x);

            if (currentX > lastX) isLeft = false;
            else if (currentX < lastX) isLeft = true;

            lastX = Mathf.RoundToInt(character.position.x);

            Vector3 target;

            if (isLeft)
            {
                target = new Vector3(character.position.x - offset.x, character.position.y + offset.y, character.position.z - 10);
            }
            else
            {
                target = new Vector3(character.position.x + offset.x, character.position.y + offset.y, character.position.z - 10);
            }

            Vector3 currentPosition = Vector3.Lerp(transform.position, target, dumping * Time.deltaTime);
            transform.position = currentPosition;
        }
    }

    public void FindCharacter(bool playerIsLeft)
    {
        character = GameObject.FindGameObjectWithTag("Character").transform;
        lastX = Mathf.RoundToInt(character.position.x);

        if (playerIsLeft)
        {
            transform.position = new Vector3(character.position.x - offset.x, character.position.y - offset.y, character.position.z - 10);
        }
        else
        {
            transform.position = new Vector3(character.position.x + offset.x, character.position.y + offset.y, character.position.z - 10);
        }
    }


}
