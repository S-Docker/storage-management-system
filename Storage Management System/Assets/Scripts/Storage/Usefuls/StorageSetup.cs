using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageSetup : MonoBehaviour
{
    public void SetupStorage(bool isDepositable, int storageSize) {
        StorageData dataScript;
        StorageView viewScript;

        ClearOldScripts();

        if (isDepositable) {
            dataScript = this.gameObject.AddComponent<DepositableStorageData>();
        } else {
            dataScript = this.gameObject.AddComponent<StorageData>();
        }
        dataScript.StorageDataConstructor(storageSize);

        viewScript = this.gameObject.AddComponent<StorageView>();
        viewScript.StorageViewConstructor(storageSize);
    }

    void ClearOldScripts() {
        if (this.gameObject.GetComponent<StorageData>() != null) {
            DestroyImmediate(this.gameObject.GetComponent<StorageData>());
        }

        if (this.gameObject.GetComponent<StorageView>() != null) {
            DestroyImmediate(this.gameObject.GetComponent<StorageView>());
        }
    }
}
