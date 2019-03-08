using UnityEngine;

//移動用コマンド
public class MoveCommand : BaseCommand {

    //コマンド実行前の座標
    private Vector3 startPos = Vector3.zero;

    //コマンド実行の目標座標
    private Vector3 targetPos = Vector3.zero;

    //移動するターゲット
    private Transform moveTarget;

    //ローカル座標系かワールド座標系か
    private bool isLocal = false;

#if UNITY_EDITOR
    private GameObject debugShadow;
#endif

    //コマンドパラメーターセットアップ
    public void SetUpCommand(Vector3 TargetPos, Transform MoveTarget, bool IsLocal) {
        moveTarget = MoveTarget;

        startPos = IsLocal ? moveTarget.localPosition : moveTarget.position;

        targetPos = TargetPos;

        isLocal = IsLocal;

#if UNITY_EDITOR
        if(debugShadow != null) {
            GameObject.Destroy(debugShadow);

            debugShadow = null;
        }
#endif
    }

    //コマンド実行
    public override void Execute() {
        if(isLocal) {
            moveTarget.transform.localPosition = targetPos;
        } else {
            moveTarget.transform.position = targetPos;
        }

        base.Execute();

#if UNITY_EDITOR
        debugShadow = GameObject.CreatePrimitive(PrimitiveType.Cube);

        debugShadow.transform.position = startPos;

        debugShadow.transform.rotation = moveTarget.transform.rotation;

        debugShadow.GetComponent<MeshRenderer>().material.SetColor("_Color", new Color(1f, 0f, 0f, 1f));
#endif
    }

    //コマンドを戻す
    public override void Undo() {
        if(isLocal) {
            moveTarget.transform.localPosition = startPos;
        } else {
            moveTarget.transform.position = startPos;
        }

        base.Undo();

#if UNITY_EDITOR
        if(debugShadow != null) {
            GameObject.Destroy(debugShadow);

            debugShadow = null;
        }
#endif

    }

    ~MoveCommand() {
        moveTarget = null;
    }

}