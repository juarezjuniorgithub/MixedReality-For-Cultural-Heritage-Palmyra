using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AroundCubeSnapping : MonoBehaviour
{
    [SerializeField] private BoxCollider boxCollider;
    [SerializeField] private Camera mainCamera;
    private GameObject[] sides = new GameObject[4];
    [SerializeField] GameObject panel;
    [SerializeField] private float refreshrate = 0.2f;
    private float refreshrateCounter;
    // Start is called before the first frame update
    void Start()
    {
        if(boxCollider == null)
        {
            boxCollider = GetComponent<BoxCollider>();
        }

        for (int i = 0; i < sides.Length; i++)
        {
            sides[i] = new GameObject();
            sides[i].transform.parent = this.transform;
            sides[i].name = "Side " + i.ToString();
        }
        float sideX = boxCollider.size.x/2;
        float sideZ = boxCollider.size.z/2;
        sides[0].transform.localPosition = transform.localPosition + new Vector3(sideX, 0, 0);
        sides[1].transform.localPosition = transform.localPosition + new Vector3(-sideX, 0, 0);
        sides[2].transform.localPosition = transform.localPosition + new Vector3(0, 0, sideZ);
        sides[3].transform.localPosition = transform.localPosition + new Vector3(0, 0, -sideZ);
    }

    // Update is called once per frame
    void Update()
    {
        refreshrateCounter += Time.deltaTime;
        if(refreshrateCounter >= refreshrate)
        {
            float minDistance = 99999;
            float currentDistance;
            int closestSideIndex = 0;
            for (int i = 0; i < sides.Length; i++)
            {
                currentDistance = Vector3.Distance(mainCamera.transform.position, sides[i].transform.position);
                if(currentDistance < minDistance)
                {
                    minDistance = currentDistance;
                    closestSideIndex = i;
                }
            }
            panel.transform.localPosition = sides[closestSideIndex].transform.localPosition;
            switch (closestSideIndex)
            {
                case (0):
                    panel.transform.forward = -transform.right;
                    break;
                case (1):
                    panel.transform.forward = transform.right;
                    break;
                case (2):
                    panel.transform.forward = -transform.forward;
                    break;
                case (3):
                    panel.transform.forward = transform.forward;
                    break;
                default:
                    break;
            }
            refreshrateCounter = 0;
        }
    }
}
