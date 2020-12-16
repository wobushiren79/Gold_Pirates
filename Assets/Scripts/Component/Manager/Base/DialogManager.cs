using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class DialogManager : BaseManager
{
    public GameObject objDialogContainer;

    public List<DialogView> listDialog = new List<DialogView>();

    public DialogView CreateDialog(DialogEnum dialogType, DialogView.IDialogCallBack callBack, DialogBean dialogBean)
    {
        return CreateDialog(dialogType, callBack, dialogBean, 0);
    }

    public DialogView CreateDialog(DialogEnum dialogType, DialogView.IDialogCallBack callBack, DialogBean dialogBean, float delayDelete)
    {
        string dialogName = EnumUtil.GetEnumName(dialogType);
        DialogView dialogModel = LoadResourcesUtil.SyncLoadData<DialogView>("UI/Dialog/" + dialogName);

        if (dialogModel)
        {
            GameObject objDialog = Instantiate(objDialogContainer, dialogModel.gameObject);
            DialogView dialogView = objDialog.GetComponent<DialogView>();
            if (dialogView == null)
                Destroy(objDialog);
            dialogView.SetCallBack(callBack);
            dialogView.SetData(dialogBean);
            if (delayDelete != 0)
                dialogView.SetDelayDelete(delayDelete);

            //改变焦点
            EventSystem.current.SetSelectedGameObject(objDialog);

            listDialog.Add(dialogView);
            Resources.UnloadUnusedAssets();
            return dialogView;
        }
        else
        {
            LogUtil.LogError("没有找到指定弹窗：" + "Resources/UI/Dialog/" + dialogName);
            return null;
        }
    }

    public void CloseAllDialog()
    {
        for (int i = 0; i < listDialog.Count; i++)
        {
            DialogView dialogView = listDialog[i];
            if (dialogView != null)
                dialogView.DestroyDialog();
        }
        listDialog.Clear();
    }

    public void RemoveDialog(DialogView dialogView)
    {
        if (dialogView != null && listDialog.Contains(dialogView))
            listDialog.Remove(dialogView);
    }
}