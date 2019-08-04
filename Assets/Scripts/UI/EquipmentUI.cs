using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentUI : MonoBehaviour
{
    public Transform eqParent;
    public GameObject eqGO;
    private EquipmentManager eqManager;
    private EqSlot[] slots;
    
    
    // Start is called before the first frame update
    void Start()
    {
        eqManager = EquipmentManager.instance;
        eqManager.onEquipmentEquiped += UpdateUI;

        slots = eqParent.GetComponentsInChildren<EqSlot>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            eqGO.SetActive(!eqGO.activeSelf);
        }
    }

    void UpdateUI()
    {
        for (int i = 0; i < eqManager.currentEquipment.Length; i++)
        {
            if (eqManager.currentEquipment[i] != null)
            {
                slots[i].AddItem(eqManager.currentEquipment[i]);
            }
            else
            {
             //   slots[i].ClearSlot();
            }
        }
    }
}
